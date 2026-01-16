using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class PlayerMovement : MonoBehaviour
{
    #region vars
    [Header("References")]
    public PlayerMovementStats movementStats;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;

    private Rigidbody2D _rb;

    //movement vars
    private Vector2 _moveVelocity;
    private bool _isFacingRight;

    private PlayerInputActions input;

    //collision check vars
    private RaycastHit2D _groundHit;
    private RaycastHit2D _headHit;
    private bool _isGrounded;
    private bool _bumpedhead;

    //jump vars
    public float VerticalVelocity { get; private set; }
    private bool _isJumping;
    private bool _isFastFalling;
    private bool _isFalling;
    private float _fastFallTime;
    private float _fastFallReleaseSpeed;
    private int _numberOfJumpsUsed;
    private bool jumpPressed;
    private bool jumpReleased;

    //apex vars
    private float _apexPoint;
    private float _timePastApexThreshold;
    private bool _isPastApexThreshold;

    //jump buffer vars
    private float _jumpBufferTimer;
    private bool _jumpReleasedDuringBuffer;

    //coyote time vars
    private float _coyoteTimer;

    #endregion
    #region Updates
    private void Awake()
    {
        input = new PlayerInputActions();
        input.Player.Enable();
        _isFacingRight = true;

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CountTimers();
        JumpChecks();
    }

    private void FixedUpdate()
    {
        CollisionChecks();
        Jump();

        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();

        if (_isGrounded )
        {
            Move(movementStats.GroundAcceleration, movementStats.GroundDeceleration, moveInput);
        }
        else
        {
            Move(movementStats.AirAcceleration, movementStats.AirDeceleration, moveInput);
        }
    }
    #endregion
    #region Movement
    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            TurnCheck(moveInput);

            Vector2 targetVelocity = Vector2.zero;
            if (input.Player.Run.IsPressed())
            {
                targetVelocity = new Vector2(moveInput.x, 0f) * movementStats.MaxRunSpeed;
            }
            else { targetVelocity = new Vector2(moveInput.x,0f) * movementStats.MaxWalkSpeed; }

            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
        }

        else if (moveInput == Vector2.zero)
        {
            _moveVelocity = Vector2.Lerp(_moveVelocity,Vector2.zero, deceleration* Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
        }
    }
    private void TurnCheck(Vector2 moveInput)
    {
        if (_isFacingRight && moveInput.x < 0)
        {
            Turn(false);
        }
        else if (!_isFacingRight && moveInput.x > 0)
        {
            Turn(true);
        }
    }
    private void Turn(bool turnRight)
    {
        if (turnRight)
        {
            _isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }
        else
        {
            _isFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }
    #endregion

    #region Jump
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Recognising jump");
            jumpPressed = true;
            jumpReleased = false;
        }
        else
        {
            jumpPressed = false;
            jumpReleased = true;
        }
    }
    private void JumpChecks()
    {
        //jump pressed
        if (jumpPressed)
        {
            _jumpBufferTimer = movementStats.JumpBufferTime;
            _jumpReleasedDuringBuffer = false;
        }
        //jump released
        if (jumpReleased)
        {
            if(_jumpBufferTimer > 0f)
            {
                _jumpReleasedDuringBuffer = true;
            }

            if (_isJumping && VerticalVelocity > 0f)
            {
                if (_isPastApexThreshold)
                {
                    _isPastApexThreshold = false;
                    _isFastFalling = true;
                    _fastFallTime = movementStats.TimeForUpwardsCancel;
                    VerticalVelocity= 0f;
                }
                else
                {
                    _isFastFalling = true;
                    _fastFallReleaseSpeed = VerticalVelocity;
                }
            }
        }

        //initiate jump with jump buffering and coyote time
        if (_jumpBufferTimer > 0f && !_isJumping && (_isGrounded || _coyoteTimer > 0f))
        {
            InitiateJump(1);

            if (_jumpReleasedDuringBuffer)
            {
                _isFastFalling= true;
                _fastFallReleaseSpeed= VerticalVelocity;
            }
        }

        //double jump
        else if (_jumpBufferTimer > 0f && _isJumping && _numberOfJumpsUsed < movementStats.NumberOfJumpsAllowed)
        {
            _isFastFalling = false;
            InitiateJump(1);
        }

        //air jump after coyote time lapsed
        else if (_jumpBufferTimer > 0f && _isFalling && _numberOfJumpsUsed < movementStats.NumberOfJumpsAllowed - 1)
        {
            InitiateJump(2);
            _isFastFalling = false;
        }

        //landed
        if ((_isJumping || _isFalling) && _isGrounded && VerticalVelocity <= 0f)
        {
            _isJumping = false;
            _isFalling = false;
            jumpPressed = false;
            jumpReleased = false;
            _isFastFalling = false;
            _fastFallTime = 0f;
            _isPastApexThreshold = false;
            _numberOfJumpsUsed= 0;

            VerticalVelocity = Physics2D.gravity.y;
        }
    }

    private void InitiateJump(int numberOfJumpsUsed)
    {
        if (!_isJumping)
        {
            _isJumping = true;
        }

        _jumpBufferTimer = 0f;
        jumpPressed = false;
        _numberOfJumpsUsed += numberOfJumpsUsed;
        VerticalVelocity = movementStats.InitialJumpVelocity;
    }

    private void Jump()
    {
        //apply gravity while jumping
        if (_isJumping)
        {
            //check for head bump
            if (_bumpedhead)
            {
                _isFastFalling = true;
            }

            //gravity on ascending
            if (VerticalVelocity >=0f)
            {
                //apex controls
                _apexPoint = Mathf.InverseLerp(movementStats.InitialJumpVelocity, 0f, VerticalVelocity);

                if(_apexPoint > movementStats.ApexThreshold)
                {
                    if(!_isPastApexThreshold)
                    {
                        _isPastApexThreshold = true;
                        _timePastApexThreshold = 0f;
                    }
                    if(_isPastApexThreshold)
                    {
                        _timePastApexThreshold += Time.fixedDeltaTime;
                        if (_timePastApexThreshold < movementStats.ApexHangTime)
                        {
                            VerticalVelocity = 0f;
                        }
                        else
                        {
                            VerticalVelocity = -0.01f;
                        }
                    }
                }
                //gravity on ascending but not past apex threshold
                else
                {
                    VerticalVelocity += movementStats.Gravity * Time.fixedDeltaTime;
                    if (_isPastApexThreshold)
                    {
                        _isPastApexThreshold = false;
                    }
                }
            }
            //gravity on descending
            else if (!_isFastFalling)
            {
                VerticalVelocity += movementStats.Gravity * movementStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }
            else if (VerticalVelocity < 0f)
            {
                if (!_isFalling)
                {
                    _isFalling = true;
                }
            }
        }

        //jump cut
        if(_isFastFalling)
        {
            if (_fastFallTime >= movementStats.TimeForUpwardsCancel)
            {
                VerticalVelocity += movementStats.Gravity * movementStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }
            else if (_fastFallTime < movementStats.TimeForUpwardsCancel)
            {
                VerticalVelocity = Mathf.Lerp(_fastFallReleaseSpeed, 0f, (_fastFallTime / movementStats.TimeForUpwardsCancel));
            }

            _fastFallTime += Time.fixedDeltaTime;
        }

        //normal gravity whilst falling
        if (!_isGrounded && !_isJumping)
        {
            if (!_isFalling)
            {
                _isFalling = true;
            }

            VerticalVelocity += movementStats.Gravity * Time.fixedDeltaTime;
        }

        //clamp fall speed
        VerticalVelocity = Mathf.Clamp(VerticalVelocity, -movementStats.MaxFallSpeed, 50f);

        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, VerticalVelocity);
    }

    #endregion

    #region Collision Checks

    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, movementStats.GroundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, movementStats.GroundDetectionRayLength, movementStats.GroundLayer);
        if (_groundHit.collider != null)
        {
            _isGrounded = true;
        }
        else { _isGrounded = false; }
    }
    private void BumpedHead()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _bodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x * movementStats.HeadWidth, movementStats.HeadDetectionRayLength);

        _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, movementStats.HeadDetectionRayLength, movementStats.GroundLayer);
        if (_headHit.collider != null)
        {
            _bumpedhead = true;
        }
        else { _bumpedhead= false; }
    }

    private void CollisionChecks()
    {
        IsGrounded();
        BumpedHead();
    }
    #endregion

    #region Timers

    private void CountTimers()
    {
        if(_jumpBufferTimer > 0)
        {
            _jumpBufferTimer -= Time.deltaTime;
        }

        if (!_isGrounded)
        {
            _coyoteTimer -= Time.deltaTime;
        }
        else { _coyoteTimer = movementStats.JumpCoyoteTime; }
    }
    #endregion
}

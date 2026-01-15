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
    public void JumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Recognising jump");
            jumpPressed = true;
            jumpReleased = false;
        }
        else if (context.canceled)
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
            }
        }

        //initiate jump with jump buffering and coyote time

        //double jump

        //air jump after coyote time lapsed

        //landed
    }

    private void Jump()
    {

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

    private void CollisionChecks()
    {
        IsGrounded();
    }
    #endregion

    #region Timers

    private void CountTimers()
    {
        _jumpBufferTimer = Time.deltaTime;

        if (!_isGrounded)
        {
            _coyoteTimer = Time.deltaTime;
        }
        else { _coyoteTimer = movementStats.JumpCoyoteTime; }
    }
    #endregion
}

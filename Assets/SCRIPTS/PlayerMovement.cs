using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public PlayerMovementStats movementStats;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;

    private Rigidbody2D _rb;

    //movement vars
    private Vector2 _moveVelocity;
    private PlayerInputActions input;
    private Vector2 moveInput;
    private bool _isFacingRight;

    //collision check vars
    private RaycastHit2D _groundfit;
    private RaycastHit2D _headfit;
    private bool _isGrounded;
    private bool _bumpedhead;

    private void Awake()
    {
        input = new PlayerInputActions();
        _isFacingRight = true;

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            //check if he needs to turn

            Vector2 targetVelocity = Vector2.zero;
            if (input.Player.Run.IsPressed())
            {
                targetVelocity = new Vector2(moveInput.x, 0f) * movementStats.MaxRunSpeed;
            }
        }
    }
}

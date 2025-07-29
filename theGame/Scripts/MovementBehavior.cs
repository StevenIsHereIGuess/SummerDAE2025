using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    [Header("GroundCheck Stuff")]
    public BoxCollider2D groundCheck;
    public BoxCollider2D wallCheck;
    public bool grounded;
    public bool OnWall;
    public LayerMask groundMask;
    public LayerMask wallMask;

    [Header("Movement")]
    public float speed;
    public float Airspeed;
    public float sprintMultiplier = 1.0f;
    public float jumpForce;
    public Rigidbody2D body;

    [Header("Wall Jump")]
    public float wallJumpHorizontalForce = 8.0f;
    private bool lastJumpInput = false;

    private bool isWallJumping = false;
    private float wallJumpTimer = 0f;
    public float wallJumpDuration = 0.2f;

    void Start()
    {
        Debug.Log("Steven Here");
    }

    void FixedUpdate()
    {
        CheckGround();
        CheckWall();
        Sprinting();

        if (!isWallJumping) // prevent overriding wall jump
        {
            MoveWInput();
        }

        Jumping();
        WallJumping();
        //MoveinAir();

        // countdown timer for wall jump
        if (isWallJumping)
        {
            wallJumpTimer -= Time.fixedDeltaTime;
            if (wallJumpTimer <= 0)
            {
                isWallJumping = false;
            }
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapArea(groundCheck.bounds.min, groundCheck.bounds.max, groundMask);
    }

    void CheckWall()
    {
        OnWall = Physics2D.OverlapArea(wallCheck.bounds.min, wallCheck.bounds.max, wallMask);
    }

    void Sprinting()
    {
        float targetMultiplier = Input.GetKey(KeyCode.X) ? 2.5f : 1.0f;
        sprintMultiplier = Mathf.Lerp(sprintMultiplier, targetMultiplier, 0.2f); // smooth transition
    }


    void MoveWInput()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            float currentSpeed = speed * sprintMultiplier;
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed, body.linearVelocity.y);
        }
        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
        }

        // Flip player sprite based on movement direction
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

    }

    void MoveinAir()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && !grounded)
        {
            float currentSpeed = Airspeed * sprintMultiplier;
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed, body.linearVelocity.y);
            Debug.Log("Air Movement is being used");
        }
        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
        }
    }

    void Jumping()
    {
        bool jumpInput = Input.GetKey(KeyCode.Z);

        if (jumpInput && !lastJumpInput && grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        }
    }

    void WallJumping()
    {
        bool jumpInput = Input.GetKey(KeyCode.Z);

        if (jumpInput && !lastJumpInput && OnWall && !grounded)
        {
            Collider2D wallCollider = Physics2D.OverlapArea(wallCheck.bounds.min, wallCheck.bounds.max, wallMask);

            if (wallCollider != null)
            {
                float wallX = wallCollider.bounds.center.x;
                float playerX = body.position.x;

                float horizontalPush;

                if (wallX > playerX)
                {
                    horizontalPush = -wallJumpHorizontalForce;
                    Debug.Log("Wall on Right, Jump Left");
                }
                else
                {
                    horizontalPush = wallJumpHorizontalForce;
                    Debug.Log("Wall on Left, Jump Right");
                }

                // Zero out velocity for clean wall jump
                body.linearVelocity = Vector2.zero;

                // Apply force for wall jump
                Vector2 jumpForceVector = new Vector2(horizontalPush, jumpForce);
                body.AddForce(jumpForceVector, ForceMode2D.Impulse);

                // Begin movement lockout
                isWallJumping = true;
                wallJumpTimer = wallJumpDuration;
            }
        }

        lastJumpInput = jumpInput;
    }
}

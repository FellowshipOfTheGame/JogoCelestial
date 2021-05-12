using System.Linq;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [Space]
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [Space]
    [SerializeField] private float wallJumpLerp = 10f;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float wallJumpDuration = .5f;
    [SerializeField] float collisionRadius = .05f;
    
    [SerializeField] private Vector2 wallJumpDirection = Vector2.up + Vector2.right;

    [Space] 
    [SerializeField] private Transform groundCheckA;
    [SerializeField] private Transform groundCheckB;
    [SerializeField] private Transform wallCheck;
    
    [SerializeField] private LayerMask collisionMask;

    private float _moveSpeed;

    private bool _onGround;
    private bool _onWall;
    private bool _wallJumped;
    private bool _canceledJump;
    private bool _facingRight = true;
    private bool _canMove = true;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_canMove)
            return;

        CheckGroundCollisions(groundCheckA, groundCheckB);
        CheckWallCollisions(wallCheck);

        if (_onGround)
        {
            _wallJumped = false;
            _canceledJump = false;
        }
        else Fall();

        Move();
    }

    private void Fall()
    {
        if (_rigidbody.velocity.y < 0f)
            _rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * fallMultiplier * Time.deltaTime);
        else if (_canceledJump)
            _rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime);
    }
    
    private void CheckGroundCollisions(Transform transformCheckA, Transform transformCheckB)
    {
        var colliders = Physics2D.OverlapAreaAll(transformCheckA.position, transformCheckB.position, collisionMask);
        _onGround = colliders.Any(t => t.gameObject != gameObject);
    }

    private void CheckWallCollisions(Transform transformCheck)
    {
        var colliders = Physics2D.OverlapCircleAll(transformCheck.position, collisionRadius, collisionMask);
        _onWall = colliders.Any(t => t.gameObject != gameObject);
    }
    
    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Move()
    {
        if (!_wallJumped)
        {
            _rigidbody.velocity = new Vector2(_moveSpeed, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity,
                new Vector2(_moveSpeed, _rigidbody.velocity.y), wallJumpLerp * Time.deltaTime);
        }

        if (_moveSpeed > 0 && !_facingRight)
            Flip();
        else if (_moveSpeed < 0 && _facingRight)
            Flip();
    }

    public void Move(float direction)
    {
        _moveSpeed = direction * speed;
    }
    
    private void CanMove() {
        _canMove = true;
    }
    
    private void Jump(Vector2 vector2)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.velocity += vector2;
    }

    public void Jump()
    {
        if (_onGround)
        {
            _onGround = false;
            
            Jump(Vector2.up * jumpForce);
        }
        else if (_onWall)
        {
            _onWall = false;
            
            var oppositeXDirection = _facingRight ? -1 : 1;
            Jump(new Vector2(wallJumpDirection.x * oppositeXDirection, wallJumpDirection.y) * wallJumpForce);
            
            Flip();
            
            _wallJumped = true;
            _canMove = false;
            
            Invoke(nameof(CanMove), wallJumpDuration);
        }
    }

    public void JumpCanceled()
    {
        _canceledJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckA.position, groundCheckB.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(wallCheck.position, collisionRadius);
    }
}

using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // Public fields
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float wallJumpLerp = 10f;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float wallJumpDuration = .5f;
    [SerializeField] float collisionRadius = .05f;
    [Tooltip("Coyote Time in Seconds")]
    [SerializeField] private float jumpGraceTime = 0.15f;
    [SerializeField] private int jumpAmmount = 2;
    [SerializeField] private float slideSpeed = 0.5f;

    [SerializeField] private Vector2 wallJumpDirection = Vector2.up + Vector2.right;

    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private int terrainDetectionCount = 3;

    [Header("Grappling Hook:")]
    public GrapplingGun grapplingGun;
    public LineRenderer m_lineRenderer;

    // Private
    private float _moveSpeed;

    [HideInInspector] public bool _onGround;
    [HideInInspector] public bool _isDead; //setado no DamageSystem.cs
    private bool _onCloud;
    private bool _onWall;
    private bool _wallJumped;
    private bool _facingRight = true;
    private bool _canMove = true;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    private float _jumpGraceTimer;
    private int _jumpCount;

    private Animator animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Decrease timers
        if (_jumpGraceTimer > 0)
        {
            _jumpGraceTimer -= Time.deltaTime;
        }

        //para grudar na parede quando estiver usando o gancho
        if(_onWall && grapplingGun.m_springJoint2D.enabled){
            animator.SetBool("naParede", true);
        }
    }

    private void FixedUpdate()
    {
        //ele nao consegue fazer nada quando esta com _isDead = true
        if (_isDead)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.gravityScale = 0;
            return;
        }
       
        if (!_canMove) return;
 

        CheckCollisions();

        if (_onGround)
        {
            _jumpGraceTimer = jumpGraceTime;
            _jumpCount = 0;
            _wallJumped = false;
        }

        if (_onWall && _rigidbody.velocity.y < 0 && _moveSpeed != 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -slideSpeed);
            animator.SetBool("naParede", true); // ANIMAÇÃO da parede fica true
        }
        else { animator.SetBool("naParede", false); } // ANIMAÇÃO da parede fica false


    Move();

        ///////////// ANIMA��ES\\\\\\\\\\\\\

        //"andando"\\
        bool estaAndando = Mathf.Abs(_rigidbody.velocity.x) >= 0.05f;
        animator.SetBool("andando", estaAndando);

        //"subindo"\\
        bool estaSubindo = _rigidbody.velocity.y >= 2f;
        animator.SetBool("subindo", estaSubindo);

        //"noAr"\\
        bool estaNoAr = _rigidbody.velocity.y < -1f && _rigidbody.velocity.y >= -6f;
        animator.SetBool("noAr", estaNoAr);

        //caindo\\
        bool estaCaindo = _rigidbody.velocity.y < -6f;
        animator.SetBool("caindo", estaCaindo);

        //aterrizagem\\
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("caindo") && _onGround)
        {
            animator.Play("aterrizagem");
        }
    }

    private void CheckCollisions()
    {
        // Ground detection
        {
            if (_onCloud) _onCloud = false;
            else _onGround = false;

            Vector2 bottomLeft = _collider.bounds.min;
            float gap = _collider.bounds.size.x / (terrainDetectionCount - 1);

            for (int i = 0; i < terrainDetectionCount; i++)
            {
                Vector2 origin = new Vector2(bottomLeft.x + i * gap, bottomLeft.y);
                _onGround = _onGround || Physics2D.Raycast(origin, Vector2.down, collisionRadius, collisionMask);
                Debug.DrawLine(origin, origin + Vector2.down * collisionRadius, _onGround ? Color.green : Color.red);
            }
        }

        // Wall detection
        {
            _onWall = false;
            Vector2 topRight = _collider.bounds.max;
            Vector2 topLeft = new Vector2(_collider.bounds.min.x, _collider.bounds.max.y);
            float gap = _collider.bounds.size.y / (terrainDetectionCount - 1);

            for (int i = 0; i < terrainDetectionCount; i++)
            {
                Vector2 originRight = new Vector2(topRight.x, topRight.y - i * gap);
                Vector2 originLeft = new Vector2(topLeft.x, topLeft.y - i * gap);
                if(_facingRight)
                    _onWall = _onWall || Physics2D.Raycast(originRight, Vector2.right, collisionRadius, collisionMask);
                else
                    _onWall = _onWall || Physics2D.Raycast(originLeft, Vector2.left, collisionRadius, collisionMask);
                Debug.DrawLine(originRight, originRight + Vector2.right * collisionRadius, _onWall ? Color.green : Color.red);
                Debug.DrawLine(originLeft, originLeft + Vector2.left * collisionRadius, _onWall ? Color.green : Color.red);
            }
        }
    }

    private void Flip()
    {
        //vira para outro lado se não tiver fazendo o wallJump
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("wallJump"))
        {
            _facingRight = !_facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
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

    private void CanMove()
    {
        _canMove = true;
    }

    private void Jump(Vector2 vector2)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.velocity += vector2;
    }

    private bool ConsumeJump()
    {
        // Walljump takes precedence if on air
        if (!_onGround && _onWall)
        {
            return false;
        }

        _jumpCount++;
        return _jumpCount < jumpAmmount;
    }

    public void Jump()
    {
        if (ConsumeJump())
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

            StartCoroutine(WallJumpAnimation());
        }
    }

    public float wallJumpAnimSec;
    private IEnumerator WallJumpAnimation()
    {
        animator.Play("wallJump");
        yield return new WaitForSeconds(wallJumpAnimSec);
        animator.Play("subindo");
    }


    //se mudar o numero do layer "Cloud", precisa mudar a "cloudLayerNumber"
    //se encostar na nuvem por cima, detecta que esta no chao
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int cloudLayerNumber = 10;
        if (collision.gameObject.layer == cloudLayerNumber)
        {
            BoxCollider2D cloudCol = collision.gameObject.GetComponent<BoxCollider2D>();
            float playerPos = _collider.transform.position.y - (_collider.size.y / 2);
            float cloudPos = cloudCol.transform.position.y + (cloudCol.size.y / 2);
            bool isPlayerUp = playerPos >= cloudPos;

            if (isPlayerUp)
            {
                _onGround = true;
                _onCloud = true;
            }
        }
    }

    //se encostar na nuvem por cima, detecta que esta no chao
    private void OnCollisionStay2D(Collision2D collision)
    {
        int cloudLayerNumber = 10;
        if (collision.gameObject.layer == cloudLayerNumber)
        {
            BoxCollider2D cloudCol = collision.gameObject.GetComponent<BoxCollider2D>();
            float playerPos = _collider.transform.position.y - (_collider.size.y / 2);
            float cloudPos = cloudCol.transform.position.y + (cloudCol.size.y / 2);
            bool isPlayerUp = playerPos >= cloudPos;

            if (isPlayerUp)
            {
                _onGround = true;
                _onCloud = true;
            }
        }
    }
}

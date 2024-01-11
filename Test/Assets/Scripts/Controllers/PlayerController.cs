using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float _jumpVelocity;
    [SerializeField] private float _moveSpeed;
    public Animator animator;
    public Transform RespawnPoint;

    [SerializeField] private LayerMask _groundlayerMask;
    [SerializeField] private Transform _groundCheck;

    private bool _isFacingRight = true;
    private bool _isPlayerJumped = false;
    private bool _isGrounded = false;


    [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] const float _groundedRadius = .001f;
    private float _horizontalMovement = .0f;
    private Vector2 _velocity = Vector2.zero;

    private Rigidbody2D _rigidbody2d;
    private BoxCollider2D _boxCollider2d;

    private void Awake()
    {
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
        _boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            _isGrounded = true;
            animator.SetBool("IsGrounded",true);
            animator.SetBool("IsJumping", false);
        }
        else
        {
            _isGrounded = false;
            animator.SetBool("IsGrounded",false);
        }

        if (_rigidbody2d.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }

        Vector3 targetVelocity = new Vector2(_horizontalMovement , _rigidbody2d.velocity.y);
        _rigidbody2d.velocity = targetVelocity;
        //_rigidbody2d.velocity = Vector2.SmoothDamp(_rigidbody2d.velocity, targetVelocity, ref _velocity, m_MovementSmoothing);


        

        if (_isGrounded && _isPlayerJumped)
        {
            _isGrounded = false;
            _isPlayerJumped = false;

            _rigidbody2d.AddForce(new Vector2(0f, _jumpVelocity));
        }

    }
    void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal")*_moveSpeed;
        if (_horizontalMovement==0f)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }

        if (_horizontalMovement > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontalMovement < 0 && _isFacingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_isGrounded)
            {
                _isPlayerJumped = true;
                animator.SetBool("IsJumping", true);
                animator.SetTrigger("Jumped");

            }
        }

            
    }
   
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = transform.localScale;
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _groundlayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
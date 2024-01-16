using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Transform RespawnPoint;
    public AudioManager audioManager;
    private Rigidbody2D _rigidbody2d;


    [Header("Movement")]
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _moveSpeed;
    private float _horizontalMovement = .0f;

    [Header("Ground checks")]
    [SerializeField] private LayerMask _groundlayerMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] float _groundedRadius = 0.5f;//Возможно стоит заметить на прямоугольник

    //Flags
    private bool _isFacingRight = true;
    private bool _isPlayerJumped = false;
    private bool _isGrounded = false;

    [Header("Combat")]
    [SerializeField] private float _attackCooldown = 3.0f;
    [SerializeField] private float _nextAttackTime = 0.0f;
    [SerializeField] private float _attackRange = 5.0f;
    [SerializeField] private int _attackDamage = 69;
    [SerializeField] private LayerMask _attackLayers;
    private Transform _attackPoint;


    private void Awake()
    {
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
        audioManager = FindFirstObjectByType<AudioManager>();
        if(audioManager is null)
        {
            Debug.LogError("Can't find audio manager");
        }
        _attackPoint =transform.Find("AttackPoint");
        if (_attackPoint is null)
        {
            Debug.LogError("Can't find attack point");
        }
        _groundCheck = transform.Find("GroundChecker");
        if (_groundCheck is null)
        {
            Debug.LogError("Can't find ground checker");
        }
        RespawnPoint = GameObject.Find("RespawnPoint").GetComponent<Transform>();
        if (RespawnPoint is null)
        {
            Debug.LogError("Can't find audio manager");
        }

    }
    void Update()
    {
        HandleMovement();
        HandleAttack();
    }
    public void Respawn()
    {
        gameObject.transform.position = RespawnPoint.position;
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
            if(!_isGrounded) animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }

        Vector3 targetVelocity = new Vector2(_horizontalMovement , _rigidbody2d.velocity.y);
        _rigidbody2d.velocity = targetVelocity;

        if (_isGrounded && _isPlayerJumped)
        {
            _isGrounded = false;
            _isPlayerJumped = false;
            _rigidbody2d.AddForce(new Vector2(0f, _jumpVelocity));
        }
    }
    
    private void HandleMovement()
    {
        _horizontalMovement = Input.GetAxis("Horizontal") * _moveSpeed;
        if (_horizontalMovement == 0f)
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
                audioManager.Play("Jump");

            }
        }
    }
   
    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z)){
             
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))//attack combo
            {
                animator.SetTrigger("Attack2");
                CheckAttack();
                _nextAttackTime = Time.time+_attackCooldown;

            }
            else if (_nextAttackTime<Time.time)//just attack 
            {
                animator.SetTrigger("Attack1");
                CheckAttack();
                _nextAttackTime = Time.time + _attackCooldown;
            }

            
        }
    }
    private void CheckAttack()
    {
        var hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackLayers);

        foreach (var item in hits)
        {
            item.GetComponent<Killable>().TakeDagage(_attackDamage);
        }
    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
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

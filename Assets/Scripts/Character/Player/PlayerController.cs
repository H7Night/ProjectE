using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : LivingEntity
{
    private Rigidbody2D _rb;
    private Animator _animator;
    public Transform groundCheck;

    public LayerMask ground;
    
    [Header("Move")] private float _moveH;
    public float moveSpeed;

    [Header("Jump")] 
    public bool doubleJump;
    private bool _isGround;
    private float _jumpForce = 7;
    private int _jumpTimes;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;
    
    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck").transform;
    }

    private void Update()
    {
        Jump();
        BetterJump();
    }
    
    void FixedUpdate()
    {
        PlayerMove();
        SwitchAnim();
        _isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    void PlayerMove()
    {
        _moveH = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        _rb.velocity = new Vector2(_moveH * moveSpeed, _rb.velocity.y);
        _animator.SetFloat("moving",Math.Abs(_moveH));
        PlayerFaceFlip();
    }

    void PlayerFaceFlip()
    {
        if(_moveH < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(_moveH > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    
    #region Jump
    void Jump()//Jump & Double Jump
    {
        if(_isGround && !doubleJump)
        {
            _jumpTimes = 1;
        }else if(_isGround && doubleJump)
        {
            _jumpTimes = 2;
        }
        if(_isGround && Input.GetKeyDown(KeyCode.Space) && _jumpTimes == 2)
        {
            //jumpParticle.Play();
            //jumpAudio.Play();
            _jumpTimes--;
            _rb.velocity = Vector2.up * _jumpForce;
            _animator.SetBool("jumping", true);
        }else if(Input.GetKeyDown(KeyCode.Space) && _jumpTimes >= 1)
        {
            _jumpTimes--;
            //jumpAudio.Play();
            _rb.velocity = Vector2.up * _jumpForce;
            _animator.SetBool("jumping", true);
        }
        
    }
    void BetterJump()
    {
        if(_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            _animator.SetBool("falling", true);
        }
        else if(_rb.velocity.y > 0 && !(Input.GetButton("Jump")))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    #endregion

    void SwitchAnim()
    {
        _animator.SetBool("idling", false);
        if(_animator.GetBool("jumping"))
        {
            if(_rb.velocity.y < 0.0f)
            {
                _animator.SetBool("jumping", false);
                _animator.SetBool("falling", true);
            }
        }
        else if(_isGround)
        {
            _animator.SetBool("falling", false);
            _animator.SetBool("idling", true);
        }
    }

}

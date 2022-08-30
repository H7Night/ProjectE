using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    [Header("Move")] private float _moveH;
    public float moveSpeed;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
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
}

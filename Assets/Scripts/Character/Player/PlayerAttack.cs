using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PolygonCollider2D _attackCollider;
    private Animator _animator;
    private float startTime = 0.2f;
    private float _time= 0.2f;
    private void Start()
    {
        _attackCollider = GetComponent<PolygonCollider2D>();
        _animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        Attack();
    }
    void Attack()
    {
        if (Input.GetMouseButton(0)) 
        {
            _animator.SetTrigger("attacking");
            StartCoroutine(StartAttack());
        }
        _attackCollider.enabled = true;
        
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(DisableHitBox());
    }
    IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(_time);
        _attackCollider.enabled = false;
    }
}

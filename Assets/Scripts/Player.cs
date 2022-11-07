using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;

    private Vector3 _moveDelta;

    private RaycastHit2D _hit;

    // Start is called before the first frame update
    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _moveDelta = new Vector3(x, y, 0);

        // Xoay người khi di chuyển phải trái
        if (_moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (_moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Tạo chuyển động
        _hit = Physics2D.BoxCast(
            transform.position, 
            _boxCollider2D.size, 
            0, 
            new Vector2(0, _moveDelta.y),
            Mathf.Abs(_moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Actor","Blocking")
        );
        if (_hit.collider == null)
        {
            transform.Translate(0, _moveDelta.y * Time.deltaTime, 0);
        }

        _hit = Physics2D.BoxCast(
            transform.position, 
            _boxCollider2D.size, 
            0, 
            new Vector2(_moveDelta.x, 0),
            Mathf.Abs(_moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Actor","Blocking")
        );
        if (_hit.collider == null)
        {
            transform.Translate(_moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
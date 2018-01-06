using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDriver : MonoBehaviour {

    public float movementSpeed;
    public float stunnedMovementSpeed;

    private Vector3 _cameraRightEdge;
    private Vector3 _cameraLeftEdge;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _isStunned;

    public bool IsStunned
    {
        set
        {
            _isStunned = value;
        }
    }

    public Vector2 MoveInput
    {
        set
        {
            _moveInput = value;
        }
    }
    
    void Start () {
        _cameraRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        _cameraLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (transform.position.x + 0.5f >= _cameraRightEdge.x)
        {
            transform.position = new Vector3(_cameraRightEdge.x - 0.5f, transform.position.y);
        }

        if (transform.position.y + 0.5f >= _cameraRightEdge.y)
        {
            transform.position = new Vector3(transform.position.x, _cameraRightEdge.y - 0.5f);
        }
        if (transform.position.x - 0.5f <= _cameraLeftEdge.x)
        {
            transform.position = new Vector3(_cameraLeftEdge.x + 0.5f, transform.position.y);
        }
        if (transform.position.y - 0.5f <= _cameraLeftEdge.y)
        {
            transform.position = new Vector3(transform.position.x, _cameraLeftEdge.y + 0.5f);
        }
    }
    
    void FixedUpdate () {
        if (_isStunned)
        {
            _rb.MovePosition((Vector2)transform.position + new Vector2(_moveInput.x * stunnedMovementSpeed * Time.fixedDeltaTime,
                _moveInput.y * stunnedMovementSpeed * Time.fixedDeltaTime));

        }
        else
        {
            _rb.MovePosition((Vector2)transform.position + new Vector2(_moveInput.x * movementSpeed * Time.fixedDeltaTime,
                _moveInput.y * movementSpeed * Time.fixedDeltaTime));

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDriver : MonoBehaviour {

    public float movementSpeed;
    public float stunnedMovementSpeed;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _isStunned;
	private ScreenBorders _screenBorders;

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
		_screenBorders = Camera.main.GetComponent<ScreenBorders>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
		Vector2 halfScale = transform.lossyScale/2;
		if (transform.position.x + halfScale.x >= _screenBorders.CameraTopRightCorner.x)
        {
			transform.position = new Vector3(_screenBorders.CameraTopRightCorner.x - halfScale.x, transform.position.y);
        }
		if (transform.position.y + halfScale.y >= _screenBorders.CameraTopRightCorner.y)
        {
			transform.position = new Vector3(transform.position.x, _screenBorders.CameraTopRightCorner.y - halfScale.y);
        }
		if (transform.position.x - halfScale.x <= _screenBorders.CameraDownLeftCorner.x)
        {
			transform.position = new Vector3(_screenBorders.CameraDownLeftCorner.x + halfScale.x, transform.position.y);
        }
		if (transform.position.y - halfScale.y <= _screenBorders.CameraDownLeftCorner.y)
        {
			transform.position = new Vector3(transform.position.x, _screenBorders.CameraDownLeftCorner.y + halfScale.y);
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

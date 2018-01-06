using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    private PongMiniGame _pongMiniGame;
    private Rigidbody2D _rb;
    private Vector2 _input;

    private float _paddleSpeed;
    private Vector3 _cameraRightEdge;
    private Vector3 _cameraLeftEdge;

    public Vector2 Input
    {
        set
        {
            _input = value;
        }
    }

    public void Init(float paddleSpeed, Vector2 cameraRightEdge, Vector2 cameraLeftEdge)
    {
        _paddleSpeed = paddleSpeed;
        _cameraRightEdge = cameraRightEdge;
        _cameraLeftEdge = cameraLeftEdge;
    }

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float halfYScale = transform.localScale.y / 2;
        if (transform.position.y + halfYScale >= _cameraRightEdge.y)
        {
            transform.position = new Vector3(transform.position.x, _cameraRightEdge.y - halfYScale);
        }
        if (transform.position.y - halfYScale <= _cameraLeftEdge.y)
        {
            transform.position = new Vector3(transform.position.x, _cameraLeftEdge.y + halfYScale);
        }

    }

    private void FixedUpdate()
    {
        _rb.MovePosition((Vector2)transform.position + new Vector2(0,
                _input.y * _paddleSpeed * Time.fixedDeltaTime));
    }
}

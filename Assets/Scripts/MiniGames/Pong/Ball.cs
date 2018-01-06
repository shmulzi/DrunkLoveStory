using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour {
    
    private float _ballInitSpeed;
    private float _ballMaxSpeed;
    private Vector3 _cameraRightEdge;
    private Vector3 _cameraLeftEdge;
    private Rigidbody2D _rb;
    private float _increaseSpeedInterval;
    private float _increaseSpeedBy;

    private float _currentSpeed;

    public void Init(float ballInitSpeed, float ballMaxSpeed, Vector2 cameraRightEdge, Vector2 cameraLeftEdge, float increaseSpeedInterval, float increaseSpeedBy)
    {
        _ballInitSpeed = ballInitSpeed;
        _ballMaxSpeed = ballMaxSpeed;
        _cameraRightEdge = cameraRightEdge;
        _cameraLeftEdge = cameraLeftEdge;
        _increaseSpeedBy = increaseSpeedBy;
        _increaseSpeedInterval = increaseSpeedInterval;
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
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * -1);
        }
        if (transform.position.y - halfYScale <= _cameraLeftEdge.y)
        {
            transform.position = new Vector3(transform.position.x, _cameraLeftEdge.y + halfYScale);
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * -1);
        }
        _rb.velocity = _currentSpeed * _rb.velocity.normalized;

    }
    
    public void Launch(Vector2 dir)
    {
        _rb.velocity = dir * _ballInitSpeed;
        _currentSpeed = _ballInitSpeed;
        StartCoroutine(IncreaseSpeedInSeconds(_increaseSpeedInterval));
    }

    public void Stop()
    {
        _currentSpeed = 0;
    }

    private IEnumerator IncreaseSpeedInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _currentSpeed += _increaseSpeedBy;
        if(_currentSpeed < _ballMaxSpeed)
        {
            yield return StartCoroutine(IncreaseSpeedInSeconds(seconds));
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "PaddleL")
        {
            transform.position = new Vector3(collision.transform.position.x + collision.transform.localScale.x / 2 + transform.localScale.x / 2, transform.position.y);
            _rb.velocity = new Vector2(_rb.velocity.x * -1, _rb.velocity.y);
        }
        if (collision.transform.name == "PaddleR")
        {
            transform.position = new Vector3(collision.transform.position.x - collision.transform.localScale.x / 2 - transform.localScale.x / 2, transform.position.y);
            _rb.velocity = new Vector2(_rb.velocity.x * -1, _rb.velocity.y);
        }
    }
}

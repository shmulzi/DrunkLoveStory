using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorders : MonoBehaviour {

	private Vector3 _cameraTopRightCorner;
	private Vector3 _cameraDownLeftCorner;

	// Use this for initialization
	void Start () {
		_cameraTopRightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
		_cameraDownLeftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 CameraTopRightCorner
	{
		get {
			return _cameraTopRightCorner;
		}
	}

	public Vector3 CameraDownLeftCorner
	{
		get {
			return _cameraDownLeftCorner;
		}
	}

	public bool IsOutOfScreen(Transform transform)
	{
		Vector2 halfScale = transform.lossyScale/2;
		if (transform.position.x + halfScale.x >= _cameraTopRightCorner.x)
		{
			return true;
		}

		if (transform.position.y + halfScale.y >= _cameraTopRightCorner.y)
		{
			return true;
		}
		if (transform.position.x - halfScale.x <= _cameraDownLeftCorner.x)
		{
			return true;
		}
		if (transform.position.y - halfScale.y <= _cameraDownLeftCorner.y)
		{
			return true;
		}
		return false;
	}
}

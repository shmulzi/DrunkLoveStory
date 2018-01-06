using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waitress : MonoBehaviour {

    public Vector2 targetTolerance;

    private List<DrinkObject> _drinkObjects;
    private Vector2 _startingPosition = new Vector2(-5,-5);
    private Vector2 _targetPosition = new Vector2(0,0);
    private CharacterDriver _characterDriver;
    private bool _goTowardsTarget;
    private bool _inPlay;
    private DrinkPlacement _tray;

    public DrinkPlacement Tray
    {
        get
        {
            return _tray;
        }
    }

	// Use this for initialization
	void Start () {
        _characterDriver = GetComponent<CharacterDriver>();
        _goTowardsTarget = true;
        _inPlay = true;
        transform.position = _startingPosition;
        _tray = GetComponentInChildren<DrinkPlacement>();
        // select a table and get the position to _targetposition

	}
	
	// Update is called once per frame
	void Update () {
        if (_inPlay)
        {
            if (_goTowardsTarget)
            {
                _characterDriver.MoveInput = transform.InverseTransformPoint(_targetPosition).normalized;
            }
            else
            {
                _characterDriver.MoveInput = transform.InverseTransformPoint(_startingPosition).normalized;
            }
        }
	}
    
    public void Activate(Vector3 tablePosition, List<DrinkObject> drinkObjects)
    {
        _inPlay = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.StartsWith("Table"))
        {
            _goTowardsTarget = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


}

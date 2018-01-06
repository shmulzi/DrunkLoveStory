using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Waitress : MonoBehaviour {

	public event Action<GameObject> OnReachedTable;

    public Vector2 targetTolerance;

    private Vector2 _startingPosition = new Vector2(-5,-5);
    private CharacterDriver _characterDriver;
    private bool _goTowardsTarget;
    private bool _inPlay;
    private DrinkPlacement _tray;
	private GameObject _table;

    public DrinkPlacement Tray
    {
        get
        {
            return _tray;
        }
    }

	public bool InPlay
	{
		get 
		{
			return _inPlay;
		}
	}

	// Use this for initialization
	void Start () {
        _characterDriver = GetComponent<CharacterDriver>();
        _goTowardsTarget = true;
        _inPlay = true;
        transform.position = _startingPosition;
        _tray = GetComponentInChildren<DrinkPlacement>();

	}
	
	// Update is called once per frame
	void Update () {
        if (_inPlay)
        {
            if (_goTowardsTarget)
            {
				_characterDriver.MoveInput = transform.InverseTransformPoint(_table.transform.position).normalized;
            }
            else
            {
                _characterDriver.MoveInput = transform.InverseTransformPoint(_startingPosition).normalized;
            }
        }
	}
    
	public void Activate(GameObject table, List<GameObject> drinks)
    {
        _inPlay = true;
		_table = table;
		foreach(GameObject drink in drinks){
			_tray.AddDrink(drink);
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject == _table)
        {
			if(OnReachedTable != null){
				OnReachedTable(_table);
			}
			DrinkPlacement tablePlacement = _table.GetComponent<DrinkPlacement>();
			while(_tray.IsEmpty()){
				tablePlacement.AddDrink(_tray.GetDrink());
			}
			_table = null;
			_goTowardsTarget = false;
        }
    }

}

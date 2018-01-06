using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private GameRules _gameRules;

    private int _number;
    private int _intoxication;
    private int _courage;


    public int Number
    {
        get
        {
            return _number;
        }
        set
        {
            _number = value;
        }
    }

    public int Intoxication
    {
        get
        {
            return _intoxication;
        }
    }

    public int Courage
    {
        get
        {
            return _courage;
        }
    }

    // Use this for initialization
    void Start () {
        _gameRules = Camera.main.GetComponent<GameRules>();
        StartCoroutine(DecreeaseMeters());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddIntoxication(int intoxToAdd)
    {
        _intoxication += intoxToAdd;
        if (_intoxication > _gameRules.MaxIntoxication)
            _intoxication = _gameRules.MaxIntoxication;
    }

    public void AddCourage(int courToAdd)
    {
        _courage += courToAdd;
        if (_courage > _gameRules.MaxCourage)
            _courage = _gameRules.MaxCourage;
    }

    private IEnumerator DecreeaseMeters()
    {
        yield return new WaitForSeconds(_gameRules.DecreaseMetersInSeconds);
        _courage -= _gameRules.DecreaseCourageOnTimer;
        _intoxication -= _gameRules.DecreaseIntoxicationOnTimer;
        if (_courage < 0)
            _courage = 0;
        if (_intoxication < 0)
            _intoxication = 0;
        yield return StartCoroutine(DecreeaseMeters());
    }

    
}

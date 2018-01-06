using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterDriver))]
[RequireComponent(typeof(StateManager))]
public class InteractionHandler : MonoBehaviour {

    private CharacterDriver _characterDriver;
    private StateManager _stateManager;
    private IInteractor _currInteractor;
    private Vector2 _input;
    public Vector2 Input
    {
        set
        {
            _input = value;
        }
    }

	// Use this for initialization
	void Start () {
        _currInteractor = GetComponent<PunchInteractor>();
        _stateManager = GetComponent<StateManager>();
        _characterDriver = GetComponent<CharacterDriver>();
	}
	
	// Update is called once per frame
	void Update () {
        _currInteractor.Input = _input;
	}

    public void GetPunched(float duration)
    {
        Debug.Log(name + " got punched for duration - " + duration);
        _stateManager.BeginState(StateManager.State.STUNNED, duration, OnStateEndedHandler);
        _characterDriver.IsStunned = true;
    }

    private void OnStateEndedHandler(StateManager.State state)
    {
        if(state == StateManager.State.STUNNED)
        {
            _characterDriver.IsStunned = false;
        }
    }
}

public interface IInteractor
{
    Vector2 Input { set; }
}

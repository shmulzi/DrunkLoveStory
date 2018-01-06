using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public enum State
    {
        STUNNED
    }

    private List<State> states;

    public delegate void OnStatedEndedHandler(State state);
    private event OnStatedEndedHandler OnStateEndedEvent;

	// Use this for initialization
	void Start () {
        states = new List<State>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BeginState(State state)
    {
        states.Add(state);
    }

    public void BeginState(State state, OnStatedEndedHandler onStateEndedHandler)
    {
        OnStateEndedEvent += onStateEndedHandler;
        BeginState(state);
    }

    public void BeginState(State state, float durationInSeconds)
    {
        BeginState(state);
        StartCoroutine(EndStateInDuration(state, durationInSeconds));
    }

    public void BeginState(State state, float durationInSeconds, OnStatedEndedHandler onStateEndedHandler)
    {
        OnStateEndedEvent += onStateEndedHandler;
        BeginState(state, durationInSeconds);
    }

    public void EndState(State state)
    {
        states.Remove(state);
        if (OnStateEndedEvent != null)
            OnStateEndedEvent(state);
    }

    public bool IsInState(State state)
    {
        return states.Contains(state);
    }

    private IEnumerator EndStateInDuration(State state, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        EndState(state);
    }
}

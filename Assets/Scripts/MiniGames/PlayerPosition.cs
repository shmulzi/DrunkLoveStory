using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour {

    private Player _player;
    private MiniGame _miniGame;
    private InputHandler _inputHandler;

    private int _playerIndex;
    private bool _inMiniGame;

	// Use this for initialization
	void Start ()
    {
        _miniGame = transform.parent.GetComponentInChildren<MiniGame>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(_player != null && _inMiniGame)
        {
            _player.transform.position = transform.position;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_player == null)
        {
            _player = collision.transform.GetComponent<Player>();
            _inputHandler = collision.transform.GetComponent<InputHandler>();
            _miniGame.OnMiniGameStarted += OnMiniGameStarted;
            if (_miniGame.GetType() == typeof(PongMiniGame))
            {
                _playerIndex = ((PongMiniGame)_miniGame).AddPlayer(_player, (name == "RPosition"));
                Debug.Log(name + " " + _playerIndex);
            }
            else
            {
                _miniGame.AddPlayer(_player);
            }
            if (_miniGame.PlayerNumSatisfied())
            {
                _miniGame.StartGame();
            }   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_player != null)
        {
            _miniGame.RemovePlayer(_player, _playerIndex);
            _player = null;
            _inputHandler = null;
            _miniGame.OnMiniGameStarted -= OnMiniGameStarted;
        }
    }

    private void OnMiniGameStarted()
    {
        _miniGame.OnMiniGameEnded += OnMiniGameEnded;
        _inputHandler.BeginMiniGame(_miniGame, _playerIndex);
        _inMiniGame = true;
        _miniGame.OnMiniGameStarted -= OnMiniGameStarted;
    }

    private void OnMiniGameEnded(int winner)
    {
        if(_player.Number == winner)
        {
            //TODO
        }
        _inMiniGame = false;
        _inputHandler.EndMiniGame();
        _miniGame.OnMiniGameEnded -= OnMiniGameEnded;
        _player = null;
    }
    
}

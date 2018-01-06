using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MiniGame : MonoBehaviour {

    public event Action OnMiniGameStarted;
    public event Action<int> OnMiniGameEnded;

    public float durationInSeconds;
    public float coolDownInSeconds;
    public int pointsToWin;
    public int numOfPlayers;
    public int couragePerPoint;
    public int couragePerWin;
    public int intoxicationPerPoint;
    public int intoxicationPerLoss;

    private Vector2[] _playerInputs;
    private int _playerInputsIndex = 0;
    private Dictionary<Player, int> _score;
    private bool _isActive;

    private Action _playerDistconnected;

    private Stack<int> _removedPlayers;
    
    

    public bool IsActive
    {
        get
        {
            return _isActive;
        }
    }

    public Dictionary<Player, int> Score
    {
        get
        {
            return _score;
        }

        protected set
        {
            _score = value;
        }
    }

    protected Vector2[] PlayerInputs
    {
        get
        {
            return _playerInputs;
        }
    }

    protected int PlayerInputsIndex
    {
        get
        {
            return _playerInputsIndex;
        }
    }

    // Use this for initialization
    protected virtual void Start () {
        _playerInputs = new Vector2[numOfPlayers];
        _score = new Dictionary<Player, int>();
        _removedPlayers = new Stack<int>();
	}

    // Update is called once per frame
    protected virtual void Update () {
        foreach (Player player in _score.Keys)
        {
            if(_score[player] >= pointsToWin)
            {
                EndGame();
                break;
            }
        }

    }

    public virtual void StartGame()
    {
        if (OnMiniGameStarted != null)
            OnMiniGameStarted.Invoke();
        _isActive = true;
    }

    public virtual void EndGame()
    {
        _isActive = false;
        if(OnMiniGameEnded != null)
        {
            int winnerNum = 1;
            if(numOfPlayers > 1)
            {
                int bestScore = 0;
                Player winner = null;
                foreach(Player player in _score.Keys)
                {
                    int score = _score[player];
                    if (score > bestScore)
                    {
                        bestScore = score;
                        winner = player;
                    } 
                }
                if (winner != null)
                    winnerNum = winner.Number;
            }
            OnMiniGameEnded.Invoke(winnerNum);
            _score.Clear();
            _playerInputsIndex = 0;
        }
    }

    private IEnumerator EndGameInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(_isActive)
            EndGame();
    }

    public int AddPlayer(Player player, Action playerDisconnected = null)
    {
        int retVal = -1;
        if(_removedPlayers.Count > 0)
        {
            retVal = _removedPlayers.Pop();
        }
        else if (_playerInputsIndex < numOfPlayers)
        {
            retVal = _playerInputsIndex;
            _playerInputsIndex++;
        }
        if (_score.Count < numOfPlayers)
        {
            _score[player] = 0;
        }
        else
        {
            Debug.LogError("Tried to add more players than allowed.");
        }
        return retVal;
    }

    public virtual void RemovePlayer(Player player, int playerIndex)
    {
        _removedPlayers.Push(playerIndex);
        _score.Remove(player);
    }

    public void PlayerScored(Player player, int points)
    {
        _score[player] += points;
    }

    public void PlayerInput(int index, Vector2 input)
    {
        _playerInputs[index] = input;
    }

    public bool PlayerNumSatisfied()
    {
        return numOfPlayers == _score.Count;
    }
}

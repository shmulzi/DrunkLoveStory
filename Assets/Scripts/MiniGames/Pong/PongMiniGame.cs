using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongMiniGame : MiniGame {
    
    public float paddleSpeed;
    public float ballInitSpeed;
    public float ballMaxSpeed;
    public float ballIncreaseSpeedBy;
    public float ballIncreaseSpeedInteval;

    private Paddle _paddleL;
    private Paddle _paddleR;
    private Ball _ball;
    private Camera _gameCamera;
    private Vector3 _cameraRightEdge;
    private Vector3 _cameraLeftEdge;
    private int _rPlayerIndex;
    private int _lPlayerIndex;
    private Player _lPlayer;
    private Player _rPlayer;
    
    public float PaddleSpeed
    {
        get
        {
            return paddleSpeed;
        }
    }

    public float BallInitSpeed
    {
        get
        {
            return ballInitSpeed;
        }
    }

    public float BallMaxSpeed
    {
        get
        {
            return ballMaxSpeed;
        }
    }

    public float BallIncreaseSpeedBy
    {
        get
        {
            return ballIncreaseSpeedBy;
        }
    }

    public float BallIncreaseSpeedInteval
    {
        get
        {
            return ballIncreaseSpeedInteval;
        }
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
        _gameCamera = GetComponentInChildren<Camera>();
        _cameraRightEdge = _gameCamera.ViewportToWorldPoint(new Vector3(1, 1));
        _cameraLeftEdge = _gameCamera.ViewportToWorldPoint(new Vector3(0, 0));
        _paddleL = transform.Find("PaddleL").GetComponent<Paddle>();
        _paddleR = transform.Find("PaddleR").GetComponent<Paddle>();
        _ball = transform.Find("Ball").GetComponent<Ball>();
        _paddleL.Init(PaddleSpeed, _cameraRightEdge, _cameraLeftEdge);
        _paddleR.Init(PaddleSpeed, _cameraRightEdge, _cameraLeftEdge);
        _ball.Init(BallInitSpeed, BallMaxSpeed, _cameraRightEdge, _cameraLeftEdge, BallIncreaseSpeedInteval, BallIncreaseSpeedBy);
    }
    
    // Update is called once per frame
    protected override void Update () {
        if (IsActive)
        {
            base.Update();
            _paddleL.Input = PlayerInputs[_lPlayerIndex];
            _paddleR.Input = PlayerInputs[_rPlayerIndex];
            if (_ball.transform.position.x >= _paddleR.transform.position.x - _paddleR.transform.localScale.x / 2f)
            {
                PlayerScored(_rPlayer, 1);
                ResetBall();
                StartCoroutine(StartMatchInSeconds(3));
            }
            if (_ball.transform.position.x <= _paddleL.transform.position.x + _paddleL.transform.localScale.x / 2f)
            {
                PlayerScored(_lPlayer, 1);
                ResetBall();
                StartCoroutine(StartMatchInSeconds(3));
            }
        }
        
    }
    
    private void ResetBall()
    {
        _ball.Stop();
        _ball.transform.localPosition = Vector2.zero;
    }

    private Vector2 RandomizeAngle()
    {
        int[] yChoices = new int[] { -1, 1 };
        float[] xChoices = new float[] { -1, -0.5f, 0.5f, 1 };

        int yIndex = UnityEngine.Random.Range(0, yChoices.Length-1);
        int xIndex = UnityEngine.Random.Range(0, xChoices.Length - 1);

        return new Vector2(xChoices[xIndex], yChoices[yIndex]).normalized;
    }

    private void StartMatch()
    {
        _ball.Launch(RandomizeAngle());
    }

    public override void StartGame()
    {
        base.StartGame();
        StartCoroutine(StartMatchInSeconds(3));
    }

    private IEnumerator StartMatchInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (IsActive)
        {
            StartMatch();
        }
       
    }

    public int AddPlayer(Player player, bool toRight)
    {
        int currIndex = base.AddPlayer(player);
        if (toRight)
        {
            _rPlayer = player;
            _rPlayerIndex = currIndex;
        }
        else
        {
            _lPlayerIndex = currIndex;
            _lPlayer = player;
        }
        return currIndex;
    }

    public override void RemovePlayer(Player player, int playerIndex)
    {
        base.RemovePlayer(player, playerIndex);
    }

    public override void EndGame()
    {
        ResetBall();
        base.EndGame();
    }

   

}

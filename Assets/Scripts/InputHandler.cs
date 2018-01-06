using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    private InteractionHandler _interactionHandler;
    private CharacterDriver _characterDriver;
    private string _playerControllerPrefix;
    private Player _player;
    
    private MiniGame _miniGame;
    private int _miniGamePlayerIndex;

    private void Start()
    {
        _player = GetComponent<Player>();
        _characterDriver = GetComponent<CharacterDriver>();
        _interactionHandler = GetComponent<InteractionHandler>();
        _playerControllerPrefix = GetPlayerPrefix(GameControls.Instance.GetPlayerController(_player.Number));
    }

    private void Update()
    {
        Vector2 rJoystickInput = new Vector2(Input.GetAxis(_playerControllerPrefix + "R_Horizontal"), Input.GetAxis(_playerControllerPrefix + "R_Vertical"));
        if (_miniGame == null)
        {
            _interactionHandler.Input = rJoystickInput;
        }
        else
        {
            _miniGame.PlayerInput(_miniGamePlayerIndex, rJoystickInput);
        }
    }

    private void FixedUpdate()
    {
        if(_miniGame == null)
        {
            _characterDriver.MoveInput = new Vector2(Input.GetAxis(_playerControllerPrefix + "_Horizontal"), Input.GetAxis(_playerControllerPrefix + "_Vertical"));
        }
    }

    private string GetPlayerPrefix(GameControls.Controller controller)
    {
        string result = null;
        switch (controller)
        {
            case GameControls.Controller.KEYBOARD:
                result = "KB";
                break;
            case GameControls.Controller.JOYSTICK_1:
                result = "J1";
                break;
            case GameControls.Controller.JOYSTICK_2:
                result = "J2";
                break;
            case GameControls.Controller.JOYSTICK_3:
                result = "J3";
                break;
            case GameControls.Controller.JOYSTICK_4:
                result = "J4";
                break;
            default:
                break;
        }
        return result;
    }

    public void BeginMiniGame(MiniGame miniGame, int playerIndex)
    {
        _miniGame = miniGame;
        _miniGamePlayerIndex = playerIndex;
    }

    public void EndMiniGame()
    {
        _miniGame = null;
    }
}

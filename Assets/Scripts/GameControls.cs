using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls {

    public enum Controller
    {
        NONE, KEYBOARD, JOYSTICK_1, JOYSTICK_2, JOYSTICK_3, JOYSTICK_4
    }

    private Dictionary<int, Controller> _controlToPlayerMap;

    private static GameControls _instance;
    public static GameControls Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameControls();
            return _instance;
        }
    }

    public GameControls()
    {
        _controlToPlayerMap = new Dictionary<int, Controller>();
    }

    public bool AssignPlayer(int playerNum, Controller controller)
    {
        if (_controlToPlayerMap.ContainsKey(playerNum))
        {
            Debug.Log("Controller already assigned for player " + playerNum);
            return false;
        }
        else
        {
            _controlToPlayerMap[playerNum] = controller;
            return true;
        }
    }

    public Controller GetPlayerController(int playerNum)
    {
        Controller result = Controller.NONE;
        if (!_controlToPlayerMap.TryGetValue(playerNum, out result))
            Debug.Log("could not assign controller for player - " + playerNum);
        return result;

          
        
    }

}

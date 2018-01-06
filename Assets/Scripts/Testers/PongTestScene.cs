using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongTestScene : MonoBehaviour {

    public Transform pongMGObj;
    public Transform p1;
    public Transform p2;


    private PongMiniGame _pongMiniGame;

	// Use this for initialization
	void Start () {
        _pongMiniGame = pongMGObj.GetComponent<PongMiniGame>();
        _pongMiniGame.AddPlayer(p1.GetComponent<Player>(), false);
        _pongMiniGame.AddPlayer(p2.GetComponent<Player>(), true);

    }

    // Update is called once per frame
    void Update () {
        //_pongMiniGame.PlayerAInput = new Vector2(Input.GetAxis("J1R_Horizontal"), Input.GetAxis("J1R_Vertical"));
        //_pongMiniGame.PlayerBInput = new Vector2(Input.GetAxis("J2R_Horizontal"), Input.GetAxis("J2R_Vertical"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneRunner : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    // Use this for initialization
    void Start () {
        
        player1.GetComponent<Player>().Number = 1;
        player2.GetComponent<Player>().Number = 2;
        GameControls.Instance.AssignPlayer(1, GameControls.Controller.JOYSTICK_1);
        GameControls.Instance.AssignPlayer(2, GameControls.Controller.JOYSTICK_2);
        StartCoroutine(AddStuff());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator AddStuff()
    {
        yield return new WaitForSeconds(6f);
        player1.GetComponent<Player>().AddCourage(10);
        player1.GetComponent<Player>().AddIntoxication(15);
        yield return StartCoroutine(AddStuff());
    }
}

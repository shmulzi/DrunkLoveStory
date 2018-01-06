using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAutomator : MonoBehaviour {

    private CharacterDriver _cd;

    private bool goLeft;

	// Use this for initialization
	void Start () {
        _cd = GetComponent<CharacterDriver>();
	}
	
	// Update is called once per frame
	void Update () {
        if (goLeft)
            _cd.MoveInput = new Vector2(-1, 0);
        else
            _cd.MoveInput = new Vector2(1, 0);
        if (transform.position.x > 5)
            goLeft = true;
        if (transform.position.x < -5)
            goLeft = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour {

    public Transform P1;
    public Transform P2;

    private Text _text;

    // Use this for initialization
    void Start () {
        _text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _text.text = "P1 Intoxication = " + P1.GetComponent<Player>().Intoxication + "\n" +
            "P1 Courage = " + P1.GetComponent<Player>().Courage + "\n";

    }
}

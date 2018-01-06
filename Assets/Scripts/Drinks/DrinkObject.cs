using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrinkObject : MonoBehaviour {

    public event Action<GameObject> OnReachedEnd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "EndPoint")
        {
            if (OnReachedEnd != null)
                OnReachedEnd(gameObject);
        }
    }
}

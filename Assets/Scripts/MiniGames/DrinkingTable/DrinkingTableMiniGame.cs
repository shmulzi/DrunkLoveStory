using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingTableMiniGame : MiniGame {

    private float _rotationSpeed = 100f;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed);
        foreach(Transform child in transform)
        {
            if (child.name.StartsWith("Drink")){
                child.rotation = Quaternion.identity;
            }
        }
    }
}

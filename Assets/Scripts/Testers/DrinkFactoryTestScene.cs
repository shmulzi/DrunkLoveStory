using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkFactoryTestScene : MonoBehaviour {

    List<GameObject> drinkObjects;

    DrinkDispenser[] drinkDispensers;
    DrinkManager drinkManager;

	// Use this for initialization
	void Start () {
        drinkDispensers = GameObject.Find("DrinkDispensers").GetComponentsInChildren<DrinkDispenser>();
        drinkManager = Resources.Load<DrinkManager>("drinkMgr");
        foreach(DrinkDispenser drinkDispenser in drinkDispensers)
        {
            drinkDispenser.Init(Camera.main.GetComponent<DrinkFactory>(), drinkManager);
            drinkDispenser.SetActive(true);
        }
        

        //DrinkManager.Instance.Load();
        //drinkObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Drink d = drinkManager.GetRandomDrink();
    //        GameObject g = Camera.main.GetComponent<DrinkFactory>().GetDrinkObject(d);
    //        g.transform.position = Vector3.zero;
    //        g.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 100);
    //        drinkObjects.Add(g);
    //    }

    //    if (Input.GetKeyDown(KeyCode.End))
    //    {
    //        GameObject g = drinkObjects[drinkObjects.Count - 1];
    //        drinkObjects.Remove(g);
    //        Camera.main.GetComponent<DrinkFactory>().ReturnDrinkObject(g);
    //    }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkFactory : MonoBehaviour {

    private const string DRINK_TEXTURE_PATH = "Assets/Materials/Textures/Drinks";

    public int numOfDrinks;

    private Vector2 _drinkObjectPool = new Vector2(-50,-50);
    private Stack<GameObject> _drinkObjectStack;

	// Use this for initialization
	void Start () {
        _drinkObjectStack = new Stack<GameObject>();
        for(int i = 0; i < numOfDrinks; i++)
        {
            GameObject drinkObject = new GameObject("BlankDrinkObject");
            drinkObject.AddComponent<SpriteRenderer>();
            drinkObject.AddComponent<CircleCollider2D>();
            drinkObject.AddComponent<DrinkObject>();
            drinkObject.transform.position = _drinkObjectPool;
            _drinkObjectStack.Push(drinkObject);
        }
	}

    public GameObject GetDrinkObject(Drink drink)
    {
        if (_drinkObjectStack.Count > 0)
        {
            GameObject drinkObject = _drinkObjectStack.Pop();
            drinkObject.GetComponent<SpriteRenderer>().sprite = drink.imageSprite;
            drinkObject.name = drink.name;
            return drinkObject;
            
        }
        return null;
    }

    public void ReturnDrinkObject(GameObject drinkObject)
    {
        drinkObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        drinkObject.GetComponent<SpriteRenderer>().sprite = null;
        drinkObject.transform.position = _drinkObjectPool;
        drinkObject.name = "BlackDrinkObject-Returned";
        _drinkObjectStack.Push(drinkObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

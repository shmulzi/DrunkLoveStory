using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPlacement : MonoBehaviour {

    public float ringSize;

    private int _numOfDrinks;
    private Stack<GameObject> _drinks;

	// Use this for initialization
	void Start () {
        _drinks = new Stack<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool AddDrink(GameObject drink)
    {
        if (_numOfDrinks < 5)
        {
            _drinks.Push(drink);
            _numOfDrinks++;
            ArrangeDrinks();
            return true;
        }
        return false;
    }

    private void ArrangeDrinks()
    {
        int drinkNum = 1;
        foreach (GameObject drink in _drinks)
        {
            Vector2 pos;
            switch (drinkNum)
            {
                case 1:
                    pos = new Vector2(ringSize, 0);
                    break;
                case 2:
                    pos = new Vector2(-ringSize, 0);
                    break;
                case 3:
                    pos = new Vector2(0, ringSize);
                    break;
                case 4:
                    pos = new Vector2(0, -ringSize);
                    break;
                case 5:
                    pos = new Vector2(0, 0);
                    break;
                default:
                    pos = new Vector2(0, 0);
                    break;
            }
            drinkNum++;
            drink.transform.localPosition = new Vector3(pos.x,pos.y);
        }
    }
    
    public GameObject GetDrink()
    {
        _numOfDrinks--;
        return _drinks.Pop();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitressManager : MonoBehaviour {

    private const string INACTIVE_WAITRESS_NAME = "InActiveWaitress";
	private const string TABLE_TAG = "Table";

    public int numOfWaitresses;
    public Vector2 waitressPool = new Vector2(-40, -40);
	public int minNumOfDrinks = 2;
	public int maxNumOfDrinks = 5;

    private List<Waitress> _waitresses;
	private List<GameObject> _availableTables;
	private List<GameObject> _inPlayTables;
	private DrinkFactory _drinkFactory;
	private DrinkManager _drinkManager;
	private ScreenBorders _screenBorders;

	public void Init(DrinkFactory drinkFactory, DrinkManager drinkManager)
	{
		_drinkFactory = drinkFactory;
		_drinkManager = drinkManager;
	}

	// Use this for initialization
	void Start () {
		_screenBorders = Camera.main.GetComponent<ScreenBorders>();
        for (int i = 0; i < numOfWaitresses; i++)
        {
            GameObject wGo = new GameObject(INACTIVE_WAITRESS_NAME);
            wGo.AddComponent<SpriteRenderer>();
            wGo.AddComponent<Rigidbody2D>().gravityScale = 0;
            wGo.AddComponent<CharacterDriver>().movementSpeed = 1;
            _waitresses.Add(wGo.AddComponent<Waitress>());
            wGo.AddComponent<CircleCollider2D>();
            wGo.transform.position = waitressPool;
        }
		_availableTables = new List<GameObject>(GameObject.FindGameObjectsWithTag(TABLE_TAG));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendWaitress()
    {
        Waitress selWaitress = null;
        while(selWaitress == null)
        {
            int rnd = Random.Range(0, numOfWaitresses - 1);
            Waitress tmpWaitress = _waitresses[rnd];
			if (!tmpWaitress.InPlay){
                selWaitress = tmpWaitress;
				int numOfDrinks = Random.Range(minNumOfDrinks, maxNumOfDrinks);
				List<GameObject> drinks = new List<GameObject>();
				for(int i = 0; i < numOfDrinks; i++){
					GameObject drinkObject = _drinkFactory.GetDrinkObject(_drinkManager.GetRandomDrink());
					Rigidbody2D rb = drinkObject.AddComponent<Rigidbody2D>();
					rb.gravityScale = 0;
					drinkObject.transform.localScale = new Vector2(0.5f, 0.5f);
					drinks.Add(drinkObject);
				}
				selWaitress.transform.transform.position = new Vector2(-5,-5);
				selWaitress.OnReachedTable += OnReachedTable;
				selWaitress.Activate(GetRandomTable(),drinks);
			}
        }

    }

	private GameObject GetRandomTable()
	{
		GameObject table = _availableTables[Random.Range(0,_availableTables.Count)];
		_availableTables.Remove(table);
		_inPlayTables.Add(table);
		return table;
	}

	private void OnReachedTable(GameObject table)
	{
		_inPlayTables.Remove(table);
		_availableTables.Add(table);
	}
}

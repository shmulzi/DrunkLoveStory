using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitressManager : MonoBehaviour {

    private const string INACTIVE_WAITRESS_NAME = "InActiveWaitress";

    public int numOfWaitresses;
    public Vector2 waitressPool = new Vector2(-40, -40);

    private List<Waitress> _waitresses;

	// Use this for initialization
	void Start () {
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendWaitress(List<GameObject> drinks)
    {
        Waitress selWaitress = null;
        while(selWaitress == null)
        {
            int rnd = Random.Range(0, numOfWaitresses - 1);
            Waitress tmpWaitress = _waitresses[rnd];
            if (tmpWaitress.name == INACTIVE_WAITRESS_NAME)
                selWaitress = tmpWaitress;
        }
        
        
    }
}

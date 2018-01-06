using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkDispenser : MonoBehaviour {

    public float drinkEverySeconds;
    public float launchForce;
    public Vector2 beginPosition;

    private bool _active;
    private DrinkFactory _drinkFactory;
    private DrinkManager _drinkManager;

    public void Init(DrinkFactory drinkFactory, DrinkManager drinkManager)
    {
        _drinkFactory = drinkFactory;
        _drinkManager = drinkManager;
    }

    public void SetActive(bool active)
    {
        _active = active;
        if (_active)
        {
            StartCoroutine(LaunchDrinkEverySeconds(drinkEverySeconds));
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void LaunchDrink()
    {
        if(_drinkFactory != null)
        {
            GameObject drinkObject = _drinkFactory.GetDrinkObject(_drinkManager.GetRandomDrink());
            Rigidbody2D rb = drinkObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = 0.2f;
            drinkObject.transform.localScale = new Vector2(0.5f, 0.5f);
            drinkObject.GetComponent<DrinkObject>().OnReachedEnd += OnReachedEnd;
            drinkObject.transform.position = transform.TransformPoint(beginPosition);
            drinkObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * launchForce);
            
        }
        else
        {
            Debug.LogError("drinkFactory is null");
        }
    }

    private void OnReachedEnd(GameObject drinkObject)
    {
        drinkObject.GetComponent<DrinkObject>().OnReachedEnd -= OnReachedEnd;
        Destroy(drinkObject.GetComponent<Rigidbody2D>());
        _drinkFactory.ReturnDrinkObject(drinkObject);
    }

    private IEnumerator LaunchDrinkEverySeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (_active)
        {
            LaunchDrink();
            yield return StartCoroutine(LaunchDrinkEverySeconds(seconds));
        }
    }

}

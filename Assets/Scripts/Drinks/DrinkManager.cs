using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrinkManager : ScriptableObject {
    
    public event Action OnListChanged;
    
    [SerializeField]
    private List<Drink> _drinks;

   
    public List<Drink> Drinks
    {
        get
        {
            return _drinks;
        }
    }

    public bool AddDrink(Drink drink)
    {
        if (!DrinkExists(drink.name))
        {
            _drinks.Add(drink);
            if (OnListChanged != null)
             OnListChanged();
            return true;
        }
        return false;
            
    }

    public void ReplaceDrink(Drink drink)
    {
        Drink drinkToReplace = null;
        foreach(Drink currDrink in _drinks)
        {
            if(currDrink.name == drink.name)
            {
                drinkToReplace = currDrink;
            }
        }
        if(drinkToReplace != null)
        {
            _drinks.Insert(_drinks.IndexOf(drinkToReplace), drink);
            _drinks.Remove(drinkToReplace);
            if (OnListChanged != null)
                OnListChanged();
        }
        else
        {
            Debug.LogError("Tried to replace drink that does not exist");
        }
        

    }

    public void RemoveDrink(string drinkName)
    {
        Drink foundDrink = null;
        foreach (Drink currDrink in _drinks)
        {
            if(currDrink.name == drinkName)
            {
                foundDrink = currDrink;
            }
        }
        if(foundDrink != null)
        {
            _drinks.Remove(foundDrink);
            if(OnListChanged != null)
                OnListChanged();
        }
        else
        {
            Debug.LogError("Could not find drink to remove - " + drinkName);
        }
    }

    public bool DrinkExists(string drinkName)
    {
        foreach(Drink currDrink in _drinks)
        {
            if(currDrink.name == drinkName)
            {
                return true;
            }
        }
        return false;
    }

    
    public Drink GetRandomDrink()
    {
        
        int index = UnityEngine.Random.Range(0, _drinks.Count);
        return _drinks[index];
    }

    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Drink {
    
    public enum DrinkType
    {
        SHOT, CHUG, SIP, FLAME
    }

    public string name;
    public Sprite imageSprite;
    public DrinkType type;
    public int courageGain;
    public int intoxicationGain;
    
}

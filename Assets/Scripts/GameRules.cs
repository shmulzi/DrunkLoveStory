using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour {

    public int maxIntoxication;
    public int maxCourage;
    public int[] intoxicationNotches;
    public int[] courageNotches;
    public float decreaseMetersInSeconds;
    public int decreaseIntoxicationOnTimer;
    public int decreaseCourageOnTimer;

    public int MaxIntoxication
    {
        get
        {
            return maxIntoxication;
        }
    }
    public int MaxCourage
    {
        get
        {
            return maxCourage;
        }
    }
    public int[] IntoxicationNotches
    {
        get
        {
            return intoxicationNotches;
        }
    }
    public int[] CourageNotches
    {
        get
        {
            return courageNotches;
        }
    }

    public float DecreaseMetersInSeconds
    {
        get
        {
            return decreaseMetersInSeconds;
        }
    }

    public int DecreaseIntoxicationOnTimer
    {
        get
        {
            return decreaseIntoxicationOnTimer;
        }
    }

    public int DecreaseCourageOnTimer
    {
        get
        {
            return decreaseCourageOnTimer;
        }
    }
}

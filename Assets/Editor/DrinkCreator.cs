using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrinkCreator : EditorWindow {

    private DrinkManager _drinkManager;

    private bool _shouldReplace;
    private string _name;
    private Sprite _sprite;
    private int _courage;
    private int _intoxication;
    private Drink.DrinkType _drinkType;
    
    static void ShowWindow()
    {
        EditorWindow.GetWindow<DrinkCreator>();
    }

    public DrinkCreator Init(DrinkManager drinkManager)
    {
        _drinkManager = drinkManager;
        return this;
    }

    private void OnGUI()
    {
        if (!_shouldReplace)
            _name = EditorGUILayout.TextField("Name", _name);
        else
            EditorGUILayout.LabelField("Name", _name);
        _sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", _sprite, typeof(Sprite), false);
        _courage = EditorGUILayout.IntField("Courage", _courage);
        _intoxication = EditorGUILayout.IntField("Intoxication", _intoxication);
        _drinkType = (Drink.DrinkType)EditorGUILayout.EnumPopup("Drink Type",_drinkType);
        if (GUILayout.Button("Save"))
        {
            Drink d = new Drink();
            d.name = _name;
            d.imageSprite = _sprite;
            d.courageGain = _courage;
            d.intoxicationGain = _intoxication;
            d.type = _drinkType;
            if (_shouldReplace)
            {
                _drinkManager.ReplaceDrink(d);
            }
            else
            {
                _drinkManager.AddDrink(d);
            }
            this.Close();
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Delete"))
        {
            _drinkManager.RemoveDrink(_name);
            this.Close();
        }
    }

    public void LoadDrink(Drink drink)
    {
        _name = drink.name;
        _sprite = drink.imageSprite;
        _courage = drink.courageGain;
        _intoxication = drink.intoxicationGain;
        _drinkType = drink.type;
        _shouldReplace = true;
    }

    private void Awake()
    {
        
    }
}

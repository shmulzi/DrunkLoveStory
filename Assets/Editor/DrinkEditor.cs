using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrinkEditor : EditorWindow {

    private const string DRINK_MANAGER_ASSET_PATH = "Assets/Resources/drinkMgr.asset";

    private DrinkManager _drinkManager;

    private DrinkManager DrinkMgr
    {
        get
        {
            _drinkManager = AssetDatabase.LoadAssetAtPath<DrinkManager>(DRINK_MANAGER_ASSET_PATH);
            if(_drinkManager == null)
            {
                _drinkManager = CreateInstance<DrinkManager>();
                AssetDatabase.CreateAsset(_drinkManager, DRINK_MANAGER_ASSET_PATH);
            }
            return _drinkManager;
        }
    }

    private class DrinkContainer
    {
        public bool hide;
        public Drink drink;
    }

    private List<DrinkContainer> _drinks;

    private DrinkEditor()
    {
        _drinks = new List<DrinkContainer>();
    }

    private void OnEnable()
    {
        DrinkMgr.OnListChanged += OnListChanged;
        OnListChanged();
    }

    [MenuItem("Game Tools/Drink Editor")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<DrinkEditor>();   
    }

    private void OnGUI()
    {
        if(_drinks != null)
        {
            foreach (DrinkContainer drinkContainer in _drinks)
            {
                drinkContainer.hide = !EditorGUILayout.Foldout(!drinkContainer.hide, drinkContainer.drink.name);
                if (!drinkContainer.hide)
                {
                    EditorGUILayout.TextField("Name", drinkContainer.drink.name);
                    EditorGUILayout.ObjectField("Sprite", drinkContainer.drink.imageSprite, typeof(Sprite),false);
                    EditorGUILayout.EnumPopup("Type", drinkContainer.drink.type);
                    EditorGUILayout.IntField("Courage", drinkContainer.drink.courageGain);
                    EditorGUILayout.IntField("Intox", drinkContainer.drink.intoxicationGain);
                    if (GUILayout.Button("Edit"))
                    {
                        EditorWindow.GetWindow<DrinkCreator>().Init(DrinkMgr).LoadDrink(drinkContainer.drink);
                    }
                }
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space();
        if (GUILayout.Button("Create"))
        {
            EditorWindow.GetWindow<DrinkCreator>().Init(DrinkMgr);
            
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space();
        if (GUILayout.Button("Reload Data"))
        {
            OnListChanged();
        }
    }

    private void Awake()
    {
        _drinks = new List<DrinkContainer>();
        OnListChanged();
        DrinkMgr.OnListChanged += OnListChanged;
    }

    private void SerializeDrinkManager()
    {
        AssetDatabase.SaveAssets();
    }

    private void OnListChanged()
    {
        _drinks.Clear();
        foreach(Drink drink in DrinkMgr.Drinks)
        {
            DrinkContainer drinkContainer = new DrinkContainer();
            drinkContainer.drink = drink;
            drinkContainer.hide = true;
            _drinks.Add(drinkContainer);
        }
        SerializeDrinkManager();
    }
}

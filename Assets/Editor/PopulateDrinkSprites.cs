using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PopulateDrinkSprites : MonoBehaviour {

    private const string DRINK_TEXTURE_PATH = "Assets/Materials/Textures/Drinks";

    [MenuItem("Game Tools/Populate Drink Sprites")]
    private static void Populate()
    {
        List<Sprite> drinkSprites = GameObject.Find("GameData").GetComponent<DrinkSprites>().drinkSprites;
        drinkSprites.Clear();
        DirectoryInfo texDir = new DirectoryInfo(DRINK_TEXTURE_PATH);
        FileInfo[] info = texDir.GetFiles("*.png");
        foreach (FileInfo f in info)
        {
            foreach (Object obj in AssetDatabase.LoadAllAssetsAtPath(DRINK_TEXTURE_PATH+"/"+f.Name))
            {
                if (obj.GetType() == typeof(Sprite))
                {
                    drinkSprites.Add((Sprite)obj);
                }
            }
        }

           
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor_Debug : MonoBehaviour //EditorWindow
{

    // int NewCurrency = 5000;

    // [MenuItem("NecatiAkpinar/Debug")]
    // public static void ShowWindow()
    // {
    //     Editor_Debug window = (Editor_Debug)GetWindow<Editor_Debug>("Debug Editor");
    //     window.minSize = new Vector2(300, 160);
    //     window.maxSize = new Vector2(300, 160);
    //     // SaveSystem.Load();
    // }

    // private void OnGUI()
    // {
    //     GUILayout.BeginArea(new Rect(20, 20, 260, 100));
    //      //GUILayout.Label("Current Currency: " + SaveSystem.Data.Currency.ToString());
    //     // GUILayout.Label("Current Gem: " + SaveSystem.Data.Gem.ToString());
    //     // NewCurrency = EditorGUILayout.IntField("New Currency", NewCurrency);
    //     if (GUILayout.Button("Reset Hero Data"))
    //     {
    //         Save_System.Data.ResetAllHeroAttributes();
    //         Debug.Log("All hero properties are cleared!");
    //         Save_System.Save();
    //     }
    //     if (GUILayout.Button("Reset ALL Data (Careful!)"))
    //     {
    //         Save_System.Data.ResetAllData();
    //         Debug.Log("All data is cleared!");
    //         Save_System.Save();
    //     }

    //     // if (GUILayout.Button("Save Hero Properties"))
    //     // {
    //     //     Manager_Game.Instance.SaveHeroProperties();
    //     //     Debug.Log("Saveledi");
    //     //     Save_System.Save();
    //     // }
        
    //     GUILayout.EndArea();
    // }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Hero_Selection : MonoBehaviour
{
    [Header("Selection")]
    [SerializeField] private Transform parentHero;
    
    private Manager_Game managerGame;
    private Manager_Resources managerResources;
    private Controller_Player controllerPlayer;
    public List<Controller_Hero_Selection> listHeroSelection;
    
    public void InitializeHeroesSelectionPanel()
    {
        listHeroSelection = new List<Controller_Hero_Selection>();

        foreach (GameObject prefabHero in Manager_Game.Instance.managerResources.listPrefabHero)
        {
            Controller_Hero controllerHero = prefabHero.GetComponent<Controller_Hero>();
            controllerHero.InitializeHeroProperties();
            GameObject selectionHero = GameObject.Instantiate(Manager_Game.Instance.managerResources.prefabHeroSelectionThumbnail,parentHero);
            Controller_Hero_Selection controllerSelectionHero = selectionHero.GetComponent<Controller_Hero_Selection>();
            controllerSelectionHero.Initialize(controllerHero);
            listHeroSelection.Add(controllerSelectionHero);
        }

        if (PlayerPrefs.HasKey("TotalHeroCount")) managerGame.defaultEnabledHeroCount = PlayerPrefs.GetInt("TotalHeroCount");
        else managerGame.defaultEnabledHeroCount = 3;

        Debug.Log($"Enabled hero count: {managerGame.defaultEnabledHeroCount}");
        for (int i = 0; i < managerGame.defaultEnabledHeroCount; i++) listHeroSelection[i].EnableHero(); //Always enable first 3 hero.

    }

    
    void Start()
    {   
        managerGame = Manager_Game.Instance;
        managerResources = Manager_Game.Instance.managerResources; //Fetching managers at the top. Execution order : Manager_Resources -> Manager_Hero ...

        InitializeHeroesSelectionPanel();

        ResetDeck(); //Always clear the deck. 
    }

    public void ResetDeck()
    {
        foreach (GameObject prefabHero in Manager_Game.Instance.managerResources.listPrefabHero)
        {
            Controller_Hero controllerHero = prefabHero.GetComponent<Controller_Hero>();
            controllerHero.properties.IsSelectedOnDeck = false;
        }
    }
    
}


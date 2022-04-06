using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Hero : MonoBehaviour
{

    [Header("Battle")]
    [SerializeField] private GameObject parentPositionHero;
    public List<Controller_Hero> listHero;

    private Manager_Game managerGame;
    private Manager_Resources managerResources;
    private Controller_Player controllerPlayer;

    private int availableSeat = 3;

    #region Events
        public static Action<Controller_Hero> OnHeroDied;
        
    #endregion

    private void OnEnable() 
    {
        OnHeroDied = EventFunction_OnHeroDied_HeroDied;
    }
    private void OnDestroy() 
    {
        OnHeroDied = null;
    }

    void Start()
    {   
        managerGame = Manager_Game.Instance;
        managerResources = Manager_Game.Instance.managerResources; //Fetching managers at the top. Execution order : Manager_Resources -> Manager_Hero ...
        InitializeHeroesOnSelectedByPlayer();
    }
    public void InitializeHeroesOnSelectedByPlayer()
    {
        List<GameObject> listPrefabHero = Manager_Game.Instance.managerResources.listPrefabHero;
        
        
        for (int i=0; i < listPrefabHero.Count; i++)
        {
            Controller_Hero prefabControllerHero = listPrefabHero[i].GetComponent<Controller_Hero>();
            if (prefabControllerHero.properties.IsSelectedOnDeck) //If it is selected on hero selection menu
            {
                GameObject hero = GameObject.Instantiate(listPrefabHero[i],this.transform);
                hero.transform.position = SetHeroPosition();
                Controller_Hero controllerHero = hero.GetComponent<Controller_Hero>();
                this.listHero.Add(controllerHero);
            }
        }
    }
    
    public Vector3 SetHeroPosition()
    {
        availableSeat--;
        return parentPositionHero.transform.GetChild(availableSeat).transform.position;
    }

    public void SelectHero(Controller_Hero pHero)
    {
        foreach (Controller_Hero controllerHero in listHero)
        {
            controllerHero.selectionBorder.SetActive(false);
            controllerHero.DeSelect(); //Reset previous selected hero.
        } 
        pHero.Select(); //Select latest hero.
            
    }


    #region Event Functions
        public void EventFunction_OnHeroDied_HeroDied(Controller_Hero pControllerHero)
        {
            listHero.Remove(pControllerHero);
            if (listHero.Count == 0) //If all heroes are dead Invoke the event of Player Lost
            {
                Manager_Game.OnBattleEndedPlayerLost?.Invoke();
            }
        }
    #endregion
    
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum EGameState{
    HERO_SELECTION,
    GAMEPLAY
}

[System.Serializable]
public enum ECombatTurn{
    HEROES,
    ENEMIES
}

public class Manager_Game : MonoBehaviour
{
    [Header("Managers")]
    public Manager_Resources managerResources;
    public Manager_UI_HeroSelectionScene managerUISelection;
    public Manager_UI_Battle managerUIBattle;
    public Manager_Hero managerHero;
    public Manager_Enemy managerEnemy;

    [Header("Controllers")]
    public Controller_Player controllerPlayer;

    [Header("Constraints")]
    public int requiredMinHeroCount = 3;
    public EGameState currentGameState;
    public ECombatTurn currentCombatTurn;

    public static Manager_Game Instance;

    #region Events
        public static Action OnBattleStarted;
        public static Action OnBattleEndedPlayerVictory;
        public static Action OnBattleEndedPlayerLost;
        public static Action OnCharacterAttackStarted;
        public static Action OnCharacterAttackEnded;
        public static Action OnEnableNewHero;
    #endregion
    
    [Header("Gameplay Variables")]
    public bool canCharacterAttack;
    public int defaultEnabledHeroCount = 3; 
    public int totalGameHeroesCount; //Number of total heroes
    public int enableHeroEveryBattleLevel = 5; //After every 5 battle, enable new hero.
    public int extraHealthMultiplier = 10; //After leveling up, increase health with using this multiplier.
    public int extraAttackPowerMultiplier = 10; //After leveling up, increase attack power with using this multiplier.
    public int requiredExperienceToLevelUP = 5; //Required exp to level up.

    private void OnEnable() 
    {
        OnBattleStarted = EventFunction_OnBattleStarted_StartBattle;
        OnBattleEndedPlayerVictory = EventFunction_OnBattleEnded_PlayerVictory;
        OnBattleEndedPlayerLost = EventFunction_OnBattleEnded_PlayerLost;
        OnCharacterAttackStarted = EventFunction_OnCharacterAttackStarted_CharacterAttackStarted;
        OnCharacterAttackEnded = EventFunction_OnCharacterAttackEnded_CharacterAttackEnded;
        OnEnableNewHero = EventFunction_OnEnableNewHero_EnableNewHero;
    }
    
    private void OnDestroy() 
    {
        OnBattleStarted = null;    
        OnCharacterAttackStarted = null;
        OnCharacterAttackEnded = null;
        OnEnableNewHero = null;
    }
        
    private void Awake() 
    {
        Instance = this;

        if (SceneManager.GetActiveScene().name == "UI_SelectHeroes") currentGameState = EGameState.HERO_SELECTION;
        else if (SceneManager.GetActiveScene().name == "Gameplay") 
        {
            currentGameState = EGameState.GAMEPLAY;
        }
        else Debug.LogError($"There is not scene loaded within this name => {SceneManager.GetActiveScene().name}.");

    }
    void Start()
    {
        #region Managers
            managerUISelection = GameObject.FindObjectOfType<Manager_UI_HeroSelectionScene>();
            managerUIBattle = GameObject.FindObjectOfType<Manager_UI_Battle>();
        #endregion

        #region Controllers
            controllerPlayer = GameObject.FindObjectOfType<Controller_Player>();
        #endregion

        currentCombatTurn = ECombatTurn.HEROES; //At the beginning, heroes starts attacking.
        canCharacterAttack = true;

        if (PlayerPrefs.HasKey("TotalHeroCount")) defaultEnabledHeroCount = PlayerPrefs.GetInt("TotalHeroCount");
        else defaultEnabledHeroCount = 3;
    }

    #region Event Functions
        public void EventFunction_OnBattleStarted_StartBattle()
        {
            Debug.Log("Battle Started");
        }

        private void EventFunction_OnBattleEnded_PlayerVictory()
        {
            List<Controller_Hero> listHero = managerHero.listHero;
            foreach (Controller_Hero hero in listHero)
            {
                if (hero.currentHealth > 0 ) hero.IncreaseExperience();  //If hero is alive
            }

            //UI
            Manager_UI_Battle.OnBattleEnded?.Invoke(EGameplayPanels.VICTORY);
        }
        private void EventFunction_OnBattleEnded_PlayerLost()
        {
            Manager_UI_Battle.OnBattleEnded?.Invoke(EGameplayPanels.DEFEAT);
        }
        private void EventFunction_OnCharacterAttackStarted_CharacterAttackStarted()
        {
            canCharacterAttack = false;
        }
        private void EventFunction_OnCharacterAttackEnded_CharacterAttackEnded()
        {
            //Change turns
            if (currentCombatTurn == ECombatTurn.HEROES) currentCombatTurn = ECombatTurn.ENEMIES;
            else if (currentCombatTurn == ECombatTurn.ENEMIES) currentCombatTurn = ECombatTurn.HEROES;

            if (currentCombatTurn == ECombatTurn.ENEMIES)
            {
                if(managerEnemy.controllerEnemy != null) managerEnemy.controllerEnemy.Attack(managerHero.listHero[UnityEngine.Random.Range(0,managerHero.listHero.Count)]);
            } 
            else if (currentCombatTurn == ECombatTurn.HEROES) 
            {
                if (managerHero.listHero.Count > 0)
                {
                    canCharacterAttack = true;
                    controllerPlayer.selectedHero.DeSelect();
                }
            }
        }

        private void EventFunction_OnEnableNewHero_EnableNewHero()
        {
            if (defaultEnabledHeroCount < totalGameHeroesCount) defaultEnabledHeroCount++;
            PlayerPrefs.SetInt("TotalHeroCount",defaultEnabledHeroCount);
            Debug.Log($"Total Enabled heroes count: {PlayerPrefs.GetInt("TotalHeroCount")}");
        }
    #endregion

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum EGameplayPanels
{
    BATTLE,
    VICTORY,
    DEFEAT
}

public class Manager_UI_Battle : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Transform parentPanel;
    [SerializeField] private GameObject panelBattle;
    [SerializeField] private GameObject panelVictory;
    [SerializeField] private GameObject panelDefeat;
    [Header("Texts")]
    [SerializeField] private TMP_Text textBattleLevel;
    [Header("Buttons")]
    [SerializeField] private Button buttonVictoryReturnToHeroSelection;
    [SerializeField] private Button buttonDefeatReturnToHeroSelection;

    [Header("Gameplay Variables")]
    public int battleLevel;

    private  Manager_Game managerGame;

    #region Events
        public static Action<EGameplayPanels> OnBattleEnded;
    #endregion

    private void OnEnable() 
    {
        OnBattleEnded = EventFunction_OnBattleEnded;
    }

    private void OnDestroy() 
    {
        OnBattleEnded = null;    
    }

    private void Awake()
    {
        managerGame = Manager_Game.Instance;

        SetTexts();
        SetButtons();
        EnablePanel(EGameplayPanels.BATTLE);
    }

    private void SetTexts()
    {
        if (PlayerPrefs.HasKey("BattleLevel"))
        {
            battleLevel = PlayerPrefs.GetInt("BattleLevel");
            textBattleLevel.text = "BATTLE " + battleLevel.ToString();
        }
        else textBattleLevel.text = "BATTLE " + battleLevel;
    }
    
    private void SetButtons()
    {
        buttonVictoryReturnToHeroSelection.onClick.AddListener(() => {
            SceneManager.LoadScene("UI_SelectHeroes");
        });

        buttonDefeatReturnToHeroSelection.onClick.AddListener(() => {
            SceneManager.LoadScene("UI_SelectHeroes");
        });
    }

    public void EnablePanel(EGameplayPanels pGameplayPanel)
    {
        foreach (Transform panel in parentPanel) panel.gameObject.SetActive(false);

        if (pGameplayPanel == EGameplayPanels.BATTLE) panelBattle.SetActive(true);
        else if (pGameplayPanel == EGameplayPanels.VICTORY) panelVictory.SetActive(true);
        else if (pGameplayPanel == EGameplayPanels.DEFEAT) panelDefeat.SetActive(true);
    }

    public void IncreaseBattleLevel()
    {
        battleLevel++;
        PlayerPrefs.SetInt("BattleLevel",battleLevel);

        if (battleLevel % managerGame.enableHeroEveryBattleLevel == 0)  Manager_Game.OnEnableNewHero?.Invoke();
            
    }

    #region Event Functions
        public void EventFunction_OnBattleEnded(EGameplayPanels pGameplayPanel)
        {
            EnablePanel(pGameplayPanel);
            IncreaseBattleLevel();
        }
    #endregion
}



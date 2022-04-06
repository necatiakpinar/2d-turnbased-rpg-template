using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager_UI_HeroSelectionScene : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button buttonStartGame;
    [SerializeField] private Button buttonResetData;
    
    [Header("Texts")]
    [SerializeField] private TMP_Text textRemainingHero;
    
    [Header("Managers & Controllers")]
    public Manager_Game managerGame;
    public Controller_Player controllerPlayer;

    #region Events
        public static Action OnHeroSelected_HeroSelection;
        public static Action OnGameStarted_WithoutMinHero;
    #endregion

    private void OnEnable() 
    {
        OnHeroSelected_HeroSelection = EventFunction_OnHeroSelected_HeroSelection_UpdateUI;
        OnGameStarted_WithoutMinHero = EventFunction_OnGameStarted_HeroSelection_WithoutMinHero;
    }
    private void OnDisable() 
    {
        OnHeroSelected_HeroSelection = null; 
        OnGameStarted_WithoutMinHero = null;
    }

    private void Start()
    {
        managerGame = Manager_Game.Instance;
        controllerPlayer = managerGame.controllerPlayer;
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        buttonStartGame.onClick.AddListener(() =>
        {
            if (controllerPlayer.listSelectedHero.Count >= managerGame.requiredMinHeroCount) //If selected heroes count equal or grater than "3"
            {
                Debug.Log("Starting the game...");
                SceneManager.LoadScene("Gameplay");
            }
            else
            {
                Debug.LogWarning($"Atleast {managerGame.requiredMinHeroCount} hero required to play game!");
                Manager_UI_HeroSelectionScene.OnGameStarted_WithoutMinHero?.Invoke();
            }
        });

        buttonResetData.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    private void EventFunction_OnHeroSelected_HeroSelection_UpdateUI()
    {
        textRemainingHero.text = controllerPlayer.listSelectedHero.Count.ToString() + "/" + managerGame.requiredMinHeroCount.ToString();
    }
    private void EventFunction_OnGameStarted_HeroSelection_WithoutMinHero()
    {
        textRemainingHero.gameObject.GetComponent<Animator>().SetTrigger("Warn");
    }

}

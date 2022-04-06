using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Controller_Hero_Selection : MonoBehaviour
{
    [Header("Hero_Body")]
    public Image imageHero;
    public Color colorHero;

    [Header("Controllers")]
    public Controller_Hero controllerHero;
    public Controller_Hero_Info infoHeroController;

    [Header("Border")]
    [SerializeField]private GameObject border;

    [Header("Timer")]
    [SerializeField] private float holdDuration;

    #region Attributes
        private bool isPressed;
        private float timer;
    #endregion

    #region Managers & Controller
        private Manager_Game managerGame;
        private Controller_Player controllerPlayer;
    #endregion

    public void Initialize(Controller_Hero pControllerHero)
    {
        controllerHero = pControllerHero;
        this.imageHero.sprite = pControllerHero.spriteRendererHero.sprite;
        this.imageHero.color = pControllerHero.spriteRendererHero.color;
    }

    private void Awake() 
    {
        this.GetComponent<Button>().interactable = false;
    }
    private void Start() 
    {
        managerGame = Manager_Game.Instance;
        controllerPlayer = managerGame.controllerPlayer;
        this.isPressed = false;
    }

    private void Update() 
    {
        if (this.isPressed)
        {
            if (IsTimerFinished())
            {
                this.isPressed = false;
                managerGame.managerResources.CreateHeroInfoPanel(this);
            }
        }
    }

    private bool IsTimerFinished()
    {
        if (timer < holdDuration) //Start Timer
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0; //Reset timer
            return true;
        }
        return false;
    }    

    #region Touch Events
        public void OnPointerClick()
        {
            if (controllerHero != null && infoHeroController == null)
            {
                if (controllerHero.properties.IsOwnedByPlayer)
                {
                    if (!controllerHero.properties.IsSelectedOnDeck && controllerPlayer.listSelectedHero.Count < managerGame.requiredMinHeroCount)
                    {
                        this.border.SetActive(true);
                        controllerHero.properties.IsSelectedOnDeck = true;
                        Controller_Player.Instance.AddHero(controllerHero);
                    } 
                    else
                    {
                        this.border.SetActive(false);
                        controllerHero.properties.IsSelectedOnDeck = false;
                        Controller_Player.Instance.RemoveHero(controllerHero);
                    } 

                    Manager_UI_HeroSelectionScene.OnHeroSelected_HeroSelection?.Invoke();
                }
            }
        }

        public void OnPointerDown()
        {
            isPressed = true;
        }

        public void OnPointerUp()
        {
            isPressed = false;
            if (infoHeroController != null) GameObject.Destroy(infoHeroController.gameObject); //If info panel is enabled, delete it.
        }
    #endregion

    public void EnableHero()
    {
        this.GetComponent<Button>().interactable = true;
        controllerHero.EnableHero();
    }
}

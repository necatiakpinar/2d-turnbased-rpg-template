using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{   
    public List<Controller_Hero> listSelectedHero;
    public Controller_Hero selectedHero;
    public Controller_Enemy selectedEnemy;
    
    #region Attributes
        public bool isHeroPressed;
        public bool isEnemyPressed;
        private float timer;
    [SerializeField] private float holdDuration;
    #endregion

    #region Managers & Controllers
        private Manager_Game managerGame;
    #endregion

    public static Controller_Player Instance;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        managerGame = Manager_Game.Instance;
    }

    public void AddHero(Controller_Hero pHero)
    {
        this.listSelectedHero.Add(pHero);
    }
    public void RemoveHero(Controller_Hero pHero)
    {
        this.listSelectedHero.Remove(pHero);
    }

    private void Update()
    {
        DetectColliders();
        CheckCharacterInfoVisibility();
    }

    private void DetectColliders()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && managerGame.canCharacterAttack)
            {
                if (hit.collider.GetComponent<Controller_Hero>() != null)
                {
                    this.isHeroPressed = true;
                    if (hit.collider.GetComponent<Controller_Hero>().controllerHeroInfo != null) 
                        GameObject.Destroy(hit.collider.GetComponent<Controller_Hero>().controllerHeroInfo); //If info panel is enabled, delete it.

                    if (hit.collider.GetComponent<Controller_Hero>() != null)
                    {
                        this.selectedHero = hit.collider.GetComponent<Controller_Hero>();
                        managerGame.managerHero.SelectHero(selectedHero);
                    }
                }
                else if (hit.collider.GetComponent<Controller_Enemy>() != null)
                {
                    this.isEnemyPressed = true;
                    this.selectedEnemy = hit.collider.GetComponent<Controller_Enemy>();
                    if (selectedHero != null && managerGame.canCharacterAttack) selectedHero.Attack(selectedEnemy);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (this.isHeroPressed)
                {
                    if (IsTimerFinished())
                    {
                        this.isHeroPressed = false;
                        managerGame.managerResources.CreateHeroInfoPanel(hit.collider.GetComponent<Controller_Hero>());
                    }
                }
                else if(this.isEnemyPressed && selectedHero == null)
                {
                    if (IsTimerFinished())
                    {
                        this.isEnemyPressed = false;
                        managerGame.managerResources.CreateEnemyInfoPanel(hit.collider.GetComponent<Controller_Enemy>());
                    }
                }
            }
        }
    }
    private void CheckCharacterInfoVisibility()
    {
        if (Input.GetMouseButtonUp(0) && managerGame.canCharacterAttack && selectedHero != null)
        {
            this.timer = 0;
            this.isHeroPressed = false;
            if (selectedHero.GetComponent<Controller_Hero>().controllerHeroInfo != null)
                GameObject.Destroy(selectedHero.GetComponent<Controller_Hero>().controllerHeroInfo.gameObject); //If info panel is enabled, delete it.
        }
        else if (Input.GetMouseButtonUp(0) && managerGame.canCharacterAttack && selectedEnemy != null)
        {
            this.timer = 0;
            this.isEnemyPressed = false;
            if (selectedEnemy.GetComponent<Controller_Enemy>().controllerEnemyInfo != null)
                GameObject.Destroy(selectedEnemy.GetComponent<Controller_Enemy>().controllerEnemyInfo.gameObject); //If info panel is enabled, delete it.
        }
    }

    private bool IsTimerFinished()
    {
        if (timer < holdDuration) timer += Time.deltaTime;
        else
        {
            timer = 0; //Reset timer
            return true;
        }
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Controller_Hero : Base_CombatActor
{
    public SO_Hero_Properties properties;
    [Header("Current Properties")]
    [SerializeField] private float currentAttackPower;
    [SerializeField] private int currentExperience;
    [SerializeField] private int currentLevel;

    [Header("Hero_Body")]
    public SpriteRenderer spriteRendererHero;
    public GameObject selectionBorder;
    
    public bool isSelected;

    [Header("Managers & Controllers")]
    private Manager_Game managerGame;
    private Manager_Hero managerHero;
    public Controller_Hero_Info controllerHeroInfo;
    private Controller_Player controllerPlayer;


    private void Start()
    {
        managerGame = Manager_Game.Instance;
        managerHero = managerGame.managerHero;
        controllerPlayer = managerGame.controllerPlayer;

        InitializeHeroProperties();

        //UI
        sliderHealth.minValue = 0;
        sliderHealth.maxValue = currentHealth;
        sliderHealth.value = currentHealth;

        this.selectionBorder.SetActive(false);
    }

    public void InitializeHeroProperties()
    {
        if (PlayerPrefs.HasKey("ExtraHealth" + this.properties.HeroID.ToString())) this.properties.ExtraHealth = PlayerPrefs.GetFloat("ExtraHealth" + this.properties.HeroID.ToString());
        else this.properties.ExtraHealth = 0;

        if (PlayerPrefs.HasKey("ExtraAttackPower" + this.properties.HeroID.ToString())) this.properties.ExtraAttackPower = PlayerPrefs.GetFloat("ExtraAttackPower" + this.properties.HeroID.ToString());
        else this.properties.ExtraAttackPower = 0;

        if (PlayerPrefs.HasKey("Experience" + this.properties.HeroID.ToString())) this.properties.Experience = PlayerPrefs.GetInt("Experience" + this.properties.HeroID.ToString());
        else this.properties.Experience = 0;

        if (PlayerPrefs.HasKey("Level" + this.properties.HeroID.ToString())) this.properties.Level = PlayerPrefs.GetInt("Level" + this.properties.HeroID.ToString());
        else this.properties.Level = 0;

        if (PlayerPrefs.HasKey("IsOwned" + this.properties.HeroID.ToString()))
        {
            if (PlayerPrefs.GetInt("IsOwned" + this.properties.HeroID.ToString()) == 0) this.properties.IsOwnedByPlayer = false;
            else if (PlayerPrefs.GetInt("IsOwned" + this.properties.HeroID.ToString()) == 1) this.properties.IsOwnedByPlayer = true;
        } 
        else this.properties.IsOwnedByPlayer = false;

        currentHealth = properties.Health + properties.ExtraHealth;
        currentAttackPower = properties.AttackPower + properties.ExtraAttackPower;
    }

    public override void Attack(Base_CombatActor pEnemy)
    {
        base.Attack(pEnemy);
        Debug.Log($"{properties.name} attacking! " );
        Vector3 startingPosition = this.transform.position;
        this.transform.DOMove(pEnemy.transform.position,1.5f).OnComplete( //Move to enemy position
            delegate
             {
                if(pEnemy != null) pEnemy.TakeDamage(this.currentAttackPower); //Attack the enemy

                this.transform.DOMove(startingPosition,1.5f).OnComplete( //Return original position.
                    delegate 
                    {
                        Manager_Game.OnCharacterAttackEnded?.Invoke(); //Call event to state ending. 
                    });
                });
    }

    public override void TakeDamage(float pDamage)
    {
        base.TakeDamage(pDamage);
        Debug.Log($"{properties.name} took damage! " );
        sliderHealth.value = currentHealth;
    }

    public override void Die()
    {
        Manager_Hero.OnHeroDied?.Invoke(this);
        base.Die();
    }

    public void Select()
    {
        this.isSelected = true;
        this.selectionBorder.SetActive(true);
        controllerPlayer.selectedHero = this;
    }
    public void DeSelect()
    {
        this.isSelected = false;
        this.selectionBorder.SetActive(false);
        controllerPlayer.selectedHero = null;
    }

    #region Hero Data
        public void IncreaseExperience()
        {
            properties.Experience++;
            PlayerPrefs.SetInt("Experience" + this.properties.HeroID.ToString(),properties.Experience);
            if (properties.Experience % managerGame.requiredExperienceToLevelUP == 0) IncreaseLevel();
        }
        public void IncreaseLevel()
        {
            properties.Level++;
            PlayerPrefs.SetInt("Level" + this.properties.HeroID.ToString(),properties.Level);
            IncreaseAttackPower(managerGame.extraAttackPowerMultiplier);
            IncreaseHealth(managerGame.extraHealthMultiplier);
        }
        public void IncreaseAttackPower(int pAttackPowerIncreaseRatio)
        {
            this.properties.ExtraAttackPower += (currentAttackPower / pAttackPowerIncreaseRatio); 
            PlayerPrefs.SetFloat("ExtraAttackPower"+this.properties.HeroID.ToString(),this.properties.ExtraAttackPower);
        }
        public void IncreaseHealth(int pHealthIncreaseRatio)
        {
            this.properties.ExtraHealth += ((properties.Health + properties.ExtraHealth) / pHealthIncreaseRatio); 
            PlayerPrefs.SetFloat("ExtraHealth"+this.properties.HeroID.ToString(),this.properties.ExtraHealth);
        }

        public void EnableHero()
        {
            this.properties.IsOwnedByPlayer = true;
            PlayerPrefs.SetInt("IsOwned" + this.properties.HeroID.ToString(),1);
        }
    #endregion

}



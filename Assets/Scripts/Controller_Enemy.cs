using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller_Enemy : Base_CombatActor
{
    public SO_Enemy_Properties properties;
    [SerializeField] private float currentAttackPower;
    
    public Controller_Enemy_Info controllerEnemyInfo;
    private Manager_Game managerGame;
    private Manager_Hero managerHero;

    private void Start() 
    {
        managerGame = Manager_Game.Instance;
        managerHero = managerGame.managerHero;

        if (PlayerPrefs.HasKey("Enemy_ExtraAttackPower" + this.properties.EnemyID.ToString())) this.properties.ExtraAttackPower = PlayerPrefs.GetFloat("Enemy_ExtraAttackPower" + this.properties.EnemyID.ToString());
        else this.properties.ExtraAttackPower = 0;

        if (PlayerPrefs.HasKey("Enemy_ExtraHealth" + this.properties.EnemyID.ToString())) this.properties.ExtraHealth = PlayerPrefs.GetFloat("Enemy_ExtraHealth" + this.properties.EnemyID.ToString());
        else this.properties.ExtraHealth = 0;


        currentHealth = properties.Health + properties.ExtraHealth;
        currentAttackPower = properties.AttackPower + properties.ExtraAttackPower;
        sliderHealth.minValue = 0;
        sliderHealth.maxValue = currentHealth;
        sliderHealth.value = currentHealth;

    }

    public override void Attack(Base_CombatActor pHero)
    {
        base.Attack(pHero);

        Vector3 startingPosition = this.transform.position;
        this.transform.DOMove(pHero.transform.position,1.5f).OnComplete( //Move to enemy position
            delegate
            {
                if (pHero != null) pHero.TakeDamage(this.currentAttackPower); //Attack the enemy
                
                this.transform.DOMove(startingPosition,1.5f).OnComplete( //Return original position.
                    delegate 
                    {
                        Manager_Game.OnCharacterAttackEnded?.Invoke(); //Call event to state ending. 
                    });
            }
        );
    }

    public override void TakeDamage(float pDamage)
    {
        base.TakeDamage(pDamage);
        Debug.Log("Enemy took damage!");
        sliderHealth.value = currentHealth;
    }

    public override void Die()
    {
        IncreaseAttackPower(managerGame.extraAttackPowerMultiplier);
        IncreaseHealth(managerGame.extraHealthMultiplier);
        Manager_Enemy.OnEnemyDied?.Invoke();
        base.Die();
    }

    #region Enemy Data
        public void IncreaseAttackPower(int pAttackPowerIncreaseRatio)
        {
            this.properties.ExtraAttackPower += (currentAttackPower / pAttackPowerIncreaseRatio); 
            PlayerPrefs.SetFloat("Enemy_ExtraAttackPower" + this.properties.EnemyID.ToString(),this.properties.ExtraAttackPower);
        }
        public void IncreaseHealth(int pHealthIncreaseRatio)
        {
            this.properties.ExtraHealth += ((properties.Health + properties.ExtraHealth) / pHealthIncreaseRatio); 
            PlayerPrefs.SetFloat("Enemy_ExtraHealth" + this.properties.EnemyID.ToString(),this.properties.ExtraHealth);
        }
    #endregion


}

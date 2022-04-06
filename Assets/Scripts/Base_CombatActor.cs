using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class Base_CombatActor : MonoBehaviour
{
    [Header("UI")]
    public GameObject actorUI;
    public Slider sliderHealth;
    public TMP_Text textHitDamage;
    public float currentHealth;

    public virtual void Attack(Base_CombatActor pHero)
    {
        Manager_Game.OnCharacterAttackStarted?.Invoke();        
    }

    public virtual void TakeDamage(float pDamage)
    {
        Manager_Game.Instance.managerResources.CreateDamageFloatingText(this.actorUI.transform,(int)pDamage);
        currentHealth -= pDamage;
        if (currentHealth <= 0) Die();
    }

    public virtual void Die()
    {   
        GameObject.Destroy(this.gameObject);
    }
}

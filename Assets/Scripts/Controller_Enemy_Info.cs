using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller_Enemy_Info : MonoBehaviour
{
    [Header("Texts")]
    public TMP_Text textEnemyHealth;
    public TMP_Text textAttackPower;
    
    [Header("Position Variables")]
    public int EnemyOffsetX = 4;
    public int EnemyOffsetY = 3;

    public void SetEnemyInformations(float pEnemyHealth, float pEnemyAttackPower)
    {
        this.textEnemyHealth.text = "Health: " + pEnemyHealth.ToString();
        this.textAttackPower.text = "Attack Power: " + pEnemyAttackPower.ToString();
    }

    public void InitializeEnemyInfo(Controller_Enemy pEnemy)
    {
        this.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(new Vector2(pEnemy.transform.position.x + EnemyOffsetX, pEnemy.transform.position.y + EnemyOffsetY));
        pEnemy.controllerEnemyInfo = this;
        SetEnemyInformations(pEnemy.properties.Health + pEnemy.properties.ExtraHealth,
                             pEnemy.properties.AttackPower + pEnemy.properties.ExtraAttackPower
                            );
    }

}

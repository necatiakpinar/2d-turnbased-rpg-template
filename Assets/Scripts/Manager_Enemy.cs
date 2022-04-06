using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Enemy : MonoBehaviour
{
    public Controller_Enemy controllerEnemy;

    private Manager_Game managerGame;

    #region Events
        public static Action OnEnemyDied;
    #endregion
    private void OnEnable() 
    {
        OnEnemyDied = EventFunction_OnEnemyDied_EnemyDied;
    }
    private void OnDestroy() 
    {
        OnEnemyDied = null;    
    }

    private void Start() 
    {
        managerGame = Manager_Game.Instance;
    }

    public void EventFunction_OnEnemyDied_EnemyDied()
    {
        controllerEnemy = null; //Normally remove from the list, however we have only one enemy for now. 
        if (controllerEnemy == null)  Manager_Game.OnBattleEndedPlayerVictory?.Invoke();
            
    }
}

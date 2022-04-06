using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy_Properties", order = 1)]
public class SO_Enemy_Properties : ScriptableObject
{
    #region Variables
        [SerializeField] private int enemyID;
        [SerializeField] private float health;
        [SerializeField] private float extraHealth;
        [SerializeField] private float attackPower;
        [SerializeField] private float extraAttackPower;
        [SerializeField] private int level;
    #endregion

    #region Properties
        public int EnemyID { get {return enemyID;} set {enemyID = value;} }
        public float Health { get {return health;} set {health = value;} }
        public float ExtraHealth { get {return extraHealth;} set {extraHealth = value;} }
        public float AttackPower { get {return attackPower;} set {attackPower = value;} }
        public float ExtraAttackPower { get {return extraAttackPower;} set {extraAttackPower = value;} }
        public int Level { get {return level;} set {level = value;} }
    #endregion

}

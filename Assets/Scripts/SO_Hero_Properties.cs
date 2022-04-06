using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EHeroType{
    MELEE,
    RANGED
}

[CreateAssetMenu(fileName = "Hero", menuName = "ScriptableObjects/Hero_Properties", order = 1)]
public class SO_Hero_Properties : ScriptableObject
{
    #region Variables
        [SerializeField] private int heroID;
        [SerializeField] private string heroName;
        [SerializeField] private float health;
        [SerializeField] private float extraHealth;
        [SerializeField] private float attackPower;
        [SerializeField] private float extraAttackPower;
        
        [SerializeField] private int experience;
        [SerializeField] private int level;
        [SerializeField] private EHeroType heroType;
        [SerializeField] private bool isOwnedByPlayer; 
        [SerializeField] private bool isSelectedOnDeck; 
            
    #endregion

    #region Properties
        public int HeroID { get {return heroID;} set {} }
        public string HeroName { get {return heroName;} set {} }
        public float Health { get {return health;} set {health = value;} }
        public float ExtraHealth { get {return extraHealth;} set {extraHealth = value;} }
        public float AttackPower { get {return attackPower;} set {attackPower = value;} }
        public float ExtraAttackPower { get {return extraAttackPower;} set {extraAttackPower = value;} }
        public int Experience { get {return experience;} set {experience = value;} }
        public int Level { get {return level;} set {level = value;} }
        public EHeroType HeroType { get {return heroType;} set {heroType = value;} }
        public bool IsOwnedByPlayer { get {return isOwnedByPlayer;} set {isOwnedByPlayer = value;} }
        public bool IsSelectedOnDeck { get {return isSelectedOnDeck;} set {isSelectedOnDeck = value;} }
    #endregion

}

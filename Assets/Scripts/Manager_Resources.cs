using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Manager_Resources : MonoBehaviour
{
    [Header("Hero Prefab")]
    public List<GameObject> listPrefabHero;
    [Header("Hero Info Panel")]
    public GameObject prefabInfoHero;
    public GameObject prefabInfoEnemy;
    [Header("Hero Prefab")]
    public List<SO_Hero_Properties> listHeroProperties;
    [Header("UI")]
    public GameObject prefabFloatingDamageText;
    public GameObject prefabHeroSelectionThumbnail;
    private Manager_Game managerGame;

    private void Start() 
    {
        managerGame = Manager_Game.Instance;
    }

    #region UI Functions
        public void CreateHeroInfoPanel(Controller_Hero_Selection pHero)
        {
            Transform infoHeroParent = Manager_Game.Instance.managerUISelection.transform;
            GameObject infoHero = GameObject.Instantiate(prefabInfoHero,infoHeroParent);
            Controller_Hero_Info infoHeroController = infoHero.GetComponent<Controller_Hero_Info>();

            infoHeroController.InitializeHeroInfo(pHero);
        }

        public void CreateHeroInfoPanel(Controller_Hero pHero)
        {
            Transform infoHeroParent = Manager_Game.Instance.managerUIBattle.transform;
            GameObject infoHero = GameObject.Instantiate(prefabInfoHero,infoHeroParent);
            Controller_Hero_Info infoHeroController = infoHero.GetComponent<Controller_Hero_Info>();

            infoHeroController.InitializeHeroInfo(pHero);
        }

        public void CreateEnemyInfoPanel(Controller_Enemy pEnemy)
        {
            Transform infoEnemyParent = Manager_Game.Instance.managerUIBattle.transform;
            GameObject infoEnemy = GameObject.Instantiate(prefabInfoEnemy,infoEnemyParent);
            Controller_Enemy_Info infoEnemyController = infoEnemy.GetComponent<Controller_Enemy_Info>();

            infoEnemyController.InitializeEnemyInfo(pEnemy);
        }

        public void CreateDamageFloatingText(Transform pParentUI,int pDamageAmounth)
        {
            GameObject floatingDamageText = GameObject.Instantiate(prefabFloatingDamageText,pParentUI);
            floatingDamageText.GetComponent<TextMeshProUGUI>().text = pDamageAmounth.ToString();
            Destroy(floatingDamageText,1);
        }
    #endregion
}



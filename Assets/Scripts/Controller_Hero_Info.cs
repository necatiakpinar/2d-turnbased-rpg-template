using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller_Hero_Info : MonoBehaviour
{
    [Header("Texts")]
    public TMP_Text textHeroName;
    public TMP_Text textHeroHealth;
    public TMP_Text textHeroLevel;
    public TMP_Text textAttackPower;
    public TMP_Text textExperience;

    [Header("Position Variables")]
    public int heroOffsetX = 4;
    public int heroOffsetY = 3;

    public void SetHeroInformations(string pHeroName,float pHeroHealth,int pHeroLevel, float pHeroAttackPower,int pHeroExperience)
    {
        this.textHeroName.text = "Name: " + pHeroName;
        this.textHeroHealth.text = "Health: " + pHeroHealth.ToString();
        this.textHeroLevel.text = "Level: " + pHeroLevel.ToString();
        this.textAttackPower.text = "Attack Power: " + pHeroAttackPower.ToString();
        this.textExperience.text = "Experience: " + pHeroExperience.ToString();
    }

    public void InitializeHeroInfo(Controller_Hero_Selection pHeroSelection)
    {
        this.GetComponent<RectTransform>().anchoredPosition3D = pHeroSelection.transform.GetComponent<RectTransform>().anchoredPosition3D;
        pHeroSelection.infoHeroController = this;
        SetHeroInformations(pHeroSelection.controllerHero.properties.HeroName,
                            pHeroSelection.controllerHero.properties.Health + pHeroSelection.controllerHero.properties.ExtraHealth,
                            pHeroSelection.controllerHero.properties.Level,
                            pHeroSelection.controllerHero.properties.AttackPower,
                            pHeroSelection.controllerHero.properties.Experience);
    }
    public void InitializeHeroInfo(Controller_Hero pHero)
    {
        this.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(new Vector2(pHero.transform.position.x + heroOffsetX, pHero.transform.position.y + heroOffsetY));
        pHero.controllerHeroInfo = this;
        SetHeroInformations(pHero.properties.HeroName,
                            pHero.properties.Health + pHero.properties.ExtraHealth,
                            pHero.properties.Level,
                            pHero.properties.AttackPower + pHero.properties.ExtraAttackPower,
                            pHero.properties.Experience);
    }

}

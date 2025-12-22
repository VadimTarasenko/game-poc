using UnityEngine;
using TMPro;

public class HeroSceneController : MonoBehaviour
{
    public HeroData hero;
    public TMP_Text heroAgilityText;
    public TMP_Text heroPowerText;
    public TMP_Text heroIntelligenceText;
    public TMP_Text heroLevelText;
    
    void Start()
    {
    }

    void Update()
    {
        heroAgilityText.text = hero.agility.ToString() + " agility";
        heroPowerText.text = hero.power.ToString() + " power";
        heroIntelligenceText.text = hero.intelligence.ToString() + " strength";
        heroLevelText.text = HeroLevel.GetLevelFromXP(hero.experience).ToString();
    }
}

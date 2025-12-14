using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using TMPro;
using System.Collections.Generic;

public class HomeController : MonoBehaviour
{
    public HeroData hero;
    public TMP_Text heroAgilityText;
    public TMP_Text heroPowerText;
    public TMP_Text heroIntelligenceText;

    void Start()
    {
    }

    void Update()
    {
        heroAgilityText.text = hero.agility.ToString();
        heroPowerText.text = hero.power.ToString();
        heroIntelligenceText.text = hero.intelligence.ToString();
        Debug.Log("Experience: " + hero.experience);
    }

    public void OnJourneyClick()
    {
        SceneManager.LoadScene("HomeScene");
    }

}

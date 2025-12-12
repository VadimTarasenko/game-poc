using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject characterScreen;
    public GameObject journeyScreen;
    public GameObject levelScreen;
    public GameObject rankingScreen;
    public GameObject upgradesScreen;
    public GameObject shopScreen;
    public GameObject spellsScreen;
    public GameObject settingsScreen;
    public GameObject gamePlayScreen;

    private void DisableAllScreens()
    {
        mainScreen.SetActive(false);
        characterScreen.SetActive(false);
        journeyScreen.SetActive(false);
        levelScreen.SetActive(false);
        rankingScreen.SetActive(false);
        upgradesScreen.SetActive(false);
        shopScreen.SetActive(false);
        spellsScreen.SetActive(false);
        settingsScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
    }

    public void ShowMainScreen()
    {
        DisableAllScreens();
        mainScreen.SetActive(true);
    }

    public void ShowCharacterScreen()
    {
        DisableAllScreens();
        characterScreen.SetActive(true);
    }

    public void ShowJourneyScreen()
    {
        DisableAllScreens();
        journeyScreen.SetActive(true);
    }

    public void ShowLevelScreen()
    {
        DisableAllScreens();
        levelScreen.SetActive(true);
    }

    public void ShowRankingScreen()
    {
        DisableAllScreens();
        rankingScreen.SetActive(true);
    }

    public void ShowUpgradesScreen()
    {
        DisableAllScreens();
        upgradesScreen.SetActive(true);
    }

    public void ShowShopScreen()
    {
        DisableAllScreens();
        shopScreen.SetActive(true);
    }

    public void ShowSpellsScreen()
    {
        DisableAllScreens();
        spellsScreen.SetActive(true);
    }

    public void ShowSettingsScreen()
    {
        DisableAllScreens();
        settingsScreen.SetActive(true);
    }

    public void ShowGamePlayScreen()
    {
        DisableAllScreens();
        gamePlayScreen.SetActive(true);
    }
}

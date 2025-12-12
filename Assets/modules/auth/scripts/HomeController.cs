using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using TMPro;
using System.Collections.Generic;

public class HomeController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text agilityText;
    public TMP_Text strengthText;
    public TMP_Text intelligenceText;

    private FirebaseAuth auth;
    private FirebaseUser currentUser;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        currentUser = auth.CurrentUser;

        if (currentUser != null)
        {
            LoadHeroData();
        }
        else
        {
            SceneManager.LoadScene("LogInScene");
            Debug.LogError("No user logged in!");
        }
    }

    void LoadHeroData()
    {
        string userId = currentUser.UserId;

        Debug.Log($"Loading hero data for user: {userId}");

        // Query user data from Firestore by userId field
        FirestoreManager.Instance.QueryDocumentsByField("heroes", "userId", userId,
            onSuccess: (data) =>
            {
                DisplayHeroStats(data);
            },
            onError: (error) =>
            {
                Debug.LogError($"Failed to load user data: {error}");
            }
        );
    }

    void DisplayHeroStats(Dictionary<string, object> data)
    {
        // Display agility
        if (agilityText != null && data.ContainsKey("agility"))
        {
            agilityText.text = $"{data["agility"]}";
        }

        // Display strength
        if (strengthText != null && data.ContainsKey("strength"))
        {
            strengthText.text = $"{data["strength"]}";
        }

        // Display intelligence
        if (intelligenceText != null && data.ContainsKey("intelligence"))
        {
            intelligenceText.text = $"{data["intelligence"]}";
        }

        Debug.Log("User stats displayed successfully");
    }
}

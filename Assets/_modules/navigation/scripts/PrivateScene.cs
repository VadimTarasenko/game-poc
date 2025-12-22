using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class PrivateScene : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        if (auth.CurrentUser == null)
        {
            SceneManager.LoadScene("LogInScene");
        }
    }
}

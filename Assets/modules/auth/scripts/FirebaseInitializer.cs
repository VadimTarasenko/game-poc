using Firebase;
using Firebase.Auth;
using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;

                // Note: Firebase Auth emulator connection in Unity requires environment variables
                // Set FIREBASE_AUTH_EMULATOR_HOST=localhost:9099 before starting Unity

                Debug.Log("Firebase initialized successfully.");
            }
            else
            {
                Debug.LogError($"Could not resolve Firebase dependencies: {task.Result}");
            }
        });
    }
}
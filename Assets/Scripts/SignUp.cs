// 12/5/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

// 12/5/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using Firebase.Auth;
using UnityEngine;
using System.Threading.Tasks;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    // Method to be called by the Button
    public void OnCreateAccountButtonClick()
    {
        // Call the async SignUp method
        SignUp();
    }

    private async void SignUp()
    {
        string email = "test@example.com"; // Replace with actual email input
        string password = "password123";   // Replace with actual password input

        try
        {
            // Use async/await for cleaner code
            AuthResult authResult = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            FirebaseUser newUser = authResult.User; // Access the User property from AuthResult
            Debug.Log($"User signed up successfully: {newUser.Email}");
        }
        catch (System.Exception ex)
        {
            // Log any error
            Debug.LogError($"Sign-up failed: {ex.Message}");
        }
    }
}
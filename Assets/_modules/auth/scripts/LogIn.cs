using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using TMPro;

public class LogIn : MonoBehaviour
{
    private FirebaseAuth auth;

    public TMP_InputField emailField;
    public TMP_InputField passwordField;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        // Check if user is already logged in
        if (auth.CurrentUser != null)
        {
            Debug.Log($"User already logged in: {auth.CurrentUser.Email}");
            // SceneManager.LoadScene("Home");
        }
    }

    // Method to be called when the Log In button is clicked
    public void OnLogInButtonClick()
    {
        string email = emailField.text;
        string password = passwordField.text;

        Debug.Log($"Logging in with email: {email} and password: {password}");

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                AuthResult authResult = task.Result;
                FirebaseUser user = authResult.User;
                Debug.Log($"User logged in successfully: {user.Email}");
                // Load the "Story" scene after successful login
                SceneManager.LoadScene("Home");
            }
            else
            {
                Debug.LogError($"Login failed: {task.Exception}");
            }
        });
    }

    public void OnSignUpButtonClick()
    {
        auth.CreateUserWithEmailAndPasswordAsync("vadym.tarasenko99@gmail.com", "qwe123456").ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                AuthResult authResult = task.Result;
                FirebaseUser user = authResult.User;
                Debug.Log($"User created successfully: {user.Email}");
            }
            else
            {
                Debug.LogError($"User creation failed: {task.Exception}");
            }
        });
    }
}
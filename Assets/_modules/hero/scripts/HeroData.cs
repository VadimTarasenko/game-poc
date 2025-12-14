using UnityEngine;
using Firebase;
using Firebase.Auth;

public class HeroData : MonoBehaviour
{
    public float agility;
    public float power;
    public float intelligence;
    public int experience;

    private FirebaseAuth auth;
    private FirebaseUser currentUser;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        currentUser = auth.CurrentUser;

        if (currentUser != null) {
            FirestoreManager.Instance.QueryDocumentsByField("heroes", "userId", currentUser.UserId,
            onSuccess: (data) => {
                agility = System.Convert.ToSingle(data["agility"]);
                power = System.Convert.ToSingle(data["power"]);
                intelligence = System.Convert.ToSingle(data["intelligence"]);
                experience = System.Convert.ToInt32(data["experience"]);
            },
            onError: (error) => {
                Debug.LogError("Failed to load hero data: " + error);
            });
        } else {
            Debug.LogError("No user logged in!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

public class FirestoreManager : MonoBehaviour
{
    private FirebaseFirestore db;
    public static FirestoreManager Instance { get; private set; }

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize Firestore
            db = FirebaseFirestore.DefaultInstance;

            Debug.Log("Firestore initialized successfully with production database.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Create or update a document
    public void SaveDocument(string collectionPath, string documentId, Dictionary<string, object> data, Action onSuccess = null, Action<string> onError = null)
    {
        db.Collection(collectionPath).Document(documentId).SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                Debug.Log($"Document saved successfully: {collectionPath}/{documentId}");
                onSuccess?.Invoke();
            }
            else
            {
                string error = task.Exception?.ToString() ?? "Unknown error";
                Debug.LogError($"Failed to save document: {error}");
                onError?.Invoke(error);
            }
        });
    }

    // Read a document
    public void GetDocument(string collectionPath, string documentId, Action<Dictionary<string, object>> onSuccess, Action<string> onError = null)
    {
        db.Collection(collectionPath).Document(documentId).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Debug.Log($"Document retrieved: {collectionPath}/{documentId}");
                    onSuccess?.Invoke(snapshot.ToDictionary());
                }
                else
                {
                    Debug.LogWarning($"Document does not exist: {collectionPath}/{documentId}");
                    onError?.Invoke("Document does not exist");
                }
            }
            else
            {
                string error = task.Exception?.ToString() ?? "Unknown error";
                Debug.LogError($"Failed to get document: {error}");
                onError?.Invoke(error);
            }
        });
    }

    // Read all documents in a collection
    public void GetCollection(string collectionPath, Action<List<Dictionary<string, object>>> onSuccess, Action<string> onError = null)
    {
        db.Collection(collectionPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                QuerySnapshot snapshot = task.Result;
                List<Dictionary<string, object>> documents = new List<Dictionary<string, object>>();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    documents.Add(document.ToDictionary());
                }

                Debug.Log($"Retrieved {documents.Count} documents from {collectionPath}");
                onSuccess?.Invoke(documents);
            }
            else
            {
                string error = task.Exception?.ToString() ?? "Unknown error";
                Debug.LogError($"Failed to get collection: {error}");
                onError?.Invoke(error);
            }
        });
    }

    // Update specific fields in a document
    public void UpdateDocument(string collectionPath, string documentId, Dictionary<string, object> updates, Action onSuccess = null, Action<string> onError = null)
    {
        db.Collection(collectionPath).Document(documentId).UpdateAsync(updates).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                Debug.Log($"Document updated successfully: {collectionPath}/{documentId}");
                onSuccess?.Invoke();
            }
            else
            {
                string error = task.Exception?.ToString() ?? "Unknown error";
                Debug.LogError($"Failed to update document: {error}");
                onError?.Invoke(error);
            }
        });
    }

    // Delete a document
    public void DeleteDocument(string collectionPath, string documentId, Action onSuccess = null, Action<string> onError = null)
    {
        db.Collection(collectionPath).Document(documentId).DeleteAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                Debug.Log($"Document deleted successfully: {collectionPath}/{documentId}");
                onSuccess?.Invoke();
            }
            else
            {
                string error = task.Exception?.ToString() ?? "Unknown error";
                Debug.LogError($"Failed to delete document: {error}");
                onError?.Invoke(error);
            }
        });
    }

    // Listen to real-time updates on a document
    public ListenerRegistration ListenToDocument(string collectionPath, string documentId, Action<Dictionary<string, object>> onUpdate)
    {
        return db.Collection(collectionPath).Document(documentId).Listen(snapshot =>
        {
            if (snapshot.Exists)
            {
                Debug.Log($"Document updated in real-time: {collectionPath}/{documentId}");
                onUpdate?.Invoke(snapshot.ToDictionary());
            }
        });
    }

    // Query documents by a specific field
    public void QueryDocumentsByField(string collectionPath, string fieldName, object fieldValue, Action<Dictionary<string, object>> onSuccess, Action<string> onError = null)
    {
        db.Collection(collectionPath).WhereEqualTo(fieldName, fieldValue).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                QuerySnapshot snapshot = task.Result;
                if (snapshot.Count > 0)
                {
                    DocumentSnapshot document = snapshot.Documents.First();
                    Debug.Log($"Document found with {fieldName} = {fieldValue}");
                    onSuccess?.Invoke(document.ToDictionary());
                }
                else
                {
                    Debug.LogWarning($"No document found with {fieldName} = {fieldValue}");
                    onError?.Invoke($"No document found with {fieldName} = {fieldValue}");
                }
            }
            else
            {
                string error = task.Exception?.ToString() ?? "Unknown error";
                Debug.LogError($"Failed to query documents: {error}");
                onError?.Invoke(error);
            }
        });
    }
}

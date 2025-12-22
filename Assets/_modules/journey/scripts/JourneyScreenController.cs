using UnityEngine;
using UnityEngine.SceneManagement;

public class JourneyScreenController : MonoBehaviour
{
    public void OnCancelClick()
    {
        SceneManager.LoadScene("Home");
    }

    public void PlayJourneyScene()
    {
        SceneManager.LoadScene("map_v2");
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryNext : MonoBehaviour
{
    public void OnStoryNextButtonClick()
    {
        SceneManager.LoadScene("SelectHero");
    }
}
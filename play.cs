using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        // Load the game scene
        SceneManager.LoadScene("gamescene");
    }
}
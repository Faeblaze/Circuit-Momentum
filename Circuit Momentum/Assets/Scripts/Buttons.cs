using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject scorePane;

    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void HighScores()
    {
        if (scorePane)
            scorePane.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Menu");
    }

    public void CloseAll()
    {
        if (scorePane)
            scorePane.SetActive(false);
    }
}

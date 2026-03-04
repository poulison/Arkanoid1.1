using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 1);
        finalScoreText.text = "SCORE: " + finalScore.ToString("0000");
    }

    public void PlayAgain()
    {
        int lastSceneIndex = PlayerPrefs.GetInt("LastScene", 1);
        SceneManager.LoadScene(lastSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }
}
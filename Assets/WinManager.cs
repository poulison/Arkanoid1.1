using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "Final Score: " + finalScore;
    }

    public void PlayAgain()
    {
        PlayerPrefs.DeleteKey("AccumulatedScore");
        PlayerPrefs.DeleteKey("FinalScore");
        SceneManager.LoadScene("cena_1");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager2 : MonoBehaviour
{   
    public static GameManager2 instance;

    private int totalBricks;

    [Header("Game Settings")]
    public int lives = 3;
    public int score = 0;

    [Header("UI")]
    public Image[] hearts;
    public TextMeshProUGUI scoreText;

    private void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(gameObject);
    }
}

    private void Start()
    {
        score = PlayerPrefs.GetInt("AccumulatedScore", 0);
        UpdateUI();
        totalBricks = FindObjectsByType<Brick2>(FindObjectsSortMode.None).Length;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;

        if (lives < 0)
            lives = 0;

        UpdateUI();

        if (lives == 0)
        {
            PlayerPrefs.SetInt("LastScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt("FinalScore", score);
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");

        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (hearts != null)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].enabled = i < lives;
            }
        }
    }

public void BrickDestroyed()
{
    totalBricks--;

    if (totalBricks <= 0)
    {
        PlayerPrefs.SetInt("FinalScore", score);
        SceneManager.LoadScene("Win");
    }
}
void LoadNextLevel()
{
    int currentIndex = SceneManager.GetActiveScene().buildIndex;

    if (currentIndex == 3) // se estiver na cena_2
    {
        PlayerPrefs.SetInt("FinalScore", score);
        SceneManager.LoadScene("Win");
    }
    else
    {
        SceneManager.LoadScene(currentIndex + 1);
    }
}

}
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        UpdateUI();
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
            Debug.Log("GAME OVER");
            // depois vamos trocar para carregar cena de GameOver
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
}
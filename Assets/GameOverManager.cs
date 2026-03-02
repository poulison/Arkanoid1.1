using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Chamado pelo botão YES
    public void PlayAgain()
    {
        int lastSceneIndex = PlayerPrefs.GetInt("LastScene", 1);
        SceneManager.LoadScene(lastSceneIndex);
    }

    // Chamado pelo botão NO
    public void QuitGame()
    {
        Application.Quit();

        // Isso aparece só no Editor (para testar)
        Debug.Log("Saiu do jogo");
    }
}
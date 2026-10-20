using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public void LoadEasy()
    {
        GameOverManager.LastGameScene = "GameEasy";
        SceneManager.LoadScene("GameEasy");
    }

    public void LoadMedium()
    {
        GameOverManager.LastGameScene = "GameMedium";
        SceneManager.LoadScene("GameMedium");
    }

    public void LoadHard()
    {
        GameOverManager.LastGameScene = "GameHard";
        SceneManager.LoadScene("GameHard");
    }
}

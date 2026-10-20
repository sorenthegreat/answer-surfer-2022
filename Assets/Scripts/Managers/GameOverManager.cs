using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private AudioClip gameOverSound;

    private AudioSource audioSource;

    public static string LastGameScene { get; set; } = "GameEasy";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        if (finalScoreText != null)
            finalScoreText.text = $"Final Score : {ScoreManager.FinalScore}";

        if (gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);
    }

    public void Retry()
    {
        SceneManager.LoadScene(LastGameScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Playing,
        Paused,
        Quiz,
        GameOver
    }

    public GameState CurrentState { get; private set; } = GameState.Playing;

    [Header("Settings")]
    [SerializeField] private string sceneToReload = "Game Over";

    [Header("Player Walking")]
    [SerializeField] private AudioClip playerWalkingSound;

    private AudioSource walkingSource;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        GameOverManager.LastGameScene = SceneManager.GetActiveScene().name;

        DynamicGI.UpdateEnvironment();

        walkingSource = GetComponent<AudioSource>();
        if (walkingSource == null)
            walkingSource = gameObject.AddComponent<AudioSource>();

        walkingSource.loop = true;
        walkingSource.clip = playerWalkingSound;
        if (playerWalkingSound != null)
            walkingSource.Play();
    }

    public bool IsPlaying()
    {
        return CurrentState == GameState.Playing;
    }

    public bool IsQuizActive()
    {
        return CurrentState == GameState.Quiz;
    }

    public void StartQuiz()
    {
        CurrentState = GameState.Quiz;
        Time.timeScale = 0f;
    }

    public void EndQuiz()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneToReload);
    }
}
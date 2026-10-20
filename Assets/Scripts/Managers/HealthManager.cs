using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int maxHearts = 3;

    [Header("UI")]
    [SerializeField] private Image[] heartImages;

    [Header("SFX")]
    [SerializeField] private AudioClip hitSound;

    private int currentHearts;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        currentHearts = maxHearts;
        UpdateUI();
    }

    public void LoseHeart()
    {
        currentHearts--;
        UpdateUI();

        if (hitSound != null)
            audioSource.PlayOneShot(hitSound);

        if (currentHearts <= 0)
        {
            ScoreManager.FinalScore = ScoreManager.Instance != null ? ScoreManager.Instance.Distance : 0;
            SceneManager.LoadScene("Game Over");
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            Color c = heartImages[i].color;
            c.a = i < currentHearts ? 1f : 0.25f;
            heartImages[i].color = c;
        }
    }
}

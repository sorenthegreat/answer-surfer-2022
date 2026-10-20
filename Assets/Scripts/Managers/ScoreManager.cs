using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public static int FinalScore { get; set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI distanceText;

    private int distance = 0;

    public int Distance => distance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddPoint()
    {
        distance++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (distanceText != null)
        {
            distanceText.text = $"Distance: {distance}";
        }
    }
}
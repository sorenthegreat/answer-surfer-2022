using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI option1Text;
    [SerializeField] private TextMeshProUGUI option2Text;
    [SerializeField] private TextMeshProUGUI option3Text;

    [Header("SFX")]
    [SerializeField] private AudioClip correctSound;

    private int correctAnswer;
    private int correctOption;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        GenerateQuestion();
    }

    public bool IsCorrect(int optionNumber)
    {
        return optionNumber == correctOption;
    }

    public void PlayCorrectSound()
    {
        if (correctSound != null)
            audioSource.PlayOneShot(correctSound);
    }

    private void GenerateQuestion()
    {
        int a = Random.Range(1, 101);
        int b = Random.Range(1, 101);
        correctAnswer = a + b;

        questionText.text = $"{a} + {b} = ?";

        correctOption = Random.Range(1, 4);

        int wrong1 = correctAnswer + Random.Range(1, 6);
        int wrong2 = correctAnswer - Random.Range(1, 6);
        if (wrong2 < 0) wrong2 = correctAnswer + Random.Range(6, 11);

        if (correctOption == 1)
        {
            option1Text.text = correctAnswer.ToString();
            option2Text.text = wrong1.ToString();
            option3Text.text = wrong2.ToString();
        }
        else if (correctOption == 2)
        {
            option1Text.text = wrong1.ToString();
            option2Text.text = correctAnswer.ToString();
            option3Text.text = wrong2.ToString();
        }
        else
        {
            option1Text.text = wrong1.ToString();
            option2Text.text = wrong2.ToString();
            option3Text.text = correctAnswer.ToString();
        }
    }
}

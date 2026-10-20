using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private int optionNumber;

    private QuestionManager qm;
    private QuestionManagerMedium qmMedium;

    private void Awake()
    {
        qm = GetComponentInParent<QuestionManager>();
        if (qm == null)
            qmMedium = GetComponentInParent<QuestionManagerMedium>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (qm == null && qmMedium == null)
            return;

        bool correct = (qm != null) ? qm.IsCorrect(optionNumber) : qmMedium.IsCorrect(optionNumber);

        if (correct)
        {
            if (qm != null)
                qm.PlayCorrectSound();
            else
                qmMedium.PlayCorrectSound();
        }
        else
        {
            Debug.Log("Wrong!");
            HealthManager.Instance.LoseHeart();

            PlayerBlink blink = other.GetComponent<PlayerBlink>();
            if (blink != null)
                blink.StartBlink();
        }
    }
}

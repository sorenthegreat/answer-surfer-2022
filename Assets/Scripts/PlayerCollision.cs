using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerBlink playerBlink;

    private void Awake()
    {
        playerBlink = GetComponent<PlayerBlink>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            HealthManager.Instance.LoseHeart();
            if (playerBlink != null)
                playerBlink.StartBlink();
        }
    }
}
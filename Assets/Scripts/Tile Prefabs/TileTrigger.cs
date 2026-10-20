using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    private Tile tile;

    private void Awake()
    {
        tile = GetComponentInParent<Tile>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        tile.PlayerEntered();
    }
}
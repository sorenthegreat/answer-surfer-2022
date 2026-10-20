using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool hasBeenScored;

    public void ResetTile()
    {
        hasBeenScored = false;
    }

    public void PlayerEntered()
    {
        if (hasBeenScored)
            return;

        hasBeenScored = true;

        ScoreManager.Instance.AddPoint();
    }
}
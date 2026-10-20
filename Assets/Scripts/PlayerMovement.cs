using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Lane References")]
    [SerializeField] private Transform leftLane;
    [SerializeField] private Transform centerLane;
    [SerializeField] private Transform rightLane;

    [Header("Movement")]
    [SerializeField] private float laneChangeSpeed = 100f;

    // 0 = Left, 1 = Center, 2 = Right
    private int currentLane = 1;
    private Transform targetLane;

    private void Start()
    {
        targetLane = centerLane;
        transform.position = centerLane.position;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying())
            return;

        HandleInput();
        MovePlayer();
    }

    private void HandleInput()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame ||
            Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            currentLane--;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame ||
            Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            currentLane++;
        }

        currentLane = Mathf.Clamp(currentLane, 0, 2);

        switch (currentLane)
        {
            case 0:
                targetLane = leftLane;
                break;

            case 1:
                targetLane = centerLane;
                break;

            case 2:
                targetLane = rightLane;
                break;
        }
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetLane.position,
            laneChangeSpeed * Time.deltaTime
        );
    }

    public int GetCurrentLane()
    {
        return currentLane;
    }

    public void SetLane(int lane)
    {
        currentLane = Mathf.Clamp(lane, 0, 2);

        switch (currentLane)
        {
            case 0:
                targetLane = leftLane;
                break;

            case 1:
                targetLane = centerLane;
                break;

            case 2:
                targetLane = rightLane;
                break;
        }
    }
}
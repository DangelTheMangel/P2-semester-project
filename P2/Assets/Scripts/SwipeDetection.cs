using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    /// <summary>
    /// Subscribes OnStartTouch when the script is enabled
    /// </summary>
    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }
    private void SwipeStart(Vector2 position, float time)
    {

    }

    private void SwipeEnd(Vector2 position, float time)
    {
        DetectSwipe();
    }
}

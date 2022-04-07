using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;

    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

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
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }
    /// <summary>
    /// Measures distance between the ending and start position and checks of the distance is large enough to be a swipe.
    /// Similar with time, if the swipe is too long, it's probably not a swipe.
    /// </summary>
    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
            (endTime - startTime) <= maximumTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
        }
    }
}

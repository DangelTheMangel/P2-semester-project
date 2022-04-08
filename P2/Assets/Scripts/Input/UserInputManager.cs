using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    [SerializeField]
    private float minumumDistance = .2f;
    [SerializeField]
    private float maximumDistance = 1;
    [SerializeField, Range(0, 1)]
    private float dirThreshold = .9f;

    private InputManage inputManager;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    private void Awake()
    {
        inputManager = InputManage.instance;
    }

    private void OnEnable()
    {
        inputManager.onStartTouch += swipeStart;
        inputManager.onEndTouch += swipeEnd;
    }

    private void OnDisable()
    {
        inputManager.onStartTouch -= swipeStart;
        inputManager.onEndTouch -= swipeEnd;
    }

    private void swipeStart(Vector2 pos, float time)
    {
        startPosition = pos;
        startTime = time;
    }

    private void swipeEnd(Vector2 pos, float time)
    {
        endPosition = pos;
        endTime = time;
        detectSwipe();
    }

    private void detectSwipe()
    {
        Debug.Log("detectSwipe" + (Vector3.Distance(startPosition, endPosition) >= minumumDistance &&
            (endTime - startTime) <= maximumDistance));
        if (Vector3.Distance(startPosition, endPosition) >= minumumDistance &&
            (endTime - startTime) <= maximumDistance)
        {
            Debug.DrawLine(startPosition, endPosition, Color.blue, 5f);

            Vector3 dir = endPosition - startPosition;
            Vector2 dir2 = new Vector2(dir.x, dir.y).normalized;
            Debug.Log("dir:" + dir + "dir2" + dir2 + "s" + startPosition + "e" + endPosition);
            swipeDirection(dir2);
        }

    }

    private void swipeDirection(Vector2 direction)
    {

        Debug.Log(
            "input" +
            direction +
            "if" +
            "^ " + ((Vector2.Dot(Vector2.up, direction) > dirThreshold)) +
            " / " + ((Vector2.Dot(Vector2.down, direction) > dirThreshold)) +
            " < " + ((Vector2.Dot(Vector2.left, direction) > dirThreshold)) +
            " > " + ((Vector2.Dot(Vector2.right, direction) > dirThreshold))
            + "number" +
            "^ " + Vector2.Dot(Vector2.up, direction) +
            " / " + Vector2.Dot(Vector2.down, direction) +
            " < " + Vector2.Dot(Vector2.left, direction) +
            " > " + Vector2.Dot(Vector2.right, direction)
            );
        if (Vector2.Dot(Vector2.up, direction) > dirThreshold)
        {
            Debug.Log("swipeUP");
        }
        else if (Vector2.Dot(Vector2.down, direction) > dirThreshold)
        {
            Debug.Log("swipeDown");
        }
        else if (Vector2.Dot(Vector2.left, direction) > dirThreshold)
        {
            Debug.Log("swipeLeft");
        }
        else if (Vector2.Dot(Vector2.right, direction) > dirThreshold)
        {
            Debug.Log("swipeRight");
        }
    }
}

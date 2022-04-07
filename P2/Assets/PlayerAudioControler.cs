using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControler : MonoBehaviour
{
    [SerializeField] GameObject soundRight, soundLeft;
    [SerializeField] bool rightEar, leftEar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         PlayerMovement player = GameManganer.Instance.player;
         Vector2 rightVector = (Vector2) player.roatationToMovementVector(0);
         spaceCheck(wallCheck(rightVector), soundRight);
         spaceCheck(leftEar, soundLeft);

    }

    void spaceCheck(bool wallCheck, GameObject ear)
    {
        if (wallCheck)
            ear.SetActive(true);
        else
            ear.SetActive(false);
    }

    bool wallCheck(Vector2 dire)
    {
        Debug.DrawRay(transform.position, dire, Color.red);
        return false;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControler : MonoBehaviour
{
    [SerializeField] GameObject soundRight, soundLeft;
    [SerializeField] bool rightEar, leftEar;
    float collisionCheckerSize = .2f;

    PlayerMovement player;
    private void Awake()
    {
        player = gameObject.GetComponent<PlayerMovement>();
    }

    public void MovementCheck()
    {
         spaceCheck(wallCheck(soundRight), soundRight);
         spaceCheck(wallCheck(soundLeft), soundLeft);

    }

    void spaceCheck(bool wallCheck, GameObject ear)
    {
        if (wallCheck)
            ear.SetActive(true);
        else
            ear.SetActive(false);
    }

    bool wallCheck(GameObject ball)
    {
        
        return (!Physics2D.OverlapCircle(ball.transform.position, collisionCheckerSize, player.whatStopsMovement));

    }

    void playerSFX(string audioClip)
    {
        SoundManager.instance.playEffect(gameObject, audioClip);
    }
}

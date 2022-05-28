using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventActivator : MonoBehaviour
{
    [SerializeField]
    GameObject Prefab;

    [SerializeField]
    string soundEffectName = ""; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the collide object are the player
        if (collision.tag == "Player") //her er de seje ting der sker når den aktiveres
        {
            //create the prefab
            GameObject obj =
            Instantiate(Prefab, this.transform.position, this.transform.rotation);
            //set the prefab parrent to be this gameobjct
            obj.transform.parent = this.gameObject.transform;
            //if there are a sound effect it play it
            if (soundEffectName != "") {
                SoundManager.instance.playEffect(gameObject, soundEffectName);
            }
        }

    }
}
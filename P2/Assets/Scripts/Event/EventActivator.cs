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
        if (collision.tag == "Player") //her er de seje ting der sker når den aktiveres
        {
            GameObject obj =
            Instantiate(Prefab, this.transform.position, this.transform.rotation);

            obj.transform.parent = this.gameObject.transform;
            if (soundEffectName != "") {
                SoundManager.instance.playEffect(gameObject, soundEffectName);
            }
        }

    }
}
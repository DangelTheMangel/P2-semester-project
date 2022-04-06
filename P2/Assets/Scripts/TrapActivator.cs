using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    [SerializeField]
    GameObject Prefab;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //her er de seje ting der sker når den aktiveres
        {
            GameObject obj =
            Instantiate(Prefab, this.transform.position, this.transform.rotation);

            obj.transform.parent = this.gameObject.transform;
        }

    }
}
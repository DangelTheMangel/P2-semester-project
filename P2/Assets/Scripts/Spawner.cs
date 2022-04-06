using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //prefab can be inserted here
    public GameObject spawnedObject;

    //timer starts at this value
    public float countAtStart = 0;

    //object is spawned when timer reaches this value
    public float fireOnThisAmount;

    //timer increases this much each second
    public float increasePerSecond;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer is controlled here
        countAtStart = countAtStart + increasePerSecond * Time.deltaTime;
        //spawns when th right ammount is reached
        if (countAtStart >= fireOnThisAmount)
        {
            //instantiates the prefab and resets the timer to 0
            Instantiate(spawnedObject, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            countAtStart = 0;
        }
    }
}

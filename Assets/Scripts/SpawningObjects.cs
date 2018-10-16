using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class SpawningObjects : MonoBehaviour {

    [SerializeField]
    List<GameObject> SceneItems;

    [SerializeField]
    int NumOfItemsToSpawn = 100;

    private BoxCollider SpawnArea;

	// Use this for initialization
	void Start ()
    {
        //SceneItems = new List<GameObject>(); //List of Prefabs used to generate objects in the scene
        SpawnRandomObjects();

        SpawnArea = GetComponent<BoxCollider>();
    }

    private int MaxRange = 10000; //10 thousand
    private void SpawnRandomObjects()
    {
        GameObject spawnedItem = null;
        Vector3 location = new Vector3();

        for (int i = 0; i < NumOfItemsToSpawn; i++)
        {
            bool isSpawned = false;
            while (!isSpawned)
            {
                float rand = Random.Range(0, MaxRange); //random number between 0 and the MaxRange
                int ratio = MaxRange / 3; //Figuring out the Ratio to be checked against 33% for this one

                if (rand < ratio)
                {
                    spawnedItem = SceneItems[0];
                    isSpawned = true;
                }
                else if (rand < ratio * 2)
                {
                    spawnedItem = SceneItems[1];
                    isSpawned = true;
                }
                else
                {
                    spawnedItem = SceneItems[2];
                    isSpawned = true;
                }

                location = SwapXYValuesFor3D();

                Debug.Log(location);
                Instantiate(spawnedItem, location, Quaternion.identity, this.transform);
            }
        }
    }

    private Vector3 SwapXYValuesFor3D()
    {   
                                                      // since the UnitCircle is a Radius of 1 need to halve the bounds
        Vector3 location = Random.insideUnitCircle * (SpawnArea.bounds.max/2);
        location.z = location.y;
        location.y = 0;
        return location;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

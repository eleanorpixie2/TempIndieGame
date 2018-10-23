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

    public List<GameObject> Statues = new List<GameObject>();

    public List<GameObject> Players = new List<GameObject>();

    private BoxCollider SpawnArea;

    private Material HiderMaterial;

	// Use this for initialization
	void Start ()
    {
        //Retreiving a Reference of the BoxCollider for its bounds
        SpawnArea = GetComponent<BoxCollider>();

        //finds a Material to Tag the Players for debugging
        DebugPlayers();

        //Does the Actual Spawning of the Objects in SceneItems
        SpawnRandomObjects();

        //Places the Players Randomly in the scene
        SpawnHiders();
    }

    private void DebugPlayers()
    {
        foreach (var mat in FindObjectsOfType<Material>())
        {
            if (mat.name == "HiderMat")
            {
                HiderMaterial = mat;
                break;
            }
        }
    }

    private void SpawnHiders()
    {
        //for i number of players
        for (int i = 0; i < InputManager.maxPlayers; i++)
        {
            int numOfPlayers = Random.Range(1, Statues.Count - 1);
            //Statues[numOfPlayers].gameObject
            Statues[numOfPlayers].gameObject.tag = string.Format("Hider{0}", i);
            Statues[numOfPlayers].gameObject.GetComponent<Renderer>().material.color = Color.green;

        }
    }


    //used to keep a reference of how large of a range the Random will be
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
                int ratio = MaxRange / SceneItems.Count; //Figuring out the Ratio to be checked against 33% for this one

                isSpawned = GetRandomObject(out spawnedItem, rand, ratio);

                //Stores value of spawnedItem's location
                location = SwapXYValuesFor3D();

                //checks if spawned was true before Creating object in the scene
                if (isSpawned)
                {
                    if (spawnedItem.tag == "HiderPlaceholder")
                    {
                        Statues.Add(Instantiate(spawnedItem, location, Quaternion.identity, this.transform));
                    }
                    else
                    {
                        Instantiate(spawnedItem, location, Quaternion.identity, this.transform);
                    }

                }
            }
        }
    }

    private bool GetRandomObject(out GameObject spawnedItem, float rand, int ratio)
    {
        if (rand < ratio)
        {
            spawnedItem = SceneItems[0];
            return true;
        }
        else if (rand < ratio * 2)
        {
            spawnedItem = SceneItems[1];
            return true;
        }
        else if (rand < ratio * 3)
        {
            spawnedItem = SceneItems[2];
            return true;
        }
        else if (rand < ratio * 4)
        {
            spawnedItem = SceneItems[3];
            return true;
        }
        else if (rand < ratio * 5)
        {
            spawnedItem = SceneItems[4];
            return true;
        }
        else if (rand < ratio * 6)
        {
            spawnedItem = SceneItems[5];
            return true;
        }

        spawnedItem = null;
        return false;

    }

    private Vector3 SwapXYValuesFor3D()
    {
        float size;

        if (SpawnArea.size.x <= SpawnArea.size.z)
        {
            size = SpawnArea.size.x;
        }
        else
        {
            size = SpawnArea.size.z;
        }

        //since the UnitCircle is a Radius of 1 need to halve the bounds
        Vector3 location = Random.insideUnitCircle * (size / 2);
        
        //swap z and y points because spawning on a flat plane
        location.z = location.y;
        location.y = 0.5f;

        return location;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

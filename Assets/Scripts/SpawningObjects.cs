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

    private BoxCollider SpawnArea;

    private Material HiderMaterial;

	// Use this for initialization
	void Start ()
    {
        //Retreiving a Reference of the BoxCollider for its bounds
        SpawnArea = GetComponent<BoxCollider>();


        //Does the Actual Spawning of the Objects in SceneItems
        SpawnRandomObjects();

        //Places the Players Randomly in the scene
        SpawnHiders();

        //finds a Material to Tag the Players for debugging
        //DebugPlayers();
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
        for (int i = 0; i < 3; i++)
        {
            List<int> lastPlayer = new List<int>();
            int numOfPlayers = -1;
            lastPlayer.Add(numOfPlayers);

            //this check insures that the same player isn't chosen again

            while (lastPlayer.Contains(numOfPlayers))
            {
                numOfPlayers = Random.Range(1, Statues.Count - 1);
            }
            lastPlayer.Add(numOfPlayers);

            //changes the gameObjects' tag to Hider
            Statues[numOfPlayers].gameObject.tag = string.Format("Hider{0}", i);
            Statues[numOfPlayers].AddComponent<HiderCameraSpawn>();

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

                    //adds "Statues" to the running Statues list if it has the correct tag
                    if (spawnedItem.tag == "HiderPlaceholder")
                    {
                        Statues.Add(spawnedItem = Instantiate(spawnedItem, location, Quaternion.Euler(0, Random.Range(-180, 180), 0), this.transform));
                    }
                    else //otherwise just spawn the item
                    {
                        spawnedItem = Instantiate(spawnedItem, location, Quaternion.Euler(0, Random.Range(-180, 180), 0), this.transform);
                    }

                    //fixing the objects position so that its center is relative to its parent's world location
                    spawnedItem.transform.position = spawnedItem.transform.localPosition + (this.transform.position * 2);
                }
            }
        }
    }

    //This will take the list of Scene Items and choose one to spawn based on how many there are
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
        else
        {
            spawnedItem = SceneItems[6];
            return true;
        }

    }

    //using the BoxCollider SpawnArea as a trigger collider it will choose a spot within it
    //to set the spawn location
    private Vector3 SwapXYValuesFor3D()
    {
        float size;

        //checks which side is larger to use the smaller area so that the objects won't be spawned outside of it
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
}

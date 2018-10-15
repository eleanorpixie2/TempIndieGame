using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawningObjects : MonoBehaviour {

    [SerializeField]
    List<GameObject> SceneItems;

    [SerializeField]
    List<Vector2> AreaVectors;

	// Use this for initialization
	void Start ()
    {
        AreaVectors = new List<Vector2>(); //used as the 4 end points of the locations starting from top left clockwise to top right > bottom right > bottom left
        SceneItems = new List<GameObject>(); //List of Prefabs used to generate objects in the scene
        SpawnRandomObjects();
    }

    private void SpawnRandomObjects()
    {
        for (int i = 0; i <100; i++)
        {
            bool isSpawned = false;
            while (!isSpawned)
            {
                float rand = Random.Range(0, 10000);

                if (rand < 10000/4)
                {
                    isSpawned = true;
                }
                else if(rand < 10000 / 2)
                {
                    isSpawned = true;

                }
                else
                {
                    isSpawned = true;

                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

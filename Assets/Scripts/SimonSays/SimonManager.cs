using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour {

    private SimonImageDisplay simonImageDisplay;
    private SimonSaysRandomizer simonSaysRandomizer;
    //[SerializeField] private GameTimer gameTimer;
    //[SerializeField] private InputManager inputManager;

    private List<int> sequenceList;

    private int maxiumSequenceLength = 5;// To increase while game is running
    private int next = 0;

    private float timeStamp;
    // Use this for initialization


    private void Awake()
    {
        timeStamp = Mathf.RoundToInt(Time.fixedTime);
    }
    void Start ()
    {
        simonImageDisplay = GetComponent<SimonImageDisplay>();
        simonSaysRandomizer = GetComponent<SimonSaysRandomizer>();

        sequenceList = simonSaysRandomizer.RandomizeSequence(maxiumSequenceLength);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Mathf.RoundToInt(Time.fixedTime) == timeStamp + 5)
        {
            Debug.Log(sequenceList[next]);

            timeStamp = Mathf.RoundToInt(Time.fixedTime);

            simonImageDisplay.DisplayImageSequence(sequenceList[next]);

            

            next++;

            if (!(next < maxiumSequenceLength)) // Once int next reaches max - reset it back to 0
            {
                next = 0;
            }
        }
	}
}

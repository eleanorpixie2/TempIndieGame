using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour {

    private SimonImageDisplay simonImageDisplay;
    private SimonSaysRandomizer simonSaysRandomizer;
    //[SerializeField] private GameTimer gameTimer;
    private InputManager inputManager;

    private List<int> sequenceList;

    private int maxiumSequenceLength = 5;// To increase while game is running
    private int next;

    private float timeStamp;

    private bool isDisplayingSequence;
    private bool awaitingPlayerInput;

    // Use this for initialization
    private void Awake()
    {
        simonImageDisplay = GetComponent<SimonImageDisplay>();
        simonSaysRandomizer = GetComponent<SimonSaysRandomizer>();
        inputManager = GetComponent<InputManager>();

        timeStamp = Mathf.RoundToInt(Time.fixedTime);
    }
    void Start ()
    {
        next = 0;

        isDisplayingSequence = true;
        awaitingPlayerInput = false;

        sequenceList = simonSaysRandomizer.RandomizeSequence(maxiumSequenceLength);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isDisplayingSequence)
            DisplaySequence();
        else if (awaitingPlayerInput)
            HandleAndCompareInput();
	}

    private void DisplaySequence()
    {
        if (Mathf.RoundToInt(Time.fixedTime) == timeStamp + 5)
        {
            timeStamp = Mathf.RoundToInt(Time.fixedTime);

            simonImageDisplay.DisplayImageSequence(sequenceList[next]);

            next++;

            if (!(next < maxiumSequenceLength)) // Once int next reaches max 
            {
                next = 0; //- reset it back to 0
                isDisplayingSequence = false;
                awaitingPlayerInput = true;
            }
        }
    }

    private void HandleAndCompareInput()
    {
        bool answeredCorrectly = false;

        if (inputManager.IsHiderInputing())
        {
            int buttonIndex = inputManager.GetButtonIndex;

            simonImageDisplay.DisplayImageSequence(buttonIndex);

            if (sequenceList[next] == buttonIndex)
            {
                answeredCorrectly = true;
                Debug.Log("Correct");
            }
            else
            {
                answeredCorrectly = false;
                Debug.Log("Wrong");
            }

            next++;

            if (!(next < maxiumSequenceLength) || answeredCorrectly == false) // Once int next reaches max or player answered wrong
            {
                sequenceList = simonSaysRandomizer.RandomizeSequence(maxiumSequenceLength);
                next = 0; //- reset it back to 0
                isDisplayingSequence = true;
                awaitingPlayerInput = false;
            }
        }
    }
}

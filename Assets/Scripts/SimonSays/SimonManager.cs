using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour {

    [SerializeField] int secondsToWait;

    [Header("Audio")]
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip wrongSound;

    

    private SimonImageDisplay simonImageDisplay;
    private SimonSaysRandomizer simonSaysRandomizer;
    //[SerializeField] private GameTimer gameTimer;
    private InputManager inputManager;

    private AudioSource audio;

    private List<int> sequenceList;

    private int maxiumSequenceLength = 3;// To increase while game is running
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

        audio = GetComponent<AudioSource>();

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


    //Main Methods
    private void DisplaySequence()
    {
        
        if (Mathf.RoundToInt(Time.fixedTime) == timeStamp + secondsToWait)
         {
            Debug.Log("Next: " + next);
            simonImageDisplay.DisplayImageSequence(sequenceList[next]);
            Debug.Log(sequenceList[next]);

            StartCoroutine(TimedSequence());

            next++;

            if (!(next < maxiumSequenceLength)) // Once int next reaches max 
            {
                next = 0; //- reset it back to 0
                isDisplayingSequence = false;
                awaitingPlayerInput = true;
            }

            timeStamp = Mathf.RoundToInt(Time.fixedTime);
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

                audio.clip = correctSound;
                audio.PlayDelayed(0);

                Debug.Log("Correct");
            }
            else
            {
                answeredCorrectly = false;

                audio.clip = wrongSound;
                audio.PlayDelayed(0);

                Debug.Log("Wrong");
            }

            next++;

            if (!(next < maxiumSequenceLength) || answeredCorrectly == false) // Once int next reaches max or player answered wrong
            {
                sequenceList = simonSaysRandomizer.RandomizeSequence(maxiumSequenceLength);
                next = 0; //- reset it back to 0
                isDisplayingSequence = true;
                awaitingPlayerInput = false;

                StartCoroutine(TimedSequence());

                timeStamp = Mathf.RoundToInt(Time.fixedTime) + 2;
            }
        }
    }

    IEnumerator TimedSequence()
    {
        yield return new WaitForSeconds(1);

        simonImageDisplay.Clear();
    }

    //**
}

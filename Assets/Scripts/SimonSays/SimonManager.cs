using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour {

    [SerializeField] int secondsToWait; //Seconds to wait in between dislay of buttons

    //Audio for when player has correctly made an input or not
    [Header("Audio")]
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip wrongSound;

    private SimonImageDisplay simonImageDisplay; //Component to display the buttons on screen
    private SimonSaysRandomizer simonSaysRandomizer; //Component to get a randomized sequence
    //[SerializeField] private GameTimer gameTimer;
    private InputManager inputManager; //Component to get the player input

    private AudioSource audio; //To get the audio component

    private List<int> sequenceList; //The sequence list to get from the simonRandomizer

    private int maxiumSequenceLength = 3;// To increase while game is running
    private int next; //Token to get to the next button on the list

    private float timeStamp; //To get the current time

    private bool isDisplayingSequence; //If currently displaying the button sequence
    private bool awaitingPlayerInput; //If waiting for player input

    // Use this for initialization
    private void Awake()
    {
        //Get Components
        simonImageDisplay = GetComponent<SimonImageDisplay>();
        simonSaysRandomizer = GetComponent<SimonSaysRandomizer>();
        inputManager = GetComponent<InputManager>();

        audio = GetComponent<AudioSource>();

        timeStamp = Mathf.RoundToInt(Time.fixedTime);//Get the current time at the beginning of the game
    }
    void Start ()
    {
        next = 0; //Next should start at 0

        isDisplayingSequence = true;//Sequence should be displayed first
        awaitingPlayerInput = false;//Then player input after sequence

        sequenceList = simonSaysRandomizer.RandomizeSequence(maxiumSequenceLength); //Get a randomized sequence list at the start and how long the sequence is
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
        
        if (Mathf.RoundToInt(Time.fixedTime) == timeStamp + secondsToWait) //waits every so seconds until displaying the next button
         {
            simonImageDisplay.DisplayImageSequence(sequenceList[next]); //Display the button on the sequence list

            StartCoroutine(TimedSequence()); //Wait a bit until clearing image

            next++; //add 1 to get to the next index in the sequence

            if (!(next < maxiumSequenceLength)) // Once int next reaches max 
            {
                next = 0; //- reset it back to 0
                isDisplayingSequence = false;//No longer displaying sequence
                awaitingPlayerInput = true;//Start getting player input
            }

            timeStamp = Mathf.RoundToInt(Time.fixedTime); //Get the current time to update the time to wait until displaying the next image
        }
    }

    private void HandleAndCompareInput()
    {
        bool answeredCorrectly = false;

        if (inputManager.IsHiderInputing()) //Is the player pressing any of the wanted buttons
        {
            int buttonIndex = inputManager.GetButtonIndex; //Get the button index that was pressed

            simonImageDisplay.DisplayImageSequence(buttonIndex); //Display the button pressed

            if (sequenceList[next] == buttonIndex) //If the button (index) pressed matches the button index on the sequence list
            {
                answeredCorrectly = true;

                audio.clip = correctSound; //add the the sound to the audion clip
                audio.PlayDelayed(0);//Play the sound
            }
            else
            {
                answeredCorrectly = false;

                audio.clip = wrongSound;//add the the sound to the audion clip
                audio.PlayDelayed(0);//Play the sound
            }

            next++; //add 1 to get to the next index in the sequence

            if (!(next < maxiumSequenceLength) || answeredCorrectly == false) // Once int next reaches max or player answered wrong
            {
                sequenceList = simonSaysRandomizer.RandomizeSequence(maxiumSequenceLength); //Get a new randomized sequence
                next = 0; //- reset it back to 0
                isDisplayingSequence = true; //display the new sequence
                awaitingPlayerInput = false;//stop getting player input

                StartCoroutine(TimedSequence());//wait a bit to clear image

                timeStamp = Mathf.RoundToInt(Time.fixedTime) + 2; //Get the current time plus a bit of a delay(2)
            }
        }
    }


    IEnumerator TimedSequence()
    {
        yield return new WaitForSeconds(1); //Wait x seconds until clearing image/ Seconds must be less than secondsToWait

        simonImageDisplay.Clear(); //Clear the image
    }

}

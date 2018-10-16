using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour {

    [SerializeField] private SimonImageDisplay simonImageDisplay;
    [SerializeField] private SimonSaysRandomizer simonSaysRandomizer;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private InputManager inputManager;


    Queue<Colors> colorList;

    // Use this for initialization
    void Start ()
    {
        colorList = simonSaysRandomizer.playBackColors;

        simonImageDisplay.DisplayImageSequence(colorList);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

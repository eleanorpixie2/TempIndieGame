using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this will be used to determine the different buttons that might need to be pressed for the 
//Simon Says part of the game
public enum Colors { RED, GREEN, BLUE, YELLOW }


public class SimonSaysRandomizer : MonoBehaviour
{

    //holding all the Color States as they are generated
    //[SerializeField]
    public Queue<Colors> playBackColors;

    //check if playBackColors has been filled
    [SerializeField]
    bool isFilled;

    //how large of a stack should the Randomizer should make
    [SerializeField]
    int colorsLimit = 1000;

    // Use this for initialization
    void Start ()
    {
        playBackColors = new Queue<Colors>();
	}

    //Ranges min and max for use with the Random function call
    private float maxRange = 1000000;
    private float minRange = 0;

    //used to section off Range by 1/4's
    private float quarterRange;

    private float randOutput;

    // Update is called once per frame
    void Update ()
    {
        for (int i = 0; i < colorsLimit/5; i++)
        {
            FillStack();
        }

    }

    /// <summary>
    /// Checks if playBackColors isFilled, and stores random float in randOutput
    /// </summary>
    private void FillStack()
    {
        if (!isFilled)
        {
            //getting 1/4 of the maxRange
            quarterRange = maxRange / 4;

            //storing a Random number between minRange and maxRange
            randOutput = Random.Range(minRange, maxRange);

            CheckRandomOutput();
        }
        else
        {
            //ClearStack();

        }
    }

    private void CheckRandomOutput()
    {
        if (randOutput <= quarterRange)
        {
            AddToStack(Colors.RED);
        }
        else if (randOutput <= (quarterRange * 2))
        {
            AddToStack(Colors.GREEN);
        }
        else if (randOutput <= (quarterRange * 3))
        {
            AddToStack(Colors.BLUE);
        }
        else if (randOutput < maxRange)
        {
            AddToStack(Colors.YELLOW);
        }
    }

    private void AddToStack(Colors current)
    {
        Debug.Log(playBackColors.Count);
        if (playBackColors.Count < colorsLimit)
        {
            playBackColors.Enqueue(current);
        }
        else
        {
            isFilled = true;
        }

    }

    private void ClearStack()
    {
        foreach (var color in playBackColors)
        {
            Debug.Log(color);
        }
        playBackColors.Clear();
        isFilled = false;
        Debug.Log("Done");
    }

    private void DebugCall(Colors current)
    {
        Debug.Log(current);
    }
}

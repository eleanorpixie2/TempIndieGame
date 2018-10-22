using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this will be used to determine the different buttons that might need to be pressed for the 
//Simon Says part of the game
public enum Colors { RED, GREEN, BLUE, YELLOW }


public class SimonSaysRandomizer : MonoBehaviour
{
    private List<int> ButtonSequence = new List<int>();
   
    private int maxButtonRange = 4; // 4 is exclusive
    private int minButtonRange = 0; // 0 is inclusive

    public List<int> RandomizeSequence(int maxSequence)
    {
        ButtonSequence.Clear();

        for (int i = 0; i < maxSequence; i++) //Fill the list until the max is reached.
        {
            ButtonSequence.Add(Random.Range(minButtonRange, maxButtonRange)); //Add a random number into the list. Range is 0 => int < 4
        }

        return ButtonSequence;


    }

}

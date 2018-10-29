using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Buttons are mapped in unity with an integer 
 * A = 0
 * B = 1
 * X = 2
 * Y = 3
 * 
 * Makes a list if integers clamped between the digits shown above
 * Will be used to compare with the input the player makes which will also be in ints
 * rather than comparing a list of enums and having unneccesary variables of bools
 */

public class SimonSaysRandomizer : MonoBehaviour
{
    private List<int> ButtonSequence = new List<int>(); //Create a new list
   
    private int maxButtonRange = 4; // 4 is exclusive
    private int minButtonRange = 0; // 0 is inclusive

    public List<int> RandomizeSequence(int maxSequence) //Parameter is how long the sequence will be
    {
        ButtonSequence.Clear(); //Clear the list in case it is already filled

        for (int i = 0; i < maxSequence; i++) //Fill the list until the max is reached.
        {
            int rand = Random.Range(0, 10000);
            if (rand < 2500)
            {
                rand = 0;
            }
            else if (rand < 5000)
            {
                rand = 1;
            }
            else if(rand < 7500)
            {
                rand = 2;
            }
            else
            {
                rand = 3;
            }

            ButtonSequence.Add(rand); //Add a random number into the list. Range is 0 => int < 4
        }

        return ButtonSequence; //Return the newly created sequence
    }

}

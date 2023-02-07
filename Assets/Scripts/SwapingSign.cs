using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapingSign : MonoBehaviour
{
    public int theNumberImEffecting;
   [HideInInspector] public bool amIPressed;

    
    /*
     * hey Tamir,
     * for this script to work you need to do a few things:
     * 1. make the class inhreret from "MonoBehaviour" instead of "Interactable"
     * 2. delete the words "override" and "QuarterScreen quarterScreen"
     * 3. you need to create a script that uses raycast from the camera and if it hits something with the component SwapingSign,
     * the script will activate the Interact() function of the SwapingSign
     * 4. the script works when the name of the sign is equal to the corresponding correct number in the right code
     * (see how the object i send you is ordered)
     * 
     * 
     * p.s i think the function CheckIfPressed() is never used
     * 
     */
    public  void Interact()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        if (SwapSignSystem.instance.theFirstPressed == -1)
        {
            SwapSignSystem.instance.FirstPress(theNumberImEffecting);
            
        }
        else
        {
            SwapSignSystem.instance.SecondPress(theNumberImEffecting);
        }
    }

    public bool CheckIfPressed()
    {
        return amIPressed;
    }

    

    
}

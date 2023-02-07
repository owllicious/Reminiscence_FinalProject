using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SwapSignSystem : MonoBehaviour
{
    public  GameObject[] swapingSigns = new GameObject[10];
    [HideInInspector] public static SwapSignSystem instance;
    [HideInInspector] public int theFirstPressed = -1;
    public int[] CorrectCombination = new int[10];
    [SerializeField] UnityEvent FirstCodeIsRightEvent;
    [SerializeField] UnityEvent SecondCodeIsRightEvent;
    bool FirstHasActivated = false;
    bool SecondHasActivated = false;


    private void Start()
    {
        if (instance == null)
            instance = this;

        if (FirstCodeIsRightEvent == null)
            FirstCodeIsRightEvent = new UnityEvent();

        if (SecondCodeIsRightEvent == null)
            SecondCodeIsRightEvent = new UnityEvent();
    }
    public void FirstPress(int numFromPress)
    {
       // swapingSigns[theFirstPressed].GetComponent<AudioSource>().Play();
        theFirstPressed = numFromPress;
       if( swapingSigns[theFirstPressed].TryGetComponent(out Animator anim))
        {
            anim.Play("SwapSignBlink");
        }
    }

    public void SecondPress(int theSecondPressed)
    {
        //swapingSigns[theSecondPressed].GetComponent<AudioSource>().Play();
        var sign1 = swapingSigns[theFirstPressed];
        var sign2 = swapingSigns[theSecondPressed];
        var tempPos = sign1.transform.position;
        var tempRot = sign1.transform.rotation;

        sign1.transform.position = sign2.transform.position;
        sign1.transform.rotation = sign2.transform.rotation;

        sign2.transform.position = tempPos;
        sign2.transform.rotation = tempRot;

        if (sign1.TryGetComponent(out Animator anim))
        {
            anim.Play("SwapSignIdle");
        }

        swapingSigns[theFirstPressed] = sign2;
        swapingSigns[theSecondPressed] = sign1;

        theFirstPressed = -1;

        CheckTheFirstCombination();
       // CheckTheSecondCombination();
    }

    void CheckTheFirstCombination()
    {


        for (int i = 0; i < 3; i++)
        {
          
            if (int.Parse(swapingSigns[i].name) != CorrectCombination[i])
            {
                return;
            }
        }

        FirstCodeActivate();
    }


   /* void CheckTheSecondCombination() 
     { 

            for (int i = 5; i < 10; i++)
            {
                if (int.Parse(swapingSigns[i].name) != CorrectCombination[i])
                {
                    return;
               
                }
            }
        SecondCodeActivate();
    }*/

    private void FirstCodeActivate()
    {
        if(!FirstHasActivated)
        {
            FirstCodeIsRightEvent.Invoke();
            FirstHasActivated = true;
        }
    }
    void SecondCodeActivate()
    {
        if (!SecondHasActivated)
        {
           SecondCodeIsRightEvent.Invoke();
            SecondHasActivated = true;
        }
    }






}

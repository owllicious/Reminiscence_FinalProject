using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{
    [Header("Sound")]

    [SerializeField] AudioSource playsound; //The sound

    [Header("UI")]

    [SerializeField] private GameObject soundText; //What the sound is saying
    [SerializeField] private GameObject itemList; //Secondry UI PopUP

    private int hasbeenplayed = 0;

    void OnTriggerEnter(Collider other)
    {
        if (!playsound.isPlaying)
        {
            playsound.Play();
            soundText.SetActive(true);
        }
        if (playsound.isPlaying)
        {
            hasbeenplayed++;
        }
    }
    private void Update()
    {
        if (!playsound.isPlaying && hasbeenplayed >= 1)
        {
            soundText.SetActive(false);
            Destroy(this.gameObject);
            itemList.SetActive(true);
        }
    }
}


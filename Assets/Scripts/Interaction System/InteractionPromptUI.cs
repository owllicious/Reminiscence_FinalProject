using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _mainCamera; // Main Camera
    [SerializeField] private GameObject _UIPanel; // Prompt Background 
    [SerializeField] private TextMeshProUGUI _promptText; // Prompt Text

    public bool IsDisplayed = false; // Is the Panel displayed

    private void Start() //Grab the camera and make sure there is no panel appearing
    {
        _mainCamera = Camera.main;
        _UIPanel.SetActive(false);
    }

    private void LateUpdate() // Makes the Interaction panel rotate with the camera 
    {
        var rotation = _mainCamera.transform.rotation;                                             
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }


    public void SetUp(string promptText) // Opens a panel with the action you can take with the item
    {
        _promptText.text = promptText;
        _UIPanel.SetActive(true);
        IsDisplayed = true;
    }
    public void Close() // Closes the panel with the action you can take with the item
    {
        _UIPanel.SetActive(false);
        IsDisplayed = false;
    }
}

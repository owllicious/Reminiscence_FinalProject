using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class Interactions : MonoBehaviour , IInteractable
{
    [Header("General")]
    
    [SerializeField] private InventoryItemData referenceItem;

    [SerializeField] private List<ItemRequirement> requirements;

    private InventorySystem inventorySystem;

    private bool removeRequirementsOnPickup;

    private GameObject _managers;

    [Header("Animations")]

    [SerializeField] private Animator _animator;

    [SerializeField] private string _animationName;

    [Header("UI")]

    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    [SerializeField] private GameObject _AdditionalText; //add the canvas

    [SerializeField] private GameObject _AdditionalText2; //add the canvas
    [Header("Camera")]

    [SerializeField] Camera _closerLookCamera;
    private Camera mainCamera;

    [Header("Sound")]

    [SerializeField] private AudioSource _audioSource;

    [Header("Prototyping")] //Prototype for now in a simple way

    [SerializeField] Transform _AstronomyRoom; //Everything inside the astronomy room that needs to be turned when button pressed

    private void Start()
    {
        mainCamera = Camera.main;

        _managers = GameObject.Find("-GameManagers-");

        inventorySystem = _managers.GetComponent<InventorySystem>(); 
    }
    public bool Interact(Interactor interactor) //Action
    {
        if (this.CompareTag("Door")) // Checks if its tagged as a door/drawer if so do the door action
        {
            if (MeetsRequirments() || requirements.Count == 0)
            {
                if (isClosed(this))
                {
                    RemoveRequirement();
                    Open();
                    AdditionalText();
                }
            }
            if(!isClosed(this))
            {
                Close();
                AdditionalText();
            }
        }
        if (this.CompareTag("Button")) // Checks if its tagged as a button if so do the button action
        {
            Press();
            AdditionalText();
        }
        if(this.CompareTag("Item")) //Checks if its tagged as an item if so it adds it to inventory
        {
            PickUp();
            AdditionalText();
        }
        if(this.CompareTag("Zoom")) //Closer Look
        {

            CloserLook();
            AdditionalText();
        }
        
        if(this.TryGetComponent(out SwapingSign ss))
        {
            ss.Interact();
        }
        return true;

    }

    private void Open() // Opens a door/drawer or something along those lines
    {
        _animator = GetComponent<Animator>(); //Find the animator of this object
        _animator.Play(_animationName,0,0.25f); //Play the animation
        Debug.Log("Door Open"); // Debugging
    }
    private void Close() // Closes a door/drawer or something along those lines
    {
        _animator = GetComponent<Animator>(); //Find the animator of this object
        _animator.Play(_animationName, 0, 0.25f);  //Play the animation
        Debug.Log("Door Close"); // Debugging
    }
    private void Press() // Preforms the action the button is meant to do -- Telescope ProtoType
    {
        float _temp;
        _temp = _AstronomyRoom.transform.rotation.y;
        _temp = _temp + 90;
        
        //_AstronomyRoom.transform.rotation.y = _temp;
        Debug.Log("Button Pressed"); // Create Pressing Action
    }
    private void PickUp() // Adds the Item to your inventory 
    {
        inventorySystem.Get(referenceItem);
        inventorySystem.Add(referenceItem); // Adds 1 to the stack
        Destroy(this.gameObject); //Removes the item from the scene
    }
    private void CloserLook() // Moves camera to a different angle in which you get a closer look and unlocks mouse so that you can click things in the area
    {
        
        if (_closerLookCamera != null)
        {
            if (_closerLookCamera.isActiveAndEnabled == false)
            {
                _closerLookCamera.gameObject.SetActive(true);
                mainCamera.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true ;
                Debug.Log("Closer Cam");
            }
            else
            {
                _closerLookCamera.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Debug.Log("Main Cam");
            }
        }
        //Cursor.lockState = CursorLockMode.None;
        Debug.Log("Open a Closer Look"); // Create a Closer Look Action
    }
    private bool isClosed(object obj) //Checks if the door is open to know if its supposed to close it or open it
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || _animator.GetCurrentAnimatorStateInfo(0).IsName("CloseDoor")) //find out which state the animator is in
        {
            _animationName = "OpenDoor"; //if its on idle or closed then it need to open it
            return true;
        }
        else
        {
            _animationName = "CloseDoor"; //if its not on idle or closed then it needs to close it
            return false;
        }
    }
    public bool MeetsRequirments()
    {
        foreach (ItemRequirement requirement in requirements)
        {
            if (!requirement.HasRequirement())
            { return false;  }
        }
        return true;
    }
    private void RemoveRequirement()
    {
        foreach(ItemRequirement requirement in requirements)
        {
            for(int i = 0; i<requirement.amount; i++)
            {
                inventorySystem.Remove(requirement.itemdata);
            }
        }
    }
    private async void AdditionalText()
    {
        if(_AdditionalText != null)
        {
            _AdditionalText.SetActive(true);
        }
        if (_AdditionalText2 != null)
        {
            _AdditionalText2.SetActive(true);
            await Task.Delay(3000);
            _AdditionalText2.SetActive(false);

        }
    }
}

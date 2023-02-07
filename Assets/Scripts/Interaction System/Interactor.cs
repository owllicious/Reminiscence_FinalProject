using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint; // Interaction Point
    [SerializeField] private float _interactionPointRadius = 0.5f; // Collider Visual Radius
    [SerializeField] private LayerMask _interactableMask; //Select the Interactable Layer for the objects for interactble objects
    [SerializeField] private InteractionPromptUI _interactionPromptUI; //Which message do you want to display when next to the object

    private readonly Collider[] _colliders = new Collider[3]; //How many interactable objects are you in contact with Max 3
    [SerializeField] private int _numFound;

    private IInteractable _interactable; //Dont worry about it :)
    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if(_numFound > 0 )
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if(_interactable != null)
            {
                if(!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(_interactable.InteractionPrompt); // Create a pop up based on the prompt


                if (Keyboard.current.eKey.wasPressedThisFrame) _interactable.Interact(this); // Do the interaction for this object if they press E
            }
        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close(); //Close the pop up
        }
    }
    private void OnDrawGizmos() //Collider Visualization
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}

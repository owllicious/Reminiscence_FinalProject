using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller; //Getting the Character Controller

    [SerializeField] private float speed = 2f; //Setting the movement Speed

    [SerializeField] private float jumpHeight = 1f; //Setting the Jump Height

    [SerializeField] private float gravity = -9.81f; //Earths Gravity

    [SerializeField] private Transform groundCheck; //Object inside the player to check if they are on the ground

    [SerializeField] private float groundDistance = 0.4f; //The Radius for the Sphere that checks distance from the ground

    [SerializeField] private LayerMask groundMask; //The layer that the object will recognize as ground

    private bool isGrounded; // Check if the player is on the ground

    private Vector3 velocity;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Checking if its grounded based on the sphere

        if (isGrounded && velocity.y < 0) //Resetting the Velocity
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //Movement
        if (Keyboard.current.shiftKey.isPressed) //Sprint instead
        {
            if (speed < 4f) speed = speed + 0.5f; //Capping the sprint speed at 4
            else speed = 4f;
        }
        else
        {
            speed = 2f;
        }
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) //Jump
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime; //Gravitational Pull
        controller.Move(velocity * Time.deltaTime);
    }
}

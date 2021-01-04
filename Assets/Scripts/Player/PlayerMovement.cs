using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] private int playerSpeed = 4; 
    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity; 
    //GroundCheck 
    [SerializeField] private Transform groundCheck; 
    private float checkerRadius = 0.4f;
    [SerializeField] private LayerMask groundMask; 
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkerRadius,groundMask);
        if(isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        if (Input.GetButton("Fire1"))
        {
            controller.Move(Camera.main.transform.forward * playerSpeed * Time.deltaTime);
        }
        
        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity*Time.deltaTime);
    }
}

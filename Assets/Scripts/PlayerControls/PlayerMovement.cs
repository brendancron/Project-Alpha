﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject menu;

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;

    public float jumpHeight = 3f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance,groundMask);

        if(isGrounded && velocity.y < -2f) {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        float x = 0;
        float z = 0;

        if(!menu.activeSelf) {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

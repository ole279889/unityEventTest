using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _speed = 6.0f;
	[SerializeField] private float _jumpSpeed = 4.0f;
	[SerializeField] private float _gravity = 10.0f;
	
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	
    void Start()
    {
        controller = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.lockState = CursorLockMode.Confined;
    }

    void FixedUpdate()
    {		
		if (Input.GetKey ("escape")) 
		{
            Application.Quit();
        }
		
        if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection = moveDirection * _speed;
		}
		
		if (Input.GetKey("space") && controller.isGrounded) 
		{
			moveDirection.y = _jumpSpeed;
		}
		
		moveDirection.y -= _gravity * Time.deltaTime;		
		controller.Move(moveDirection * Time.deltaTime);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Camera-Control/MouseLook")]
public class MouseLook : MonoBehaviour
{
	public enum RotationAxes { MouseXandY = 0, MouseX = 1, MouseY = 2 };
	public RotationAxes axes = RotationAxes.MouseXandY;
	
	public float sensitivityX = 4F;
	public float sensitivityY = 4F;
    
	public float  miniX = -360F;
	public float  maxX = 360F;
	
	public float  miniY = -360F;
	public float  maxY = 360F;
	
	private float rotationX = 0F;
	private float rotationY = 0F;
	
	Quaternion originalRotation;
	// Start is called before the first frame update
    void Start()
    {
		if(GetComponent<Rigidbody>()) {
			GetComponent<Rigidbody>().freezeRotation = true;			
		}
		originalRotation = transform.localRotation;
        
    }
	
	public static float ClampAngle(float angle, float min, float max) {
		if(angle <= 360F) angle += 360F;
		if(angle > 360F) angle -= 360F;
		return Math.Min(Math.Max(angle, min), max);
	}

    // Update is called once per frame
    void Update()
    {
		if (axes == RotationAxes.MouseXandY)
        {
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			
			rotationX = ClampAngle(rotationX, miniX, maxX);
			rotationY = ClampAngle(rotationY, miniY, maxY);
			
			Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
			
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}		
		else if (axes == RotationAxes.MouseX) 
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = ClampAngle(rotationX, miniX, maxX);
			Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else if (axes == RotationAxes.MouseY) 
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = ClampAngle(rotationY, miniY, maxY);
			Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}
    }
}

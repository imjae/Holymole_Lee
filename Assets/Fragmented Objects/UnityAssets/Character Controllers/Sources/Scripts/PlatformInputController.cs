using UnityEngine;
using System;

[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/Platform Input Controller")]

public class PlatformInputController:MonoBehaviour{
    // This makes the character turn to face the current movement speed per default.
    public bool autoRotate = true;
    public float maxRotationSpeed = 360.0f;
    
    CharacterMotor motor;
    
    // Use this for initialization
    public void Awake() {
    	motor = GetComponent<CharacterMotor>();
    }
    
    // Update is called once per frame
    public void Update() {
    	// Get the input vector from keyboard or analog stick
    	Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    	
    	if (directionVector != Vector3.zero) {
    		// Get the length of the directon vector and then normalize it
    		// Dividing by the length is cheaper than normalizing when we already have the length anyway
    		float directionLength = directionVector.magnitude;
    		directionVector = directionVector / directionLength;
    		
    		// Make sure the length is no bigger than 1
    		directionLength = Mathf.Min(1.0f, directionLength);
    		
    		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
    		// This makes it easier to control slow speeds when using analog sticks
    		directionLength = directionLength * directionLength;
    		
    		// Multiply the normalized direction vector by the modified length
    		directionVector = directionVector * directionLength;
    	}
    	
    	// Rotate the input vector into camera space so up is camera's up and right is camera's right
    	directionVector = Camera.main.transform.rotation * directionVector;
    	
    	// Rotate input vector to be perpendicular to character's up vector
    	Quaternion camToCharacterSpace = Quaternion.FromToRotation(-Camera.main.transform.forward, transform.up);
    	directionVector = (camToCharacterSpace * directionVector);
    	
    	// Apply the direction to the CharacterMotor
    	motor.inputMoveDirection = directionVector;
    	motor.inputJump = Input.GetButton("Jump");
    	
    	// Set rotation to the move direction	
    	if (autoRotate && directionVector.sqrMagnitude > 0.01f) {
    		Vector3 newForward = ConstantSlerp(
    			transform.forward,
    			directionVector,
    			maxRotationSpeed * Time.deltaTime
    		);
    		newForward = ProjectOntoPlane(newForward, transform.up);
    		transform.rotation = Quaternion.LookRotation(newForward, transform.up);
    	}
    }
    
    public Vector3 ProjectOntoPlane(Vector3 v,Vector3 normal) {
    	return v - Vector3.Project(v, normal);
    }
    
    public Vector3 ConstantSlerp(Vector3 from,Vector3 to,float angle) {
    	float value = Mathf.Min(1.0f, angle / Vector3.Angle(from, to));
    	return Vector3.Slerp(from, to, value);
    }
    
    // Require a character controller to be attached to the same game object

}
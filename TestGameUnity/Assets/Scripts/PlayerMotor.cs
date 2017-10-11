using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires a Rigidbody component.
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
	
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private float camRotationX = 0.0f;
	private float currentCamRotationX = 0.0f;

	[Range(0.0f, 90.0f)]
	public float cameraAngleLimit = 0.0f;

	// Reference components.
	private Rigidbody body;

	// This field is optional. Will only apply
	// rotation on y axis if it is not referenced.
	[SerializeField]
	private Camera cam;

	// Called when the PlayerMotor is created.
	// Gets the rigid body component.
	// Sets the distance to ground.
	void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	// Set the velocity to the velocity we
	// calculated in the PlayerController.
	public void SetVelocity(Vector3 pvelocity) {
		velocity = pvelocity;
	}

	// Set the rotation to the rotation we
	// calculated in the PlayerController.
	public void SetRotation(Vector3 protation) {
		rotation = protation;
	}

	// Set the camera rotation to the camera rotation
	// we calculate in the PlayerController.
	public void SetCameraRotation(float pcamRotationX) {
		camRotationX = pcamRotationX;
	}

	// Runs every physics iteration.
	void FixedUpdate() {
		Move();
		Rotate();

		// If there is not camera, only apply 
		// rotation to the y axis.
		if (cam != null) {
			RotateCamera();
		}
	}

	// Apply the velocity to the player.
	void Move() {
		// Do not move player if the velocity is zero.
		if (velocity != Vector3.zero) {
			// Will not move player into wall.*
			body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
		}
	}

	// Apply the rotation to the player.
	void Rotate() {
		// Do not rotate player if rotation is zero.
		if (rotation != Vector3.zero) {
			body.MoveRotation(body.rotation * Quaternion.Euler(rotation));
		}
	}

	// Apply the rotation to the camera.
	void RotateCamera() {
		// Do not rotate camera is rotation is zero.
		if (camRotationX != 0.0f) {
			currentCamRotationX -= camRotationX;

			// Clamp rotation to the camera angle limit. 
			// So we cannot look all the way over our head, and
			// under our feet.
			currentCamRotationX = Mathf.Clamp(currentCamRotationX, -cameraAngleLimit, cameraAngleLimit);

			// Apply the rotation.
			cam.transform.localEulerAngles = new Vector3(currentCamRotationX, 0.0f, 0.0f);
		}
	}
}

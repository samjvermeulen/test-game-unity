using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires a PlayerMotor script.
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5.0f;

	[SerializeField]
	private float mouseSensitivity = 3.0f;

	private PlayerMotor motor;

	// Called when PlayerController is created.
	// Gets the player motor component.
	void Start() {
		// Hide player cursor.
		Cursor.visible = false;

		motor = GetComponent<PlayerMotor> ();
	}

	// Called every frame of the game.
	// Gets input from the player, then converts it
	// into the movement velocity vector.
	void Update() {
		// Get the direction of movement from the input.
		float xMov = Input.GetAxisRaw("Horizontal");
		float zMov = Input.GetAxisRaw("Vertical");

		// Create independant axis movement velocity vectors.
		Vector3 movHorizontal = transform.right * xMov;
		Vector3 movVertical = transform.forward * zMov;

		// Create the movement velocity vector.
		Vector3 velocity = (movHorizontal + movVertical) * speed;

		// Create independant axis rotation float.
		float xRot = Input.GetAxisRaw("Mouse Y");
		float yRot = Input.GetAxisRaw("Mouse X");

		// We apply rotation on y axis to the rigid body for turning.
		// But for x axis rotation we only want the camera to move,
		// not the entire body.

		// Create y rotation vector with mouse sensitivity applied.
		Vector3 rotation = new Vector3 (0.0f, yRot, 0.0f) * mouseSensitivity;

		// Create x rotation vector with mouse sensitivity applied.
		float camRotation = xRot * mouseSensitivity;

		// Apply the movement, rotation, and camera rotation.
		motor.SetVelocity(velocity);
		motor.SetRotation(rotation);
		motor.SetCameraRotation(camRotation);
	}
}

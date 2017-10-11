using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires a BoxCollider. (Used as the trigger for the
// ground detection.
[RequireComponent(typeof(BoxCollider))]
public class GroundSensor : MonoBehaviour {

	private bool touchingGround;
	private bool changed;

	public BoxCollider boxCollider;

	// Called when the script is created.
	// Sets box collider to be a trigger.
	void Start() {
		boxCollider.isTrigger = true;
	}
		
	void OnTriggerEnter() {
		touchingGround = true;
	}

	void OnTriggerExit() {
		touchingGround = false;
	}

	public bool IsTouching() {
		return touchingGround;
	}

	public bool HasChanged() {
		return changed;
	}

	public void SetChanged(bool pchanged) {
		changed = pchanged;
	}

	void LateUpdate() {
		changed = false;
	}
}

using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/Camera Zone for Third Person Camera")]
[RequireComponent (typeof (BoxCollider))]
public class CameraZone : MonoBehaviour {

	public ThirdPersonCamera.CameraStates cameraZoneEffect;

	public Color color = Color.red; 

	[Header("Zone Effect Settings : StickToObject")]
	public GameObject objectToStickTo;

	void OnDrawGizmos() {
		Gizmos.color = color;
		Gizmos.DrawWireCube (transform.position, GetComponent<BoxCollider> ().size);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			switch (cameraZoneEffect) {
			case ThirdPersonCamera.CameraStates.StickToObject:
			default:
				other.GetComponent<ThirdPersonPlayer> ().gamecam.objectToStickTo = objectToStickTo;
				break;

			case ThirdPersonCamera.CameraStates.Small:
				other.GetComponent<ThirdPersonPlayer> ().gamecam.small = true;
				break;
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			switch (cameraZoneEffect) {
			case ThirdPersonCamera.CameraStates.StickToObject:
			default:
				other.GetComponent<ThirdPersonPlayer> ().gamecam.objectToStickTo = null;
				break;

			case ThirdPersonCamera.CameraStates.Small:
				other.GetComponent<ThirdPersonPlayer> ().gamecam.small = false;
				break;
			}
		}
	}
}

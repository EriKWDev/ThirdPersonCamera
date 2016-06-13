using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/Camera Zone for Third Person Camera")]
[RequireComponent (typeof (BoxCollider))]
public class CameraZone : MonoBehaviour {

	public ThirdPersonCamera.CameraStates cameraZoneEffect;

	public bool showLines = false;
	public Color color = Color.red; 

	[Header("Zone Effect Settings : StickToObject")]
	public GameObject objectToStickTo;

	void OnDrawGizmos() {
		if(showLines)
			DrawCube (transform.position, transform.rotation, GetComponent<BoxCollider> ().size);
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

	public void DrawCube (Vector3 position, Quaternion rotation, Vector3 scale) {
		DrawCube (position, rotation, scale, color);
	}

	public void DrawCube (Vector3 position, Quaternion rotation, Vector3 scale, Color cubeColor) {
		Matrix4x4 cubeTransform = Matrix4x4.TRS (position, rotation, scale);
		Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

		Gizmos.matrix = cubeTransform;
		Gizmos.color = cubeColor;

		Gizmos.DrawWireCube (Vector3.zero, Vector3.one);

		Gizmos.matrix = oldGizmosMatrix;
	}
}

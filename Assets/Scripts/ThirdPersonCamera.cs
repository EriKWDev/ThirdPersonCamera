using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/Third Person Camera Follow Script")]
[DisallowMultipleComponent]
public class ThirdPersonCamera : MonoBehaviour {

	[Header("Debugging")]
	public bool debug = false;

	[Header("Public Values")]
	public Transform followTransform;

	[Header("Private Values")]
	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float offsetSides;

	[Header("Smoothing and Damping")]
	[SerializeField]
	private float camSmoothDampTime = 0.1f;
	[SerializeField]
	private Vector3 velocityCamSmooth = Vector3.zero;

	private Vector3 lookDir;
	private Vector3 targetPosition;

	void LateUpdate() {
		Vector3 characterOffset = followTransform.position + new Vector3 (0f, distanceUp, 0f);

		lookDir = characterOffset - transform.position;
		lookDir.y = 0f;
		lookDir.Normalize ();
		if (debug) {
			Debug.DrawRay (transform.position, lookDir, Color.green);
		}

		targetPosition = characterOffset + followTransform.up * distanceUp - lookDir * distanceAway + ;

		if (debug) {
			Debug.DrawRay (followTransform.position + (-lookDir * distanceAway), Vector3.up * distanceUp, Color.red);
			Debug.DrawRay (followTransform.position, -1f * lookDir * distanceAway, Color.blue);
			Debug.DrawLine (followTransform.position, targetPosition, Color.magenta);
		}

		CompensateForWalls (characterOffset, ref targetPosition);

		SmoothPosition (transform.position, targetPosition);

		transform.LookAt (characterOffset);
	}

	private void SmoothPosition(Vector3 fromPos, Vector3 toPos) {
		transform.position = Vector3.SmoothDamp (fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget) {
		if (debug) {
			Debug.DrawLine (fromObject, fromObject, Color.cyan);
		}
		RaycastHit wallHit = new RaycastHit ();
		if (Physics.Linecast (fromObject, fromObject, out wallHit)) {
			if (debug) {
				Debug.DrawRay (wallHit.point, Vector3.left, Color.red);
			}	
			toTarget = new Vector3 (wallHit.point.x, wallHit.point.y, wallHit.point.z);
		}
	}
}

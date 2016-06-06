using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/Third Person Camera Follow Script")]
[DisallowMultipleComponent]
public class ThirdPersonCamera : MonoBehaviour {

	[Header("Debugging")]
	public bool debug = false;

	[Header("Private Values")]
	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform followTransform;
	[SerializeField]
	private Vector3 offset = new Vector3 (0f, 1.5f, 0f);

	[Header("Smoothing and Damping")]
	[SerializeField]
	private float camSmoothDampTime = 0.1f;
	private Vector3 velocityCamSmooth = Vector3.zero;

	private Vector3 lookDir;
	private Vector3 targetPosition;

	//[Header("Public Values")]

	void Start () {
		followTransform = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update () {
	
	}

	void OnDrawGizmos () {
		
	}

	//Test change for commit
	void LateUpdate() {
		Vector3 characterOffset = followTransform.position + offset;

		lookDir = characterOffset - transform.position;
		lookDir.y = 0f;
		lookDir.Normalize ();
		if (debug) {
			Debug.DrawRay (transform.position, lookDir, Color.green);
		}

		targetPosition = followTransform.position + followTransform.up * distanceUp - lookDir * distanceAway;

		if (debug) {
			Debug.DrawRay (followTransform.position + (-lookDir * distanceAway), Vector3.up * distanceUp, Color.red);
			Debug.DrawRay (followTransform.position, -1f * lookDir * distanceAway, Color.blue);
			Debug.DrawLine (followTransform.position, targetPosition, Color.magenta);;
		}

		SmoothPosition (transform.position, targetPosition);


		transform.LookAt (followTransform);
	}

	private void SmoothPosition(Vector3 fromPos, Vector3 toPos) {
		transform.position = Vector3.SmoothDamp (fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}
}

using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/Third Person Camera Follow Script")]
[DisallowMultipleComponent]
public class ThirdPersonCamera : MonoBehaviour {

	[Header("Debug")]
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

		targetPosition = followTransform.position + followTransform.up * distanceUp - followTransform.forward * distanceAway;

		if (debug) {
			Debug.DrawRay (followTransform.position + (-followTransform.forward * distanceAway), Vector3.up * distanceUp, Color.red);
			Debug.DrawRay (followTransform.position, -1f * followTransform.forward * distanceAway, Color.blue);
			Debug.DrawLine (followTransform.position, targetPosition, Color.magenta);;
		}

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		transform.LookAt (followTransform);
	}
}

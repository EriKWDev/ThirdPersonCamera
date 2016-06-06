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
	private Transform follow;
	private Vector3 targetPosition;

	//[Header("Public Values")]

	void Start () {
		follow = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update () {
	
	}

	void OnDrawGizmos () {
		
	}

	//Test change for commit
	void LateUpdate() {
		targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;

		if (debug) {
			Debug.DrawRay (follow.position, Vector3.up * distanceUp, Color.red);
			Debug.DrawRay (follow.position, -1f * follow.forward * distanceAway, Color.blue);
			Debug.DrawLine (follow.position, targetPosition, Color.magenta);;
		}

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		transform.LookAt (follow);
	}
}

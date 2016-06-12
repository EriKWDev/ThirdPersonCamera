using UnityEngine;
using System.Collections;

public class drawMeInGizmos : MonoBehaviour {

	public Color color = Color.red; 
	public Vector3 size = new Vector3 (0.2f, 0.2f, 0.2f);

	void OnDrawGizmos() {
		Gizmos.color = color;
		Gizmos.DrawCube (transform.position, size);
	}
}

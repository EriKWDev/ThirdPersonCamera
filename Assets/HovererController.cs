using UnityEngine;
using System.Collections;

public class HovererController : MonoBehaviour {

    public float acceleration;
    public float rotationRate;

    public float turnRotationAngle;
    public float turnRotationSeekSpeed;
    private Camera playerCamera;

    public CameraBuffEffects.Buffs buff;

    public Material[] playerMaterials;

    private float rotationVelocity;
    private float groundAngleVelocity;
    private float originalAcceleration;

    private Rigidbody rigidbody;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        originalAcceleration = acceleration;
        playerCamera = GetComponentInChildren<Camera>();
        transform.position = new Vector3(0f, 14f, 0f);
        GetComponent<Renderer>().material = playerMaterials[GameObject.FindGameObjectsWithTag("Player").Length - 1];
    }

	void FixedUpdate () {
        if (Physics.Raycast(transform.position, transform.up * -1f, 3f)) {
            rigidbody.drag = 1f;

            Vector3 forwardForce = transform.forward * acceleration * Input.GetAxis("Vertical");

            forwardForce = forwardForce * Time.deltaTime * rigidbody.mass;

            rigidbody.AddForce(forwardForce);
        } else {
            rigidbody.drag = 0f;
        }

        Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis("Horizontal");

        turnTorque = turnTorque * Time.deltaTime * rigidbody.mass;
        rigidbody.AddTorque(turnTorque);

        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, Input.GetAxis ("Horizontal") * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
	}

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        switch (buff)
        {
            case CameraBuffEffects.Buffs.Speed:
                acceleration = originalAcceleration * 1.3f;
                break;
            case CameraBuffEffects.Buffs.None:
            default:
                acceleration = originalAcceleration;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Buff>() != null)
        {
            playerCamera.GetComponent<CameraBuffEffects>().buff = other.GetComponent<Buff>().buff;
            buff = other.GetComponent<Buff> ().buff;
        }
    }
}

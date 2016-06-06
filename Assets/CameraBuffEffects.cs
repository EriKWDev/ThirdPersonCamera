using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraBuffEffects : MonoBehaviour
{
    
    public enum Buffs
    {
        None,
        Speed
    }

    private float fov;
    private bool speedBuff = false;

    public float velocity;
    public Buffs buff;

    void Start()
    {
        fov = this.GetComponent<Camera> ().fieldOfView;
    }

	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.E)) {
          buff = Buffs.Speed;
        }

        switch (buff)
        {
            case Buffs.Speed:
                speedBuff = true;
                break;

            case Buffs.None:
            default:
                deActivateAllBuffEffects();
                break;
        }

        float newFov = 0f;
        if (speedBuff)
        {
            this.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>().enabled = true;
            newFov = fov * 2f;
        }
        else
        {
            GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>().enabled = false;
            newFov = fov;
        }

        this.GetComponent<Camera>().fieldOfView = Mathf.SmoothDamp(this.GetComponent<Camera>().fieldOfView, newFov, ref velocity, 0.4f);
	}

    void deActivateAllBuffEffects () {
        speedBuff = false;
    }
}

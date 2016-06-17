using UnityEngine;
using System.Collections;

public class Buff : MonoBehaviour {
    public CameraBuffEffects.Buffs buff;

    void Update ()
    {
        transform.Rotate(Vector3.one);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : MonoBehaviour,IMovement {

    public float speed;

    public Vector3 Move() {
        return Vector3.left * speed * Time.fixedDeltaTime;
    }

    public void SetSpeed(float s) {
        speed = s;
    }
}

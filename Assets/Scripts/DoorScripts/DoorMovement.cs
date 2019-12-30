using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour {
    
    public Vector3 closed;

    public Vector3 open;

    public float speed = 0.05f;

    public CheckPoint checkPoint;

    void Update() {
        if(checkPoint.IsActive()) {
            transform.position = Vector3.MoveTowards(transform.position, open, speed);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, closed, speed);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : CheckPoint {
    
    int objectsOn = 0;

    void OnTriggerEnter(Collider col) {
        objectsOn++;
    }

    void OnTriggerExit(Collider col) {
        objectsOn--;
    }

    public override bool IsActive() {
        return (objectsOn > 0);
    }

}

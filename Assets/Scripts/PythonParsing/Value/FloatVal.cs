using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVal : NumVal {
    public readonly float floatVal;

    public FloatVal(float f) {
        floatVal = f;
    }

    public FloatVal(NumVal v) {
        if(typeof(FloatVal).IsInstanceOfType(v)) {
            floatVal = (float)(((IntVal)v).intVal);
        } else if(typeof(FloatVal).IsInstanceOfType(v)) {
            floatVal = (((FloatVal)v).floatVal);
        } else {
            throw new TypeMismatchException("this error should never occur! Must be wrong class that implements numVal");
        }
    }

}

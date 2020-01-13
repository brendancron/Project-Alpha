using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringVal : Value {
    public readonly string stringVal;

    public StringVal(string s) {
        stringVal = s;
    }
}

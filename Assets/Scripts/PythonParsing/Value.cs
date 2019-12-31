using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value {
    public readonly string str_val;
    public readonly bool bool_val;
    public readonly float float_val;

    public readonly ValType valType;

    public Value(string s) {
        str_val = s;
        bool_val = false;
        float_val = 0;
        valType = ValType.String;
    }

    public Value(float f) {
        str_val = "";
        bool_val = false;
        float_val = f;
        valType = ValType.Float;
    }

    public Value(bool b) {
        str_val = "";
        bool_val = b;
        float_val = 0;
        valType = ValType.String;
    }

    public enum ValType {
        String,
        Bool,
        Float
    }

}

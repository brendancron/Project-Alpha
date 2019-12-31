using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value {
    public readonly string str_val;
    public readonly bool bool_val;
    public readonly float float_val;
    public readonly FunctionVal func_val;
    public readonly ObjectVal obj_val;
    public readonly ClassVal class_val;

    public readonly ValType valType;

    public Value(string s) {
        str_val = s;
        bool_val = false;
        float_val = 0;
        func_val = null;
        valType = ValType.String;
    }

    public Value(float f) {
        str_val = "";
        bool_val = false;
        float_val = f;
        func_val = null;
        valType = ValType.Float;
    }

    public Value(bool b) {
        str_val = "";
        bool_val = b;
        float_val = 0;
        func_val = null;
        valType = ValType.String;
    }

    public Value(FunctionVal f) {
        str_val = "";
        bool_val = false;
        float_val = 0;
        func_val = f;
        valType = ValType.Function;
    }

    public enum ValType {
        String,
        Bool,
        Float,
        Function,
        ObjectType,
        ClassType
    }

    public Value GetValue() {
        switch(valType) {
            case ValType.Bool:
                return new Value(bool_val);
            case ValType.Float:
                return new Value(float_val);
            case ValType.String:
                return new Value(str_val);
            case ValType.Function:
                return new Value(func_val);
        }
        return null;
    }

}

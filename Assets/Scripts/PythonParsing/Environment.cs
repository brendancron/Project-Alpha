using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment {

    List<Dictionary<string,Value>> environment;

    public Environment() {
        environment = new List<Dictionary<string, Value>>();
        PushScope();
    }

    public void PushScope() {
        environment.Insert(0, new Dictionary<string, Value>());
    }

    public void RemoveScope() {
        environment.RemoveAt(0);
    }

    public Value GetVar(string name) {
        foreach(Dictionary<string, Value> scope in environment) {
            if(scope.ContainsKey(name)) {
                return scope[name];
            }
        }
        throw VariableDoesNotExistException(name + " does not exist in the given context");
    }

    public void AssignVar(string name, Value val) {
        Dictionary<string, Value> scope = environment[0];
        if(scope.ContainsKey(name)) {
            scope[name] = val;
        } else {
            scope.Add(name, val);
        }
    }

}

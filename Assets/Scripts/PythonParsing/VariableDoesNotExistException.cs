using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableDoesNotExistException : Exception {
    public VariableDoesNotExistException(string message): base(message) {}
}

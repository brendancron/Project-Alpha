using System;

public class VariableDoesNotExistException : Exception {
    public VariableDoesNotExistException(string message): base(message) {}
}

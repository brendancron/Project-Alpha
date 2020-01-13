using System.Collections;
using System.Collections.Generic;

public class FunctionVal : Value {

    public readonly List<string> parameters;
    public readonly Statement code;

    public FunctionVal(List<string> parameters, Statement code) {
        this.parameters = parameters;
        this.code = code;
    }

}

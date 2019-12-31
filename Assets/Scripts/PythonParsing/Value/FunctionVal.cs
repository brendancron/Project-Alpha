using System.Collections;
using System.Collections.Generic;

public class FunctionVal {

    public readonly List<Expression> args;
    public readonly Statement code;

    public FunctionVal(List<Expression> args, Statement code) {
        this.args = args;
        this.code = code;
    }

}

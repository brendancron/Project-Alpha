using System.Collections;
using System.Collections.Generic;

public class FunctionVal {

    public readonly List<Expression> args;
    public readonly Statement code;

    publuc FunctionVal(List<Expression> args, Statement code) {
        this.args = args;
        this.code = code;
    }

}

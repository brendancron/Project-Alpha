using System.Collections.Generic;

public class FunctionCallExpression : Expression{
	public FunctionCallExpression(Expression fn, List<Expression> arguments) {
		this.args = arguments;
	}
	private List<Expression> args;
	private Expression fn;

	public override string ToString() {
		return "FunctionCall("+args+")";
	}

	public override Value Eval(Environment env) {
		FunctionVal fVal = fn.Eval(env);
		if(fVal.valType == Value.ValType.Function) {
			FunctionVal f = fVal.func_val;
			//THIS MUST BE CORRECT ENVIRONMENT CHECK
			Environment e = f.code.Eval(env);
			return e.GetVar("~ret");
		} else {
			throw TypeMismatchException("Must be a function type!!!!! REEEEEEE");
		}
	}
}
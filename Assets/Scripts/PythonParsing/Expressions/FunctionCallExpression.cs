using System.Collections.Generic;

public class FunctionCallExpression : Expression{
	public FunctionCallExpression(Expression fn, List<Expression> arguments) {
		this.args = arguments;
		this.fn = fn;
	}
	private List<Expression> args;
	private Expression fn;

	public override string ToString() {
		return "FunctionCall("+args+")";
	}

	public override Value Eval(Environment env) {
		Value fVal = fn.Eval(env);
		if(fVal.valType == Value.ValType.Function) {
			FunctionVal f = fVal.func_val;
			//THIS MUST BE CORRECT ENVIRONMENT CHECK
			env.PushScope();
			//Should correspond to None!!!
			env.AssignVar("~ret", null);
			f.code.Eval(env);
			Value val = env.GetVar("~ret");
			env.RemoveScope();
			return val;
		} else {
			throw new TypeMismatchException("Must be a function type!!!!! REEEEEEE");
		}
	}
}
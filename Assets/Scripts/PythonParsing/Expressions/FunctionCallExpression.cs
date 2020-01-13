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
		Environment temp = new Environment(env);
		Value fVal = fn.Eval(env);
		if(typeof(FunctionVal).IsInstanceOfType(fVal)) {
			FunctionVal f = (FunctionVal)(fVal);
			//THIS MUST BE CORRECT ENVIRONMENT CHECK
			env.PushScope();
			//Should correspond to None!!!
			env.AssignVar("~ret", null);
			if(args.Count != f.parameters.Count) {
				throw new System.Exception("ERROR"); //PLEASE HANDLE THIS LINE
			}
			for(int i = 0; i < args.Count; i++) {
				env.AssignVar(f.parameters[i], args[i].Eval(temp));
			}
			f.code.Eval(env);
			Value val = env.GetVar("~ret");
			env.RemoveScope();
			return val;
		} else {
			throw new TypeMismatchException("Must be a function type!!!!! REEEEEEE");
		}
	}
}
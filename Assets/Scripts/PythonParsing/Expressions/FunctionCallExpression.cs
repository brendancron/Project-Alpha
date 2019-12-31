using System.Collections.Generic;

public class FunctionCallExpression : Expression{
	public FunctionCallExpression(string id, List<Expression> arguments) {
		this.id = id;
		this.args = arguments;
	}
	private string id;
	private List<Expression> args;

	public override string ToString() {
		return "FunctionCall( "+id+", "+args+" )";
	}

	public override Value Eval() {
		return null;
	}
}
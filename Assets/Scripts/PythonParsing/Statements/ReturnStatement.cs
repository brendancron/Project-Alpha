public class ReturnStatement : Statement{
	public ReturnStatement(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Return( "+expr+" )";
	}

	public override void Eval(Environment env) {
		env.AssignVar("~ret", expr.Eval(env));
	}
}
public class CommandStatement : Statement{
	public CommandStatement(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Command( "+expr+" )";
	}

	public override void Eval(Environment env){}
}
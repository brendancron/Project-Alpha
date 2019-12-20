public class ReturnStatement : Statement{
	public ReturnStatement(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Return( "+expr+" )";
	}
}
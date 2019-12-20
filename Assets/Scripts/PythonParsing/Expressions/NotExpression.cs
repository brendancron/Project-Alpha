public class NotExpression : Expression{
	public NotExpression(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Not( "+expr+" )";
	}
}
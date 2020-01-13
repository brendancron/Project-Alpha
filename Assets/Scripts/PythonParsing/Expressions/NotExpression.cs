public class NotExpression : Expression{
	public NotExpression(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Not( "+expr+" )";
	}
	public override Value Eval(Environment env) {
		Value ev = expr.Eval(env);
		if(typeof(BoolVal).IsInstanceOfType(ev)) {
			return new BoolVal(!(((BoolVal)(ev)).boolVal));
		}
		throw new TypeMismatchException("Must be a bool type");
	}
}
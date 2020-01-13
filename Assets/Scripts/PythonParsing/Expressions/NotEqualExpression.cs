public class NotEqualExpression : Expression{
	public NotEqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "NotEqual( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		return new BoolVal(!(((BoolVal)(new EqualExpression(left, right).Eval(env))).boolVal));
	}
}
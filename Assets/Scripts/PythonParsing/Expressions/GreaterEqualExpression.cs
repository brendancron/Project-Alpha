public class GreaterEqualExpression : Expression{
	public GreaterEqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "GreaterEqual( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		BoolVal gb = (BoolVal)(new GreaterExpression(left, right).Eval(env));
		BoolVal eb = (BoolVal)(new EqualExpression(left, right).Eval(env));
		return new BoolVal(gb.boolVal || eb.boolVal);
	}
}
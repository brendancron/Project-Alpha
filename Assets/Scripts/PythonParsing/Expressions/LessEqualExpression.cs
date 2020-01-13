public class LessEqualExpression : Expression{
	public LessEqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "LessEqual( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		BoolVal lb = (BoolVal)(new LessExpression(left, right).Eval(env));
		BoolVal eb = (BoolVal)(new EqualExpression(left, right).Eval(env));
		return new BoolVal(lb.boolVal || eb.boolVal);
	}
}
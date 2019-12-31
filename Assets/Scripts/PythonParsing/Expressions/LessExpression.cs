public class LessExpression : Expression{
	public LessExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Less( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		return null;
	}
}
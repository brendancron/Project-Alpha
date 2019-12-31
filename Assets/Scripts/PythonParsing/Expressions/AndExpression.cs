public class AndExpression : Expression{
	public AndExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "And( "+left+", "+right+" )";
	}

	public override Value Eval() {
		return null;
	}
}
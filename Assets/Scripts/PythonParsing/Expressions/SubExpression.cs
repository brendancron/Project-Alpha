public class SubExpression : Expression{
	public SubExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Sub( "+left+", "+right+" )";
	}

	public override Value Eval() {
		return null;
	}
}
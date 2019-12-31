public class GreaterExpression : Expression{
	public GreaterExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Greater( "+left+", "+right+" )";
	}

	public override Value Eval() {
		return null;
	}
}
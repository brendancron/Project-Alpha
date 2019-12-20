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
}
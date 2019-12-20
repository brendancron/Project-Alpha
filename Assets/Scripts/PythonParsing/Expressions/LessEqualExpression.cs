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
}
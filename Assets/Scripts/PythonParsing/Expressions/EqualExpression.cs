public class EqualExpression : Expression{
	public EqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Equal( "+left+", "+right+" )";
	}
}
public class OrExpression : Expression{
	public OrExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Or( "+left+", "+right+" )";
	}
}
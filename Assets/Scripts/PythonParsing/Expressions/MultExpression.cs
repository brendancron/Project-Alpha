public class MultExpression : Expression{
	public MultExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Mult( "+left+", "+right+" )";
	}
}
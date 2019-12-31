public class DotExpression : Expression{
	public DotExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Dot( "+left+", "+right+" )";
	}

	public override Value Eval() {
		return null;
	}
}
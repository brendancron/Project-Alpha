public class PowExpression : Expression{
	public PowExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Pow( "+left+", "+right+" )";
	}

	public override Value Eval() {
		return null;
	}
}
public class DivExpression : Expression{
	public DivExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Div( "+left+", "+right+" )";
	}

	public override Value Eval() {
		return null;
	}
}
public class AddExpression : Expression{
	public AddExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Add( "+left+", "+right+" )";
	}
}
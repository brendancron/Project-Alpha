public class IntExpression : Expression{
	public IntExpression(int val) {
		this.val = val;
	}
	private int val;

	public override string ToString() {
		return "Int( "+val+" )";
	}
}
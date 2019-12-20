public class BoolExpression : Expression {
	public BoolExpression(bool val) {
		this.val = val;
	}
	private bool val;

	public override string ToString() {
		return "Bool( "+val+" )";
	}
}
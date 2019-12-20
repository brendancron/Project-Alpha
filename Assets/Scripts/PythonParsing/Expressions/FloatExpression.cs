public class FloatExpression : Expression{
	public FloatExpression(float val) {
		this.val = val;
	}
	private float val;

	public override string ToString() {
		return "Float( "+val+" )";
	}
}

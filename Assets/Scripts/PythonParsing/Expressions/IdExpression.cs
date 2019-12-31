public class IDExpression : Expression{
	public IDExpression(string id) {
		this.id = id;
	}
	private string id;

	public override string ToString() {
		return "ID( "+id+" )";
	}

	public override Value Eval() {
		return null;
	}
}
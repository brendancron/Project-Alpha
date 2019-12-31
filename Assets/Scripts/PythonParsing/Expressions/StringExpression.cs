public class StringExpression : Expression {
	public StringExpression(string str) {
		this.str = str;
	}
	private string str;

	public override string ToString() {
		return "String( "+str+" )";
	}

	public override Value Eval(Environment env) {
		return new Value(str);
	}
}
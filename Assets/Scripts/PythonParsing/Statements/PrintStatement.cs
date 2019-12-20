public class PrintStatement : Statement{
	public PrintStatement(Expression expr) {
		this.expr = expr;
	}
	private Expression expr;

	public override string ToString() {
		return "Print( "+expr+" )";
	}
}
public class AssignStatement : Statement{
	public AssignStatement(Expression var, Expression val) {
		this.var = var;
		this.val = val;
	}
	private Expression var;
	private Expression val;

	public override string ToString() {
		return "Assign( "+var+", "+val+" )";
	}

	public override void Eval(Environment env){}
}
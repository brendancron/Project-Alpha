public class WhileStatement : Statement{
	public WhileStatement(Expression cond, Statement code) {
		this.cond = cond;
		this.code = code;
	}
	private Expression cond;
	private Statement code;

	public override string ToString() {
		return "While( "+cond+", "+code+" )";
	}

	public override void Eval(Environment env){}
}
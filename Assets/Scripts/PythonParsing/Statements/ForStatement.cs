public class ForStatement : Statement{
	public ForStatement(string id, Expression iter, Statement code) {
		this.id = id;
		this.iter = iter;
		this.code = code;
	}
	private string id;
	private Expression iter;
	private Statement code;

	public override string ToString() {
		return "For( "+id+", "+iter+", "+code+" )";
	}

	public override void Eval(Environment env){}
}
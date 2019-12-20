using System.Collections.Generic;

public class FunctionDeclarationStatement : Statement{
	public FunctionDeclarationStatement(string id, List<Expression> args, Statement code) {
		this.id = id;
		this.args = args;
		this.code = code;
	}
	private string id;
	private List<Expression> args;
	private Statement code;

	public override string ToString() {
		return "FunctionDecl( "+id+", "+args+", "+code+" )";
	}
}
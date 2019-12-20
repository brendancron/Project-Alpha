public class IfStatement : Statement{
	public IfStatement(Expression cond, Statement if_block, Statement else_block) {
		this.cond = cond;
		this.if_block = if_block;
		this.else_block = else_block;
	}
	private Expression cond;
	private Statement if_block;
	private Statement else_block;

	public override string ToString() {
		return "If( "+cond+", "+if_block+", "+else_block+" )";
	}
}
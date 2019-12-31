public class SeqStatement : Statement{
	public SeqStatement(Statement left, Statement right) {
		this.left = left;
		this.right = right;
	}
	private Statement left;
	private Statement right;

	public override string ToString() {
		return "Seq( "+left+", "+right+" )";
	}

	public override void Eval(Environment env){}
}
public class ClassStatement : Statement{
	public ClassStatement(string id, Statement block) {
		this.id = id;
		this.block = block;
	}
	private string id;
	private Statement block;

	public override string ToString() {
		return "Class( "+id+", "+block+" )";
	}
}
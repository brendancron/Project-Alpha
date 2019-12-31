public class DotExpression : Expression {
	public DotExpression(Expression left, Exxpression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Dot( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		lv = left.Eval(env);
		switch(lv.valType) {
			case Value.ValType.ObjectType:
				return rv.Eval(rv.obj_val.env);
			case Value.ValType.ClassType:
				return rv.Eval(rv.class_val.env);
			default:
				throw new VariableDoesNotExistException("Dot Operations must be used on an object or class!");
		}
	}

	public override Tuple<Environment, string> GetEnvironment(Environment env) {
		lv = left.Eval(env);
		switch(lv.valType) {
			case Value.ValType.ObjectType:
				return rv.GetEnvironment(rv.obj_val.env);
			case Value.ValType.ClassType:
				return rv.GetEnvironment(rv.class_val.env);
			default:
				throw new VariableDoesNotExistException("Dot Operations must be used on an object or class!");
		}
	}
}
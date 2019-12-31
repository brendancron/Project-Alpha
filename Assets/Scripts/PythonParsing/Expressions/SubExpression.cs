public class SubExpression : Expression{
	public SubExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Sub( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(lv.valType == Value.ValType.Float && rv.valType == Value.ValType.Float) {
			return new Value(lv.float_val - rv.float_val);
		}
		throw new TypeMismatchException("must both be numbers!");
	}
}
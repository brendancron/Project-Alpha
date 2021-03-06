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
		if(typeof(IntVal).IsInstanceOfType(lv)) {
			if(typeof(IntVal).IsInstanceOfType(rv)) {
				return new IntVal(((IntVal)(lv)).intVal - ((IntVal)(rv)).intVal);
			}
			lv = new FloatVal((IntVal)lv);
		}
		if(typeof(FloatVal).IsInstanceOfType(lv)) {
			if(typeof(IntVal).IsInstanceOfType(rv)) {
				rv = new FloatVal((IntVal)rv);
			}
			if(typeof(FloatVal).IsInstanceOfType(rv)) {
				return new FloatVal(((FloatVal)(lv)).floatVal - ((FloatVal)(rv)).floatVal);
			}
		}
		throw new TypeMismatchException("must both be numbers!");
	}
}
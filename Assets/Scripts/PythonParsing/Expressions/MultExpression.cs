public class MultExpression : Expression{
	public MultExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Mult( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(typeof(IntVal).IsInstanceOfType(lv)) {
			if(typeof(IntVal).IsInstanceOfType(rv)) {
				int li = ((IntVal)(lv)).intVal;
				int ri = ((IntVal)(rv)).intVal;
				return new IntVal(li*ri);
			} else {
				lv = new FloatVal((IntVal)lv);
			}
		}
		if(typeof(FloatVal).IsInstanceOfType(lv) && typeof(FloatVal).IsInstanceOfType(rv)) {
			float lf = ((FloatVal)lv).floatVal;
			float rf = ((FloatVal)rv).floatVal;
			return new FloatVal(lf*rf);
		} else {
			throw new TypeMismatchException("must both be numbers!");
		}
	}
}
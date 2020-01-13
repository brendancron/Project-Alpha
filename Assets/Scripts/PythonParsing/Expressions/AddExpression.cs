public class AddExpression : Expression{
	public AddExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Add( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(typeof(StringVal).IsInstanceOfType(lv)) {
			if(typeof(StringVal).IsInstanceOfType(rv)) {
				return new StringVal(((StringVal)(lv)).stringVal + ((StringVal)(rv)).stringVal);
			}
		} else if(typeof(IntVal).IsInstanceOfType(lv)) {
			if(typeof(IntVal).IsInstanceOfType(rv)) {
				return new IntVal(((IntVal)(lv)).intVal + ((IntVal)(rv)).intVal);
			}
			lv = new FloatVal((IntVal)lv);
		}
		if(typeof(FloatVal).IsInstanceOfType(lv)) {
			if(typeof(IntVal).IsInstanceOfType(rv)) {
				rv = new FloatVal((IntVal)rv);
			}
			if(typeof(FloatVal).IsInstanceOfType(rv)) {
				return new FloatVal(((FloatVal)(lv)).floatVal + ((FloatVal)(rv)).floatVal);
			}
		}
		throw new TypeMismatchException("types cannot be added!");
	}
}
public class DivExpression : Expression{
	public DivExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Div( "+left+", "+right+" )";
	}

	public override Value Eval() {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(lv.valType == Value.ValType.Float && rv.valType == Value.ValType.Float) {
			if(rv.float_val == 0) {
				throw new DivideByZeroException("cannot divide by zero!");
			}
			return new Value(lv.float_val/rv.float_val);
		}
		throw new TypeMismatchException("must both be numbers!");
	}

	public class DivideByZeroException : Exception {
		public DivideByZeroException(string message) : base(message) {}
	}
}
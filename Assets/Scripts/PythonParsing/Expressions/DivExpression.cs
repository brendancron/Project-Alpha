using System;

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

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(typeof(IntVal).IsInstanceOfType(lv)) {
			lv = new FloatVal((IntVal)lv);
		}
		if(typeof(IntVal).IsInstanceOfType(rv)) {
			rv = new FloatVal((IntVal)rv);
		}
		if(typeof(FloatVal).IsInstanceOfType(lv) && typeof(FloatVal).IsInstanceOfType(rv)) {
			float lf = ((FloatVal)lv).floatVal;
			float rf = ((FloatVal)rv).floatVal;
			if(rf == 0) {
				throw new DivideByZeroException("cannot divide by zero!");
			}
			return new FloatVal(lf/rf);
		} else {
			throw new TypeMismatchException("must both be numbers!");
		}
	}

	public class DivideByZeroException : Exception {
		public DivideByZeroException(string message) : base(message) {}
	}
}
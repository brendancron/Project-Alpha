using System;

public class PowExpression : Expression{
	public PowExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Pow( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(typeof(NumVal).IsInstanceOfType(lv) && typeof(NumVal).IsInstanceOfType(rv)) {
			FloatVal f1 = new FloatVal((NumVal)lv);
			FloatVal f2 = new FloatVal((NumVal)rv);
			return new FloatVal((float)(Math.Pow(f1.floatVal, f2.floatVal)));
		}
		throw new TypeMismatchException("must both be numbers!");
	}
}
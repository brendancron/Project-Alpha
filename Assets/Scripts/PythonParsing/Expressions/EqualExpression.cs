public class EqualExpression : Expression{
	public EqualExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Equal( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(typeof(StringVal).IsInstanceOfType(lv) && typeof(StringVal).IsInstanceOfType(rv)) {
			return new BoolVal(((StringVal)lv).stringVal == ((StringVal)rv).stringVal);
		} else if(typeof(NumVal).IsInstanceOfType(lv) && typeof(NumVal).IsInstanceOfType(rv)) {
			return new BoolVal(((NumVal)lv).Compare((NumVal)rv) == 0);
		} else if(typeof(BoolVal).IsInstanceOfType(lv) && typeof(BoolVal).IsInstanceOfType(rv)) {
			return new BoolVal(((BoolVal)lv).boolVal == ((BoolVal)rv).boolVal);
		} else {
			//might need more cases!!!!
			return new BoolVal(lv == rv);
		}
	}
}
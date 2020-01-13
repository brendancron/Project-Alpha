public class LessExpression : Expression{
	public LessExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Less( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(typeof(StringVal).IsInstanceOfType(lv)) {
			if(typeof(StringVal).IsInstanceOfType(rv)) {
				return new BoolVal(string.Compare(((StringVal)(lv)).stringVal , ((StringVal)(rv)).stringVal) < 0);
			}
			throw new TypeMismatchException("must both be strings");
		} else if(typeof(NumVal).IsInstanceOfType(lv)) {
			if(typeof(NumVal).IsInstanceOfType(rv)) {
				return new BoolVal(((NumVal)lv).Compare((NumVal)rv) > 0);
			}
		}
		throw new TypeMismatchException("Invalid comparison");
	}
}
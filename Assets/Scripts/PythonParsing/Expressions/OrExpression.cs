public class OrExpression : Expression{
	public OrExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "Or( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(typeof(BoolVal).IsInstanceOfType(lv)) {
			if(((BoolVal)(lv)).boolVal) {
				return new BoolVal(true);
			} else {
				return rv;
			}
		} else {
			return lv;
		}
	}
}
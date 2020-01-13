public class AndExpression : Expression{
	public AndExpression(Expression left, Expression right) {
		this.left = left;
		this.right = right;
	}
	private Expression left;
	private Expression right;

	public override string ToString() {
		return "And( "+left+", "+right+" )";
	}

	public override Value Eval(Environment env) {
		Value lv = left.Eval(env);
		Value rv = right.Eval(env);
		if(typeof(BoolVal).IsInstanceOfType(lv)) {
			if(((BoolVal)(lv)).boolVal) {
				return rv;
			} else {
				return new BoolVal(false);
			}
		} else {
			return rv;
		}
	}
}
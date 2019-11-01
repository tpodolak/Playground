object Main extends App {
  var rational = new Rational(1, 2) + new Rational(2, 3)
  println(rational)
}

class Rational(n: Int, d: Int) {
  require(d != 0)

  private val g = gcd(n.abs, d.abs)
  val numer: Int = n / g
  val denom: Int = d / g

  override def toString = s"$n/$d"

  def + (right: Rational): Rational =
      new Rational(
        numer * right.denom + right.numer * denom,
        denom * right.denom )

  def + (right: Int): Rational =
    new Rational(numer + right * denom, denom)

  def - (right: Rational): Rational =
    new Rational(
      numer * right.denom - right.numer * denom,
      denom * right.denom )

  def - (right: Int): Rational =
    new Rational(
      numer - right * denom, denom)

  def * (right: Rational): Rational =
    new Rational(numer * right.numer, denom * right.denom)

  def * (right: Int): Rational =
    new Rational(numer * right, denom)

  def / (right: Rational): Rational =
    new Rational(numer * right.denom, denom * right.numer)

  def / (right: Int): Rational =
    new Rational(right, right * denom)

  def gcd(a: Int, b: Int): Int = {
    if( b == 0) a else gcd(b, a % b)
  }
}
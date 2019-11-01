object Monads extends App {

  trait Attempt[+A] {
    // bind
    def flatMap[B](f: A => Attempt[B]): Attempt[B]

  }

  object Attempt{
    // unit
    def apply[A](a: => A): Attempt[A] = {
      try{
        Success(a)
      }catch {
        case e: Throwable => Fail(e)
      }
    }
  }

  case class Success[+A](value: A) extends Attempt[A] {
    override def flatMap[B](f: A => Attempt[B]): Attempt[B] = {
      try{
        f(value)
      }catch {
        case e: Throwable => Fail(e)
      }
    }
  }

  case class Fail(e: Throwable) extends Attempt[Nothing] {
    override def flatMap[B](f: Nothing => Attempt[B]): Attempt[B] = {
      this
    }
  }

  /*
  left identity
  unit.bind(f) => f(x)
   */
  val f:Int => Attempt[Int] = x => Success(x + 1)
  val g:Int => Attempt[Int] = x => Success(x + 2)
  val x = 1
  println(Success(x).flatMap(f) == f(x))
  /*
  right identity
  unit.bind(unit) == unit
   */
  println(Success(x).flatMap(Success(_)) == Success(x))

  // associativity
  println(Success(x).flatMap(f).flatMap(g) == Success(x).flatMap(x => f(x).flatMap(g)))
}
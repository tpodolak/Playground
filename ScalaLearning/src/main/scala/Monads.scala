object Monads extends App {

  trait Attempt[+A] {
    // bind
    def flatMap[B](f: A => Attempt[B]): Attempt[B]

  }

  object Attempt {
    // unit
    def apply[A](a: => A): Attempt[A] = {
      try {
        Success(a)
      } catch {
        case e: Throwable => Fail(e)
      }
    }
  }

  case class Success[+A](value: A) extends Attempt[A] {
    override def flatMap[B](f: A => Attempt[B]): Attempt[B] = {
      try {
        f(value)
      } catch {
        case e: Throwable => Fail(e)
      }
    }
  }

  case class Fail(e: Throwable) extends Attempt[Nothing] {
    override def flatMap[B](f: Nothing => Attempt[B]): Attempt[B] = {
      this
    }
  }
  
  class Lazy[T](value: => T){
    private lazy val internalValue = value
    def use:T = internalValue
    def flatMap[B](f: ( => T) => Lazy[B]) : Lazy[B] =
      f(internalValue)
  }

  object Lazy{
    def apply[T](input: => T): Lazy[T] = new Lazy[T](input)
  }
  
  class Monad[T] {
    def flatMap[B](f: T => Monad[B]): Monad[B] = ???
    
    def map[B](f: T => B): Monad[B] = flatMap(x => Monad(f(x)))
    
    def flatten(f: Monad[Monad[T]]) : Monad[T] =  f.flatMap(x => x)
  }
  
  object Monad{
    def apply[T](input: T) : Monad[T] = ???
  }
  
  var lazyInstance = Lazy{
    println("called")
    1
  }
  
  lazyInstance.flatMap(x => Lazy(x))
  
  lazyInstance.use
  
  /*
  left identity
  unit.bind(f) => f(x)
   */
  val f: Int => Attempt[Int] = x => Success(x + 1)
  val g: Int => Attempt[Int] = x => Success(x + 2)
  val x = 1
  println(Success(x).flatMap(f) == f(x))
  /*
  right identity
  unit.bind(unit) == unit
   */
  println(Success(x).flatMap(Success(_)) == Success(x))

  // associativity
  println(Success(x).flatMap(f).flatMap(g) == Success(x).flatMap(x => f(x).flatMap(g)))
  
  val list = List(1, 2, 3, 4)
  val mappedList = list.map(_*2)
  println(mappedList)
  
  var flatMappedList = list.flatMap(x => List(x, x + 1, x + 2))
  println(flatMappedList)
}
object HigherKindedTypes extends App {

  // kind of open generics a bit extended
  
  trait HigherKindedType[F[_]]
  
  /*
  trait MyList[T] {
    def map[B](f: T => B): MyList[B]
  }
  
  trait MyOption[T] {
    def map[B](f: T => B): MyOption[B]
  }
   */
  // instead of repeating above we can use
  
  trait Map[T, F[_]]{
    def map[B](f: T => B): F[B]
  }

  case class SomeOption[T](val value: T) extends Map[T, SomeOption] {
    override def map[B](f: T => B): SomeOption[B] = new SomeOption[B](f(value))
  }
  
  val x: SomeOption[Int] = new SomeOption(1)
  val value: SomeOption[String] = x.map(x => s"somevalue$x".toString)
  
  println(x)
  println(value)
  
  // flatmap
  trait Monad[F[_], A]{
    def flatMap[B](f:A => F[B]) : F[B]
    def map[B](f:A => B): F[B]
  }
  
  implicit class MonadList[A](list: List[A]) extends Monad[List, A] {
    override def flatMap[B](f: A => List[B]): List[B] = list.flatMap(f)

    override def map[B](f: A => B): List[B] = list.map(f)
  }
  
  def multiply[F[_], A, B](implicit ma: Monad[F, A], mb: Monad[F, B]) : F[(A, B)] = {
    for {
      a <- ma
      b <- mb
    } yield (a,b)
    
    // ma.flatMap(a => mb.map(b => ))
  }
  
  val tuples: List[(Int, Int)] = multiply(new MonadList(List(1,2,3)), new MonadList(List(1,2)))
  val mixedTuples = multiply(new MonadList(List("str","str2")), new MonadList(List(1,2 )))
  val implicits = multiply(List("str","str"), List(1,2))
  println(tuples)
  println(mixedTuples)
  println(implicits)
}

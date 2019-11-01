import scala.annotation.tailrec

object LazyEvaluation extends App {

  def byName(x: => Int) = x + x + x +1
  def byNameNeed(x: => Int) = {
    lazy val t = x
    t + t + t + 1
  }
  def retrieveMagicValue = {
    // long computation
    Thread.sleep(1000)
    println("waiting")
    42
  }

  def retrieveMagicValue2() = {
    // long computation
    Thread.sleep(1000)
    println("waiting")
    42
  }

  // byName(retrieveMagicValue)
  byName(retrieveMagicValue2())
  // byNameNeed(retrieveMagicValue)

  abstract class MyStream[+A] {
    def isEmpty: Boolean
    def head: A
    def tail: MyStream[A]

    // prepend
    def #::[B >: A](element: B) : MyStream[B] // prepend
    def ++[B >: A](anotherStream: => MyStream[B]) : MyStream[B]

    def foreach(f: A => Unit) : Unit
    def map[B](f: A => B): MyStream[B]
    def flatMap[B](f: A => MyStream[B]) : MyStream[B]
    def filter(predicate: A => Boolean): MyStream[A]

    def take(n: Int) : MyStream[A]
    def takeAsList(n: Int) : List[A]

    @tailrec
    final def toList[B >: A](acc: List[B] = Nil):List[B] =
      if(isEmpty)
        acc
    else
        tail toList(head :: acc)
  }

  class LazyStream[A](lazyHead: => A, generator: A => A) extends MyStream[A]{
    lazy val lazyTail = new LazyStream[A](generator(lazyHead), generator)
    override def isEmpty: Boolean = false

    override def #::[B >: A](element: B): MyStream[B] = ???

    override def ++[B >: A](anotherStream: => MyStream[B]): MyStream[B] = ???

    override def foreach(f: A => Unit): Unit = ???

    override def map[B](f: A => B): MyStream[B] = ???

    override def flatMap[B](f: A => MyStream[B]): MyStream[B] = ???

    override def filter(predicate: A => Boolean): MyStream[A] = ???

    override def take(n: Int): MyStream[A] = ???

    override def takeAsList(n: Int): List[A] = ???

    override def head: A = lazyHead

    override def tail: MyStream[A] = lazyTail
  }

  class InfiniteStream[A](override val head: A, generator: A => A) extends MyStream[A]{

    lazy val lazyTail = new InfiniteStream[A](generator(head), generator)

    override def isEmpty: Boolean = false

    override def #::[B >: A](element: B): MyStream[B] = ???

    override def ++[B >: A](anotherStream: => MyStream[B]): MyStream[B] = ???

    override def foreach(f: A => Unit): Unit = {
      f(head)
      tail foreach f
    }

    override def map[B](f: A => B): MyStream[B] = tail.map(f)

    override def flatMap[B](f: A => MyStream[B]): MyStream[B] = ???

    override def filter(predicate: A => Boolean): MyStream[A] = ???

    override def take(n: Int): MyStream[A] = ???

    override def takeAsList(n: Int): List[A] = ???

    override def tail: MyStream[A] = lazyTail
  }

  object EmptyStream extends MyStream[Nothing] {
    override def isEmpty: Boolean = true

    override def head: Nothing = throw new NoSuchElementException

    override def tail: MyStream[Nothing] = throw new NoSuchElementException

    override def #::[B >: Nothing](element: B): MyStream[B] = new Cons[B](element, this)

    override def ++[B >: Nothing](anotherStream: => MyStream[B]): MyStream[B] = anotherStream

    override def foreach(f: Nothing => Unit): Unit = ()

    override def map[B](f: Nothing => B): MyStream[B] = this;

    override def flatMap[B](f: Nothing => MyStream[B]): MyStream[B] = this

    override def filter(predicate: Nothing => Boolean): MyStream[Nothing] = this

    override def take(n: Int): MyStream[Nothing] = this

    override def takeAsList(n: Int): List[Nothing] = Nil
  }

  class Cons[+A](hd: A, tl: => MyStream[A]) extends MyStream[A] {
    override def isEmpty: Boolean = false

    override val head: A = hd

    override lazy val tail: MyStream[A] = tl

    override def #::[B >: A](element: B): MyStream[B] = new Cons(element, this)

    override def ++[B >: A](anotherStream: => MyStream[B]): MyStream[B] = new Cons[B](head, tail ++ anotherStream)

    override def foreach(f: A => Unit): Unit = {
      f(head)
      tail foreach f
    }

    override def map[B](f: A => B): MyStream[B] = new Cons[B](f(head), tail map f)

    override def flatMap[B](f: A => MyStream[B]): MyStream[B] = f(head) ++ tail.flatMap(f)

    override def filter(predicate: A => Boolean): MyStream[A] = {
      if(predicate(head)){
        new Cons[A](head, tail filter predicate)
      }
      else tail filter predicate
    }

    override def take(n: Int): MyStream[A] =
      if(n <= 0) EmptyStream
    else if (n == 1) new Cons[A](head, EmptyStream)
    else new Cons[A](head, tail.take(n - 1))


    override def takeAsList(n: Int): List[A] = take(n).toList()
  }

  object MyStream{
    def from[A](start: A)(generator: A => A): MyStream[A] = new Cons[A](start, MyStream.from(generator(start))(generator))

  }

  MyStream.from(1)(x => x + 1) take 1 foreach println

}
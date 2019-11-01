import scala.annotation.tailrec

trait MySet[A] extends (A => Boolean){
  def contains(elem : A) : Boolean
  def +(elem: A) : MySet[A]
  def ++(anotherSet: MySet[A]) : MySet[A] // union
  def map[B](f: A => B) : MySet[B]
  def flatMap[B](f: A => MySet[B]) : MySet[B]
  def filter(f: A => Boolean) : MySet[A]
  def foreach(f: A => Unit) : Unit
  def -(elem: A) : MySet[A] // remove
  def &(otherSet: MySet[A]): MySet[A] // intersection
  def --(otherSet: MySet[A]) : MySet[A] // difference
  def apply(elem: A): Boolean = contains(elem)
  def unary_!  : MySet[A]
}

class EmptySet[A] extends MySet[A] {
  override def contains(elem: A): Boolean = false

  override def +(elem: A): MySet[A] = new NonEmptySet[A](elem, this)

  override def ++(anotherSet: MySet[A]): MySet[A] = anotherSet

  override def map[B](f: A => B): MySet[B] = new EmptySet[B]

  override def flatMap[B](f: A => MySet[B]): MySet[B] = new EmptySet[B]

  override def filter(f: A => Boolean): MySet[A] = this

  override def foreach(f: A => Unit): Unit = ()

  override def -(elem: A): MySet[A] = this

  override def &(otherSet: MySet[A]): MySet[A] = this

  override def --(otherSet: MySet[A]): MySet[A] = this

  override def unary_! : MySet[A] = new PropertyBasedSet[A](_ => true)
}

// all elements of type A which satisfy property
class PropertyBasedSet[A](property: A => Boolean) extends MySet[A]{
  override def contains(elem: A): Boolean = property(elem)

  override def +(elem: A): MySet[A] =
    new PropertyBasedSet[A](x => property(x) || elem == x)

  override def ++(anotherSet: MySet[A]): MySet[A] =
    new PropertyBasedSet[A](x => property(x) || anotherSet.contains(x))

  override def map[B](f: A => B): MySet[B] = politelyFail

  override def flatMap[B](f: A => MySet[B]): MySet[B] = politelyFail

  override def filter(f: A => Boolean): MySet[A] = new PropertyBasedSet[A](x => property(x) && f(x))

  override def foreach(f: A => Unit): Unit = politelyFail

  override def -(elem: A): MySet[A] = filter(x => x != elem)

  override def &(otherSet: MySet[A]): MySet[A] = filter(otherSet)

  override def --(otherSet: MySet[A]): MySet[A] = filter(!otherSet)

  override def unary_! : MySet[A] = new PropertyBasedSet[A](x => !property(x))

  def politelyFail = throw new IllegalArgumentException("Really deep rabbit hole !")
}

class NonEmptySet[A](val head: A, val tail: MySet[A]) extends MySet[A] {
  override def contains(elem: A): Boolean =
    head == elem || tail.contains(elem)

  override def +(elem: A): MySet[A] =
    if(this contains elem) this
    else new NonEmptySet[A](elem, this)

  override def ++(anotherSet: MySet[A]): MySet[A] =
    tail ++ anotherSet + head

  override def map[B](f: A => B): MySet[B] = (tail map f) + f(head)

  override def flatMap[B](f: A => MySet[B]): MySet[B] = (tail flatMap f) ++ f(head)

  // [1, 2, 3] x > 2
  // [2 , 3] 1
  // [3] 2, 1
  // [] 3, 2, 1
  // [3] 2, 1
  // [3] 1
  // [3]
  override def filter(f: A => Boolean): MySet[A] = {
    val filteredTail = tail filter f
    if(f(head)) filteredTail + head
    else filteredTail
  }

  override def foreach(f: A => Unit): Unit = {
    f(head)
    tail foreach f
  }

  override def -(elem: A): MySet[A] =
    this.filter(x => x != elem)

  override def &(otherSet: MySet[A]): MySet[A] =
    filter(otherSet)

  override def --(otherSet: MySet[A]): MySet[A] =
    filter(!otherSet)

  override def unary_! : MySet[A] = new PropertyBasedSet[A](x => !this.contains(x))
}

object MySet{
  def apply[A](args: A*): MySet[A] = {
    // buildSet(seq(1, 2, 3), [])
    // buildSet(seq(2, 3), [] + 1)
    // buildSet(seq(3), [1] + 2)
    // buildSet(seq(), [1 ,2] + 3)
    // [1 ,2 ,3]
    @tailrec
    def buildSet(valueSequence: Seq[A], acc: MySet[A]): MySet[A] = {
      if(valueSequence.isEmpty) acc
      else
        buildSet(valueSequence.tail, acc + valueSequence.head)
      // new NonEmptySet[A](valueSequence.head, buildSet(valueSequence.tail, acc))
    }

    buildSet(args.toSeq, new EmptySet[A])
  }
}

object MySetApp extends App{
  val mySet = MySet(1, 2, 3)

  mySet foreach println

  mySet - 1 foreach println
  println
  MySet(1,2,3) & MySet(2, 3) foreach println
  println
  MySet(1,2,3) -- MySet(2,3) foreach println

  val s = MySet(1,2,3,4)

  val negative  = !s
  println(negative(2))
  println(negative(5))
}

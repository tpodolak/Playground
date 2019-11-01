object PatternMatchingMore extends App {
  case class Or[A, B](a: A, b: B)

  case class :::[A](a:A, b:A)

  var either = Or(2, "two")

  var pair= :::(1,1)

  pair match {
    case :::(1,1) => println("infix matched")
  }


  pair match {
    case 1 ::: 1  => println("infix matched other syntax")
  }

  either match {
    case Or(a, b) => println(s"$a or $b")
  }

  // // infix pattern same as above but different syntax
  either match {
    case a Or b => println(s"$a or $b from infix pattern")
  }

  // pattern match sequences

  abstract class MyList[+A]{
    def head: A = ???
    def tail: MyList[A] = ???
  }

  case object Empty extends MyList[Nothing]
  case class Cons[+A](override val head:A , override val tail: MyList[A]) extends MyList[A]

  object MyList{

    def unapplySeq[A](list: MyList[A]): Option[Seq[A]]  =
      if(list == Empty){
        println("Empty")
        Some(Seq.empty)
      }
      else unapplySeq(list.tail).map(list.head +: _)
  }

  val x= Cons(1, Cons(2, Cons(3, Empty)))

  x match {
    case MyList(1, 2, 3) => println("first")
  }

 abstract class ObjectWrapper[T]{
    def isEmpty: Boolean
    def get: T
  }

  object PersonWrapper{
    def unapply(arg: Person): ObjectWrapper[(String, Int)] = new ObjectWrapper[(String, Int)] {

      override def isEmpty: Boolean = false

      override def get: (String, Int) = (arg.name, arg.age)
    }
  }

  (new Person("John", age = 25) :: new Person("John", age = 26) :: Nil).foreach({
    x =>
      x match {
        case a @ PersonWrapper("John", 26) => println(s"John string ${a.age}")
        case PersonWrapper(_, _) => println("other")
      }
  })
}

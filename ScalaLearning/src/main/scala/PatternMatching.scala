class Person(val name: String, val age: Int)

object PatternMatching extends App {

  val lists = List(List(3), List(2,3), List(2, 3, 4))
  var list = lists.foreach{ inner =>

    var single = inner match {
      case head :: Nil => println("single")
      case head :: (s @ other) :: Nil => println(s"exactly two ${s}")
      case head ::other => println("two or more")
    }
  }

  println("other syntax")
  lists.foreach(inner => {

    inner match {
      case List(_) => println("single")
      case List(_, _) => println("exactly two")
      case List(_, _, _* ) => println("two or more")
      case _ => println("other")
    }
  })

  println("Checking exact item on index")

  lists.head match {
    case List(2) => print("first element equals two")
    case list @ List(3) => print(s"first element equals three ${list}")
  }

  class Person(val name:String, val age:Int)

  object Person {

    def unapply(arg: Person): Option[(String, Int)] = Some((arg.name, arg.age))
  }

  //name doesnt have to be the same as class name
  object PersonMatcher{
    def unapply(arg: Person): Option[(String, Int)] = Some((arg.name, arg.age))

    def unapply(arg: Int): Option[String] = Some(if (arg > 10) "above" else "below" )
  }

  object even{
    // can return bool instead of option[]
    def unapply(arg: Int): Boolean = arg % 2 == 0
  }

  println("Pattern matching custom")
  List(new Person("John", 25), new Person("John", 26)).foreach(person => {
    person match {
      case PersonMatcher("John", 25) => println("matched person with custom matcher")
      case Person("John", 25) => println("matched person")
      case Person(_, _) => println("other person")
      case _ => println("not matched")
    }

    person.age match{
      case even() => println("eveb")
      case PersonMatcher(x) => println(s"From not the same type $x")
    }
  })

  object GreaterThanTenMatcher{
    def unapply(arg: Int): Option[Int] = {
      if(arg > 10) Some(arg)
      else None
    }
  }

  List(1,2, 10, 11, 12).foreach(elem => {

    elem match {
      case x if x > 2 && x < 11 => println("first range")
      case GreaterThanTenMatcher(12) => println("12 and greater than")
      case GreaterThanTenMatcher(x) => println(s"$x is greater than 10")
      case _ => println("not")
    }

  })
}



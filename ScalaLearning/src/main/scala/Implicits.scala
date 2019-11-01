object Implicits extends App {

  "abc" -> "cde" // ArrowAssoc

  case class Person(name: String) {
    def greet = s"Hi my name is $name"
  }
  
  implicit def fromStringToPerson(name: String) = Person(name)
  
  def increment(int: Int)(implicit amount: Int) = amount + int
  implicit val defaultAmount = 10
  println(increment(1))
  println("Peter".greet)
}


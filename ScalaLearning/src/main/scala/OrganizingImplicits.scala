object OrganizingImplicits extends App  {

  implicit val reverseOrdering: Ordering[Int] = Ordering.fromLessThan(_ > _)
  
  println(List(1,4,5,3,2).sorted)
  // scala.Predef
  
  /*
  Implicits:
  - val/var
  - objects
  - accessor methods - def with no parentheses
   */

  case class Person(name: String, age: Int)
  
  implicit def personOrdering = Ordering.fromLessThan[Person]((left,right) => left.age > right.age )
  
  val person = List(
    Person("first", 1),
    Person("second", 10),
    Person("third", 20)
  )
  
  println(person.sorted)
  
  /*
  Implicit scope
  - normal scope = LOCAL
  - imported scope
  - companions of all types involved in method signature
    - List
    - Ordering
    - all the types involved - A or any supertype
   */
  //   def sorted[B >: A](implicit ord: Ordering[B]): List = {
} 

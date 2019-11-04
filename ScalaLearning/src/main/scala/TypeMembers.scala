object TypeMembers extends App {

  class Animal
  class Dog extends Animal
  class Cat extends Animal
  
  trait X{
    this: Cat =>
  }
  
  class AnimalCollection{
    type AnimalType // abstract type member
    type BoundedAnimal <: Animal
    type SuperBoundedAnimal >: Dog <: Animal
    type AnimalC = Cat // type alias
  }
  
  val ac = new AnimalCollection
  /*

val dog: ac.AnimalType = ??? 

val cat: ac.BoundedAnimal = new Cat
 */
  val pup: ac.SuperBoundedAnimal = new Dog
  val cat: ac.AnimalC  = new Cat
  trait MyList{
    type T
    def add(element: T): MyList
  }
  
  class NonEmptyList(value: Int) extends MyList {
    override type T = Int
    def add(element:  Int): MyList = ???
  }
  
//  // .type 
//  type CatsType = cat.type 
//  val newCat: CatsType = cat
  
  trait MList{
    type A
    def head: A
    def tail: MList
  }
  
  class CustomList(hd: String, tl: CustomList) extends MList {
    override type A = String
    
    override def head: String = ???

    override def tail: MList = ???
  }

  class IntList(hd: Int, tl: CustomList) extends MList {
    override type A = Int

    override def head: Int = ???

    override def tail: MList = ???
  }
  
  trait ApplicableToNumbers extends MList{
    override type A <: Number
  }
}

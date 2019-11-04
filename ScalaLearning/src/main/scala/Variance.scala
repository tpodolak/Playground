object Variance extends App {

  trait Animal
  class Dog extends Animal
  class Cat extends Animal
  class Kitty extends Cat
  class Crocodile extends Animal
  
  class Cage[T] // invariant
  class CCage[+T] // covariant
  val ccage: CCage[Animal] = new CCage[Cat]
  
  // contrvariance - opposite ov variance
  class XCage[-T]
  val xcage: XCage[Cat] = new XCage[Animal]
  
  class InvariantCage[T](animal: T)
  
  // covariant positions
  
  class CovariantCage[+T](val animal: T) // covariant position
  
  // class ContravariantCage[-T](val animal: T) // NOT COMPILE
  
  // class CovariantVariableCage[+T](var animal: T) // NOT COMPILE types of vars are in contravariant position

//   class AnotherVariableCage[+T]{
//     def addAnimal(animal: T) = { // NOT COMPILE types of vars are in contravariant position
//       
//     }
//   } 

  class MyList[+A]{
    // B must be supertype of A
    def add[B >: A](element: B): MyList[B] = { // widening the type
      new MyList[B]
    }
  }
  
  val emptySet = new MyList[Kitty]
  val newList = emptySet.add(new Animal {})
  
  // METHOD ARGUMENTS ARE IN CONTRAVARIANT POSITION
  
  /*
  class PetShop[-T]{
    def get(isItPuppy: Boolean) : T // METHOD RETURN TYPES ARE COVARIANT POSITION
  }
   */
  
  
}

object FBoundedPolymorphism extends App  {

  trait Animal {
    type animalType <: Animal
    def breed: List[animalType]
  }
  
  trait AnimalGeneric[A <: AnimalGeneric[A]]{ // recursive type: F-Bounded polymorphism
    def breed: List[AnimalGeneric[A]]
  }
  
  class SomeAnimal extends AnimalGeneric[SomeAnimal]{
    override def breed: List[AnimalGeneric[SomeAnimal]] = ???
  }
  
  class Cat extends Animal{
    override type animalType = Cat
    override def breed: List[Cat] = List[Cat]()
  }
  
  // enforcing class to be used in FBP
  trait AnimalSelf[A <: AnimalSelf[A]]{
    this: A => 
    def breed: List[AnimalSelf[A]]
  }
  
  class AnotherCat extends AnimalSelf[AnotherCat]{
    override def breed: List[AnimalSelf[AnotherCat]] = ???
  }
  
  // but still can bypass type safety with inheritance
  class PersianCat extends AnotherCat{
    override def breed: List[AnimalSelf[AnotherCat]] = List(new AnotherCat) // different cat returned
  }
  
  trait AnimalAnother
  trait CanBreed[A]{
    def breed(a: A) : List[A]
  }
  
  class Dog extends AnimalAnother
  object Dog{
    implicit object DogCanBreed extends CanBreed[Dog] {
      override def breed(a: Dog): List[Dog] = List[Dog]()
    }
  }
  
  implicit class CanBreedOps[A](animal: A){
    def breed(implicit canBreed: CanBreed[A]) : List[A] = canBreed.breed(animal)
  }
  
  val dog = new Dog
  dog.breed // implicit conversion
  
  // if I do it incorrectly it wont compile
  class SomeCat
  object SomeCat{
    implicit object CatCanBreed extends CanBreed[Dog] {
      override def breed(a: Dog): List[Dog] = List()
    }
  }
  
  /*
  val cat = new SomeCat
  cat.breed // wont compile as there is no implicits which take SomeCat as type parameter
   */
}

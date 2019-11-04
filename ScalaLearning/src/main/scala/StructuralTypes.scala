object StructuralTypes extends App {

  // structural types
  type JavaClosable = java.io.Closeable
  
  class HipsterClosable{
    def close(): Unit = println("closing from hipster closable")
    def closeSilently(): Unit = println("not making a sound")
  }
  
  // like shapes(which will most probably be available in c#9) in c#
  type UnifiedClosable = {
    def close() : Unit // STRUCTURAL TYPE
  }

  def closeQuietly(closable: UnifiedClosable) = closable.close()
  
  closeQuietly(new JavaClosable {
    override def close(): Unit = println("close from java console")
  })
  
  closeQuietly(new HipsterClosable)

  // TYPE REFINEMENTS - enriched JAVA closable
  type AdvancedClosable = JavaClosable{
    def closeSilently(): Unit {
    
    }
  }
  
  class AdvancedJavaClosable extends JavaClosable{
    override def close(): Unit = println("Java closes")
    def closeSilently(): Unit = println("Java closes silently")
  }
  
  def closeShh(advClosable: AdvancedClosable): Unit = advClosable.closeSilently()
  
  closeShh(new AdvancedJavaClosable)
  // closeShh(new HipsterClosable) // wont compile as HipsterClosable doesnt extend JavaClosable
  
  // using structural types as standalone types
  def altClose(closable: {def close(): Unit }) : Unit = closable.close()
  
  altClose(new AdvancedJavaClosable)
  altClose(new JavaClosable{
    override def close(): Unit = println("java closable abstract")
  })
  
  // type-checking => duck typing
  
  type SoundMaker = {
    def makeSound(): Unit
  }
  
  class Dog{
    def makeSound(): Unit = println("bark")
  }
  
  class Car{
    def makeSound(): Unit = println("wroom")
  }
  
  // static duck typing
  val dog: SoundMaker = new Dog
  val car: SoundMaker = new Car
  
  // based on reflection - performance hit
  
  trait CBL[+T] {
    def head:T
    def tail: CBL[T]
  }
  
  class Human{
    def head: Brain = new Brain
  }
  
  class Brain {
    override def toString: String = "BRAINZ"
  }
  
  def f[T](somethingWithHead: {def head: T }): Unit = println(somethingWithHead.head)

  case object CBNil extends CBL[Nothing] {
    override def head: Nothing = ???

    override def tail: CBL[Nothing] = ???
  }

  case class CBCons[T](override val head: T, override val tail: CBL[T]) extends CBL[T]
  f(CBNil)
  f(new Human)
  
  object HeadEqualizer {
    type Headable[T] = {
        def head: T
    }

    def ===[T](a: Headable[T], b: Headable[T]): Boolean = a.head == b.head
  }
  
  val brainsList = CBCons(new Brain, CBNil)
  val stringsList= CBCons("bRAINZ", CBNil) 
  HeadEqualizer.===(brainsList, new Human)
  
  // problem:
  HeadEqualizer.===(new Human, stringsList) // not type safe
}

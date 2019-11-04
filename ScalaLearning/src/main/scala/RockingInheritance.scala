object RockingInheritance extends App  {

  trait Writer[T]{
    def write(value: T): Unit
  }
  
  trait Closable[T]{
    def close(status: Int): Unit
  }
  
  trait GenericStream[T]{
    def foreach(f: T => Unit): Unit
  }
  
  def processStream[T](stream: GenericStream[T] with Writer[T] with Closable[T]): Unit = {
    stream.foreach(f => println(f))
    stream.close(0)
  }
  
  // diamond problem
  trait Animal {
    def name: String
  }
  trait Lion extends Animal{
    override def name: String = "Lion"
  }
  trait Tiger extends Animal{
    override def name: String = "Tiger"
  }
  
  class Mutant extends Lion with Tiger {
  }
  
 // LAST OVERRIDES GET PICKED
  val mutant = new Mutant
  println(mutant.name)
  
  // the super problem + type linearization
  
  trait Cold{
    def print = println("cold")
  }
  
  trait Green extends Cold{
    override def print: Unit = {
      println("green")
      super.print
    }
  }

  trait Blue extends Cold{
    override def print: Unit = {
      println("blue")
      super.print
    }
  }
  
  class Red{
    def print = println("red")
  }
  
  class White extends Red with Green with Blue{
    override def print: Unit = {
      println("white")
      super.print
    }
  }
  // type linearization of WHITE any ref -> red -> cold -> green -> blue -> <white>
  val color = new White
  color.print
}

object SelfTypes extends App {

  trait Instrumentalist {
    def play(): Unit
  }
  
  trait SomeInstrumentalist {
    
  }
  
  trait Singer {
    // self-type - if you need multiple add with another one
    self: Instrumentalist with SomeInstrumentalist =>
    def sign(): Unit
  }
  
  class LeadSinger extends Singer with Instrumentalist with SomeInstrumentalist {
    override def sign(): Unit = ???

    override def play(): Unit = ???
  }
  
  /* Illegal
  class Vocalist extends Singer{
    override def sign(): Unit = ???
  }
  */
    // valid
  val jamesHetfield = new Singer with Instrumentalist with SomeInstrumentalist {
    override def sign(): Unit = ???

    override def play(): Unit = ???
  }
  
  class Guitarist extends Instrumentalist{
    override def play(): Unit = ???
  }
  
  // valid 
  val ericClapton = new Guitarist with Singer with SomeInstrumentalist {
    override def sign(): Unit = ???
  }
  
  // CAKE PATTERN - dependency injection
  trait ScalaComponent{
    // API
    def action(x: Int): String
  }
  
  trait ScalaDependentComponent{
    self: ScalaComponent =>
    
    def dependentAction(x: Int): String = action(x) + " this rocks!"
  }
  
  // layer 1
  trait Picture extends ScalaComponent
  trait Stats extends ScalaComponent
  
  trait Profile extends ScalaDependentComponent with Picture
  trait Analytics extends ScalaDependentComponent with Stats
}

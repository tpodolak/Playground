object CurriesAndPartiallyAppliedFunctions extends App {

  val supperAdder: Int => Int => Int = x => y => x + y


  def curriedAdder(x: Int)(y : Int) : Int = x + y

  val add4: Int => Int = curriedAdder(4)
  println(add4(2))

  // without type annotation we need to use _
  val add4NoType = curriedAdder(4) _

  println(add4NoType(2))

  val simpleAddFunction = (x: Int, y: Int) => x + y
  def simpleAddMethod(x:Int, y: Int) = x + y
  def curriedAddMethod(x: Int)(y: Int) = x + y

  println(simpleAddFunction.curried(7)(2))
  val intToInt= simpleAddMethod(7, _: Int)
  println(intToInt(2))

  def concatentor(a:String, b:String, c:String) : String = a + b + c

  val insertName = concatentor("Hello, I'm ", _, " How are you?")
  println(insertName("Daniel"))

  def format(format: String)(x: Double) : String = format.format(x)
  val simpleFormatter = format("%4.2f") _
  println(List(Math.PI,2,3).map(simpleFormatter))

  def byName(n: => Unit): Unit = {
    println("by name")
    n
  }
  def byFunction(n: () => Int ) = n() + 1
  def method: Unit = println("method")
  def parenMethod(): Unit = println("param method")


  // byName(23) // ok
  byName(method) // ok 42

  byName(parenMethod()) // ok\
  byName(parenMethod) // ok but == byName(paramMethod())
//  // byName(() => 42) // not ok
//
//  // byFunction(45) // not ok
//  // byFunction(method) // not ok
//  byFunction(parenMethod) // ok unlike the one above
}


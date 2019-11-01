object PartialFunctions extends App {

  // partial functions - applicable for certain values only

  val function = (x: Int) => x + 1

  val ifFunction = (x: Int) =>
    if(x == 2) 44
    else if (x == 3) 44
    else throw new IllegalArgumentException

  // partial function
  val ifBetterFunction: PartialFunction[Int,Int] = {
    case 1 => 1
    case 2 => 2
  }


  println(ifBetterFunction(1)) // works
  println(ifBetterFunction(2)) // works
  // println(ifBetterFunction(3)) // throws

  // you can test if it is applicable

  println(ifBetterFunction.isDefinedAt(2)) // true
  println(ifBetterFunction.isDefinedAt(3)) // false

  // lift so it wont throw - now it is a total function returning Option[T]

  ifBetterFunction.lift(1) match {
    case Some(_) => println("defined")
    case None => "not defined"
  }

  var combined = ifBetterFunction orElse[Int, Int]{
    case 3 => 3
  }

  println(combined.lift(4))

  println(List(1,2,3).map({
    case 1 => 2
    case 2 => 3
    case 3 => 4
  }))

//  THIS will throw as there is no match for 3
//  println(println(List(1,2,3).map({
//    case 1 => 2
//    case 2 => 3
//  })))

  // PARTIAL FUNCTION CAN ONLY HAVE ONE PARAMETER

  val partialFunctionFromAnonymous = new PartialFunction[Int,Int] {
    override def isDefinedAt(x: Int): Boolean = x == 1

    // isDefinedAt is not called automatically if we do this via anonymous class
    override def apply(v1: Int): Int = v1 match{
      case 1 => 2
    }
  }

  println(partialFunctionFromAnonymous(1))


}

object ChatBot extends App {

  val processHi: PartialFunction[String, String] = {
    case "Hi" => "Hi"
    case "hi" => "hi"
  }

  val processName: PartialFunction[String, String] = {
    case "Name" => "Name"
    case "name" => "name"
  }

  val fallBack: PartialFunction[String,String] = {
    case _ => "Don't know"
  }

  val combined = processHi orElse processName orElse fallBack
  scala.io.Source.stdin.getLines().foreach(line => println(combined(line)))

}


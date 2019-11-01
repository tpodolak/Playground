object Syntax extends App {

  trait Action{
    def act(x: String): String
  }

  var anonymousTrait: Action = (x: String) => this.toString

  def singleArgMethod(x: String): String = x

  var result = singleArgMethod{
    var x = 1
    x = x +1
    if(x > 1)
      1.toString
    else
      2.toString
  }

  println(result);

  var counter = 20
  var block = {
    var insideBlock = 1
    counter += 1

  }

  var expression = {
    if (counter > 1) 43

    1
  }
  counter = 1

  println(s"expression $expression")



  var aCodeBlock = {

  }

  var xx = List(1, 2, 3) :+ 1
  println(xx)

  class MyStream[T]{
    def -->:(input:T): MyStream[T] = this
  }

  val x = 1 -->: 2 -->: new MyStream[Int];

  class MapLikeStream[T, M]

  var streamOfInt: Int MapLikeStream BigDecimal = new MapLikeStream[Int,BigDecimal]

}

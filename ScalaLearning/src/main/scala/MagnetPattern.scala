import scala.concurrent.Future

object MagnetPattern extends App {

  // method overloading
  class P2PRequest
  class P2PResponse
  class Serializer[T]
  trait Actor {
    def receive(statusCode: Int): Int
    def receive(request: P2PRequest): Int
    def receive(response: P2PResponse): Int
    def receive[T : Serializer](message: T): Int
    def receive[T: Serializer](message: T, statusCode: Int): Int
    def receive(future: Future[P2PRequest]): Int
  }

  /*
  1 - type erasure
  2 - lifting doesnt work
  3 - type inference and default args
   */
  
  trait MessageMagnet[Result]{
    def apply(): Result
  }
  
  def receive[R](magnet: MessageMagnet[R]) : R = magnet()
  
  implicit class FromP2PRequest(request: P2PRequest) extends MessageMagnet[Int] {
    override def apply(): Int = {
      // logic for handling P2P request
      println("Handling p2p request")
      42
    }
  }
  
  implicit class FromP2PResponse(response: P2PResponse) extends MessageMagnet[Int] {
    override def apply(): Int = {
      println("Handling P2P response")
      24
    }
  }
  
  receive(new P2PResponse)
  receive(new P2PRequest)
  
  class Handler{
    def handle(s: => String) = {
      println(s)
      println(s)
    }
  }
  
  trait HandleMagnet{
    def apply(): Unit
  }
  
  def handle(magnet: HandleMagnet) = magnet()
  
  implicit class fromStringToHandle(s: => String) extends HandleMagnet {
    override def apply(): Unit = {
      println(s)
      println(s)
    }
  }
  
  def sideEffectMethod(): String ={
    println("hello, scala")
    "hahaha"
  }
  
//  handle(sideEffectMethod())
//  handle{
//    println("hello, scala")
//    "magnet"
//  }
  
  val handler = new Handler
  handler.handle({
    println("hello, scala")
    "jajaja"
  })
}

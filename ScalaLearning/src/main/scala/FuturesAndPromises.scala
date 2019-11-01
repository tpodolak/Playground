import scala.concurrent._
import scala.concurrent.ExecutionContext.Implicits._
import scala.util.{Failure, Random, Success}
import scala.concurrent.duration._
object FuturesAndPromises extends App {

  def calculateMeaningOfLife: Int = {
    Thread.sleep(2000)
    42
  }

  val future = Future {
    calculateMeaningOfLife
  }

  future.onComplete({
    case Success(value) => println(s"Success $value")
    case Failure(exception) => println(s"Failed $exception")
  })

  Thread.sleep(3000)
  println(future.value)

  case class Profile(id: String, name: String) {
    def poke(anotherProfile: Profile) = {
      println(s"${this.name} pooking $anotherProfile")
    }
  }

  object SocialNetwork {
    val names = Map(
      "fb.id.1-zuck" -> "Mark",
      "fb.id.2-bill" -> "Bill",
      "fb.id.0-dummy" -> "Dummy",
    )

    val friends = Map(
      "fb.id.1-zuck" -> "fb.id.2-bill"
    )

    val random = new Random()

    def fetchProfile(id: String): Future[Profile] = Future {
      Thread.sleep(random.nextInt(300))
      Profile(id, names(id))
    }

    def fetchBestFriend(profile: Profile): Future[Profile] = Future {
      Thread.sleep(random.nextInt(400))
      val bfId = friends(profile.id)
      Profile(bfId, names(bfId))
    }
  }

  // client

  for {mark <- SocialNetwork.fetchProfile("fb.id.1-zuck")
       bill <- SocialNetwork.fetchBestFriend(mark)
       } mark.poke(bill)

  // fallbacks 
  val recover = SocialNetwork.fetchProfile("unknown").recover{
    case e: Throwable => Profile("dummy", "dummy name")
  }
  
  val recoverWith = SocialNetwork.fetchProfile("unknown").recoverWith({
    case e: Throwable => SocialNetwork.fetchProfile("fb.id.0-dummy")
  })
  
  for {
    first <- recover
    second <- recoverWith
  }{
    println(first)
    println(second)
  }
    
  Thread.sleep(3000)

  case class User(name: String)
  case class Transaction(sender: String, receiver: String, amount: BigDecimal, status: String)
  
  object BankingApp{
    val name = "JVM"
    
    def fetchUser(name: String): Future[User] = Future {
      Thread.sleep(500)
      User(name)
    }
    
    def createTransaction(user: User, merchantName: String, amount: BigDecimal) = Future{
      Thread.sleep(1000)
      Transaction(user.name, merchantName, amount, "success")
      
    }
    
    def purchase(userName: String, item: String, merchantName: String, cost: BigDecimal): String ={
      val statusFuture = for {
        user <- fetchUser(userName)
        transaction <- createTransaction(user, merchantName, cost)
      }
        yield transaction.status
      
      Await.result(statusFuture, 2.seconds)
    }
  }
  
  println(BankingApp.purchase("Daniel", "iphone","merchant", 1))
  
  val promise = Promise[Int]()
  
  val promiseFuture = promise.future
  
  promiseFuture.onComplete{
    case Success(r) => println(s"[consumer] - I've received $r")
  }
  
  val producer = new Thread(()=>{
    println("[producer] crunching numbers")
    Thread.sleep(500)
    promise.success(42)
    println("[producer] done")
  })
  
  producer.start()
  
  Thread.sleep(2000)
  
  def first(left: Future[Int], right: Future[Int]) = {
    val promise = Promise[Int]()
    left.onComplete(f => promise.success(f.get))
    right.onComplete(f => promise.success(f.get))
    
    promise.future
  }
}
import java.util.Date

object JSONSerialization extends App {

  case class User(name: String, age: Int, email: String)

  case class Post(content: String, createdAt: Date)

  case class Feed(user: User, posts: List[Post])

  sealed trait JSONValue {
    def stringify: String
  }

  final case class JSONString(value: String) extends JSONValue {
    override def stringify: String = "\"" + value + "\""
  }
  
  final case class JSONNumber(value: Int) extends JSONValue {
    override def stringify: String =  value.toString
  }
  
  final case class JSONArray(list: List[JSONValue]) extends JSONValue {
    override def stringify: String = list.map(_.stringify).mkString("[",",","]")
  }
  
  final case class JSONObject(values: Map[String, JSONValue]) extends JSONValue {
    override def stringify: String = values.map{
      case (key, value) => "\"" + key + "\":" + value.stringify
    }.mkString("{", ",", "}")
  }
  
  val data = JSONObject(Map(
    "user" -> JSONString("John"),
    "posts" -> JSONArray(List(
      JSONString("Scala"),
      JSONNumber(453)
    ))
  ))
  
  implicit class JSONOps[T](input: T){
    def toJSON(implicit converter: JSONConverter[T]) = converter.convert(input)
  }
  
  trait JSONConverter[T] {
    def convert(value:T) : JSONValue
  }
  
  object JSONConverter{
    def apply[T : JSONConverter]() = implicitly[JSONConverter[T]]
  }
  
  implicit object StringConverter extends JSONConverter[String] {
    override def convert(value: String): JSONValue = JSONString(value)
  }
  
  implicit object NumberConverter extends JSONConverter[Int] {
    override def convert(value: Int): JSONValue = JSONNumber(value)
  }
  
  implicit object UserConverter extends JSONConverter[User] {
    override def convert(user: User): JSONValue = JSONObject(Map(
      "name" -> JSONString(user.name),
      "age" -> JSONNumber(user.age),
      "email" -> JSONString(user.email)
    ))
  }

  implicit object PostConverter extends JSONConverter[Post] {
    override def convert(post: Post): JSONValue = JSONObject(Map(
      "content" -> JSONString(post.content),
      "createdAt" -> JSONString(post.createdAt.toString)
    ))
  }
  
  implicit object FeedConverter extends JSONConverter[Feed] {
    override def convert(value: Feed): JSONValue = JSONObject(Map(
      "user" -> value.user.toJSON,
      "posts" -> JSONArray(value.posts.map(_.toJSON)
    )))
  }

  private val john = User("John", 1, "email")
  println(JSONConverter[User].convert(john).stringify)
  
  println(Feed(john, List(Post("content", new Date(System.currentTimeMillis())))).toJSON.stringify)
  
}

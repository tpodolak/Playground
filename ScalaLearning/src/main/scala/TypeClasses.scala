object TypeClasses extends App {

  trait HTMLWritable {
    def toHtml: String
  }

  case class User(name: String, age: Int, email: String) extends HTMLWritable {
    override def toHtml: String = s"<div>$name ($age yo) <a href=$email/> </div>"
  }

  trait HtmlSerializer[T]{
    def serialize(input: T): String
  }
  
  implicit object UserHtmlSerializer extends HtmlSerializer[User] {
    override def serialize(user: User): String = s"<div>${user.name} (${user.age} yo) <a href=${user.email}/> </div>"
  }

  println(HtmlSerializer[User].serialize(User("John", 18, "john@gmail.com")))
  println(HtmlSerializer.serialize(User("John", 18, "john@gmail.com")))
  
  object HtmlSerializer{
    def serialize[T : HtmlSerializer](input: T) = implicitly[HtmlSerializer[T]].serialize(input)

    def apply[T](implicit serializer: HtmlSerializer[T]) = serializer
  }
  
  println(HtmlSerializer.serialize(User("John", 1, "john@gmail.com")))
}
  
  

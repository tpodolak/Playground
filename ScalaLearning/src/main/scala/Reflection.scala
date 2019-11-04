import scala.reflect.runtime.{universe => ru }


object Reflection extends App {

  // scala reflection API exclusively 

  case class Person(name: String){
    def sayMyName(): Unit = println(s"Hi, my name is $name")
  }
  
  val runtimeMirror = ru.runtimeMirror(getClass.getClassLoader)
  val clazz = runtimeMirror.staticClass("Reflection.Person")
  println(clazz)
  val cm = runtimeMirror.reflectClass(clazz)
  val construtor = clazz.primaryConstructor.asMethod
  val constructorMirror = cm.reflectConstructor(construtor)
  
  val instance = constructorMirror.apply("john").asInstanceOf[Person]
  println(instance)
  
  val p = Person("some person")
  val methodName = "sayMyName";
  private val reflected = runtimeMirror.reflect(p)
  private val method: ru.MethodSymbol = ru.typeOf[Person].decl(ru.TermName(methodName)).asMethod
  println(reflected.reflectMethod(method).apply())
  
  // type erause
  import ru._
  val ttag = typeTag[Person]
  println(ttag.tpe)
  
  class MyMap[K, V]
  
  def getTypeArguments[T](value: T)(implicit typeTag: TypeTag[T]) = typeTag.tpe match{
    case TypeRef(_,_, typeArguments) => typeArguments
    case _ => List()
  }
  
  val myMap = new MyMap[Int, String]()
  println(getTypeArguments(myMap))
  
  def isSubtype[A, B](implicit ttagA: TypeTag[A], ttagB: TypeTag[B]): Boolean = {
    ttagA.tpe <:< ttagB.tpe
  }
  
  class Animal
  class Dog extends Animal
  println(isSubtype[Dog, Animal])
  
  val anotherMethodSymbol = typeTag[Person].tpe.decl(ru.TermName(methodName)).asMethod
  val sameMethod = reflected.reflectMethod(anotherMethodSymbol)
  sameMethod.apply()
}

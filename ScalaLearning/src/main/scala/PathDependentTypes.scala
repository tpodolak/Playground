object PathDependentTypes extends App  {

  class Outer{
    class Inner
    object InnerObject
    type InnerType
    
    def print(i: Inner) = println(i)
    // Outer#Inner common supertype of Inner class
    def printGeneral(i: Outer#Inner) =  println("common")
    def addMethod: Int = {
      class InMethodClass
      42
    }
    
    val outer = new Outer
    // via instance of outer - not like in C#
    val inner = new outer.Inner
    val otherOuter = new Outer
    
    // will not compile
    // val otherInner: outer.Inner = new otherOuter.Inner
    otherOuter.print(new otherOuter.Inner)
    
    // wil not compile as the types are different
    //otherOuter.print(new outer.Inner)

    // but I can use it via general supertype overload
   otherOuter.printGeneral(inner)    
  }
  
  trait ItemLike{
    type Key
  }
  
  trait Item[K] extends ItemLike{
    override type Key = K
  }
  
  trait IntItem extends Item[Int]
  trait StringItem extends Item[String]
  
  def get[ItemType <: ItemLike](key: ItemType#Key) = {
    
  }
  
  get[IntItem](42)
}

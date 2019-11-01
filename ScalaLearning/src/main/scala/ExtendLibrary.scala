object ExtendLibrary extends App {

  implicit class RichInt(value: Int){
    def isEven(): Boolean = value % 2 == 0
    
    def squareRoot() = Math.sqrt(value)
  }
  
  2.isEven()
}

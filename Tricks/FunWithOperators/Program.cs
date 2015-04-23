using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass myClass = new MyStruct();
        }
    }

    public class MyClass
    {
        
    }

    public struct MyStruct
    {
        public static implicit operator MyClass(MyStruct myStruct)
        {
            return new MyClass();
        }
    }
}

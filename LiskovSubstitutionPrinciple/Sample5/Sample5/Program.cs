using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample5
{
    public class Program
    {
        static void Main(string[] args)
        {
            INote note = new ReadOnlyNote();
            note.save("Let's do this!");

            INote note2 = new WriteableNote();
            note2.save("Let's do this!");

            Console.ReadKey();
            /*/
            A x = new B();
            //...
            A y=new B();

            //...
            A z= new B();

            //...*/
        }
    }

    public class A
    {

    }

    public class B :A
    {
        
    }

    public interface INote
    {
        void save(string text);
    }


    public class ReadOnlyNote : INote
    {
      
        public void save(string text)
        {
           Console.WriteLine("Dont Permition!");
        }

    }

    public class WriteableNote : INote
    {
       

        public void save(string text)
        {
            
            Console.WriteLine("Let's do this!");
            //Save Process 
        }

    }
}

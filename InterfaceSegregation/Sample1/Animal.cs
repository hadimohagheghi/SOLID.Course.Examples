using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_SOLID
{
    public interface IAnimal
    {
        void Eat();
        
    }

    public interface IParande : IAnimal
    {
       
        //void Run();
        void Fly();
        //void Swim();
    }
    public interface IAbzi : IAnimal
    {
        void Swim();
    }
    public interface IKhoshki : IAnimal
    {
        void Run();
    }

    public class Dolphin : IAbzi
    {
        public void Eat()
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            throw new NotImplementedException();
        }
    }

    public class Rabbit : IKhoshki
    {
        public void Eat()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}

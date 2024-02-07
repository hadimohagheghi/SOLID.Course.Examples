using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager DBManager=new DatabaseManager(new EmailNotification());
            DBManager.Remove();
            Console.ReadKey();
        }
    }


    public interface INotification
    {
        void Send(string message);
    }

    public class EmailNotification:INotification
    {
        public void Send(string message)
        {
           Console.WriteLine($"Email Sent! : {message}");
        }

    }

    public class SmsNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"SMS Sent! : {message}");
        }
    }

    //......

    public class DatabaseManager
    {
        
        private INotification _notification;

        public DatabaseManager(INotification notification)
        {
            _notification= notification;
        }
        
        public void Add()
        {
            //////////......////
            _notification.Send("Add New Record");
        }

        public void Remove()
        {
            //////////......////
            _notification.Send("Remove");
        }

        public void Update()
        {
            //////////......////
            _notification.Send("Update");
        }
    }
}

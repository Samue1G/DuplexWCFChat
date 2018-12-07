using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Client.Proxy;

namespace Client
{
    class Program
    {

        public class MyClient : Proxy.IMyServiceCallback
        {
            public void RecieveMessage(string username, string message)
            {
                Console.WriteLine("{0}:{1}", username, message);
            }
        }


        static void Main(string[] args)
        {
            InstanceContext context = new InstanceContext(new MyClient());
            Proxy.MyServiceClient client = new Proxy.MyServiceClient(context);

            Console.WriteLine("Enter name");
            var username = Console.ReadLine();
            client.Join(username);
            Console.WriteLine();
            Console.WriteLine("Enter message");
            Console.WriteLine("Press Q to exit");

            var message = Console.ReadLine();
            while (message != "Q")
            {
                if(!string.IsNullOrEmpty(message))
                    client.SendMessage(message);
                message = Console.ReadLine();
            }
            client.Close();
        }
    }
}

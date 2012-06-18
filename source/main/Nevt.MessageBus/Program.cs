using System;

namespace Nevt.MessageBus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mb = new MessageBus();

            mb.RegisterHandler<Int32>("my.address", message =>
            {

            });

            mb.RegisterHandler<Int32>("my.address", (message, context) =>
            {
                context.Reply();
            });
        }
    }
}
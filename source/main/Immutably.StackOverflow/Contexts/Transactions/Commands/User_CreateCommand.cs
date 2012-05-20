using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class User_CreateCommand : ICommand
    {
        public Guid UserId { get; set; }
        public String About { get; set; }
    }
}
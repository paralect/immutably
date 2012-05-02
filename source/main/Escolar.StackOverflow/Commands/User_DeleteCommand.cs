using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class User_DeleteCommand : ICommand
    {
        public Guid UserId { get; set; }
    }
}
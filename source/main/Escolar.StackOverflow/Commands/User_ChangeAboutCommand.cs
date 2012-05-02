using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class User_ChangeAboutCommand : ICommand
    {
        public Guid UserId { get; set; }
        public String About { get; set; }
    }
}
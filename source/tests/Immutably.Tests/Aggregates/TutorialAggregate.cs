using System;
using Immutably.Aggregates;
using Immutably.Messages;

namespace Immutably.Tests.Aggregates
{
    public class TutorialAggregateContext
    {
         
    }

    public class User : Aggregate<UserState>
    {
        public void CreateUser(String userId, String name, Int32 score)
        {
            Apply<UserCreated>(created =>
            {
                created.Id = userId;
                created.Name = name;
                created.Score = score;
            });
        }

        public void ChangeName(String newName)
        {
            Apply<UserNameChanged>(changed =>
            {
                changed.Id = Id;
                changed.Id = newName;
            });
        }

        public void ChangeScore(Int32 newScore)
        {
            Apply<UserScoreChanged>(changed =>
            {
                changed.Id = Id;
                changed.Score = newScore;
            });
        }
    }

    public class UserState : IState
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Score { get; set; }

        public void On(UserCreated created)
        {
            Id = created.Id;
            Name = created.Name;
            Score = created.Score;
        }

        public void On(UserNameChanged changed)
        {
            Name = changed.Name;
        }

        public void On(UserScoreChanged changed)
        {
            Score = changed.Score;
        }
    }

    public class UserCreated : IEvent
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Int32 Score { get; set; }
    }

    public class UserNameChanged : IEvent
    {
        public String Id { get; set; }
        public String Name { get; set; }        
    }

    public class UserScoreChanged : IEvent
    {
        public String Id { get; set; }
        public Int32 Score { get; set; }        
    }

}
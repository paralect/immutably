using System;
using Immutably.Aggregates;
using Immutably.Messages;

namespace Immutably.Tests.Aggregates.Tutorial
{
    public class TutorialAggregateContext
    {
         
    }

    public class User : StatefullAggregate<UserState>
    {
        public void CreateUser(String userId, String name, Int32 score)
        {
            if (String.IsNullOrEmpty(name))
                throw new Exception("Cannot create user with empty name");

            if (score < 0)
                throw new Exception("Cannot create user with empty name");

            Apply<UserCreated>(created =>
            {
                created.Id = userId;
                created.Name = name;
                created.Score = score;
            });
        }

        public void ChangeName(String newName)
        {
            if (State.Score < 10)
                throw new Exception("User can change name only if he has score 10 or more");

            if (String.IsNullOrEmpty(newName))
                throw new Exception("User name cannot be changed on empty name");

            Apply<UserNameChanged>(changed =>
            {
                changed.Id = Id;
                changed.Name = newName;
            });
        }

        public void ChangeScore(Int32 newScore)
        {
            if (newScore < 0)
                throw new Exception("User score cannot be changed to negative score");

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
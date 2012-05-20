using System;
using Immutably.StackOverflow.Contexts.Transactions.Commands;
using Immutably.StackOverflow.Contexts.Transactions.Domain;

namespace Immutably.StackOverflow.Contexts.Transactions.Handlers
{
    public class SiteCommandHandler
    {
        public SiteCommandHandler()
        {
            
        }

        public void Handle(Site_CreateCommand command)
        {
            /*var store = new AggregateStore();

            using(var session = store.OpenStream())
            {
                var site2 = session.GetById<Site>(Guid.Empty);
                site2.Create(command);
                session.SaveTransition();
            }


            var site = new Site(null, null);
            site.Create(command);
            */

            throw new NotImplementedException();
        }

        public void Handle(Site_ChangeNameCommand command)
        {
            var site = new Site(null, null);
            site.ChangeName(command);
        }
    }
}
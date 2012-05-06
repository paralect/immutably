using System;
using Machine.Specifications;

namespace Escolar.Tests
{
    [Subject(typeof(Account), "Funds transfer")]
    public class when_transferring_between_two_accounts : AccountSpecs
    {
        Because of = () =>
          fromAccount.Transfer(1m, toAccount);

        It should_debit_the_from_account_by_the_amount_transferred = () =>
          fromAccount.Balance.ShouldEqual(0m);

        It should_credit_the_to_account_by_the_amount_transferred = () =>
          toAccount.Balance.ShouldEqual(2m);
    }

    [Subject(typeof(Account), "Funds transfer"), Tags("failure")]
    public class when_transferring_an_amount_larger_than_the_balance_of_the_from_account : AccountSpecs
    {
        static Exception exception;
        Because of = () =>
          exception = Catch.Exception(() => fromAccount.Transfer(2m, toAccount));

        It should_not_allow_the_transfer = () =>
          exception.ShouldBeOfType<Exception>();
    }

    public class failure { }

    public abstract class AccountSpecs
    {
        protected static Account fromAccount;
        protected static Account toAccount;

        Establish context = () =>
        {
            fromAccount = new Account { Balance = 1m };
            toAccount = new Account { Balance = 1m };
        };
    }

    public class Account
    {
        private decimal _balance;

        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public void Transfer(decimal amount, Account toAccount)
        {
            if (amount > _balance)
            {
                throw new Exception(String.Format("Cannot transfer ${0}. The available balance is ${1}.", amount, _balance));
            }

            _balance -= amount;
            toAccount.Balance += amount;
        }
    }


}
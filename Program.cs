using System;
using System.Linq;

namespace FirstBankOfSuncoast
{
  class Program
  {
    static void Main(string[] args)
    {
      var isRunning = true;
      var errorMessage = "Sorry, that is not a valid input. Try Again.";

      // TEST USER
      var testUser = new User();
      testUser.UserName = "austinparvin";
      testUser.AddAccount("checking", 100.00);
      testUser.AddAccount("savings", 200.00);


      while (isRunning)
      {
        testUser.ShowAccounts();
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("(DEPOSIT 'd'), (WITHDRAW 'w'), (TRANSFER 't'), (QUIT 'q')");
        var userInput = Console.ReadLine().ToLower();

        switch (userInput)
        {
          ////////////////////////////////// DEPOSIT //////////////////////////////
          case "d":
            // Which account to deposit into
            Console.WriteLine("Which account would you like to deposit into?");
            testUser.ShowAccounts();
            int accountToDeposit;
            int.TryParse(Console.ReadLine(), out accountToDeposit);

            while (!testUser.Accounts.Any(account => account.AccountNumber == accountToDeposit))
            {
              Console.WriteLine(errorMessage);
              int.TryParse(Console.ReadLine(), out accountToDeposit);

            }

            // How mush to deposit
            Console.WriteLine("How much would you like to deposit into the account?");
            double amountToDeposit;
            double.TryParse(Console.ReadLine(), out amountToDeposit);


            while (amountToDeposit <= 0)
            {
              Console.WriteLine(errorMessage);
              double.TryParse(Console.ReadLine(), out amountToDeposit);
            }


            testUser.Deposit(accountToDeposit, amountToDeposit);
            break;

          /////////////////////////////// Withdraw ////////////////////////////////////
          case "w":

            // Which account to withdraw from
            Console.WriteLine("Which account would you like to withdraw from?");
            testUser.ShowAccounts();
            int accountToWithdraw;
            int.TryParse(Console.ReadLine(), out accountToWithdraw);

            while (!testUser.Accounts.Any(account => account.AccountNumber == accountToWithdraw))
            {
              Console.WriteLine(errorMessage);
              int.TryParse(Console.ReadLine(), out accountToWithdraw);

            }

            // How much to withdraw
            Console.WriteLine("How much would you like to withdraw into the account?");
            double amountToWithdraw;
            double.TryParse(Console.ReadLine(), out amountToWithdraw);

            while (amountToWithdraw > testUser.Accounts.First(account => account.AccountNumber == accountToWithdraw).Balance || amountToWithdraw <= 0)
            {
              Console.WriteLine(errorMessage);
              double.TryParse(Console.ReadLine(), out amountToWithdraw);
            }

            testUser.Withdraw(accountToWithdraw, amountToWithdraw);
            break;

          /////////////////////////////// TRANSFER ////////////////////////////////////
          case "t":

            // Which account to transfer from
            Console.WriteLine("Which account would you like to transfer from?");
            testUser.ShowAccounts();
            int accountToTransferFrom;
            int.TryParse(Console.ReadLine(), out accountToTransferFrom);

            while (!testUser.Accounts.Any(account => account.AccountNumber == accountToTransferFrom))
            {
              Console.WriteLine(errorMessage);
              int.TryParse(Console.ReadLine(), out accountToTransferFrom);

            }

            // Which account to transfer to
            Console.WriteLine("Which account would you like to transfer to?");
            testUser.ShowAccounts();
            int accountToTransferTo;
            int.TryParse(Console.ReadLine(), out accountToTransferTo);

            while (!testUser.Accounts.Any(account => account.AccountNumber == accountToTransferTo) || accountToTransferFrom == accountToTransferTo)
            {
              Console.WriteLine(errorMessage);
              int.TryParse(Console.ReadLine(), out accountToTransferTo);

            }

            // How much to transfer
            Console.WriteLine("How much would you like to transfer?");
            double amountToTransfer;
            double.TryParse(Console.ReadLine(), out amountToTransfer);


            while (amountToTransfer > testUser.Accounts.First(account => account.AccountNumber == accountToTransferFrom).Balance || amountToTransfer <= 0)
            {
              Console.WriteLine(errorMessage);
              double.TryParse(Console.ReadLine(), out amountToTransfer);
            }
            testUser.Transfer(accountToTransferFrom, accountToTransferTo, amountToTransfer);
            break;
          case "q":
            isRunning = false;
            break;
          default:
            Console.WriteLine(errorMessage);
            break;

        }
      }
    }
  }
}


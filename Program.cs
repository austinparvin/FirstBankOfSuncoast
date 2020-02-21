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
      testUser.Password = "password";
      testUser.AddAccount("checking", 100.00);
      testUser.AddAccount("savings", 200.00);


      while (isRunning)
      {
        testUser.ShowAccounts();
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("(DEPOSIT 'd'), (WITHDRAW 'w'), (TRANSFER 't'), (QUIT 'q'), (USER SETTINGS 'u')");
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

            while (!testUser.Accounts.Any(a => a.AccountNumber == accountToDeposit))
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
            testUser.Withdraw();
            break;

          /////////////////////////////// TRANSFER ////////////////////////////////////
          case "t":

            // Which account to transfer from
            Console.WriteLine("Which account would you like to transfer from?");
            testUser.ShowAccounts();
            int accountToTransferFrom;
            int.TryParse(Console.ReadLine(), out accountToTransferFrom);

            while (!testUser.Accounts.Any(a => a.AccountNumber == accountToTransferFrom))
            {
              Console.WriteLine(errorMessage);
              int.TryParse(Console.ReadLine(), out accountToTransferFrom);

            }

            // Which account to transfer to
            Console.WriteLine("Which account would you like to transfer to?");
            testUser.ShowAccounts();
            int accountToTransferTo;
            int.TryParse(Console.ReadLine(), out accountToTransferTo);

            while (!testUser.Accounts.Any(a => a.AccountNumber == accountToTransferTo) || accountToTransferFrom == accountToTransferTo)
            {
              Console.WriteLine(errorMessage);
              int.TryParse(Console.ReadLine(), out accountToTransferTo);

            }

            // How much to transfer
            Console.WriteLine("How much would you like to transfer?");
            double amountToTransfer;
            double.TryParse(Console.ReadLine(), out amountToTransfer);


            while (amountToTransfer > testUser.Accounts.First(a => a.AccountNumber == accountToTransferFrom).Balance || amountToTransfer <= 0)
            {
              Console.WriteLine(errorMessage);
              double.TryParse(Console.ReadLine(), out amountToTransfer);
            }
            testUser.Transfer(accountToTransferFrom, accountToTransferTo, amountToTransfer);
            break;


          ///////////////////////////////////// QUIT ///////////////////////////////////////
          case "q":
            isRunning = false;
            break;

          /////////////////////////////// USER SETTINGS ////////////////////////////////////
          case "u":
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("(ADD ACCOUNT 'a'), (CLOSE ACCOUNT 'c'), (CHANGE PASSWORD 'p')");
            userInput = Console.ReadLine().ToLower();
            switch (userInput)
            {

              // ADDING ACCOUNTS
              case "a":
                Console.WriteLine("What kind of account would you like to add? (SAVINGS 's') or (CHECKING 'c')");
                var accountType = Console.ReadLine().ToLower();

                while (accountType != "c" && accountType != "s")
                {
                  Console.WriteLine(errorMessage);
                  accountType = Console.ReadLine().ToLower();
                }

                if (accountType == "c")
                {
                  accountType = "checking";
                }
                else if (accountType == "s")
                {
                  accountType = "savings";
                }

                System.Console.WriteLine("How much would you like to deposit?");

                double openDepositAmount;
                double.TryParse(Console.ReadLine(), out openDepositAmount);


                while (openDepositAmount <= 0)
                {
                  Console.WriteLine(errorMessage);
                  double.TryParse(Console.ReadLine(), out openDepositAmount);
                }


                testUser.AddAccount(accountType, openDepositAmount);
                break;

              // CLOSING ACCOUNTS
              case "c":
                Console.WriteLine("Which account would you like to close?");
                int accountNum;
                int.TryParse(Console.ReadLine(), out accountNum);

                while (!testUser.Accounts.Any(a => a.AccountNumber == accountNum))
                {
                  Console.WriteLine(errorMessage);
                  int.TryParse(Console.ReadLine(), out accountNum);

                }

                bool close = true;
                while (close && testUser.Accounts.First(a => a.AccountNumber == accountNum).Balance != 0)
                {
                  Console.WriteLine("Sorry your account must have a 0 balance before closing.\nWould you like to withdraw from this account? (YES 'y') or (NO 'n')");
                  userInput = Console.ReadLine().ToLower();

                  while (userInput != "y" && userInput != "n")
                  {
                    Console.WriteLine(errorMessage);
                    userInput = Console.ReadLine().ToLower();
                  }

                  if (userInput == "y")
                  {
                    testUser.Withdraw(accountNum);
                  }
                  else
                  {
                    close = false;
                  }
                }

                if (close)
                {
                  testUser.CloseAccount(accountNum);
                }
                else
                {
                  System.Console.WriteLine("Exited closing account");
                }


                break;
              case "p":
                System.Console.WriteLine("Please enter your new password");
                var newPassword = Console.ReadLine();
                testUser.Password = newPassword;

                break;
            }
            break;

          default:
            Console.WriteLine(errorMessage);
            break;

        }
      }
    }
  }
}
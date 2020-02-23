using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Newtonsoft.Json;

namespace FirstBankOfSuncoast
{
    class Program
    {
        static void SaveBankData(Bank bank)
        {
            string json = JsonConvert.SerializeObject(bank);
            System.IO.File.WriteAllText("bank.txt", json);
        }
        static void Main(string[] args)
        {
            var bank = new Bank();
            var currentUser = new User();

            //Populate bank with data from bank.txt
            bank = JsonConvert.DeserializeObject<Bank>(File.ReadAllText(@"bank.txt"));

            // error message
            var errorMessage = "Sorry, that is not a valid input. Try Again.";
            var isRunning = true;
            bool loggedIn = false;

            while (isRunning)
            {
                // LOGIN
                while (!loggedIn)
                {
                    System.Console.WriteLine("Greetings, would you like to (LOGIN 'l') or (SIGN UP 's')?");
                    // Get user input
                    var userAnswer = Console.ReadLine().ToLower();

                    // validate
                    while (userAnswer != "l" && userAnswer != "s")
                    {
                        System.Console.WriteLine(errorMessage);
                        userAnswer = Console.ReadLine().ToLower();
                    }

                    if (userAnswer == "l")
                    {

                        // UserName
                        System.Console.WriteLine("Please enter your username");
                        var userName = Console.ReadLine().ToLower();
                        while (!bank.Users.Any(u => u.UserName == userName))
                        {
                            Console.WriteLine("User not found. Try again.");
                            userName = Console.ReadLine().ToLower();
                        };

                        // Password
                        System.Console.WriteLine("Please enter your password");
                        var password = Console.ReadLine();
                        while (password != bank.Users.First(u => u.UserName == userName).Password)
                        {
                            Console.WriteLine("Password incorrect. Try again.");
                            password = Console.ReadLine().ToLower();
                        };

                        // Set Current User
                        currentUser = bank.Users.First(u => u.UserName == userName);
                        loggedIn = true;
                    }
                    else if (userAnswer == "s")
                    {
                        // UserName
                        System.Console.WriteLine("Please enter your username");
                        var userName = Console.ReadLine().ToLower();
                        while (bank.Users.Any(u => u.UserName == userName))
                        {
                            Console.WriteLine("Username is taken. Try again.");
                            userName = Console.ReadLine().ToLower();
                        };

                        // Password
                        System.Console.WriteLine("Please enter your password");
                        var password = Console.ReadLine();

                        bank.CreateUser(userName, password);
                        SaveBankData(bank);
                    }

                }

                var userInput = "";

                if (!currentUser.Accounts.Any())
                {
                    userInput = "u";
                }
                else
                {
                    currentUser.ShowAccounts();
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("(DEPOSIT 'd'), (WITHDRAW 'w'), (TRANSFER 't'), (QUIT 'q'), (USER SETTINGS 'u')");
                    userInput = Console.ReadLine().ToLower();
                }

                switch (userInput)
                {
                    ////////////////////////////////// DEPOSIT //////////////////////////////
                    case "d":
                        // Which account to deposit into
                        Console.WriteLine("Which account would you like to deposit into?");
                        currentUser.ShowAccounts();
                        int accountToDeposit;
                        int.TryParse(Console.ReadLine(), out accountToDeposit);

                        while (!currentUser.Accounts.Any(a => a.AccountNumber == accountToDeposit))
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


                        currentUser.Deposit(accountToDeposit, amountToDeposit);
                        SaveBankData(bank);
                        break;

                    /////////////////////////////// Withdraw ////////////////////////////////////
                    case "w":

                        // Which account to withdraw from
                        currentUser.Withdraw();
                        SaveBankData(bank);
                        break;

                    /////////////////////////////// TRANSFER ////////////////////////////////////
                    case "t":

                        // Which account to transfer from
                        Console.WriteLine("Which account would you like to transfer from?");
                        currentUser.ShowAccounts();
                        int accountToTransferFrom;
                        int.TryParse(Console.ReadLine(), out accountToTransferFrom);

                        while (!currentUser.Accounts.Any(a => a.AccountNumber == accountToTransferFrom))
                        {
                            Console.WriteLine(errorMessage);
                            int.TryParse(Console.ReadLine(), out accountToTransferFrom);

                        }

                        // Which account to transfer to
                        Console.WriteLine("Which account would you like to transfer to?");
                        currentUser.ShowAccounts();
                        int accountToTransferTo;
                        int.TryParse(Console.ReadLine(), out accountToTransferTo);

                        while (!currentUser.Accounts.Any(a => a.AccountNumber == accountToTransferTo) || accountToTransferFrom == accountToTransferTo)
                        {
                            Console.WriteLine(errorMessage);
                            int.TryParse(Console.ReadLine(), out accountToTransferTo);

                        }

                        // How much to transfer
                        Console.WriteLine("How much would you like to transfer?");
                        double amountToTransfer;
                        double.TryParse(Console.ReadLine(), out amountToTransfer);


                        while (amountToTransfer > currentUser.Accounts.First(a => a.AccountNumber == accountToTransferFrom).Balance || amountToTransfer <= 0)
                        {
                            Console.WriteLine(errorMessage);
                            double.TryParse(Console.ReadLine(), out amountToTransfer);
                        }
                        currentUser.Transfer(accountToTransferFrom, accountToTransferTo, amountToTransfer);
                        SaveBankData(bank);
                        break;

                    /////////////////////////////// USER SETTINGS ////////////////////////////////////
                    case "u":

                        if ((currentUser.Accounts != null) && (!currentUser.Accounts.Any()))
                        {
                            userInput = "a";
                        }
                        else
                        {
                            Console.WriteLine("What would you like to do?");
                            Console.WriteLine("(ADD ACCOUNT 'a'), (CLOSE ACCOUNT 'c'), (CHANGE PASSWORD 'p')");
                            userInput = Console.ReadLine().ToLower();
                        }

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


                                currentUser.AddAccount(accountType, openDepositAmount);
                                SaveBankData(bank);
                                break;

                            // CLOSING ACCOUNTS
                            case "c":
                                Console.WriteLine("Which account would you like to close?");
                                int accountNum;
                                int.TryParse(Console.ReadLine(), out accountNum);

                                while (!currentUser.Accounts.Any(a => a.AccountNumber == accountNum))
                                {
                                    Console.WriteLine(errorMessage);
                                    int.TryParse(Console.ReadLine(), out accountNum);

                                }

                                bool close = true;
                                while (close && currentUser.Accounts.First(a => a.AccountNumber == accountNum).Balance != 0)
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
                                        currentUser.Withdraw(accountNum);
                                    }
                                    else
                                    {
                                        close = false;
                                    }
                                }

                                if (close)
                                {
                                    currentUser.CloseAccount(accountNum);
                                }
                                else
                                {
                                    System.Console.WriteLine("Exited closing account");
                                }

                                SaveBankData(bank);
                                break;
                            case "p":
                                System.Console.WriteLine("Please enter your new password");
                                var newPassword = Console.ReadLine();
                                currentUser.Password = newPassword;
                                SaveBankData(bank);
                                break;
                        }
                        break;

                    ///////////////////////////////////// QUIT ///////////////////////////////////////
                    case "q":
                        isRunning = false;
                        SaveBankData(bank);
                        break;
                }
            }
        }
    }
}
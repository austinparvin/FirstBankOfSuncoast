using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstBankOfSuncoast
{
  public class User
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public List<Account> Accounts { get; set; } = new List<Account>();
    public int AccountNumberCounter { get; set; } = 1;
    // Methods
    public void AddAccount(string accountName, double accountBalance)
    {
      var newAccount = new Account()
      {
        Name = accountName,
        Balance = accountBalance,
        AccountNumber = AccountNumberCounter
      };
      Accounts.Add(newAccount);
      AccountNumberCounter++;
    }

    public void CloseAccount(int accountNum)
    {
      Accounts.RemoveAll(a => a.AccountNumber == accountNum);
    }
    public void ShowAccounts()
    {
      foreach (var a in Accounts)
      {
        Console.WriteLine($"Name:       {a.Name}");
        Console.WriteLine($"Balance:    {a.Balance}");
        Console.WriteLine($"Acc. Num:   {a.AccountNumber}");
        Console.WriteLine("");
      }
    }

    public void Deposit(int accountNumber, double amountToAdd)
    {
      Accounts.First(account => account.AccountNumber == accountNumber).Balance += amountToAdd;
    }


    // Withdraw
    public void Withdraw()
    {
      Console.WriteLine("Which account would you like to withdraw from?");
      ShowAccounts();
      int accountToWithdraw;
      int.TryParse(Console.ReadLine(), out accountToWithdraw);

      while (!Accounts.Any(a => a.AccountNumber == accountToWithdraw))
      {
        Console.WriteLine("errorMessage");
        int.TryParse(Console.ReadLine(), out accountToWithdraw);

      }

      // How much to withdraw
      Console.WriteLine("How much would you like to withdraw into the account?");
      double amountToWithdraw;
      double.TryParse(Console.ReadLine(), out amountToWithdraw);

      while (amountToWithdraw > Accounts.First(a => a.AccountNumber == accountToWithdraw).Balance || amountToWithdraw <= 0)
      {
        Console.WriteLine("errorMessage");
        double.TryParse(Console.ReadLine(), out amountToWithdraw);
      }

      Accounts.First(account => account.AccountNumber == accountToWithdraw).Balance -= amountToWithdraw;
    }

    public void Withdraw(int accountNumber)
    {


      // How much to withdraw
      Console.WriteLine("How much would you like to withdraw into the account?");
      double amountToWithdraw;
      double.TryParse(Console.ReadLine(), out amountToWithdraw);

      while (amountToWithdraw > Accounts.First(a => a.AccountNumber == accountNumber).Balance || amountToWithdraw <= 0)
      {
        Console.WriteLine("errorMessage");
        double.TryParse(Console.ReadLine(), out amountToWithdraw);
      }

      Accounts.First(account => account.AccountNumber == accountNumber).Balance -= amountToWithdraw;
    }

    public void Withdraw(int accountNumber, double amountToSubtract)
    {
      Accounts.First(account => account.AccountNumber == accountNumber).Balance -= amountToSubtract;
    }

    
    public void Transfer(int accountToTransferFrom, int accountToTransferTo, double amountToTransfer)
    {
      Withdraw(accountToTransferFrom, amountToTransfer);
      Deposit(accountToTransferTo, amountToTransfer);
    }

  }
}
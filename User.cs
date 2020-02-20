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
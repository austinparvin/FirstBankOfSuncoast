# First Bank Of Suncoast

A console app that will let you track a savings and a checking account total by performing "transactions"

This will save your information in a txt file in JSON so you can track your account totals over time, automatically.

This application includes:

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [JSON](https://www.json.org/json-en.html)
- Persistante data
- MVC design pattern

```C#
static void SaveBankData(Bank bank)
        {
            string json = JsonConvert.SerializeObject(bank);
            System.IO.File.WriteAllText("bank.txt", json);
        }
```

## User Actions

- Login/Sign Up
- Withdrawal
- Deposits
- Transfer
- Add accounts
- Close accounts
- Change password

## App In Action

### SIGN UP
![record it](http://g.recordit.co/YGtfxeXTm6.gif)

User data is then saved to the bank.txt immediately

![record it](http://g.recordit.co/iFgbcNt2qn.gif)

### LOGIN

(on login or sign up, if a user has no account they are prompted to open one)

![record it](http://g.recordit.co/zwMwGJhujd.gif)

### WITHDRAWAL
![record it](http://g.recordit.co/uanizL3bpY.gif)

### DEPOSIT
![record it](http://g.recordit.co/WIhra65dCS.gif)

### TRANSFER
![record it](http://g.recordit.co/uIvF7PTuxl.gif)

### USER SETTINGS

- Add Account
- Close Account
- Change Password

![record it](http://g.recordit.co/HLETE9ecxe.gif)

### QUIT

(data persists between logins)

![record it](http://g.recordit.co/qND0U3VoBM.gif)

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataCon.Models;

namespace DataCon.Pages
{
    public class IndexModel : PageModel
    {
        public BankAccount Account1 { get; set; }
        public BankAccount Account2 { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        [BindProperty]
        public string Currency { get; set; }

        [BindProperty]
        public string SelectedAccount { get; set; } = "Account1"; // Default to Account1

        public string ErrorMessage { get; set; } // Property to store error messages

        public IndexModel()
        {
            Account1 = new BankAccount(10000, "1234");
            Account2 = new BankAccount(2000, "5678");
        }

        public void OnGet()
        {
            LoadAccountBalances();
        }

        public void OnPost()
        {
            LoadAccountBalances();
        }

        public void OnPostDeposit()
        {
            LoadAccountBalances(); // Ensure balances are up-to-date

            if (SelectedAccount == "Account1")
            {
                Account1.Deposit(Amount, Currency);
                HttpContext.Session.SetString("Account1Balance", Account1.Balance.ToString());
            }
            else if (SelectedAccount == "Account2")
            {
                Account2.Deposit(Amount, Currency);
                HttpContext.Session.SetString("Account2Balance", Account2.Balance.ToString());
            }
        }

        public void OnPostWithdraw()
        {
            LoadAccountBalances(); // Ensure balances are up-to-date

            if (SelectedAccount == "Account1")
            {
                if (Account1.Balance >= Amount)
                {
                    Account1.Withdraw(Amount, Currency);
                    HttpContext.Session.SetString("Account1Balance", Account1.Balance.ToString());
                }
                else
                {
                    ErrorMessage = "Insufficient funds in Account 1234.";
                }
            }
            else if (SelectedAccount == "Account2")
            {
                if (Account2.Balance >= Amount)
                {
                    Account2.Withdraw(Amount, Currency);
                    HttpContext.Session.SetString("Account2Balance", Account2.Balance.ToString());
                }
                else
                {
                    ErrorMessage = "Insufficient funds in Account 5678.";
                }
            }
        }

        private void LoadAccountBalances()
        {
            var account1Balance = HttpContext.Session.GetString("Account1Balance");
            if (!string.IsNullOrEmpty(account1Balance))
            {
                Account1.SetBalance(decimal.Parse(account1Balance));
            }

            var account2Balance = HttpContext.Session.GetString("Account2Balance");
            if (!string.IsNullOrEmpty(account2Balance))
            {
                Account2.SetBalance(decimal.Parse(account2Balance));
            }
        }
    }
}

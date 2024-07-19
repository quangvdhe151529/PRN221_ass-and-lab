using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly PizzaStore.Data.AppDbContext _context;

        public IndexModel(PizzaStore.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Account> Accounts { get;set; } = default!;
        [BindProperty]
        public Account Account { get; set; }

        //GET
        public async Task OnGetAsync()
        {
            if (_context.Accounts != null)
            {
                Accounts = await _context.Accounts.ToListAsync();
            }
        }

        //POST
        public async Task<IActionResult> OnPostAsync()
        {
           
            string actionValue = Request.Form["Action"];
            if (actionValue == "CREATE")
            {
                _context.Accounts.Add(Account);
                await _context.SaveChangesAsync();
            }
            else if (actionValue == "EDIT")
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == Account.AccountId);
                if(account != null)
                {
                    account.Username = Account.Username;
                    account.Password = Account.Password;
                    account.Type = Account.Type;
                    _context.Accounts.Update(account);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}

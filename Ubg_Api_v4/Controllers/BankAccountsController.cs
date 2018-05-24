using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Ubg_Api_v4.Models;

namespace Ubg_Api_v4.Controllers
{
    public class BankAccountsController : ApiController
    {
        private Ubg_Api_v4Context db = new Ubg_Api_v4Context();

        // GET: api/BankAccounts
        public IQueryable<BankAccount> GetBankAccounts()
        {
            return db.BankAccounts;
        }

        // GET: api/BankAccounts/5
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> GetBankAccount(string id)
        {
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return Ok(bankAccount);
        }

        // PUT: api/BankAccounts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBankAccount(string id, BankAccount bankAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bankAccount.Id)
            {
                return BadRequest();
            }

            db.Entry(bankAccount).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankAccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BankAccounts
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> PostBankAccount(BankAccount bankAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BankAccounts.Add(bankAccount);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BankAccountExists(bankAccount.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bankAccount.Id }, bankAccount);
        }

        // DELETE: api/BankAccounts/5
        [ResponseType(typeof(BankAccount))]
        public async Task<IHttpActionResult> DeleteBankAccount(string id)
        {
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            db.BankAccounts.Remove(bankAccount);
            await db.SaveChangesAsync();

            return Ok(bankAccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankAccountExists(string id)
        {
            return db.BankAccounts.Count(e => e.Id == id) > 0;
        }
    }
}
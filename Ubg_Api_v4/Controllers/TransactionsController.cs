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
    public class TransactionsController : ApiController
    {
        private Ubg_Api_v4Context db = new Ubg_Api_v4Context();

        //// GET: api/Transactions
        //public IQueryable<Transaction> GetTransactions()
        //{
        //    return db.Transactions;
        //}

        //// GET: api/Transactions/5
        //[ResponseType(typeof(Transaction))]
        //public async Task<IHttpActionResult> GetTransaction(string id)
        //{
        //    Transaction transaction = await db.Transactions.FindAsync(id);
        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(transaction);
        //}

        //// PUT: api/Transactions/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutTransaction(string id, Transaction transaction)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != transaction.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(transaction).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TransactionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Transactions
        //[ResponseType(typeof(Transaction))]
        //public async Task<IHttpActionResult> PostTransaction(Transaction transaction)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Transactions.Add(transaction);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TransactionExists(transaction.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = transaction.Id }, transaction);
        //}

        //// DELETE: api/Transactions/5
        //[ResponseType(typeof(Transaction))]
        //public async Task<IHttpActionResult> DeleteTransaction(string id)
        //{
        //    Transaction transaction = await db.Transactions.FindAsync(id);
        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Transactions.Remove(transaction);
        //    await db.SaveChangesAsync();

        //    return Ok(transaction);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(string id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}
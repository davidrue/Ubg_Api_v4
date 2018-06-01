using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Ubg_Api_v4.Models;
using Ubg_Api_v4.QRCodes;

namespace Ubg_Api_v4.Controllers
{
    public class ActorsController : ApiController
    {
        private Ubg_Api_v4Context db = new Ubg_Api_v4Context();

        // GET: api/Actors

        public IQueryable<Actor> GetActors()
        {
            return db.Actors;
        }

        // GET: api/Actors/5
        [ResponseType(typeof(Actor))]
        [Route("api/actors/{actorID}/oneActor/{Token}")]
        public async Task<IHttpActionResult> GetActor(string actorId, string Token)
        {
            Actor actor = await db.Actors.FindAsync(Token);
            if (actor == null)
            {
                return NotFound();
            }

            CodeGenerator qrGenerator = new CodeGenerator();
            Image img = qrGenerator.RenderQRWithPicture();


            //return Ok(img);
            return Ok(actor);
        }

        //private IHttpActionResult Ok(Actor actor, Image img)
        //{
        //    throw new NotImplementedException();
        //}

        //// PUT: api/Actors/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutActor(string id, Actor actor)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != actor.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(actor).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ActorExists(id))
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

        //// POST: api/Actors
        //[ResponseType(typeof(Actor))]
        //public async Task<IHttpActionResult> PostActor(Actor actor)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Actors.Add(actor);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ActorExists(actor.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = actor.Id }, actor);
        //}

        // POST: vendor-api/{version}/register
        [ResponseType(typeof(Actor))]
        [Route("vendor-api/{version}/register", Name = "RegisterNewVendor")]
        public async Task<HttpResponseMessage> PostActor(ActorViewModels.RegisterViewModel actorModel)
        {

            Actor actor = new Actor();

            actor.IsCommercial = true;
            actor.IsPrivate = false;

            actor.firstIban = actorModel.firstIban.Replace(" ", "");
            actor.SurName = actorModel.SurName;
            actor.Name = actorModel.Name;
            actor.Email = actorModel.Email;
            actor.Password = actorModel.Password;
            actor.UserName = actorModel.UserName;
            actor.Id = HelperMethods.GetUniqueKey(15);



            BankAccount bankAccount = new BankAccount();
            bankAccount.Id = HelperMethods.GetUniqueKey(14);
            bankAccount.Iban = actor.firstIban;
            bankAccount.Priority = 1;

            bankAccount.ActorId = actor.Id;
            


            if (db.Actors.Any(u => u.UserName == actor.UserName))
            {
                //If userName already exists
                var respons1e  = Request.CreateResponse((HttpStatusCode)461, new HttpError("UserName already exists"));
                return respons1e;
                //return new System.Web.Http.Results.ResponseMessageResult(
                //Request.CreateErrorResponse((HttpStatusCode)461,new HttpError("UserName already exists")));

            }
            else
            {
                //if (!ModelState.IsValid)
                //{
                //    //var response = 
                //    //return BadRequest(ModelState);
                //}
               
                db.Actors.Add(actor);

            
            db.BankAccounts.Add(bankAccount);           
              
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                 throw;
                
            }

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{

            //        throw;

            //}
            var response = Request.CreateResponse(HttpStatusCode.Created, actor);
            string uri = Url.Link("RegisterNewVendor", new { id = actor.Id });
            response.Headers.Location = new Uri(uri);
            return response;

            //string uri = Url.Link("GetBookById", new { id = book.BookId });
            
            //return CreatedAtRoute("DefaultApi", new { id = actor.Id }, actor);
        }

        // DELETE: api/Actors/5
        [ResponseType(typeof(Actor))]
        public async Task<IHttpActionResult> DeleteActor(string id)
        {
            Actor actor = await db.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            db.Actors.Remove(actor);
            await db.SaveChangesAsync();

            return Ok(actor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActorExists(string id)
        {
            return db.Actors.Count(e => e.Id == id) > 0;
        }
    }
}
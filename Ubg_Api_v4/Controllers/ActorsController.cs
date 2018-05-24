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
        public async Task<IHttpActionResult> GetActor(string id)
        {
            Actor actor = await db.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

        // PUT: api/Actors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActor(string id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actor.Id)
            {
                return BadRequest();
            }

            db.Entry(actor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        [ResponseType(typeof(Actor))]
        public async Task<IHttpActionResult> PostActor(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Actors.Add(actor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActorExists(actor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = actor.Id }, actor);
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            
            return Ok(actor);
        }

        // GET: vendor-api/v1.0/confirm       
        [Route("vendor-api/{version}/confirm", Name = "ConfirmPaymentVendor")]
        public async Task<HttpResponseMessage> ConfirmPayment(QrTransactionModel.PaymentConfirmationModel paymentConfirmation)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse((HttpStatusCode)422, ModelState);
            }
            
            QrTransactionModel.PaymentConfirmationAnswerModel answer = new QrTransactionModel.PaymentConfirmationAnswerModel();

            return Request.CreateResponse(HttpStatusCode.OK, answer);
        }


        // POST: vendor-api/{version}/register
        [ResponseType(typeof(Actor))]
        [Route("vendor-api/{version}/register", Name = "RegisterNewVendor")]
        public async Task<HttpResponseMessage> PostActor(ActorViewModels.RegisterViewModel actorModel)
        {
            if (!ModelState.IsValid)
            {               
                //Password has no unique StatusCode
                var response2 = Request.CreateResponse((HttpStatusCode) 422, ModelState);
                return response2;
            }

            Actor actor = new Actor();           

            actor.IsCommercial = true;
            actor.IsPrivate = false;

            actor.firstIban = actorModel.first_iban.Replace(" ", "");
            actor.SurName = actorModel.sur_name;
            actor.Name = actorModel.name;
            actor.Email = actorModel.email;
            actor.Password = actorModel.password;
            actor.UserName = actorModel.user_name;
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

        // POST: vendor-api/{version}/auth       
        [Route("vendor-api/{version}/auth", Name = "RequestToken")]
        public async Task<HttpResponseMessage> PostTokenRequest(ActorViewModels.RequestTokenViewModel requestTokenViewModel)
        {
            
            if (!ModelState.IsValid)
            {                
                var response2 = Request.CreateResponse((HttpStatusCode)422, ModelState);
                return response2;
            }

            if (!db.Actors.Any(u => u.UserName == requestTokenViewModel.user_name))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, requestTokenViewModel.user_name);
            }
            string AuthToken = HelperMethods.GetUniqueKey(20);
            if (db.Actors.FirstOrDefault(u => u.UserName == requestTokenViewModel.user_name).Password == requestTokenViewModel.password)
            {
                db.Actors.FirstOrDefault(u => u.UserName == requestTokenViewModel.user_name).AuthToken = AuthToken;
            }
            else
            {
                var response1 = Request.CreateResponse(HttpStatusCode.Unauthorized, "Password does not match!");
                return response1;
            }            
            
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;

            }

            var response = Request.CreateResponse((HttpStatusCode)200, AuthToken);
            return response;
        }

        // POST: vendor-api/{version}/auth       
        [Route("vendor-api/{version}/generate", Name = "RequestQrCode")]
        public async Task<HttpResponseMessage> PostRequestQrCode(QrTransactionModel.RequestQrCodeViewModel requestQrCodeViewModel)
        {
            if (!ModelState.IsValid)
            {
                var response2 = Request.CreateResponse((HttpStatusCode)422, ModelState);
                return response2;
            }

            if (!db.Actors.Any(u => u.UserName == requestQrCodeViewModel.user_name))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, requestQrCodeViewModel.user_name);
            }
            if (!(db.Actors.FirstOrDefault(u => u.UserName == requestQrCodeViewModel.user_name).AuthToken == requestQrCodeViewModel.auth_token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "AuthToken is not valid!" );
            }
            if(requestQrCodeViewModel.available_until > 259200)
            {
                return Request.CreateResponse((HttpStatusCode) 468, "Availability is too long. Maximum is 72 hours.");
            }
            if (requestQrCodeViewModel.available_until < 60)
            {
                return Request.CreateResponse((HttpStatusCode)469, "Availability is too short. Minimum is 60 seconds.");
            }
            if (requestQrCodeViewModel.reference.Length > 140)
            {
                return Request.CreateResponse((HttpStatusCode)464, "Reference code is too long. Max length is 140 chars.");
            }

            Transaction transaction = new Transaction();
            transaction.Amount = requestQrCodeViewModel.amount;
            transaction.Currency = requestQrCodeViewModel.currency;
            transaction.Reference = requestQrCodeViewModel.reference;
            transaction.AvailableUntil = DateTime.Now.AddSeconds(requestQrCodeViewModel.available_until);
            transaction.AdjustibleUp = requestQrCodeViewModel.adjustible_up;
            transaction.AdjustibleDown = requestQrCodeViewModel.adjustible_down;
            transaction.Status = "Open";
            transaction.RecipientId = db.Actors.FirstOrDefault(u => u.UserName == requestQrCodeViewModel.user_name).Id;
            string transactionId = HelperMethods.GetUniqueKey(12);
            transaction.Id = transactionId;

            db.Transactions.Add(transaction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;

            }

            String transactionUrl = "ubg.transfer/" + transactionId;

            CodeGenerator qrGenerator = new CodeGenerator();
            string img = qrGenerator.RenderQRWithPicture(transactionUrl);


            QrTransactionModel.QrLinKRefViewModel qrLinKRefViewModel = new QrTransactionModel.QrLinKRefViewModel();
            qrLinKRefViewModel.link = transactionUrl;
            qrLinKRefViewModel.img = img;
            qrLinKRefViewModel.refId = transactionId;

            //TODO Test if QR should be included

            return Request.CreateResponse(HttpStatusCode.Created, qrLinKRefViewModel);          
        }

        // GET: client-api/{version}/get-information/{ref_id}
        [ResponseType(typeof(Actor))]
        [Route("client-api/{version}/get-information/{ref_id}")]
        public async Task<HttpResponseMessage> GetInformation([FromUriAttribute] string ref_id)
        {
            if (!db.Transactions.Any(u => u.Id == ref_id))
            {
                return Request.CreateResponse((HttpStatusCode) 465 , ref_id);
            }
            Transaction transaction = await db.Transactions.FirstOrDefaultAsync(u => u.Id == ref_id);
            if(transaction.AvailableUntil < DateTime.Now)
            {
                return Request.CreateResponse((HttpStatusCode) 466, "The transaction is expired: " + ref_id);
            }


            QrTransactionModel.GetInformationFromQRCodeModelAnswer answer = new QrTransactionModel.GetInformationFromQRCodeModelAnswer();
            answer.iban = db.Actors.FirstOrDefault(u => u.Id == transaction.RecipientId).firstIban;
            answer.amount = transaction.Amount;
            answer.currency = transaction.Currency;
            answer.reference = transaction.Reference;
            answer.adjustible_up = transaction.AdjustibleUp;
            answer.adjustible_down = transaction.AdjustibleDown;

            return Request.CreateResponse(HttpStatusCode.OK, answer);

        }

        // POST: client-api/{version}/make-payment/{ref_id}/{client_id}     
        [Route("vendor-api/{version}/generate", Name = "MakePayment")]
        public async Task<HttpResponseMessage> PostMakePayment([FromUriAttribute] string ref_id, [FromUriAttribute] string client_id, decimal adjusted_amount)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse((HttpStatusCode)422, ModelState);
               
            }

            if (!db.Transactions.Any(u => u.Id == ref_id))
            {
                return Request.CreateResponse((HttpStatusCode) 465, "Could not find Transaction: " + ref_id);
            }

            Transaction transaction = new Transaction();
            transaction = db.Transactions.FirstOrDefault(u => u.Id == ref_id);

            if (transaction.AvailableUntil < DateTime.Now)
            {
                return Request.CreateResponse((HttpStatusCode)466, "The transaction is expired: " + ref_id);
            }

            if (!db.Actors.Any(u => u.Id == client_id))
            {
                return Request.CreateResponse((HttpStatusCode)467, "Unknown Client: " + client_id);
            }

            db.Transactions.FirstOrDefault(u => u.Id == ref_id).SenderId = client_id;
            db.Transactions.FirstOrDefault(u => u.Id == ref_id).Amount = adjusted_amount;
            db.Transactions.FirstOrDefault(u => u.Id == ref_id).Status = "Paid";

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;

            }

            return Request.CreateResponse(HttpStatusCode.OK, "Successfull Transaction!");
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
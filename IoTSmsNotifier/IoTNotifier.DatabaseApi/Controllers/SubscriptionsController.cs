using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTNotifier.DatabaseApi.Model;
using IoTNotifier.DatabaseApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IoTNotifier.DatabaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        DatabaseRepository databaseRepository;

        public SubscriptionsController()
        {
            databaseRepository = new DatabaseRepository();
        }

        // GET api/subscriptions
        [HttpGet]
        public ActionResult<IEnumerable<Subscription>> Get()
        {
            Console.WriteLine("Get all subscriptions");

            try
            {
                return Ok(databaseRepository.GetAllSubscriptions());
            }
            catch (Exception ex)
            { 
                return StatusCode(500, ex);
            }
            
        }

        // GET api/subscriptions/5
        [HttpGet("{id}")]
        public ActionResult<Subscription> Get(int id)
        {
            Console.WriteLine($"Get subscription with id: {id}");

            try
            {
                return Ok(databaseRepository.GetSubscriptionById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/subscriptions
        [HttpPost]
        public ActionResult<Subscription> Post([FromBody] Subscription subscription)
        {
            Console.WriteLine($"Post subscription: {Newtonsoft.Json.JsonConvert.SerializeObject(subscription)}");

            try
            {
                if(subscription == null 
                    || subscription.City == null
                    || subscription.PhoneNumber == null
                    || (subscription.DailySubscription && subscription.TimeToSend == null)
                    || (!subscription.DailySubscription && subscription.TimeToSend != null))
                {
                    return BadRequest();
                }

                var result = databaseRepository.UpsertSubscription(subscription);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
            
        }

        [HttpDelete]
        public ActionResult<IEnumerable<Subscription>> Delete()
        {
            Console.WriteLine($"Delete all subscriptions");

            try
            {
                return Ok(databaseRepository.Clear());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Console.WriteLine($"Delete subscriptions id: {id}");

            try
            {
                databaseRepository.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        //PUT api/subscriptions/5
        [HttpPut("{id}")]
        public ActionResult<Subscription> Put(int id, [FromBody] Subscription subscription)
        {
            Console.WriteLine($"Put subscription id: {id}, content: {Newtonsoft.Json.JsonConvert.SerializeObject(subscription)}");

            try
            {
                if (subscription == null
                    || subscription.City == null
                    || subscription.PhoneNumber == null
                    || (subscription.DailySubscription && subscription.TimeToSend == null)
                    || (!subscription.DailySubscription && subscription.TimeToSend != null)
                    || id == 0)
                {
                    return BadRequest();
                }

                var result = databaseRepository.UpdateSubscription(id, subscription);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE api/subscriptions/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }

    }

}

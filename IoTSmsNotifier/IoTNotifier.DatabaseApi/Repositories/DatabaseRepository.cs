using IoTNotifier.DatabaseApi.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTNotifier.DatabaseApi.Repositories
{
    public class DatabaseRepository
    {
        public Subscription UpsertSubscription(Subscription subscription)
        {
            Subscription oldSubscriptionFromDB = GetSubscriptionByPhoneNumber(subscription.PhoneNumber);

            if (oldSubscriptionFromDB == null)
            {
                return AddSubscription(subscription);
            }

            return UpdateSubscription(oldSubscriptionFromDB.Id, subscription);
        }

        public Subscription AddSubscription(Subscription subscription)
        {
            using (var db = new LiteDatabase(@".\Subscriptions.db"))
            {
                var subscriptions = db.GetCollection<Subscription>("subscriptions");
                subscriptions.Insert(subscription);
                
                return subscription;
            }
        }

        public IEnumerable<Subscription> GetAllSubscriptions()
        {
            using (var db = new LiteDatabase(@".\Subscriptions.db"))
            {
                var result = db.GetCollection<Subscription>("subscriptions").FindAll().ToList();
                return result;
            }
        }

        public Subscription GetSubscriptionById(int id)
        {
            using (var db = new LiteDatabase(@".\Subscriptions.db"))
            {
                var result = db.GetCollection<Subscription>("subscriptions").FindById(id);
                return result;
            }
        }

        public Subscription GetSubscriptionByPhoneNumber(string phoneNumber)
        {
            using (var db = new LiteDatabase(@".\Subscriptions.db"))
            {
                var result = db.GetCollection<Subscription>("subscriptions").FindOne(x => x.PhoneNumber == phoneNumber);
                return result;
            }
        }

        public IEnumerable<Subscription> Clear()
        {
            using (var db = new LiteDatabase(@".\Subscriptions.db"))
            {
                db.DropCollection("subscriptions");
                var result = db.GetCollection<Subscription>("subscriptions").FindAll().ToList();
                return result;
            }
        }


        public void DeleteById(int id)
        {
            using (var db = new LiteDatabase(@".\Subscriptions.db"))
            {
                db.GetCollection<Subscription>("subscriptions").Delete(id);
            }
        }

        public Subscription UpdateSubscription(int id, Subscription subscription)
        {
            using (var db = new LiteDatabase(@".\Subscriptions.db"))
            {
                var subscriptions = db.GetCollection<Subscription>("subscriptions");
                subscriptions.Update(id, subscription);

                return subscription;
            }
        }
    }
}

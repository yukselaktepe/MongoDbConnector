using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MongoConnectorLibrary
{
    public class MongoConnector<T> : IMongoConnector<T> where T : Base, new()
    {
        public MongoDatabase mongodb;
        public MongoConnector()
        {
            var conn = new MongoClient("mongodb://localhost:27017");
            var server = conn.GetServer();
            mongodb = server.GetDatabase("ExampleDB");
        }


        /// <summary>
        /// Verilen değer kadar kayıt getirir
        /// </summary>
        public object GetCollection(int count = 0)
        {
            object colection;
            try
            {
                if (count != 0)
                    colection = mongodb.GetCollection<T>(typeof(T).Name).FindAll().SetLimit(count).ToList<T>();
                else
                    colection = mongodb.GetCollection<T>(typeof(T).Name).FindAll().ToList<T>();
            }
            catch (Exception ex)
            {
                colection = ex.Message;
            }
            return colection;
        }

        /// <summary>
        /// Sayfalama
        /// </summary>
        public object GetPage(int skip, int count)
        {
            object colection;
            try
            {
                colection = mongodb.GetCollection<T>(typeof(T).Name).FindAll().Skip(skip).Take(count).ToList<T>();
            }
            catch (Exception ex)
            {
                colection = ex.Message;
            }
            return colection;
        }

        /// <summary>
        /// Verilen criteria ve count göre belirtilen selector'ü getirir.
        /// </summary>
        public object GetPageCriteriaSelector(Expression<Func<T, bool>> criteria, Expression<Func<T, T>> selector, int count = 0)
        {
            object colection;
            try
            {
                if (count != 0)
                    colection = mongodb.GetCollection<T>(typeof(T).Name).FindAll().SetLimit(count).AsQueryable<T>().Where(criteria).Select(selector).ToList();
                else
                    colection = mongodb.GetCollection<T>(typeof(T).Name).FindAll().AsQueryable<T>().Where(criteria).Select(selector).ToList();
            }
            catch (Exception ex)
            {
                colection = ex.Message;
            }
            return colection;
        }

        /// <summary>
        /// Verilen count'a göre selector u getirir.
        /// </summary>
        public object GetPageSelector(Expression<Func<T, bool>> selector, int count = 0)
        {
            object colection;
            try
            {
                if (count != 0)
                    colection = mongodb.GetCollection<T>(typeof(T).Name).FindAll().SetLimit(count).AsQueryable<T>().Select(selector).ToList();
                else
                    colection = mongodb.GetCollection<T>(typeof(T).Name).FindAll().AsQueryable<T>().Select(selector).ToList();
            }
            catch (Exception ex)
            {
                colection = ex.Message;
            }


            return colection;
        }

        /// <summary>
        /// verilen criteria'a göre skip de belirtilen sayıdan sonrasında belirtiken count kadar veri getirir.
        /// </summary>
        public object GetPageCriteria(Expression<Func<T, bool>> criteria, int count = 0, int skip = 0)
        {
            object colection;
            try
            {
                var col = mongodb.GetCollection<T>(typeof(T).Name);
                if (count != 0 && skip != 0)
                    colection = col.Find(Query<T>.Where(criteria)).SetSkip(skip).SetLimit(count).ToList();
                else if (count != 0 && skip == 0)
                    colection = col.Find(Query<T>.Where(criteria)).SetLimit(count).ToList();
                else if (count == 0 && skip != 0)
                    colection = col.Find(Query<T>.Where(criteria)).SetSkip(skip).ToList();
                else
                    colection = col.Find(Query<T>.Where(criteria)).ToList();
            }
            catch (Exception ex)
            {
                colection = ex.Message;
            }
            return colection;
        }

        /// <summary>
        /// skip de belirtilen sayıdan sonrasında belirtiken count kadar veri getirir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public object GetPageList(int count = 0, int skip = 0)
        {
            object colection;
            try
            {
                var col = mongodb.GetCollection<T>(typeof(T).Name);
                colection = col.Find(Query.Null).SetSkip(skip).SetLimit(count).ToList();

            }
            catch (Exception ex)
            {
                colection = ex.Message;
            }
            return colection;
        }
        public bool UpdateModel(T model)
        {
            bool result = true;
            try
            {
                var collection = mongodb.GetCollection<T>(typeof(T).Name);
                var query = Query<T>.EQ(p => p.Id, model.Id);
                var replacement = Update<T>.Replace(model);
                collection.Update(query, replacement);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool Save(T model)
        {
            bool result = true;
            try
            {
                var col = mongodb.GetCollection<T>(typeof(T).Name);
                col.Insert(model);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
        public bool Delete(T model)
        {
            bool result;
            try
            {
                var col = mongodb.GetCollection<T>(typeof(T).Name);
                var query = Query.EQ("_id", model.Id);
                col.Remove(query);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
        public bool Delete(ObjectId id)
        {
            bool result = true;
            try
            {
                var col = mongodb.GetCollection<T>(typeof(T).Name);
                var query = Query.EQ("_id", id);
                col.Remove(query);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
        public bool Exists(T model)
        {
            bool result = true;
            try
            {
                var collection = mongodb.GetCollection<T>(typeof(T).Name);
                var query = Query.EQ("_id", model.Id);
                var fields = Fields.Include("_id");
                var res = collection.Find(query).SetFields(fields).SetLimit(1).FirstOrDefault();
                if (res == null)
                {
                    result = false;
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public int Count(Expression<Func<T, bool>> criteria, int count = 0, int skip = 0)
        {
            int cout = 0;
            try
            {
                var col = mongodb.GetCollection<T>(typeof(T).Name);
                cout = Convert.ToInt32(col.Find(Query<T>.Where(criteria)).Count());
            }
            catch (Exception ex)
            {
            }
            return cout;
        }
        public ObjectId GetId(Expression<Func<T, bool>> criteria)
        {
            ObjectId Id = new ObjectId();
            try
            {
                var col = mongodb.GetCollection<T>(typeof(T).Name);
                Id = col.Find(Query<T>.Where(criteria)).FirstOrDefault().Id;
            }
            catch (Exception ex)
            {
            }
            return Id;
        }

    }
}

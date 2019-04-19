using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace MongoConnectorLibrary
{
    public interface IMongoConnector<T> where T : Base, new()
    {

        /// <summary>
        /// Verilen değer kadar kayıt getirir
        /// </summary>
        List<T> GetCollection(int count = 0);
        /// <summary>
        /// Sayfalama
        /// </summary>
        List<T> GetPage(int skip, int count);
        /// <summary>
        /// Verilen criteria ve count göre belirtilen selector'ü getirir.
        /// </summary>
        List<T> GetPageCriteriaSelector(Expression<Func<T, bool>> criteria, Expression<Func<T, T>> selector, int count = 0);
        /// <summary>
        /// Verilen count'a göre selector u getirir.
        /// </summary>
        List<T> GetPageSelector(Expression<Func<T, bool>> selector, int count = 0);
        /// <summary>
        /// verilen criteria'a göre skip de belirtilen sayıdan sonrasında belirtiken count kadar veri getirir.
        /// </summary>
        List<T> GetPageCriteria(Expression<Func<T, bool>> criteria, int count = 0, int skip = 0);
        /// <summary>
        /// skip de belirtilen sayıdan sonrasında belirtiken count kadar veri getirir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        List<T> GetPageList(int count = 0, int skip = 0);
        bool UpdateModel(T model);
        bool Save(T model);
        bool Delete(T model);
        bool Delete(ObjectId id);
        bool Exists(T model);
        int Count(Expression<Func<T, bool>> criteria, int count = 0, int skip = 0);
        ObjectId GetId(Expression<Func<T, bool>> criteria);
    }
}

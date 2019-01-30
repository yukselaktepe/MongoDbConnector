# MongoDbConnector
C# MongoDB Connector

## IMongoConnector:

#Untyped Documents
```c#
using MongoDB.Bson;
using System.Linq.Expressions;
```

```c#
public interface IMongoConnector<T> where T : Base, new()
    {
        object GetCollection(int count = 0);
        object GetPage(int skip, int count);
        object GetPageCriteriaSelector(Expression<Func<T, bool>> criteria, Expression<Func<T, T>> selector, int count = 0);
        object GetPageSelector(Expression<Func<T, bool>> selector, int count = 0);
        object GetPageCriteria(Expression<Func<T, bool>> criteria, int count = 0, int skip = 0);
        object GetPageList(int count = 0, int skip = 0);
        bool UpdateModel(T model);
        bool Save(T model);
        bool Delete(T model);
        bool Delete(ObjectId id);
        bool Exists(T model);
        int Count(Expression<Func<T, bool>> criteria, int count = 0, int skip = 0);
        ObjectId GetId(Expression<Func<T, bool>> criteria);
    }

```



## Example:
```c#
    public class Base
    {
        [BsonId]
        public ObjectId Id { get; set; }

    }
    public class Category : Base
    {
        public string KategoriName { get; set; }
        public string KategoriDesc { get; set; }
    }
 ```   
# Csharp MongoDB Save
#Untyped Documents
```c#
using MongoConnectorLibrary;
using MongoDB.Bson;
using MongoDbConnector.Model;
using Newtonsoft.Json;
```

 ```c#
    public class Program
    {
         static IMongoConnector<Category> connector = new MongoConnector<Category>();
         static void Main(string[] args)
         {
            Save();
         }
         
        static void Save()
        {
            Category category = new Category()
            {
                KategoriName = "Bilgisayar",
                KategoriDesc = "Bilgisayar Ürünleri"
            };

            connector.Save(category);
        }
    }
 ``` 

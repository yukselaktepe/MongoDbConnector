using MongoConnectorLibrary;
using MongoDB.Bson;
using MongoDbConnector.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDbConnector
{
    public class Program
    {
        static IMongoConnector<Category> connector = new MongoConnector<Category>();
        static void Main(string[] args)
        {

            //Save();
            GetCollection();
            //GetPageCriteriaAndUpdate();
            //DeleteModel();
            //DeleteId();
            //Exist();
            //GetId();
        }

        static void Save()
        {
            Category category = new Category()
            {
                Name = "Bilgisayar",
                Desc = "Bilgisayar Ürünleri"
            };

            connector.Save(category);
        }

        static void GetPageCriteriaAndUpdate()
        {
            var category = (connector.GetPageCriteria(x => x.Name == "Bilgisayar") as List<Category>).FirstOrDefault();
            category.Name = "Telefon";
            category.Desc = "Telefon Açıklama";
            connector.UpdateModel(category);
        }

        static void GetCollection()
        {
            var categories = connector.GetCollection() as List<Category>;
            string jsonStr = JsonConvert.SerializeObject(categories);
            Console.WriteLine(jsonStr);
            Console.ReadLine();
        }

        static void DeleteModel()
        {
            ObjectId id = new ObjectId("5c41ba7c73a202e1e4695344");
            var category = (connector.GetPageCriteria(x => x.Id == id) as List<Category>).FirstOrDefault();
            connector.Delete(category);
        }

        static void DeleteId()
        {
            ObjectId id = new ObjectId("5c41ba7c73a202e1e4695344");
            connector.Delete(id);
        }

        static void Exist()
        {
            ObjectId id = new ObjectId("5c41ba7c73a202e1e4695344");
            var category = (connector.GetPageCriteria(x => x.Id == id) as List<Category>).FirstOrDefault();

            var model = new Category()
            {
                Id = new ObjectId(),
                Name = "Test Kategori",
                Desc = "Desc"
            };

            var result = connector.Exists(model);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        static void GetId()
        {
            var result = connector.GetId(x => x.Name == "Bilgisayar");
            Console.WriteLine(result);
            Console.ReadLine();

        }
    }
}

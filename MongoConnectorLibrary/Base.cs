using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnectorLibrary
{
    public class Base
    {
        [BsonId]
        public ObjectId Id { get; set; }

    }
}

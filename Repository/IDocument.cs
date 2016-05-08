using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Repository
{
    public interface IDocument
    {
        [BsonId]
        Guid Id { get; set; }
    }
}

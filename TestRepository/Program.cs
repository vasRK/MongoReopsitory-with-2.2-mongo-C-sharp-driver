using MongoDB.Bson;
using MongoDB.Driver;
using Repository;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            IMongoRepository repo = new MongoRepository();
            Random rnd = new Random();
            rnd.Next(1, 13);

            ///
            /// Taken from SO
            /// To Create Random String
            ///
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //var stringChars = new char[8];
            //var random = new Random();
            ///
            /// Create some 10k docs to test with.
            ///
            //for (int i = 0; i < 10000; i++ )
            //{
            //    for (int j = 0; j < stringChars.Length; j++)
            //    {
            //        stringChars[j] = chars[random.Next(chars.Length)];
            //    }

            //    var c1 = new Cat()
            //    {
            //        //Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            //        Id = Guid.NewGuid(),
            //        Age =  rnd.Next(1, 13),
            //        Name =new String(stringChars),
            //        Hobbies = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
            //    };

            //    repo.Persist(c1);
            //}

            //Before Update.
            var filter1 = new DocumentFilter<Cat>()
            {
                PageSize = 20,
                Page = 1,
                Filter = Builders<Cat>.Filter.Regex(x => x.Name, BsonRegularExpression.Create(new Regex("Boston"))),//Can use EQ which gives exact match. But this will help in searching.
                Sort = Builders<Cat>.Sort.Ascending(x => x.Name)
            };
            var results1 = repo.Query<Cat>(filter1).ToList();

            //Update with set
            //            var updateDefination = Builders<Cat>.Update.Set(x => x.Name, "Boston").Set(x => x.Hobbies, new List<Guid>() { Guid.NewGuid() });
            //update with push and pull -> this will throw as mongo doesnot support update with push & pull on same array in one request
            //var updateDefination = Builders<Cat>.Update.Push<Guid>(x => x.Hobbies, Guid.NewGuid()).Unset(x => x.Childrens).Pull(x => x.Hobbies, Guid.Parse("b1d31773-498e-4c3e-bbbd-05af8ba09622"));
            //Udate with pull.
            //var updateDefination = Builders<Cat>.Update.Unset(x => x.Childrens).Pull(x => x.Hobbies, Guid.Parse("b1d31773-498e-4c3e-bbbd-05af8ba09622"));
            //one more update with set.
            //var updateDefination = Builders<Cat>.Update.Set(x => x.Reports, new List<Report>() { new Report() { Id = Guid.NewGuid(), Score = 76 } });
            //update with pullFilter to remove docs with query on sub-documents.
            var updateDefination = Builders<Cat>.Update.PullFilter(x => x.Reports, Builders<Report>.Filter.Eq(r => r.Score, 76));
            var result = repo.Update<Cat>(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), updateDefination);

            var filter = new DocumentFilter<Cat>()
            {
                PageSize = 20,
                Page = 1,
                Filter = Builders<Cat>.Filter.Regex(x => x.Name, BsonRegularExpression.Create(new Regex("Boston"))),
                Sort = Builders<Cat>.Sort.Ascending(x => x.Name)
            };
            var results = repo.Query<Cat>(filter).ToList();
        }
    }
}

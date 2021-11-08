using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isskirstytosios.Data.Repositories
{
    public class NeoInsert : IDisposable
    {

        private bool _disposed = false;
        private readonly IDriver _driver;

        ~NeoInsert() => Dispose(false);

        public NeoInsert(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public string GetItems()
        {
            string items;
            using (var session = _driver.Session())
            {
                items = session.WriteTransaction(tx =>
                {
                    var result = tx.Run("MATCH (n:Item) RETURN n");
                    return result.ToString();
                });
            }
            return items;
        }

        public void PrintGreeting(string message)
        {
            using (var session = _driver.Session())
            {
                var greeting = session.WriteTransaction(tx =>
                {
                    var result = tx.Run("CREATE (a:Greeting) " +
                                        "SET a.message = $message " +
                                        "RETURN a.message + ', from node ' + id(a)",
                        new { message });
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(greeting);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _driver?.Dispose();
            }

            _disposed = true;
        }

        public static void Main()
        {
            using (var greeter = new NeoInsert("neo4j+s://neo4jlab.westeurope.cloudapp.azure.com:7687", "neo4j", "neo4jlab123"))
            {
                greeter.GetItems();
            }
        }
    }
}

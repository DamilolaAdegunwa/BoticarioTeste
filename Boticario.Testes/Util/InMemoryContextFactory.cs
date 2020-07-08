using Boticario.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Boticario.Testes.Util
{
    public static class InMemoryContextFactory
    {
        public static BoticarioContext Create()
        {
            var options = new DbContextOptionsBuilder<BoticarioContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new BoticarioContext(options);
        }

        public static IConfiguration CreateConfiguration()
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            return config;
        }
    }
}

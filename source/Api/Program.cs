﻿using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebCore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()                
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:4040")
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }        
    }
}

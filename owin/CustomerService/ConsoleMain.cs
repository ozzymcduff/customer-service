﻿using Microsoft.Owin.Hosting;
using System;

namespace Customers
{
    public class ConsoleMain
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080/";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started");
                Console.ReadKey();
                Console.WriteLine("Stopping");
            }
        }
    }
}

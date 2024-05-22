﻿using Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LoggerProvider
    {
        private static ILoggerFactory _Factory = null;

        public static void ConfigureLogger(ILoggerFactory factory)
        {
           
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            docPath = Path.Combine(docPath, "logs.json");
            FileLoggerProvider provider = new FileLoggerProvider(docPath);
            factory.AddProvider(provider);
        }


        private static ILoggerFactory LoggerFactory {
            get {
                if (_Factory == null) { 
                    _Factory = new LoggerFactory();
                    ConfigureLogger(LoggerFactory);
                } 
                return _Factory;
            } 
            set { _Factory = value; }
        }

        public static ILogger GetLogger()
        {
            return LoggerFactory.CreateLogger(typeof(LoggerProvider));
        }
    


    }
}

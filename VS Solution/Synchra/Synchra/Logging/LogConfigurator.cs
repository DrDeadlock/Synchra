﻿using log4net.Config;
namespace Synchra.Logging
{
    public static class LogConfigurator
    {
        private static readonly string _relPathToXMLConfig
            = "/Configs/loggerConfig.xml";
        public static void Configure()
        {
            string path =
                System.IO.Directory.GetCurrentDirectory()
                + _relPathToXMLConfig;

            log4net.GlobalContext.Properties["LogFileName"] = MockGetLogPath();

            XmlConfigurator.Configure(new System.IO.FileInfo(path));
        }

        private static string MockGetLogPath()
        {
            return @"";
        }
    }
}
﻿namespace PolyvPlayerSDK.Sample.Config
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    [Serializable]
    public class ConfigRoot
    {
        private static string SavePath
        {
            get
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ffmeplay");
                if (Directory.Exists(folder) == false)
                    Directory.CreateDirectory(folder);

                var configFilePath = Path.Combine(folder, "config.xml");
                return configFilePath;
            }
        }

        public string Version { get; set; } = typeof(ConfigRoot).Assembly.GetName().Version.ToString();
        public string FFmpegPath { get; set; } = @"C:\ffmpeg\";
        public List<string> HistoryEntries { get; set; } = new List<string>();

        public static ConfigRoot Load()
        {
            if (File.Exists(SavePath) == false)
            {
                var config = new ConfigRoot();
                config.Save();
            }

            var serializer = new XmlSerializer(typeof(ConfigRoot));
            using (var readStream = File.OpenRead(SavePath))
            {
                var result = serializer.Deserialize(readStream) as ConfigRoot;
                return result;
            }
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(ConfigRoot));
            using (var readStream = File.Open(SavePath, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(readStream, this);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using FolderMapper.Classes;
using Newtonsoft.Json;

namespace FolderMapper.Services
{
    public class MapperService
    {
        /// <summary>
        /// Source directory, when the program is currently
        /// </summary>
        private FmDirectory Source { get;}
        /// <summary>
        /// Where the files will end up
        /// </summary>
        private FmDirectory Destination { get; }
        /// <summary>
        /// Config for storing system settings
        /// </summary>
        private FmConfig SystemConfig { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destinationUrl"></param>
        public MapperService(string sourceDir, string destinationUrl)
        {
            Source = new FmDirectory{Url = sourceDir};
            Destination = new FmDirectory {Url = destinationUrl};
            //Get the config from the .fm folder
            SetupSysConfig();
        }

        /// <summary>
        /// Name given to the config file name
        /// </summary>
        private const string ConfigFileName = "fmConfig";

        /// <summary>
        /// Sets the system config for the program
        /// </summary>
        private void SetupSysConfig()
        {
            //If the foler does not exist we need to create it
            if (!Directory.Exists(Source.Url + "\\.fm"))
            {
                //Create the dir
                var fmDir = Directory.CreateDirectory(Source.Url + "\\.fm");
                //Create the config file
                var file = File.CreateText(Path.Combine(fmDir.FullName, ConfigFileName));

                //Write the json to it
                file.AutoFlush = true;
                file.WriteLine(JsonConvert.SerializeObject(ConfigSeedMethod()));
                //Set the folder to hiddent
                fmDir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            //Update the system config
            SystemConfig = JsonConvert.DeserializeObject<FmConfig>(
                File.ReadAllText(Path.Combine(Source.Url + "\\.fm", ConfigFileName)));
        }

        /// <summary>
        /// This creates the config for the first time
        /// </summary>
        /// <returns></returns>
        private string ConfigSeedMethod()
            => JsonConvert.SerializeObject(
                new FmConfig
                {
                    DestinationDir = Source.Url
                });
    }
}

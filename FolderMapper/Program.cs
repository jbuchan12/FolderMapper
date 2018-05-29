using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderMapper.Services;

namespace FolderMapper
{
    class Program
    {
        /// <summary>
        /// FmDirectory application is running from
        /// </summary>
        private static string CurrentDir { get; set; }

        /// <summary>
        /// Main program entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Set the current app directory
            CurrentDir = Environment.CurrentDirectory;
            //First argument is the program command
            ExecuteCommand(args[0],args[1]);
            //Keep console app open for debug
            var input = Console.ReadLine();
        }

        /// <summary>
        /// Execute the command entered by the user
        /// </summary>
        /// <param name="command"></param>
        /// <param name="arguments"></param>
        private static void ExecuteCommand(string command, params string[] arguments)
        {
            //Get the correct enum for the command entered
            Enum.TryParse<Enums.Commands>(command, out var result);

            switch(result)
            {
                #region Help
                //Help will display all possible commands
                case Enums.Commands.help:
                {
                    //Get all enum commands
                    Enum.GetNames(typeof(Enums.Commands))
                        .ToList()
                        .ForEach(Console.WriteLine);
                    return;
                }
                 
                #endregion

                //Start mapping process
                case Enums.Commands.map:
                {
                    //Create a new mapper service
                    var mapper = new MapperService(CurrentDir,arguments[0]);

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

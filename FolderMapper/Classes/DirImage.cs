using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderMapper.Classes
{
    public class DirImage
    {
        /// <summary>
        /// Unique Id for the given dir
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The url for the starting point of the image
        /// </summary>
        public string RootUrl { get; set; }
        /// <summary>
        /// All the dirs that are below this root dir
        /// </summary>
        public List<DirImage> ChildDirectories { get; set; }
        /// <summary>
        /// All the files that are found at this level
        /// </summary>
        public List<string> ChildFiles { get; set; }
        /// <summary>
        /// Get all the files within all the dirs
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllChildFiles()
            => ChildDirectories
                .SelectMany(x => x.ChildFiles)
                .ToList();

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirImage(string url)
        {
            Id = Guid.NewGuid();

            RootUrl = url;

            ChildFiles = Directory.EnumerateFiles(url)
                .ToList();

            ChildDirectories = Directory.EnumerateDirectories(url)
                .Select(x => new DirImage(x))
                .ToList();
        }


    }
}

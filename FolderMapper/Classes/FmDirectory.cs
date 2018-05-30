using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderMapper.Classes
{
    public class FmDirectory
    {
        /// <summary>
        /// Given url for a directory
        /// </summary>
        public string Url { get; set; }

        public List<string> Files { get; set; }

    }
}

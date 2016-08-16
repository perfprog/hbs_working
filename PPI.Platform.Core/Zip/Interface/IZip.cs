using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Platform.Core.Zip.Interface
{
    public interface IZip
    {
        void AddFiles(string selectionCriteria, string fileDirectory, bool recursive);
        void AddFiles(MemoryStream memoryFile, string fileName);
        void New(string filePath);
        void Save();     
    }
}

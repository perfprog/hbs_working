using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.Composition;

namespace PPI.Platform.Zip
{
    using ICSharpCode.SharpZipLib.Zip;
    using ICSharpCode.SharpZipLib.Core;
    using PPI.Platform.Core.Zip.Interface;
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude = true)]      
    
    [Export(typeof(IZip))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Zip : IZip
    {
        protected ZipOutputStream _zipStream;
        protected string _filePath;
        public Zip()
        {}
        public void New(string filePath)
        {
            _filePath = filePath;
            FileStream fileStream = File.Create(_filePath);
            _zipStream = new ZipOutputStream(fileStream);
            //_zipStream.SetLength(3);
            _zipStream.IsStreamOwner = true;
        }
        public void AddFiles(string selectionCriteria, string fileDirectory, bool recursive)
        {
            string[] files;

            int folderOffset = _filePath.Length + (_filePath.EndsWith("\\") ? 0 : 1);

            if (recursive)
                files = Directory.GetFiles(fileDirectory, selectionCriteria,SearchOption.AllDirectories);
            else
                files = Directory.GetFiles(fileDirectory, selectionCriteria, SearchOption.TopDirectoryOnly);

            foreach (var item in files)
            {
                FileInfo fileInfo = new FileInfo(item);
                string entryName = item.Substring(folderOffset);
                entryName = ZipEntry.CleanName(entryName);
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fileInfo.LastWriteTime;
                newEntry.Size = fileInfo.Length;

                _zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(item))
                {
                    StreamUtils.Copy(streamReader, _zipStream, buffer);
                }                
                _zipStream.CloseEntry();
            }
            //string[] folders = Directory.GetDirectories(fileDirectory);
            //foreach (string folder in folders)
            //{
            //    CompressFolder(folder, _zipStream, folderOffset);
            //}
        }

        public void Save()
        {
            _zipStream.Close();
        }




        public void AddFiles(MemoryStream memoryStream,string fileName)
        {                     
            memoryStream.Position = 0; //set at start of file
            _zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            ZipEntry newEntry = new ZipEntry(fileName);
            newEntry.DateTime = DateTime.Now;
            _zipStream.PutNextEntry(newEntry);

            StreamUtils.Copy(memoryStream, _zipStream, new byte[4096]);
            _zipStream.CloseEntry();

        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PPI.Platform.Mvc.Utility
{
    using PPI.Platform.Core.Utility;
    public class SecureHttpPostedFile : HttpPostedFileBase
    {
        HttpPostedFileBase _baseFile;
        public SecureHttpPostedFile(HttpPostedFileBase httpPostedFileBase)           
        { 
            _baseFile = httpPostedFileBase;
        }

        public override void SaveAs(string filename)
        {
            //Encrypt
            BinaryReader bReader = new BinaryReader(_baseFile.InputStream);
            byte[] bytes = MachineKeyWrapper.Protect(bReader.ReadBytes(this.ContentLength), "files");
            File.WriteAllBytes(filename, bytes);
            //base.SaveAs(filename);
        }
        public override int ContentLength
        {
            get
            {
                return _baseFile.ContentLength;
            }
        }
        public override string ContentType
        {
            get
            {
                return _baseFile.ContentType;
            }
        }
        public override string FileName
        {
            get
            {
                return _baseFile.FileName;
            }
        }
        public override Stream InputStream
        {
            get
            {
                return _baseFile.InputStream;
            }
        }
        public static StreamReader SecureStream(string filename)
        {
            byte[] bytes = MachineKeyWrapper.Unprotect(File.ReadAllBytes(filename),"files");
            return new StreamReader(new MemoryStream(bytes), Encoding.Default);
        }
    }
}

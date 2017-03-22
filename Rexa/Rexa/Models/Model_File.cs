using Rexa.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbController
{
    public class Model_File
    {
        DcDataContext dc = new DcDataContext();
        public List<v_File> Select()
        {
            return dc.v_Files.ToList();
        }
        public bool Insert(string Name, string Type, byte[] File)
        {

            dc.File_Insert(null, Name, Type, File);
            return true;

        }
        public bool Delete(Guid id)
        {
            try
            {
                dc.File_Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(Guid id, string Type, string Name, byte[] Content)
        {
            try
            {
                dc.File_Update(id, Name, Type, Content);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

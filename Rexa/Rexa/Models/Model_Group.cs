using Rexa.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbController
{
    public class Model_Group
    {
        public bool Insert(v_Group props)
        {
            try
            {
                new DcDataContext().Group_Insert(props.ParentId, props.Name, props.Publish, props.AdminUsername);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(v_Group props)
        {
            try
            {
                new DcDataContext().Group_Update(props.Id, props.ParentId, props.Name, props.Publish, props.AdminUsername);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Guid Id)
        {
            try
            {
                new DcDataContext().Group_Delete(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public class MenuItem
        {
            public int Index { get; set; }
            public int Depth { get; set; }
            public v_Group OtherProps { get; set; }
        }

        public List<MenuItem> Select()
        {
            List<MenuItem> Output = new List<MenuItem>();

            v_Group[] Data = new DcDataContext().v_Groups.OrderBy(x => x.Serial).ToArray();
            int TotalItems = Data.Length;
            v_Group[] PrimaryTable = new v_Group[TotalItems];
            v_Group[] SecondaryTable = new v_Group[TotalItems];
            int Index = 0;

            while (Index < TotalItems)
            {
                int a = 0;
               while (a < Data.Length - Index)
                {
                    PrimaryTable[a] = Data[Index + a];
                    a++;
                }
                int b = Index + 0;
                while (b < TotalItems)
                {
                    Data[b] = null;
                    b++;
                }
                v_Group CurrentItem = PrimaryTable[0];
                int c = 0;
                foreach (var item in PrimaryTable)
                {
                    if (item != null)
                    {
                        if (item.ParentId == CurrentItem.Id || item.Id == CurrentItem.Id)
                        {
                        }
                        else
                        {
                    
                                SecondaryTable[c] = item;
                                PrimaryTable[c] = null;
 
                        }
                        c++;
                    }
 

                }
                int d = Index + 0;
                int e = 0;
                foreach (var item in PrimaryTable)
                {
                    if (item != null)
                    {
                        Data[d] = PrimaryTable[e];
                        d++;
                        PrimaryTable[e] = null;
                    }
                    e++;
                }
                int f = 0;
               
                foreach (var item in SecondaryTable)
                {
                    if (item != null)
                    {
                        Data[d] = SecondaryTable[f];
                        d++;
                        SecondaryTable[f] = null;
                    }
                    f++;
                }
                a = b = c = d = e = f = 0;

                Index++;
            }

            int z = 0;
            foreach (var v in Data)
            {

                bool IsCounting = true;
                v_Group currentCountingItem = v;
                int depth = 0;
                while (IsCounting)
                {
                    if (currentCountingItem.ParentId != null)
                    {
                        currentCountingItem = Data.Where(k => k.Id == currentCountingItem.ParentId).First();
                        depth++;
                    }
                    else
                        IsCounting = false;
                }
                Output.Add(new MenuItem { Index = z, Depth = depth, OtherProps = v });
                z++;
            }
            return Output;

        }
    }
}

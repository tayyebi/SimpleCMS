using Rexa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbController
{
    public class Model_Post
    {

        public bool Insert(v_Post props)
        {
            try
            {
                new DcDataContext().Post_Insert(props.Title, props.Abstract, props.Body, props.Publish, props.Date, props.AdminUsername, props.GroupId);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(v_Post props)
        {
            try
            {
                new DcDataContext().Post_Update(props.Id, props.Title, props.Abstract, props.Body, props.Publish, props.Date, props.AdminUsername, props.GroupId);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete (Guid Id)
        {
            try
            {
                new DcDataContext().Post_Delete(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<v_Post> Select()
        {
            return new DcDataContext().v_Posts.ToList();
        }
    }
}

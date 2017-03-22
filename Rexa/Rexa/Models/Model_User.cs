using Rexa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbController
{
    public class Model_User
    {
        public class LoginResult
        {
            public string Fullname { get; set; }
            public string Username { get; set; }
            public bool Success { get; set; }
            public bool IsAdmin { get; set; }
        }

        public IEnumerable<v_User> Select()
        {
            return new DcDataContext().v_Users;
        }

        public LoginResult Login(string Username, string Password)
        {
            try
            {
                var loginResult = new DcDataContext().User_Login(Username, Password).FirstOrDefault();
                return new LoginResult { Username = Username, Success = Convert.ToBoolean(loginResult.Success), Fullname = loginResult.Fullname, IsAdmin = Convert.ToBoolean(loginResult.IsAdmin) };
            }
            catch
            {
                return new LoginResult { Success = false, Fullname = null, Username = Username, IsAdmin = false };
            }
        }

        public bool Insert(v_User props, string Password)
        {
            try
            {
                new DcDataContext().User_Insert(props.Username, Password, props.Fullname, props.IsAdmin);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(string prevUsername, v_User newProps)
        {
            try
            {
                new DcDataContext().User_Update(prevUsername, newProps.Username, newProps.Fullname, newProps.IsAdmin);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(string Username)
        {
            try
            {
                new DcDataContext().User_Delete(Username);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Password(string Username, string prevPassword, string newPassword)
        {
            try
            {
                new DcDataContext().User_Password(Username, prevPassword, newPassword);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
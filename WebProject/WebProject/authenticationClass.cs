using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;
using System.Web.SessionState;

namespace WebProject
{
    public class authenticationClass
    {
        private QuanLiSach1Entities MyDB = new QuanLiSach1Entities();
        public int authenticate(HttpRequestBase Request, HttpSessionStateBase Session)
        {

            object user;
            try
            {
                user = Request.Cookies["username"].Value;
            }
            catch
            {
                return 0;
            }
            if (user == null)
            {
                return 0;
            }
            string userLogin = user.ToString();
            bool[] userLoginState = { false, false };

            bool returnState = false;
            bool userRole = false;
            try
            {
                userLoginState = (bool[])Session[userLogin];
                returnState = userLoginState[0];
                userRole = userLoginState[1];
            }
            catch
            {
                return 0;
            }
            if (!returnState) return 0;
            if (!userRole) return 1;
            return 2;
        }
    }
}
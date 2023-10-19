using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class CurrentSession
    {
        public readonly IHttpContextAccessor Context;
        public CurrentSession(IHttpContextAccessor _Context)
        {
            Context = _Context;
        }

        public void SetCurrentUser(string user) {
            Context.HttpContext.Session.SetString("username", user);
        }

        public string GetCurrentUser() {
            return Context.HttpContext.Session.GetString("username");
        }
        public void LogOut() {
            Context.HttpContext.Session.Remove("username");
        }

    }
}

using Techbart.DB;
using Techbart.DB.Repositories;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AdminDashboard.Models
{
	public class IdentityServices
    {
		public readonly string _identityToken ="admin_techbart_token";

		public bool Login(Controllers.LoginModel loginModel)
        {
            var customer = TechbartRepository.GetCustomerRepository().GetCustomer(loginModel.Email, loginModel.Password) as Customer;
            if (customer == null)
            {
                return false;
            }
            var roles = customer.GetRoles();

            if (roles == null || !roles.Contains("Admin"))
            {
                return false;
            }

            string userData = string.Join("|", roles);

            // Create ticket
            var ticket = new FormsAuthenticationTicket(
                1,
                customer.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(120),
                false,
                userData,
                FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            faCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(faCookie);

            return true;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
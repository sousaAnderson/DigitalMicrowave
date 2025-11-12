using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalMicrowave
{
    public class BasePage : System.Web.UI.Page
    {
        protected string ApiToken { get; private set; }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Session["AuthToken"] == null)
            {                
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {                
                ApiToken = Session["AuthToken"].ToString();
            }
        }
    }
}
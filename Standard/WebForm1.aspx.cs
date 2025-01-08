using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            datepicker.Attributes.Add("readonly", "true");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string k = datepicker.Value;
            Label1.Text = k;
        }
    }
}
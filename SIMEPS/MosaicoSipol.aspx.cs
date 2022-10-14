using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIMEPS
{
    public partial class Mosaico1 : System.Web.UI.Page
    {

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}
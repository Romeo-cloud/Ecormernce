using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;  
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecormerce.User
{
	public partial class User : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)

		{
            // to codes is to create injection to show the carousel only at the homepage that is default.aspx
            if (Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                // load control
                Control sliderUserControl = (Control)Page.LoadControl("SliderUserControl.ascx");
                pnlSliderUC.Controls.Add(sliderUserControl);
            }
        }
	}
}
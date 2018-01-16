using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGEGWebClient
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SGEGService.SGEGPublicServiceClient client = new SGEGService.SGEGPublicServiceClient("BasicHttpBinding_ISGEGPublicService");
            //Label1.Text = client.GetPublicMessage();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SGEGService.SGEGPrivateServiceClient client = new SGEGService.SGEGPrivateServiceClient("NetTcpBinding_ISGEGPrivateService");
            //Label2.Text = client.GetPrivateMessage();
        }
    }
}
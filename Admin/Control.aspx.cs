using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin
{
    public partial class Control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      
        protected void Send_Click(object sender, EventArgs e)
        {
            int state=0;
            if(rbControlYes.Checked){
                state=1;
            }
            DtCms.DBUtility.DbHelperSQL.ExecuteSql("update dt_ControlWeb set WebState="+state);
            Response.Write("<script>alert('操作成功！');</script>");
        }
    }
}
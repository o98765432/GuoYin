using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.Consultant
{
    public partial class CorparationsEdit : System.Web.UI.Page
    {
        int id;
        BLL.Consultant con = new BLL.Consultant();
        DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"].ToString() != "" && Request.QueryString["id"] != null)
            {
                this.id = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
            ShowInfo();

        }


        /// <summary>
        /// 显示信息
        /// </summary>
        public void ShowInfo()
        {
            try
            {
                string sqlWhere = " Id = " + id + "";

                ds = con.getList(sqlWhere);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtCompanyName.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                    txtWebsite.Text = ds.Tables[0].Rows[0]["Requestwebsites"].ToString();
                    txtIndustry.Text = ds.Tables[0].Rows[0]["industry"].ToString();
                    txtExpats.Text = ds.Tables[0].Rows[0]["expatsNumber"].ToString();

                    txtCompayAdd.Text = ds.Tables[0].Rows[0]["companyAdd"].ToString();

                    txtCountry.Text = ds.Tables[0].Rows[0]["country"].ToString();
                    txtCity.Text = ds.Tables[0].Rows[0]["state"].ToString();
                    txtState.Text = ds.Tables[0].Rows[0]["city"].ToString();
                    txtContactorName.Text = ds.Tables[0].Rows[0]["contactorName"].ToString();
                    txtContactorEmail.Text = ds.Tables[0].Rows[0]["contactorEmail"].ToString();
                    txtTelephoneNo.Text = ds.Tables[0].Rows[0]["telePhone"].ToString();
                    txtLearning.Text = ds.Tables[0].Rows[0]["requestLearning"].ToString(); ;

                 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
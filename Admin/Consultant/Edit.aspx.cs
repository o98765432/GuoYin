using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.Consultant
{
    public partial class Edit : System.Web.UI.Page
    {
        int id;
        BLL.Consultant con = new BLL.Consultant();
        DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"].ToString() != "" && Request.QueryString["id"] != null)
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

                ds =  con.getList(sqlWhere);

                if(ds.Tables[0].Rows.Count > 0)
                {

                    #region "基本信息"
                    txtFullName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                    txtNationality.Text = ds.Tables[0].Rows[0]["Nationality"].ToString();
                    txtCompanyName.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                    txtContact.Text = ds.Tables[0].Rows[0]["Contact"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                    txtDate.Text = ds.Tables[0].Rows[0]["addTime"].ToString();
                    txtTelephont.Text = ds.Tables[0].Rows[0]["telePhone"].ToString();
                    txtStayChina.Text = ds.Tables[0].Rows[0]["stayChina"].ToString();
                    txtMandarin.Text = ds.Tables[0].Rows[0]["Mandarin"].ToString();
                    #endregion

                    #region "How did you learn Mandarin?"
                    if (ds.Tables[0].Rows[0]["TrainingSchool"].ToString() == "1")
                    {
                        TrainingSchool.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["TrainingSchool"].ToString() == "2")
                    {
                        this.University.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["TrainingSchool"].ToString() == "3")
                    {
                        PrivateTutor.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["TrainingSchool"].ToString() == "4")
                    {
                        SelfStudy.Checked = true;
                    }
                    #endregion

                    #region "Which district do you live in?"
                    if (ds.Tables[0].Rows[0]["Shekou"].ToString() == "1")
                    {
                        this.Shekou.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Shekou"].ToString() == "2")
                    {
                        this.Nanshan.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Shekou"].ToString() == "3")
                    {
                        this.Futian.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Shekou"].ToString() == "4")
                    {
                        this.Luohu.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Shekou"].ToString() == "5")
                    {
                        this.Baoan.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Shekou"].ToString() == "6")
                    {
                        this.Longgang.Checked = true;
                    }
                    #endregion

                    #region "What's your goal of learning Mandarin? "
                    if (ds.Tables[0].Rows[0]["Business"].ToString() == "1")
                    {
                        this.Business.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Business"].ToString() == "2")
                    {
                        this.DailyLife.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Business"].ToString() == "3")
                    {
                        this.Selfdevelopment.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Business"].ToString() == "4")
                    {
                        this.HSKtest.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Business"].ToString() == "5")
                    {
                        this.MandarinOthers.Checked = true;
                    }
                    #endregion

                    #region "How many hours can you make for Mandarin courses per week?"
                    if (ds.Tables[0].Rows[0]["FourHours"].ToString() == "1")
                    {
                        this.FourHours.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["FourHours"].ToString() == "2")
                    {
                        this.SixHours.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["FourHours"].ToString() == "3")
                    {
                        this.TenHours.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["FourHours"].ToString() == "4")
                    {
                        this.Everydayfordailycourses.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["FourHours"].ToString() == "5")
                    {
                        this.Weekendonly.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["FourHours"].ToString() == "6")
                    {
                        this.weekOthers.Checked = true;
                    }
                    #endregion

                    #region "What's your best time for classes?"
                    if (ds.Tables[0].Rows[0]["Mornings"].ToString() == "1")
                    {
                        this.Mornings.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Mornings"].ToString() == "2")
                    {
                        this.Afternoons.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Mornings"].ToString() == "3")
                    {
                        this.Evenings.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["Mornings"].ToString() == "4")
                    {
                        this.Weekdays.Checked = true;                    
                    }
                    if (ds.Tables[0].Rows[0]["Mornings"].ToString() == "5")
                    {
                        this.Weekends.Checked = true;
                    }

                    #endregion

                    #region "What type of classes are you interested in?"
                    if (ds.Tables[0].Rows[0]["SmallGroupClass"].ToString() == "1")
                    {
                        this.SmallGroupClass.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["SmallGroupClass"].ToString() == "2")
                    {
                        this.StandardGroupClass.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["SmallGroupClass"].ToString() == "3")
                    {
                        this.BigGroupClass.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["SmallGroupClass"].ToString() == "4")
                    {
                        this.PrivateClass.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["SmallGroupClass"].ToString() == "5")
                    {
                        this.ImmersionClass.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["SmallGroupClass"].ToString() == "6")
                    {
                        this.IntensiveClass.Checked = true;
                    }

                    #endregion

                    #region "How did you know about us?"
                    if (ds.Tables[0].Rows[0]["WordofMouth"].ToString() == "1")
                    {
                        this.WordofMouth.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["WordofMouth"].ToString() == "2")
                    {
                        this.BoothAdverts.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["WordofMouth"].ToString() == "3")
                    {
                        this.Referral.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["WordofMouth"].ToString() == "4")
                    {
                        this.Website.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["WordofMouth"].ToString() == "5")
                    {
                        this.aboutOthers.Checked = true;
                    }

                    #endregion

                    txtSomething.Text = ds.Tables[0].Rows[0]["something"].ToString();
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
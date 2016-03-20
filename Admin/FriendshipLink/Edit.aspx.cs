using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web.Admin.FriendshipLink
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        protected int classid;
        protected int drpClassId;
        public int id;
        protected string ver;
        protected string classList;
        protected BLL.FriendshipLink shipLink = new BLL.FriendshipLink();

        protected string strtitle = "添加";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ver = Session["ver"].ToString();
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                this.id = Convert.ToInt32(Request.QueryString["id"]);
            }
            DataSet ds = shipLink.GetClassListById(this.id, ver);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    classList = ds.Tables[0].Rows[i]["ClassList"].ToString();
                }

                this.drpClassId = Convert.ToInt32(classList.Substring(1, 3));

            } 
            if (Request.QueryString["classid"] != null && Request.QueryString["classid"].ToString() != "")
            {
                this.classid = Convert.ToInt32(Request.QueryString["classid"]);
            }

            if (!IsPostBack)
            {
                btnSave.Visible = updateflag;
                chkLoginLevel("editDownloads");
                //绑定类别
                ChannelTreeBind(this.drpClassId, "请选择所属类别...", (int)Channel.FriendshipLink, this.ddlClassId, "cn");

                if (id > 0)
                {
                    strtitle = "修改";
                    getFrindshipInfoById();
                }
            }


          
        }


        /// <summary>
        /// 根据友情链接id获得该条详细信息
        /// </summary>
        public void getFrindshipInfoById()
        {
            try 
            {
                BLL.FriendshipLink shipBll = new BLL.FriendshipLink();
                Model.FriendshipLink shipModel = new Model.FriendshipLink();

                shipModel = shipBll.GetModel(this.id, this.ver);

                if (shipModel != null)
                {
                    this.ddlClassId.SelectedValue = shipModel.ClassId.ToString();
                    txtTitles.Text = shipModel.Title;
                    txtSortId.Text = shipModel.SortId.ToString();
                    txtIslock.Text = shipModel.IsLock.ToString();
                    txtImgUrl.Text = shipModel.ImgUrl;
                    txtHref.Text = shipModel.Href;
                    txtFilepath.Text = shipModel.HtmlPath;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
  
    
        /// <summary>
        /// 根据友情链接ID修改此条信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
   
            try 
            {
                BLL.FriendshipLink Bll = new BLL.FriendshipLink();
                Model.FriendshipLink model = new Model.FriendshipLink();
                model.Id = this.id;
                model.ClassId =Convert.ToInt32(ddlClassId.SelectedValue);
                model.Title = txtTitles.Text;
                model.SortId = Convert.ToInt32(txtSortId.Text);
                model.IsLock = Convert.ToInt32(txtIslock.Text);
                model.ImgUrl = txtImgUrl.Text;
                model.Href = txtHref.Text;
                model.HtmlPath = txtFilepath.Text;
                model.Ver = Session["ver"].ToString();


                SaveLogs("[友情链接模块]编辑文章：" + model.Title);

                //uploadImage();

               

                if (this.id > 0)
                {
                    Bll.Update(model);
                    Response.Write("<script type='text/javascript'>alert('修改成功');location.href='list.aspx?classid=" + classid + "'</script>");
                    Response.End();
                }
                else
                {
                    Bll.Add(model);
                    Response.Write("<script type='text/javascript'>alert('添加成功');location.href='list.aspx?classid=" + classid + "'</script>");
                    Response.End();
                }

            
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlClassId.SelectedIndex > 0)
            {
                DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
                DtCms.Model.Channel model = new DtCms.Model.Channel();
                model = bll.GetModel(int.Parse(this.ddlClassId.SelectedValue), "cn");

                // this.txtFilepath.Text = model.Filepath;

            }

        }
    
    }
}
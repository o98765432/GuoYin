using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace DtCms.Web.Admin.Network
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                     //总条数
        public int page;                       //当前页
        public int pagesize;                   //设置每页显示的大小

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pagesize = webset.LinkPageNum;
            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewLinks");
                this.RptBind(" WebUrl in('dongbei','huabei','xibei','huadong','huazhong','xinan','huanan','taiwan') and ver='"+Session["ver"].ToString()+"'");
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            //获得总条数
            this.pcount = bll.GetCount(strWhere);
            if (this.pcount > 0)
            {
                this.lbtnDel.Enabled = true;
            }
            else
            {
                this.lbtnDel.Enabled = false;
            }

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, "AddTime desc");
            this.rptList.DataBind();
        }
        #endregion

        //生成动画
        protected void lbtnAudit_Click(object sender, EventArgs e)
        {
            string filename = "mapjq.xml";
            DtCms.BLL.Links bll = new DtCms.BLL.Links();

            SortedList hash = new SortedList();
           
         
            hash.Add("dongbei", "东北地区");
            hash.Add("huabei", "华北地区");
            hash.Add("xibei", "西北地区");
            hash.Add("xinan", "西南地区");
            hash.Add("huazhong", "华中地区");
            hash.Add("huadong", "华东地区");
            hash.Add("huanan", "华南地区");
            hash.Add("taiwan", "台湾地区");

            XmlDocument xmlDoc = new XmlDocument();
            string filepath = Server.MapPath("../../xml/" + filename);
            xmlDoc.Load(filepath);
            XmlNode map = xmlDoc.SelectSingleNode("map");
            map.RemoveAll();
            XmlAttribute total = xmlDoc.CreateAttribute("total");
            total.Value = "36";
            map.Attributes.Append(total);

            Dictionary<string, string> dics = new Dictionary<string, string>();
            dics.Add("dongbei", "东北地区");
            dics.Add("huabei", "华北地区");
            dics.Add("xibei", "西北地区");
            dics.Add("xinan", "西南地区");
            dics.Add("huazhong", "华中地区");
            dics.Add("huadong", "华东地区");
            dics.Add("huanan", "华南地区");
            dics.Add("taiwan", "台湾地区");
         
            foreach (var dic in dics)
            {
                XmlElement CN = xmlDoc.CreateElement("CN");
                XmlAttribute cnName = xmlDoc.CreateAttribute("name");
                cnName.Value = dic.Value;
                CN.Attributes.Append(cnName);
                DataSet ds = bll.GetList(" WebUrl='"+dic.Key+"'");
                foreach (DataRow dr_map in ds.Tables[0].Rows)
                {
                    XmlElement site = xmlDoc.CreateElement("site");
                    XmlAttribute siteName = xmlDoc.CreateAttribute("name");
                    siteName.Value = dr_map["Title"].ToString();
                    XmlAttribute haoma = xmlDoc.CreateAttribute("haoma");
                    haoma.Value = dr_map["UserTel"].ToString();
                    XmlAttribute dizhi = xmlDoc.CreateAttribute("dizhi");
                    dizhi.Value = dr_map["UserMail"].ToString();

                    site.Attributes.Append(siteName);
                    site.Attributes.Append(haoma);
                    site.Attributes.Append(dizhi);
                    CN.AppendChild(site);

                }
                map.AppendChild(CN);
            }

            

            xmlDoc.Save(filepath);
            JscriptPrint("生成动画成功！", "List.aspx", "Success");
        }

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delLinks");
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    //删除图片
                    DeleteFile(bll.GetModel(id).ImgUrl);
                    //保存日志
                    SaveLogs("[链接管理]删除链接：" + bll.GetModel(id).Title);
                    //删除记录
                    bll.Delete(id);
                }
            }
            JscriptPrint("批量删除成功啦！", "List.aspx", "Success");
        }

        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (ddlClassId.SelectedIndex == 0) {
                this.RptBind(" WebUrl in('dongbei','huabei','xibei','huadong','huazhong','xinan','huanan','taiwan')");
            }
            else
            {
                this.RptBind(" WebUrl = '"+this.ddlClassId.SelectedValue+"'");
            }
        }
    }
}

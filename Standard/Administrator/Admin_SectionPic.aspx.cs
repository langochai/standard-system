using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard.Administrator
{
    public partial class Admin_SectionPic : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StandardConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                // Response.Redirect("../Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                Response.Redirect("../Login.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.AbsoluteUri);

            }
            else if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["permit"].ToString()) >= 2)
                {

                    alert_permit.Attributes.CssStyle.Add("display", "");
                    content.Attributes.CssStyle.Add("display", "none");
                    alert_permit.InnerHtml = "Tài khoản của bạn không thể vào chức năng này";

                }
                else if (Convert.ToInt32(Session["permit"].ToString()) < 2)
                {
                    content.Attributes.CssStyle.Add("display", "");
                    alert_permit.Attributes.CssStyle.Add("display", "none");
                    loadGV("");
                }

            }

        }

        DataTable dt_GV( string item)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Seach_SecPic", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@item", item);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        protected void GV_SectionPic_RowEditing(object sender, GridViewEditEventArgs e)
        {
            alert.InnerHtml = "";
            GV_SectionPic.EditIndex = e.NewEditIndex;

            loadGV(txt_search.Text.Trim());
        }

        protected void GV_SectionPic_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            alert.InnerHtml = "";
            GV_SectionPic.EditIndex = -1;
            loadGV(txt_search.Text.Trim());
        }

        protected void GV_SectionPic_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            alert.InnerHtml = "";
            string factory = (GV_SectionPic.Rows[e.RowIndex].FindControl("factory") as DropDownList).SelectedValue.Trim();
            string pic = (GV_SectionPic.Rows[e.RowIndex].FindControl("txt_pic") as TextBox).Text.Trim();
            string AM = (GV_SectionPic.Rows[e.RowIndex].FindControl("txt_AM") as TextBox).Text.Trim();
            string id = (GV_SectionPic.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Update_SecPic", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@factory", factory);
            cmd.Parameters.AddWithValue("@pic_username", pic);
            cmd.Parameters.AddWithValue("@Am_above", AM);
            cmd.Parameters.AddWithValue("@dep_c", id);
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
            cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"]);

            cmd.ExecuteNonQuery();
            con.Close();
            GV_SectionPic.EditIndex = -1;
            loadGV(txt_search.Text.Trim());
        }

        protected void GV_SectionPic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                string factory = (GV_SectionPic.FooterRow.FindControl("dr_factory") as DropDownList).SelectedValue.Trim();
                string dep = (GV_SectionPic.FooterRow.FindControl("txt_depfooter") as TextBox).Text.Trim();
                string pic = (GV_SectionPic.FooterRow.FindControl("txt_Picfooter") as TextBox).Text.Trim();
                string AM = (GV_SectionPic.FooterRow.FindControl("txt_AMfooter") as TextBox).Text.Trim();
                con.Open();
                string qr = "select count (*) from Dep_mst where dep_c='" + dep + "'";
                SqlCommand cmd_count = new SqlCommand(qr, con);
                string count = cmd_count.ExecuteScalar().ToString();
                con.Close();
                if (dep == "" || pic=="" || AM=="")
                {
                    alert.InnerHtml = "Các thông tin không được để trống";
                }
                
                else if(count != "0")
                {
                    alert.InnerHtml = "Không thể thêm! Bộ phận này đã tồn tại trước đó";
                }
                else
                {
                    alert.InnerHtml = "";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Admin_Add_SecPic", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dep_c", dep);
                    cmd.Parameters.AddWithValue("@factory", factory);
                    cmd.Parameters.AddWithValue("@pic_username", pic);
                    cmd.Parameters.AddWithValue("@AM_above", AM);
                    cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"]);
                    cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                    loadGV(txt_search.Text.Trim());

                }
               
            }
        }

        protected void GV_SectionPic_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = (GV_SectionPic.DataKeys[e.RowIndex].Value.ToString());
            try
            {
                con.Open();
                alert.InnerHtml = "";
                string qr_dele = "delete Dep_mst where dep_c='" + id + "' ";
                SqlCommand cmd_dele = new SqlCommand(qr_dele, con);
                cmd_dele.ExecuteNonQuery();
                con.Close();
                loadGV(txt_search.Text.Trim());
            }
            catch (Exception ex)
            {
                alert.InnerHtml = ex.Message;
            }
            finally
            {
                con.Close();
            }
            

        }

        void loadGV(string searchitem)
        {
            DataTable dt = new DataTable();
            dt= dt_GV(searchitem);
            GV_SectionPic.DataSource = dt;
            GV_SectionPic.DataBind();
            lblTongsodongGV1.Text = GV_SectionPic.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";
        }

        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            loadGV(txt_search.Text.Trim());
            txt_search.Focus();


        }

        protected void GV_SectionPic_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_SectionPic.PageIndex = e.NewPageIndex;

            loadGV(txt_search.Text.Trim());
            alert.InnerHtml = "";
        }
    }
}
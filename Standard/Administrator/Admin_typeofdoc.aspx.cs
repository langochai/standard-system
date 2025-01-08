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
    public partial class Admin_typeofdoc : System.Web.UI.Page
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
        DataTable dt_GV(string doc_type_nm)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Search_Type", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@doc_type_nm", doc_type_nm);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            alert.InnerHtml = "";
            loadGV(txt_search.Text.Trim());
            txt_search.Focus();
        }

        protected void GV_TypeofDoc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            alert.InnerHtml = "";
            GV_TypeofDoc.EditIndex = e.NewEditIndex;
            loadGV(txt_search.Text.Trim());
        }

        protected void GV_TypeofDoc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            alert.InnerHtml = "";
            string a = (GV_TypeofDoc.Rows[e.RowIndex].FindControl("txt_doc_typenm") as TextBox).Text.Trim();
            string id = (GV_TypeofDoc.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_updateType", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@doc_type_id", id);
            cmd.Parameters.AddWithValue("@doc_type_nm", a);
            cmd.ExecuteNonQuery();
            con.Close();
            GV_TypeofDoc.EditIndex = -1;
            loadGV(txt_search.Text.Trim());


        }

        protected void GV_TypeofDoc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            alert.InnerHtml = "";
            GV_TypeofDoc.EditIndex = -1;
            loadGV(txt_search.Text.Trim());
        }

        protected void GV_TypeofDoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = (GV_TypeofDoc.DataKeys[e.RowIndex].Value.ToString());
            string qr = "select count(*) from SVWSDocument where (delete_f is not null or delete_f != '0') and doc_type_id ='" + id + "'";
            con.Open();
            SqlCommand cmd_ck = new SqlCommand(qr, con);
            int ck_duplicate = Convert.ToInt32( cmd_ck.ExecuteScalar().ToString());
            if(ck_duplicate > 0)
            {
                alert.InnerHtml = "Không thành công! Bạn không thể xóa loại tài liệu này vì đang tồn tại tiêu chuẩn liên quan";
                con.Close();
            }
            else
            {
                alert.InnerHtml = "";
                string qr_dele = "delete Document_type_mst where doc_type_id='" + id + "' ";
                SqlCommand cmd_dele = new SqlCommand(qr_dele,con);
                cmd_dele.ExecuteNonQuery();
                con.Close();
                loadGV(txt_search.Text.Trim());

            }
           
        }

        protected void GV_TypeofDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "AddNew")
            {
                string a = (GV_TypeofDoc.FooterRow.FindControl("txt_doc_typenm_footer") as TextBox).Text.Trim();
                if (a.Trim() == "")
                {
                    alert.InnerHtml = "Các thông tin không được để trống";
                }
                else
                {
                    alert.InnerHtml = "";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Admin_AddType", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@doc_type_nm", a);
                    cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"]);
                    cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadGV(txt_search.Text.Trim());

                }

            }
        }

        void loadGV(string searchitem)
        {
            DataTable dt = new DataTable();
            dt= dt_GV(searchitem);
            GV_TypeofDoc.DataSource = dt;
            GV_TypeofDoc.DataBind();
            lblTongsodongGV1.Text = GV_TypeofDoc.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";
        }

        protected void GV_TypeofDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_TypeofDoc.PageIndex = e.NewPageIndex;

            loadGV(txt_search.Text.Trim());
            alert.InnerHtml = "";
        }
    }
}
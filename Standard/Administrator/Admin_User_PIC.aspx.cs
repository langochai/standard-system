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
    public partial class Admin_User_PIC : System.Web.UI.Page
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
                
                 if (Convert.ToInt32(Session["permit"].ToString()) == 2)
                {
                    content.Attributes.CssStyle.Add("display", "");
                    alert_permit.Attributes.CssStyle.Add("display", "none");

                    loadGV("");
                }

            }

        }

        DataTable dt_GV(string item)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_PICSearch_Users", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@item", item);
            cmd.Parameters.AddWithValue("@section", Session["dep_c"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        protected void GV_User_RowEditing(object sender, GridViewEditEventArgs e)
        {
            alert.InnerHtml = "";
            GV_User.EditIndex = e.NewEditIndex;
            loadGV(txt_search.Text.Trim());

        }

        protected void GV_User_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            alert.InnerHtml = "";
            GV_User.EditIndex = -1;
            loadGV(txt_search.Text.Trim());

        }

        protected void GV_User_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            alert.InnerHtml = "";
            string pass = (GV_User.Rows[e.RowIndex].FindControl("txt_pass") as TextBox).Text.Trim();
            string dep_c = (GV_User.Rows[e.RowIndex].FindControl("dr_dep") as DropDownList).SelectedValue.Trim();
            string email = (GV_User.Rows[e.RowIndex].FindControl("txt_email") as TextBox).Text.Trim();
            string permit = (GV_User.Rows[e.RowIndex].FindControl("dr_permit") as DropDownList).SelectedValue.Trim();
            string id = (GV_User.DataKeys[e.RowIndex].Value.ToString());
            if (pass == "" || dep_c == "" || email == "" || permit == "")
            {
                alert.InnerHtml = "Các thông tin không được để trống";
            }
            else
            {
                alert.InnerHtml = "";
                con.Open();
                SqlCommand cmd = new SqlCommand("Admin_Update_User", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@password", pass);
                cmd.Parameters.AddWithValue("@dep_c", dep_c);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@permit", permit);
                cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
                cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
                cmd.Parameters.AddWithValue("@user_id", id);

                cmd.ExecuteNonQuery();
                con.Close();
                GV_User.EditIndex = -1;
                loadGV(txt_search.Text.Trim());

            }

        }

        protected void GV_User_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                string user = (GV_User.FooterRow.FindControl("txt_userfooter") as TextBox).Text.Trim();
                string pass = (GV_User.FooterRow.FindControl("txt_passfooter") as TextBox).Text.Trim();
                string dep = (GV_User.FooterRow.FindControl("dr_depfooter") as DropDownList).SelectedValue.Trim();
                string email = (GV_User.FooterRow.FindControl("txt_emailfooter") as TextBox).Text.Trim();
                string permit = (GV_User.FooterRow.FindControl("dr_permitfooter") as DropDownList).SelectedValue.Trim();
                con.Open();
                string qr = "select count(*) from User_mst where user_id='" + user + "'";
                SqlCommand cmd_count = new SqlCommand(qr, con);
                string count = cmd_count.ExecuteScalar().ToString() ;
                con.Close();
                if (user == "" || pass == "" || dep == "" || email == "" || permit == "")
                {
                    alert.InnerHtml = "Các thông tin không được để trống";
                }
                else if (count != "0")
                {
                    alert.InnerHtml = "Không thành công! User này đã tồn tại";
                }
                else
                {
                    alert.InnerHtml = "";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Admin_Add_User", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", user);
                    cmd.Parameters.AddWithValue("@password", pass);
                    cmd.Parameters.AddWithValue("@dep_c", dep);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@permit", permit);
                    cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"]);
                    cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    loadGV(txt_search.Text.Trim());

                }
                

            }
        }

        protected void GV_User_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = (GV_User.DataKeys[e.RowIndex].Value.ToString());
            try
            {
                con.Open();
                alert.InnerHtml = "";
                string qr_dup = "select count(*) from Dep_mst where pic_username='" + id + "' or AM_above='" + id + "'";
                SqlCommand cmd_dup = new SqlCommand(qr_dup, con);
                int ck = Convert.ToInt32(cmd_dup.ExecuteScalar().ToString().Trim());
                if (ck > 0)
                {
                    alert.InnerHtml = "Không thể xóa! User này đang là PIC hoặc AM của bộ phận.";
                }
                else
                {
                    alert.InnerHtml = "";
                    string qr_dele = "delete User_mst where user_id='" + id + "' ";
                    SqlCommand cmd_dele = new SqlCommand(qr_dele, con);
                    cmd_dele.ExecuteNonQuery();
                    con.Close();
                    loadGV(txt_search.Text.Trim());
                }

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
            dt = dt_GV(searchitem);

            if (dt.Rows.Count > 0)
            {
                GV_User.DataSource = dt;
                GV_User.DataBind();
                lblTongsodongGV1.Text = GV_User.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";
            }
            else
            {
                BindDummyRow();
            }
            
        }

        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            dummy.Columns.Add("user_id");
            dummy.Columns.Add("password");
            dummy.Columns.Add("dep_c");
            dummy.Columns.Add("email");
            dummy.Columns.Add("permit_nm");
            dummy.Columns.Add("fullname");
            dummy.Columns.Add("STT");

            //dummy.Columns.Add("first_serial");
            dummy.Rows.Add();
            GV_User.DataSource = dummy;
            GV_User.DataBind();
            lblTongsodongGV1.Text = GV_User.Rows.Count.ToString() + "/" + dummy.Rows.Count.ToString() + " bản ghi";

            //lbltongsodongfooter.Text = GridView1.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";


        }

        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            loadGV(txt_search.Text.Trim());
        }

        protected void GV_User_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_User.PageIndex = e.NewPageIndex;

            loadGV(txt_search.Text.Trim());
            alert.InnerHtml = "";
        }
    }
}
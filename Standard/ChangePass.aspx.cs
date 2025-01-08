using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard
{
    public partial class ChangePass : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StandardConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                // Response.Redirect("../Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                Response.Redirect("../Login.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.AbsoluteUri);

            }

        }

        int is_rightpass(string user, string pass)
        {
            int check = 0;
            con.Open();
            string qr = "select count(*) from User_mst where user_id='" + user + "' and password='" + pass + "'";
            SqlCommand cmd = new SqlCommand(qr, con);
            check = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            return check;
        }

        int is_matchPassword(string curentpass, string confpass)
        {
            int check = 0;
            if(curentpass != confpass)
            {
                check = 0;
            }
            else
            {
                check = 1;
            }
            return check;
        }

       

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            if(is_rightpass(txt_user.Text.Trim(), txt_curpass.Text.Trim()) == 0)
            {
                alert.Attributes.Add("class", "text-danger");
                alert.InnerHtml = "Thông tin user hoặc password không đúng";
            }
            //else if(is_matchPassword(txt_newpass.Text.Trim(), txt_confrmPass.Text.Trim())==0)
            //{
            //    alert.InnerHtml = "Password xác nhận không khớp";
            //}
            else if(txt_newpass.Text.Length < 8)
            {
                alert.Attributes.Add("class", "text-danger");
                alert.InnerHtml = "Password mới cần ít nhất 8 kí tự";
            }

            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Changepass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_id", txt_user.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txt_newpass.Text.Trim());
                cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
                cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
                cmd.ExecuteNonQuery();
                con.Close();
                alert.InnerHtml = "Đã đổi mật khẩu thành công!";
                alert.Attributes.Add("class", "text-success");
            }
        }
    }
}
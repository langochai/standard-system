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
    public partial class SVWSDocPrivate_Create : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StandardConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                // Response.Redirect("../Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                Response.Redirect("../Login.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.AbsoluteUri);

            }
            if (!IsPostBack)
            {
                txt_apply_dt.Attributes.Add("readonly", "true");
                txt_expire_dt.Attributes.Add("readonly", "true");
            }

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string des_vi = txt_des_vi.Text.Trim();
            des_vi = des_vi.Replace("\r\n", "<br/>");
            string des_en = txt_des_en.Text.Trim();
            des_en=des_en.Replace("\r\n", "<br/>");
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_PRV_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_prv_c", txt_doc_c.Text.Trim());
            cmd.Parameters.AddWithValue("@svws_prv_ver", txt_ver.Text.Trim());
            cmd.Parameters.AddWithValue("@svws_prv_nm_vi", txt_title_vi.Text.Trim());
            cmd.Parameters.AddWithValue("@svws_prv_nm_eng", txt_title_en.Text.Trim());
            //cmd.Parameters.AddWithValue("@doc_type_id", dr_type_doc.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@detail_vi", des_vi);
            cmd.Parameters.AddWithValue("@detail_eng", des_en);
            cmd.Parameters.AddWithValue("@make_dep_c", dr_sec_make.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@apply_dt", txt_apply_dt.Text.Trim());
            cmd.Parameters.AddWithValue("@finish_dt", txt_expire_dt.Text.Trim() == "" ? null : txt_expire_dt.Text.Trim());
            cmd.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
            cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"]);
            cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
           
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("../SVWSDocPrivate/SVWSDocPrivate.aspx");
        }
    }
}
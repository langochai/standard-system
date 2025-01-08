using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard.SVWSDocPrivate
{
    public partial class SVWSDocPrivate_Edit : System.Web.UI.Page
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
                load_infor(Request.QueryString["doc_id"]);
            }

        }
        void load_infor(string doc_id)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SVWS_LoadPRV_Detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_prv_id", doc_id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            //dr_type_doc.SelectedValue = dt.Rows[0]["doc_type_id"].ToString();
            txt_title_vi.Text = dt.Rows[0]["svws_prv_nm_vi"].ToString();
            txt_title_en.Text = dt.Rows[0]["svws_prv_nm_eng"].ToString();
            txt_doc_c.Text = dt.Rows[0]["svws_prv_c"].ToString();
            txt_expire_dt.Text = dt.Rows[0]["finish_dt"].ToString();
            dr_sec_make.SelectedValue = dt.Rows[0]["make_dep_c"].ToString();
            txt_apply_dt.Text = dt.Rows[0]["apply_dt"].ToString();
            txt_ver.Text = dt.Rows[0]["svws_prv_ver"].ToString();

            txt_des_vi.Text = dt.Rows[0]["detail_vi"].ToString().Replace("<br/>", "\n"); 
            txt_des_en.Text = dt.Rows[0]["detail_eng"].ToString().Replace("<br/>", "\n");

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string des_vi = txt_des_vi.Text.Trim();
            des_vi = des_vi.Replace("\r\n", "<br/>");
            string des_en = txt_des_en.Text.Trim();
            des_en = des_en.Replace("\r\n", "<br/>");

            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_PRV_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_prv_id", Request.QueryString["doc_id"]);
            cmd.Parameters.AddWithValue("@svws_prv_c", txt_doc_c.Text);
            cmd.Parameters.AddWithValue("@svws_prv_ver", txt_ver.Text);
            cmd.Parameters.AddWithValue("@svws_prv_nm_vi", txt_title_vi.Text);
            cmd.Parameters.AddWithValue("@svws_prv_nm_eng", txt_title_en.Text);
            //cmd.Parameters.AddWithValue("@doc_type_id", dr_type_doc.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@detail_vi", des_vi);
            cmd.Parameters.AddWithValue("@detail_eng", des_en);
            cmd.Parameters.AddWithValue("@make_dep_c", dr_sec_make.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@apply_dt", txt_apply_dt.Text);
            cmd.Parameters.AddWithValue("@finish_dt", txt_expire_dt.Text==""? null : txt_expire_dt.Text);
            cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
            cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
            cmd.ExecuteNonQuery();


            con.Close();
            Response.Redirect("../SVWSDocPrivate/SVWSDocPrivate.aspx");
        }
    }
}
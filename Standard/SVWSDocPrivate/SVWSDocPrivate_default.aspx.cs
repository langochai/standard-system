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
    public partial class SVWSDocPrivate_default : System.Web.UI.Page
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
                GridView1.DataSource = dt_GV("", "", Session["dep_c"].ToString());
                GridView1.DataBind();
            }

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = dt_GV(txt_doc_c.Text, txt_doc_nm.Text, dr_dep.SelectedValue.Trim());
            GridView1.DataBind();

        }

        DataTable dt_GV(string svws_prv_c, string svws_prv_nm_vi, string make_dep_c)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_LoadPRV_active", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_prv_c", svws_prv_c);
            cmd.Parameters.AddWithValue("@svws_prv_nm_vi", svws_prv_nm_vi);
            cmd.Parameters.AddWithValue("@make_dep_c", make_dep_c);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            con.Close();
            return dt;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                string doc_id = e.CommandArgument.ToString().Trim();
                Response.Redirect("../SVWSDocPrivate/SVWSDocPrivate_Detail.aspx?doc_id=" + doc_id);
            }
        }
    }
}
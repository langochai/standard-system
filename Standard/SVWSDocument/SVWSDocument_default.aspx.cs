using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard.SVWSDocument
{
    public partial class SVWSDocument_default : System.Web.UI.Page
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
                GridView1.DataSource = dt_GV("", "", "", "");
                GridView1.DataBind();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                string doc_id = e.CommandArgument.ToString().Trim();
                Response.Redirect("../SVWSDocument/SVWSDocument_Detail.aspx?doc_id=" + doc_id);
            }
        }

        DataTable dt_GV(string svws_std_c, string svws_std_nm_vi, string make_dep_c, string doc_type_id)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_load_Document_active", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_std_c", svws_std_c);
            cmd.Parameters.AddWithValue("@svws_std_nm_vi", svws_std_nm_vi);
            cmd.Parameters.AddWithValue("@make_dep_c", make_dep_c);
            cmd.Parameters.AddWithValue("@doc_type_id", doc_type_id);
            cmd.Parameters.AddWithValue("@username", Session["username"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            con.Close();
            return dt;
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = dt_GV(txt_doc_c.Text, txt_doc_nm.Text, dr_dep.SelectedValue.Trim(), dr_type_doc.SelectedValue.Trim());
            GridView1.DataBind();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Access the HiddenField value
                HiddenField hiddenActiveF = (HiddenField)e.Row.FindControl("HiddenActiveF");
                HiddenField hasDeclinedControl = (HiddenField)e.Row.FindControl("HiddenHasDeclinedF");
                string active_f = hiddenActiveF.Value;
                bool hasDeclined = hasDeclinedControl.Value == "1";
                if (active_f == "0")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(252, 213, 180);
                }
                if (hasDeclined)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 69, 0);
                    e.Row.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = dt_GV("", "", "", "");
            GridView1.DataBind();
            alert.InnerHtml = "";
        }
    }
}
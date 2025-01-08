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
    public partial class SVWSDocPrivate_Detail : System.Web.UI.Page
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
                load_detail(Request.QueryString["doc_id"].ToString());
            }

        }
        void load_detail(string doc_id) 
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SVWS_LoadPRV_Detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_prv_id", doc_id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            if (dt.Rows.Count == 0)
            {
                alert.InnerHtml = "Tài liệu không tồn tại";
                alert.Attributes.Add("class", "text-danger");
            }
            else
            {
                alert.InnerHtml = "";
                //lbl_typeofStd.Text = dt.Rows[0]["doc_type_nm"].ToString();
                lbl_stdName_vi.Text = dt.Rows[0]["svws_prv_nm_vi"].ToString();
                lbl_stdName_en.Text = dt.Rows[0]["svws_prv_nm_eng"].ToString();
                lbl_stdCd.Text = dt.Rows[0]["svws_prv_c"].ToString();
                lbl_duedate.Text = dt.Rows[0]["finish_dt"].ToString();
                lbl_dep_make.Text = dt.Rows[0]["make_dep_c"].ToString();
                lbl_apply_dt.Text = dt.Rows[0]["apply_dt"].ToString();
                lbl_ver.Text = dt.Rows[0]["svws_prv_ver"].ToString();
                //lbl_dep.Text = dt.Rows[0]["dep_c"].ToString();
                //des_vi.InnerHtml = dt.Rows[0]["detail_vi"].ToString();
                des_vi.InnerHtml = $"<pre>{dt.Rows[0]["detail_vi"].ToString()}</pre>";
                des_en.InnerHtml = dt.Rows[0]["detail_eng"].ToString();
                loadGV_file(doc_id);


            }



        }

        void loadGV_file(string doc_id)
        {
            string qr = "select svws_prv_file_id, REVERSE(SUBSTRING(REVERSE(link_file),0,CHARINDEX('/',REVERSE(link_file)))) as link_file from SVWSDocPrivate_file where svws_prv_id ='" + doc_id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(qr, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            GV_file.DataSource = dt;
            GV_file.DataBind();
        }

        protected void GV_file_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                con.Open();
                string id = e.CommandArgument.ToString().Trim();
                string qr = "select link_file from SVWSDocPrivate_file where svws_prv_file_id='" + id + "'";
                SqlCommand cmd = new SqlCommand(qr, con);
                string link_file = cmd.ExecuteScalar().ToString().Trim();
                con.Close();
                string path1 = Server.MapPath(link_file);

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();

                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("Content-Disposition", "attachment; filename=" + link_file.Substring(link_file.LastIndexOf("/") + 1));
                response.ContentType = "application/octet-stream";
                response.WriteFile(path1);
                response.Flush();
                response.End();

            }
        }
    }
}
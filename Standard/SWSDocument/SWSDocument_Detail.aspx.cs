using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard.SWSDocument
{
    public partial class SWSDocument_Detail : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StandardConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["username"] == null)
                {
                    // Response.Redirect("../Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                    Response.Redirect("../Login.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.AbsoluteUri);

                }
                alert.Attributes.Add("css", "");
                alert.InnerHtml = "";

            }
            else if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("../Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                }
                else
                {
                    lbl_id.Text = Request.QueryString["sws_doc_id"];
                    load_information(lbl_id.Text.Trim());
                    load_GVFile(lbl_id.Text.Trim());
                    load_GVSec(lbl_id.Text.Trim());
                }
            }

        }


        void load_information(string id)
        {
            lbl_swsDocument_cd.Visible = true;
            lbl_ver.Visible = true;
            lbl_nameVi.Visible = true;
            lbl_nameEN.Visible = true;
            lbl_Receive_dt.Visible = true;
            lbl_comment.Visible = true;


            con.Open();

            
            SqlCommand cmd = new SqlCommand("SWS_Document_Detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("sws_doc_id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string k = dt.Rows[0]["active_f"].ToString().Trim();

           
           

            lbl_swsDocument_cd.Text = dt.Rows[0]["sws_standard_c"].ToString().Trim();
            lbl_ver.Text = dt.Rows[0]["sws_standard_ver"].ToString().Trim();
            lbl_nameVi.Text = dt.Rows[0]["sws_standard_nm_vi"].ToString().Trim();
            lbl_nameEN.Text = dt.Rows[0]["sws_standard_nm_en"].ToString().Trim();
            lbl_Receive_dt.Text = dt.Rows[0]["receive_dt"].ToString().Trim();
            lbl_comment.Text = dt.Rows[0]["comment"].ToString().Trim();
            con.Close();
        }

        void load_GVFile(string id)
        {
            con.Open();
            string qr = "select swsDoc_file_id, REVERSE(SUBSTRING(REVERSE(link_file),0,CHARINDEX('/',REVERSE(link_file)))) as link_file from SWSDocument_file where swsDoc_id='" + id + "'";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                con.Open();
                string id = e.CommandArgument.ToString().Trim();
                string qr = "  select link_file from SWSDocument_file where swsDoc_file_id='" + id + "'";
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

            con.Open();
            string qr_link = "select link_file from SWSDocument_file where swsDoc_file_id='" + id + "'";
            SqlCommand cmd_link = new SqlCommand(qr_link, con);
            string myPath = cmd_link.ExecuteScalar().ToString();
            myPath = Server.MapPath(myPath);

            string qr = "delete SWSDocument_file where swsDoc_file_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(qr, con);
            cmd.ExecuteNonQuery();


            con.Close();
            File.Delete(myPath);
            load_GVFile(Request.QueryString["sws_doc_id"]);
            //loadGVFile(key);
        }

        void load_GVSec(string id)
        {
            if (id != "")
            {
                con.Open();
                string qr = "select swsDocument_relate_id, dep_c from SWSDocument_RelateDep where sws_doc_id='" + id + "' and [sws_status_c] !=7";
                SqlCommand cmd = new SqlCommand(qr, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GV_section.DataSource = dt;
                GV_section.DataBind();
                con.Close();

            }

        }


   








        

        int check_Section(string sws_doc_id)
        {
            int check = 0;
            string qr = "select count(*) from SWSDocument_RelateDep a inner join SWSDocument b on a.sws_doc_id=b.sws_doc_id where a.sws_doc_id='" + sws_doc_id + "' and a.sws_status_c !=7";
            con.Open();
            SqlCommand cmd = new SqlCommand(qr, con);
            check = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return check;
        }
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            if (check_Section(Request.QueryString["sws_doc_id"]) > 0)
            {
                alert.Attributes.Add("class", "alert_danger");
                alert.InnerHtml = "Không thể xóa! Bạn cần xóa bộ phận liên quan trước.";
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SWS_deleteDocument", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
                cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
                cmd.Parameters.AddWithValue("@sws_doc_id", Request.QueryString["sws_doc_id"]);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/SWSDocument/SWSDocument.aspx");
            }
        }
        int status_Section(string swsDocument_relate_id)
        {
            int status = 0;
            string qr = "  select sws_status_c from SWSDocument_RelateDep where swsDocument_relate_id='" + swsDocument_relate_id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(qr, con);
            status = Convert.ToInt32(cmd.ExecuteScalar());

            con.Close();
            return status;
        }

        protected void GV_section_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = (GV_section.DataKeys[e.RowIndex].Values["swsDocument_relate_id"].ToString());
            if (status_Section(id) >= 2)
            {
                string display = "Không thể xóa. Bộ phận đã xác nhận tiêu chuẩn này!";
                ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SWS_deleteSec", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@swsDocument_relate_id", id);
                cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
                cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
                cmd.ExecuteNonQuery();

                con.Close();
                load_GVSec(Request.QueryString["sws_doc_id"]);
            }
        }


    }
}
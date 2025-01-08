using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard
{
    public partial class SWSDocument_Edit : System.Web.UI.Page
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
           

            con.Open();

            SqlCommand cmd = new SqlCommand("SWS_Document_Detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("sws_doc_id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string k = dt.Rows[0]["active_f"].ToString().Trim();

           
            txt_swsDocument_cd.Text = dt.Rows[0]["sws_standard_c"].ToString().Trim();
            txt_ver.Text = dt.Rows[0]["sws_standard_ver"].ToString().Trim();
            txt_nameVi.Text = dt.Rows[0]["sws_standard_nm_vi"].ToString().Trim();
            txt_nameEN.Text = dt.Rows[0]["sws_standard_nm_en"].ToString().Trim();
            txt_Receive_dt.Text = dt.Rows[0]["receive_dt"].ToString().Trim();
            txt_comment.Text = dt.Rows[0]["comment"].ToString().Trim();

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

        protected void btn_AddDep_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_insertDocDep", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sws_doc_id", lbl_id.Text.Trim());
            cmd.Parameters.AddWithValue("@dep_c", cb_Section.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
            cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
            cmd.ExecuteNonQuery();

            con.Close();
            load_GVSec(lbl_id.Text.Trim());

        }

       

        protected void btn_save_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_updateSWSDocument", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sws_standard_c", txt_swsDocument_cd.Text.Trim());
            cmd.Parameters.AddWithValue("@sws_standard_ver", txt_ver.Text.Trim());
            cmd.Parameters.AddWithValue("@sws_standard_nm_vi", txt_nameVi.Text.Trim());
            cmd.Parameters.AddWithValue("@sws_standard_nm_en", txt_nameEN.Text.Trim());
            cmd.Parameters.AddWithValue("@receive_dt", txt_Receive_dt.Text.Trim());
            cmd.Parameters.AddWithValue("@comment", txt_comment.Text.Trim());
            cmd.Parameters.AddWithValue("@sws_doc_id", lbl_id.Text.Trim());
            cmd.ExecuteNonQuery();
            string display = "Cập nhật thành công!";
            ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            con.Close();
            load_information(lbl_id.Text.Trim());
            Response.Redirect("../SWSDocument/SWSDocument.aspx");

        }

        protected void btn_start_Click(object sender, EventArgs e)
        {

            if (GV_section.Rows.Count == 0 || GridView1.Rows.Count == 0)
            {
                alert.Attributes.Add("class", "alert_danger");

                alert.InnerHtml = "Không thành công! Bạn cần chọn đầy đủ Bộ phận và File";
            }
            else
            {

                con.Open();
                string qr = "select sws_doc_id, sws_standard_c,sws_standard_ver,  sws_standard_nm_vi from SWSDocument  where  sws_doc_id='" + lbl_id.Text.Trim() + "'";
                
                SqlDataAdapter da = new SqlDataAdapter(qr, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                string link = ConfigurationManager.AppSettings["prefixpath"].ToString() + "/SWSDocument/SWS_ViewProgress.aspx?sws_doc_id=" + lbl_id.Text.Trim() + "";
                string FullUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + link;

                string subject = "Thông báo tiêu chuẩn mới";
                string body = @"<p>(Đây là email tự động từ hệ thống tiêu chuẩn)</p><br/>" +
                    "<p><div>Một tiêu chuẩn mới có liên quan đến bộ phận của bạn đã được thêm vào hệ thống.</div>" +
                    "<div>Click vào đường link bên dưới để xem chi tiết và Xác nhận tiêu chuẩn</div>" +
                    "<a href='" + FullUrl + "'>" + FullUrl + "</a></p>" +
                    "<p>[Loại tiêu chuẩn] " +
                     "<i>:Tiêu chuẩn SWS" + "</i><br/>" +
                    // "[Loại tài liệu] " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                    //"<i>:" + dt.Rows[0]["doc_type_nm"].ToString() + "</i><br/>" +
                    "[Tên tiêu chuẩn] " +
                    "<i>&nbsp;:" + dt.Rows[0]["sws_standard_nm_vi"].ToString() + "</i></p>";

                send_email(dt_getEmail(1, lbl_id.Text.Trim()), dt_getEmail(2, lbl_id.Text.Trim()), subject, body);
                con.Open();
                SqlCommand cmd_update = new SqlCommand("SWS_active", con);
                cmd_update.CommandType = CommandType.StoredProcedure;
                cmd_update.Parameters.AddWithValue("@sws_doc_id", lbl_id.Text.Trim());
                cmd_update.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
                cmd_update.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
                cmd_update.ExecuteNonQuery();
                con.Close();
                alert.InnerHtml = "Đã gửi mail thành công";
                alert.Attributes.Add("class", "text-success");
            

            }
            

        }
        DataTable dt_getEmail(int casek, string doc_id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_getEmail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sws_doc_id", doc_id);
            cmd.Parameters.AddWithValue("@case", casek);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt_to = new DataTable();
            da.Fill(dt_to);
            con.Close();
            return dt_to;

        }

        void send_email(DataTable dt_to, DataTable dt_cc, string subject, string body)
        {
            //SmtpClient smtpClient = new SmtpClient("172.17.132.20", 25);
            SmtpClient smtpClient = new SmtpClient("202.15.130.231", 25);
            //smtpClient.Credentials = new System.Net.NetworkCredential("is-check@svws.sws.com", "123456789");
            smtpClient.Credentials = new System.Net.NetworkCredential("svws.sm.std-admin@sws.com", "");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("is-check@svws.sws.com");
            mailMessage.From = new MailAddress("svws.sm.std-admin@sws.com");
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            mailMessage.Body = body;


            for (int i = 0; i < dt_to.Rows.Count; i++)
            {
                if (dt_to.Rows[i]["email"].ToString() != "")
                {
                    mailMessage.To.Add(new MailAddress(dt_to.Rows[i]["email"].ToString()));
                }
            }
            for (int i = 0; i < dt_cc.Rows.Count; i++)
            {
                if (dt_cc.Rows[i]["email"].ToString() != "")
                {
                    mailMessage.CC.Add(new MailAddress(dt_cc.Rows[i]["email"].ToString()));
                }

            }
            smtpClient.Send(mailMessage);
            mailMessage.Dispose();
            mailMessage = null;
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
            int status=0;
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
            //if (status_Section(id) >= 2)
            //{
                
                //string display = "Bộ phận này đã xác nhận tiêu chuẩn rồi! Bạn có chắc sẽ loại bỏ bộ phận này?";
                //ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            //}
            //else
            //{
                con.Open();
                SqlCommand cmd = new SqlCommand("SWS_deleteSec", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@swsDocument_relate_id", id);
                //cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
                //cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
                //cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
                cmd.ExecuteNonQuery();
                
                con.Close();
                load_GVSec(Request.QueryString["sws_doc_id"]);
            //}
        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["sws_doc_id"].Trim() != "")
            {
                try
                {
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    var Extension = fileName.Substring(fileName.LastIndexOf('.')).ToLower();


                    //if (Extension == "xlsx")
                    //{
                    string filename = txt_swsDocument_cd.Text.Trim() + txt_ver.Text.Trim() + DateTime.Now.ToString().Replace('/', '_').Replace(':', '_').Replace(' ', '_') + Extension;

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/File/SWS_File/" + filename));
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SWS_insertDocument_file", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swsDoc_id", Request.QueryString["sws_doc_id"].Trim());
                    cmd.Parameters.AddWithValue("@link_file", "~/File/SWS_File/" + filename);
                    cmd.Parameters.AddWithValue("@create_dt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@create_per", Session["username"].ToString());
                    cmd.Parameters.AddWithValue("@class", lbl_class.Text.Trim());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    load_GVFile(Request.QueryString["sws_doc_id"]);

                    //}
                    //    else
                    //{
                    //    alert_file.InnerHtml = "File nhập vào phải là định dạng .xlsx";
                    //}

                }
                catch (Exception ex)
                {
                    alert_file.InnerHtml = ex.Message;
                }

            }
        }
    }
}
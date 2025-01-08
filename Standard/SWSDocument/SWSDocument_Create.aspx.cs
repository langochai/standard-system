using Microsoft.Office.Core;
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
    public partial class SWSDocument_Create : System.Web.UI.Page
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
                
                
                    Receive_dt.Attributes.Add("readonly", "true");
                
            }
                    
           
            //((Label)Master.FindControl("lbl_mainMenu")).Text = Language.value == "en" ? "SWS Standard" : "Tiêu chuẩn SWS";
            //((Label)Master.FindControl("lbl_subMenu")).Text = Language.value == "en" ? "Manage Document" : "Quản lý tiêu chuẩn";


        }

        void loadGVFile(string key)
        {
            if (key != "")
            {
                alert_file.InnerHtml = "";
                con.Open();
                string qr = "select swsDoc_file_id, REVERSE(SUBSTRING(REVERSE(link_file),0,CHARINDEX('/',REVERSE(link_file)))) as link_file from SWSDocument_file where swsDoc_id='" + key + "'";
                SqlCommand cmd = new SqlCommand(qr, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                con.Close();

            }
           
        }

        void loadGVSec(string key)
        {
            if (key != "")
            {
                con.Open();
                string qr = "select swsDocument_relate_id, dep_c from SWSDocument_RelateDep where sws_doc_id='" + key + "'";
                SqlCommand cmd = new SqlCommand(qr, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GV_section.DataSource = dt;
                GV_section.DataBind();
                con.Close();
                
            }

        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_insertDocument", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sws_standard_c", txt_swsDocument_cd.Text.Trim());
            cmd.Parameters.AddWithValue("@sws_standard_ver", txt_ver.Text.Trim());
            cmd.Parameters.AddWithValue("@sws_standard_nm", txt_nameVi.Text.Trim());
            cmd.Parameters.AddWithValue("@sws_standard_nm_en", txt_nameEN.Text.Trim());
            cmd.Parameters.AddWithValue("@receive_dt", Receive_dt.Value);
            cmd.Parameters.AddWithValue("@comment", txt_comment.Text.Trim());
            
            cmd.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
            cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());

            lbl_key.Text = cmd.ExecuteScalar().ToString().Trim();
            if(lbl_key.Text.Trim() != "")
            {
                alert.Attributes.Add("class", "alert_success");
                alert.InnerHtml = "Đã thêm thành công";
                Response.Redirect("../SWSDocument/SWSDocument.aspx");
                //attach_content.Attributes.CssStyle.Add("display", "");
                //btn_start.Visible = true;
                //Response.Redirect("SWSDocument_Edit.aspx?sws_doc_id=" + lbl_key.Text.Trim() + "");
            }
            else
            {
                alert.InnerHtml = "Insert fail";
                alert.Attributes.Add("class", "alert_danger");
            }

            con.Close();

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

                SqlCommand cmd = new SqlCommand("SWS_activeDocument", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sws_doc_id", lbl_key.Text.Trim());
                cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
                cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();

            }
           
            //send email
        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            if(lbl_key.Text.Trim() != "")
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
                    cmd.Parameters.AddWithValue("@swsDoc_id", lbl_key.Text.Trim());
                    cmd.Parameters.AddWithValue("@link_file", "~/File/SWS_File/" + filename);
                    cmd.Parameters.AddWithValue("@create_dt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@create_per", Session["username"].ToString());
                    cmd.Parameters.AddWithValue("@class", lbl_class.Text.Trim());
                    cmd.ExecuteNonQuery();



                    con.Close();
                    
                    loadGVFile(lbl_key.Text.Trim());

                    //}
                    //    else
                    //{
                    //    alert_file.InnerHtml = "File nhập vào phải là định dạng .xlsx";
                    //}

                }
                catch (Exception ex)
                {
                    alert_file.Attributes.Add("class", "alert_danger");
                    alert_file.InnerHtml = ex.Message;
                }

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
            File.Delete(myPath);
            con.Close();
            loadGVFile(lbl_key.Text.Trim());
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

        protected void btn_AddDep_Click(object sender, EventArgs e)
        {
            string dep = cb_Section.SelectedValue.Trim();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SWS_isDuplicate_Dep", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@sws_doc_id", lbl_key.Text.Trim());
            cmd1.Parameters.AddWithValue("@dep_c", dep);
            int count = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
            if (count > 0)
            {
                alert_Section.InnerHtml = "Bộ phận này đã được thêm trước đó";
                alert_Section.Attributes.Add("css", "alert_danger");

            }
            else
            {
                alert_Section.InnerHtml = "";
                SqlCommand cmd = new SqlCommand("SWS_insertDocDep", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sws_doc_id", lbl_key.Text.Trim());
                cmd.Parameters.AddWithValue("@dep_c", cb_Section.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
                cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
                cmd.ExecuteNonQuery();

            }


           

            con.Close();
            loadGVSec(lbl_key.Text.Trim());

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
                alert_Section.Attributes.Add("css", "alert_danger");
                alert_Section.InnerHtml= "Không thể xóa. Bộ phận đã xác nhận tiêu chuẩn này!";
                
            }
            else
            {
                alert_Section.InnerHtml = "";
                con.Open();
                SqlCommand cmd = new SqlCommand("SWS_deleteSec", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@swsDocument_relate_id", id);
                cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
                cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
                cmd.ExecuteNonQuery();

                con.Close();
                loadGVSec(Request.QueryString["sws_doc_id"]);
            }
        }
       

       

        
    }
}
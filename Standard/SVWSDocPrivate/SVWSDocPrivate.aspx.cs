using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Standard.SVWSDocument.SVWSDocPrivate
{
    public partial class SVWSDocPrivate : System.Web.UI.Page
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
                if (Convert.ToInt32( Session["permit"].ToString()) <=2 )
                {
                    loadGV("", "", Session["dep_c"].ToString());
                    
                }
                else
                {
                    Response.Redirect("../SVWSDocPrivate/SVWSDocPrivate_default.aspx");

                }
            }

        }
        //
        void loadGV(string svws_prv_c, string svws_prv_nm_vi, string make_dep_c)
        {
            DataTable dt = new DataTable();
            dt= dt_GV(svws_prv_c, svws_prv_nm_vi, make_dep_c);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //for(int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (dt.Rows[i][7].ToString() != "1")
            //    {
            //        GridView1.Rows[i].BackColor= System.Drawing.Color.FromArgb(252, 213, 180);
            //    }
            //}
            lblTongsodongGV1.Text = GridView1.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            alert.InnerHtml = "";
            loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim());
        }

        DataTable dt_GV(string svws_prv_c, string svws_prv_nm_vi, string make_dep_c)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_LoadPRV", con);
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
            else if (e.CommandName == "add_link")
            {
                lbl_doc_id_file.Text = e.CommandArgument.ToString().Trim();
                filePopUp.Show();

                load_GV_file(lbl_doc_id_file.Text);

            }
            else if (e.CommandName == "edit")
            {
                string doc_id = e.CommandArgument.ToString().Trim();
                Response.Redirect("../SVWSDocPrivate/SVWSDocPrivate_Edit.aspx?doc_id=" + doc_id);
            }
            else if (e.CommandName == "sendEmail")
            {
                string doc_id = e.CommandArgument.ToString().Trim();
                if (dt_file(doc_id).Rows.Count==0)
                {
                    alert.InnerHtml = "Không thể active! Bạn chưa thêm File tài liệu vào tiêu chuẩn này";
                    alert.Attributes.Add("class", "text-danger");
                }
                else
                {
                    
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SVWS_PRV_active", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@svws_prv_id", doc_id);
                    cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
                    cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim());

                    DataTable dt_content = dt_detail_std(doc_id);
                    //Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
                    //Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                    //oMailItem.To = address;
                    // body, bcc etc...
                    //oMailItem.Body= "xin chào";
                    
                    string link = ConfigurationManager.AppSettings["prefixpath"].ToString() + "/SVWSDocPrivate/SVWSDocPrivate_Detail.aspx?doc_id=" + doc_id + "";
                    string FullUrl =  Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + link;
                    string body= " < br />[Mã tiêu chuẩn]:" + dt_content.Rows[0]["svws_prv_c"].ToString() +
                        "<br/>[Tên tiêu chuẩn]:" + dt_content.Rows[0]["svws_prv_nm_vi"].ToString() +
                        "<br/>Click vào link bên dưới để xem chi tiết:" +
                        "<br/><a href='" + FullUrl + "'>" + FullUrl + "</a>"; 
                    string url = string.Format("mailto:{0}?subjecr={1}&body={2}", " ", "", FullUrl);
                    Response.Redirect(url);
                    //Response.Redirect("mailto:MyEmail@something.com?subject=this is the subject&body= ");
                    

                    //oMailItem.HTMLBody = "<br/>[Mã tiêu chuẩn]:" + dt_content.Rows[0]["svws_prv_c"].ToString() +
                    //    "<br/>[Tên tiêu chuẩn]:" + dt_content.Rows[0]["svws_prv_nm_vi"].ToString() +
                    //    "<br/>Click vào link bên dưới để xem chi tiết:" +
                    //    "<br/><a href='" + FullUrl + "'>" + FullUrl + "</a>";

                    //for (int i = 0; i < dt_file(doc_id).Rows.Count; i++)
                    //{
                    //    string path = Server.MapPath((dt_file(doc_id).Rows[i]["link_file"].ToString()));

                    //    //oMailItem.Attachments.Add(new Attachment(path));
                    //    oMailItem.Attachments.Add(path, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);

                    //}


                    //oMailItem.Display(false);
                    //if (oMailItem.Attachments != null)
                    //{
                    //    for (Int32 i = oMailItem.Attachments.Count - 1; i >= 0; i--)
                    //    {
                    //        oMailItem.Attachments[i].Dispose();
                    //    }
                    //    oMailItem.Attachments.Clear();
                    //    oMailItem.Attachments.Dispose();
                    //}
                }

            }    

        }
        DataTable dt_file(string doc_id)
        {
            con.Open();
            string qr = "select * from SVWSDocPrivate_file where svws_prv_id='" + doc_id + "'";

           
            SqlCommand cmd = new SqlCommand(qr, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        DataTable dt_detail_std(string id)
        {
            con.Open();
            string qr = "select *from SVWSDocPrivate where svws_prv_id='" + id + "'";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        void load_GV_file(string prv_id)
        {
            con.Open();
            string qr = "select svws_prv_file_id, REVERSE(SUBSTRING(REVERSE(link_file),0,CHARINDEX('/',REVERSE(link_file)))) as link_file from SVWSDocPrivate_file where svws_prv_id ='" + prv_id + "'";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GV_File.DataSource = dt;
            GV_File.DataBind();
            con.Close();
        }
        //string get_doc_id()
        //{
        //    con.Open();
        //    string qr = "select svws_doc_id, svws_std_c from SVWSDocument where svws_doc_id='" + lbl_doc_id_file.Text + "'";
        //    SqlCommand cmd = new SqlCommand(qr, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    string doc_id = dt.Rows[0]["svws_doc_id"].ToString().Trim();
        //    con.Close();
        //    return doc_id;
        //}

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (fileName != "")
                {
                    string pathtocheck = Server.MapPath("~/File/SVWS_PRVFile/" + fileName);
                    HttpPostedFile file = FileUpload1.PostedFile;
                    int iFileSize = file.ContentLength;


                    if (iFileSize > 10485760)  // 10MB
                    {
                        // File exceeds the file maximum size
                        alert_file.InnerHtml = "Bạn chỉ được phép upload tài liệu dưới 10M";
                        alert_file.Attributes.CssStyle.Add("class", "text-danger");
                    }
                    else if (System.IO.File.Exists(pathtocheck))
                    {
                        alert_file.InnerHtml = "Tên file đã tồn tại, vui lòng nhập lại một tên khác";
                        alert_file.Attributes.Add("class", "text-danger");
                        filePopUp.Show();
                    }
                    else
                    {
                        alert_file.InnerHtml = "";
                        var Extension = fileName.Substring(fileName.LastIndexOf('.')).ToLower();
                        string qr = "select svws_prv_id, svws_prv_c from SVWSDocPrivate where svws_prv_id='" + lbl_doc_id_file.Text + "'";
                        SqlCommand cmd = new SqlCommand(qr, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        string std_c = dt.Rows[0]["svws_prv_c"].ToString().Trim();
                        //string doc_id = dt.Rows[0]["svws_doc_id"].ToString().Trim();
                        //if (Extension == "xlsx")
                        //{

                        //string filename = std_c + "_" + DateTime.Now.ToString().Replace('/', '_').Replace(':', '_').Replace(' ', '_') + Extension;
                        string filename = fileName;
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/File/SVWS_PRVFile/" + filename));
                        System.Threading.Thread.Sleep(8000);

                        SqlCommand cmd_insert = new SqlCommand("SVWS_PRV_insertFile", con);
                        cmd_insert.CommandType = CommandType.StoredProcedure;
                        cmd_insert.Parameters.AddWithValue("@svws_prv_id", lbl_doc_id_file.Text);
                        cmd_insert.Parameters.AddWithValue("@link_file", "~/File/SVWS_PRVFile/" + filename);
                        cmd_insert.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
                        cmd_insert.Parameters.AddWithValue("@pms_i_usr", Session["username"].ToString());
                        cmd_insert.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
                        cmd_insert.ExecuteNonQuery();
                        con.Close();
                        filePopUp.Show();
                        load_GV_file(lbl_doc_id_file.Text);
                    }

                    
                }



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
        
        
        protected void GV_File_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GV_File.DataKeys[e.RowIndex].Value.ToString();
            con.Open();
            string qr_link = "select link_file from SVWSDocPrivate_file where svws_prv_file_id='" + id + "'";
            SqlCommand cmd_link = new SqlCommand(qr_link, con);
            string myPath = cmd_link.ExecuteScalar().ToString();
            myPath = Server.MapPath(myPath);

            string qr = "delete SVWSDocPrivate_file where svws_prv_file_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(qr, con);
            cmd.ExecuteNonQuery();


            con.Close();
            File.Delete(myPath);
            filePopUp.Show();
            load_GV_file(lbl_doc_id_file.Text);
        }

       

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_deletePRV", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_prv_id", id);
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
            cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
            cmd.ExecuteNonQuery();
            con.Close();
            loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim());
            
        }

        protected void GV_File_RowCommand(object sender, GridViewCommandEventArgs e)
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
        

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
         
            loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim());
            alert.InnerHtml = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String mailStr = "mailto:MyEmail@something.com?subject=this is the subject&body=this is the body";
            System.Diagnostics.Process.Start(mailStr);

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (FileUpload1.FileBytes.Length > 10485760)

            {

                args.IsValid = false;

            }

            else

            {

                args.IsValid = true;

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // whatever your condition
                if (e.Row.Cells[7].Text == "0")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(252, 213, 180);// This will make row back color red}
                }
            }
        }
    }
}
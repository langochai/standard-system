

using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard.SVWSDocument
{
    public partial class SVWSDocument : System.Web.UI.Page
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
                if (Session["permit"].ToString() == "0" || Session["permit"].ToString() == "1")
                {
                    loadGV("", "", "", "");
                   
                    
                }
                else
                {
                    Response.Redirect("../SVWSDocument/SVWSDocument_default.aspx");
                    
                }
            }
            
        }
        DataTable dt_GV(string svws_std_c, string svws_std_nm_vi, string make_dep_c, string doc_type_id)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_load_Document", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_std_c", svws_std_c);
            cmd.Parameters.AddWithValue("@svws_std_nm_vi", svws_std_nm_vi);
            cmd.Parameters.AddWithValue("@make_dep_c", make_dep_c);
            cmd.Parameters.AddWithValue("@doc_type_id", doc_type_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            con.Close();
            return dt;

        }


        void loadGV(string svws_std_c, string svws_std_nm_vi, string make_dep_c, string doc_type_id)
        {
            DataTable dt = new DataTable();
            dt= dt_GV(svws_std_c, svws_std_nm_vi, make_dep_c, doc_type_id);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            lblTongsodongGV1.Text = GridView1.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";
        }

        protected void btnOpenPopUp_Click(object sender, EventArgs e)
        {
            
            mpePopUp.Show();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "add_dep")
                {
                    lbl_doc_id.Text = e.CommandArgument.ToString().Trim();
                    mpePopUp.Show();
                    load_infoPopupDep();
                }
                else if (e.CommandName == "add_link")
                {
                    lbl_doc_id_file.Text = e.CommandArgument.ToString().Trim();
                    filePopUp.Show();

                    // load_GV_file(get_doc_id());
                    load_GV_file(lbl_doc_id_file.Text);

                }

                else if (e.CommandName == "detail")
                {
                    string doc_id = e.CommandArgument.ToString().Trim();
                    Response.Redirect("../SVWSDocument/SVWSDocument_Detail.aspx?doc_id=" + doc_id);
                }

                else if (e.CommandName == "edit")
                {
                    string doc_id = e.CommandArgument.ToString().Trim();
                    Response.Redirect("../SVWSDocument/SVWSDocument_edit.aspx?doc_id=" + doc_id);
                }

                else if (e.CommandName == "sendEmail")
                {

                    string doc_id = e.CommandArgument.ToString().Trim();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SVWS_get_confirm_list_status", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@svws_doc_id", doc_id);
                    bool allowActivation = Convert.ToInt32(cmd.ExecuteScalar()) == 0;
                    con.Close();
                    if (!allowActivation)
                    {
                        alert.InnerHtml = "Không thể active! Tiêu chuẩn chưa có đủ chữ ký xác nhận triển khai";
                        alert.Attributes.Add("class", "text-danger");
                    }
                    else if (is_existDep(doc_id) == 0 || is_existFile(doc_id) == 0)
                    {
                        alert.InnerHtml = "Không thể active! Bạn chưa chọn đủ Bộ phận áp dụng hoặc File tài liệu";
                        alert.Attributes.Add("class", "text-danger");
                    }
                    else
                    {
                        con.Open();
                        string qr = "select svws_doc_id, [svws_std_c],[svws_std_ver],  b.doc_type_nm , [svws_std_nm_vi], [svws_std_nm_eng] from SVWSDocument a inner join Document_type_mst b on a.doc_type_id=b.doc_type_id where  a.svws_doc_id='" + doc_id + "'";
                        ;
                        SqlDataAdapter da = new SqlDataAdapter(qr, con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        if (dt_getEmail(1, doc_id).Rows.Count == 0 || dt_getEmail(2, doc_id).Rows.Count == 0)
                        {
                            alert.InnerHtml = "Không thể active! Pic-AM của bộ phận liên quan chưa có tài khoản đăng nhập";
                            alert.Attributes.Add("class", "text-danger");
                        }
                        else
                        {
                            string link = ConfigurationManager.AppSettings["prefixpath"].ToString() + "/SVWSDocument/SVWSDocument_Detail.aspx?doc_id=" + doc_id + "";
                            string FullUrl = "microsoft-edge:"+ Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + link;
                            // string k = VirtualPathUtility.ToAbsolute(link);
                            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
                            string subject = "[SVWS Standard] Thông báo về tiêu chuẩn mới";
                            string body = @"<p>(Đây là email tự động từ hệ thống tiêu chuẩn, vui lòng không reply lại email này)</p><br/>" +
                                "<p><div>Một tiêu chuẩn mới có liên quan đến bộ phận của bạn đã được thêm vào hệ thống.</div>" +
                                "<div>Click vào đường link bên dưới để xem chi tiết và Xác nhận tiêu chuẩn</div>" +
                                "<a href='" + FullUrl + "'>" + FullUrl + "</a></p>" +
                                "<p>[Loại tiêu chuẩn] " +
                                 "<i>:Tiêu chuẩn SVWS" + "</i><br/>" +
                                "[Loại tài liệu] " + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                               "<i>:" + dt.Rows[0]["doc_type_nm"].ToString() + "</i><br/>" +
                                "[Tên tiêu chuẩn] " +
                                "<i>&nbsp;:" + dt.Rows[0]["svws_std_nm_vi"].ToString() + "</i></p>";

                            send_email(dt_getEmail(1, doc_id), dt_getEmail(2, doc_id), subject, body);
                            con.Open();
                            
                            SqlCommand cmd_update = new SqlCommand("SVWS_active", con);
                            cmd_update.CommandType = CommandType.StoredProcedure;
                            cmd_update.Parameters.AddWithValue("@svws_doc_id", doc_id);
                            cmd_update.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
                            cmd_update.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
                            cmd_update.ExecuteNonQuery();
                            con.Close();
                            alert.InnerHtml = "Đã gửi mail thành công";
                            alert.Attributes.Add("class", "text-success");
                            loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim(), dr_type_doc.SelectedValue.Trim());
                            
                        }

                    }
                }

        }

            catch (Exception ex)
            {
                alert.InnerHtml = ex.Message;
                alert.Attributes.Add("class", "text-danger");
            }


}

        int is_existFile(string doc_id)
        {
            con.Open();
            string qr = "select count (*) from SVWSDocument_file where svws_doc_id='" + doc_id + "'";

            int chk = 0;
            SqlCommand cmd = new SqlCommand(qr, con);
            chk = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            return chk;
        }

        int is_existDep(string doc_id)
        {
            con.Open();
            string qr = " select count(*) from SVWSDocument_relateDep where svws_doc_id='" + doc_id + "'";
            int chk = 0;
            SqlCommand cmd = new SqlCommand(qr, con);
            chk = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            return chk;
        }

        
 

        DataTable dt_getEmail(int casek, string doc_id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("getEmail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_doc_id", doc_id);
            cmd.Parameters.AddWithValue("@case", casek);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt_to = new DataTable();
            da.Fill(dt_to);
            con.Close();
            return dt_to;

        }

        void send_email( DataTable dt_to, DataTable dt_cc, string subject, string body)
        {
            //SmtpClient smtpClient = new SmtpClient("172.17.132.20", 25);
            SmtpClient smtpClient = new SmtpClient("202.15.130.231", 25);
            smtpClient.Credentials = new System.Net.NetworkCredential("svws.sm.std-admin@sws.com", "");
            //smtpClient.Credentials = new System.Net.NetworkCredential("is-check@svws.sws.com", "123456789");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage();
           // mailMessage.From = new MailAddress("is-check@svws.sws.com");
            mailMessage.From = new MailAddress("svws.sm.std-admin@sws.com");
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            for (int i=0; i<dt_to.Rows.Count; i++)
            {
                if(dt_to.Rows[i]["email"].ToString() != "")
                {
                    mailMessage.To.Add(new MailAddress(dt_to.Rows[i]["email"].ToString()));
                }
            }
            for (int i = 0; i < dt_cc.Rows.Count; i++)
            {
                if(dt_cc.Rows[i]["email"].ToString() != "")
                {
                    mailMessage.CC.Add(new MailAddress(dt_cc.Rows[i]["email"].ToString()));
                }
                
            }
            

            //for(int i=0;i< dt_file.Rows.Count; i++)
            //{
            //    string path = Server.MapPath((dt_file.Rows[i]["link_file"].ToString()));
               
            //    mailMessage.Attachments.Add(new Attachment(path));
                
            //}
            
             
            smtpClient.Send(mailMessage);
            
            //if (mailMessage.Attachments != null)
            //{
            //    for (Int32 i = mailMessage.Attachments.Count - 1; i >= 0; i--)
            //    {
            //        mailMessage.Attachments[i].Dispose();
            //    }
            //    mailMessage.Attachments.Clear();
            //    mailMessage.Attachments.Dispose();
            //}
            mailMessage.Dispose();
            mailMessage = null;
        }

        DataTable get_file(string doc_id)
        {
            con.Open();
            string qr = "select link_file from SVWSDocument_file where svws_doc_id='" + doc_id + "' ";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
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
        void load_infoPopupDep()
        {
            con.Open();
            string qr = "SELECT t1.dep_c FROM Dep_mst t1 LEFT JOIN SVWSDocument_relateDep t2 ON t2.dep_c = t1.dep_c  and t2.svws_doc_id='"+ lbl_doc_id.Text.Trim()+"' WHERE t2.dep_c IS NULL";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            chk_dep.DataSource = dt;
            chk_dep.DataBind();
            string qr_dep = "select svwsDocument_relate_id, dep_c from SVWSDocument_relateDep where svws_doc_id=" + lbl_doc_id.Text.Trim();
            SqlDataAdapter da_dep = new SqlDataAdapter(qr_dep, con);
            DataTable dt_dep = new DataTable();
            da_dep.Fill(dt_dep);
            GV_dep.DataSource = dt_dep;
            GV_dep.DataBind();
            con.Close();
        }

        void load_inforPopupFile(string svws_doc_id)
        {
           
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            con.Open();
            
            foreach (System.Web.UI.WebControls.ListItem li in chk_dep.Items)
            {
                if (li.Selected)
                {
                    string dep_c = li.ToString();
                    SqlCommand cmd = new SqlCommand("SVWS_insertDep", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@svws_doc_id", lbl_doc_id.Text.Trim());
                    cmd.Parameters.AddWithValue("@dep_c", dep_c);
                    cmd.Parameters.AddWithValue("@confirm_f", "0");
                    cmd.Parameters.AddWithValue("@confirm_per", Session["username"]);
                    cmd.Parameters.AddWithValue("@confirm_dt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
                    cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"]);
                    cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
                    cmd.ExecuteNonQuery();

                }
            }

            SqlCommand insertDeps = new SqlCommand("SVWS_insert_confirming_department", con);
            insertDeps.CommandType = CommandType.StoredProcedure;
            insertDeps.Parameters.AddWithValue("@svws_doc_id", lbl_doc_id.Text.Trim());
            insertDeps.ExecuteNonQuery();

            con.Close();
            mpePopUp.Show();
            load_infoPopupDep();
        }

        protected void GV_dep_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id= (GV_dep.DataKeys[e.RowIndex].Values["svwsDocument_relate_id"].ToString());
            con.Open();
            string qr = "delete SVWSDocument_relateDep where svwsDocument_relate_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(qr, con);
            cmd.ExecuteNonQuery();
            SqlCommand insertDeps = new SqlCommand("SVWS_insert_confirming_department", con);
            insertDeps.CommandType = CommandType.StoredProcedure;
            insertDeps.Parameters.AddWithValue("@svws_doc_id", lbl_doc_id.Text.Trim());
            insertDeps.ExecuteNonQuery();
            con.Close();
            mpePopUp.Show();
            load_infoPopupDep();
        }
        void load_GV_file(string doc_id)
        {
            con.Open();
            string qr = "select svwsDoc_file_id, REVERSE(SUBSTRING(REVERSE(link_file),0,CHARINDEX('/',REVERSE(link_file)))) as link_file from SVWSDocument_file where svws_doc_id ='" + doc_id + "'";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GV_File.DataSource = dt;
            GV_File.DataBind();
            con.Close();
        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            //try
            //{
                con.Open();
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            if(fileName != "")
            {
                string pathtocheck = Server.MapPath("~/File/SVWS_File/" + fileName);
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
                    string qr = "select svws_doc_id, svws_std_c from SVWSDocument where svws_doc_id='" + lbl_doc_id_file.Text + "'";
                    SqlCommand cmd = new SqlCommand(qr, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    string std_c = dt.Rows[0]["svws_std_c"].ToString().Trim();
                    string doc_id = dt.Rows[0]["svws_doc_id"].ToString().Trim();
                    string filename = fileName;
                    //string filename = std_c + "_" + DateTime.Now.ToString().Replace('/', '_').Replace(':', '_').Replace(' ', '_') + Extension;

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/File/SVWS_File/" + filename));

                    SqlCommand cmd_insert = new SqlCommand("SVWS_insertFile", con);
                    cmd_insert.CommandType = CommandType.StoredProcedure;
                    cmd_insert.Parameters.AddWithValue("@svws_doc_id", lbl_doc_id_file.Text);
                    cmd_insert.Parameters.AddWithValue("@link_file", "~/File/SVWS_File/" + filename);
                    cmd_insert.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
                    cmd_insert.Parameters.AddWithValue("@pms_i_usr", Session["username"].ToString());
                    cmd_insert.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
                    cmd_insert.ExecuteNonQuery();
                    con.Close();
                    filePopUp.Show();
                    load_GV_file(doc_id);
                }

               
            }



               

            //}
            //catch (Exception ex)
            //{
            //    alert_file.InnerHtml = ex.Message;
            //}

        }

        protected void GV_File_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GV_File.DataKeys[e.RowIndex].Value.ToString();
           
            con.Open();
            string qr_link = "select link_file from SVWSDocument_file where svwsDoc_file_id='" + id + "'";
            SqlCommand cmd_link = new SqlCommand(qr_link, con);
            string myPath = cmd_link.ExecuteScalar().ToString();
            myPath = Server.MapPath(myPath);

            string qr = "delete SVWSDocument_file where svwsDoc_file_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(qr, con);
            cmd.ExecuteNonQuery();
            con.Close();
            if (File.Exists(myPath))
            {
                File.Delete(myPath);
            }
            
            filePopUp.Show();
            load_GV_file(lbl_doc_id_file.Text.Trim());

        }

        protected void GV_File_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "download")
            {
                con.Open();
                string id = e.CommandArgument.ToString().Trim();
                string qr = "select link_file from SVWSDocument_file where svwsDoc_file_id='" + id + "'";
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
                filePopUp.Show();
                load_GV_file(lbl_doc_id_file.Text.Trim());
               

            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_deleteDoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_doc_id", id);
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
            cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
            cmd.ExecuteNonQuery();
            con.Close();
            loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim(), dr_type_doc.SelectedValue.Trim());
           

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim(), dr_type_doc.SelectedValue.Trim());  
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField countMissing = (HiddenField)e.Row.FindControl("HiddenFCountMissing");
                bool isCompleted = Convert.ToInt32(countMissing.Value) < 1;
                bool isActivated = e.Row.Cells[7].Text == "1";
                if (!isActivated && !isCompleted)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(252, 213, 180);
                }
                else if (!isActivated && isCompleted)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 250, 205);
                }
            }
        }

        protected void btn_export_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim(), dr_type_doc.SelectedValue.Trim());
            alert.InnerHtml = "";
        }
    }
}
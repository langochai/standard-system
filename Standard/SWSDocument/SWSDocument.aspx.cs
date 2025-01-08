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


namespace Standard.SWSDocument
{
    public partial class SWSDocument : System.Web.UI.Page
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
                
                 if (Convert.ToInt32(Session["permit"].ToString()) > 3)
                {
                
                    alert_permit.Attributes.CssStyle.Add("display", "");
                    content.Attributes.CssStyle.Add("display", "none");
                    alert_permit.InnerHtml = "Tài khoản của bạn không thể vào chức năng này";
                    

                }
                else
                {
                    content.Attributes.CssStyle.Add("display", "");
                    alert_permit.Attributes.CssStyle.Add("display", "none");
                    load_GV("");
                }
            }
                   
            //((Label)Master.FindControl("lbl_mainMenu")).Text = Language.value == "en" ? "SWS Standard" : "Tiêu chuẩn SWS";
            //((Label)Master.FindControl("lbl_subMenu")).Text = Language.value == "en" ? "Manage Document" : "Quản lý tiêu chuẩn";
            //lbl_mainMenu.Text= Language.value == "en" ? "SWS Standard" : "Tiêu chuẩn SWS";
            //lbl_submenu.Text = Language.value == "en" ? "Manage Document" : "Quản lý tiêu chuẩn";
        }
        void load_GV(string itemSeach)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_SearchDocument", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@searchItem",itemSeach);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            GV_SWSDocument.DataSource = dt;
            GV_SWSDocument.DataBind();
            con.Close();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (dt.Rows[i]["active_f"].ToString() != "1")
            //    {
            //        GV_SWSDocument.Rows[i].BackColor = System.Drawing.Color.FromArgb(252, 213, 180);
            //    }
            //}
            lblTongsodongGV1.Text = GV_SWSDocument.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";

        }

        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_SearchDocument", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@searchItem", txt_search.Text.Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            GV_SWSDocument.DataSource = dt;
            GV_SWSDocument.DataBind();
            con.Close();

        }

        protected void GV_SWSDocument_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    string id = e.CommandArgument.ToString().Trim();
                    Response.Redirect("SWSDocument_Detail.aspx?sws_doc_id=" + id + "");

                }
                else if (e.CommandName == "view_progress")
                {
                    string id = e.CommandArgument.ToString().Trim();
                    Response.Redirect("SWS_ViewProgress.aspx?sws_doc_id=" + id + "");
                }
                else if (e.CommandName == "edit")
                {
                    string id = e.CommandArgument.ToString().Trim();
                    Response.Redirect("../SWSDocument/SWSDocument_Edit.aspx?sws_doc_id=" + id);
                }




                else if (e.CommandName == "sendEmail")
                {
                    string doc_id = e.CommandArgument.ToString().Trim();
                    if (is_existDep(doc_id) == 0 || is_existFile(doc_id) == 0)
                    {
                        alert.InnerHtml = "Không thể active! Bạn chưa chọn đủ Bộ phận áp dụng hoặc File tài liệu";
                        alert.Attributes.Add("class", "text-danger");
                    }
                    else
                    {
                        con.Open();
                        string qr = "select sws_doc_id, sws_standard_c,sws_standard_ver,  sws_standard_nm_vi from SWSDocument  where  sws_doc_id='" + doc_id + "'";
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


                            string link = ConfigurationManager.AppSettings["prefixpath"].ToString() + "/SWSDocument/SWS_ViewProgress.aspx?sws_doc_id=" + doc_id + "";
                            //http://172.17.132.28/tieuchuan/SWSDocument/SWS_ViewProgress.aspx?sws_doc_id=1025
                            string FullUrl = "microsoft-edge:"+ Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + link;

                            string subject = "[SWS Standard]Thông báo tiêu chuẩn mới";
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

                            send_email(dt_getEmail(1, doc_id), dt_getEmail(2, doc_id), subject, body);
                            con.Open();
                            SqlCommand cmd_update = new SqlCommand("SWS_active", con);
                            cmd_update.CommandType = CommandType.StoredProcedure;
                            cmd_update.Parameters.AddWithValue("@sws_doc_id", doc_id);
                            cmd_update.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
                            cmd_update.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
                            cmd_update.ExecuteNonQuery();
                            con.Close();
                            alert.InnerHtml = "Đã gửi mail thành công";
                            alert.Attributes.Add("class", "text-success");
                            load_GV(txt_search.Text.Trim());

                        }


                    }
                }

            }
            catch(Exception ex)
            {
                alert.InnerHtml = ex.Message;
                alert.Attributes.Add("class", "text-danger");
            }
            finally
            {
                con.Close();
            }
            
        }

        void load_GV_file(string doc_id)
        {
            con.Open();
            string qr = "select swsDoc_file_id, REVERSE(SUBSTRING(REVERSE(link_file),0,CHARINDEX('/',REVERSE(link_file)))) as link_file from SWSDocument_file where swsDoc_id ='" + doc_id + "'";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GV_File.DataSource = dt;
            GV_File.DataBind();
            con.Close();
        }
        void load_infoPopupDep()
        {
            con.Open();
            string qr = "SELECT t1.dep_c FROM Dep_mst t1 LEFT JOIN SWSDocument_RelateDep t2 ON t2.dep_c = t1.dep_c  and t2.sws_doc_id='" + lbl_doc_id.Text.Trim() + "' WHERE t2.dep_c IS NULL";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            chk_dep.DataSource = dt;
            chk_dep.DataBind();
            string qr_dep = "select swsDocument_relate_id, dep_c from SWSDocument_RelateDep where sws_doc_id=" + lbl_doc_id.Text.Trim();
            SqlDataAdapter da_dep = new SqlDataAdapter(qr_dep, con);
            DataTable dt_dep = new DataTable();
            da_dep.Fill(dt_dep);
            GV_dep.DataSource = dt_dep;
            GV_dep.DataBind();
            con.Close();
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
        int is_existFile(string doc_id)
        {
            con.Open();
            string qr = "select count (*) from SWSDocument_file where swsDoc_id='" + doc_id + "'";

            int chk = 0;
            SqlCommand cmd = new SqlCommand(qr, con);
            chk = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            return chk;
        }

        int is_existDep(string doc_id)
        {
            con.Open();
            string qr = " select count(*) from SWSDocument_RelateDep where sws_doc_id='" + doc_id + "'";
            int chk = 0;
            SqlCommand cmd = new SqlCommand(qr, con);
            chk = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            con.Close();
            return chk;
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

        protected void GV_SWSDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // whatever your condition
                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(252, 213, 180);// This will make row back color red}
                }
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

        protected void GV_SWSDocument_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GV_SWSDocument.DataKeys[e.RowIndex].Value.ToString();
            if (check_Section(id) > 0)
            {
                alert.Attributes.Add("class", "alert_danger");
                alert.InnerHtml = "Không thể xóa! Bạn cần xóa bộ phận liên quan trước.";
            }
            else
            {
                
                con.Open();
                SqlCommand cmd = new SqlCommand("SWS_deleteDocument", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sws_doc_id", id);
                //cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
                //cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"]);
                cmd.ExecuteNonQuery();
                con.Close();
                load_GV(txt_search.Text.Trim());
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            con.Open();

            foreach (ListItem li in chk_dep.Items)
            {
                if (li.Selected)
                {
                    string dep_c = li.ToString();
                   
                    SqlCommand cmd = new SqlCommand("SWS_insertDocDep", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sws_doc_id", lbl_doc_id.Text.Trim());
                    cmd.Parameters.AddWithValue("@dep_c", dep_c);
                    cmd.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
                    cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"].ToString());
                    cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
                    cmd.ExecuteNonQuery();
                    //SqlCommand cmd = new SqlCommand("SVWS_insertDep", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@svws_doc_id", lbl_doc_id.Text.Trim());
                    //cmd.Parameters.AddWithValue("@dep_c", dep_c);
                    //cmd.Parameters.AddWithValue("@confirm_f", "0");
                    //cmd.Parameters.AddWithValue("@confirm_per", Session["username"]);
                    //cmd.Parameters.AddWithValue("@confirm_dt", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@pms_i_usr", Session["username"]);
                    //cmd.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
                    //cmd.ExecuteNonQuery();

                }
            }

            con.Close();
            load_GV(txt_search.Text.Trim());
            mpePopUp.Show();
            load_infoPopupDep();
        }

        protected void GV_dep_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = (GV_dep.DataKeys[e.RowIndex].Values["swsDocument_relate_id"].ToString());
            con.Open();
            string qr = "delete SWSDocument_RelateDep where swsDocument_relate_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(qr, con);
            cmd.ExecuteNonQuery();
            con.Close();
            mpePopUp.Show();
            load_infoPopupDep();
        }


        protected void btn_upload_Click(object sender, EventArgs e)
        {
            //try
            //{

          

           
            con.Open();
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            if (fileName != "")
            {
                string pathtocheck = Server.MapPath("~/File/SWS_File/" + fileName);
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
                    string qr = "select sws_doc_id, sws_standard_c from SWSDocument where sws_doc_id='" + lbl_doc_id_file.Text + "'";
                    SqlCommand cmd = new SqlCommand(qr, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    string std_c = dt.Rows[0]["sws_standard_c"].ToString().Trim();
                    string doc_id = dt.Rows[0]["sws_doc_id"].ToString().Trim();
                    string filename = fileName;
                    //string filename = std_c + "_" + DateTime.Now.ToString().Replace('/', '_').Replace(':', '_').Replace(' ', '_') + Extension;

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/File/SWS_File/" + filename));

                    //SqlCommand cmd_insert = new SqlCommand("SVWS_insertFile", con);
                    //cmd_insert.CommandType = CommandType.StoredProcedure;
                    //cmd_insert.Parameters.AddWithValue("@svws_doc_id", lbl_doc_id_file.Text);
                    //cmd_insert.Parameters.AddWithValue("@link_file", "~/File/SVWS_File/" + filename);
                    //cmd_insert.Parameters.AddWithValue("@pms_i_ymd", DateTime.Now);
                    //cmd_insert.Parameters.AddWithValue("@pms_i_usr", Session["username"].ToString());
                    //cmd_insert.Parameters.AddWithValue("@pms_i_class", lbl_class.Text.Trim());
                    //cmd_insert.ExecuteNonQuery();

                    SqlCommand cmd_insert = new SqlCommand("SWS_insertDocument_file", con);
                    cmd_insert.CommandType = CommandType.StoredProcedure;
                    cmd_insert.Parameters.AddWithValue("@swsDoc_id", lbl_doc_id_file.Text.Trim());
                    cmd_insert.Parameters.AddWithValue("@link_file", "~/File/SWS_File/" + filename);
                    cmd_insert.Parameters.AddWithValue("@create_dt", DateTime.Now);
                    cmd_insert.Parameters.AddWithValue("@create_per", Session["username"].ToString());
                    cmd_insert.Parameters.AddWithValue("@class", lbl_class.Text.Trim());
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
            string qr_link = "select link_file from SWSDocument_file where swsDoc_file_id='" + id + "'";
            SqlCommand cmd_link = new SqlCommand(qr_link, con);
            string myPath = cmd_link.ExecuteScalar().ToString();
            myPath = Server.MapPath(myPath);

            string qr = "delete SWSDocument_file where swsDoc_file_id='" + id + "'";
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
            if (e.CommandName == "download")
            {
                con.Open();
                string id = e.CommandArgument.ToString().Trim();
                string qr = "select link_file from SWSDocument_file where swsDoc_file_id='" + id + "'";
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

        protected void GV_SWSDocument_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_SWSDocument.PageIndex = e.NewPageIndex;
            load_GV(txt_search.Text.Trim());
            alert.InnerHtml = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            load_GV(txt_search.Text.Trim());
        }
    }
}
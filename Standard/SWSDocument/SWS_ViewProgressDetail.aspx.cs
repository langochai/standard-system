﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard.SWSDocument
{
    public partial class SWS_ViewProgressDetail : System.Web.UI.Page
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
                txt_duedate.Attributes.Add("readonly", "true");
                load_information();
            }
        }

        void load_filename(string sws_doc_id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_LoadFilename", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@swsDoc_id", sws_doc_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            fileLinks.DataSource = dt;
            fileLinks.DataBind();
            con.Close();
        }
        DataTable get_detailProgress()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_LoadDetailProgress", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;

        }
        DataTable pic_user()
        {
            con.Open();
            string qr = "select pic_username, AM_above from Dep_mst where dep_c = '" + lbl_dep.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();
            return dt;
        }

        DataTable get_ConfirmLog()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_LoadConfirmLog", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }


        void displayDeploy_nodeploy()
        {
            btn_deploy.Enabled = true;
            btn_no_deploy.Enabled = true;
            btn_issue.Enabled = false;
            btn_complete.Enabled = false;
            // btn_back.Enabled = false;
            txt_duedate.Visible = true;
            lbl_duedate.Visible = false;
            lbl_svws_std.Visible = true;
            txt_svws_std.Visible = false;
            btn_save.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;
            txt_comment.Visible = true;
            lbl_comment.Visible = false;
        }

        void display_nochange()
        {
            //btn_back.Enabled = false;
            btn_deploy.Enabled = false;
            btn_no_deploy.Enabled = false;
            btn_issue.Enabled = false;
            btn_complete.Enabled = false;
            txt_duedate.Visible = false;
            lbl_duedate.Visible = true;
            lbl_svws_std.Visible = true;
            txt_svws_std.Visible = false;
            btn_save.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;
            txt_comment.Visible = false;
            lbl_comment.Visible = true;
        }


        void display_isse()
        {
            btn_deploy.Enabled = false;
            btn_no_deploy.Enabled = false;
            btn_issue.Enabled = true;
            btn_complete.Enabled = false;
            //btn_back.Enabled = false;
            txt_duedate.Visible = false;
            lbl_duedate.Visible = true;
            lbl_svws_std.Visible = true;
            txt_svws_std.Visible = false;
            btn_save.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;
            txt_comment.Visible = true;
            lbl_comment.Visible = false;
        }


        void display_complete()
        {
            btn_deploy.Enabled = false;
            btn_no_deploy.Enabled = false;
            btn_issue.Enabled = false;
            btn_complete.Enabled = true;
            //btn_back.Enabled = false;
            txt_duedate.Visible = true;
            lbl_duedate.Visible = true;
            lbl_svws_std.Visible = true;
            txt_svws_std.Visible = true;
            btn_save.Visible = false;
            btn_cancel.Visible = false;
            btn_edit.Visible = false;
            txt_comment.Visible = true;
            lbl_comment.Visible = false;
        }

        void load_DetailProgress(string duedate, string svws_c)
        {

            DataTable dt = get_detailProgress();

            int status_c = Convert.ToInt32(dt.Rows[0]["sws_status_c"].ToString());
            int group_order = Convert.ToInt32(dt.Rows[0]["group_order"].ToString());


            DataTable dt_GV = new DataTable();
            dt_GV.Columns.Add("sws_status_c");
            dt_GV.Columns.Add("sws_status_nm");
            dt_GV.Columns.Add("pms_i_usr");
            dt_GV.Columns.Add("pms_i_ymd");

            dt_GV.Rows.Add("0", "Bắt đầu", "", "");
            dt_GV.Rows.Add("2", "PIC-Xác nhận triển khai", "", "");
            dt_GV.Rows.Add("8", "AM-Xác nhận triển khai", "", "");
            dt_GV.Rows.Add("3", "Đã Issue", "", "");
            dt_GV.Rows.Add("4", "Hoàn thành", "", "");



            DataTable dt_log = new DataTable();
            dt_log = get_ConfirmLog();
            for (int i = 0; i < dt_log.Rows.Count; i++)
            {
                dt_GV.Rows[i]["sws_status_c"] = dt_log.Rows[i]["sws_status_c"];
                dt_GV.Rows[i]["sws_status_nm"] = dt_log.Rows[i]["sws_status_nm"];
                dt_GV.Rows[i]["pms_i_usr"] = dt_log.Rows[i]["pms_i_usr"];
                dt_GV.Rows[i]["pms_i_ymd"] = dt_log.Rows[i]["pms_i_ymd"];

            }



            DataTable dt_picUser = pic_user();
            string usser = Session["username"].ToString();
            if (Session["permit"].ToString() == "0" || (dt_picUser.Rows[0]["pic_username"].ToString().Trim() == Session["username"].ToString().Trim() || dt_picUser.Rows[0]["AM_above"].ToString().Trim() == Session["username"].ToString().Trim()))
            {
                if (group_order < 3)
                {
                    if (dt_picUser.Rows[0]["pic_username"].ToString().Trim() == Session["username"].ToString().Trim())
                    {
                        displayDeploy_nodeploy();
                    }
                    else if (dt_picUser.Rows[0]["AM_above"].ToString().Trim() == Session["username"].ToString().Trim())
                    {
                        if (group_order == 2)
                        {
                            displayDeploy_nodeploy();
                        }
                        else
                        {
                            display_nochange();
                        }
                    }


                }
                else if (group_order == 3)
                {
                    if (status_c == 8)
                    {
                        display_isse();
                    }
                    else if (status_c == 9)
                    {
                        display_nochange();
                    }

                }

                else
                {
                    display_nochange();
                }



            }
            if (Session["permit"].ToString() == "0")
            {
                if (status_c == 9 || status_c == 3)
                {
                    display_complete();
                }
                //else
                //{

                //    display_nochange();
                //}

            }
            if ((Session["permit"].ToString() == "4"))
            {
                display_nochange();
            }


            GV_DetailProgress.DataSource = dt_GV;
            GV_DetailProgress.DataBind();
            if (status_c == 9)
            {
                GV_DetailProgress.Rows[3].Cells[0].ForeColor = Color.FromArgb(191, 191, 191);
                GV_DetailProgress.Rows[4].Cells[0].BackColor = Color.FromArgb(255, 255, 136);
            }
            if (status_c == 6)
            {
                GV_DetailProgress.Rows[4].Visible = false;
            }
            for (int i = 0; i < dt_GV.Rows.Count; i++)
            {
                if (dt_GV.Rows[i][2].ToString().Trim() == "")
                {
                    if (status_c != 9)
                    {
                        GV_DetailProgress.Rows[i].Cells[0].BackColor = Color.FromArgb(255, 255, 136);
                    }




                    break;
                }

            }

            con.Close();
        }



        void load_information()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_LoadDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("swsDocument_relate_id", Request.QueryString["id"]);
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
                lbl_sws_standard_c.Text = dt.Rows[0]["sws_standard_c"].ToString();
                lbl_ver.Text = dt.Rows[0]["sws_standard_ver"].ToString().Trim();
                lbl_doc_nm.Text = dt.Rows[0]["sws_standard_nm_vi"].ToString().Trim();
                lbl_receiptdt.Text = dt.Rows[0]["receive_dt"].ToString().Trim();
                lbl_dep.Text = dt.Rows[0]["dep_c"].ToString().Trim();
                lbl_status.Text = dt.Rows[0]["sws_status_nm"].ToString().Trim();
                txt_duedate.Text = dt.Rows[0]["duedate"].ToString().Trim();
                lbl_duedate.Text = dt.Rows[0]["duedate"].ToString().Trim();
                lbl_svws_std.Text = dt.Rows[0]["svws_std_c"].ToString().Trim();
                txt_svws_std.Text = dt.Rows[0]["svws_std_c"].ToString().Trim();
                txt_comment.Text = dt.Rows[0]["comment"].ToString().Trim();
                lbl_comment.Text = dt.Rows[0]["comment"].ToString().Trim();


                load_filename(dt.Rows[0]["sws_doc_id"].ToString().Trim());

                display_nochange();

                load_DetailProgress(dt.Rows[0]["duedate"].ToString().Trim(), dt.Rows[0]["svws_std_c"].ToString().Trim());
            }



        }
        DataTable checkPICAM(string dep)
        {
            con.Open();
            string qr = "select pic_username, AM_above from Dep_mst where dep_c='" + dep + "'";
            SqlDataAdapter da = new SqlDataAdapter(qr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;

        }

        protected void btn_deploy_Click(object sender, EventArgs e)
        {
            //string value = GV_DetailProgress.Rows[1].Cells[1].Text.Trim();
            DataTable dt = get_detailProgress();
            int status = Convert.ToInt32(dt.Rows[0]["sws_status_c"].ToString());

            if (status == 0)
            {
                string display = "Không thành công! Bạn chưa đọc tài liệu";
                ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            }
            else if (txt_duedate.Text.Trim() == "")
            {

                alert_btn.Attributes.Add("class", "text-danger");
                alert_btn.InnerHtml = "Bạn cần cập nhật due date dịch tài liệu";

            }
            else
            {
                alert_btn.InnerHtml = "";
                DataTable PICAM = checkPICAM(Session["dep_c"].ToString());
                if (PICAM.Rows.Count > 0 && PICAM.Rows[0]["pic_username"].ToString().Trim() == Session["username"].ToString())
                {
                    confirm_deploy(2);
                }
                else if (PICAM.Rows.Count > 0 && PICAM.Rows[0]["AM_above"].ToString().Trim() == Session["username"].ToString() || Session["permit"].ToString() == "0")
                {
                    confirm_deploy(8);
                }


                load_information();
                //load_DetailProgress();

            }

        }

        void confirm_deploy(int sws_status_c)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_cfrm_deploy", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@confirm_deploy_dt", DateTime.Now);
            cmd.Parameters.AddWithValue("@confirm_deploy_per", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@duedate", txt_duedate.Text.Trim());
            cmd.Parameters.AddWithValue("@comment", txt_comment.Text.Trim());
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
            cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
            cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
            cmd.Parameters.AddWithValue("@sws_status_c", sws_status_c);
            cmd.ExecuteNonQuery();
            con.Close();
        }



        protected void lnk_file_Click(object sender, EventArgs e)
        {


        }

        protected void fileLinks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                con.Open();
                string id = e.CommandArgument.ToString().Trim();
                string qr_check = "select count(*) from SWSDocument_RelateDep where swsDocument_relate_id='" + Request.QueryString["id"] + "' and read_dt is  null";

                SqlCommand cmd_check = new SqlCommand(qr_check, con);
                string check = cmd_check.ExecuteScalar().ToString();
                if (check != "0")
                {
                    if ((Session["permit"].ToString() == "2" & Session["dep_c"].ToString().Trim() == lbl_dep.Text.Trim()) || (Session["permit"].ToString() == "0" & Session["dep_c"].ToString().Trim() == lbl_dep.Text.Trim()))
                    {
                        //update status read
                        SqlCommand cmd_update = new SqlCommand("SWS_Read", con);
                        cmd_update.CommandType = CommandType.StoredProcedure;
                        cmd_update.Parameters.AddWithValue("@read_per", Session["username"].ToString());
                        cmd_update.Parameters.AddWithValue("@read_dt", DateTime.Now);
                        cmd_update.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
                        cmd_update.ExecuteNonQuery();
                    }

                }


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

        protected void btn_no_deploy_Click(object sender, EventArgs e)
        {
            DataTable dt = get_detailProgress();
            int status = Convert.ToInt32(dt.Rows[0]["sws_status_c"].ToString());
            if (status == 0)
            {
                string display = "Không thành công! Bạn chưa đọc tài liệu";
                ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);
            }
            else
            {
                DataTable PICAM = checkPICAM(Session["dep_c"].ToString());
                if ((PICAM.Rows.Count > 0 && PICAM.Rows[0]["pic_username"].ToString().Trim() == Session["username"].ToString()))
                {
                    confirm_noDeploy(5);
                }
                else if ((PICAM.Rows.Count > 0 && PICAM.Rows[0]["AM_above"].ToString().Trim() == Session["username"].ToString() || Session["permit"].ToString() == "0"))
                {
                    confirm_noDeploy(9);
                }


                load_information();
                //load_DetailProgress();

            }



        }

        void confirm_noDeploy(int sws_status_c)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SWS_noDeploy", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
            cmd.Parameters.AddWithValue("@confirm_deploy_dt", DateTime.Now);
            cmd.Parameters.AddWithValue("@confirm_deploy_per", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
            cmd.Parameters.AddWithValue("@comment", txt_comment.Text.Trim());
            cmd.Parameters.AddWithValue("@sws_status_c", sws_status_c);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        protected void btn_issue_Click(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_Issue", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@approve_issue_per", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@approve_issue_dt", DateTime.Now);
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
            cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
            cmd.ExecuteNonQuery();
            con.Close();
            sendEmailtoAdmin();
            load_information();
            //load_DetailProgress();
        }

        void sendEmailtoAdmin()
        {
            string link = ConfigurationManager.AppSettings["prefixpath"].ToString() + "/SWSDocument/SWS_ViewProgress.aspx?sws_doc_id=" + Request.QueryString["id"] + "";
            string FullUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + link;

            string subject = "[SVWS Standard]Thông báo tiêu chuẩn đã cập nhật trạng thái Issued.";
            string body = @"<p>(Đây là email tự động từ hệ thống tiêu chuẩn)</p><br/>" +
                "<p><div>Một tiêu chuẩn vừa được cập nhật trạng thái Issued.</div>" +
                "<div>Admin vui lòng cập nhập mã số SVWS, chi tiết truy cập link bên dưới</div>" +
                "<a href='" + FullUrl + "'>" + FullUrl + "</a></p>" +
                "<p>[Loại tiêu chuẩn] " +
                 "<i>:Tiêu chuẩn SWS" + "</i><br/>" +
                "[Mã tiêu chuẩn] " +
                "<i>&nbsp;:" + lbl_sws_standard_c.Text + "</i><" +
                "[Tên tiêu chuẩn] " +
                "<i>&nbsp;:" + lbl_doc_nm.Text + "</i><" +
                "[Bộ phận] " +
                "<i>&nbsp;:" + lbl_dep.Text.Trim() + "</i></p>";
            send_email(dt_getEmailAdmin(), subject, body);
        }

        DataTable dt_getEmailAdmin()
        {
            con.Open();
            string qr = "select *from User_mst where permit=0";
            SqlCommand cmd = new SqlCommand(qr, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt_to = new DataTable();
            da.Fill(dt_to);
            con.Close();
            return dt_to;

        }

        void send_email(DataTable dt_to, string subject, string body)
        {
            //SmtpClient smtpClient = new SmtpClient("172.17.132.20", 25);
            SmtpClient smtpClient = new SmtpClient("202.15.130.231", 25);
            //smtpClient.Credentials = new System.Net.NetworkCredential("is-check@svws.sws.com", "123456789");
            smtpClient.Credentials = new System.Net.NetworkCredential("svws.sm.std-admin@sws.com", "");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage();
            // mailMessage.From = new MailAddress("is-check@svws.sws.com");
            mailMessage.From = new MailAddress("svws.sm.std-admin@sws.com");
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            mailMessage.To.Add(dt_to.Rows[0]["email"].ToString());

            smtpClient.Send(mailMessage);
            mailMessage.Dispose();
            mailMessage = null;
        }

        protected void btn_complete_Click(object sender, EventArgs e)
        {
            DataTable dt = get_detailProgress();
            int status = Convert.ToInt32(dt.Rows[0]["sws_status_c"].ToString());

            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_Complete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
            cmd.Parameters.AddWithValue("@complete_dt", DateTime.Now);
            cmd.Parameters.AddWithValue("@complete_per", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
            cmd.Parameters.AddWithValue("@status", status == 3 ? 4 : 6);
            cmd.Parameters.AddWithValue("@svws_std_c", txt_svws_std.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();
            load_information();
            //load_DetailProgress();
        }



        int get_status(int status)
        {
            int pointer = status;
            if (pointer == 5)
            {
                pointer = 1;
            }
            else if (pointer != 1 & pointer != 5)
            {
                pointer = pointer - 1;
            }
            return pointer;
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            //DataTable dt = get_detailProgress();
            //int status = Convert.ToInt32(dt.Rows[0]["sws_status_c"].ToString());
            //int new_status = get_status(status);
            //con.Open();
            //SqlCommand cmd = new SqlCommand("SWS_back", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@sws_status_c", new_status);
            //cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
            //cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
            //cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
            //cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
            //cmd.ExecuteNonQuery();
            //con.Close();
            //load_information();

            DataTable dt = get_detailProgress();
            int status = Convert.ToInt32(dt.Rows[0]["sws_status_c"].ToString());

            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_CancelConfirm", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@sws_status_c", status);
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text);
            cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
            cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
            cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
            cmd.ExecuteNonQuery();
            con.Close();
            load_information();




        }

        protected void btn_save_Click(object sender, EventArgs e)
        {

            string duedate = txt_duedate.Visible == true ? txt_duedate.Text.Trim() : "";
            int check = 0;
            if (Session["permit"].ToString() != "0")
            {
                if (duedate == "")
                {
                    alert.InnerHtml = "Due date không được để trống";
                    alert.Attributes.Add("class", "text-danger");
                    check = 1;
                }
            }
            else if (Session["permit"].ToString() == "0")
            {
                if (txt_svws_std.Text.Trim() == "")
                {
                    alert.InnerHtml = "Mã SVWS không được để trống";
                    alert.Attributes.Add("class", "text-danger");
                    check = 1;
                }

            }
            if (check == 0)
            {
                alert.InnerHtml = "";
                con.Open();
                SqlCommand cmd = new SqlCommand("SWS_fillInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@svws_std_c", txt_svws_std.Visible == true ? txt_svws_std.Text.Trim() : "");
                cmd.Parameters.AddWithValue("@duedate", duedate);
                cmd.Parameters.AddWithValue("@comment", txt_comment.Text.Trim());
                cmd.Parameters.AddWithValue("@swsDocument_relate_id", Request.QueryString["id"]);
                cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
                cmd.Parameters.AddWithValue("@pms_u_usr", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@pms_u_ymd", DateTime.Now);
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_duedate.Visible = true;
                lbl_svws_std.Visible = true;
                lbl_comment.Visible = true;
                txt_duedate.Visible = false;
                txt_comment.Visible = false;
                txt_svws_std.Visible = false;
                load_information();

            }


        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            if (Session["permit"].ToString() != "0")
            {
                btn_edit.Visible = false;
                btn_save.Visible = true;
                btn_cancel.Visible = true;
                txt_comment.Visible = true;
                txt_duedate.Visible = true;
                txt_duedate.Text = lbl_duedate.Text;
                lbl_duedate.Visible = false;
                lbl_comment.Visible = false;
            }
            else if (Session["permit"].ToString() == "0")
            {


                btn_edit.Visible = false;
                btn_save.Visible = true;
                btn_cancel.Visible = true;
                txt_svws_std.Visible = true;
                txt_svws_std.Text = lbl_svws_std.Text;
                lbl_svws_std.Visible = false;
                txt_duedate.Visible = true;
                txt_comment.Visible = true;
                txt_duedate.Text = lbl_duedate.Text;
                lbl_duedate.Visible = false;
                lbl_comment.Visible = false;
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_edit.Visible = true;
            btn_save.Visible = false;
            btn_cancel.Visible = false;
            txt_duedate.Visible = false;
            txt_comment.Visible = false;
            txt_svws_std.Visible = false;
            lbl_svws_std.Visible = true;
            lbl_duedate.Visible = true;
            lbl_comment.Visible = true;
        }
    }
}
using DocumentFormat.OpenXml.Office.Word;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Standard.SVWSDocument
{
    public partial class SVWSDocument_Detail : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StandardConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                // Response.Redirect("../Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                Response.Redirect("../Login.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.AbsoluteUri);

            }
            else
            {
                if (!IsPostBack)
                {
                    load_detail(Request.QueryString["doc_id"].ToString());
                }
            }

        }
        void load_detail(string doc_id)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SVWS_LoadDoc_Detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_doc_id", doc_id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string checkConfirmQuery = $"select count(*) from SVWSDocument_confirmActivate where svws_doc_id = {doc_id} and user_id = '{Session["username"]}'";
            SqlCommand cmdCheck = new SqlCommand(checkConfirmQuery, con);
            bool isConfirmed = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0;

            con.Close();
            if (dt.Rows.Count == 0)
            {
                alert.InnerHtml = "Tài liệu không tồn tại";
                alert.Attributes.Add("class", "text-danger");
            }
            else
            {
                string language = Standard.Language.value;
                alert.InnerHtml = "";
                lbl_typeofStd.Text = dt.Rows[0]["doc_type_nm"].ToString();
                lbl_stdName_vi.Text = dt.Rows[0]["svws_std_nm_vi"].ToString();
                lbl_stdName_en.Text = dt.Rows[0]["svws_std_nm_eng"].ToString();
                lbl_stdCd.Text = dt.Rows[0]["svws_std_c"].ToString();
                lbl_duedate.Text = dt.Rows[0]["finish_dt"].ToString();
                lbl_dep_make.Text = dt.Rows[0]["make_dep_c"].ToString();
                lbl_apply_dt.Text = dt.Rows[0]["apply_dt"].ToString();
                lbl_ver.Text = dt.Rows[0]["svws_std_ver"].ToString();
                //lbl_dep.Text = dt.Rows[0]["dep_c"].ToString();
                des_vi.InnerHtml = dt.Rows[0]["detail_vi"].ToString();
                des_en.InnerHtml = dt.Rows[0]["detail_eng"].ToString();
                bool isActivated = Convert.ToString(dt.Rows[0]["active_f"]) == "1";
                //litConfirm.Text = language == "en" ? "Confirm" : "Xác nhận";
                //litQuestionConfirm.Text = language == "en" ? "Do you understand this standard?" : "Xác nhận đã hiểu tiêu chuẩn này";
                //litSignUp.Text = language == "en" ? "Sign up" : "Xác nhận ký";
                //litQuestionSignUp.Text = language == "en" ? "Do you sign up to issue this standard?" : "Xác nhận đồng ý ban hành tiêu chuẩn này";
                confirm_understand.Visible = isActivated;
                confirm_activation.Visible = !isActivated;
                btnSendMailSignUp.Visible = Convert.ToInt32(Session["permit"]) < 2 && !isActivated;
                confirm_list.Visible = Convert.ToInt32(Session["permit"]) < 2;
                lblSignUpMessage.Text = language == "en" ? "Do you sign up to issue this standard?" : "Xác nhận đồng ý ban hành tiêu chuẩn này";
                if (isConfirmed)
                {
                    lblSignUpMessage.Text = language == "en" ? "You have confirmed this standard" : "Bạn đã xác nhận tiêu chuẩn này";
                    LinkButton2.Visible = false;
                }
                loadGV_file(doc_id);
                load_listDep();
            }



        }
        protected void btnSendMailSignUp_Click(object sender, EventArgs e)
        {
            con.Open();
            string svws_doc_id = Request.QueryString["doc_id"];
            using (SqlCommand cmd = new SqlCommand("SVWS_get_emails_confirming", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@svws_doc_id", svws_doc_id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    string email = row["email"].ToString();
                    string userId = row["user_id"].ToString();
                    string url = HttpContext.Current.Request.Url.ToString();
                    string rootUrl = url.Substring(0, url.LastIndexOf('/'));
                    rootUrl += "/SVWSDocument_Detail.aspx?doc_id=";

                    using (SqlCommand sendmail = new SqlCommand("SVWS_send_emails_confirming", con))
                    {
                        sendmail.CommandType = CommandType.StoredProcedure;
                        sendmail.Parameters.AddWithValue("@email", email);
                        sendmail.Parameters.AddWithValue("@user_id", userId);
                        sendmail.Parameters.AddWithValue("@root_url", rootUrl);
                        sendmail.Parameters.AddWithValue("@svws_doc_id", svws_doc_id);
                        sendmail.ExecuteNonQuery();
                    }
                }
                con.Close();
                Response.Redirect(Request.RawUrl);
            }
        }

        void loadGV_file(string doc_id)
        {
            string qr = "select svwsDoc_file_id, REVERSE(SUBSTRING(REVERSE(link_file),0,CHARINDEX('/',REVERSE(link_file)))) as link_file from SVWSDocument_file where svws_doc_id ='" + doc_id + "'";
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

            }
        }
        void confirm_doc(string doc_id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_ConfirmDoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_doc_id", doc_id);
            cmd.Parameters.AddWithValue("@dep_c", Session["dep_c"]);
            cmd.Parameters.AddWithValue("@confirm_per", Session["username"]);
            cmd.Parameters.AddWithValue("@pms_u_class", lbl_class.Text.Trim());
            cmd.Parameters.AddWithValue("@pms_u_usr", lbl_class.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();

        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["permit"].ToString()) <= 2)
            {
                confirm_doc(Request.QueryString["doc_id"]);
                load_listDep();
            }

        }
        protected void btn_confirm_issue_Click(object sender, EventArgs e)
        {
            string docId = Request.QueryString["doc_id"];
            string userId = Session["username"].ToString();
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SVWS_insert_confirm", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@doc_id", docId);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        void load_listDep()
        {
            string doc_id = Request.QueryString["doc_id"];
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select active_f from SVWSDocument where svws_doc_id = " + doc_id, con))
            {
                bool isActivated = Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                con.Close();
                if (isActivated)
                {
                    string qr = "select dep_c, confirm_f from SVWSDocument_relateDep where svws_doc_id=" + doc_id;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(qr, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    list_dep.DataSource = dt;
                    list_dep.DataBind();
                }
                else
                {
                    string qr = "select dep_c, null as confirm_f from SVWSDocument_confirmDep where svws_doc_id=" + doc_id;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(qr, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    list_dep.DataSource = dt;
                    list_dep.DataBind();
                }
            }
        }



        protected void list_dep_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataitem = (ListViewDataItem)e.Item;
                string callswaiting = DataBinder.Eval(dataitem.DataItem, "confirm_f").ToString();
                if (callswaiting == "0")
                {
                    //HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("Main");
                    HtmlTableCell row = (HtmlTableCell)e.Item.FindControl("Main");
                    //row.BgColor = Color.FromArgb(248,203,173);
                    row.BgColor = "#F8CBAD";
                }
                else if (callswaiting == "")
                {
                    string doc_id = Request.QueryString["doc_id"];
                    string dep_c = DataBinder.Eval(dataitem.DataItem, "dep_c").ToString();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SVWS_get_confirm_list_status", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@svws_doc_id", doc_id);
                    cmd.Parameters.AddWithValue("@dep_c", dep_c);
                    bool isCompleted = Convert.ToInt32(cmd.ExecuteScalar()) == 0;
                    con.Close();
                    if(!isCompleted)
                    {
                        HtmlTableCell row = (HtmlTableCell)e.Item.FindControl("Main");
                        row.BgColor = "#F8CBAD";
                    }
                }

            }
        }
    }
}
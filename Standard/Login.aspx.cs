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

namespace Standard
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StandardConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                Session.Remove("username");

                //TextBox1.Visible = true;
                //TextBox2.Visible = true;
                //Button1.Visible = true;
            }
            else
            {

                //TextBox1.Visible = true;
                //TextBox2.Visible = true;
                //Button1.Visible = true;
            }

        }

      

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Request.QueryString["returnUrl"] != null)
                //    returnUrl = Request.QueryString["returnUrl"];
                //if (returnUrl.ToLower().Contains("default.aspx"))
                //    returnUrl = returnUrl.Replace("default.aspx", "");
                //Security.authorityCls arc = new Security.authorityCls();
                //if (arc.IsLoginUser(UserName, Password))
                //    if (!string.IsNullOrEmpty(returnUrl))
                //        Response.Redirect(returnUrl);
                //    else
                //        Response.Redirect("~/");
                //alert.InnerHtml = "UserID or password is incorrect.";



                con.Open();
                string qr = "select * from User_mst where user_id ='" + TextBox1.Text.Trim() + "'and password='" + TextBox2.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(qr, con);
                //cmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim());
                //cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)

                {
                    string permit = dt.Rows[0]["permit"].ToString();
                    
                    alert.InnerHtml = "";
                    Session["username"] = TextBox1.Text.Trim();
                    Session["permit"] = permit;
                    Session["dep_c"] = dt.Rows[0]["dep_c"].ToString();
                    //((Label)Master.FindControl("lb_name")).Text = "Xin chào " + Session["username"];


                    //Response.Redirect("~/Home.aspx");
                    //var redirUrl = HttpUtility.UrlDecode(Request["ReturnUrl"]);
                    string redirUrl = Request["ReturnUrl"];


                    if (!string.IsNullOrWhiteSpace(redirUrl))
                    {
                       // redirUrl = ConfigurationManager.AppSettings["prefixpath"].ToString() + redirUrl;
                        //var mappedPath = Page.MapPath(redirUrl.Trim());
                        //if (File.Exists(redirUrl))
                        //{
                            Response.Redirect(redirUrl.Trim());
                            //Response.Redirect("http://172.17.132.28/tieuchuan/SVWSDocument/SVWSDocument_Detail.aspx?doc_id=5");
                        //}
                        
                    }
                    else
                    {
                        //Response.Redirect("http://172.17.132.28/tieuchuan/SVWSDocument/SVWSDocument_Detail.aspx?doc_id=5");
                         Response.Redirect("~/SVWSDocument/SVWSDocument.aspx");
                        
                    }

                }

                else

                {

                    alert.InnerHtml = "Thông tin đăng nhập sai";

                }

            }
            //catch (Exception ex)
            //{
            //    alert.InnerHtml = ex.ToString();
            //}
            finally
            {
                con.Close();
            }
        }
    }
}
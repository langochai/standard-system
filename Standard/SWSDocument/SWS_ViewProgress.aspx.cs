using ClosedXML.Excel;
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
    public partial class SWS_ViewProgress : System.Web.UI.Page
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
                
                 if ( Convert.ToInt32( Session["permit"].ToString()) > 3)
                {

                    alert_permit.Attributes.CssStyle.Add("display", "");
                    content.Attributes.CssStyle.Add("display", "none");
                    alert_permit.InnerHtml = "Tài khoản của bạn không thể vào chức năng này";

                }
                else if (Convert.ToInt32(Session["permit"].ToString()) <= 3)
                {
                    content.Attributes.CssStyle.Add("display", "");
                    alert_permit.Attributes.CssStyle.Add("display", "none");
                    string k = Request.QueryString["sws_doc_id"];
                    if (Request.QueryString["sws_doc_id"] != null)
                    {
                        load_GV("10", "", "", "", "", Request.QueryString["sws_doc_id"]);
                    }
                    else
                    {
                        load_GV("10", "", "", "", "", "");
                    }
                }
                
            }
            else
            {

            }

        }
        void load_GV(string sws_status_c, string sws_standard_c, string dep_c, string receipt_fromDt, string receipt_toDt, string sws_doc_id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SWSDocument_status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sws_status_c", sws_status_c);
            cmd.Parameters.AddWithValue("@sws_standard_c", sws_standard_c);
            cmd.Parameters.AddWithValue("@dep_c", dep_c);
            cmd.Parameters.AddWithValue("@receipt_fromDt", receipt_fromDt);
            cmd.Parameters.AddWithValue("@receipt_toDt", receipt_toDt);
            cmd.Parameters.AddWithValue("@sws_doc_id", sws_doc_id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GV_Document_status.DataSource = dt;
            GV_Document_status.DataBind();
            con.Close();
            lblTongsodongGV1.Text = GV_Document_status.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            load_GV(dr_status.SelectedValue.Trim(),txt_doc_c.Text.Trim(), dr_Dep.SelectedValue.Trim(), txt_receipt_fromDt.Text.Trim(), txt_receipt_toDate.Text.Trim(), "");

        }

        protected void GV_Document_status_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                string id = e.CommandArgument.ToString().Trim();
                Response.Redirect("SWS_ViewProgressDetail.aspx?id=" + id + "");
            }
        }

        protected void btn_export_Click(object sender, EventArgs e)
        {
            if (txt_receipt_fromDt.Text.Trim() == "" || txt_receipt_toDate.Text.Trim() == "")
            {
                alert.InnerHtml = "Bạn phải chọn Ngày nhận từ - đến";
                alert.Attributes.Add("css", "text-danger");
            }
            else
            {
                alert.InnerHtml = "";
                try
                {


                    string path1 = Server.MapPath("/File/Form/Export_SWS.xlsx");
                    XLWorkbook xlWorkbook1 = new XLWorkbook(path1);
                    string filename = "sample1 " + DateTime.Today.ToShortDateString();
                    Filldata(xlWorkbook1, filename);
                }
                catch (Exception ex)
                {
                    alert.InnerHtml = ex.Message;
                    alert.Attributes.CssStyle.Add("css", "text-danger");
                }
            }
        }
        private void Filldata(XLWorkbook xlWorkbook, string filename)
        {

            //try
            //{
            alert.InnerHtml = "";
            string fromdate = txt_receipt_fromDt.Text.Trim();
            string todate = txt_receipt_toDate.Text.Trim();

            
            con.Open();
            SqlCommand cmd = new SqlCommand("SWS_exportdata", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@receipt_dt_from", fromdate);
            cmd.Parameters.AddWithValue("@receipt_dt_to", todate);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
           
            IXLWorksheet worksheets = xlWorkbook.Worksheet(1);

                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        for (int k = 0; k < 13; k++)
                        {
                            worksheets.Row(j+8).Cell(k+1).Value = dt.Rows[j][k].ToString();
                        }
                        worksheets.Row(j+8).Cell(15).Value = dt.Rows[j][13].ToString();

                    }

                }

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

            response.Clear();
            response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".xlsx;");
            MemoryStream stream = new MemoryStream();

            xlWorkbook.SaveAs(stream);
            stream.WriteTo(response.OutputStream);
            stream.Close();
            response.End();


            xlWorkbook.Dispose();






            //}
            //catch (Exception ex)
            //{
            //    alert.InnerHtml = ex.Message;

            //}




        }

        protected void GV_Document_status_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_Document_status.PageIndex = e.NewPageIndex;
            load_GV(dr_status.SelectedValue.Trim(), txt_doc_c.Text.Trim(), dr_Dep.SelectedValue.Trim(), txt_receipt_fromDt.Text.Trim(), txt_receipt_toDate.Text.Trim(), "");
            alert.InnerHtml = "";
        }
    }
}
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Standard.SVWSDocument
{
    public partial class SVWSDocument_ConfirmList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StandardConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
               // Response.Redirect("../Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery));
                Response.Redirect("../Login.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.AbsoluteUri);

            }
            btnExport.Text = Standard.Language.value == "vi"? "Xuất file" : "Export";
            loadData();
        }
        #region Grid view

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            loadData();
            //loadGV(txt_doc_c.Text.Trim(), txt_doc_nm.Text.Trim(), dr_dep.SelectedValue.Trim(), dr_type_doc.SelectedValue.Trim());
            //alert.InnerHtml = "";
        }
        #endregion
        void loadData()
        {
            DataTable dt = getData();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            lblTongsodongGV1.Text = GridView1.Rows.Count.ToString() + "/" + dt.Rows.Count.ToString() + " bản ghi";
        }
        DataTable getData()
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("SVWS_get_confirmation_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@svws_doc_id", Request.QueryString["doc_id"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = getData();
            string standardName = "", standardCode = "", standardVersion = "";
            con.Open();
            string query = $"SELECT svws_std_nm_vi,svws_std_c,svws_std_ver FROM [SVWSDocument] WHERE [svws_doc_id] = {Request.QueryString["doc_id"]}";

            using (SqlCommand command = new SqlCommand(query, con))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        standardName = reader.GetString(0);
                        standardCode = reader.GetString(1);
                        standardVersion = reader.GetString(2);
                    }
                }
            }
            con.Close();
            string filePath = Server.MapPath("~/Assets/ConfirmList.xlsx");
            if (File.Exists(filePath))
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    int startRow = 17;
                    string departments = string.Join(", ", dt.AsEnumerable()
                                                .Select(row => row.Field<string>("department"))
                                                .Distinct()
                                                .OrderBy(value => value));
                    worksheet.Cell(6, 3).Value += departments;
                    worksheet.Cell(11, 9).Value = standardName;
                    worksheet.Cell(12, 7).Value = standardCode;
                    worksheet.Cell(13, 6).Value = standardVersion;
                    foreach (DataRow row in dt.Rows)
                    {
                        worksheet.Cell(startRow, 3).Value = row[0].ToString();
                        worksheet.Cell(startRow, 5).Value = row[2].ToString();
                        worksheet.Cell(startRow, 8).Value = row[5].ToString();
                        worksheet.Cell(startRow, 11).Value = row[6].ToString();
                        worksheet.Cell(startRow, 20).Value = row[4].ToString() == "1" ? "☑ Có" : ""; //"☒ Không";
                        worksheet.Cell(startRow, 27).Value = row[3] == DBNull.Value? "": Convert.ToDateTime(row[3]).ToString("yyyy-MM-dd hh:mm:ss");
                        worksheet.Cell(startRow, 27).DataType = XLDataType.DateTime;
                        startRow += 1;
                    }
                    using (var newStream = new MemoryStream())
                    {
                        workbook.SaveAs(newStream);
                        newStream.Seek(0, SeekOrigin.Begin);
                        Response.Clear(); 
                        Response.Buffer = true; 
                        Response.AddHeader("content-disposition", "attachment;filename=Danh_sách_xác_nhận.xlsx"); 
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; 
                        Response.BinaryWrite(newStream.ToArray()); 
                        Response.End();
                    }
                }
            }
        }
    }
}
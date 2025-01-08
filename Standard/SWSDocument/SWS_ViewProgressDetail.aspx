<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SWS_ViewProgressDetail.aspx.cs" Inherits="Standard.SWSDocument.SWS_ViewProgressDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="../resources/jquery.min.js"></script>
    <script src="../resources/popper.min.js"></script>
    <%--<script src="resources/bootstrap.min.js"></script>
    <link href="resources/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="../resources/jquery-ui.css" rel="stylesheet" />
    <%--<script src="resources/jquery-1.12.4.js"></script>--%>
    <script src="../resources/jquery-ui.js"></script>


    

  <script type="text/javascript">

      $(function () {
          $("[id$=txt_duedate]").datepicker({
              showOn: 'button',
              buttonImageOnly: true,
              buttonImage: '../Images/calendar.png',
              dateFormat: 'dd-M-yy' 
          });
      });

  </script>

    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSWSVPDtl01)"></asp:Label>
                        
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn SWS /" : "SWS Standard /"  ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                               <a href="../SWSDocument/SWS_ViewProgress.aspx" style="color:white">
                                    <%Response.Write(Standard.Language.value == "vi" ? "Tiến độ triển khai /" : "View Progress /"  ); %>
                                </a>
                            </li>
                            <li style="padding-top:9px;"><i ></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Chi tiết" : "Detail " ); %>

                            </li>
                            
                        </ol>
                   
                    </div>
    
    </div>
    <div class="container2">
        <span style="font-size:14px">
            <%Response.Write(Standard.Language.value == "vi" ? "Xem chi tiết" : "View detail"  ); %>
        </span>
        <div style="padding-top:10px; padding-bottom:10px">
            <asp:Button ID="btn_edit"  runat="server" Text="Sửa" Visible="false" OnClick="btn_edit_Click"  />
            <asp:Button ID="btn_save"  runat="server" Text="Lưu" OnClick="btn_save_Click" Visible="false" />
            <%--<asp:Button ID="btn_saveSVWS"  runat="server" Text="Lưu"  Visible="false" />--%>
            <asp:Button ID="btn_cancel"  runat="server" Text="Hủy" OnClick="btn_cancel_Click"  Visible="false" />
        </div>
        <div id="alert" runat="server"></div>
        <table style="width:100%">
            <tr>
                <td style="width:20%; height: 22px;">
                    <%Response.Write(Standard.Language.value == "vi" ? "Mã tiêu chuẩn:" : "Document code:"  ); %>
                </td>
                <td style="width:30%; height: 22px; color:#C65911">
                    <asp:Label ID="lbl_sws_standard_c" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width:20%; height: 22px;">
                    <%Response.Write(Standard.Language.value == "vi" ?  "Trạng thái" : "Status" ); %>
                </td>
                <td style="width:30%; height: 22px;color:#C65911; font-weight:bold">
                    <asp:Label ID="lbl_status" runat="server" ></asp:Label>

                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <%Response.Write(Standard.Language.value == "vi" ? "Ver" : "Ver"); %>
                </td>
                <td style="width:30%; color:#C65911">
                    <asp:Label ID="lbl_ver" runat="server"  ></asp:Label>
                </td>
                <td style="width:20%">
                    <%Response.Write(Standard.Language.value == "vi" ? "Kế hoạch hoàn thành" : "Kế hoạch hoàn thành"); %>
                </td>
                <td style="width:30%; color:#C65911">
                    
                    <asp:TextBox ID="txt_duedate" runat="server" Visible="false"></asp:TextBox>
                     
                    <asp:Label ID="lbl_duedate" runat="server" Visible="false" ></asp:Label>
                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txt_duedate"
                                CssClass="text-danger" ErrorMessage="required." />--%>
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <%Response.Write(Standard.Language.value == "vi" ? "Tên tiêu chuẩn" : "Document name"  ); %>
                </td>
                <td style="width:30%; color:#C65911">
                    <asp:Label ID="lbl_doc_nm" runat="server" ></asp:Label>
                </td>
                <td style="width:20%">
                    <%Response.Write(Standard.Language.value == "vi" ? "Mã TC SVWS" : "SVWS standard code"  ); %>
                </td>
                <td style="width:30%; color:#C65911">
                    <asp:Label ID="lbl_svws_std" runat="server" Visible="false" ></asp:Label>
                    <asp:TextBox ID="txt_svws_std" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <%Response.Write(Standard.Language.value == "vi" ?  "Ngày nhận" : "Receipt date" ); %>

                </td>
                <td style="width:30%; color:#C65911">
                    <asp:Label ID="lbl_receiptdt" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <%Response.Write(Standard.Language.value == "vi" ? "Bộ phận" : "Section"  ); %>
                </td>
                <td style="width:30%; color:#C65911">
                    <asp:Label ID="lbl_dep" runat="server" ></asp:Label>
                </td>
            </tr>
             <tr>
                <td style="width:20%">
                     <%Response.Write(Standard.Language.value == "vi" ? "Ghi chú" : "Comment"  ); %>
                </td>
                <td style="width:30%" >
                    <asp:Label ID="lbl_comment" runat="server" Visible="false"  ></asp:Label>
                    <asp:TextBox ID="txt_comment" Width="100%" runat="server" TextMode="MultiLine" Visible="false" ></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        File:
        <br />
        <asp:Repeater id="fileLinks" runat="server" OnItemCommand="fileLinks_ItemCommand">   
           <ItemTemplate>
               <table>
                   <tr>
                       <td>

                       </td>
                       <td>
                           <asp:LinkButton ID="lnk_file" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"swsDoc_file_id") %>' CommandName="download"  runat="server" Text='<%# Bind("link_file") %>'></asp:LinkButton>
                           <%--<asp:HyperLink ID="linkid" runat="server" NavigateUrl='<%# Container.DataItem.ToString() %>' Text='<%# Bind("link_file") %>'  />--%>
                       </td>
                   </tr>
               </table>

           </ItemTemplate>
        </asp:Repeater>
        <div class="menu_control">
            <span>
                <asp:Button ID="btn_back" runat="server" Enabled="false" class="btn btn-warning" Text="Hủy xác nhận" OnClick="btn_back_Click" />
            </span>
            <span style="padding-left:5px">
                <asp:Button ID="btn_deploy" runat="server" Enabled="false" CssClass="btn btn-primary" Text="Xác nhận triển khai" OnClick="btn_deploy_Click" OnClientClick="return confirm ('Bạn có chắc sẽ Xác nhận triển khai?');" />
            </span>
            <span style="padding-left:5px">
                <asp:Button ID="btn_no_deploy" runat="server" Enabled="false" CssClass="btn btn-primary" Text="Không triển khai" OnClick="btn_no_deploy_Click" OnClientClick="return confirm ('Bạn có chắc sẽ Không triển khai?');" />
            </span>
            <span style="padding-left:5px">
                <asp:Button ID="btn_issue" runat="server" Enabled="false" CssClass="btn btn-primary" Text="Issue" OnClick="btn_issue_Click" OnClientClick="return confirm ('Bạn có chắc sẽ Issue tiêu chuẩn?');" />
            </span>
            <span style="padding-left:5px">
                <asp:Button ID="btn_complete" runat="server" Enabled="false" CssClass="btn btn-primary" Text="Hoàn thành" OnClick="btn_complete_Click" OnClientClick="return confirm ('Bạn có chắc sẽ Hoàn thành?');" />
            </span>
            
        </div>
        <div id="alert_btn" runat="server"></div>
        <div class="detail_progress">
            <asp:GridView ID="GV_DetailProgress" Width="60%" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="activity" HeaderText="Activity" />
                    <asp:BoundField DataField="user" HeaderText="User" />
                    <asp:BoundField DataField="datetime" HeaderText="Date time" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#c9c9c9" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

        </div>
    </div>
</asp:Content>

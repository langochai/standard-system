<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SWS_ViewProgress.aspx.cs" Inherits="Standard.SWSDocument.SWS_ViewProgress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
     <div id="alert_permit" runat="server" style="display:none; color:red; font-style:italic"></div>
   <div id="content" style="display:" runat="server">
    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSWSVP01)"></asp:Label>
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn SWS /" : "SWS Standard /"  ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                 <a href="../SWSDocument/SWS_ViewProgress.aspx" style="color:white">
                                    <%Response.Write(Standard.Language.value == "vi" ? "Tiến độ triển khai" : "View Progress"  ); %>
                                </a>
                                
                            </li>
                            
                            <li style="float:right;">
                                   
                                <%--<asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search"></asp:TextBox>--%>
                            </li>
                           
                        </ol>
                   
                    </div>
    
    </div>
    <div class="container2">
        <%--<div class="col-lg-12">--%>
            <table style="width:50%">
                <tr>
                    <td style="height: 30px"><%Response.Write(Standard.Language.value == "vi" ? "Trạng thái" : "Status" ); %></td>
                    <td style="height: 30px" colspan="3">
                        <asp:DropDownList ID="dr_status" Width="100%" AppendDataBoundItems="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="sws_status_nm" DataValueField="sws_status_c">
                            <asp:ListItem Value="10">ALL</asp:ListItem>
                        </asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT * FROM [SWSStatus_mst] where sws_status_c!=7 ORDER BY [sws_status_c]"></asp:SqlDataSource>

                    </td>
                    <td rowspan="2"; style="padding-left:10px">
                        
                        <asp:Button ID="btn_search" class="btn btn-primary" runat="server" Text="Search" OnClick="btn_search_Click" />
                        
                    </td>
                </tr>

                <tr>
                    <td style="height: 30px"><%Response.Write(Standard.Language.value == "vi" ?  "Mã tiêu chuẩn" : "Document code" ); %></td>
                    <td style="height: 30px" colspan="3">
                        <asp:TextBox ID="txt_doc_c" Width="100%" runat="server"></asp:TextBox>

                    </td>
                </tr>

                <tr>
                    <td style="height: 30px"><%Response.Write(Standard.Language.value == "vi" ? "Bộ phận" : "Section"  ); %></td>
                    <td style="height: 30px" colspan="3" >
                        <asp:DropDownList ID="dr_Dep" Width="100%" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDataSource2" DataTextField="dep_c" DataValueField="dep_c">
                            <asp:ListItem Value="">ALL</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT [dep_c], [dep_nm] FROM [Dep_mst] ORDER BY [dep_c]"></asp:SqlDataSource>
                    </td>
                    <td style="padding-left:10px">
                        <asp:Button ID="btn_export" CssClass="btn btn-primary" runat="server" Text="Export" OnClick="btn_export_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%Response.Write(Standard.Language.value == "vi" ?  "Ngày nhận từ" : "Receipt from date" ); %>
                    </td>
                    <td >
                        <asp:TextBox ID="txt_receipt_fromDt" Width="100px" runat="server"></asp:TextBox>
                    </td>
                
                    <td>
                        <%Response.Write(Standard.Language.value == "vi" ? "Đến" : "To date"  ); %>
                    </td>
                    <td >
                        <asp:TextBox ID="txt_receipt_toDate" Width="100px" runat="server"></asp:TextBox>

                    </td>
                </tr>
                
            </table>
        <br />

        <%--</div>--%>
        <div id="alert" runat="server"></div>
        <br />
        <div>
            <asp:GridView Width="100%" ID="GV_Document_status" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                 OnRowCommand="GV_Document_status_RowCommand" AllowPaging="True" PageSize="15" OnPageIndexChanging="GV_Document_status_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="stt" HeaderText="STT" ItemStyle-Width="5px" />
                    <asp:TemplateField HeaderText="Trạng thái">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("sws_status_nm") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("sws_status_nm") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bộ phận">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("dep_c") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("dep_c") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mã tiêu chuẩn SWS">
                       
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("sws_standard_c") %>' 
                                                CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"swsDocument_relate_id") %>' ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ver">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("sws_standard_ver") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("sws_standard_ver") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Mã SVWS">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_svws" runat="server" Text='<%# Bind("svws_std_c") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_svws" runat="server" Text='<%# Bind("svws_std_c") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tên tiêu chuẩn">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("sws_standard_nm_vi") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("sws_standard_nm_vi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày nhận">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("receive_dt") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("receive_dt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle  Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#C9C9C9" Font-Bold="True" ForeColor="White" />
                <PagerStyle  ForeColor="black" HorizontalAlign="right" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div style="height:20px; font-style:italic;"><asp:Label ID="lblTongsodongGV1" runat="server"></asp:Label></div>
    </div>
   </div>

        <script src="../resources/jquery.min.js"></script>
    <script src="../resources/popper.min.js"></script>
    <link href="../resources/jquery-ui.css" rel="stylesheet" />
    <script src="../resources/jquery-ui.js"></script>
   
     <%--<script src="resources/jquery.min.js"></script>
    <script src="resources/popper.min.js"></script>
    <link href="resources/jquery-ui.css" rel="stylesheet" />
    <script src="resources/jquery-ui.js"></script>--%>


  <script type="text/javascript">

      $(function () {
          $("[id$=txt_receipt_fromDt]").datepicker({
              showOn: 'button',
              buttonImageOnly: true,
              buttonImage: '../Images/calendar.png',
              dateFormat: 'dd-M-yy' 
          });
      });

      $(function () {
          $("[id$=txt_receipt_toDate]").datepicker({
              showOn: 'button',
              buttonImageOnly: true,
              buttonImage: '../Images/calendar.png',
              dateFormat: 'dd-M-yy' 
          });
      });

  </script>


   
</asp:Content>

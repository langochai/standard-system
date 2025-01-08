<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Admin_typeofdoc.aspx.cs" Inherits="Standard.Administrator.Admin_typeofdoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <div id="alert_permit" runat="server" style="display:none; color:red; font-style:italic"></div>
    <div id="content" style="display:none" runat="server">
    <div  class="row"   >
        
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDADMINTOD)"></asp:Label>
                        
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Quản lý /" : "Administrator /" ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../Administrator/Admin_typeofdoc.aspx" style="color:white">
                                     <%Response.Write(Standard.Language.value == "vi" ? "Loại tài liệu" : "Type of Document"  ); %>
                                </a>
                            </li>
                            
                                <li style="float:right">      
                                    <asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search" OnTextChanged="txt_search_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>
                           
                        </ol>
                   
                    </div>
    
    </div>
    <div class="row">
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading" style="font-size:15px; font-weight:bold;">
                <%Response.Write(Standard.Language.value == "vi" ?  "Loại tài liệu" : "Type of Document" ); %>
              
            </header>
            <div id="alert" runat="server" style="font-style:italic; color:red" ></div>
            <asp:GridView ID="GV_TypeofDoc" Width="60%" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
                AutoGenerateColumns="False" DataKeyNames="doc_type_id" OnRowEditing="GV_TypeofDoc_RowEditing" 
                 OnRowUpdating="GV_TypeofDoc_RowUpdating" OnRowCancelingEdit="GV_TypeofDoc_RowCancelingEdit"
                OnRowDeleting="GV_TypeofDoc_RowDeleting" OnRowCommand="GV_TypeofDoc_RowCommand" ShowFooter="True" AllowPaging="True"
                 OnPageIndexChanging="GV_TypeofDoc_PageIndexChanging" PageSize="15">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775"
                     />
                <Columns>
                    <asp:BoundField DataField="STT" HeaderText="STT" InsertVisible="False" ReadOnly="True" SortExpression="STT" />
                    <asp:TemplateField HeaderText="Loại tài liệu" SortExpression="doc_type_nm" ItemStyle-Width="70%">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_doc_typenm" runat="server" Text='<%# Bind("doc_type_nm") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("doc_type_nm") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_doc_typenm_footer" runat="server" Width="100%" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server"  CommandName="Edit">Sửa</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" OnClientClick="return confirm ('bạn có chắc muốn xóa?');">Xóa</asp:LinkButton>
                        </ItemTemplate>
                         <EditItemTemplate>
                             <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update">Lưu</asp:LinkButton>
                             <asp:LinkButton ID="LinkButton4" runat="server"  CommandName="Cancel">Hủy</asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="AddNew">Thêm</asp:LinkButton>

                        </FooterTemplate>
                </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#F7F6F3" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#c9c9c9" Font-Bold="True" ForeColor="White" />
                <PagerStyle  ForeColor="black" HorizontalAlign="right" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
           
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT [doc_type_id], [doc_type_nm] FROM [Document_type_mst] ORDER BY [doc_type_nm]"></asp:SqlDataSource>
           
        </section>
        <div style="height:20px; font-style:italic;"><asp:Label ID="lblTongsodongGV1" runat="server"></asp:Label></div>
    </div>
        </div>
    </div>
</asp:Content>

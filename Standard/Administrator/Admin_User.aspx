<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Admin_User.aspx.cs" Inherits="Standard.Administrator.Admin_User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <div id="alert_permit" runat="server" style="display:none; color:red; font-style:italic"></div>
    <div id="content" style="display:none" runat="server">
    <div  class="row"   >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDADMINUR)"></asp:Label>
                        
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ?  "Quản lý /" : "Administrator /" ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../Administrator/Admin_User.aspx" style="color:white">
                                     <%Response.Write(Standard.Language.value == "vi" ? "Người dùng" : "Users" ); %>
                                </a>
                            </li>
                            
                                <li style="float:right">      
                                    <asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search" OnTextChanged="txt_search_TextChanged"  AutoPostBack="true"></asp:TextBox>
                                </li>
                           
                        </ol>
                   
                    </div>
    
    </div>
    <div class="row">
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading" style="font-size:15px; font-weight:bold;">
                <%Response.Write(Standard.Language.value == "vi" ? "Người dùng" : "Users" ); %>
              
            </header>
            <div id="alert" runat="server" style="font-style:italic; color:red" ></div>
            <asp:GridView ID="GV_User" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
                AutoGenerateColumns="False" DataKeyNames="user_id" OnRowEditing="GV_User_RowEditing"
                OnRowCancelingEdit="GV_User_RowCancelingEdit" OnRowUpdating="GV_User_RowUpdating"
                 OnRowCommand="GV_User_RowCommand" OnRowDeleting="GV_User_RowDeleting" ShowFooter="True" AllowPaging="True"
                 OnPageIndexChanging="GV_User_PageIndexChanging" PageSize="15">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                     <asp:BoundField DataField="STT" HeaderText="STT" ItemStyle-Width="5px" ReadOnly="True" />
                    <asp:TemplateField HeaderText="user_id" SortExpression="user_id">
                       <%-- <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("user_id") %>'></asp:Label>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("user_id") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_userfooter" runat="server" style="max-width:135px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Họ tên" SortExpression="fullname">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_fullname" runat="server" Text='<%# Bind("fullname") %>' style="max-width:135px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("fullname") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_fullnamefooter" runat="server" style="max-width:135px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="password" SortExpression="password">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_pass" runat="server" Text='<%# Bind("password") %>' style="max-width:135px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("password") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_passfooter" runat="server" style="max-width:135px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bộ phận" SortExpression="dep_c">
                        <EditItemTemplate>
                            <asp:DropDownList ID="dr_dep" runat="server" DataSourceID="SqlDataSource1" SelectedValue='<%# Bind("dep_c") %>' DataTextField="dep_c" DataValueField="dep_c"></asp:DropDownList>    
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("dep_c") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="dr_depfooter" runat="server" DataSourceID="SqlDataSource1" DataValueField="dep_c" DataTextField="dep_c"></asp:DropDownList>
                             
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="email" SortExpression="email">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_email" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_emailfooter" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Giới tính" SortExpression="sex">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("sex") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("sex") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="permit" SortExpression="permit_nm">
                        <EditItemTemplate>
                            <asp:DropDownList ID="dr_permit" runat="server" DataSourceID="SqlDataSource2" SelectedValue='<%# Bind("permit") %>' DataTextField="permit_nm" DataValueField="permit"></asp:DropDownList>
                           
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("permit_nm") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="dr_permitfooter" runat="server" DataSourceID="SqlDataSource2" DataTextField="permit_nm" DataValueField="permit"></asp:DropDownList>
                            
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server"  CommandName="Edit">Sửa</asp:LinkButton> |
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" OnClientClick="return confirm ('bạn có chắc muốn xóa?');">Xóa</asp:LinkButton>
                        </ItemTemplate>
                         <EditItemTemplate>
                             <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update">Lưu</asp:LinkButton> |
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
                <PagerStyle ForeColor="black" HorizontalAlign="right" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
           
           <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="select *from Permit_mst where permit !=0 order by permit desc"></asp:SqlDataSource> 
           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT [dep_c] FROM [Dep_mst] ORDER BY [dep_c]"></asp:SqlDataSource>
        </section>
        <div style="height:20px; font-style:italic;"><asp:Label ID="lblTongsodongGV1" runat="server"></asp:Label></div>
    </div>
        </div>
    </div>
</asp:Content>

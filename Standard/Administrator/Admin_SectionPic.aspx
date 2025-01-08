<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Admin_SectionPic.aspx.cs" Inherits="Standard.Administrator.Admin_SectionPic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <div id="alert_permit" runat="server" style="display:none; color:red; font-style:italic"></div>
    <div id="content" style="display:none" runat="server">
    <div  class="row"   >
        
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDADMINSP01)"></asp:Label>
                        
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Quản lý /" : "Administrator /"  ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../Administrator/Admin_typeofdoc.aspx" style="color:white">
                                     <%Response.Write(Standard.Language.value == "vi" ? "Bộ phận-Người phụ trách" : "Section-Pic"  ); %>
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
                <%Response.Write(Standard.Language.value == "vi" ? "Bộ phận - Người phụ trách" : "Section-PIC"  ); %>
              
            </header>
            <div id="alert" runat="server" style="font-style:italic; color:red" ></div>
            <asp:GridView ID="GV_SectionPic" runat="server" Width="80%" CellPadding="4" ForeColor="#333333" GridLines="None" 
                AutoGenerateColumns="False" DataKeyNames="dep_c"
                 OnRowEditing="GV_SectionPic_RowEditing" OnRowCancelingEdit="GV_SectionPic_RowCancelingEdit"
                 OnRowUpdating="GV_SectionPic_RowUpdating" OnRowCommand="GV_SectionPic_RowCommand"
                 OnRowDeleting="GV_SectionPic_RowDeleting" ShowFooter="True" AllowPaging="True" PageSize="15" OnPageIndexChanging="GV_SectionPic_PageIndexChanging" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775"
                     />
                <Columns>
                    <asp:TemplateField HeaderText="STT">
                       <%-- <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STT") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("STT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bộ phận" SortExpression="dep_c">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("dep_c") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("dep_c") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_depfooter" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nhà máy" SortExpression="factory">
                        <EditItemTemplate>
                            <asp:DropDownList ID="factory" runat="server" SelectedValue='<%# Bind("factory") %>' DataTextField="factory" DataValueField="factory">
                                <asp:ListItem Text="Hà Nam" Value="Hà Nam"></asp:ListItem> 
                                <asp:ListItem Text="Nam Định" Value="Nam Định"></asp:ListItem> 
                                <asp:ListItem Text="Thanh Liêm" Value="Thanh Liêm"></asp:ListItem>
                            </asp:DropDownList>    
                        </EditItemTemplate>

                        <ItemTemplate>
                            <asp:Label ID="lbl_factory" runat="server" Text='<%# Bind("factory") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <FooterTemplate>
                            <asp:DropDownList ID="dr_factory" runat="server" SelectedValue='<%# Bind("factory") %>' DataValueField="factory" DataTextField="factory">
                                <asp:ListItem Text="Hà Nam" Value="Hà Nam"></asp:ListItem> 
                                <asp:ListItem Text="Nam Định" Value="Nam Định"></asp:ListItem> 
                                <asp:ListItem Text="Thanh Liêm" Value="Thanh Liêm"></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PIC" SortExpression="pic_username">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_pic" runat="server" Text='<%# Bind("pic_username") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("pic_username") %>'></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:TextBox ID="txt_Picfooter" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="AM_above" SortExpression="AM_above">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_AM" runat="server" Text='<%# Bind("AM_above") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("AM_above") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt_AMfooter" runat="server"></asp:TextBox>
                            
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
                <PagerStyle ForeColor="black" HorizontalAlign="right" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
           
        </section>
        <div style="height:20px; font-style:italic;"><asp:Label ID="lblTongsodongGV1" runat="server"></asp:Label></div>
    </div>
        </div>
    </div>
</asp:Content>

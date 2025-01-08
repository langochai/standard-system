<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SWSDocument_Detail.aspx.cs" Inherits="Standard.SWSDocument.SWSDocument_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    
    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSWSMDDtl01)"></asp:Label>
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "en" ? "SWS Standard /" : "Tiêu chuẩn SWS /"); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../SWSDocument/SWSDocument.aspx" style="color:white">
                                    <%Response.Write(Standard.Language.value == "en" ? "Manage Document /" : "Quản lý tiêu chuẩn /"); %>
                                </a> 
                            </li>
                            <li style="padding-top:9px;"><i ></i>
                                <%Response.Write(Standard.Language.value == "en" ? "Detail Document" : "Thông tin chi tiết"); %>
                            </li>
                            
                                <li style="float:right">
                                   
                                    <%--<asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search"></asp:TextBox>--%>

                                </li>
                           
                        </ol>
                   
                    </div>
    
    </div>
    <asp:Label ID="lbl_id" runat="server" Visible="false"></asp:Label>
     
    <div id="form_container">  
         <header class="panel-heading">
                <div style="font-size:18px; font-weight:bold">
                    
                     <%Response.Write(Standard.Language.value == "en" ? "View Document" : "Thông tin chi tiết"); %>
                </div>
            </header>
 
            <i>Những mục đánh dấu (*) là bắt buộc</i>

        

    <table class="created_table" style="margin-top:10px;"  >
        <tbody>
            <tr>
                <td >  
                    <%Response.Write(Standard.Language.value == "en" ? "Standard code SWS(*)" : "Mã tiêu chuẩn SWS(*)"); %>
                   
  
                </td>
                 <td>
                     <asp:Label ID="lbl_swsDocument_cd" runat="server" ForeColor="#C65911" ></asp:Label>
                     
                </td>

            </tr>
         
            <tr>
                 <td>
                     <%Response.Write(Standard.Language.value == "en" ? "Ver(*)" : "Phiên bản"); %>
                    
                </td>
                <td>
                    <asp:Label ID="lbl_ver" runat="server" ForeColor="#C65911" ></asp:Label>
                    
                </td>
             
            </tr>
            <tr>
                 <td>   
                     <%Response.Write(Standard.Language.value == "en" ? "Standard name SWS (vi)(*)" : "Tên tiêu chuẩn SWS (vi)(*)"); %>

                </td>
                <td>
                    <asp:Label ID="lbl_nameVi" runat="server" ForeColor="#C65911" ></asp:Label>
                     
                </td>
             
            </tr>
            <tr>
                <td> 
                    <%Response.Write(Standard.Language.value == "en" ? "Standard name SWS (en)" : "Tên tiêu chuẩn SWS (en)"); %>
                     
                </td>
                <td>
                    <asp:Label ID="lbl_nameEN" runat="server" ForeColor="#C65911" ></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                     <%Response.Write(Standard.Language.value == "en" ? "Receive date(*)" : "Ngày nhận(*)"); %>
                </td>
                 <td>
                     <asp:Label ID="lbl_Receive_dt" runat="server" ForeColor="#C65911" ></asp:Label>
                   
                </td>
            </tr>
            <tr>
                <td>
                     <%Response.Write(Standard.Language.value == "en" ? "Comment" : "Ghi chú"); %>
                </td>
                <td>
                    <asp:Label ID="lbl_comment" runat="server" ForeColor="#C65911" ></asp:Label>
                    
                </td>
            </tr>
              </tbody>
    </table>

         <div id="alert" runat="server" ></div>
    <%--<input id="saveForm" class="button_text" type="submit" name="btn_add" value= "Thêm mới" />--%>
    <div id="attach_content" runat="server" style="margin-top:10px; display:" >
        <table style="width:100%;">
            <tr>
                <td style="width:50%; vertical-align:top">
                    <div style="padding:10px;">
                        <%Response.Write(Standard.Language.value == "en" ? "file document" : "File tài liệu"); %>
                       
                        <br />
                        <div style="padding:10px;" >
                              <span style="clear:both"></span>
                            
                            <%--<asp:LinkButton ID="LinkButton1" runat="server">Upload</asp:LinkButton>--%>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" style="width:100%; margin-top:20px;" CellPadding="4" ForeColor="#333333" 
                                GridLines="None" ShowHeaderWhenEmpty="True" DataKeyNames="swsDoc_file_id"
                                 OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="File name">
                                        <ItemTemplate>
                                            
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("link_file") %>' 
                                                CommandName="download" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"swsDoc_file_id") %>' ></asp:LinkButton>
                                        </ItemTemplate>

                                        <ItemStyle Width="90%" />

                                    </asp:TemplateField>
                                   
                                    
                                    
                                  
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
                </td>
                <td style="vertical-align:top">
                    <div style="padding:10px;">
                        <%Response.Write(Standard.Language.value == "en" ? "Related Section" : "Bộ phận liên quan"); %>
                       
                        <div style="padding:10px;">
                            <asp:GridView ID="GV_section" runat="server" AutoGenerateColumns="False" style="width:100%; margin-top:20px;" CellPadding="4"
                                ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" 
                                OnRowDeleting="GV_section_RowDeleting" DataKeyNames="swsDocument_relate_id">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField  HeaderText="Section" >
                                        <ItemTemplate  >
                                            <asp:HyperLink ID="HyperLink1" runat="server"  Text='<%# Bind("dep_c") %>' ></asp:HyperLink>
                                            
                                        </ItemTemplate>
                                        <ItemStyle Width="90%" />
                                    </asp:TemplateField>
                                    

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

                </td>
            </tr>
        </table>
        
       
    </div>

    <div id="footer-content">

    </div>
</div>


</asp:Content>

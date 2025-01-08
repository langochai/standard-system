<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SWSDocument_Edit.aspx.cs" Inherits="Standard.SWSDocument_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="../resources/jquery.min.js"></script>
    <script src="../resources/popper.min.js"></script>
    <link href="../resources/jquery-ui.css" rel="stylesheet" />
    <script src="../resources/jquery-ui.js"></script>

      <script type="text/javascript">

          $(function () {
              $("[id$=Receive_dt]").datepicker({
                  showOn: 'button',
                  buttonImageOnly: true,
                  buttonImage: '../Images/calendar.png',
                  dateFormat: 'dd-M-yy' 
              });
          });

  </script>
    
    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSWSMDEdit01)"></asp:Label>
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn SWS /" : "SWS Standard /"  ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../SWSDocument/SWSDocument.aspx" style="color:white">
                                    <%Response.Write(Standard.Language.value == "vi" ?  "Quản lý tiêu chuẩn /" : "Manage Document /" ); %>
                                </a> 
                            </li>
                            <li style="padding-top:9px;"><i ></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Thông tin chi tiết" : "Detail Document"  ); %>
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
                    
                     <%Response.Write(Standard.Language.value == "vi" ? "Thông tin chi tiết" : "View Document"  ); %>
                </div>
            </header>
 
            <i>Những mục đánh dấu (*) là bắt buộc</i>

        

    <table class="created_table" style="margin-top:10px;"  >
        <tbody>
            <tr>
                <td >  
                    <%Response.Write(Standard.Language.value == "vi" ? "Mã tiêu chuẩn SWS(*)" : "Standard code SWS(*)"  ); %>
                   
  
                </td>
                 <td>
                     
                     <asp:TextBox ID="txt_swsDocument_cd" runat="server" ></asp:TextBox>
                     <%--<input id="element_11" name="element_11" class="element text medium" type="text" maxlength="255" value="" placeholder="Nhập mã số quản lý" />--%>
                      <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_swsDocument_cd"
                                CssClass="text-danger" ErrorMessage="required." />
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                            runat="server" Display="dynamic" CssClass="text-danger"
                            ControlToValidate="txt_swsDocument_cd" 
                            ValidationExpression="^([\S\s]{0,20})$" 
                            ErrorMessage="Nhập mã tiêu chuẩn không quá 20 kí tự">
                    </asp:RegularExpressionValidator>
                </td>

            </tr>
         
            <tr>
                 <td>
                     <%Response.Write(Standard.Language.value == "vi" ?  "Phiên bản" : "Ver(*)" ); %>
                    
                </td>
                <td>
                    
                     <asp:TextBox ID="txt_ver" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_ver"
                                CssClass="text-danger" ErrorMessage="required." />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                            runat="server" Display="dynamic" CssClass="text-danger"
                            ControlToValidate="txt_ver" 
                            ValidationExpression="^([\S\s]{0,3})$" 
                            ErrorMessage="Version không được quá 3 kí tự">
                        </asp:RegularExpressionValidator>
                </td>
             
            </tr>
            <tr>
                 <td>   
                     <%Response.Write(Standard.Language.value == "vi" ? "Tên tiêu chuẩn SWS (vi)(*)" : "Standard name SWS (vi)(*)"  ); %>

                </td>
                <td>
                    
                     <asp:TextBox ID="txt_nameVi" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_nameVi"
                                CssClass="text-danger" ErrorMessage="required." />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                            runat="server" Display="dynamic" CssClass="text-danger"
                            ControlToValidate="txt_nameVi" 
                            ValidationExpression="^([\S\s]{0,500})$" 
                            ErrorMessage="Tên tiêu chuẩn không quá 500 kí tự">
                        </asp:RegularExpressionValidator>
                </td>
             
            </tr>
            <tr>
                <td> 
                    <%Response.Write(Standard.Language.value == "vi" ?  "Tên tiêu chuẩn SWS (en)" : "Standard name SWS (en)" ); %>
                     
                </td>
                <td>
                   
                    <asp:TextBox ID="txt_nameEN" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                     <%Response.Write(Standard.Language.value == "vi" ?  "Ngày nhận(*)" : "Receive date(*)" ); %>
                </td>
                 <td>
                     
                    <asp:TextBox ID="txt_Receive_dt" runat="server" ></asp:TextBox>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_Receive_dt"
                                CssClass="text-danger" ErrorMessage="required." />
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                            runat="server" Display="dynamic" CssClass="text-danger"
                            ControlToValidate="txt_Receive_dt" 
                            ValidationExpression="^([\S\s]{0,500})$" 
                            ErrorMessage="Tên tiêu chuẩn không quá 500 kí tự">
                        </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                     <%Response.Write(Standard.Language.value == "vi" ? "Ghi chú" : "Comment"  ); %>
                </td>
                <td>
                    
                    <asp:TextBox ID="txt_comment" runat="server" ></asp:TextBox>
                </td>
            </tr>
              </tbody>
    </table>

        <span ><asp:Button ID="btn_save" runat="server" Text="Lưu" Width="89px" CssClass="btn-primary" OnClick="btn_save_Click" /></span>
         <div id="alert" runat="server" ></div>
    <%--<input id="saveForm" class="button_text" type="submit" name="btn_add" value= "Thêm mới" />--%>
    <div id="attach_content" runat="server" style="margin-top:10px; display:none" >
        <table style="width:100%;">
            <tr>
                <td style="width:50%; vertical-align:top">
                    <div style="padding:10px;">
                        <%Response.Write(Standard.Language.value == "vi" ? "File tài liệu" : "file document"  ); %>
                       
                        <br />
                        <div style="padding:10px;" >
                              <span style="clear:both"></span>
                                  <div style="float:left">
                                       <asp:FileUpload ID="FileUpload1"  runat="server" />
                                  </div>
                                  <div style="" >
                                      <asp:Button ID="btn_upload" runat="server" Text="Upload" OnClick="btn_upload_Click" />
                                  </div>                                   
                              <div id="alert_file" runat="server"></div>
                            
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/Delete.png" CommandName="delete" runat="server" OnClientClick="return confirm ('bạn có chắc muốn xóa?');" />
                                        </ItemTemplate>
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
                        <%Response.Write(Standard.Language.value == "vi" ? "Bộ phận liên quan" : "Related Section"  ); %>
                       
                        <div style="padding:10px;">
                            <asp:DropDownList ID="cb_Section" Width="70%" runat="server" DataSourceID="SqlDataSource1" DataTextField="dep_c" DataValueField="dep_c">
                               
                            </asp:DropDownList>
                            
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT [dep_c], [dep_nm] FROM [Dep_mst] ORDER BY [dep_nm]"></asp:SqlDataSource>
                            
                            <asp:Button ID="btn_AddDep" runat="server" Text="Thêm" OnClick="btn_AddDep_Click" />
                            <div id="alert_Section" runat="server"></div>
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/Delete.png" CommandName="Delete" runat="server" OnClientClick="return confirm ('bạn có chắc muốn xóa?');" />
                                        </ItemTemplate>
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

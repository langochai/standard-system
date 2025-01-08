<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SVWSDocPrivate_Create.aspx.cs" Inherits="Standard.SVWSDocPrivate_Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script src="../resources/jquery.min.js"></script>
    <script src="../resources/popper.min.js"></script>
    <link href="../resources/jquery-ui.css" rel="stylesheet" />
    <script src="../resources/jquery-ui.js"></script>

      <script type="text/javascript">

          $(function () {
              $("[id$=txt_apply_dt]").datepicker({
                  showOn: 'button',
                  buttonImageOnly: true,
                  buttonImage: '../Images/calendar.png',
                  dateFormat: 'dd-M-yy' 
              });
          });
          $(function () {
              $("[id$=txt_expire_dt]").datepicker({
                  showOn: 'button',
                  buttonImageOnly: true,
                  buttonImage: '../Images/calendar.png',
                  dateFormat: 'dd-M-yy' 
              });
          });

  </script>
    
    
    <style type="text/css">
        .content_col{
            width: 15%;
            padding:5px;
        }
    </style>
    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSVWSDPCreate)"></asp:Label>
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn SVWS /" : "SVWS Standard /"  ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../../SVWSDocPrivate/SVWSDocPrivate.aspx" style="color:white">
                                <%Response.Write(Standard.Language.value == "vi" ?  "Tiêu chuẩn riêng /" : "Private Standard /" ); %>
                                    </a>
                            </li>
                            <li style="padding-top:9px;"><i ></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Thêm mới" : "Add New"  ); %>
                            </li>
                            
                                <%--<li style="float:right">
                                   
                                    <asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search"></asp:TextBox>

                                </li>--%>
                           
                        </ol>
                   
                    </div>
    </div>
    <div>
        <h4> 
            <% Response.Write(Standard.Language.value == "vi" ? "Tạo mới tài liệu" : "Create Document" ); %>
        </h4>
        <div style="font-style:italic">
            <span>
                <%Response.Write(Standard.Language.value == "vi" ?  "Những mục đánh dấu (*) là bắt buộc" : "Items (*) are required" ); %>
            </span>
            <span style="float:right">
                <asp:Button ID="btn_save" CssClass="btn btn-primary" runat="server" Text="Save document" OnClick="btn_save_Click"  >
                </asp:Button >
            </span>
        </div>
        <div>
            <table style="width:100%">
                <tr style="padding:5px;">
                    <td class="content_col" >
                        <% Response.Write(Standard.Language.value == "vi" ? "Mã số*" : "Document code*" ); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_doc_c" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_doc_c" CssClass="text-danger" ErrorMessage="required." />
                    </td>
                </tr>
                <tr>
                    <td class="content_col">
                        <% Response.Write(Standard.Language.value == "vi" ? "Ver*" : "Ver*"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ver" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_ver" CssClass="text-danger" ErrorMessage="required" />
                    </td>
                </tr>
                <tr>
                    <td class="content_col">
                        <%Response.Write(Standard.Language.value == "vi" ? "Bộ phận lập*" : "Section Make*"  ); %>
                    </td>
                    <td>
                        <asp:DropDownList ID="dr_sec_make" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDataSource2" DataValueField="dep_c" DataTextField="dep_c">
                            
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="dr_sec_make" CssClass="text-danger" ErrorMessage="required" />
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT [dep_c] FROM [Dep_mst] where dep_c=@dep_c ORDER BY [dep_c]">
                            <SelectParameters>
                            <asp:SessionParameter Name="dep_c" SessionField="dep_c" Type="String" />
                       
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <%--<tr>
                    <td class="content_col">
                        <% Response.Write(Standard.Language.value=="en" ? "Type of Document*": "Loại tài liệu*"); %>
                    </td>
                    <td>
                        <asp:DropDownList ID="dr_type_doc" runat="server" DataSourceID="SqlDataSource1" AppendDataBoundItems="true" DataValueField="doc_type_id" DataTextField="doc_type_nm" >
                            <asp:ListItem Value="">---</asp:ListItem>
                        </asp:DropDownList>
                        
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="dr_type_doc" CssClass="text-danger" ErrorMessage="required" />
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand=" select doc_type_id, doc_type_nm from Document_type_mst order by doc_type_nm"></asp:SqlDataSource>
                    </td>
                </tr>--%>
                <tr>
                    <td class="content_col">
                        <%Response.Write(Standard.Language.value=="en"? "Document name (vi)*": "Tiêu đề (vi)*"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_title_vi" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_title_vi" CssClass="text-danger" ErrorMessage="requied" />
                    </td>
                </tr>
                 <tr>
                    <td class="content_col">
                        <%Response.Write(Standard.Language.value=="en"? "Document name (en)": "Tiêu đề (en)"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_title_en" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="content_col">
                        <%Response.Write(Standard.Language.value=="en"? "Description (vi)*": "Mô tả (vi)*"); %>
                    </td>
                    <td>
                        <%--<textarea id="txt_des" runat="server" rows="6" style="width:100%"></textarea>--%> 
                        <asp:TextBox ID="txt_des_vi" runat="server" TextMode="MultiLine" Rows="6" Width="100%"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_des_vi" CssClass="text-danger" ErrorMessage="required" />
                    </td>
                </tr>
                <tr>
                    <td class="content_col">
                        <%Response.Write(Standard.Language.value=="en"? "Description (en)": "Mô tả (en)"); %>
                    </td>
                    <td>
                        <%--<textarea id="txt_des_enn" rows="6"  runat="server" style="width:100%"></textarea>--%>
                        <asp:TextBox ID="txt_des_en" runat="server" TextMode="MultiLine" Rows="6" Width="100%"></asp:TextBox>
                       
                    </td>
                </tr>
                <tr>
                    <td class="content_col">
                        <%Response.Write(Standard.Language.value=="en"? "Apply date*": "Ngày áp dụng*"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_apply_dt" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_apply_dt" CssClass="text-danger" ErrorMessage="required" />
                    </td>
                </tr>
                <tr>
                    <td class="content_col">
                        <%Response.Write(Standard.Language.value=="en"? "Expire date": "Ngày hết hạn"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_expire_dt" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txt_expire_dt" CssClass="text-danger" ErrorMessage="required" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

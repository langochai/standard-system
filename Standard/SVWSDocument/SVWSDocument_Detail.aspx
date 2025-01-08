<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SVWSDocument_Detail.aspx.cs" Inherits="Standard.SVWSDocument.SVWSDocument_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSVWSVDtl01)"></asp:Label>
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "en" ? "SVWS Standard /" : "Tiêu chuẩn SVWS /"); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../SVWSDocument/SVWSDocument.aspx" style="color:white">
                                <%Response.Write(Standard.Language.value == "en" ? "Public Standard /" : "Tiêu chuẩn chung /"); %>
                                </a>
                            </li>
                            <li style="padding-top:9px;">
                                <%Response.Write(Standard.Language.value == "en" ? "Details" : "Chi tiết"); %>
                            </li>
                            
                                <%--<li style="float:right">
                                   
                                    <asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search"></asp:TextBox>

                                </li>--%>
                           
                        </ol>
                   
                    </div>
    </div> 
    <div id="form_container">
        <h2>
            <asp:Label ID="lbl_typeofStd" runat="server" ></asp:Label>
        </h2>
        <div id="alert" runat="server" ></div>
        <table class="table table-striped table-advance table-hover">
            <tbody>
                <tr>
                    <td rowspan="6" class="td" style="width:70%">
                        <div id="divname" style="font-weight:bold">
                            <p class="p1">
                                <asp:Label ID="lbl_stdName_vi" runat="server" ></asp:Label>
                            </p>
                            <p class="p2">
                                <asp:Label ID="lbl_stdName_en" runat="server" ></asp:Label>
                            </p>
                        </div>
                    </td>
                    <td class="td2" style="width:15%">
                     
                        Mã số:
               
                    </td>
                    <td>
                        <asp:Label ID="lbl_stdCd" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                       Bộ phận lập:
                    </td>
                    <td>
                        <asp:Label ID="lbl_dep_make" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                       Phiên bản:
                    </td>
                    <td>
                        <asp:Label ID="lbl_ver" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        Ngày áp dụng:
                    </td>
                    <td>
                        <asp:Label ID="lbl_apply_dt" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        Ngày hết hạn:
                    </td>
                    <td>
                        <asp:Label ID="lbl_duedate" runat="server" ></asp:Label>
                    </td>
                </tr>
                <asp:PlaceHolder ID="confirm_list" runat="server">
                <tr>
                    <td colspan="2" style="background-color:#164F8C;">
                        <a href="<%= "SVWSDocument_ConfirmList.aspx" + Request.Url.Query%>" style="color:white;display:block">Danh sách xác nhận</a>
                    </td>
                </tr>
                </asp:PlaceHolder>
            </tbody>
        </table>
        
        <table class="table table-striped table-advance table-hover">
            <tbody>
                <tr >
                    <td>
                        <%Response.Write(Standard.Language.value == "en" ? "Section apply(*)" : "Bộ phận áp dụng(*)"); %>
                        <asp:LinkButton ID="btnSendMailSignUp" runat="server" OnClick="btnSendMailSignUp_Click" OnClientClick="return confirm('Hệ thống sẽ gửi mail thông báo đến bộ phận liên quan, bạn có muốn tiếp tục?')">
                            <span class="glyphicon glyphicon-envelope"></span>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <asp:ListView runat="server" ID="list_dep" OnItemDataBound="list_dep_ItemDataBound" >
                        <LayoutTemplate>
                            <table>
                            <tr >
                               <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>  
                            
                                <td id="Main"  runat="server">
                                    <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Bind("confirm_f") %>'></asp:Label>
                                    <asp:Label ID="lbl_dep" runat="server" Text='<%# Bind("dep_c") %>'></asp:Label>
                                </td> 
                             
                        </ItemTemplate>
                    </asp:ListView>
                        <%--<asp:Label ID="lbl_dep" runat="server" Font-Bold="True"></asp:Label>--%>
                    </td>
                </tr>
                
         
                <tr>
                    <td style="width:20%" class="td3">
                        <%Response.Write(Standard.Language.value == "en" ? "Description (vi)" : "Mô tả (vi)"); %>
                        
                    </td>
                    <td class="td4">
                        <div id="des_vi" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td class="td3">
                        <% Response.Write(Standard.Language.value == "en" ? "Description (en)" : "Mô tả (en)"); %>
                       
                    </td>
                    <td class="td4">
                       <div id="des_en" runat="server"></div>

                    </td>
                </tr>
                <tr>
                    <td>
                        
                        Link file:
                    </td>
                    <td>
                        <asp:GridView ID="GV_file" runat="server" AutoGenerateColumns="False"
                            ShowHeader="False" GridLines="None" OnRowCommand="GV_file_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("link_file") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" 
                                            CommandName="download"  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svwsDoc_file_id") %>'
                                            runat="server" Text='<%# Bind("link_file") %>'
                                            OnClientClick="localStorage.setItem('confirmRead'+((new URLSearchParams(window.location.search)).get('doc_id')), 1);"
                                            ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <asp:PlaceHolder ID="confirm_understand" runat="server">
                <tr>
                    <td>
                        <%Response.Write(Standard.Language.value == "en" ? "Confirm" : "Xác nhận");%>
                    </td>
                    <td>
                        <%Response.Write(Standard.Language.value == "en" ? "Do you understand this standard?" : "Xác nhận đã hiểu tiêu chuẩn này ");%>
                        <asp:LinkButton ID="btn_confirm" runat="server" OnClick="btn_confirm_Click">OK</asp:LinkButton>
                    </td>
                </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="confirm_activation" runat="server">
                <tr>
                    <td>
                        <%Response.Write(Standard.Language.value == "en" ? "Sign up" : "Xác nhận ký");%>
                    </td>
                    <td>
                        <asp:Label ID="lblSignUpMessage" runat="server"></asp:Label>
                        <asp:LinkButton ID="LinkButton2" runat="server" 
                            OnClick="btn_confirm_issue_Click" 
                            OnClientClick="return hasReadFiles()">OK</asp:LinkButton>
                    </td>
                </tr>
                </asp:PlaceHolder>
            </tbody>
        </table>
        <!--Xác nhận đã hiểu-->
        <div id="note" style="font-style:italic">     
               <%Response.Write(Standard.Language.value == "en" ?
                         "(*) The items marked in orange are not confirmed this Standard" :
                         "(*) Những bộ phận được đánh dấu màu cam là bộ phận chưa xác nhận tiêu chuẩn này.");
                   %>    
        </div>
       <script>
           function hasReadFiles() {
               const docID = (new URLSearchParams(window.location.search)).get('doc_id')
               const hasClickedOnFile = localStorage.getItem('confirmRead'+docID);
               if (hasClickedOnFile != 1) alert('Vui lòng đọc file tiêu chuẩn trước khi xác nhận.')
               return hasClickedOnFile == 1
           }
       </script>
    </div>
</asp:Content>

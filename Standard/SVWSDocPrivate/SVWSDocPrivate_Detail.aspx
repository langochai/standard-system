<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SVWSDocPrivate_Detail.aspx.cs" Inherits="Standard.SVWSDocPrivate.SVWSDocPrivate_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSVWSDPDetail)"></asp:Label>
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn SVWS /" : "SVWS Standard /"  ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../SVWSDocPrivate/SVWSDocPrivate.aspx" style="color:white">
                                <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn riêng /" : "Private Standard /"  ); %>
                                </a>
                            
                                    </li>
                            <li style="padding-top:9px;">
                                <%Response.Write(Standard.Language.value == "vi" ? "Chi tiết" : "Detail"  ); %>
                            </li>
                            
                                <%--<li style="float:right">
                                   
                                    <asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search"></asp:TextBox>

                                </li>--%>
                           
                        </ol>
                   
                    </div>
    </div> 
    <div id="form_container">
        <h2>
            <%--<asp:Label ID="lbl_typeofStd" runat="server" ></asp:Label>--%>
        </h2>
        <div id="alert" runat="server"></div>
        <table class="table table-striped table-advance table-hover">
            <tbody>
                <tr>
                    <td rowspan="5" class="td" style="width:70%">
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
                

            </tbody>
        </table>
        
        <table class="table table-striped table-advance table-hover">
            <tbody>
               
                
         
                <tr>
                    <td style="width:20%" class="td3">
                        <%Response.Write(Standard.Language.value == "vi" ? "Mô tả (vi)" : "Description (vi)"  ); %>
                        
                    </td>
                    <td class="td4">
                        <div id="des_vi" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td class="td3">
                        <% Response.Write(Standard.Language.value == "vi" ? "Mô tả (en)" : "Description (en)"  ); %>
                       
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
                            ShowHeader="False" GridLines="None" OnRowCommand="GV_file_RowCommand"  >
                            <Columns>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("link_file") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" CommandName="download"  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svws_prv_file_id") %>' runat="server" Text='<%# Bind("link_file") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <!--Xác nhận đã hiểu-->
       <%-- <div id="note" style="font-style:italic">     
               <%Response.Write(Standard.Language.value == "en" ?
                         "(*) The Items marked in orange are not confirmed this Standard" :
                         "(*) Những bộ phận được đánh dấu màu cam là bộ phận chưa xác nhận tiêu chuẩn này.");
                   %>    
        </div>--%>
       
    </div>
</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SVWSDocPrivate_default.aspx.cs" Inherits="Standard.SVWSDocPrivate.SVWSDocPrivate_default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <style type="text/css">
        @media (max-width:1200px) {
            .overflow{
           
              width: 100%;
              overflow-x: scroll;
        }
        }
        .overflow{
           
              width: 100%;
              overflow-x: scroll;
        }
        .tableBackground
        {
	        background-color:silver;
	        opacity:0.7;
        }
         .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
         /*#ModalPopupExtender1{
             overflow:scroll;
             width:100px;
             height:90%;
         }*/
        @media (max-width:1200px) {
            .Popup {
                overflow: scroll;
                width: 90%;
                height: 90%;
                background-color: #FFFFFF;
                padding: 10px;
                font-size: 10px;
                /*width: 700px;
            height: 400px;*/
            }
        }

         @media (min-width:1200px) {
            .Popup {
                background-color: #FFFFFF;
                padding: 10px 20px 10px 20px;
                font-size: 10px;
               
                width: 700px;
                height: 400px;
            }
            .head{
                background-color:#2E8A56;
                font-size:20px;
                font-weight:bold;
                color:white;
                text-align:center;
            }
        }

    </style>
  <%-- <script type="text/javascript">
       $(document).on("click", ".menu", function () {
           $find("mpePopUp").show();
           return false;
       })
    </script>--%>
    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSVWSDPDeflt)"></asp:Label>
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "en" ? "SVWS Standard /" : "Tiêu chuẩn SVWS /"); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../SVWSDocument/SVWSDocument/SVWSDocPrivate.aspx" style="color:white">
                                    <%Response.Write(Standard.Language.value == "en" ? "Private Standard" : "Tiêu chuẩn riêng"); %>
                                </a>
                            </li>
                            
                                <%--<li style="float:right">
                                   
                                    <asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search"></asp:TextBox>

                                </li>--%>
                           
                        </ol>
                   
                    </div>
    </div>
    <div>
    <div class="">
        <table style="width:50%">
            <tr>
                <td>
                    <%Response.Write(Standard.Language.value == "en" ? "Document code" : "Mã tiêu chuẩn"); %>
                </td>
                <td>
                    <asp:TextBox ID="txt_doc_c" runat="server"></asp:TextBox>
                </td>
                <td rowspan="2"; style="padding-left:10px">
                        
                        <asp:Button ID="btn_search" class="btn btn-primary" runat="server" Text="Search" OnClick="btn_search_Click"  />
                        
                </td>
            </tr>
            <tr>
                <td>
                    <%Response.Write(Standard.Language.value == "en" ? "Document name" : "Tên tiêu chuẩn"); %>
                </td>
                <td>
                    <asp:TextBox ID="txt_doc_nm" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <%Response.Write(Standard.Language.value == "en" ? "Section" : "Bộ phận"); %>
                </td>
                <td>
                    <asp:DropDownList ID="dr_dep" runat="server" DataSourceID="SqlDataSource1" DataTextField="dep_c" DataValueField="dep_c">
                        
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT [dep_c] FROM [Dep_mst] where dep_c=@dep_c ORDER BY [dep_c]">
                        <SelectParameters>
                        <asp:SessionParameter Name="dep_c" SessionField="dep_c" Type="String" />
                       
                    </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td style="padding-left:10px">
                        <asp:Button ID="btn_export" CssClass="btn btn-primary" runat="server" Text="Export" Visible="false" />
                    </td>
            </tr>
           <%-- <tr>
                <td>
                    <%Response.Write(Standard.Language.value == "en" ? "Type of Document" : "Loại tài liệu:"); %>
                </td>
                <td>
                    <asp:DropDownList ID="dr_type_doc" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDataSource2" DataTextField="doc_type_nm" DataValueField="doc_type_id">
                        <asp:ListItem Value="">All</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT [doc_type_id], [doc_type_nm] FROM [Document_type_mst]"></asp:SqlDataSource>
                </td>
            </tr>--%>
        </table>
    </div>
        <div>
             <div id="alert" runat="server" style="padding:10px;"></div>

            
            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                GridLines="None" DataKeyNames="svws_prv_id" 
                 OnRowCommand="GridView1_RowCommand"  >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <asp:BoundField DataField="stt" HeaderText="STT" ItemStyle-Width="5px" />
                    <asp:TemplateField HeaderText="Mã TC">
                       
                        <ItemTemplate>
                            <%--<a href="#" ><span class="glyphicon glyphicon-envelope"></span></a>--%>
                            
                             <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("svws_prv_c") %>' 
                                                CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svws_prv_id") %>' ></asp:LinkButton>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="svws_prv_ver" HeaderText="Ver" />
                    
                    <asp:TemplateField HeaderText="Tiêu đề (vi)" ItemStyle-Width="25%" >
                        
                        <ItemTemplate>

                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("svws_prv_nm_vi") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tiêu đề (en)" ItemStyle-Width="25%" >
                        
                        <ItemTemplate>

                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("svws_prv_nm_eng") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="make_dep_c" HeaderText="Bộ phận lập" />
                    <asp:BoundField DataField="apply_dt" HeaderText="Ngày áp dụng" />
                  
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


  <script type="text/javascript">

      $(function () {
          $("[id$=TextBox1]").datepicker({
              showOn: 'button',
              buttonImageOnly: true,
              buttonImage: 'Images/calendar.png',
              dateFormat: 'dd-M-yy' 
          });
      });

  </script>
</asp:Content>

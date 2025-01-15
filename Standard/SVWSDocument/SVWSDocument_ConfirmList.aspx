<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" EnableViewState="true" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="SVWSDocument_ConfirmList.aspx.cs" Inherits="Standard.SVWSDocument.SVWSDocument_ConfirmList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
        @media (max-width:1200px) {
            .Popup {
                overflow: scroll;
                width: 90%;
                height: 90%;
                background-color: #FFFFFF;
                padding: 10px;
                font-size: 10px;
            }
        }

         @media (min-width:1200px) {
            .Popup {
                background-color: #FFFFFF;
                padding: 10px 20px 10px 20px;
                font-size: 10px;
               
                width: 700px;
                height: 450px;
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

    <div class="row" >
        <div class="col-lg-12" >
            <asp:Label ID="lbl_class" runat="server" Text="(STDSVWSVD01)"></asp:Label>
            <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                <li style="padding-top:9px;"><i class="fa fa-home"></i>
                    <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn SVWS /" : "SVWS Standard /"  ); %>

                </li>
                <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                    <a href="../SVWSDocument/SVWSDocument.aspx" style="color:white">
                        <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn chung/" : "Public Standard/"  ); %>
                    </a>
                </li>
                <li style="padding-top:9px;">
                    <a href="<%= "SVWSDocument_Detail.aspx" + Request.Url.Query%>" style="color:white">
                        <%Response.Write(Standard.Language.value == "en" ? "Details/" : "Chi tiết/"); %>
                    </a>
                </li>
                <li style="padding-top:9px;">
                    <a style="color:white">
                    <%Response.Write(Standard.Language.value == "vi" ? "Danh sách xác nhận" : "Confirmation list"  ); %>
                    </a>
                </li>
            </ol>
        </div>
    </div>
    <div>
        <div>
            <asp:Button runat="server" ID="btnExport" BackColor="#2E8A56" ForeColor="White" OnClick="btnExport_Click"></asp:Button>
             <div id="alert" runat="server" style="padding-top:10px"></div>
            <asp:GridView ID="GridView1" Width="70%" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                GridLines="Horizontal" DataKeyNames="user_id" BorderColor="#DDDDDD"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                 >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <asp:BoundField DataField="STT" HeaderText="STT" ItemStyle-Width="5px" />
                    <asp:TemplateField HeaderText="user_id" >
                        <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("user_id") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="fullname" HeaderText="Họ tên"  />
                    <asp:BoundField DataField="department" HeaderText="Bộ phận"  />
                    <asp:BoundField DataField="cofirm_date" HeaderText="Ngày ký"  />
                    <asp:TemplateField HeaderText="Xác nhận ký" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label ID="lblConfirmed" runat="server" 
                                Text='<%# Eval("cofirmation") == DBNull.Value ? "" : 
                                    Convert.ToBoolean(Eval("cofirmation"))? "☑ Đồng ý" : "☒ Từ chối" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="note" HeaderText="Ghi chú"  />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#c9c9c9" Font-Bold="True" ForeColor="White" />
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
</asp:Content>

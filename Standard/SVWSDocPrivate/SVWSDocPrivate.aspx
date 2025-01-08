<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SVWSDocPrivate.aspx.cs" Inherits="Standard.SVWSDocument.SVWSDocPrivate.SVWSDocPrivate" %>
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
     

  <%-- <script type="text/javascript">
       $(document).on("click", ".menu", function () {
           $find("mpePopUp").show();
           return false;
       })
    </script>--%>
    <div class="row" >
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSVWSVD01)"></asp:Label>
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ?  "Tiêu chuẩn SVWS /" : "SVWS Standard /" ); %>

                            </li>
                           
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="../SVWSDocPrivate/SVWSDocPrivate.aspx" style="color:white">
                                    <%Response.Write(Standard.Language.value == "vi" ? "Tiêu chuẩn riêng" : "Private Standard" ); %>
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
                    <%Response.Write(Standard.Language.value == "vi" ? "Mã tiêu chuẩn" : "Document code" ); %>
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
                    <%Response.Write(Standard.Language.value == "vi" ? "Tên tiêu chuẩn" : "Document name" ); %>
                </td>
                <td>
                    <asp:TextBox ID="txt_doc_nm" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <%Response.Write(Standard.Language.value == "vi" ? "Bộ phận" : "Section" ); %>
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
            <div style="float:right">
                    <a href="../SVWSDocPrivate/SVWSDocPrivate_Create.aspx">
                        <i class="icon_plus_alt2"></i>
                        <%Response.Write(Standard.Language.value == "vi" ? "Thêm mới" : "New"  ); %>
                            
                    </a>

                </div>


           <%-- <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
           <a href="mailto:?subject=this is the subject&body=this is the body">sendEmail</a>--%>
            
            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                GridLines="Horizontal" DataKeyNames="svws_prv_id" 
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" BorderColor="#DDDDDD" AllowPaging="True"
                PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <asp:BoundField DataField="stt" HeaderText="STT" ItemStyle-Width="5px" />
                    <asp:TemplateField HeaderText="Mã TC">
                        
                        <ItemTemplate>
                            
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="sendEmail" ToolTip="Kích hoạt & Gửi email" OnClientClick="return confirm ('Bạn có chắc sẽ kích hoạt tiêu chuẩn này?');" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svws_prv_id") %>'  ><span class="glyphicon glyphicon-envelope"></span></asp:LinkButton>
                            
                             <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("svws_prv_c") %>' 
                                                CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svws_prv_id") %>' ></asp:LinkButton>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="svws_prv_ver" HeaderText="Ver" />
                    
                    <asp:TemplateField HeaderText="Tiêu đề (vi)" ItemStyle-Width="30%" >
                        
                        <ItemTemplate>

                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("svws_prv_nm_vi") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tiêu đề (en)" Visible="false" ItemStyle-Width="25%" >
                        
                        <ItemTemplate>

                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("svws_prv_nm_eng") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="make_dep_c" HeaderText="Bộ phận lập" />
                    <asp:BoundField DataField="apply_dt" HeaderText="Ngày áp dụng" />
                    <asp:BoundField DataField="active_f" HeaderText="Kích hoạt" />
                    <asp:TemplateField ItemStyle-Width="150px">
                        <ItemTemplate >
                            <div class="btn-group" style="float:right;">
                                
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="add_link" CssClass="btn btn-primary" ToolTip="File" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svws_prv_id") %>'><i class="fa fa-folder"></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="edit" CssClass="btn btn-warning" ToolTip="Sửa" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svws_prv_id") %>'><i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="delete" OnClientClick="return confirm ('bạn có chắc muốn xóa?');" CssClass="btn btn-danger" ToolTip="Xóa" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svws_prv_id") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                <%--<a class="btn btn-primary" href="#"><i class="icon_plus_alt2" ></i></a>
                                <a class="btn btn-success" href="#"><i class="fa fa-folder"></i></a>
                                <a class="btn btn-danger" href="#"><i class="fa fa-trash-o"></i></a>--%>
                            </div>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
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
        <div>
           <asp:ScriptManager ID="asm" runat="server" />
            <asp:Button ID="btnOpenPopUp" runat="server" text="Open PopUp" style="display:none; " />
           <%--<asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>--%>
           
           

            <ajaxToolkit:ModalPopupExtender ID="filePopUp" runat="server" 
                TargetControlID="btnOpenPopUp" PopupControlID="popup_file" 
                CancelControlID="btn_closeFile"
                BackgroundCssClass="tableBackground">

            </ajaxToolkit:ModalPopupExtender>
            <div id="popup_file" class="Popup" style="display:none">
                
                <div class="head">
                    File tài liệu
                </div>
                <div style="font-style:italic; font-size:small">*Chỉ được phép upload tài liệu dưới 10M</div>
                
                     <div style="padding:10px; font-size:15px;" >
                              <span style="clear:both"></span>
                                  <div style="float:left">
                                       <asp:FileUpload ID="FileUpload1" Width="300px"  runat="server" />
                                  </div>
                                  <div style="" >
                                      <asp:Button ID="btn_upload" runat="server" Text="Thêm" OnClientClick="return ProgressBar()" OnClick="btn_upload_Click" />
                                  </div> 
                                 <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="FileUpload1"

                                 ErrorMessage="File size should not be greater than 10 MB." OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>--%>
                         <div id="divUpload" runat="server" style="display:none ">
                            <div style="width: 300pt; text-align: center;">
                                Uploading...</div>
                            <div style="width: 300pt; height: 20px; border: solid 1pt gray">
                                <div id="divProgress" runat="server" style="width: 1pt; height: 20px; background-color: orange;
                                    display: none">
                                </div>
                            </div>
                            <div style="width: 300pt; text-align: center;">
                                <asp:Label ID="lblPercentage" runat="server" Text="Label"></asp:Label></div>
                            <br />
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </div>
                              <div id="alert_file" runat="server"></div>
                                <asp:Label ID="lbl_doc_id_file" runat="server" Visible="false" ></asp:Label>
                            <div style="height:280px">  
                            <div style="max-height:280px; overflow-y:scroll; padding-top:10px;">
                            <%--<asp:LinkButton ID="LinkButton1" runat="server">Upload</asp:LinkButton>--%>
                            <asp:GridView ID="GV_File" runat="server" AutoGenerateColumns="False" style="width:100%;" CellPadding="4" ForeColor="#333333" 
                                GridLines="None" ShowHeaderWhenEmpty="True" DataKeyNames="svws_prv_file_id"
                                 OnRowDeleting="GV_File_RowDeleting" OnRowCommand="GV_File_RowCommand"  >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="File name">
                                        <ItemTemplate>
                                            
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("link_file") %>' 
                                                CommandName="download" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"svws_prv_file_id") %>' ></asp:LinkButton>
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
                      
                 </div>
                <div style="vertical-align:bottom; text-align:center" >
                    <asp:Button id="btn_closeFile" runat="server" text="Đóng" CssClass="btn btn-close"  />

                </div>
                 
                      
                      
                      
                
                      
                 
            </div>
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

    <script  type="text/javascript">
        var size = 2;
        var id = 0;

        function ProgressBar() {
            if (document.getElementById('<%=FileUpload1.ClientID %>').value != "") {
                var uploadControl = document.getElementById('<%= FileUpload1.ClientID %>');
                if (uploadControl.files[0].size > 10485760) {
                    alert("File không được quá 10MB");
                    return false;
                }
                else {
                    
                    document.getElementById('<%=divProgress.ClientID %>').style.display = "block";
                    document.getElementById('<%=divUpload.ClientID %>').style.display = "block";
                    id = setInterval("progress()", 20);
                    return true;
                }

               
            }
            else {
                alert("Select a file to upload");
                return false;
            }

        }

        function progress() {
            size = size + 1;
            if (size > 299) {
                clearTimeout(id);
            }
            document.getElementById('<%=divProgress.ClientID %>').style.width = size + "pt";
            document.getElementById("<%=lblPercentage.ClientID %>").firstChild.data = parseInt(size / 3) + "%";
        }

    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SWSDocument.aspx.cs" Inherits="Standard.SWSDocument.SWSDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">

  <meta name="viewport" content="width=device-width, initial-scale=1">

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


    <div id="alert_permit" runat="server" style="display:none; color:red; font-style:italic"></div>
    <div id="content" style="display:" runat="server">
    <div  class="row"   >
        
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDSWSMD01)"></asp:Label>
                        
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "vi" ?  "Tiêu chuẩn SWS /" : "SWS Standard /" ); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <a href="/SWSDocument/SWSDocument.aspx" style="color:white">
                                     <%Response.Write(Standard.Language.value == "vi" ? "Quản lý tiêu chuẩn" : "Manage Document" ); %>
                                </a>
                            </li>
                            
                                <li style="float:right">      
                                    <asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search" OnTextChanged="txt_search_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>
                           
                        </ol>
                   
                    </div>
    
    </div>
    <div class="row">
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading">
                Advanced Table
                <div style="float:right">
                    <a href="SWSDocument_Create.aspx">
                        <%Response.Write(Standard.Language.value == "vi" ? "Thêm" : "New"  ); %>
                    </a>

                </div>
            </header>
            <div id="alert" runat="server" ></div>
            <asp:GridView ID="GV_SWSDocument" Width="100%" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
                AutoGenerateColumns="False" DataKeyNames="sws_doc_id" OnRowCommand="GV_SWSDocument_RowCommand"
                 OnRowDataBound="GV_SWSDocument_RowDataBound" OnRowDeleting="GV_SWSDocument_RowDeleting" AllowPaging="True"
                PageSize="15" OnPageIndexChanging="GV_SWSDocument_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775"
                     />
                <Columns>
                     <asp:BoundField DataField="stt" HeaderText="STT" ItemStyle-Width="5px" />
                    <asp:TemplateField HeaderText="Mã TC SWS" ItemStyle-Width="150px">
                      
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="sendEmail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"sws_doc_id") %>' OnClientClick="return confirm ('Hệ thống sẽ gửi mail thông báo đến bộ phận liên quan, bạn có muốn tiếp tục?');" ><span class="glyphicon glyphicon-envelope"></span></asp:LinkButton>
                            <%--<asp:LinkButton ID="LinkButton6" runat="server" CommandName="sendEmail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"sws_doc_id") %>' OnClientClick="return confirm ('Hệ thống sẽ gửi mail thông báo đến bộ phận liên quan, bạn có muốn tiếp tục?');" ><span class="glyphicon glyphicon-envelope"></span></asp:LinkButton>--%>
                           <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("sws_standard_c") %>' 
                                                CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"sws_doc_id") %>' ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="sws_standard_ver" HeaderText="Ver" ItemStyle-Width="10px" />
                    <asp:BoundField DataField="sws_standard_nm_vi"  ItemStyle-Width="250px" HeaderText="Tên tiêu chuẩn" />
                    <asp:BoundField DataField="receive_dt" HeaderText="Ngày nhận" />
                    <asp:BoundField DataField="active_f" HeaderText="Kích hoạt" />
                    <asp:BoundField DataField="dep_c" HeaderText="Bộ phận" />
                    <asp:TemplateField ItemStyle-Width="100px" >
                        <ItemTemplate>
                            
                             <asp:LinkButton ID="lnk_progress" runat="server" Text='Xem tiến độ' 
                                                CommandName="view_progress" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"sws_doc_id") %>' ></asp:LinkButton>
                          
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="180px">
                        <ItemTemplate >
                            <span class="btn-group" style="float:right">
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="add_dep" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"sws_doc_id") %>' CssClass="btn btn-primary" ToolTip="Bộ phận liên quan"><i class="icon_plus_alt2" ></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="add_link" CssClass="btn btn-primary" ToolTip="File" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"sws_doc_id") %>'><i class="fa fa-folder"></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="edit" CssClass="btn btn-warning" ToolTip="Sửa" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"sws_doc_id") %>'><i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="delete" OnClientClick="return confirm ('bạn có chắc muốn xóa?');" CssClass="btn btn-danger" ToolTip="Xóa" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"sws_doc_id") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                   
                            </span>
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
           
        </section>
         <div style="height:20px; font-style:italic;"><asp:Label ID="lblTongsodongGV1" runat="server"></asp:Label></div>
    </div>
        <div>
           <asp:ScriptManager ID="asm" runat="server" />
            <asp:Button ID="btnOpenPopUp" runat="server" text="Open PopUp" style="display:none; " />
           <%--<asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>--%>
            <ajaxToolkit:ModalPopupExtender ID="mpePopUp" runat="server" 
                TargetControlID="btnOpenPopUp" PopupControlID="popup_dep" 
                CancelControlID="btnCancel"
                BackgroundCssClass="tableBackground">

            </ajaxToolkit:ModalPopupExtender>
            <div id="popup_dep" class="Popup" style="display:none" >
                
                <div class="head">
                    Bộ phận liên quan
                </div>
                
                 <div id="main" >
                     <table style="width:100%">
                         <tr>
                             <td style="vertical-align:top; width:60%">
                                 <div>
                                     <h4>Chọn bộ phận</h4>
                                     <div style="height:250px">
                                     <asp:checkboxlist id="chk_dep" Width="100%" runat="server" RepeatColumns="5"  DataTextField="dep_c" DataValueField="dep_c"/>
                                     
                
                                     <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:StandardConnectionString %>" SelectCommand="SELECT [dep_c] FROM [Dep_mst]"></asp:SqlDataSource>
                                     <asp:Label ID="lbl_doc_id" runat="server" Visible="false"></asp:Label>
                                     </div>
                                         <div id="DivbtnOK" ><asp:Button id="btnOk" runat="server" text="Áp dụng" CssClass="btn btn-primary" OnClick="btnOk_Click" /></div>
                                 </div>

                             </td>
                             <td style="vertical-align:top; width:40%">
                                 
                                     <h4>Bộ phận đã chọn</h4>
                                 <div style="max-height:280px; overflow-y:scroll">
                                     <asp:GridView ID="GV_dep"  runat="server" Width="100%" Font-Size="12px" 
                                         AutoGenerateColumns="False" DataKeyNames="swsDocument_relate_id" 
                                         CellPadding="4" ForeColor="#333333" GridLines="None"
                                          OnRowDeleting="GV_dep_RowDeleting" >
                                         <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
                                         <Columns>
                                            
                                             <asp:BoundField DataField="dep_c" HeaderText="Bộ phận" SortExpression="dep_c" />
                                             <asp:TemplateField>
                                                 
                                                <ItemTemplate>
                                                    <div style="float:right">
                                                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/Delete.png" CommandName="delete" runat="server" OnClientClick="return confirm ('bạn có chắc muốn xóa?');" Width="15" Height="15" />
                                                    </div>
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
                             </td>
                         </tr>
                     </table>
                 </div>
                <div id="Divbtncancel" style="vertical-align:bottom; text-align:center" >
                    <asp:Button id="btnCancel" runat="server" text="Đóng" CssClass="btn btn-close"   />

                </div>
                 
                      
                      
                      
                
                      
                 
            </div>

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
                                       <asp:FileUpload ID="FileUpload1" Width="300px"  runat="server"  />
                                  </div>
                                  <div style="" >
                                      <asp:Button ID="btn_upload" runat="server" Text="Thêm" OnClick="btn_upload_Click" OnClientClick="return ProgressBar()"  />
                                  </div>

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
                                GridLines="None" ShowHeaderWhenEmpty="True" DataKeyNames="swsDoc_file_id"
                                 OnRowDeleting="GV_File_RowDeleting" OnRowCommand="GV_File_RowCommand"  >
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
                      
                 </div>
                <div style="vertical-align:bottom; text-align:center" >
                    <asp:Button id="btn_closeFile" runat="server" text="Đóng" CssClass="btn btn-close"  />

                </div>
                 
                      
                      
                      
                
                      
                 
            </div>
        </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Standard.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="form_container">

    <h1><a>Lập tài liệu</a></h1>

   
        <div class="form_description">
            <h2>Lập tài liệu mới</h2>
            <i>Những mục đánh dấu (*) là bắt buộc</i>
        </div>

    <table class="table table-striped table-advance table-hover" style="width:100%">
        <tbody>
            <tr>
                <td>
                     <div class="one-row">
                        <label class="description" for="element_11">Mã số quản lý(*)</label>
                        <div>             
              
                            <div>
                    
                            </div>

                            <input id="element_11" name="element_11" class="element text medium" type="text" maxlength="255" value="" placeholder="Nhập mã số quản lý" />
                        </div>
                    </div>
                </td>
                <td rowspan="4">
                    <div class="one-row">
                        <label class="description" for="element_12">Chi tiết (VI)(*)</label>
                        <div>
               
                            <div>
                                <textarea  id="TextArea1" cols="40" rows="10"></textarea>
                            </div>

                        </div>
                    </div>

                </td>
                
                </tr>
               <tr>
                   <td>
                    <div class="one-row">
                        <label class="description" for="element_12">Phiên bản(*)</label>
                        <div>
               
                            <div>
                                <asp:DropDownList ID="dr_version" runat="server"></asp:DropDownList>
                            </div>

                        </div>
                    </div>

                </td>
                   <td></td>
                    

                </tr>
            <tr>
                <td>
                    <div class="one-row">
                        <label class="description" for="element_14">Loại tiêu chuẩn(*)</label>
                        <div>
                
                            <div>
                                <asp:DropDownList ID="dr_type" runat="server"></asp:DropDownList>
                    
                            </div>
                        </div>
                    </div>

                </td>
                <td>
                    

                </td>

               </tr>
            <tr>
                 <td>
                    <div class="one-row">
                        <label class="description" for="element_15">Tên tiêu chuẩn(vi)(*)</label>
                        <div>
                
                            <div>
                                <asp:TextBox ID="txt_nameVi" runat="server"></asp:TextBox>

                            </div>

                        </div>
                    </div>

                </td>
                <td>
                    

                </td>
            </tr>
            <tr>
                <td>
                    <div class="one-row">
                        <label class="description" for="element_16">Tên tiêu chuẩn(en) </label>
                        <div>
                            <asp:TextBox ID="txt_nameEN" runat="server"></asp:TextBox>
                        </div>
                    </div>

                </td>
                <td>
                </td>    
            </tr>
              </tbody>
    </table>

                

                
               
  

        <div class="one-row">
            <label class="description" for="element_19">Kích hoạt </label>
            <span>

            </span>
        </div>
        <div class="one-row">
            <label class="description" for="element_4">Ngày áp dụng(*)</label>
            <div>
                
                <div>
                    <%--@Html.ValidationMessageFor(model => model.ApplyDate, null, new { @class = "text-danger" })--%>
                </div>

            </div>

        </div>
        <div class="one-row">
            <label class="description" for="element_5">Ngày hết hạn </label>
            <div>

                <%--@Html.TextBoxFor(model => model.FinishDate, null, new { @class = "element text medium" })--%>
                <div>
                    <%--@Html.ValidationMessageFor(model => model.FinishDate, null, new { @class = "text-danger" })--%>
                </div>

            </div>
        </div>
            
      

       
        
        <div class="one-row">
            <div class="buttons">
                <input type="hidden" name="form_id" value="1142418" />
                <input id="saveForm" class="button_text" type="submit" name="submit" value="Thêm mới" />
            </div>
        </div>
    
   
     <script type="text/javascript">
        $(function () {
            $("#ApplyDate").datepicker({ dateFormat: 'yy-mm-dd' }).val();
            $("#FinishDate").datepicker({ dateFormat: 'yy-mm-dd' }).val();
        });
    </script>

    <div id="footer-content">

    </div>
</div>
</asp:Content>

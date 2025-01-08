<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="Standard.ChangePass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <div id="alert_permit" runat="server" style="display:none; color:red; font-style:italic"></div>
    <div id="content" style="display:" runat="server">
    <div  class="row"   >
        
                    <div class="col-lg-12" >
                        <asp:Label ID="lbl_class" runat="server" Text="(STDCHANPASS)"></asp:Label>
                        
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "en" ? "Change password /" : "Thay đổi mật khẩu"); %>

                            </li>
                        </ol>
                    </div>
    
    </div>
        <div>
            <table>
                <tr>
                    <td>
                        <% Response.Write(Standard.Language.value == "en" ? "User" : "Tên đăng nhập"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_user" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_user"
                                CssClass="text-danger" ErrorMessage="required." />
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <%Response.Write(Standard.Language.value == "en" ? "Current Password" : "Mật khẩu hiện tại"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_curpass" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_curpass"
                                CssClass="text-danger" ErrorMessage="required." />
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <%Response.Write(Standard.Language.value == "en" ? "New Password" : "Mật khẩu mới"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_newpass" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_curpass"
                                CssClass="text-danger" ErrorMessage="required." />
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <%Response.Write(Standard.Language.value == "en" ? "Confirm Password" : "Xác nhận lại mât khẩu"); %>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_confrmPass" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                           ErrorMessage="Mật khẩu xác nhận không khớp." CssClass="text-danger"
                            ControlToCompare="txt_newpass"
                             ControlToValidate="txt_confrmPass">
   
                        </asp:CompareValidator>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Button ID="btn_ok" runat="server" Text="OK" OnClick="btn_ok_Click" />
            <asp:Button ID="btn_reset" runat="server" Text="Reset" />
            <div id="alert" runat="server" ></div>
            
        </div>
    </div>
</asp:Content>

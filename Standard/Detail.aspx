<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Standard.Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="form_container">
    <h2>Loại tiêu chuẩn</h2>
    <table class="table table-striped table-advance table-hover">
        <tbody>
            <tr>
                <td rowspan="5" class="td1">
                    <div id="divname">
                        <p class="p1">
                           Tên tiêu chuẩn
                        </p>
                        <p class="p2">
                           Tên tiêu chuẩn EN
                        </p>
                    </div>
                </td>
                <td class="td2">
                     
                    Mã số:
               
                </td>
            </tr>
            <tr>
                <td class="td2">
                   Bộ phận lập:
              </td>
            </tr>
            <tr>
                <td class="td2">
                   Phiên bản:
                </td>
            </tr>
            <tr>
                <td class="td2">
                    Ngày áp dụng:
              </td>
            </tr>
            <tr>
                <td class="td2">
                    Ngày hết hạn:
             </td>
            </tr>

        </tbody>
    </table>
    <div id="ApplySection" class="left_panel">
        @Html.Partial("_ViewApplySection", Model.lstread)
    </div>
    <table class="table table-striped table-advance table-hover">
        <tbody>
         
            <tr>
                <td class="td3">
                    Mô tả (Tiếng Việt)
                </td>
                <td class="td4">@Html.Raw(document.Detail)</td>
            </tr>
            <tr>
                <td class="td3">
                    Mô tả (Tiếng Anh)
                </td>
                <td class="td4">@Html.Raw(document.Detail_en)</td>
            </tr>

        </tbody>
    </table>
    <!--Thêm thông tin về đường dẫn-->
    <div id="resultLinkDownload">     
            @Html.Partial("_ViewLinkDownload", Model.lstlink)       
    </div>
    <div id="resultAccept">
         @Html.Partial("_ViewAccept", Model.lstAccept)
    </div>
</div>
</asp:Content>

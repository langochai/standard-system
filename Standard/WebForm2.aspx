<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Standard.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        html
        {
            background-color: Gray;
            font: 14px Georgia,Serif,Garamond;
        }
        h1
        {
            color: Green;
            font-size: 18px;
            border-bottom: Solid 1px orange;
        }
        .clear
        {
            clear: both;
        }
        .lbl
        {
            color: green;
            font-weight: bold;
        }
        .upperColumn
        {
            margin: auto;
            width: 600px;
            border-bottom: Solid 1px orange;
            background-color: white;
            padding: 10px;
        }
        .bottomColumn
        {
            margin: auto;
            width: 600px;
            background-color: white;
            padding: 10px;
        }
    </style>
   

    <script  type="text/javascript">
        var size = 2;
        var id = 0;

        function ProgressBar() {
            if (document.getElementById('<%=ImageFile.ClientID %>').value != "") {
                document.getElementById('<%=divProgress.ClientID %>').style.display = "block";
                document.getElementById('<%=divUpload.ClientID %>').style.display = "block";
                id = setInterval("progress()", 20);
                return true;
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


     <div class="upperColumn">
        <h1>
            File Upload Example</h1>
        <asp:Label ID="lblImageFile" Text="Load Image" AssociatedControlID="ImageFile" runat="server"
            CssClass="lbl" />
        <asp:FileUpload ID="ImageFile" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnAddImage" runat="server" Text="Add Image" OnClientClick="return ProgressBar()"
            OnClick="btnAddImage_Click" />
        <asp:Button ID="btnShowImage" Text="Show Image" runat="server" OnClick="btnShowImage_Click" />
        <div id="divUpload" runat="server" style="display: none">
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
    </div>
    <br class="clear" />
    <div class="bottomColumn">
        <asp:DataList ID="dlImageList" RepeatColumns="3" runat="server">
            <ItemTemplate>
                <asp:Image ID="imgShow" ImageUrl='<%# Eval("Name","~/UploadImages/{0}")%>' Style="width: 200px"
                    runat="server" AlternateText='<%# Eval("Name") %>' />
                <br />
                <%# Eval("Name") %>
            </ItemTemplate>
        </asp:DataList>
    </div>

</asp:Content>

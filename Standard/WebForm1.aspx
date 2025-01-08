<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Standard.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <%--<script src="resources/jquery.min.js"></script>--%>
    <%--<script src="resources/popper.min.js"></script>--%>
    <%--<script src="resources/bootstrap.min.js"></script>
    <link href="resources/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="resources/jquery-ui.css" rel="stylesheet" />
    <%--<script src="resources/jquery-1.12.4.js"></script>--%>
    <%--<script src="resources/jquery-ui.js"></script>--%>


    

  <script type="text/javascript">

      $(function () {
          //$("[id$=TextBox1]").datepicker({
          //    showOn: 'button',
          //    buttonImageOnly: true,
          //    buttonImage: 'Images/calendar.png'
          //});
          $("#datepicker").datepicker();
      });

  </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" id="datepicker" runat="server">
            <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>

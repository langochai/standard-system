<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Standard.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<meta name="viewport" content="width=1,initial-scale=1,user-scalable=1" />
	<link rel="stylesheet" type="text/css" href="Assets/assets_login/bootstrap/css/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="Assets/assets_login/css/styles.css" />
</head>
<body>
    <section class="container">
			<section class="login-form">
				<div style="float:right; padding-right:10px;">
						<asp:Image ID="iben" runat="server" ImageUrl="~/Images/1396342291_United Kingdom(Great Britain).png"
                            title="English" onclick='setCookie("lang","en",365)' />
                        <asp:Image ID="ibvi" runat="server" ImageUrl="~/Images/1396342371_Vietnam-Flag.png" title="Vietnamese" onclick='setCookie("lang","vi",365)' />
					</div>
				<form method="post" runat="server"  role="login">
					
					<h1>
                        <%Response.Write(Standard.Language.value == "vi" ?  "Hệ thống quản lý tiêu chuẩn" : "Standard Management System"  ); %>
                        

					</h1>
					<p>
                        <%Response.Write(Standard.Language.value == "vi" ? "Nhập User và mật khẩu để đăng nhập vào hệ thống." : "Input User & password to start using system." ); %>
                        

					</p>
					<div id="alert" runat="server" style="font-style:italic; color:red"></div>
					<div class="form-group">
	    				<div class="input-group">
		      				<div class="input-group-addon"><span class="text-primary glyphicon glyphicon-envelope"></span></div>
                            <asp:TextBox ID="TextBox1" runat="server" placeholder="User"  class="form-control"></asp:TextBox>
							<%--<input type="email" name="email" placeholder="Email address" required class="form-control" />--%>
						</div>
					</div>
					<div class="form-group">
	    				<div class="input-group">
		      				<div class="input-group-addon"><span class="text-primary glyphicon glyphicon-lock"></span></div>
                            <asp:TextBox ID="TextBox2" runat="server" placeholder="Password" TextMode="Password" class="form-control"></asp:TextBox>
							<%--<input type="password" name="password" placeholder="Password" required class="form-control" />--%>
						</div>
					</div>
					
					<div style="padding-top:30px;">
                        <asp:Button ID="Button1"  CssClass="btn btn-block btn-success" runat="server" Text="Đăng nhập" OnClick="Button1_Click"  />
					<%--<button type="submit" name="go" class="btn btn-block btn-success">Đăng nhập</button>--%>
					</div>
					
				</form>
			</section>
	</section>
</body>
	 <script type="text/javascript">

        // get set language
        function activeMenu(MenuID) {
            if (document.getElementById) {
                var menu = document.getElementById(MenuID)
                menu.className = 'active';
            }
        }
        function setCookie(cname, value, exdays) {
                var exdate = new Date();
                exdate.setDate(exdate.getDate() + exdays);
                var cvalue = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = cname + "=" + cvalue;
              window.location.href = window.location.href.replace(/#.*$/, '');
        //    location.reload(false);
         //   window.history.forward(1);
            
            //window.location.reload();
        }

        function getCookie(cname) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == cname) {
                    return unescape(y);
                }
            }
         }

    </script>
    <%--<script src="/Assets/Admin/js/jquery.js"></script>--%>
    <script src="/Assets/Admin/js/bootstrap.min.js"></script>

    <script src="/Scripts/jquery.unobtrusive-ajax.min.js"></script>
    <!-- nice scroll -->
    <script src="/Assets/Admin/js/jquery.scrollTo.min.js"></script>
    <script src="/Assets/Admin/js/jquery.nicescroll.js" type="text/javascript"></script><!--custome script for all page-->
    <script src="/Assets/Admin/js/scripts.js"></script>
	<script src="assets_login/bootstrap/js/bootstrap.min.js"></script>
</html>

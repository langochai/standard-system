﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Home2.aspx.cs" Inherits="Standard.Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <div class="row" >
                    <div class="col-lg-12" >
                        
                        <ol class="breadcrumb" style="background-color:#2E8A56; color:white; height:40px;">
                            <li style="padding-top:9px;"><i class="fa fa-home"></i>
                                <%Response.Write(Standard.Language.value == "en" ? "SWS Standard /" : "Tiêu chuẩn SWS /"); %>

                            </li>
                            <li style="padding-top:9px;"><i class="fa fa-bars"></i>
                                <%Response.Write(Standard.Language.value == "en" ? "Manage Document" : "Quản lý tiêu chuẩn"); %>
                            </li>
                            
                                <li style="float:right">
                                   
                                    <asp:TextBox ID="txt_search" runat="server" class="form-control" placeholder="Search"></asp:TextBox>

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
                    <a href="Create.aspx">Thêm</a>

                </div>
            </header>

            <table class="table table-striped table-advance table-hover">
                <tbody>
                    <tr>
                        <th><i class="icon_document"></i>
                           
                             <% Response.Write(Standard.Language.value == "en" ? "Document code" : "Mã tiêu chuẩn"); %>
                        </th>
                        <th><i class="icon_"></i> 
                            
                             <% Response.Write(Standard.Language.value == "en" ? "Version" : "Phiên bản"); %>
                        </th>
                        <th><i class="icon_"></i>
                          
                             <% Response.Write(Standard.Language.value == "en" ? "Document name" : "Tên tiêu chuẩn"); %>
                        </th>
                        <th><i class="icon_profile"></i>
                           
                             <% Response.Write(Standard.Language.value == "en" ? "Receive date" : "Ngày nhận"); %>
                        </th>
                        <th><i class="icon_"></i>
                            
                             <% Response.Write(Standard.Language.value == "en" ? "Status" : "Trạng thái"); %>
                        </th>
                        <th><i class="icon_"></i>
                           
                             <% Response.Write(Standard.Language.value == "en" ? "Related Section" : "Bộ phận"); %>
                        </th>
                    </tr>
                    <tr>
                        <td>Angeline Mcclain</td>
                        <td>2004-07-06</td>
                        <td>dale@chief.info</td>
                        <td>Rosser</td>
                        <td>176-026-5992</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                        <td>
                       
            

                    </tr>
                    <tr>
                        <td>Sung Carlson</td>
                        <td>2011-01-10</td>
                        <td>ione.gisela@high.org</td>
                        <td>Robert Lee</td>
                        <td>724-639-4784</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="Detail.aspx">Osborne</a> </td>
                        <td>2006-10-29</td>
                        <td>sol.raleigh@language.edu</td>
                        <td>York</td>
                        <td>180-456-0056</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Dalia Marquez</td>
                        <td>2011-12-15</td>
                        <td>angeline.frieda@thick.com</td>
                        <td>Alton</td>
                        <td>690-601-1922</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Selina Fitzgerald</td>
                        <td>2003-01-06</td>
                        <td>moshe.mikel@parcelpart.info</td>
                        <td>Waelder</td>
                        <td>922-810-0915</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Abraham Avery</td>
                        <td>2006-07-14</td>
                        <td>harvey.jared@pullpump.org</td>
                        <td>Harker Heights</td>
                        <td>511-175-7115</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Caren Mcdowell</td>
                        <td>2002-03-29</td>
                        <td>valeria@hookhope.org</td>
                        <td>Blackwell</td>
                        <td>970-147-5550</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Owen Bingham</td>
                        <td>2013-04-06</td>
                        <td>thomas.christopher@firstfish.info</td>
                        <td>Rule</td>
                        <td>934-118-6046</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Ahmed Dean</td>
                        <td>2008-03-19</td>
                        <td>lakesha.geri.allene@recordred.com</td>
                        <td>Darrouzett</td>
                        <td>338-081-8817</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Mario Norris</td>
                        <td>2010-02-08</td>
                        <td>mildred@hour.info</td>
                        <td>Amarillo</td>
                        <td>945-547-5302</td>
                        <td>
                            <div class="btn-group">
                                <a class="btn btn-primary" href="#"><i class="icon_plus_alt2"></i></a>
                                <a class="btn btn-success" href="#"><i class="icon_check_alt2"></i></a>
                                <a class="btn btn-danger" href="#"><i class="icon_close_alt2"></i></a>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </section>
    </div>
</div>


     


    

  <script type="text/javascript">

      $(function () {
          $("[id$=TextBox1]").datepicker({
              showOn: 'button',
              buttonImageOnly: true,
              buttonImage: 'Images/calendar.png'
          });
      });

  </script>
</asp:Content>

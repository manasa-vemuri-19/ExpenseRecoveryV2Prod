<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpenseRecoveryTesting.aspx.cs"
    Inherits="ExpenseRecoveryTesting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function confirmAction() {
            var ReturnValue = confirm("Are you sure you want to send the automated mailers? ");
            return ReturnValue;
        }
    </script>
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/app/delayedBillingGrid_1.css" rel="stylesheet" />
    <script src="Content/Scripts/jquery-1.12.4.js"></script>
    <script src="Scripts/app/Helper.js"></script>
    <script src="Scripts/app/Entities.js"></script>
    <link href="Content/css/jquery-ui.min.css" rel="stylesheet" />
    <script src="Content/Scripts/bootstrap.min.js"></script>
    <script src="Content/Scripts/GridValidations-old.js"></script>
    <script src="Scripts/jquery-ui-1.12.js" type="text/javascript"></script>
    <%--<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>
    <script src="Content/Scripts/jquery.twbsPagination.js"></script>
    <script src="Scripts/app/DB_8.js" type="text/javascript"></script>
    <link href="Content/css/animate.css" rel="stylesheet" />
    <link href="Content/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript">

        $(function () {


            var hash = document.location.hash;
            if (hash) {
                $('.nav-tabs a[href="' + hash + '"]').tab('show');

            }
            var Ageingbucket = getQueryString("Ageingbucket");
            if (Ageingbucket == "") {
                tab('Summary');
            }

        });

        function Refresh() {
            //debugger;
            window.location = "ExpenseRecovery.aspx?#menu1";
        }
        function RefreshPage() {
            // debugger;
            var PracticeLine = getQueryString("practiceline");
            var billingCategory = getQueryString("category");
            var BillingStatus = getQueryString("subcategory");
            var Ageingbucket = getQueryString("ageingbucket");

            if (Ageingbucket != "") {
                PracticeLine = getQueryString("PracticeLine");
                if (PracticeLine == "") {
                    PracticeLine = "All";
                }
                billingCategory = getQueryString("category");
                BillingStatus = getQueryString("subcategory");
                Ageingbucket = getQueryString("ageingbucket");
                window.open("ExpenseRecovery.aspx?ageingbucket=" + Ageingbucket + "&category=" + billingCategory + "&subcategory=" + BillingStatus + "&practiceline=" + PracticeLine, "_self");
            }
            else {
                window.location = "ExpenseRecovery.aspx?#menu1";
            }

        }

        function ClearFilter() {

            $('#ddlPracticeLine')[0].selectedIndex = 0;
            $('#ddlCustomerCode')[0].selectedIndex = 0;
            $('#ddlPM')[0].selectedIndex = 0;
            $('#ddlDM')[0].selectedIndex = 0;
            $('#ddlAgeingBucket')[0].selectedIndex = 0;
            $('#ddlProfitCentre')[0].selectedIndex = 0;
            $('#ddlCommitments')[0].selectedIndex = 0;
            $('#ddlLongText')[0].selectedIndex = 0;
            $('#ddlCategory_Top')[0].selectedIndex = 0;
            $('#ddlSubCategory_Top')[0].selectedIndex = 0;

            buildGridMain(2);
        }

        function UploadProgress() {

            $('.Msg').empty();
            $("#imgUpload").show();
        }

        function DownloadProgress(val) {
            if (val == "1") {
                $("#imgDownload").show();
            }
            else if (val == "2") {
                $("#imgDownload1").show();
            }
            else if (val == "3") {
                $("#imgloading2").show();
            }

        }

        function tab(value) {
            //debugger;
            if (value == "Access") {
                $("#iframe2").attr("src", "DelegateAccess.aspx");
            }
            else if (value == "Summary") {
                $("#iframe3").attr("src", "ExpenseRecoverySummary.aspx");
            }
            else if (value == "Billing") {
                var Ageingbucket = getQueryString("Ageingbucket");
                if (Ageingbucket != "") {
                    Refresh();
                }
            }
        }

        function Modal() {

            var date = $("#<%= hdDate.ClientID %>").val();

            setTimeout(function () {
                $("#iframe5").attr("src", "Upload.aspx?Date=" + date);
                $('#myModalReport').on('shown.bs.modal', function () {
                    $(this).find('.modal-dialog').css({
                        width: '50%',
                        height: 'auto',
                        'max-height': '50%'

                    });
                });
                $('#myModalReport').modal('show');
                $('#myModalReport').show();

            }, 200);

        }
        
    </script>
    <style type="text/css">
        .search
        {
            float: left;
            margin-top: 9px;
            margin-left: 5px;
        }
        
        .searchbtn
        {
            float: left;
            margin-top: 5px;
            margin-left: 5px;
        }
        
        .searchPractice
        {
            float: left;
            margin-top: 9px;
            margin-left: 5px;
            clear: both;
        }
        
        .help
        {
            float: right;
            margin-right: 10px;
            margin-left: 5px;
            margin-top: 10px;
        }
        
        .Dndright
        {
            float: right;
            margin-right: 10px;
            margin-left: 5px;
            margin-top: 10px;
        }
        
        .movedown
        {
            position: absolute;
            opacity: 0;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
        }
        
        .modal-footer
        {
            padding: 15px;
            text-align: left !important;
            border-top: 1px solid #e5e5e5;
        }
        
        
        #myModalReport .modal-dialog
        {
            width: 730px !important;
            height: 200px !important;
            top: 100px;
        }
   
        
        .modal-header
        {
            padding-top: 5px !important;
            background-color: rgb(51, 51, 51) !important;
            height: 30px !important;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
            font-family: Calibri !important;
            font-size: small !important;
        }
        
        .modal-title
        {
            color: floralwhite !important;
            font-family: Calibri !important;
            font-size: small !important;
        }
        
        .close
        {
            color: floralwhite !important;
            font-size: 1em;
        }
        
        
        #tbldb td
        {
            white-space: nowrap;
        }
        
        
        
        table
        {
            border-spacing: 0;
            border-collapse: separate;
        }
        .clsRemarks
        {
            height: 22px !important;
            overflow: hidden !important;
            width: 200px !important;
        }
        .clsstatus
        {
            max-width: 192px !important;
            min-width: 192px !important;
        }
        
        .clsExpenseClosureDate
        {
            max-width: 75px !important;
        }
        
        .clsCategory
        {
            max-width: 200px !important;
        }
        
        #ddlCategory_Top
        {
            max-width: 105px !important;
        }
        
        #ddlSubCategory_Top
        {
            max-width: 92px !important;
        }
        
        #ddlLongText
        {
            max-width: 145px !important;
        }
        
        #ddlPM
        {
            max-width: 80px !important;
        }
        
        #ddlDM
        {
            max-width: 80px !important;
        }
        
        .clsInitiatedDate
        {
            max-width: 75px !important;
        }
        
        .style1
        {
            width: 115px;
        }
        
        .style8
        {
            width: 78px;
            height: 30px;
        }
        
        
        .style13
        {
            font-size: small;
        }
        
        .style11
        {
            height: 30px;
        }
        
        .style8
        {
            width: 78px;
            height: 30px;
        }
        
        .style17
        {
            height: 30px;
            width: 150px;
        }
        
        .style19
        {
            width: 227px;
            height: 30px;
        }
        
        .style6
        {
            width: 116px;
            height: 30px;
        }
        
        .style12
        {
            width: 97%;
        }
        
        .style15
        {
            width: 50px;
        }
        
        .GridRow
        {
            margin-top: 0px;
        }
        
        .delDrp
        {
            font-size: 6pt;
            margin-left: 0px;
        }
        
        .style23
        {
            width: 2112px;
        }
        
        
        
        
        .style25
        {
            height: 30px;
            width: 87px;
        }
        
        .style26
        {
            width: 50px;
        }
        
        .style27
        {
            height: 30px;
            width: 42px;
        }
        
        .style28
        {
            height: 30px;
            width: 131px;
        }
        
        .style36
        {
            height: 30px;
            width: 123px;
        }
        
        .style37
        {
            width: 82px;
            height: 30px;
        }
        
        .style38
        {
            height: 30px;
            width: 110px;
        }
        
        .style39
        {
            height: 30px;
            width: 115px;
        }
        
        .style40
        {
            width: 5%;
        }
        
        .style43
        {
            width: 699px;
        }
        
        .overlay
        {
            position: fixed;
            text-align: justify;
            padding-left: 500px;
            padding-top: 200px;
            width: 100%;
            height: 100%;
            z-index: 1000;
            opacity: 1;
            -moz-opacity: 1;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        
        
        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover
        {
            color: black !important;
            cursor: default;
            background-color: white !important;
            height: 25px !important;
            padding-top: 4px !important;
        }
        
        .nav-tabs > li > a
        {
            margin-right: 2px;
            line-height: 1.4285;
            border-radius: 4px 4px 0 0;
            color: white !important;
            background-color: gray !important;
            height: 25px !important;
            padding-top: 4px !important;
        }
    </style>
    <style>
        .gifmodal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" AsyncPostBackTimeout="300" runat="server">
    </asp:ScriptManager>
    <div style="margin-top: 5px; margin-left: 3px">
        <div style="width: 70%; float: left">
            <ul class="nav nav-tabs" id="myTabs">
                <li class="active"><a data-toggle="tab" href="#home" onclick="tab('Summary')">Summary</a></li>
                <li><a data-toggle="tab" href="#menu1" onclick="tab('Billing')">Expense Recovery</a></li>
                <li><a data-toggle="tab" href="#menu2" onclick="tab('Access')">Delegate Access</a></li>
            </ul>
        </div>
        <div style="width: 30%; float: left; border-bottom-width: 1px; border-bottom-color: #ddd;
            border-bottom-style: solid; padding-bottom: 10px">
            <asp:Label ID="lblDate" runat="server" Style="text-align: left; font-size: x-small;"
                Font-Names="Verdana" Font-Size="10pt" Font-Bold="true" ForeColor="Black" Text="Label"></asp:Label>
        </div>
        <div class="tab-content">
            <div id="home" class="tab-pane fade in active">
                <iframe id="iframe3" runat="server" style="border: 0px; height: 650px; width: 100%">
                </iframe>
            </div>
            <div id="menu1" class="tab-pane fade">
                <div class="searchPractice">
                    <asp:DropDownList ID="ddlPracticeLine" runat="server" AppendDataBoundItems="true"
                        CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="ALL">--Select PL--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlCustomerCode" runat="server" AppendDataBoundItems="true"
                        CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="ALL">--Select CustCode--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlPM" runat="server" AppendDataBoundItems="true" CssClass="delDrp"
                        Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="-1">--Select PM--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlDM" runat="server" AppendDataBoundItems="true" CssClass="delDrp"
                        Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="-1">--Select DM--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlAgeingBucket" runat="server" AppendDataBoundItems="true"
                        CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="-1">--Ageing Bkt--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlProfitCentre" runat="server" AppendDataBoundItems="true"
                        CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="ALL">--Select ProfitCentre--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlCommitments" runat="server" AppendDataBoundItems="true"
                        CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="ALL">--Missed Commitments--</asp:ListItem>
                        <asp:ListItem Value="Yes">YES</asp:ListItem>
                        <asp:ListItem Value="No">NO</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlLongText" runat="server" AppendDataBoundItems="true" CssClass="delDrp"
                        Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="ALL">--Select G/L Acct Long Text--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlCategory_Top" runat="server" AppendDataBoundItems="true"
                        CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="ALL">--Select Category--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search">
                    <asp:DropDownList ID="ddlSubCategory_Top" runat="server" AppendDataBoundItems="true"
                        CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt">
                        <asp:ListItem Value="ALL">--Select Status--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="searchbtn">
                    <button type="button" class="btn btn-info btn-sm" style="margin-left: 5px; margin-top: 0px;
                        font-family: Calibri; font-size: small; height: 25px; padding-top: 2px; padding-bottom: 2px"
                        title="Search" onclick="buildGridMain('1')">
                        <i class="fa fa-search"></i>
                    </button>
                </div>
                <div class="searchbtn">
                    <button type="button" class="btn btn-info btn-sm" style="margin-left: 5px; margin-top: 0px;
                        font-family: Calibri; font-size: small; height: 25px; padding-top: 2px; padding-bottom: 2px"
                        title="Clear Filter" onclick="ClearFilter()">
                        <i class="fa fa-refresh"></i>
                    </button>
                </div>
                <div class="search">
                    <span style="color: red; display: none" id="lblLoad">Searching...</span>
                    <img id="imgloading" style="display: none" src="Content/images/loader.gif" />
                </div>
                <asp:UpdatePanel ID="Up2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="float: left; width: 150px; margin-top: 5px">
                            <div style="float: left">
                                <button id="btnSave" type="button" class="btn btn-info btn-sm" style="margin-left: 5px;
                                    font-family: Calibri; font-size: small; height: 25px; padding-top: 2px; padding-bottom: 2px;
                                    margin-right: 5px" onclick="save()" title="Save">
                                    <i class="fa fa-save"></i>
                                </button>
                                <asp:ImageButton ID="btndownloadold" Style="position: relative; font-family: Calibri;
                                    font-size: small; height: 25px; padding-top: 5px;" ImageUrl="~/Content/images/font-awesome_4-7-0_download_15_0_eff4fa_5cc0de.png"
                                    class="btn btn-info btn-sm" ToolTip="Download" OnClick="btndownloadold_Click"
                                    OnClientClick="DownloadProgress(3)" Visible="true" runat="server"></asp:ImageButton>
                                <asp:ImageButton ID="btnDownloadPivot" Style="position: relative; font-family: Calibri;
                                    font-size: small; height: 25px; padding-top: 5px" ImageUrl="~/Content/images/font-awesome_4-7-0_tasks_15_0_eff4fa_5cc0de.png"
                                    class="btn btn-info btn-sm" ToolTip="Download Pivot" OnClick="btndownloadpivot_Click"
                                    OnClientClick="DownloadProgress(3)" Visible="False" runat="server"></asp:ImageButton>
                                <img id="imgloading2" style="display: none" src="Content/images/loader.gif" />
                            </div>
                            <div style="float: left">
                                <asp:Button ID="btnDownload" Style="position: relative; font-family: Calibri; font-size: small;
                                    height: 25px; padding-top: 1px" Text="Download" class="btn btn-info btn-sm" OnClick="btnDownload_Click"
                                    OnClientClick="DownloadProgress(2)" Visible="False" runat="server"></asp:Button>
                            </div>
                            <div style="float: left; margin-left: 3px; margin-top: 3px">
                                <img id="imgDownload1" style="display: none" src="Content/images/loader.gif" />
                            </div>
                        </div>
                        <iframe id="iframe" runat="server" style="display: none"></iframe>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbBulkUpdate" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btnDownloadPivot" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btndownloadold" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="help">
                    <asp:ImageButton ID="ImageHelp" runat="server" ImageAlign="Left" Height="20px" Width="20px"
                        ImageUrl="~/img/Help-icon.png" OnClick="ImageHelp_Click" /><asp:Label ID="Label3"
                            runat="server" CssClass="lbl" Font-Names="Calibri" Font-Size="8pt" ToolTip="Help"
                            Font-Bold="True"></asp:Label>
                </div>
                <asp:UpdatePanel ID="Up1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="float: left; margin-top: 5px; margin-left: 9px">
                            <span class="style13" style="margin-left: 3px"><strong>
                                <asp:Label ID="lblMsgBulk" runat="server" Font-Names="Calibri" Font-Size="8pt" Text="Updates Can be uploaded Using Excel template:"></asp:Label></strong></span>
                            &nbsp;<span class="style13"><asp:LinkButton ID="lbBulkUpdate" runat="server" OnClientClick="DownloadProgress(1)"
                                Style="font-size: 11px; cursor: pointer; font-weight: bold; font-style: normal;
                                font-family: Calibri" OnClick="lbBulkUpdate_Click">Download For Bulk Update</asp:LinkButton></span>
                            / <a style="font-size: 11px; cursor: pointer; font-weight: bold; font-style: normal;
                                font-family: Calibri" onclick="Modal()">Upload</a>
                            <img id="imgDownload" style="display: none" src="Content/images/loader.gif" />
                            &nbsp;&nbsp;&nbsp; <span id="Label2" class="delDrp" style="display: inline-block;
                                color: Red; font-family: Calibri; font-size: 8pt; font-weight: bold; width: 406px;">
                                *Red Colored lines indicate the Missed Commitments.</span>
                        </div>
                        <iframe id="iframe1" runat="server" style="display: none" />
                        <asp:HiddenField ID="hddata" runat="server" />
                         
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <div style="margin-left: 10px; margin-right: 20px; min-height: 250px">
                    <div class="divgrid gridportion">
                        <table id="tbldb" class="table-fill">
                            <thead>
                                <tr>
                                    <th>
                                        <input id="chkHead" type="checkbox" onclick="selectall(this)" />
                                    </th>
                                    <th>
                                        Active Project Code
                                    </th>
                                    <th>
                                        Customer Code
                                    </th>
                                    <th>
                                        Amount in USD
                                    </th>
                                    <th>
                                        Category
                                    </th>
                                    <th>
                                        Status
                                    </th>
                                    <th>
                                        Expense Closure Date
                                    </th>
                                    <th>
                                        Expense/Reversal Initiated Date
                                    </th>
                                    <th>
                                        Invoice/Confirmation No
                                    </th>
                                    <th>
                                        AHD Req No
                                    </th>
                                    <th>
                                        Remarks
                                    </th>
                                    <th>
                                        Stage Code
                                    </th>
                                    <th>
                                        With Whom
                                    </th>
                                    <th>
                                        Client IBU
                                    </th>
                                    <th>
                                        PM Mail Id
                                    </th>
                                    <th>
                                        DM Mail Id
                                    </th>
                                    <th>
                                        Profit Center
                                    </th>
                                    <th>
                                        Reference
                                    </th>
                                    <th>
                                        Document Number
                                    </th>
                                    <th>
                                        Local Currency
                                    </th>
                                    <th>
                                        Amount in Local Currency
                                    </th>
                                    <th>
                                        Posting Date
                                    </th>
                                    <th>
                                        Text
                                    </th>
                                    <th>
                                        Country
                                    </th>
                                    <th>
                                        Ageing Days
                                    </th>
                                    <th>
                                        Contract Type
                                    </th>
                                    <th>
                                        Vendor Code
                                    </th>
                                    <th>
                                        G/L Acct Long Text
                                    </th>
                                    <th>
                                        Update
                                    </th>
                                    <th>
                                        LastUpdatedBY
                                    </th>
                                    <th>
                                        LastUpdatedOn
                                    </th>
                                    <th>
                                        ResponseFromFinance
                                    </th>
                                    <th style="display: none">
                                        Unique
                                    </th>
                                    <th style="display: none">
                                        SubCategory_Old
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
                <table class="gridportion">
                    <tr>
                        <td id="tblpage" style="display: none">
                            <ul id="pagination-demo" class="pagination-sm">
                            </ul>
                        </td>
                        <td>
                            <img id="imgloadingpage" style="display: none" src="Content/images/loading.gif" />
                        </td>
                    </tr>
                </table>
                <%--   <span class="spanNotData">No Data Found!!</span>--%>
                <asp:HiddenField ID="hdnddldata" Value="" runat="server" />
                <%--<table>
                                <tr>
                                    
                                    <td class="style19">&nbsp;&nbsp;<asp:Label ID="lblAsofdate" runat="server" Font-Names="Calibri"
                                        Font-Size="8pt" Text="Download previous dates report here: " Visible="False"></asp:Label>
                                    </td>
                                    <td class="style17">
                                        <asp:DropDownList ID="ddlAsofdate" runat="server" AutoPostBack="True"
                                            DataSourceID="SqlDataSource1" DataTextField="asofdate"
                                            DataValueField="asofdate" Font-Names="Calibri" Font-Size="8pt"
                                            Visible="False">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:DBConnectString %>"
                                            SelectCommand="SELECT DISTINCT [asofdate] FROM [DelayedBilling_Backup]"></asp:SqlDataSource>
                                    </td>

                                    <td></td>
                                </tr>
                            </table>--%>
               
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div id="myModalReport" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">
                                    &times;</button>
                                <h4 class="modal-title">
                                    Upload</h4>
                            </div>
                            <div class="modal-body" style="margin-top: 0px!important; margin-left: 0px!important;
                                height: 200px!important">
                                <div style="margin-top: 5px">
                                    <asp:HiddenField ID="hdDate" runat="server" />
                                    <iframe id="iframe5" runat="server" style="border: 0px;width:700px"></iframe>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="menu2" class="tab-pane fade">
                <iframe id="iframe2" runat="server" style="border: 0px; height: 650px; width: 100%">
                </iframe>
            </div>
        </div>
    </form>
</body>
</html>

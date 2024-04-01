<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Content/Scripts/jquery-1.12.4.js"></script>
    <script>
        function UploadProgress() {

            $('.Msg').empty();

            $("#imgUpload").show();
        }

        function check() {

            alert('Data successfully uploaded ');

            parent.RefreshPage();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-top: 50px">
        <asp:ScriptManager ID="sm" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <!-- Modal content-->
                <div style="margin-left: 10px;">
                    <div style="float: left; margin-top: 5px; margin-left: 170px">
                        <asp:FileUpload ID="fuBulkUpload" runat="server" Font-Names="Calibri" Font-Size="8pt" /></div>
                    <div style="float: left; margin-left: 10px">
                        <asp:Button ID="btnUpload" Style="font-family: Calibri; font-size: small; height: 25px;
                            padding-top: 0px" Text="Upload" class="btn btn-success btn-sm" OnClick="btnUpload_Click"
                            OnClientClick="UploadProgress()" runat="server"></asp:Button>
                    </div>
                    <div style="float: left; margin-left: 10px; margin-top: 5px">
                        <img id="imgUpload" style="display: none" src="Content/images/loader.gif" /></div>
                    <br />
                    <div style="float: left; clear: both; margin-top: 5px">
                        <asp:Label ID="lblMsg" CssClass="Msg" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="8pt" ForeColor="Red"></asp:Label></div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DelegateAccess.aspx.cs" Inherits="DelegateAccess" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Content/Scripts/bootstrap.min.js"></script>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
       <link href="Content/css/jquery-ui.min.css" rel="stylesheet" />
       
    <script language="javascript">

        var color = 'White';
        function changeColor(obj) {
            var rowObject = getParentRow(obj);
            var parentTable = document.getElementById("<%=chkCustomerCodeList.ClientID%>");
            if (color == '') {
                color = getRowColor();
            }
            if (obj.checked) {
                rowObject.style.backgroundColor = '#A3B1D8';
            }
            else {
                rowObject.style.backgroundColor = color;
                color = 'White';
            }

            // private method
            function getRowColor() {
                if (rowObject.style.backgroundColor == 'White') return parentTable.style.backgroundColor;
                else return rowObject.style.backgroundColor;
            }

        }

        // This method returns the parent row of the object

        function getParentRow(obj) {
            do {
                obj = obj.parentElement;
            }
            while (obj.tagName != "TR")
            return obj;
        }


        function TurnCheckBoixGridView(id) {
            var frm = document.forms[0];

            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox" && frm.elements[i].id.indexOf("<%= chkCustomerCodeList.ClientID %>") == 0) {
                    frm.elements[i].checked = document.getElementById(id).checked;
                }
            }
        }

        function SelectAll(id) {

            var parentTable = document.getElementById("<%=chkCustomerCodeList.ClientID%>");
            var color

            if (document.getElementById(id).checked) {
                color = '#A3B1D8'
            }
            else {
                color = 'White'
            }

            for (i = 0; i < parentTable.rows.length; i++) {
                parentTable.rows[i].style.backgroundColor = color;
            }
            TurnCheckBoixGridView(id);

        }
</script>
    <style type="text/css">
        .style7
        {
            width: 745px;
        }
        .style8
        {
            width: 611px;
            
        }
        .FormText
        {
            font-family:Calibri;
        }
         input[type="radio"]
        {
            margin: 4px 4px 4px 4px;    
        }
        .scroll_checkboxes {
                 height:200px;
                 overflow-x:scroll;
        }

    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-left:20px">
    <br />
    <table>
    
     <tr></tr>
    <tr>
        <td colspan="3" class="style7">
            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td class="style8">
            <asp:Label ID="lblEmpmailID" runat="server" Text="EmpmailID : " Font-Names="Calibri" Font-Size="9pt"></asp:Label>
            <asp:TextBox ID="txtEmpmailID" runat="server" Font-Names="Calibri" Font-Size="8pt"></asp:TextBox>
            &nbsp;
            <asp:Label ID="lblMailIDMessage" runat="server" Text="*Please enter mailID (without suffixing @infosys.com)" Font-Names="Calibri" Font-Size="9pt"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <br />
        </td>
        <td class="style7">
            <asp:Button ID="btnCheckAccess" runat="server" Text="Check Access"  Font-Names="Calibri" 
                Font-Size="9pt"  
                onclick="btnCheckAccess_Click" class="btn btn-info btn-sm" Style="position: relative; font-family: Calibri; font-size: small; height: 25px; padding-top: 1px" />
            &nbsp;<asp:Button ID="btnCopyDelegatedAccess" runat="server" Text="Show Delegated Ids" 
                onclick="btnCopyDelegatedAccess_Click"  Font-Names="Calibri" 
                Font-Size="9pt" class="btn btn-info btn-sm" Style="position: relative; font-family: Calibri; font-size: small; height: 25px; padding-top: 1px" />
            
                  &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Names="Calibri" 
                Font-Size="9pt" onclick="btnCancel_Click" Width="134px" class="btn btn-info btn-sm" Style="position: relative; font-family: Calibri; font-size: small; height: 25px; padding-top: 1px" />
            <br />
           
            </td>
    </tr>
<tr><td class="style8">&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td></tr>
 
     
         <tr><td class="style8"><asp:Panel ID="plAlreadyDelegated" runat="server" Width="100%" Visible="false">
                <table>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Enter EmpmailID and click on delegate access button to copy the below access" Font-Names="Calibri" Font-Size="9pt"></asp:Label>
                        </td>
                         
                    <tr>
                        <td>
                            <div class="scroll_checkboxes">
                                <asp:RadioButtonList ID="rbAlreadyDelegated" AutoPostBack="true" runat="server" CssClass="FormText" RepeatLayout="Flow" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbAlreadyDelegated_SelectedIndexChanged"
                            RepeatColumns="1" BorderWidth="0">
                                </asp:RadioButtonList>
                                
                               
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel></td>
    </tr>

    <tr>
        <td class="style8">
            <asp:RadioButtonList ID="rdlSelection" runat="server" AutoPostBack="true" 
                Font-Names="Calibri" Font-Size="9pt" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdlSelection_SelectedIndexChanged" >

               
                <asp:ListItem>MCC</asp:ListItem>
                <asp:ListItem>CC</asp:ListItem>
                <asp:ListItem>PracticeLine</asp:ListItem>
                <asp:ListItem>ProfitCentre</asp:ListItem>
                <asp:ListItem>PM</asp:ListItem>
                <asp:ListItem>DM</asp:ListItem>
           

               
            </asp:RadioButtonList>            
                   
        </td>
    </tr>



         <tr>
        <td class="style8">
           <asp:Panel ID="plCustomerCode" runat="server" Width="100%" Visible="false">
                <table>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblCustomerCode" runat="server" Text="Customer Codes : " Font-Names="Calibri" Font-Size="9pt"></asp:Label>
                        </td>
                           <tr>
                        <td>
                            <asp:LinkButton ID="lnkchkCustomerCode" runat="server" Text="Select All" ForeColor="Blue" OnClick="lbSelectAllCustomerCode_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkUnchkCustomerCode" runat="server" Text="Unselect All" ForeColor="Blue" OnClick="lbUnselectAllCustomerCode_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <div class="scroll_checkboxes">
                                <asp:CheckBoxList ID="chkCustomerCodeList" runat="server" CssClass="FormText" RepeatDirection="Vertical"
                            RepeatColumns="1" BorderWidth="0">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel></td>
                    </tr>
       
  <tr>
        <td class="style8">
            <asp:Panel ID="plMCC" runat="server" Width="100%" Visible="false">
                <table>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblMCC" runat="server" Text="MCCs : " Font-Names="Calibri" Font-Size="9pt"></asp:Label>
                        </td>
                           <tr>
                        <td>
                            <asp:LinkButton ID="lnkchkMCC" runat="server" Text="Select All" ForeColor="Blue" OnClick="lbSelectAllMCC_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkUnchkMCC" runat="server" Text="Unselect All" ForeColor="Blue" OnClick="lbUnselectAllMCC_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <div class="scroll_checkboxes">
                                <asp:CheckBoxList ID="chkMCCList" runat="server" CssClass="FormText" RepeatDirection="Vertical" 
                            RepeatColumns="1" BorderWidth="0">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel></td>
                    </tr>
       
  <tr>
        <td class="style8">
            <asp:Panel ID="plPracticeLine" runat="server" Width="100%" Visible="false">
                <table>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblPracticeLine" runat="server" Text="Practice Lines : " Font-Names="Calibri" Font-Size="9pt"></asp:Label>
                        </td>
                           <tr>
                        <td>
                            <asp:LinkButton ID="lnkchkPracticeLine" runat="server" Text="Select All" ForeColor="Blue" OnClick="lbSelectAllPracticeLine_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkUnchkPracticeLine" runat="server" Text="Unselect All" ForeColor="Blue" OnClick="lbUnselectAllPracticeLine_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <div class="scroll_checkboxes">
                                <asp:CheckBoxList ID="chkPracticeLineList" runat="server" CssClass="FormText" RepeatDirection="Vertical" 
                            RepeatColumns="1" BorderWidth="0">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel></td>
                    </tr>
         <tr>
        <td class="style8">
            <asp:Panel ID="plProfitCentre" runat="server" Width="100%" Visible="false">
                <table>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblProfitCentre" runat="server" Text="Profit Centres : " Font-Names="Calibri" Font-Size="9pt"></asp:Label>
                        </td>
                           <tr>
                        <td>
                            <asp:LinkButton ID="lnkchkProfitCentre" runat="server" Text="Select All" ForeColor="Blue" OnClick="lbSelectAllProfitCentre_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkUnchkProfitCentre" runat="server" Text="Unselect All" ForeColor="Blue" OnClick="lbUnselectAllProfitCentre_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <div class="scroll_checkboxes">
                                <asp:CheckBoxList ID="chkProfitCentreList" runat="server" CssClass="FormText" RepeatDirection="Vertical" 
                            RepeatColumns="1" BorderWidth="0">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel></td>
                    </tr>
       
  <tr>
        <td class="style8">
            <asp:Panel ID="plPM" runat="server" Width="100%" Visible="false">
                <table>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblPM" runat="server" Text="PMs : " Font-Names="Calibri" Font-Size="9pt"></asp:Label>
                        </td>
                           <tr>
                        <td>
                            <asp:LinkButton ID="lnkchkPM" runat="server" Text="Select All" ForeColor="Blue" OnClick="lbSelectAllPM_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkUnchkPM" runat="server" Text="Unselect All" ForeColor="Blue" OnClick="lbUnselectAllPM_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <div class="scroll_checkboxes">
                                <asp:CheckBoxList ID="chkPMList" runat="server" CssClass="FormText" RepeatDirection="Vertical" 
                            RepeatColumns="1" BorderWidth="0">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel></td>
                    </tr>
       

  <tr>
        <td class="style8">
            <asp:Panel ID="plDM" runat="server" Width="100%" Visible="false">
                <table>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblDM" runat="server" Text="DMs : " Font-Names="Calibri" Font-Size="9pt"></asp:Label>
                        </td>
                           <tr>
                        <td>
                            <asp:LinkButton ID="lnkchkDM" runat="server" Text="Select All" ForeColor="Blue" OnClick="lbSelectAllDM_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkUnchkDM" runat="server" Text="Unselect All" ForeColor="Blue" OnClick="lbUnselectAllDM_Click" Font-Names="Calibri" Font-Size="8pt"></asp:LinkButton>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <div class="scroll_checkboxes">
                                <asp:CheckBoxList ID="chkDMList" runat="server" CssClass="FormText" RepeatDirection="Vertical" 
                            RepeatColumns="1" BorderWidth="0">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel></td>
                    </tr>
       <tr>

       
        <td class="style8" style="padding-top:10px">
          <asp:Button ID="btnDelegate" runat="server" Text="Delegate Access" Font-Names="Calibri" 
                Font-Size="9pt" onclick="btnDelegate_Click" Width="134px" class="btn btn-info btn-sm" Style="position: relative; font-family: Calibri; font-size: small; height: 25px; padding-top: 1px" />
        </td>
    </tr>
                </table>
            </asp:Panel></td>
                    </tr>




    </table>
    </div>
</asp:Content>


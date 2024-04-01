<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="~/ExpenseRecoveryOld.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <script runat="server">

        protected void gvExpenseRec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    </script>
    <script type="text/javascript">



        function showProgress() {

            var updateProgress = $get("<%= UpdateProgress.ClientID %>");

            updateProgress.innerHTML = '<img src="img/loading.gif" width="200" height ="200"/>';
            updateProgress.style.display = "";
            updateProgress.className += "overlay";


        }


           

         


    </script>
    <style type="text/css">
        .style11
        {
            margin-right: 5px;
            margin-left: 5px;
            padding-right: 5px;
            padding-left: 5px;
        }
        .delDrp
        {
        }
        .style13
        {
            width: 409px;
        }
        .style14
        {
            width: 980px;
        }
        .style19
        {
        }
        .style23
        {
        }
        .button
        {
            color: black;
            height: 20px;
            padding-top: 1px;
            padding-bottom: 1px;
            padding-left: 5px;
            padding-right: 5px;
            margin: 1px;
            vertical-align: middle;
            text-align: center;
        }
        
        .lbl
        {
            display: block;
            float: left;
            padding-top: 2px; /*This needs to be modified to fit */
        }
        
        .button:hover
        {
            background-color: White;
            color: #336699;
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
        
        .grid
        {
            width: 100%;
        }
        
        .grid th
        {
            background-color: #f0f0f0;
            color: Black;
            padding: 5px;
            border: 1px solid black;
        }
        
        .grid tr:nth-child(even)
        {
            background-color: #ffffff;
        }
        
        .grid tr:nth-child(odd)
        {
            background-color: #E3EAEB;
        }
        
        .grid td
        {
            padding-left: 5px;
            padding-right: 5px;
        }
        
        
        
        
        
        
        
        .style24
        {
            font-family: Calibri;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Always">
        <ContentTemplate>
            <span class="style13"></span>
            <table>
                <tr>
                    <td class="style11">
                        <asp:DropDownList ID="ddlCustomerCode" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlCustomerCode_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select CustomerCode--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:DropDownList ID="ddlPracticeLine" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlPracticeLine_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select PracticeLine--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <b>
                            <asp:DropDownList ID="ddlProfitCentre" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="True" CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlProfitCentre_SelectedIndexChanged">
                                <asp:ListItem Value="ALL">--Select ProfitCentre--</asp:ListItem>
                            </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:DropDownList ID="ddlPM" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlPM_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select PM--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:DropDownList ID="ddlDM" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlDM_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select DM--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:DropDownList ID="ddlAgeingBucket" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlAgeingBucket_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select AgeingBucket--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:DropDownList ID="ddlCommitments" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlCommitments_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Missed Commitments--</asp:ListItem>
                            <asp:ListItem Value="Yes">YES</asp:ListItem>
                            <asp:ListItem Value="No">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="middle">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Left" Height="20px"
                            Width="20px" ImageUrl="~/img/Help-icon.png" OnClick="ImageButton1_Click" /><asp:Label
                                runat="server" CssClass="lbl" Font-Names="Calibri" Font-Size="8pt" Text="Help"
                                Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="style11">
                        <asp:DropDownList ID="ddlLongText" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlLongText_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select G/L Acct Long Text--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:DropDownList ID="ddlCategory_Top" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlCategory_Top_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select Category--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style11">
                        <asp:DropDownList ID="ddlSubCategory_Top" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="delDrp" Font-Names="Calibri" Font-Size="8pt" OnSelectedIndexChanged="ddlSubCategory_Top_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select Status--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="delDrp" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="8pt" ForeColor="Red" Text="*Red Colored lines indicate the Missed Commitments."
                            Width="350px">
                        </asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="style19">
                        <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" CausesValidation="true"
                            CssClass="button" />
                    </td>
                    <%--            <caption>--%>
                    <td class="style19">
                        <asp:Button ID="BtnCancel" runat="server" CausesValidation="false" OnClick="BtnCancel_Click"
                            Text="Cancel" CssClass="button" />
                    </td>
                    <td class="style19">
                        <asp:Button ID="btnDownload0" runat="server" Enabled="false" OnClientClick="showProgress()"
                            OnClick="btnDownload_Click1" Text="Download" CssClass="button" />
                    </td>
                    <td class="style19">
                        <%--            </caption>--%>
                        <%--   </tr>--%>
                        <%--   <tr>--%>
                        <asp:Button ID="btnResetFilters" runat="server" Text="Reset Filters" OnClick="btnResetFilters_Click"
                            CssClass="button" Visible="False" />
                    </td>
                    <td class="style19">
                        <asp:Button ID="btnDownloadPivot" Text="Download Pivot" Visible="false" Enabled="false"
                            runat="server" OnClick="Button1_Click" CssClass="button" OnClientClick="showProgress()" />
                    </td>
                    <td class="style14">
                        <asp:Label ID="Lblmsg" runat="server" Style="font-weight: 700; float: 7pt; font-family: Calibri;
                            font-size: 8pt;"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="style23">
                        <span class="style24">Updates can be uploaded using Excel template</span> :
                        <asp:LinkButton ID="lbBulkUpdate" runat="server" Font-Names="Calibri" Font-Size="8pt"
                            OnClientClick="showProgress()" OnClick="lbBulkUpdate_Click">Download Bulk Update Template</asp:LinkButton>
                        <asp:LinkButton ID="lbUpload" runat="server" Font-Names="Calibri" Font-Size="8pt"
                            OnClick="lbUpload_Click">Upload</asp:LinkButton>
                    </td>
                    <td style="text-align: justify" colspan="6" class="style23">
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fuBulkUpload" runat="server" Font-Names="Calibri" Font-Size="8pt" />
                                        <asp:Button ID="btnBulkUpload" runat="server" Font-Names="Calibri" Font-Size="8pt"
                                            Height="21px" OnClick="btnBulkUpload_Click" OnClientClick="showProgress()" Style="height: 21px"
                                            Text="Upload" />
                                    </td>
                                </tr>
                                <%--                            <tr>
                                <td class="style21">
                                    &nbsp;</td>
                                <td style="text-align: left" class="style6">
                                    &nbsp;</td>

                            </tr>--%>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblChkBoxErrorMsg" runat="server" Text="Only one row can be updated at a time , for bulk update use the Download Bulk Update Template button."></asp:Label>
            <asp:GridView ID="gvExpenseRec" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="grid" Font-Names="Calibri" Font-Size="8pt" margin-top="0px" OnPageIndexChanging="gvExpenseRec_ExceptionPageIndexChanging"
                OnRowDataBound="gvExpenseRec_ExceptionRowDataBound" PageSize="15" RowStyle-Font-Names="Calibri"
                RowStyle-Font-Size="8" ShowFooter="True" Width="100%" OnSelectedIndexChanged="gvExpenseRec_SelectedIndexChanged"
                ForeColor="#333333" GridLines="None">
                <PagerStyle BackColor="#F0F0F0" ForeColor="#333333" HorizontalAlign="Left" VerticalAlign="Bottom" />
                <RowStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#FFFBD6" ForeColor="#333333" />
                <%--       <alternatingrowstyle  Height="80px"/>--%>
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField AccessibleHeaderText="ID" HeaderText="ID " Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Mark for Edit " HeaderText="Mark for Edit">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbEdit" runat="server" AutoPostBack="true" Font-Names="Calibri"
                                Font-Size="8pt" OnCheckedChanged="chkChange_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Active Project Code" HeaderText="Active Project Code">
                        <ItemTemplate>
                            <asp:Label ID="lblActiveProjectCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="CustomerCode" HeaderText="Customer Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="AmntUSD" HeaderText="Amount in USD">
                        <ItemTemplate>
                            <asp:Label ID="lblAmntUSD" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" ItemStyle-Wrap="false" InsertVisible="False"
                        SortExpression="SubCategory">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                Visible="<%# IsInEditMode %>" ForeColor="Black" Font-Names="Calibri" Font-Size="8pt">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Status" InsertVisible="False"
                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="SubSubCategory">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lblSubCategory" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"
                                Visible="<%# IsInEditMode %>" ForeColor="Black" Font-Names="Calibri" Font-Size="8pt">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Comments" InsertVisible="False" >
                            <HeaderStyle />
                           
                            <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Visible='<%# !(bool) IsInEditMode %>'  ></asp:Label>
                                <asp:TextBox ID="TxtComments" runat="server" Visible='<%# IsInEditMode %>' ></asp:TextBox>
                                  
                            </ItemTemplate>
                        </asp:TemplateField--%>
                    <asp:TemplateField HeaderText="Expense Closure Date" InsertVisible="False" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="False" SortExpression="SubCategory">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseClosureDate" runat="server"></asp:Label>
                            <%--        Text='<%# Eval("[BillCommitmntdt]","{0:d}") %>'--%>
                            <asp:ImageButton ID="btnDate" runat="Server" Font-Names="Calibri" Font-Size="8pt"
                                Height="15px" ImageAlign="Right" ImageUrl="~/img/calendar.gif" OnClick="btnDate_Click"
                                Text="" Width="15px" />
                            <br />
                            <asp:Calendar ID="CalExpenseClosureDate" runat="server" BackColor="White" BorderColor="#999999"
                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="Black" Height="180px" OnSelectionChanged="CalExpenseClosureDate_SelectionChanged"
                                Visible="false" Width="200px">
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="8pt" />
                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                            </asp:Calendar>
                            <asp:TextBox ID="txtCal" runat="server" Visible="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqValDate" runat="server" ControlToValidate="txtCal"
                                ErrorMessage="Please select a date." Visible="false"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expense/Reversal Initiated Date" InsertVisible="False"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="False"
                        SortExpression="SubCategory">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lblInitatedDate" runat="server"></asp:Label>
                            <%--        Text='<%# Eval("[BillCommitmntdt]","{0:d}") %>'--%>
                            <asp:ImageButton ID="btnInitatedDate" runat="Server" Font-Names="Calibri" Font-Size="8pt"
                                Height="15px" ImageAlign="Right" ImageUrl="~/img/calendar.gif" OnClick="btnInitatedDate_Click"
                                Text="" Width="15px" />
                            <br />
                            <asp:Calendar ID="CalInitatedDate" runat="server" BackColor="White" BorderColor="#999999"
                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="Black" Height="180px" OnSelectionChanged="CalInitatedDate_SelectionChanged"
                                Visible="False" Width="200px">
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="8pt" />
                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                            </asp:Calendar>
                            <asp:RequiredFieldValidator ID="reqValDate1" runat="server" ControlToValidate="txtCalInitated"
                                ErrorMessage="Please select a date." Visible="false"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField ControlStyle-Width="100px" HeaderText="Update from Account team"
                        InsertVisible="False" SortExpression="SubCategory" Visible="false">
                        <HeaderStyle />
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUpdatefromAccountteam" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUpdatefromAccountteam" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlUpdatefromAccountteam" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlUpdatefromAccountteam_SelectedIndexChanged">
                                <%--<asp:ListItem Text="Already Invoiced" Value="Already Invoiced" />
                                    <asp:ListItem Text="Will be Billed in the Current Month" Value="Will be Billed in the Current Month" />
                                    <asp:ListItem Text="Will be Billed in Current Quarter" Value="Will be Billed in Current Quarter" />
                                    <asp:ListItem Text="Will be Billed in Next Quarter" Value="Will be Billed in Next Quarter" />
                                    <asp:ListItem Text="To be reversed -  DM Approval Obtained" Value="To be reversed -  DM Approval Obtained" />
                                    <asp:ListItem Text="To be reversed -  DM Approval pending" Value="To be reversed -  DM Approval pending" />--%>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Invoice/Confirmation No" InsertVisible="False"
                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Invoice No">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceNo" runat="server" Visible="true"></asp:Label>
                            <asp:TextBox ID="TxtInvoiceNo" runat="server" Visible="<%# IsInEditMode %>" ForeColor="Black"
                                Font-Names="Calibri" Font-Size="8pt" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtInvoiceNo"
                                Enabled="false" ErrorMessage="Enter Invoice" Visible="<%# IsInEditMode %>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtInvoiceNo"
                                Enabled="false" ErrorMessage="Enter Valid No." ValidationExpression="[0-9]+"
                                Visible="<%# IsInEditMode %>"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                    </asp:TemplateField>
                    <%--    <asp:TemplateField HeaderText="Confirmation No"  InsertVisible="False" SortExpression="Confirmation No" ItemStyle-VerticalAlign="Top">
                              <HeaderStyle />
                               <ItemStyle VerticalAlign="Top" />
                            <EditItemTemplate>
                               
                                <asp:TextBox ID="TxtConfirmationNo" runat="server" Visible='<%# IsInEditMode %>' ></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblConfirmationNo" runat="server" Visible='<%# !(bool) IsInEditMode %>'></asp:Label>
                                <asp:TextBox ID="TxtConfirmationNo" runat="server" Visible='<%# IsInEditMode %>' ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter confirmation no." ControlToValidate="TxtConfirmationNo" Enabled="false" ></asp:RequiredFieldValidator>    
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderText="AHD Req No" InsertVisible="False"
                        SortExpression="Serv Centrale Req No">
                        <HeaderStyle />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblServCentraleReqNo" runat="server" Visible="<%# !(bool) IsInEditMode %>"></asp:Label>
                            <asp:TextBox ID="TxtServCentraleReqNo" runat="server" Visible="<%# IsInEditMode %>"
                                ForeColor="Black" Font-Names="Calibri" Font-Size="8pt"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Remarks" InsertVisible="False"
                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Remarks">
                        <HeaderStyle />
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Visible="<%# !(bool) IsInEditMode %>"></asp:Label>
                            <asp:TextBox ID="TxtRemarks" runat="server" Visible="<%# IsInEditMode %>" ForeColor="Black"
                                Font-Names="Calibri" Font-Size="8pt" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtRemarks"
                                Enabled="false" ErrorMessage="Please provide your comments."></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField AccessibleHeaderText="Stage Code" HeaderText="Stage Code">
                        <ItemTemplate>
                            <asp:Label ID="lblStageCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="With Whom" HeaderText="With Whom">
                        <ItemTemplate>
                            <asp:Label ID="lblWithWhom" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>

                    <asp:TemplateField AccessibleHeaderText="Client IBU" HeaderText="Client IBU ">
                        <ItemTemplate>
                            <asp:Label ID="lblClientIBU" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="PM" HeaderText="PM Mail Id">
                        <ItemTemplate>
                            <asp:Label ID="lblPM" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="DM" HeaderText="DM Mail Id">
                        <ItemTemplate>
                            <asp:Label ID="lblDM" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Profit Center" HeaderText="Profit Center">
                        <ItemTemplate>
                            <asp:Label ID="lblProfitCenter" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Reference" HeaderText="Reference">
                        <ItemTemplate>
                            <asp:Label ID="lblReference" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Document Number" HeaderText="Document Number">
                        <ItemTemplate>
                            <asp:Label ID="lblDocumentNumber" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="LocalCurrency" HeaderText="Local Currency">
                        <ItemTemplate>
                            <asp:Label ID="lblLocalCurrency" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Amount in Local Currency" HeaderText="Amount in Local Currency">
                        <ItemTemplate>
                            <asp:Label ID="lblAmntLC" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Posting Date" HeaderText="Posting Date">
                        <ItemTemplate>
                            <asp:Label ID="lblPostingDate" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Text" ItemStyle-Wrap="false" HeaderText="Text">
                        <ItemTemplate>
                            <asp:Label ID="lblText" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Country" HeaderText="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblCountry" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Ageing Days" HeaderText="Ageing Days">
                        <ItemTemplate>
                            <asp:Label ID="lblAgeingDays" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Ageing Bucket" HeaderText="Ageing Bucket"
                        Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAgeingBucket" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Contract Type" HeaderText="Contract Type">
                        <ItemTemplate>
                            <asp:Label ID="lblContractType" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Vendor Code" HeaderText="Vendor Code">
                        <ItemTemplate>
                            <asp:Label ID="lblVendorCode" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                   
                   
                    <asp:TemplateField AccessibleHeaderText="G/L Acct Long Text" ItemStyle-Wrap="false"
                        HeaderText="G/L Acct Long Text">
                        <ItemTemplate>
                            <asp:Label ID="lblGLAcctLongText" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Update" HeaderText="Update">
                        <ItemTemplate>
                            <asp:Label ID="lblUpdate" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="LastUpdatedBY" HeaderText="LastUpdatedBY">
                        <ItemTemplate>
                            <asp:Label ID="lblLastUpdatedBY" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="LastUpdatedOn" HeaderText="LastUpdatedOn">
                        <ItemTemplate>
                            <asp:Label ID="lblLastUpdatedOn" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="ResponseFromFinance" HeaderText="ResponseFromFinance">
                        <ItemTemplate>
                            <asp:Label ID="lblResponseFromFinance" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="Unique" HeaderText="Unique" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblUnique" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField AccessibleHeaderText="SubCategory_Old" HeaderText="SubCategory_Old"
                        Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblSubCategory_Old" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BorderStyle="Solid" BackColor="#F0F0F0" BorderColor="Black" BorderWidth="1px"
                    Font-Bold="True" ForeColor="White" />
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" BackColor="#990000" Wrap="false"
                    Height="25px" Font-Names="Calibri" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                    Font-Bold="True" ForeColor="White" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                <ProgressTemplate>
                    <div id="dvLoader">
                        <div id="LoaderDiv">
                            <img alt="" src="img/loading.gif" width="200" height="200" style="display: none" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <iframe id="iframe" runat="server" style="display: none"></iframe>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBulkUpload" />
            <asp:PostBackTrigger ControlID="lbBulkUpdate" />
            <asp:PostBackTrigger ControlID="btnDownload0" />
            <%--<asp:PostBackTrigger ControlID="btnDate" />--%>
            <%--<asp:PostBackTrigger ControlID="gvExpenseRec" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

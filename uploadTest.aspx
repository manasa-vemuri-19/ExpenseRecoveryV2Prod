<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="uploadTest.aspx.cs" Inherits="uploadTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

.overlay

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

</style>

<script type="text/javascript">

    function showProgress() {

        var updateProgress = $get("<%= UpdateProgress.ClientID %>");

        updateProgress.style.display = "block";

    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>

<asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">

<ContentTemplate>
 <table>
            <tr>
                <td class="style11">
                    <asp:DropDownList ID="ddlCustomerCode" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="True" CssClass="delDrp" Font-Names="Verdana" Font-Size="8pt" OnSelectedIndexChanged="ddlCustomerCode_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Select CustomerCode--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style11">
                    <asp:DropDownList ID="ddlPracticeLine" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="True" CssClass="delDrp" Font-Names="Verdana" Font-Size="8pt" OnSelectedIndexChanged="ddlPracticeLine_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Select PracticeLine--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style11">
                    <b>
                        <asp:DropDownList ID="ddlProfitCentre" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="delDrp" Font-Names="Verdana" Font-Size="8pt" OnSelectedIndexChanged="ddlProfitCentre_SelectedIndexChanged">
                            <asp:ListItem Value="ALL">--Select ProfitCentre--</asp:ListItem>
                        </asp:DropDownList>
                </td>
                <td class="style11">
                    <asp:DropDownList ID="ddlPM" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                        CssClass="delDrp" Font-Names="Verdana" Font-Size="8pt" OnSelectedIndexChanged="ddlPM_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Select PM--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style11">
                    <asp:DropDownList ID="ddlDM" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                        CssClass="delDrp" Font-Names="Verdana" Font-Size="8pt" OnSelectedIndexChanged="ddlDM_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Select DM--</asp:ListItem>
                    </asp:DropDownList>
                </td>
               <td class="style11">
                    <asp:DropDownList ID="ddlAgeingBucket" runat="server" 
                        AppendDataBoundItems="true" AutoPostBack="True" CssClass="delDrp" 
                        Font-Names="Verdana" Font-Size="8pt" 
                        OnSelectedIndexChanged="ddlAgeingBucket_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Select AgeingBucket--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                   <td class="style11">
                    <asp:DropDownList ID="ddlCommitments" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="True" CssClass="delDrp" Font-Names="Verdana" Font-Size="8pt" OnSelectedIndexChanged="ddlCommitments_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Missed Commitments--</asp:ListItem>
                        <asp:ListItem Value="Yes">YES</asp:ListItem>
                        <asp:ListItem Value="No">NO</asp:ListItem>
                    </asp:DropDownList>
                    </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="style11">
                    <asp:DropDownList ID="ddlLongText" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                        CssClass="delDrp" Font-Names="Verdana" Font-Size="7pt" OnSelectedIndexChanged="ddlLongText_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Select G/L Acct Long Text--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style11">
                    <asp:DropDownList ID="ddlCategory_Top" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="True" CssClass="delDrp" Font-Names="Verdana" Font-Size="7pt" OnSelectedIndexChanged="ddlCategory_Top_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Select Category--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style11">
                    <asp:DropDownList ID="ddlSubCategory_Top" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="True" CssClass="delDrp" Font-Names="Verdana" Font-Size="7pt" OnSelectedIndexChanged="ddlSubCategory_Top_SelectedIndexChanged">
                        <asp:ListItem Value="ALL">--Select Status--</asp:ListItem>
                    </asp:DropDownList>
                </td>
             
                   <td><asp:Label ID="Label2" runat="server" CssClass="delDrp" Font-Bold="True" Font-Names="Verdana"
            Font-Size="8pt" ForeColor="Red" Text="*Red Colored lines indicate the Missed Commitments."
            Width="350px">
            </asp:Label></td>
            </tr>
        </table>
        <table>
        <tr>
        <td class="style19">
            <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" 
                CausesValidation="true" CssClass="button" />
            </td>
<%--            <caption>--%>
   <td class="style19">
                <asp:Button ID="BtnCancel" runat="server" CausesValidation="false" 
                    OnClick="BtnCancel_Click" Text="Cancel" CssClass="button" />
                    </td>
                       <td class="style19">
                <asp:Button ID="btnDownload0" runat="server" Enabled="false" 
                    OnClick="btnDownload_Click1" Text="Download"   CssClass="button" />
                    </td>
                       <td class="style19">
                  
<%--            </caption>--%>
             <%--   </tr>--%>
             <%--   <tr>--%>
                 <asp:Button ID="btnResetFilters" runat="server"    Text="Reset Filters" OnClick="btnResetFilters_Click" CssClass="button" />
                 </td>
                 <td class="style19">
        <asp:Button ID="btnDownloadPivot" Text="Download Pivot" Visible="false" Enabled="false"
            runat="server" OnClick="Button1_Click" CssClass="button" />

           </td>
           <td class="style14">
        
                   <asp:Label ID="Lblmsg" runat="server" 
                   
                   
                   
                       
                       style="font-weight: 700; float:7pt; font-family: Verdana; font-size: x-small;"></asp:Label>
                 
                  
                   
                  
            </td>

           
                </tr>
        </table>
        <table>
            <tr>
                <td class="style23">
                    Updates can be uploaded using Excel template :
                    <asp:LinkButton ID="lbBulkUpdate" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        OnClick="lbBulkUpdate_Click">Download Bulk Update Template</asp:LinkButton>
                    <asp:LinkButton ID="lbUpload" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        OnClick="lbUpload_Click">Upload</asp:LinkButton>
                </td>
                <td style="text-align: justify" colspan="6" class="style23">
                    
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fuBulkUpload" runat="server" Font-Names="Verdana" Font-Size="8pt" />
                                     <asp:Button ID="btnBulkUpload" runat="server" Font-Names="Verdana" 
                                        Font-Size="8pt" Height="21px" OnClick="btnBulkUpload_Click" OnClientClick="ShowProgress()" 
                                        Style="height: 21px" Text="Upload" />
                                </td>
                              
                                
                                

                            </tr>
<%--                            <tr>
                                <td class="style21">
                                    &nbsp;</td>
                                <td style="text-align: left" class="style6">
                                    &nbsp;</td>

                            </tr>--%>
                            
                        </table>
                    
                </td>
          
            </tr>

        </table>
        <asp:Label ID="lblChkBoxErrorMsg" runat="server" Text="Only one row can be updated at a time , for bulk update use the Download Bulk Update Template button."></asp:Label>
<asp:GridView ID="gvExpenseRec" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CssClass="grid"
            Font-Names="Verdana" Font-Size="8pt" margin-top="0px" 
            OnPageIndexChanging="gvExpenseRec_ExceptionPageIndexChanging" 
            OnRowDataBound="gvExpenseRec_ExceptionRowDataBound" PageSize="15" 
            RowStyle-Font-Names="Tahoma" RowStyle-Font-Size="8" ShowFooter="True" 
            Width="100%" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <PagerStyle BackColor="#F0F0F0" ForeColor="#333333" HorizontalAlign="Left" 
                VerticalAlign="Bottom" />
            <RowStyle HorizontalAlign="Justify" 
                VerticalAlign="Top" Height="10px" BackColor="#FFFBD6" 
                ForeColor="#333333" />
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
                <asp:TemplateField AccessibleHeaderText="Mark for Edit " 
                    HeaderText="Mark for Edit">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbEdit" runat="server" AutoPostBack="true" 
                            Font-Names="Verdana" Font-Size="8pt" OnCheckedChanged="chkChange_Click" />
                    </ItemTemplate>
                    <HeaderStyle />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Active Project Code" 
                    HeaderText="Active Project Code">
                    <ItemTemplate>
                        <asp:Label ID="lblActiveProjectCode" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="CustomerCode" 
                    HeaderText="Customer Code">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerCode" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="AmntUSD" HeaderText="Amount in USD">
                    <ItemTemplate>
                        <asp:Label ID="lblAmntUSD" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExpenseClosure Date" InsertVisible="False" 
                    ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" 
                    ItemStyle-Wrap="False" SortExpression="SubCategory">
                    <ItemTemplate>
                        <asp:Label ID="lblExpenseClosureDate" runat="server"></asp:Label>
                        <%--        Text='<%# Eval("[BillCommitmntdt]","{0:d}") %>'--%>
                        <asp:ImageButton ID="btnDate" runat="Server" CssClass="menuTop" 
                            Font-Names="Verdana" Font-Size="8pt" Height="15px" ImageAlign="TextTop" 
                            ImageUrl="~/img/calendar.gif" OnClick="btnDate_Click" Text="" Width="15px" />
                        <br />
                        <asp:Calendar ID="CalExpenseClosureDate" runat="server" BackColor="White" 
                            BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                            Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                            OnSelectionChanged="CalExpenseClosureDate_SelectionChanged" Visible="False" 
                            Width="200px">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                        </asp:Calendar>
                        <asp:TextBox ID="txtCal" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValDate" runat="server" 
                            ControlToValidate="txtCal" ErrorMessage="Please select a date." Visible="false"></asp:RequiredFieldValidator>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expense/Reversal Initiated Date" 
                    InsertVisible="False" ItemStyle-HorizontalAlign="NotSet" 
                    ItemStyle-VerticalAlign="Top" ItemStyle-Wrap="false" 
                    SortExpression="SubCategory">
                    <HeaderStyle />
                    <ItemTemplate>
                        <asp:Label ID="lblInitatedDate" runat="server"></asp:Label>
                        <%--        Text='<%# Eval("[BillCommitmntdt]","{0:d}") %>'--%>
                        <asp:ImageButton ID="btnInitatedDate" runat="Server" CssClass="menuTop" 
                            Font-Names="Verdana" Font-Size="8pt" Height="15px" ImageAlign="TextTop" 
                            ImageUrl="~/img/calendar.gif" OnClick="btnInitatedDate_Click" Text="v" 
                            Width="15px" />
                        <br />
                        <asp:Calendar ID="CalInitatedDate" runat="server" BackColor="White" 
                            BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                            Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                            OnSelectionChanged="CalInitatedDate_SelectionChanged" Visible="False" 
                            Width="200px">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                        </asp:Calendar>
                        <asp:TextBox ID="txtCalInitated" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValDate1" runat="server" 
                            ControlToValidate="txtCalInitated" ErrorMessage="Please select a date." 
                            Visible="false"></asp:RequiredFieldValidator>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="150px" HeaderText="Category" 
                    InsertVisible="False" ItemStyle-VerticalAlign="Top" 
                    SortExpression="SubCategory">
                    <ControlStyle Width="200px" />
                    <HeaderStyle />
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" 
                            Visible="<%# IsInEditMode %>">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="200px" HeaderText="Status" 
                    InsertVisible="False" ItemStyle-HorizontalAlign="Center" 
                    ItemStyle-VerticalAlign="Top" SortExpression="SubSubCategory">
                    <HeaderStyle />
                    <ItemTemplate>
                        <asp:Label ID="lblSubCategory" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" 
                            Visible="<%# IsInEditMode %>">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ControlStyle Width="200px" />
                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Comments" InsertVisible="False" >
                            <HeaderStyle />
                           
                            <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Visible='<%# !(bool) IsInEditMode %>'  ></asp:Label>
                                <asp:TextBox ID="TxtComments" runat="server" Visible='<%# IsInEditMode %>' ></asp:TextBox>
                                  
                            </ItemTemplate>
                        </asp:TemplateField--%>
                <asp:TemplateField ControlStyle-Width="100px" 
                    HeaderText="Update from Account team" InsertVisible="False" 
                    SortExpression="SubCategory" Visible="false">
                    <ControlStyle Width="100px" />
                    <HeaderStyle />
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlUpdatefromAccountteam" runat="server" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUpdatefromAccountteam" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlUpdatefromAccountteam" runat="server" 
                            AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlUpdatefromAccountteam_SelectedIndexChanged">
                            <%--<asp:ListItem Text="Already Invoiced" Value="Already Invoiced" />
                                    <asp:ListItem Text="Will be Billed in the Current Month" Value="Will be Billed in the Current Month" />
                                    <asp:ListItem Text="Will be Billed in Current Quarter" Value="Will be Billed in Current Quarter" />
                                    <asp:ListItem Text="Will be Billed in Next Quarter" Value="Will be Billed in Next Quarter" />
                                    <asp:ListItem Text="To be reversed -  DM Approval Obtained" Value="To be reversed -  DM Approval Obtained" />
                                    <asp:ListItem Text="To be reversed -  DM Approval pending" Value="To be reversed -  DM Approval pending" />--%>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="100px" 
                    HeaderText="Invoice/Confirmation No" InsertVisible="False" 
                    ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" 
                    SortExpression="Invoice No">
                    <ControlStyle Width="100px" />
                    <HeaderStyle />
                    <ItemStyle VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblInvoiceNo" runat="server" Visible="true"></asp:Label>
                        <asp:TextBox ID="TxtInvoiceNo" runat="server" Visible="<%# IsInEditMode %>"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="TxtInvoiceNo" Enabled="false" ErrorMessage="Enter Invoice" 
                            Visible="<%# IsInEditMode %>"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="TxtInvoiceNo" Enabled="false" ErrorMessage="Enter Valid No." 
                            ValidationExpression="[0-9]+" Visible="<%# IsInEditMode %>"></asp:RegularExpressionValidator>
                    </ItemTemplate>
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
                <asp:TemplateField ControlStyle-Width="80px" HeaderText="AHD Req No" 
                    InsertVisible="False" ItemStyle-VerticalAlign="Top" 
                    SortExpression="Serv Centrale Req No">
                    <ControlStyle Width="80px" />
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblServCentraleReqNo" runat="server" 
                            Visible="<%# !(bool) IsInEditMode %>"></asp:Label>
                        <asp:TextBox ID="TxtServCentraleReqNo" runat="server" 
                            Visible="<%# IsInEditMode %>"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="200px" HeaderText="Remarks" 
                    InsertVisible="False" ItemStyle-HorizontalAlign="Center" 
                    ItemStyle-VerticalAlign="Top" SortExpression="Remarks">
                    <ControlStyle Width="200px" />
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Visible="<%# !(bool) IsInEditMode %>"></asp:Label>
                        <asp:TextBox ID="TxtRemarks" runat="server" Visible="<%# IsInEditMode %>"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="TxtRemarks" Enabled="false" 
                            ErrorMessage="Please provide your comments."></asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Client IBU" ControlStyle-Width="100px" 
                    HeaderText="Client IBU ">
                    <ItemTemplate>
                        <asp:Label ID="lblClientIBU" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
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
                <asp:TemplateField AccessibleHeaderText="Profit Center" 
                    HeaderText="Profit Center">
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
                <asp:TemplateField AccessibleHeaderText="Document Number" 
                    HeaderText="Document Number">
                    <ItemTemplate>
                        <asp:Label ID="lblDocumentNumber" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="LocalCurrency" 
                    HeaderText="Local Currency">
                    <ItemTemplate>
                        <asp:Label ID="lblLocalCurrency" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Amount in Local Currency" 
                    HeaderText="Amount in Local Currency">
                    <ItemTemplate>
                        <asp:Label ID="lblAmntLC" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Posting Date" 
                    HeaderText="Posting Date">
                    <ItemTemplate>
                        <asp:Label ID="lblPostingDate" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Text" ControlStyle-Width="200px" 
                    HeaderText="Text">
                    <ItemTemplate>
                        <asp:Label ID="lblText" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="200px" />
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Width="200px" Wrap="true" />
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
                <asp:TemplateField AccessibleHeaderText="Ageing Bucket" 
                    HeaderText="Ageing Bucket" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAgeingBucket" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Contract Type" 
                    HeaderText="Contract Type">
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
                <asp:TemplateField AccessibleHeaderText="G/L Acct Long Text" 
                    ControlStyle-Width="150px" HeaderText="G/L Acct Long Text">
                    <ItemTemplate>
                        <asp:Label ID="lblGLAcctLongText" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="150px" />
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Width="50px" Wrap="true" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Update" HeaderText="Update">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdate" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="LastUpdatedBY" 
                    HeaderText="LastUpdatedBY">
                    <ItemTemplate>
                        <asp:Label ID="lblLastUpdatedBY" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="LastUpdatedOn" 
                    HeaderText="LastUpdatedOn">
                    <ItemTemplate>
                        <asp:Label ID="lblLastUpdatedOn" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="ResponseFromFinance" 
                    HeaderText="ResponseFromFinance">
                    <ItemTemplate>
                        <asp:Label ID="lblResponseFromFinance" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Unique" HeaderText="Unique" 
                    Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblUnique" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="SubCategory_Old" 
                    HeaderText="SubCategory_Old" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblSubCategory_Old" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           
            <FooterStyle BorderStyle="Solid" BackColor="#F0F0F0" BorderColor="Black" 
                BorderWidth="1px" Font-Bold="True" ForeColor="White" />
           
            <HeaderStyle 
                HorizontalAlign="Center" VerticalAlign="Top" BackColor="#990000" 
                Font-Names="Verdana" BorderColor="Black" BorderStyle="Solid" 
                BorderWidth="1px" Font-Bold="True" ForeColor="White" />
           
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <br />
        <br />

               <asp:GridView ID="gvDownloadData" runat="server" CellPadding="4" Visible="False"
            OnRowDataBound="gvExpenseRec_ExceptionRowDataBound" ForeColor="#333333" GridLines="None">
            <RowStyle ForeColor="#333333" BackColor="#FFFBD6" />
            
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle  Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>

</ContentTemplate>

<Triggers>

<asp:PostBackTrigger ControlID="btnBulkUpload" />

</Triggers>

</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">

<ProgressTemplate>

<div class="overlay">

<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">

<img alt="" src="img/loader.gif" style="width: 150px; height: 150px" />

</div>

</div>

</ProgressTemplate>

</asp:UpdateProgress>



</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style type="text/css">
            .grid

    {

        width:100%;
        font-size: small;
    }

    .grid th

    {

        background-color:#f0f0f0;

        color:Black;
        padding:5px;
        border:1px solid black;

    }
      .grid tr:nth-child(even)

    {

        background-color:#FFFBD6;

    }

    .grid tr:nth-child(odd)

    {

        background-color:#FFFFFF;

    }
    .expensebilled
    {
        background-color:black;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr><td style="width: 10%"></td>
<td>
    <br />
    <br />

<asp:Label Text="Please refer to below table for the allowed Category, their coresponding Status and the mandatory fields to be entered" 
        runat="server" 
        style="font-family: Verdana; font-size: small;"></asp:Label>
    &nbsp;:<br />
    &nbsp;<asp:GridView ID="GridView1" runat="server" CellPadding="4" 
        Font-Names="Verdana" Font-Size="8pt" CssClass="grid" 
        
        AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" 
        onrowdatabound="GridView1_RowDataBound">
        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BorderColor="Black" BorderStyle="None" Font-Size="8pt" 
            ForeColor="#333333" BackColor="#FFFBD6" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
           <asp:BoundField DataField="Category" HeaderText="Category" />
                <asp:BoundField DataField="SubCategory" HeaderText="Status" />
                <asp:BoundField DataField="MandatoryFields" HeaderText="Mandatory Fields" /></Columns>
    </asp:GridView>
  
    </td></tr>
</table>

</asp:Content>


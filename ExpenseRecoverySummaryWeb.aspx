<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpenseRecoverySummaryWeb.aspx.cs" Inherits="ExpenseRecoverySummaryWeb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            generateTable();

        }
        );


        function generateTable() {

            var itemsJSONString = document.getElementById('hdnfldData').value;
            var itemsJSONOBJ = undefined;
            if (itemsJSONString != '' && itemsJSONString != undefined)
                itemsJSONOBJ = JSON.parse(itemsJSONString)
            var items = itemsJSONOBJ;
            //console.log(itemsJSONString);



            //for sl

            var itemsJSONString_1 = document.getElementById('hdnfldData_1').value;
            var itemsJSONOBJ_1 = undefined;
            if (itemsJSONString_1 != '' && itemsJSONString_1 != undefined)
                itemsJSONOBJ_1 = JSON.parse(itemsJSONString_1)
            var items_1 = itemsJSONOBJ_1;
            //ends here


            //table creation
            //for the first row th
            var noOfItems_1 = itemsJSONOBJ_1.length;
            var theader_sl = document.getElementById("tblSummaryPage_tr");
            var theader_sl_hidden = document.getElementById('tblSummaryPage_trHidden');
            if (noOfItems_1 > 0) {
                for (var k = 0; k < noOfItems_1; k++) {

                    var th_1 = document.createElement("td");
                    th_1.innerHTML = (itemsJSONOBJ_1[k].PracticeLine).trim();
                    th_1.colSpan = 8;
                    theader_sl.appendChild(th_1);

                    for (var i = 0; i < 8; i++) {
                        var th_hidden = document.createElement("td");
                        th_hidden.innerHTML = (itemsJSONOBJ_1[k].PracticeLine).trim();
                        theader_sl_hidden.appendChild(th_hidden);
                    }
                }
            }


            //first row creation done  


            var noOfItems = itemsJSONOBJ.length;
            if (noOfItems > 0) {


                var col = [];
                for (var i = 0; i < noOfItems; i++) {
                    for (var key in itemsJSONOBJ[i]) {
                        if (col.indexOf(key) === -1) {

                            col.push(key);

                        }
                    }
                }


                for (var i = 0; i < col.length; i++) {
                    if (col[i] != "Group" && col[i] !== "Category" && col[i] !== "SubCategory") {
                        hRow = document.getElementById("tblSummaryPage_tr_1")
                        var th = document.createElement("td");

                        var var_header = RenameHeader(col[i]);

                        th.innerHTML = var_header;
                        hRow.appendChild(th);
                    }

                }
            }




            for (var i = 0; i < noOfItems; i++) {

                var bRow = document.createElement("tr");


                for (var j = 0; j < col.length; j++) {
                    var td = document.createElement("td");
                    td.innerHTML = itemsJSONOBJ[i][col[j]];
                    bRow.appendChild(td);

                }
                tBody = document.getElementById("id_body");
                tBody.appendChild(bRow)

            }

            //table creation ends
            colourcode()
            MouseHoverFunction()
            URLRedirectionFunction()
        }

        function MouseHoverFunction() {
            $("td").mouseenter(function () {
                var a = this.innerText;
                // var col1_1 = $('td', this.parentElement).eq(0)[0];
                var col1 = $('td', this.parentElement).eq(0)[0].innerText;
                var col2 = $('td', this.parentElement).eq(1)[0].innerText;
                var col3 = $('td', this.parentElement).eq(2)[0].innerText;
                var columnIndex = $('td', this.parentElement).index(this);


                if (columnIndex == 0 && col1 != 'Total' && col1 != "Group") { this.style.cursor = 'pointer'; }
                else if (columnIndex == 0 && col1 == 'Total') { this.style.cursor = 'pointer'; }
                else if (columnIndex == 1 && col2 != "Not Billable" && col1 != 'Total') { this.style.cursor = 'pointer'; }
                else if (columnIndex == 2 && col2 != "Not Billable" && col1 != 'Total') { this.style.cursor = 'pointer'; }
                else if (columnIndex > 2 && a != 0 && col2 != "Not Billable") { this.style.cursor = 'pointer'; }

            });
        }

        function URLRedirectionFunction() {
            $("td").click(function () {
                debugger

                var a = this.innerText;
                // var col1_1 = $('td', this.parentElement).eq(0)[0];
                var col1 = $('td', this.parentElement).eq(0)[0].innerText;
                var col2 = $('td', this.parentElement).eq(1)[0].innerText;
                var col3 = $('td', this.parentElement).eq(2)[0].innerText;
                var columnIndex = $('td', this.parentElement).index(this);
                var bucket = $('td', '#tblSummaryPage_tr_1').eq($('td', this.parentElement).index(this) - 3)[0].innerText; // this needs to be corrected like below 
                var practice = $('td', '#tblSummaryPage_trHidden').eq($('td', this.parentElement).index(this))[0].innerText;

                //console.log(col1_1)
                console.log(col1)
                console.log(col2)
                console.log(col3)
                console.log(columnIndex);
                console.log(practice);

                var value = this.innerText;
                console.log(value);

                console.log(bucket)


                if (columnIndex == 0 && col1 != 'Total' && col1 != "Group") {
                    window.parent.location.href = "ExpenseRecovery.aspx?category=" + col1 + "&subcategory=ALL&ageingbucket=ALL&practiceline=ALL";
                }
                else if (columnIndex == 0 && col1 == 'Total') {
                    window.parent.location.href = "ExpenseRecovery.aspx?category=ALL&subcategory=ALL&ageingbucket=ALL&practiceline=ALL";
                }
                else if (columnIndex == 1 && col2 != "Not Billable" && col1 != 'Total') {
                    window.parent.location.href = "ExpenseRecovery.aspx?category=" + col2 + "&subcategory=ALL&ageingbucket=ALL&practiceline=ALL";
                }
                else if (columnIndex == 2 && col2 != "Not Billable" && col1 != 'Total') {
                    window.parent.location.href = "ExpenseRecovery.aspx?category=" + col2
                        + "&subcategory=" + col3 + "&ageingbucket=ALL&practiceline=ALL";
                }

                else if (columnIndex > 2 && bucket != "Total" && value != 0 && col2 != "Not Billable") {
                    var bucket_passValue;
                    var sl_passValue;
                    if (bucket.indexOf("yr") > -1) { bucket_passValue = bucket; } else { bucket_passValue = bucket.replace("<=", "< ") + " Days"; }
                    if (practice.indexOf("EAS") > -1) { sl_passValue = "ALL"; } else { sl_passValue = practice; }

                    if (col1 == 'Total') {
                        window.parent.location.href = "ExpenseRecovery.aspx?category=ALL&subcategory=ALL&ageingbucket=" + bucket_passValue + "&practiceline=" + sl_passValue;
                    }

                    else {
                        window.parent.location.href = "ExpenseRecovery.aspx?category=" + col2 + "&subcategory=" + col3 + "&ageingbucket=" + bucket_passValue + "&practiceline=" + sl_passValue;
                    }

                    console.log(bucket_passValue)
                }

                else if (columnIndex > 2 && bucket == "Total" && value != 0 && col2 != "Not Billable") {
                    var sl_passValue;
                    if (practice.indexOf("EAS") > -1) { sl_passValue = "ALL"; } else { sl_passValue = practice; }

                    if (col1 == 'Total') {
                        window.parent.location.href = "ExpenseRecovery.aspx?category=ALL&subcategory=ALL&ageingbucket=ALL&practiceline=" + sl_passValue;

                    }
                    else {
                        window.parent.location.href = "ExpenseRecovery.aspx?category=" + col2 + "&subcategory=" + col3 + "&ageingbucket=ALL&practiceline" + sl_passValue;

                    }

                }


            });

        }

        function RenameHeader(headerVal) {
            if (headerVal.indexOf('30') > -1) { var_header = "<=30"; }
            else if (headerVal.indexOf('31-60') > -1) { var_header = "31-60" }
            else if (headerVal.indexOf('61-90') > -1) { var_header = "61-90"; }
            else if (headerVal.indexOf('91-180') > -1) { var_header = "91-180"; }
            else if (headerVal.indexOf('181-365') > -1) { var_header = "181-365"; }
            else if (headerVal.indexOf('>2yr') > -1) { var_header = "> 2yr"; }
            else if (headerVal.indexOf('1yr-2yr') > -1) { var_header = "1yr-2yr"; }
            //else if (headerVal.includes('Total')) { var_header = "Total"; }
            else if (headerVal.indexOf('Total') > -1) { var_header = "Total"; }
            else { var_header = headerVal; }
            return var_header;
        }

        function colourcode() {



            var columnindex = $('td:contains("Group")').index();
            if (columnindex != -1) {
                $('tbody tr').each(function () {
                    var column = $('td', this).eq(columnindex);
                    column.css({ backgroundColor: 'LightGrey' });
                    var column_text = $('tbody tr:contains("Update Needed From PM/DMs")');
                    column_text.css({ color: 'Red' });
                });
            }


            var columnindex_1 = $('td:contains("Category")').index();
            if (columnindex_1 != -1) {
                $('tbody tr').each(function () {
                    var column_1 = $('td', this).eq(columnindex_1);
                    column_1.css({ backgroundColor: 'LightGrey' });
                });
            }

            var columnindex_1 = $('td:contains("Status")').index();
            if (columnindex_1 != -1) {
                $('tbody tr').each(function () {

                    var column_1 = $('tbody tr:contains("Expense confirmation already raised")');
                    column_1.css({ backgroundColor: '#F8CBAD' });

                    var column_1 = $('tbody tr:contains("Pending with Finance for activity code creation, approval, etc.")');
                    column_1.css({ backgroundColor: '#F8CBAD' });

                    var column_1 = $('tbody tr:contains("Reversal Pending with Finance")');
                    column_1.css({ backgroundColor: '#F8CBAD' });

                    var column_1 = $('tbody tr:contains("Pending client sign-off")');
                    column_1.css({ backgroundColor: '#c9eeee' });

                    var column_1 = $('tbody tr:contains("Pending with delivery - PM/DM")');
                    column_1.css({ backgroundColor: '#c9eeee' });

                    var column_1 = $('tbody tr:contains("Reversal Pending with PM/DM")');
                    column_1.css({ backgroundColor: '#c9eeee' });

                    var column_1 = $('tbody tr:contains("Update Pending")');
                    column_1.css({ backgroundColor: '#c9eeee' });

                    var column_1 = $('tbody tr:contains("Pending with IS - system issues")');
                    column_1.css({ backgroundColor: 'e3c7ff' });

                    var column_1 = $('tbody tr:contains("Expenses_Billed as part of Billable Rate")');
                    column_1.css({ backgroundColor: '#CECE66' });

                    var column_1 = $('tbody tr:contains("Expenses_Billed as part of contract value")');
                    column_1.css({ backgroundColor: '#CECE66' });

                    var column_1 = $('tbody tr:contains("Expenses_Incorrectly marked as customer rec")');
                    column_1.css({ backgroundColor: '#CECE66' });

                    var column_1 = $('tbody tr:contains("Expenses_Not billable as per PO/SOW/MSA")');
                    column_1.css({ backgroundColor: '#CECE66' });

                    var column_1 = $('tbody tr:contains("Expenses_Already Billed separately as fixed amount")');
                    column_1.css({ backgroundColor: '#CECE66' });

                    var column_1 = $('tbody tr:contains("Expenses_Old claims not billed to Customer")');
                    column_1.css({ backgroundColor: '#CECE66' });

                    var column_1 = $('tbody tr:contains("Expenses_Already Billed under Diff project code")');
                    column_1.css({ backgroundColor: '#CECE66' });


                    var column_1 = $('tbody tr:contains("Other")');
                    column_1.css({ backgroundColor: '#CECE66' });



                });
            }
            $('#id_body > tr:last-child >td').css({ 'background-color': 'dimgray', 'color': 'white' });
            $('#id_body tr > td').each(function (index, elem) {

                
                var fltValue = parseFloat(elem.innerText);
                if (isNaN(fltValue))
                    elem.style.textAlign = "left";
                else {
                    // Numbers
                    elem.style.textAlign = "right";
                    elem.innerText = fltValue.toLocaleString();

                }

            });

            $('#id_body tr td').each(function (index, elem) {
                elem.style.border = '1px solid black';
            });
            $('#id_body tr td:nth-child(1)').each(function (index, elem) {
                elem.style.border = '';
                elem.style.borderRight ='1px solid black';
            });
             $('#id_body tr td:nth-child(2)').each(function (index, elem) {
                 elem.style.border = '';                  
            });

            // Grouping First Column
            var __prev1 = ''
            $("#id_body tr td:nth-child(1)").each(function (index, elem) {
                if (__prev1 == elem.innerText) {                   
                    elem.style.color = 'lightgrey';
                }
                else {
                    __prev1 = elem.innerText;
                    elem.style.borderTop = '1px solid black'; 
                }
            });
            // Grouping Second Column
            var __prev2 = ''
            $("#id_body tr td:nth-child(2)").each(function (index, elem) {
                 if (__prev2 == elem.innerText) {                   
                    elem.style.color = 'lightgrey';
                }
                else {
                    __prev2 = elem.innerText;
                    elem.style.borderTop = '1px solid black'; 
                }
            });

            //groupTable($('#id_body tr:has(td)'), 0, 2);
            //$('#id_body .deleted').remove();


        }

        function groupTable($rows, startIndex, total) {
            if (total === 0) {
                return;
            }
            var i, currentIndex = startIndex, count = 1, lst = [];
            var tds = $rows.find('td:eq(' + currentIndex + ')');
            var ctrl = $(tds[0]);
            lst.push($rows[0]);
            for (i = 1; i <= tds.length; i++) {
                if (ctrl.text() == $(tds[i]).text()) {
                    count++;
                    $(tds[i]).addClass('deleted');
                    lst.push($rows[i]);
                }
                else {
                    if (count > 1) {
                        ctrl.attr('rowspan', count);
                        groupTable($(lst), startIndex + 1, total - 1)
                    }
                    count = 1;
                    lst = [];
                    ctrl = $(tds[i]);
                    lst.push($rows[i]);
                }
            }
        }



    </script>
    <style type="text/css">
        /*th{
            
            background-color:dimgray;
            

        }*/

        #id_body td {
            /*padding: .75rem;*/
            vertical-align: top;
            /*border: 1px solid black !important;*/
            border-collapse: collapse !important;
        }

        table {
            border-collapse: collapse;
        }



        #id_body {
            padding: 0.25rem;
            vertical-align: top;
            border-top: 1px solid #dee2e6;
            white-space: nowrap;
            text-align: left;
            direction: ltr;
            font-family: Calibri;
            font-size: 10pt;
            font-style: normal;
            font-weight: 400;
            border-collapse: collapse;
        }

        #id_thead {
            vertical-align: middle;
            text-align: center;
            direction: ltr;
            border: 1pt solid black;
            background-color: DimGray;
            color: white;
            font-family: Calibri;
            font-size: 10pt;
            height: 40px;
            white-space: nowrap;
            /*width:100px;*/
        }

        .box1 {
            box-sizing: border-box;
            width: 12px;
            height: 12px;
            /*padding: 1px;*/
            border: 1px solid gray;
        }

        .table {
            font-size: 10pt;
            border: 1px solid black;
        }

        .tab {
            padding-left: 35px;
        }

        #tblSummaryPage_tr > td {
            border: 1px solid black;
        }

        #tblSummaryPage_tr_1 > td {
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Calibri; font-size: 12pt; color: red; text-align: left">
            <b>Please click on the numbers below to provide your inputs</b>
        </div>
        <div style="height: 5px"></div>
        <div>
            <div style="font-family: Calibri; font-size: 12pt; color: dimgray; text-align: left">
                <b>Expense Amount(in USD) By Ageing Bucket(in Days)</b><span class="tab"></span>

                <span style="display: inline-block; background-color: lightcoral" class="box1"></span><span style="font-size: 11px; font-family: Arial">&nbsp;Pending with Finance</span>

                <span style="display: inline-block; background-color: #c9eeee" class="box1"></span><span style="font-size: 11px; font-family: Arial">&nbsp;Pending with Delivery</span>

                <span style="display: inline-block; background-color: thistle" class="box1"></span><span style="font-size: 11px; font-family: Arial">&nbsp;Pending with IS</span>

                <span style="display: inline-block; background-color: #CECE66" class="box1"></span><span style="font-size: 11px; font-family: Arial">&nbsp;Workflow Already Initiated</span>


            </div>
        </div>
        <div>
            <div style="height: 5px"></div>
            <%-- &nbsp<p  id="box1">Pending with Finance</p>--%>
            <table class="table" style="width: 100%; border-style: solid" id="tblSummaryPage">
                <thead id="id_thead">
                    <tr id="tblSummaryPage_trHidden" style="display: none">
                        <td rowspan="1">ALL</td>
                        <td rowspan="1">ALL</td>
                        <td rowspan="1">ALL</td>
                    </tr>
                    <tr id="tblSummaryPage_tr">
                        <td rowspan="2">Group</td>
                        <td rowspan="2">Category</td>
                        <td rowspan="2">Status</td>
                    </tr>
                    <tr id="tblSummaryPage_tr_1"></tr>
                </thead>
                <tbody id="id_body">
                </tbody>
            </table>
        </div>
        <asp:HiddenField ID="hdnfldData" runat="server" />
        <asp:HiddenField ID="hdnfldData_1" runat="server" />
    </form>
</body>
</html>

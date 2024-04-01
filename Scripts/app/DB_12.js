//global variables 
/// <reference path="Entities.js" />

var gridDDLItems = [];
var totalRowCount = 0;
var countPerPage = 15;
var disabledPage = [];
var gridData = [];

$(function () {
    //debugger;
    preReqLoad();
    var val = 0;
    buildGridMain(val);
});



function getInputValues(val) {
    var value;

    //debugger;
    var Ageingbucket = getQueryString("ageingbucket")
    var billingCategory;
    var BillingStatus;
    var PracticeLine;
    if (Ageingbucket != "") {

        billingCategory = getQueryString("category");
        BillingStatus = getQueryString("subcategory");
        PracticeLine = getQueryString("practiceline");
        if (BillingStatus == "No Response") {
            BillingStatus = "";
        }

        var tab = '#menu1';
        $('#myTabs a[href="' + tab + '"]').tab('show');
    }
    else {
        Ageingbucket = "All";
        billingCategory = "All";
        BillingStatus = "All";
        PracticeLine = "All";

    }

    if (val == "1") {

        billingCategory = $.trim($('[id$="ddlSubCategory_Top"]  option:selected').val());

        if (billingCategory == "-1") {
            BillingStatus = "ALL";
            billingCategory = "ALL";
        }
        else if (billingCategory == "") {

        }
        //            else {
        //                if (billingCategory != "Not Billable" && billingCategory != "Already Billed") {
        //                    BillingStatus = "Billable";
        //                }
        //                else {
        //                    BillingStatus = $.trim($('[id$="ddlSubCategory_Top"]  option:selected').val());
        //                }
        //            }

        Ageingbucket = $.trim($('[id$="ddlAgeingBucket"]  option:selected').val()) == "-1" ? "ALL" : $.trim($('[id$="ddlAgeingBucket"]  option:selected').val())

        var i = $.trim($('[id$="ddlSubCategory_Top"]  option:selected').val());

        if (i == "") {
            BillingStatus = "";
        }


        value = {

            customerCode: $.trim($('[id$="ddlCustomerCode"] option:selected').val()),
            PM: $.trim($('[id$="ddlPM"]  option:selected').val()) == "-1" ? "All" : $.trim($('[id$="ddlPM"]  option:selected').val()),
            DM: $.trim($('[id$="ddlDM"]  option:selected').val()) == "-1" ? "All" : $.trim($('[id$="ddlDM"]  option:selected').val()),
            glLongText: $.trim($('[id$="ddlLongText"]  option:selected').val()),
            profitCentre: $.trim($('[id$="ddlProfitCentre"]  option:selected').val()),
            PracticeLine: $.trim($('[id$="ddlPracticeLine"]  option:selected').val()),
            category: $.trim($('[id$="ddlCategory_Top"]  option:selected').val()),
            status: $.trim($('[id$="ddlSubCategory_Top"]  option:selected').val()),
            Commitment: $.trim($('[id$="ddlCommitments"]  option:selected').val()),
            MailID: "",
            role: "",
            Ageingbucket: $.trim($('[id$="ddlAgeingBucket"]  option:selected').val()) == "-1" ? "All" : $.trim($('[id$="ddlAgeingBucket"]  option:selected').val()),
            //                Report_BillingCategory: billingCategory ==""?"ALL":billingCategory,
            //                Report_BillingStatus: BillingStatus ==""?"ALL":BillingStatus,
            //                Report_AgeingBucket: Ageingbucket,
            //                Report_PracticeLine: PracticeLine ==""?"EAS":PracticeLine,
            pageno: 1,
            pagesize: 15

        };


        //            alert(BillingStatus);
        //            alert(billingCategory);
        //            alert(BillingStatus == "" ? "ALL" : BillingStatus);
    }
    else if (val == "2") {
        value = {
            customerCode: "All",
            Ageingbucket: "All",
            glLongText: "All",
            PM: "All",
            DM: "All",
            profitCentre: "All",
            category: "All",
            status: "All",
            PracticeLine: "All",
            MailID: "",
            role: "",
            Commitment: "All",
            //Report_BillingCategory: billingCategory == "" ? "ALL" : billingCategory,
            //Report_BillingStatus: BillingStatus == "" ? "ALL" : BillingStatus,
            //Report_AgeingBucket: Ageingbucket == "" ? "ALL" : Ageingbucket,
            //Report_PracticeLine: PracticeLine == "" ? "EAS" : PracticeLine,
            pageno: 1,
            pagesize: 15
            //                customerCode_Filter: "All",
            //                PM_Filter: "All",
            //                DM_Filter: "All",
            //                glLongText_Filter: "All",
            //                ProfitCentre_Filter: "All",
            //                PracticeLine_Filter: "All",
            //                category_Filter: "All",
            //                Status_Filter: BillingStatus == "" ? BillingStatus : "All",
            //                Commitment: "All",
            //                MailID: "",
            //                AccessTYpe: "",
            //                ageingBucket_Filter: "All",
            //                pageno: 1,
            //                pagesize: 15


        };

    }
    else {
        value = {
            customerCode: "All",
            Ageingbucket: Ageingbucket == "" ? "All" : Ageingbucket,
            glLongText: "All",
            PM: "All",
            DM: "All",
            profitCentre: "All",
            category: billingCategory == "" ? "All" : billingCategory,
            status: BillingStatus == "" ? "All" : BillingStatus,
            PracticeLine: PracticeLine == "" ? "All" : PracticeLine,
            MailID: "",
            role: "",
            Commitment: "All",
            //Report_BillingCategory: billingCategory == "" ? "ALL" : billingCategory,
            //Report_BillingStatus: BillingStatus == "" ? "ALL" : BillingStatus,
            //Report_AgeingBucket: Ageingbucket == "" ? "ALL" : Ageingbucket,
            //Report_PracticeLine: PracticeLine == "" ? "EAS" : PracticeLine,
            pageno: 1,
            pagesize: 15
            //                customerCode_Filter: "All",
            //                PM_Filter: "All",
            //                DM_Filter: "All",
            //                glLongText_Filter: "All",
            //                ProfitCentre_Filter: "All",
            //                PracticeLine_Filter: "All",
            //                category_Filter: "All",
            //                Status_Filter: BillingStatus == "" ? BillingStatus : "All",
            //                Commitment: "All",
            //                MailID: "",
            //                AccessTYpe: "",
            //                ageingBucket_Filter: "All",
            //                pageno: 1,
            //                pagesize: 15


        };

    }
    //alert(PracticeLine + "-" + Ageingbucket + "-" + BillingStatus + "-" + billingCategory);
    //debugger;
    return value;
}

function getQueryString(key) {
    // debugger;
    return decodeURIComponent(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + encodeURIComponent(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));
}

function buildGridMain(val) {
    // debugger;
    //$('#lblLoad').show();
    $('#imgloading2').show();


    setTimeout(function () {
        //debugger;
        //$('#lblLoad').hide();
        $('#imgloading2').show();
        var value = getInputValues(val);

        var obj = GetInitalCount(value);
        var totalRowCount = obj.Count;
        //$('.delDrp1').html(obj.DBUSD);
        if (totalRowCount == 0) {
            $('#btnSave').hide();
            $('#btnDownload').hide();
            $('.gridportion').hide();
            $('#imgloading2').hide();
            alert("No Data Found!!");
        }
        else {
            //debugger;
            $('#btnSave').show();
            $('#btnDownload').show();
            var totalPages = Math.ceil(totalRowCount / countPerPage);
            $('#pagination-demo').twbsPagination('destroy');
            $('#pagination-demo').twbsPagination({
                totalPages: totalPages,
                visiblePages: 7,
                onPageClick: function (event, page) {

                    $('#imgloadingpage').show();
                    disabledPage = [];
                    $('.page-item.disabled').each(function (a, b) { disabledPage.push(b.innerText) });
                    enabledisablepage(disabledPage, 'enable');
                    value.pageno = page;




                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        url: "ExpenseRecovery.aspx/GetData",
                        data: "{'Data':'" + JSON.stringify(value) + "'}",
                        success: function (Record) {
                            //   debugger;
                            var response = JSON.parse(Record.d);
                            var items = response.Table;
                            var count = response.Table1[0].TotalCount;
                            var data = getValidGridSource(items);
                            generateHTMLTable(data);


                            enabledisablepage(disabledPage, 'disable');
                            $('#imgloading2,#imgloadingpage').hide();
                            $('#lblLoad').hide();
                            $('#tblpage').show();
                            $('.gridportion').show();


                            enabledisabledatepicker();
                        },

                        Error: function (textMsg) {
                            // debugger;
                        }
                    });

                }
            });
        }
    }, 100);
}

function GetInitalCount(value) {
    var obj = {};
    //  debugger;
    $.ajax({
        type: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "ExpenseRecovery.aspx/GetInitialData",
        data: "{'Data':'" + JSON.stringify(value) + "'}",
        success: function (Record) {
            //  debugger;
            var response = JSON.parse(Record.d);
            obj.Count = response.Count;
            obj.DBUSD = response.DBUSD;



        },

        Error: function (textMsg) {
            //   debugger;
        }
    });
    return obj;
}

function preReqLoad(value) {
    //var count = 0;
    //debugger;
    //var value = getInputValues();
    var ddlJson = $('[id$="hdnddldata"]').val();
    var response = JSON.parse(ddlJson);
    //count = response.Count;
    var data = response.Table;
    for (var i = 0; i < data.length; i++) {
        var item = new statusReason();
        item.contracttype = data[i].contracttype;
        item.status = data[i].Subcategory;
        item.category = data[i].Category;
        if (item.status == 'To Be Billed')
            item.status = 'Billable';
        gridDDLItems.push(item);
    }

}

function enabledisablepage(flag, enabledisable) {
    // debugger;
    if (enabledisable == 'disable') {
        $('.page-item:not(".active")').removeClass('disabled');
        for (var i = 0; i < flag.length; i++) {
            var item = flag[i].toLowerCase();
            item = item == 'previous' ? 'prev' : item;
            $('.page-item.' + item).addClass('disabled');
        }
    } else
        $('.page-item:not(".active")').addClass('disabled')

}


function selectall(chk) {
    // debugger;
    var flag = chk.checked;
    $('.clschk').prop('checked', flag);
}

function save() {
    debugger;
    var dataToSave = [];
    var error = '';
    var cnt = 1;
    var rows = $('#tbldb tbody tr').filter(function (index, tr) {
        return $('.clschk:checked', tr).length == 1;
    });
    if (rows.length > 0) {
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            var ctrl_status = $('.clsstatus', row);
            var ctrl_Category = $('.clsCategory', row);
            var ctrl_ExpenseClosureDate = $('.clsExpenseClosureDate', row);
            var status = ctrl_status.val();
            var category = ctrl_Category.val();
            var ClosureDate = ctrl_ExpenseClosureDate.val();
            var InitiatedDate = $('.clsInitiatedDate', row).val();
            var InvoiceNo = $('.clsInvoiceNo', row).val();
            var Remarks = $('.clsRemarks', row).val();
            var Unique = $('.clstdUnique', row).text();
            var ServReqNo = $('.clsServReqNo', row).val();
            var PM = $('.clstdPM', row).text();
            var DM = $('.clstdDM', row).text();
            var ProfitCenter = $('.clstdProfitCenter', row).text();
            var CustomerCode = $('.clstdCustomerCode', row).text();

            var obj = new saveData();
            obj.Category = category;
            obj.Status = status;
            obj.ExpenseClosureDate = ClosureDate;
            obj.InitiatedDate = InitiatedDate;
            obj.InvoiceNo = InvoiceNo;
            obj.Remarks = Remarks;
            obj.Unique = Unique;
            obj.ServReqNo = ServReqNo;
            obj.PM = PM;
            obj.DM = DM;
            obj.ProfitCenter = ProfitCenter;
            obj.CustomerCode = CustomerCode;
            dataToSave.push(obj);

            //debugger;
            if (['', undefined, '-select-'].indexOf(category) > -1) {
                //                alert('Please select the Category at row'+rows.index());
                //                ctrl_Category[0].focus();
                //                return false;
                error += cnt++ + '. Select Category at row ' + rows[i].rowIndex + '\n';
            }
            if (['', undefined, '-select-'].indexOf(status) > -1) {
                //                alert('Please select the status' + rows.index());
                //                ctrl_status[0].focus();
                //                return false;
                error += cnt++ + '. Select status at row ' + rows[i].rowIndex + '\n';
            }

            //            if ([''].indexOf(date) > -1) {
            //                alert('Please select the date');
            //                ctrl_date[0].focus();
            //                return false;
            //            }
            else {
                var ClosureDate = Helper.getDateObject(ClosureDate);
                var InitiatedDate = Helper.getDateObject(InitiatedDate);

                var today = new Date();
                if (['Expense confirmation already raised', 'Reversal Pending with Finance'].indexOf(status) > -1) {
                    if (InitiatedDate > today) {
                        //                        alert('Please enter a Past Billing Commitment Date' + rows.index());
                        //                        InitiatedDate[0].focus();
                        //                        return false;
                        error += cnt++ + '. Enter Past Expense Initiated Date at row ' + rows[i].rowIndex + '\n';
                    }
                }
                if (['Reversal Pending with PM/DM', 'Pending client sign-off', 'Pending with delivery - PM/DM', 'Pending with Finance for activity code creation, approval, etc.', 'Pending with IS - system issues'].indexOf(status) > -1) {
                    if (ClosureDate < today) {
                        //                        alert('Billing date should not be a past date' + rows.index());
                        //                        ctrl_ExpenseClosureDate[0].focus();
                        //                        return false;
                        error += cnt++ + '. Enter future Expense Closure Date at row ' + rows[i].rowIndex + '\n';
                    }
                }
            }
            if (category == 'Expense Billed') {
                if (['null', '', undefined].indexOf(InvoiceNo) > -1) { 
                    error += cnt++ + '. (Invoice No) is mandatory for [Expense billed],  at row ' + rows[i].rowIndex + '\n';
                }
            }

            if (category == 'Expense To Be Billed') {
                if (['null', '', undefined].indexOf(ClosureDate) > -1) {
                    error += cnt++ + '. (ClosureDate) is mandatory for [Expense to be billed],  at row ' + rows[i].rowIndex + '\n';
                }
            }



        }
        if (error != '') {

            var headMsg = 'Please check for the below Error(s):\n';
            alert(headMsg + error);
            return false;
        }

        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "ExpenseRecovery.aspx/Save",
            data: "{'Data':'" + JSON.stringify(dataToSave) + "'}",
            success: function (response) {
                //  debugger;
                if (response.d == "1") {
                    alert('Data updated successfully..')
                    buildGridMain('1');
                }
            },
            error: function (a) { } // debugger; }
        });

        return true;
    }
    else {
        alert('Please select a row to save');
    }



}

function generateHTMLTable(data) {
    //debugger;
    $('#tbldb tbody tr').remove();
    $('#chkHead').prop("checked", false);
    var $tbl = $('#tbldb tbody');
    var rows = [];
    var colstoignore = ['billingstatuslatest'];
    for (var i = 0; i < data.length; i++) {
        var item = data[i];
        var tds = [];
        for (var col in item) {
            if (colstoignore.indexOf(col) == -1) {
                tds.push('<td class="clstd' + col + '">');
                if (['chk'].indexOf(col) > -1) {
                    tds.push('<input type="checkbox" id="txt' + col + i + '"  class="cls' + col + '"    />');
                    tds.push('<input type="hidden" id="hdnSubCategory_Old' + i + '"  value="' + item['SubCategory_Old'] + '"    />');
                }
                else if (['ServReqNo'].indexOf(col) > -1) {
                    tds.push('<input type="text" id="txt' + col + i + '"  class="cls' + col + '" value="' + item[col] + '"  />');
                }
                else if (['InvoiceNo'].indexOf(col) > -1) {
                    tds.push('<input type="text" id="txt' + col + i + '"  class="cls' + col + '" value="' + item[col] + '"  />');
                }
                else if (['Remarks'].indexOf(col) > -1) {
                    tds.push('<textarea type="text"  id="txt' + col + i + '" class="cls' + col + '" title="' + toNotNull(item[col]) + '"  >' + toNotNull(item[col]) + '</textarea>');
                }
                else if (['Category'].indexOf(col) > -1) {
                    tds.push('<select initialvalue="' + item[col] + '" id="ddl' + col + i + '" class="cls' + col + '"  onchange="changeCategory(this)"  />');
                }
                else if (['status'].indexOf(col) > -1) {
                    tds.push('<select  initialvalue="' + item[col] + '" id="ddl' + col + i + '" class="cls' + col + '" onchange="changestatus(this)"  />');
                }
                else if (['InitiatedDate'].indexOf(col) > -1) {

                    tds.push(' <div class="clsdivInitiatedDate"> <input type="text" id="txt' + col + i + '"  class="cls' + col + ' datepicker" value="' + Helper.formatDate(item[col]) + '"  /> </ div>');
                }
                else if (['ExpenseClosureDate'].indexOf(col) > -1) {

                    tds.push(' <div class="clsdivExpenseClosureDate"> <input type="text" id="txt' + col + i + '"  class="cls' + col + ' datepicker" value="' + Helper.formatDate(item[col]) + '"  /> </ div>');
                }
                else if (['PostingDate', 'LastUpdatedOn'].indexOf(col) > -1) {
                    tds.push(Helper.formatDate((item[col]))); ;
                }
                else if (['Unique', 'SubCategory_Old'].indexOf(col) > -1) {
                    tds.push('<div style="display:none">' + item[col] + '</div>');
                }
                else {
                    tds.push((item[col]));
                }
                tds.push('</td>');
            }
        }
        var tds = tds.join('');
        rows.push('<tr>' + tds + '</tr>');
    }

    var body = rows.join('');
    $tbl.append(body);

    var trs = $('#tbldb tbody tr');
    for (var i = 0; i < trs.length; i++) {
        var row = trs[i];
        var contracttype = $('.clstdContractType', row).text();
        var Category = $('#ddlCategory' + i, row).attr('initialvalue');
        var status = $('#ddlstatus' + i, row).attr('initialvalue');

        //        var InitiatedDate = $('.clsInitiatedDate', row);
        //        var ExpenseClosureDate = $('.clsExpenseClosureDate', row);
        var SubCategory_Old = $('#hdnSubCategory_Old' + i, row).val();

        //contracttype = ['CTM'].indexOf(contracttype) > -1 ? 'FP' : contracttype;

        var items1 = Helper.getCascadingItemsFor(gridDDLItems, 'contracttype', contracttype, '', '', 'category');
        Helper.populateDDL('ddlCategory' + i, items1, '-select-');
        Helper.setDDLValue('ddlCategory' + i, Category);
        document.getElementById('ddlstatus' + i).setAttribute('Index', i);

        document.getElementById('ddlCategory' + i).setAttribute('contractType', contracttype);

        document.getElementById('ddlCategory' + i).setAttribute('ddlstatusId', 'ddlstatus' + i);
        document.getElementById('ddlCategory' + i).onchange();
        Helper.setDDLValue('ddlstatus' + i, status);
        //var ddl = $('#ddlstatus' + i);    
        if (SubCategory_Old == 'Past Commitment') {
            $('td', row).css('color', 'red');
            $('select,input', row).css('color', 'red');
        }

    }

    $(".datepicker").datepicker({
        showOn: "both",
        buttonImage: "Content/images/calendar.gif",
        buttonImageOnly: true,
        buttonText: "Select date",
        changeYear: true,
        yearRange: "2007:2027"
    });


}
function enabledisabledatepicker() {
    var trs = $('#tbldb tbody tr');
    for (var i = 0; i < trs.length; i++) {
        changestatus($('#ddlstatus' + i)[0]);
    }
}


function toNotNull(value) {

    value = value + '';
    if (value == 'null')
        value = value.replace('null', '');
    if (value == 'undefined')
        value = value.replace('undefined', '');
    return value;
}
function changeCategory(ddl) {
    //debugger;
    var value = ddl.value;
    var type = ddl.getAttribute('contractType');
    var ddlstatusId = ddl.getAttribute('ddlstatusId');
    var items2 = Helper.getCascadingItemsFor(gridDDLItems, 'contracttype', type, 'category', value, 'status');
    Helper.populateDDL(ddlstatusId, items2, '-select-');
}
function changestatus(ddl) {

    var value = ddl.value;
    var index = ddl.getAttribute('Index');

    if (value == 'Expense confirmation already raised' || value == 'Reversal Pending with Finance') {
        $('#txtExpenseClosureDate' + index).datepicker("option", "disabled", true);
        $('#txtInitiatedDate' + index).datepicker("option", "disabled", false);
    }
    else if (value == 'Reversal Pending with PM/DM' || value == 'Pending client sign-off' || value == 'Pending with delivery - PM/DM' || value == 'Pending with Finance for activity code creation, approval, etc.' || value == 'Pending with IS - system issues') {
        $('#txtExpenseClosureDate' + index).datepicker("option", "disabled", false);
        $('#txtInitiatedDate' + index).datepicker("option", "disabled", true);
    }
    else {
        $('#txtExpenseClosureDate' + index).datepicker("option", "disabled", true);
        $('#txtInitiatedDate' + index).datepicker("option", "disabled", true);
    }
}

function getValidGridSource(items) {
    //debugger;
    var data = [];

    for (var i = 0; i < items.length; i++) {
        var item = items[i];
        var obj = new ExpenseRecovery();
        obj.ActiveProjectCode = item['Active Project Code'];
        obj.Category = item["Category"];
        obj.Remarks = item["Remarks"];
        obj.StageCode = item["Stage Code"];
        obj.WithWhom = item["With Whom"];
        obj.ClientIBU = item["Client IBU"];
        obj.ProfitCenter = item["Profit Center"];
        obj.Reference = item["Reference"];
        obj.DocumentNumber = item['Document Number'];
        obj.LocalCurrency = item['Local Currency'];
        obj.PostingDate = item['Posting Date'];
        obj.Text = item['Text'];
        obj.Country = item['Country'];
        obj.AgeingDays = item["Ageing Days"];
        obj.ContractType = item['Contract Type'];
        obj.VendorCode = item['Vendor Code'];
        obj.GLAcctLongText = item['G/L Acct Long Text'];
        obj.Update = item['Update'];
        obj.LastUpdatedBY = item['LastUpdatedBY'];
        obj.LastUpdatedOn = item["LastUpdatedOn"];
        obj.ResponseFromFinance = item['ResponseFromFinance'];
        obj.CustomerCode = item['CustomerCode'];
        obj.AmntinUSD = item['Amnt in USD'];
        obj.status = item['Sub-Category'];
        obj.ExpenseClosureDate = item['ExpenseClosureDate'];
        obj.InitiatedDate = item['InitiatedDate'];
        obj.InvoiceNo = item['Invoice No'];
        obj.ServReqNo = item['Serv Centrale Req No'];
        obj.PM = item['PM'];
        obj.DM = item['DM'];
        obj.AmountinLC = item['Amount in LC'];
        obj.Unique = item['Unique'];
        obj.SubCategory_Old = item['SubCategory_Old'];
        data.push(obj);
    }
    return data;

}
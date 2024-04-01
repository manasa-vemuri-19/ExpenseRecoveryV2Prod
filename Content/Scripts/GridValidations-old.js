
var data = [
           { status: '-Select-', reason: '-Select-' },
           { status: 'Expense Billed', reason: 'Expense confirmation already raised' },
           { status: 'Expense To Be Billed', reason: 'Pending client sign-off' },
           { status: 'Expense To Be Billed', reason: 'Pending with Finance for activity code creation, approval, etc.' },
           { status: 'Expense To Be Billed', reason: 'Pending with delivery - PM/DM' },
           { status: 'Expense To Be Billed', reason: 'Pending with IS - system issues' },
           { status: 'Not Billable-Billed as part of milestone at fixed cost', reason: 'Reversal Pending with Finance' },
           { status: 'Not Billable-Billed as part of milestone at fixed cost', reason: 'Reversal Pending with PM/DM' },
           { status: 'Not Billable-Not Client Recoverable', reason: 'Reversal Pending with Finance' },
           { status: 'Not Billable-Not Client Recoverable', reason: 'Reversal Pending with PM/DM' },
           { status: 'Update Pending', reason: '' }
];

function Validations() { }


Validations.populateStatus = function () {

    var options = [], optionshtml = [];
    for (var i = 0; i < data.length; i++) {
        if (options.indexOf(data[i].status) == -1)
            options.push(data[i].status);
    }
    for (var i = 0; i < options.length; i++) {
        optionshtml.push("<option value='" + options[i] + "'>" + options[i] + "</option>");
    }
    var html = optionshtml.join('');
    document.getElementById('ddlstatus').innerHTML = html;
}
Validations.populateReason = function (status) {
    var options = [], optionshtml = [];
    for (var i = 0; i < data.length; i++) {
        if (data[i].status == status)
            if (options.indexOf(data[i].reason) == -1)
                options.push(data[i].reason);

    }
    for (var i = 0; i < options.length; i++) {
        optionshtml.push("<option value='" + options[i] + "'>" + options[i] + "</option>");
    }

    var html = optionshtml.join('');
    document.getElementById('ddlreason').innerHTML = html;
}

Validations.changeStatus = function (ddl) {
    Validations.populateReason(ddl.value);

}
Validations.setStatus = function (status) {

    document.getElementById('ddlstatus').value = status;
}
Validations.setReason = function (reason) {
    document.getElementById('ddlreason').value = reason;
}

Validations.getDateObject = function (dateStr) {
    if (dateStr == '') return undefined;
    var parts = dateStr.split('/');
    var mm = parseInt( parts[0])-1;
    var dd = parts[1];
    var yy = parts[2];
    var dateObj = new Date(yy, mm, dd);
    return dateObj;
}

Validations.save = function () {
    var status = document.getElementById('ddlstatus').value;
    var reason = document.getElementById('ddlreason').value;
    var inconf = document.getElementById('txtInvConfNo').value;
    var initDate = Validations.getDateObject( document.getElementById('txtInitDate').value);
    var closureDate =Validations.getDateObject(  document.getElementById('txtClosureDate').value);
    if (status == 'Expense Billed') {        
        if (inconf == '') {
            alert('Please enter the Invoice/Confirmation No');
            return false;
        }
    }
    if (status == 'Expense To Be Billed') {
        var validItems = ['Pending client sign-off', 'Pending with Finance for activity code creation, approval, etc.', 'Pending with delivery - PM/DM', 'Pending with IS - system issues'];
        if (validItems.indexOf(reason) == -1) {
            alert('Please select a valid item');
            return false;
        }
        if (closureDate == undefined) {
            alert('Please select the closure date');
            return false;
        }
        else {
            if (closureDate < new Date()) {
                alert('Please select a future date');
                return false;
            }
        }
        //  06/25/2017
    }

    if (status == 'Not Billable-Billed as part of milestone at fixed cost' || status == 'Not Billable-Not Client Recoverable') {
        var validItems = ['Reversal Pending with PM/DM','Reversal Pending with Finance'];
        if (validItems.indexOf(reason) == -1) {
            alert('Please select a valid item');
            return false;
        }

        if (reason == 'Reversal Pending with Finance') {
            if (initDate == undefined) {
                alert('Please select the Init date');
                return false;
            }
            else if (initDate > new Date()) {
                alert('Init Date should be lesser than today');
                return false;
            }
        }
        if (reason == 'Reversal Pending with PM/DM') {
            if (closureDate == undefined) {
                alert('Please select the closureDate  ');
                return false;
            }
            else if (closureDate < new Date()) {
                alert('Please select a future date');
                return false;
            }
        }
    }



}
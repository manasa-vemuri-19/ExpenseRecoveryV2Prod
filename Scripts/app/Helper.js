/// <reference path="../jquery-1.12.4.js" />


function Helper() { }


Helper.populateDDL = function (id, items, defaultValue) {
    
    var options = [], optionshtml = [];

    for (var i = 0; i < items.length; i++)
        if (options.indexOf(items[i]) == -1)
            options.push(items[i]);

    if (['', undefined].indexOf(defaultValue) == -1)
        optionshtml.push("<option value=''>" + defaultValue + "</option>");

    for (var i = 0; i < options.length; i++)
        optionshtml.push("<option value='" + options[i] + "'>" + options[i] + "</option>");

    var html = optionshtml.join('');
    if (document.getElementById(id) == undefined)
        console.log('id', id);
    document.getElementById(id).innerHTML = html;


}
Helper.setDDLValue = function (id, value) {
    
    var ddl = document.getElementById(id);
    var options = ddl.options;
    var temp = [];
    for (var i = 0; i < options.length; i++) {
        temp.push(options[i].text);
    }
    if (temp.indexOf(value) > -1)
        ddl.value = value;
    else
        ddl.value = '';
}

Helper.getColumnItems = function (items, column) {
    
    var temp = [];
    for (var i = 0; i < items.length; i++)
        temp.push(items[i][column]);
    return temp;
}

Helper.getCascadingItemsFor = function (items, column1Left, column1LeftValue, column2Left, column2LeftValue, columnRight) {
    
    var temp = [];
    if (['', undefined].indexOf(column2Left) > -1)
        for (var i = 0; i < items.length; i++) {
            if ((items[i][column1Left]) == column1LeftValue)
                temp.push(items[i][columnRight]);
        }
    else
        for (var i = 0; i < items.length; i++) {
            if ((items[i][column1Left]) == column1LeftValue && (items[i][column2Left]) == column2LeftValue)
                temp.push(items[i][columnRight]);
        }

    return temp;
}

Helper.getDateObject = function (dateStr) {
    
    if (dateStr == '') return undefined;
    var parts = dateStr.split('/');
    var mm = parseInt(parts[0]) - 1;
    var dd = parts[1];
    var yy = parts[2];
    var dateObj = new Date(yy, mm, dd);
    return dateObj;
}

Helper.formatDate = function (dateStr) {

    if (dateStr == null) {
        return '';
    }
    var dateObj = new Date(dateStr);
    var m = dateObj.getMonth() + 1;
    var d = dateObj.getDate();
    var y = dateObj.getFullYear();
    var format = m + '/' + d + '/' + y;
    return format;

}

$.fn.extend({
    animateCss: function (animationName) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        this.addClass('animated ' + animationName).one(animationEnd, function () {
            $(this).removeClass('animated ' + animationName);
        });
        return this;
    }
});


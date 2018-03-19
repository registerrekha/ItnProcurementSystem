

//var app = angular.module("omsapp", []);

//app.controller("CustomerSearchController", CustomerSearchController);
//// Create the instant search filter

//app.filter('searchFor', function () {

//    return function (arr, searchCustomer) {

//        if (!searchCustomer) {
//            return arr;
//        }

//        var result = [];

//        searchCustomer = searchCustomer.toLowerCase();

//        // Using the forEach helper method to loop through the array
//        angular.forEach(arr, function (item) {

//            if (item.firstName.toLowerCase().indexOf(searchCustomer) !== -1) {
//                result.push(item);
//            }

//        });

//        return result;
//    };

//});

//// The controller

//function CustomerSearchController($scope) {

//    // The data model. These items would normally be requested via AJAX,
//    // but are hardcoded here for simplicity. See the next example for
//    // tips on using AJAX.

//    $scope.customers = [
//        {
//            id: '1',
//            firstName: 'Rekha1',
//            lastName: 'v1',
//            email: 'test@gmail.com'
//        },
//        {
//            id: '2',
//            firstName: 'R2',
//            lastName: 'V2',
//            email: 'test2@gmail.com'
//        },
//        {
//            id: '3',
//            firstName: 'VJ3',
//            lastName: 'modela',
//            email: 'modela@gmail.com'
//        },
//        {
//            id: '4',
//            firstName: 'C#',
//            lastName: 'Chh',
//            email: 'chh@gmail.com'
//        }
//    ];


//}


function onAjaxLoad() {
    //$("#js_spinner").show();
    //$("#overlay").show();
    alert("ajax load");
}
function onAjaxEnd() {
    //$("#js_spinner").hide();
    //$("#overlay").hide();
    //tablesorterFunction();
    alert("ajax end");
}


function tablesorterFunction() {
    //$("table.tablesorter").tablesorter({
    //    widthFixed: true, widgets: ['zebra'],
    //}).bind("sortEnd", function (event) {
    //    var table = event.target, currentSort = table.config.sortList, columnNum = currentSort[0][0];
    //    sortColumnName = $(table.config.headerList[columnNum]).text();
    //    //alert(sortColumnName);
    //    sortColumnName = $.trim(sortColumnName);
    //});
}
$(function () {
    var opt = {
        rowCount: 6,
        columnCount: 16
    }
    var grid = new TubeGrid(document.getElementById("tubesContainer"), opt);

    var menu = [{
            name: 'create',
            title: 'create button',
            fun: function (obj, event) {
                //alert(' add button')
                var dt = $(obj.trigger).data("options");
                console.log("create button click on row " + dt.row + " column " + dt.column);
            }
        }, {
            name: 'update',
            title: 'update button',
            fun: function (obj, event) {
                //alert(' update button')
                var dt = $(obj.trigger).data("options");
                console.log("update button click on row " + dt.row + " column " + dt.column);
            }
        }, {
            name: 'delete',
            title: 'delete button',
            fun: function (obj, event) {
                //alert(' delete button')
                var dt = $(obj.trigger).data("options");
                console.log("delete button click on row " + dt.row + " column " + dt.column);
            }
        }];

    //Calling context menu
    //$('.grid-cell').contextMenu(menu, {triggerOn:'contextmenu'});
    $( document ).tooltip();
});
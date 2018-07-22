$(function () {
    var opt = {
        rowCount: 12,
        columnCount: 8,
		blockCount: 3
    }
    var grid = new TubeGrid(document.getElementById("tubesContainer"), opt);

    // var menu = [{
    //         name: 'create',
    //         title: 'create button',
    //         fun: function (obj, event) {
    //             //alert(' add button')
    //             var dt = $(obj.trigger).data("options");
    //             console.log("create button click on row " + dt.row + " column " + dt.column);
    //         }
    //     }, {
    //         name: 'update',
    //         title: 'update button',
    //         fun: function (obj, event) {
    //             //alert(' update button')
    //             var dt = $(obj.trigger).data("options");
    //             console.log("update button click on row " + dt.row + " column " + dt.column);
    //         }
    //     }, {
    //         name: 'delete',
    //         title: 'delete button',
    //         fun: function (obj, event) {
    //             //alert(' delete button')
    //             var dt = $(obj.trigger).data("options");
    //             console.log("delete button click on row " + dt.row + " column " + dt.column);
    //         }
    //     }];
    //Calling context menu
    //$('.grid-cell').contextMenu(menu, {triggerOn:'contextmenu'});


    $(document).tooltip();


    $("#move").click(function (e) {
        if(!window.externalObj){
            return;
        }
        let selectedTubes = [];
        $.each($("#tubesContainer .grid-cell.selected"), (idx, val) => {
            let classArr = val.className.split(' ');
            let obj = {};
            classArr.forEach((val2, idx2) => {
                if(val2.match(/^r\d+$/)){
                    obj.row = parseInt(val2.substr(1));
                } else if(val2.match(/^c\d+$/)){
					obj.column = parseInt(val2.substr(1));
				}
            });
			selectedTubes.push(obj);
        });
		window.externalObj.Move(JSON.stringify(selectedTubes));

    });

});
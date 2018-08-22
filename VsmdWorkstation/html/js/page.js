$(function () {

    var gridCtxMenu = [{
        name: 'move2Here',
        title: '移动到这里',
        fun: function (obj, event) {
            if (window.externalObj) {
                let ele = window.__grid.getLastClickedTube();
                if (ele) {
                    console.log('move to tube, row: ', ele.row, ', column: ', ele.col);
                    window.externalObj.MoveToHere(JSON.stringify({ row: ele.row, col: ele.col }));
                }
            }
        }
    },
    {
        name: 'drip',
        title: '滴液',
        fun: function (obj, event) {
            if (window.externalObj) {
                let ele = window.__grid.getLastClickedTube();
                if (ele) {
                    console.log('drip tube, row: ', ele.row, ', column: ', ele.col);
                    window.externalObj.DripTube(JSON.stringify({ row: ele.row, col: ele.col }));
                }
            }
        }
    }];

	window.JsExecutor = {
	    buildGrid: function (options) {
	        if (window.__grid) {
	            window.__grid.buildGrid(options);
	        } else {
	            window.__grid = new TubeGrid(document.getElementById("tubesContainer"), options);
	            $('#tubesContainer').contextMenu(gridCtxMenu, { triggerOn: 'contextmenu' });
	        }
	        //addContextMenu();
		},
		moveCallBack: function(row, col) {
			window.__grid.getCell(row, col).addClass("moveDone");
		},
		beforeMove: function () {
			window.__grid.enterMoveMode();
		},
		afterMove: function () {
			window.__grid.leaveMoveMode();
		},
		getSelectedTubes: function () {
			return window.__grid.getSelectedTubes();
		},
		startDrip: function () {
		    if (window.externalObj) {
		        window.externalObj.StartDrip(JSON.stringify(JsExecutor.getSelectedTubes()));
		    }
		},
		pauseMove: function () {
		    window.__grid.pauseMove();
		},
		resetTube: function(){
		    window.__grid.resetTube();
		},
		resumeMove: function () {
		    if (window.externalObj) {
		        window.externalObj.StartDrip(JSON.stringify(JsExecutor.getSelectedTubes()));
		    }
		}
	}
	if (window.externalObj && window.externalObj.DomLoaded) {
	    window.externalObj.DomLoaded();
	}
	
})
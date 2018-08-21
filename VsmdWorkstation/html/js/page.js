$(function () {
    function addContextMenu() {
        var menu = [{
            name: 'move2Here',
            title: '移动到这里',
            fun: function (obj, event) {
                if (window.externalObj) {
                    window.externalObj.MoveToHere(JSON.stringify({ row: 1, col: 1 }));
                }
            }
        }];
        //Calling context menu
        $('.grid-cell').contextMenu(menu, { triggerOn: 'contextmenu' });
    }
     

	window.JsExecutor = {
	    buildGrid: function (options) {
	        if (window.__grid) {
	            window.__grid.buildGrid(options);
	        } else {
	            window.__grid = new TubeGrid(document.getElementById("tubesContainer"), options);
	        }
	        addContextMenu();
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
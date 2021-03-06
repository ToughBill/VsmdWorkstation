﻿$(function () {

    var gridCtxMenu = [{
        name: '移动到这里',
        title: '移动到这里',
        fun: function (obj, event) {
            if (window.externalObj) {
                let ele = window.__grid.getLastClickedTube();
                if (ele) {
                    window.externalObj.MoveToHere(JSON.stringify(ele.coord));
                }
            }
        }
    },
    {
        name: '滴液',
        title: '滴液',
        fun: function (obj, event) {
            if (window.externalObj) {
                let ele = window.__grid.getLastClickedTube();
                if (ele) {
                    window.externalObj.DripTube(JSON.stringify(ele.coord));
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
	            window.__grid.onSelected = function (totalCount) {
	                if (window.externalObj && window.externalObj.OnTubeSelected) {
	                    window.externalObj.OnTubeSelected(totalCount);
	                }
	            };
	        }
		},
	    moveCallBack: function (blockNum, row, col) {
	        window.__grid.moveCallBack(blockNum, row, col);
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
		getSelectedTubeCount:function(){
		    return window.__grid.getSelectedTubes().length;
		},
		startDrip: function () {
		    if (window.externalObj) {
		        window.externalObj.StartDrip(JSON.stringify(JsExecutor.getSelectedTubes()));
		    }
		},
		setDrippingTube: function (blockNum, row, col) {
		    window.__grid.setDrippingTube(blockNum, row, col);
		},
		pauseMove: function () {
		    window.__grid.pauseMove();
		},
		selectAllTubes: function(){
            window.__grid.selectAllTubes();
            if (__grid.onSelected) {
               
                __grid.onSelected(__grid.getSelectedTubes().length);
            }

           
        
		},
		selectTubes: function(count){
		    window.__grid.selectTubes(count);
		},
		reverseSelect: function(){
            window.__grid.reverseSelect();
            if (__grid.onSelected) {

                __grid.onSelected(__grid.getSelectedTubes().length);
            }
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
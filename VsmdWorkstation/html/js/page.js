$(function () {
	window.JsExecutor = {
		buildGrid: function (options) {
			window.__grid = new TubeGrid(document.getElementById("tubesContainer"), options);
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
		}
	}
})
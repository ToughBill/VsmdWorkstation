window.TubeGrid = (function () {
    var defaultSettings = {
        type: 1, // site type
        gridCount: 1,
        siteCount: 1,
        tubeWidth: 20,
        tubeHeight: 20
    }
    const CHAR_CODE_A = 65;
    const Type_Invalid = 0;
    const Type_Site = 1;
    const Type_Grid = 2;
    var GridMode = {
        Idle: 0,
        Move: 1,
        PauseMove: 2
    }
    function TubeGrid(container, options_) {
        this.container = container;
        this.tubeEleArr = [];
        this.mode = GridMode.Idle;
        let gridEditor = this;
        let clickX, clickY;
        let drippingTubeFlickerInterval;

        function initOptions(options_) {
            gridEditor.options = options_ || {};
            gridEditor.tubeWidth = options_.tubeWidth || defaultSettings.tubeWidth;
            gridEditor.siteCount = options_.siteCount || defaultSettings.siteCount;
            gridEditor.type = options_.type !== undefined ? options_.type : defaultSettings.type;
            gridEditor.gridCount = options_.gridCount || defaultSettings.gridCount;
            gridEditor.siteCount = options_.siteCount || defaultSettings.siteCount;
            gridEditor.rowCount = options_.rowCount;
            gridEditor.columnCount = options_.columnCount;
        }

        initOptions(options_);

        this.renderHeaders = function($table, blockNum) {
            let $header = $("<thead class='grid-headers'></thead>");
            let $hrow = $("<tr></tr>");
            if (this.type == Type_Grid) {
                if (blockNum == 0) {
                    let $numCell = $("<th class = 'grid-row-number'></th>");
                    $hrow.append($numCell);
                }
                $header.append($hrow);
                //let $headerCell = $("<th class='grid-header'>" + String.fromCharCode(CHAR_CODE_A + i + (blockNum * this.columnCount)) + "</th>");
                let $headerCell = $("<th class='grid-header'>" + (blockNum + 1) + "</th>");
			    $hrow.append($headerCell);
            } else {
                let $numCell = $("<th class = 'grid-row-number'></th>");
                $hrow.append($numCell);
                $header.append($hrow);
			    for (let i = 0; i < this.columnCount; i++) {
			        //let $headerCell = $("<th class='grid-header'>" + String.fromCharCode(CHAR_CODE_A + i + (blockNum * this.columnCount)) + "</th>");
			        let $headerCell = $("<th class='grid-header'>" + (i + 1) + "</th>");
			        $hrow.append($headerCell);
			    }
			}
            
			$table.append($header);
		}
        this.renderRows = function ($table, blockNum) {
            let $rows = $("<tbody></tbody>");
            if (this.type == Type_Grid) {
                
                for (let i = 0; i < this.rowCount; i++) {
                    let $row = $("<tr></tr>");
                    if (blockNum == 0) {
                        let $numCell = $("<th  class = 'grid-row-number'>" + (String.fromCharCode(CHAR_CODE_A + i)) + "</th>");
                        $row.append($numCell);
                    }

                    let posClass = "g" + (blockNum + 1) + " r" + (i + 1);
                    let titleVal = "grid: " + (blockNum + 1) + ", row: " + (i + 1);
                    let $cell = $("<td><div class = 'grid-cell tube " + posClass + "' title='" + titleVal + "'></div></td>");
                    $row.append($cell);
                    $rows.append($row);
                }
                
                
            } else {
                for (let i = 0; i < this.rowCount; i++) {
                    let $row = $("<tr></tr>");
                    let $numCell = $("<th  class = 'grid-row-number'>" + (String.fromCharCode(CHAR_CODE_A + i)) + "</th>");
                    $row.append($numCell);
                    for (let j = 0; j < this.columnCount; j++) {
                        let posClass = "s" + (blockNum + 1) + " r" + (i + 1) + " c" + (j + 1);
                        let titleVal = "site: " + (blockNum + 1) + ", row: " + (i + 1) + ", column: " + (j + 1);
                        let $cell = $("<td><div class = 'grid-cell tube " + posClass + "' title='" + titleVal + "'></div></td>");
                        $row.append($cell);
                    }
                    $rows.append($row);
                }
            }
            
			$table.append($rows);
		}
        
        this.renderGrid = function () {
            let $con = $(this.container);
            for (let i = 0; i < this.gridCount; i++) {
                let $table = $('<table class="table" id="grid_' + i + '"></table>');
                this.renderHeaders($table, i);
                this.renderRows($table, i);
                $con.append($table);
                $table.width($table.find('tbody').width() + 20);
            }
        }
        this.renderSite = function () {
            let $con = $(this.container);
            for (let i = 0; i < this.siteCount; i++) {
                let $table = $('<table class="table" id="site_' + i + '"></table>');
                this.renderHeaders($table, i);
                this.renderRows($table, i);
                $con.append($table);
                if (i > 0) {
                    $table.find('thead').css('visibility', 'hidden');
                }
                $table.width($table.find('tbody').width() + 20);
            }
        }
        this.render = function () {
            if (!this.rowCount || !this.columnCount) {
                return;
            }
            let $con = $(this.container);
            //let conWidth = this.tubeWidth * this.columnCount + parseInt($con.css('padding-left')) + parseInt($con.css('padding-right'));
            //$con.width(conWidth);
            $con.addClass("tubeGrid");
            $con.removeClass("site grid");
            $con.addClass(this.type == Type_Site ? "site" : "grid");
            if (this.type == Type_Site) {
                this.renderSite();
            } else if (this.type == Type_Grid) {
                this.renderGrid();
            }
            //for(let i = 0; i < this.blockCount; i++){
            //	let $table = $('<table class="table" id="block_' + i + '"></table>');
            //	this.renderHeaders($table, i);
            //	this.renderRows($table, i);
            //	$con.append($table);
            //	$table.width($table.find('tbody').width() + 20);
            //}   
        }
        this.enableBoxSelection = function() {
            function clearBubble(e) {
                if (e.stopPropagation) {
                    e.stopPropagation();
                } else {
                    e.cancelBubble = true;
                }

                if (e.preventDefault) {
                    e.preventDefault();
                } else {
                    e.returnValue = false;
                }
            }
            var $container = $(this.container);
            $container
                .on('mousedown', function (eventDown) {
                    if ($(eventDown.target).hasClass('grid-cell')) {
                        clickX = eventDown.clientX;
                        clickY = eventDown.clientY;
                    } else {
                        clickX = -1;
                        clickY = -1;
                    }
                    
                    if (gridEditor.mode != GridMode.Idle) {
                        return;
                    }
                    if (eventDown.button != 0) {
                        return;
                    }
                    if (!eventDown.ctrlKey) {
                        let rowC, colC;
                        let className = eventDown.target.className
                        let match1 = className.match(/\S*(r\d+)/);
                        if (match1 && match1.length > 0) {
                            rowC = match1[0];
                        }
                        let match2 = className.match(/\S*(c\d+)/);
                        if (match2 && match2.length > 0) {
                            colC = match2[0];
                        }
                        $(gridEditor.container).find('.grid-cell.selected,.grid-cell.moveDone').not('.' + rowC + '.' + colC).removeClass('selected moveDone');
                        //gridEditor.resetTube();
                    }
                    let isSelect = true;

                    let $selectBoxDashed = $('<div class="select-box-dashed"></div>');
                    $('body').append($selectBoxDashed);

                    let startX = eventDown.x || eventDown.clientX;
                    let startY = eventDown.y || eventDown.clientY;
                    $selectBoxDashed.css({
                        left: startX,
                        top : startY
                    });

                    var _x = null;
                    var _y = null;
                    clearBubble(eventDown);
                    $container.on('mousemove', function(eventMove) {
                        $selectBoxDashed.css('display', 'block');
                        _x = eventMove.x || eventMove.clientX;
                        _y = eventMove.y || eventMove.clientY;
                        var _left   = Math.min(_x, startX);
                        var _top    = Math.min(_y, startY);
                        var _width  = Math.abs(_x - startX);
                        var _height = Math.abs(_y - startY);
                        $selectBoxDashed.css({
                            left  : _left,
                            top   : _top,
                            width : _width,
                            height: _height
                        });
                        let boxPos = $selectBoxDashed[0].getBoundingClientRect();
                        $container.find('.grid-cell').each(function(idx, val) {
                            var $item = $(val);
                            var elePos = val.getBoundingClientRect();
                            let cx = elePos.left + elePos.width / 2;
                            let cy = elePos.top + elePos.height / 2;
                            //console.log($(val));
                            //console.log(`cx: ${cx}, boxPos.left: ${boxPos.left}, boxPos.right: ${boxPos.right}`);
                            //console.log(`cy: ${cy}, boxPos.top: ${boxPos.top}, boxPos.bottom: ${boxPos.bottom}`);
                            if((cx > boxPos.left && cx < boxPos.right) &&
                                (cy > boxPos.top && cy < boxPos.bottom)){
                                if(!$item.hasClass("selected")){
                                    $item.addClass('selected');
                                }
                            } else {
                                if($item.hasClass("selected")){
                                    $item.removeClass('selected');
                                }

                            }
                        });

                        clearBubble(eventMove);
                    });

                    $(document).on('mouseup', function() {
                        $container.off('mousemove');
                        $selectBoxDashed.remove();

                        // if (selectCallback) {
                        //     selectCallback();
                        // }
                    });
                })
                .on('click', '.grid-cell', function (ev) {
                    if (gridEditor.mode != GridMode.Idle) {
                        return;
                    }
                    $(".grid-cell.moveDone").removeClass("moveDone");
                    if(ev.ctrlKey) {
                        $(this).toggleClass('selected');
                    } else {
                        let alreadySelected = $(this).hasClass('selected');
                        $container.find('.grid-cell.selected').removeClass('selected');
                        if (!alreadySelected) {
                            $(this).addClass('selected');
                        }
                    }

                });
        }
        this.getTubePos = function (type, tubeEle) {
            let classArr = tubeEle.className.split(' ');
            if (type == Type_Site) {
                let site, row, column;
                classArr.forEach((val2, idx2) => {
                    if (val2.match(/^s\d+$/)) {
                        site = parseInt(val2.substr(1));
                    } else if (val2.match(/^r\d+$/)) {
                        row = parseInt(val2.substr(1));
                    } else if (val2.match(/^c\d+$/)) {
                        column = parseInt(val2.substr(1));
                    }
                });
                return { site: site, row: row, col: column };
            } else if (type == Type_Grid) {
                let grid, row;
                classArr.forEach((val2, idx2) => {
                    if (val2.match(/^g\d+$/)) {
                        grid = parseInt(val2.substr(1));
                    } else if (val2.match(/^r\d+$/)) {
                        row = parseInt(val2.substr(1));
                    }
                });
                return { grid: grid, row: row };
            }
            return {};
        }
        this.getLastClickedTube = function () {
            if (clickX <= 0 || clickY <= 0) {
                return null;
            }
            let eleArr = document.elementsFromPoint(clickX, clickY);
            let targetEle;
            for(let i = 0; i < eleArr.length; i++){
                if ($(eleArr[i]).hasClass('grid-cell')) {
                    targetEle = eleArr[i];
                    break;
                }
            }
            
            let pos = { element: targetEle, type: gridEditor.type };
            if (targetEle) {
                let coord = this.getTubePos(gridEditor.type, targetEle);
                coord.type = gridEditor.type;
                pos.coord = coord;
            }
            return pos;
        }
        this.getCell = function (blockNum, row, col) {
            if (gridEditor.type == Type_Site) {
                return $(".grid-cell" + ".s" + blockNum + ".r" + row + ".c" + col);
            } else if (gridEditor.type == Type_Grid) {
                return $(".grid-cell" + ".g" + blockNum + ".r" + row);
            }
        }
        this.moveCallBack = function (blockNum, row, col) {
            if (drippingTubeFlickerInterval) {
                clearInterval(drippingTubeFlickerInterval);
                this.getCell(blockNum, row, col).removeClass('flickerColor');
            }
            this.getCell(blockNum, row, col).addClass("moveDone");
        }
        this.setDrippingTube = function (blockNum, row, col) {
            if (drippingTubeFlickerInterval) {
                clearInterval(drippingTubeFlickerInterval);
            }
            drippingTubeFlickerInterval = setInterval(() => {
                this.getCell(blockNum, row, col).toggleClass('flickerColor');
            }, 500)
        }
        this.enterMoveMode = function () {
            gridEditor.mode = GridMode.Move;
        }
        this.leaveMoveMode = function () {
            gridEditor.mode = GridMode.Idle;
        }
        this.pauseMove = function () {
            gridEditor.mode = GridMode.PauseMove;
        }
        this.selectAllTubes = function () {
            $(".grid-cell").removeClass("moveDone").addClass("selected");
        }
        this.reverseSelect = function () {
            $(".grid-cell.selected").addClass("oriSelected").removeClass("selected");
            $(".grid-cell").not(".oriSelected").addClass("selected");
            $(".grid-cell.oriSelected").removeClass("oriSelected");
        }
        this.resetTube = function () {
            $(".grid-cell.selected").removeClass("selected");
            $(".grid-cell.moveDone").removeClass("moveDone");
            gridEditor.mode = GridMode.Idle;
        }
        this.buildGrid = function (options_) {
            initOptions(options_);
            $(gridEditor.container).html("");
            gridEditor.render();
        }
        this.getSelectedTubes = function () {
			let selectedTubes = [];
			$.each($(gridEditor.container).find(".grid-cell.selected").not(".moveDone"), (idx, val) => {
			    let pos = this.getTubePos(gridEditor.type, val);
			    pos.type = gridEditor.type;
				//let classArr = val.className.split(' ');
				//let obj = {};
				//classArr.forEach((val2, idx2) => {
				//	if(val2.match(/^r\d+$/)){
				//		obj.row = parseInt(val2.substr(1));
				//	} else if(val2.match(/^c\d+$/)){
				//		obj.column = parseInt(val2.substr(1));
				//	}
				//});
				selectedTubes.push(pos);
			});
			return selectedTubes;
        }

        this.render();
        this.enableBoxSelection();
    }

    return TubeGrid;
})();
window.TubeGrid = (function () {
    var defaultSettings = {
		blockCount: 1,
        tubeWidth: 20,
        tubeHeight: 20
    }
    const CHAR_CODE_A = 65;
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

        function initOptions(options_) {
            gridEditor.options = options_ || {};
            
            gridEditor.tubeWidth = options_.tubeWidth || defaultSettings.tubeWidth;
            gridEditor.tubeHeight = options_.tubeHeight || defaultSettings.tubeHeight;
            gridEditor.blockCount = options_.blockCount || defaultSettings.blockCount;
            gridEditor.rowCount = options_.rowCount;
            gridEditor.columnCount = options_.columnCount;
        }

        initOptions(options_);

        this.renderHeaders = function($table, blockNum) {
			let $header = $("<thead class='grid-headers'></thead>");
			let $hrow = $("<tr></tr>");
			if(blockNum == 0){
				let $numCell = $("<th  class = 'grid-row-number'></th>");
				$hrow.append($numCell);
            }
			$header.append($hrow);
			for(let i = 0; i < this.columnCount; i++){
				let $headerCell = $("<th class='grid-header'>" + String.fromCharCode(CHAR_CODE_A + i + (blockNum * this.columnCount)) + "</th>");
				$hrow.append($headerCell);
			}
			$table.append($header);
		}
		this.renderRows = function ($table, blockNum) {
            let $rows = $("<tbody></tbody>");
            for(let i = 0; i < this.rowCount; i++){
                let $row = $("<tr></tr>");
                if(blockNum == 0){
					let $numCell = $("<th  class = 'grid-row-number'>" + (i + 1) + "</th>");
					$row.append($numCell);
                }
                for(let j = 0; j < this.columnCount; j++){
					let posClass = "r" + (i + 1) + " c" + (j + 1 + blockNum * this.columnCount);
					let titleVal = (i + 1) + "," + (j + 1 + blockNum * this.columnCount);
                    let $cell = $("<td><div class = 'grid-cell tube " + posClass + "' title='" + titleVal + "'></div></td>");
					$row.append($cell);
                }
				$rows.append($row);
            }
			$table.append($rows);
		}
        this.render = function() {
            if (!this.rowCount || !this.columnCount) {
                return;
            }
            let $con = $(this.container);
            //let conWidth = this.tubeWidth * this.columnCount + parseInt($con.css('padding-left')) + parseInt($con.css('padding-right'));
            //$con.width(conWidth);
            $con.addClass("tubeGrid");

            for(let i = 0; i < this.blockCount; i++){
				let $table = $('<table class="table" id="block_' + i + '"></table>');
				this.renderHeaders($table, i);
				this.renderRows($table, i);
				$con.append($table);
				$table.width($table.find('tbody').width() + 20);
            }



            // for (let i = 0; i < this.rowCount; i++) {
            //     let $rowEle = $("<div class='grid-row'></div>");
            //     for (let j = 0; j < this.columnCount; j++) {
            //         let posClass = "r" + (i+1) + " c" + (j+1);
            //         let $cell = $("<div class = 'grid-cell tube " + posClass + "' title=" + (i+1) + "," + (j+1) + "></div>");
            //         $cell.data("options", {row: i+1, column: j+1});
            //         $rowEle.append($cell);
            //     }
            //     $con.append($rowEle);
            // }
             
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

        this.getCell= function (row, col) {
            return $(".grid-cell" + ".r" + row + ".c" + col);
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
			return selectedTubes;
        }

        this.render();
        this.enableBoxSelection();
    }

    return TubeGrid;
})();
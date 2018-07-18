$(function () {
    var opt = {
        rowCount: 6,
        columnCount: 16
    }
    var grid = new TubeGrid(document.getElementById("tubesContainer"), opt);


    $("#setFirstTubePos").click(function (e) {
        $("#tips").text('Please use "←" and "→" to move the axis')
    })
});

$(function () {
    $('#txtKeyword').catcomplete({
        delay: 0,
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "../Complete/CompleteHoSo.asmx/autocomplete",
                data: "{'searchitem':'" + request.term + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    response($.map(result.d, function (el) {
                        return {
                            label: el.title
                        };
                    }));
                }
            });
        }
            , select: function (event, ui) {
                var catid = document.getElementById("ddlCate").value;
                document.location = "/tim-kiem.html?page=0&cat=" + catid + "&key=" + ui.item.value;
            }

    });
});
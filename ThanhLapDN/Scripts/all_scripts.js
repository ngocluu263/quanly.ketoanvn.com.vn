//Support
$(window).load(function () {
    var state = false;

    $("#toggle-slide-button").click(function () {
        if (!state) {
            $('#map-legend').animate({ width: "toggle" }, 0);
            $('#toggle-slide-button img').attr('src', '/Images/support_icon.png');

            state = true;
        }
        else {
            $('#map-legend').animate({ width: "toggle" }, 0);
            $('#toggle-slide-button img').attr('src', '/Images/support_icon.png');

            state = false;
        }
    });
});
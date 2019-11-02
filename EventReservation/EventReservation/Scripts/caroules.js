var mainFunction = function (options) {
    var vars = {
        id: '',
        bootsGrid: ["col-md-1", "col-md-2", "col-md-3", "col-md-4", "col-md-6", "col-md-12", "col-sm-1", "col-sm-2", "col-sm-3", "col-sm-4", "col-sm-6", "col-sm-12", "col-lg-1", "col-lg-2", "col-lg-3", "col-lg-4", "col-lg-6", "col-lg-12", "col-xs-1", "col-xs-2", "col-xs-3", "col-xs-4", "col-xs-6", "col-xs-12", "col-xl-1", "col-xl-2", "col-xl-3", "col-xl-4", "col-xl-6", "col-xl-12"],
        breakPoint: [1200, 992, 768, 567, 0]
    }
    var root = this;

    construct = function (v, options) {
        $.extend(v, options);
    }
    this.GetUnique = function (e) {
        var l = [],
            s = temp_c = [],
            t = vars.bootsGrid;
        $(e).find('.carousel-item').each(function () {
            for (var l = $(this).find('div').first().attr("class").split(/\s+/), t = 0; t < l.length; t++) s.push(l[t])
        });
        for (var c = 0; c < s.length; c++) temp_c = s[c].split("-"), 2 == temp_c.length && (temp_c.push(""), temp_c[2] = temp_c[1], temp_c[1] = "xs", s[c] = temp_c.join("-")), -1 == $.inArray(s[c], l) && $.inArray(s[c], t) && l.push(s[c]);
        setcss(l, '#' + vars.id + ' .carousel-item', '#' + vars.id + ' .carousel-inner')
        return l
    }
    setcss = function (e, l, s) {
        var t = [];
        $.each(vars.breakPoint, function (i, e) {
            t.push("");
        })
        for (var c = d = f = g = 0, o = [], a = 0; a < e.length; a++) {
            var i = e[a].split("-");
            if (3 == i.length) {
                switch (i[1]) {
                    case "xl":
                        d = 0;
                        break;
                    case "lg":
                        d = 1;
                        break;
                    case "md":
                        d = 2;
                        break;
                    case "sm":
                        d = 3;
                        break;
                    case "xs":
                        d = 4
                }
                t[d] = i[2]
            }
        }
        var slider = document.getElementById(vars.id);
        var style = document.createElement("STYLE");
        slider.insertBefore(style, slider.childNodes[0]).id = vars.id + '-style';
        var style = $("#" + vars.id + '-style');
        for (var n = 0; n < t.length; n++)
            if ("" !== t[n]) {
                if (0 === c && (c = 12 / t[n]),
                    f = 12 / t[n],
                    g = 100 / f,
                    a = s + " > .carousel-item.active.carousel-item-right," + s + " > .carousel-item.carousel-item-next {-ms-transform: translate3d(" + g + "%, 0, 0);-o-transform: translate3d(" + g + "%, 0, 0);-moz-transform: translate3d(" + g + "%, 0, 0);-webkit-transform: translate3d(" + g + "%, 0, 0);transform: translate3d(" + g + ", 0, 0);left: 0;}" + s + " > .carousel-item.active.carousel-item-left," + s + " > .carousel-item.carousel-item-prev {-webkit-transform: translate3d(-" + g + "%, 0, 0);transform: translate3d(-" + g + "%, 0, 0);left: 0;}" + s + " > .carousel-item.carousel-item-left, " + s + " > .carousel-item.carousel-item-prev.carousel-item-right, " + s + " > .carousel-item.active {-webkit-transform: translate3d(0, 0, 0);transform: translate3d(0, 0, 0);left: 0;}", f > 1) {

                    for (k = 0; k < f - 1; k++) {
                        o.push(l + " .cloneditem-" + k);
                    }
                    o.length && (a = a + o.join(",") + "{display: block;}"), o = []
                }
                0 !== vars.breakPoint[n] && (a = "@media all and (min-width: " + vars.breakPoint[n] + "px){" + a + "}"),
                    style.prepend(a)
            }
        $(l).each(function () {
            for (var e = $(this), l = 0; l < c - 1; l++) {
                (e = e.next()).length || (e = $(this).siblings(":first")), e.children(":first-child").clone().addClass("cloneditem-" + l).appendTo($(this))
            }
        })
    }
    construct(vars, options);
}
$.fn.responsiveCarousel = function (option) {
    $(this).each(function () {
        var _root = this;
        option = { id: $(this).attr('id') }
        var main = new mainFunction(option);
        main.GetUnique(_root);
    })
};
//Use Different Slider IDs for multiple slider
$(document).ready(function () {
    $('#slider-1').responsiveCarousel();
    $('#slider-2').responsiveCarousel();
});
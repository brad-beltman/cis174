$(function () {
    var tooltip = $("#tooltip");
    $('[data-tooltip]').bind('mouseover', function () {
        var $this = $(this), offset = $this.offset(), posX = offset.left, posY = offset.top;
        posX += $this.find('span').innerWidth();
        tooltip.
            css({ left: posX + "px", top: posY + "px" }).
            text($this.attr('data-tooltip')).
            removeClass("nd");
    }).bind('mouseout', function () { tooltip.addClass('nd'); });
});
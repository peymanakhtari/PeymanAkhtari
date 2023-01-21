
// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
      $('#goToTop').css('display','block')
    }
    else {
        $('#goToTop').css('display','none')
    }
};
function scrollFunction() {
}
// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
//# sourceMappingURL=project.js.map
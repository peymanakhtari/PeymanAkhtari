$(document).ready(function () {
  let value = params.scrollto;
  if (value != null) {
    var element = document.getElementById(value).scrollIntoView();
    // window.scrollTo(0, document.body.scrollHeight);
  }
});

try {
    $(document).scroll(function () {
      var currentScroll = $(this).scrollTop();
      var webServicescroll = $("#webservice").offset().top - 300;
      if (currentScroll > webServicescroll) {
        document.getElementById('webservice').classList.remove('collapsWebservice')
        document.getElementsByClassName('hideTitle')[0].classList.remove('hideTitle')
        document.getElementsByClassName('hideP')[0].classList.remove('hideP')
      }
    });
} catch (error) {
    
}

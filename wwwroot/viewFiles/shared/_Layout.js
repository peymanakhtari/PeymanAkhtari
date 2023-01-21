var replaceDigits = function () {
  var map = [
    "&#1776;",
    "&#1777;",
    "&#1778;",
    "&#1779;",
    "&#1780;",
    "&#1781;",
    "&#1782;",
    "&#1783;",
    "&#1784;",
    "&#1785;",
  ];
  document.body.innerHTML = document.body.innerHTML.replace(
    /\d(?=[^<>]*(<|$))/g,
    function ($0) {
      return map[$0];
    }
  );
};

window.onload = function () {
  $(".loader").addClass("d-none");
  $("#img_peyman").addClass("show_img_peyman");
  $(".btn_foolstack").addClass("show_btn_foolstack");
  $("#skillDiv").removeClass("hideSkills");
  $("#skillDiv").addClass("showSkills");
  $("#skillDiv ul li span").addClass("showSkillSpan");
  $(".span1").addClass("moveSpan1");
  $(".span2").addClass("moveSpan2");
  $(".card_container").css("display", "flex");
};
const params = new Proxy(new URLSearchParams(window.location.search), {
  get: (searchParams, prop) => searchParams.get(prop),
});

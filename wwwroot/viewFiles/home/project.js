"use strict";
function scrollToTitle(id) {
    var Element = document.getElementById("title_" + id);
    Element === null || Element === void 0 ? void 0 : Element.scrollIntoView({ behavior: "smooth", block: "center", inline: "nearest" });
}
//# sourceMappingURL=project.js.map
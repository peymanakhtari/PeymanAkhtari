

function scrollToTitle(id: string) {
    var Element = document.getElementById("title_" + id);
    Element?.scrollIntoView({ behavior: "smooth", block: "center", inline: "nearest" })
}

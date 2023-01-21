"use strict";
function scrollTrigger(selector, option = {}) {
    var els = document.querySelectorAll(selector);
    let elems = Array.from(els);
    els.forEach(el => {
        addObserver(el, option);
    });
}
let minId = 0;
let maxId = 0;
function applyChange() {
    var _a;
    for (let index = minId; index <= maxId; index++) {
        let p = (_a = document.getElementById(index)) === null || _a === void 0 ? void 0 : _a.children[1];
        p === null || p === void 0 ? void 0 : p.classList.add('active');
        let parent = document.getElementById(index);
        parent.classList.add('active');
    }
}
function addObserver(el, options) {
    if (!('IntersectionObserver' in window)) {
        if (options.cb) {
            options.cb(el);
        }
        else {
            // console.log('ggffgf')
            // entry.target.classList.add('active')
        }
        return;
    }
    let observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            // console.log(entry)
            if (entry.isIntersecting) {
                if (options.cb) {
                    options.cb(el);
                    console.log('hjhjhjhj');
                }
                else {
                    // console.log(entry)
                    const entryId = parseInt(entry.target.id);
                    if (minId === 0 || maxId === 0) {
                        minId = entryId;
                        maxId = entryId;
                    }
                    else {
                        minId = Math.min(minId, entryId);
                        maxId = Math.max(maxId, entryId);
                    }
                    applyChange();
                }
                observer.unobserve(entry.target);
            }
        });
    }, options);
    observer.observe(el);
}
scrollTrigger('.skill');
//# sourceMappingURL=skills.js.map
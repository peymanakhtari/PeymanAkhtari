function scrollTrigger(selector: string, option = {}) {
    var els = document.querySelectorAll(selector)
    let elems: Element[] = Array.from(els);
    els.forEach(el => {
        addObserver(el, option)
    })
}
let minId: number = 0;
let maxId: number = 0;

function applyChange() {
    for (let index = minId; index <= maxId; index++) {
        let p: Element = document.getElementById(index as unknown as string)?.children[1]!;
        p?.classList.add('active');
        let parent: Element = document.getElementById(index as unknown as string)!;
        parent.classList.add('active')
    }
}

function addObserver(el:Element, options:any){
    if (!('IntersectionObserver' in window)) {
        if(options.cb){
            options.cb(el)
        }else{
            // console.log('ggffgf')
            // entry.target.classList.add('active')
        }
        return
    }
    let observer = new IntersectionObserver((entries, observer) => {

        entries.forEach(entry => {
            // console.log(entry)
            if(entry.isIntersecting){
                if(options.cb){
                    options.cb(el)
                    console.log('hjhjhjhj')
                }else{
                    // console.log(entry)
                        const entryId = parseInt(entry.target.id);
                        if (minId === 0 || maxId === 0) {
                          minId = entryId;
                          maxId = entryId;
                        } else {
                          minId = Math.min(minId, entryId);
                          maxId = Math.max(maxId, entryId);
                        }
                        applyChange()
                }
                observer.unobserve(entry.target)
            }
        })
    }, options)
    observer.observe(el)
}
scrollTrigger('.skill')


// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var timeouts = [];
function slide(val) {
    var elem = document.getElementById('d_' + val.split('_')[1]);
    var elem1 = document.querySelector('#d_' + val.split('_')[1] + ' .feature');
    var btn = document.querySelector('#d_' + val.split('_')[1] + ' .feature .btn_more_features');
    elem.classList.add('show_detail');
    timeouts.push(
        setTimeout(() => {
            elem1.classList.add('unfaid')
            btn.classList.add('pushup_btn_more_feature')
        }, 400)
    )
    timeouts.push(
        setTimeout(() => {
             elem.classList.add('overflow-visible-detail')
        }, 1100)
    )
}
window.addEventListener('click', function (e) {
    var list = document.getElementsByClassName('show_detail');
  
    if (list.length > 0) {
        for (var i = 0; i < list.length; i++) {
            if (list[i].contains(e.target)) {
                return
            }
            var featuerclick=false;
            var btns=this.document.querySelectorAll('.btnFeatures');
            btns.forEach((btn)=>{
                if (btn.contains(e.target)) {
                    featuerclick=true
                }
            })
            if (!document.getElementById('p_' + list[i].id.split('_')[1]).contains(e.target)) {
                if (!featuerclick) {
                    for (var j = 0; j < timeouts.length; j++) {
                        clearTimeout(timeouts[j]);
                    }
                    featuerclick=false;
                }
              
                var id = list[i].id;
                var elem = document.querySelector('#' + id + ' .feature')
                var btn= document.querySelector('#' + id + ' .feature .btn_more_features' )
                elem.classList.remove('unfaid')
                btn.classList.remove('pushup_btn_more_feature')
                list[i].classList.remove("overflow-visible-detail");
                list[i].classList.remove("show_detail");
                 
            }
        }
    }
});

try {
    fetch('https://api.ipregistry.co/?key=emd7ydduzfa2yjz7')
    .then(function (response) {
        return response.json();
    })
    .then(function (payload) {
        if (payload.location.country.name=='Iran'&&!localStorage.getItem('showedLang')) {
           setTimeout(() => {
            $("html, body").animate({ scrollTop: 0 }, "fast");
            $('#langAlertDiv').css('height',$('main').height() + $('.container').height())
            $('.speech-bubble').css('display','block')
           },9000);
        }
    });
function UserKnowLang(){
    $('#langAlertDiv').css('display','none')
    $('.speech-bubble').css('display','none')
     localStorage.setItem('showedLang',true)
}
  
} catch (error) {
    
}


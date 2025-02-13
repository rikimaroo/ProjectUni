document.addEventListener('DOMContentLoaded', function(){
    const password = document.getElementById('inputPass')
    const eye = document.getElementById('eye')
    eye.addEventListener('click', function(){
        if (password.type == 'password') {
            password.type = 'text'
            eye.classList.remove("bi-eye-fill");
            eye.classList.add("bi-eye-slash-fill");
        } else {
            password.type = 'password'
            eye.classList.remove("bi-eye-slash-fill");
            eye.classList.add("bi-eye-fill");
        }
    })
})

$('.pagination-inner a').on('click', function() {
    $(this).siblings().removeClass('pagination-active');
    $(this).addClass('pagination-active');
})



document.addEventListener("DOMContentLoaded", function () {
    let navbarToggler = document.querySelector(".navbar-toggler");
    let blogDiv = document.getElementById("blogDiv");
    let navbarCollapse = document.getElementById("navbarSupportedContent");

    navbarToggler.addEventListener("click", function () {
        if (navbarCollapse.classList.contains("show")) {
            blogDiv.classList.remove("d-none");
        } else {
            blogDiv.classList.add("d-none");
        }
    });

    navbarCollapse.addEventListener("hidden.bs.collapse", function () {
        blogDiv.classList.remove("d-none");
    });
});



function change_image(image){
    var container = document.getElementById("main-image");
    container.src = image.src;
}
document.addEventListener("DOMContentLoaded", function(event) {
});




$(document).ready(function() {
    $("#owl-demo").owlCarousel({
        rtl:true,
        dots: false,
        responsive:{
            0:{
                items:2
            },
            600:{
                items:3
            },
            1000:{
                items:5
            }
        }
    });   
});

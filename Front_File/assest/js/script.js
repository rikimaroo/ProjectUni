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

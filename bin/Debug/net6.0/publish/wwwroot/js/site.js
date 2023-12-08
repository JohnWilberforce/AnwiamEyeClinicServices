// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var check = document.querySelector('.check');
check.addEventListener("click", checkTick);

function checkTick(e){

    if (e.target.checked) {

        check.checked = true;

        check.Disabled = true;

    }
    else {

        e.preventDefault();
    }

}
//var navLink = document.querySelector('#op');
//navLink.addEventListener('click', function (e) {
//    this.classList.toggle('changeColor');
//});

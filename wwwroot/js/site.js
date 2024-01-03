// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//var check = document.querySelector('.check');
//check.addEventListener("click", checkTick);

//function checkTick(e){

//    if (e.target.checked) {

//        check.checked = true;

//        check.Disabled = true;

//    }
//    else {

//        e.preventDefault();
//    }

//}

function calcSum() {
    const multiselect = document.getElementById('services');
    let selOptions = multiselect.selectedOptions;
    console.log(selOptions[0].value);
    let sum = 0;
    for (let i = 0; i < selOptions.length; i++) {
        if (selOptions[i].value == 'VFT') {
            sum+= 150;
        }
        if (selOptions[i].value == 'OCT') {
            sum+= 250;
        }
        if (selOptions[i].value == 'Macula') {
            sum+= 100;
        }
        if (selOptions[i].value == 'Pachymetry') {
            sum+= 100;
        }
        if (selOptions[i].value == 'RetinalImaging') {
            sum+= 100;
        }
        if (selOptions[i].value == 'Frame') {
            sum+= 0;
        }
        

        let amount = document.getElementById('Amount');
        amount.value = sum;
    }
}

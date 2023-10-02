// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function increment(boton) {
    //const botones = document.getElementsByClassName('.botona');
    const vista = document.getElementsByClassName('quantity');
        boton.onclick = (e) => {
            const control = e.target;
            let valor = 0;
            valor = Number(control.dataset.number);
            valor++;
            control.dataset.number = valor;
            for (let i = 0; i < vista.length; i++) {
                vista[i].value = valor;
            }
    }
}

function decrement() {
    var i = document.getElementById("quantity");
    if (i.value > 0)
        i.value--;
}

function incrementer() {
    var i = document.getElementById("portion");
    i.value++;
}

function decrementer() {
    var i = document.getElementById("portion");
    if (i.value > 0)
        i.value--;

}

//< input type = "radio" id = "rdBtn" onclick = "if(document.getElementById('rdBtn').checked===true){document.getElementById('texto').style.display ='';}else{document.getElementById('texto').style.display = 'none';}" />
//    <input type="text" id="texto" style="display:none" />

function adder() {
    var o = document.getElementById('add');
    
    html = '<tr><td>' +
        '<div class="row align-content-start">' +
        '<div class="col-sm mb-3">' +
        '<label class="form-label">Cantidad</label>' +
        '<table>' +
            '<tr>' +
                '<td><button type="button" id="btnPlusC" class="btn_invisible" onclick="increment()"><i class="bi bi-plus"></i></button></td>' +
                '<td><input asp-for="Ingredient.IngredientAmount" type="text" class="form-control" id="quantity"></td>' +
                '<td><button type="button" id="btnMinusC" class="btn_invisible" onclick="decrement()"><i class="bi bi-dash"></i></button></td>' +
            '</tr>' +
        '</table>' +
        '</div>' +
        '<div class="col-sm mb-3">' +
        '<label class="form-label">Unidad</label>' + 
        '<select asp-for="Ingredient.IngredientUnit" class="form-select" asp-items="Model.Itype"></select>' +
        '</div>' +
        '</div>' +
        '<div class="row align-content-start">' +
            '<div class="col-sm mb-3">' +
                '<label class="form-label">Descripción ingrediente</label>' +
                '<input asp-for="Ingredient.IngredientName" type="text" class="form-control" placeholder="Nombre ingrediente" />' +
            '</div>' +
        '</div>' +
        '</td></tr>';

    $('#point').append(html);
}

function titleDisplay() {
    var t = document.getElementById("title");
    var e = document.getElementById("TitleSection");
    if (t.value != "") {
        e.style.visibility = "none";
    }
}

$('timepicker').clockTimePicker({
    duration: true,
    durationNegative: true,
    precision: 5,
    i18n: {
        cancelButton: 'Abbrechen'
    },
    onAdjust: function (newVal, oldVal) {
        //...
    }
});

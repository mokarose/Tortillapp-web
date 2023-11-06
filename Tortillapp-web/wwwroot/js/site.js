// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function increment(boton, pos) {
    const botones = document.querySelector(".buttons");
    const vista = document.getElementById("Ingredient_" + pos + "__IngredientAmount");
    //Ingredients_0__IngredientAmount
    let valor;
    boton.onclick = (e) => {
        const control = e.target;
        valor = vista.value;
        valor++;
        control.dataset.number = valor;
        vista.value = valor;
    }
}

function decrement(boton, pos) {
    const botones = document.querySelector(".buttons");
    const vista = document.getElementById("Ingredient_" + pos + "__IngredientAmount");
    //Ingredient_0__IngredientAmount
    let valor;
    boton.onclick = (e) => {
        const control = e.target;
        valor = vista.value;
        valor--;
        control.dataset.number = valor;
        vista.value = valor;
    }
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

function adder(boton) {
    var o = document.getElementById('add');
    let pos;
    pos = o.value++ 
    
    html = '<tr><td>' +
        '<div class="row align-content-start">' +
        '<div class="col-sm mb-3">' +
        '<label class="form-label">Cantidad</label>' +
        '<table>' +
        '<tr>' +
        '<td><button type="button" class="btn_invisible botona" onclick="increment(this,' + pos + ')"><i class="bi bi-plus"></i></button></td>' +
        '<td><input name="Ingredients[' + pos + '].IngredientAmount" type="text" value="0" class="form-control quantity"></td>' +
        '<td><button type="button" class="btn_invisible botonb" onclick="decrement(this,' + pos + ')"><i class="bi bi-dash"></i></button></td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '<div class="col-sm mb-3">' +
        '<label class="form-label">Unidad</label>' +
        '<select name="Ingredient[' + pos + '].IngredientUnit" class="form-select" asp-items="Model.Itype"></select>' +
        '</div>' +
        '</div>' +
        '<div class="row align-content-start">' +
            '<div class="col-sm mb-3">' +
                '<label class="form-label">Descripción ingrediente</label>' +
        '<input name="Ingredient[' + pos + '].IngredientName" type="text" class="form-control" placeholder="Nombre ingrediente" />' +
            '</div>' +
        '</div>' +
        '</td></tr>';

    $('#point').append(html);
}

function displaySelectedImage(event, elementId) {
    const selectedImage = document.getElementById(elementId);
    const fileInput = event.target;

    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            selectedImage.src = e.target.result;
        };

        reader.readAsDataURL(fileInput.files[0]);
    }
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

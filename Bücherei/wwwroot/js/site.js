// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { error } = require("jquery");

// Write your JavaScript code.

    //var jsonString = '[';
    //jsonString += '"Buchnummer : "'+ $(buchnummer).val()
    //    Buchnummer : $(buchnummer),
    //    Sachgebiet: $(Buch_Sachgebiet).val(),
    //    Titel: $(Buch_Sachgebiet).val(),
    //    Autor: $(Buch_Autor).val(),
    ////    Ort: $(Buch_Ort).val(),
    ////    Erscheinungsjahr: $(Buch_Erscheinungsjahr).val()
    //console.log(test);
    ////'[{"Buchnummer":{"0":{},"length":1},"Sachgebiet":"1","Titel":"1","Autor":"1","Ort":"1","Erscheinungsjahr":"1"}]'
    //$.ajax({
    //    contentType: 'application/json',
    //    dataType: "json",
    //    type: "POST",
    //    url: "/Ausleihe/InsertListe",
    //    data: '[{ "Buchnummer": "0", "Sachgebiet": "1", "Titel": "1", "Autor": "1", "Ort": "1", "Erscheinungsjahr": 1 }]',
    //    success: function (r) {
    //        alert(r + " record(s) inserted.");
    //        //document.createForm.submit();
    //    },
    //    error: function (ex) {
    //        console.log(ex);
    //    }
    //});


    //Loop through the Table rows and build a JSON array.
$("#btnSave").on("click", function () {
    var buecher = new Array();
    $("#tblAusleihe TBODY TR").each(function () {
        var row = $(this);
        var buch = {};
        buch.Buchnummer = row.find("TD").eq(0).html();
        buch.Autor = row.find("TD").eq(1).html();
        buch.Titel = row.find("TD").eq(2).html();
        buch.Sachgebiet = row.find("TD").eq(3).html();
        buch.Ort = row.find("TD").eq(4).html();
        buch.Erscheinungsjahr = row.find("TD").eq(5).html();

        buecher.push(buch);
    });

    //Send the JSON array to Controller using AJAX.
    //$.ajax({
    //    type: "POST",
    //    url: "/Home/InsertAusleihe",
    //    data: JSON.stringify(bueccher),
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (r) {
    //        alert(r + " record(s) inserted.");
    //    }
    //});
});
function addRow() {
    //Zellen Referenz "holen"
    var txtBuchnummer = $("#txtBuchnummer");
    var txtAutor = $("#txtAutor");
    var txtSachgebiet = $("#txtSachgebiet");
    var txtOrt = $("#txtOrt");
    var txtErscheinungsjahr = $("#txtErscheinungsjahr");

    //tbody referenz "holen" und eine reihe hinzufügen
    var tBody = $("#tblAusleihe > TBODY")[0];
    var row = tBody.insertRow(-1);

    //Zellen einfügen
    var cell = $(row.insertCell(-1));
    cell.html(txtBuchnummer.val());

    cell = $(row.insertCell(-1))
    cell.html(txtAutor.val());

    cell = $(row.insertCell(-1))
    cell.html(txtSachgebiet.val());

    cell = $(row.insertCell(-1))
    cell.html(txtOrt.val());

    cell = $(row.insertCell(-1))
    cell.html(txtErscheinungsjahr.val());

    //Add Button cell.
    cell = $(row.insertCell(-1));
    var btnRemove = $('<input style="width:150px" class="btn btn-danger" />');
    btnRemove.attr("type", "button");
    btnRemove.attr("onclick", "Remove(this);");
    btnRemove.val("Remove");
    cell.append(btnRemove);

    //Clear the TextBoxes.
    txtBuchnummer.val("");
    txtAutor.val("");
    txtSachgebiet.val("");
    txtOrt.val("");
    txtErscheinungsjahr.val("");
};

function Remove(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete: " + name)) {
        //Get the reference of the Table.
        var table = $("#tblAusleihe")[0];

        //Delete the Table row using it's Index.
        table.deleteRow(row[0].rowIndex);
    }
};

//function für Bücher
function checkBuch(buchnummer_) {

    $.ajax({
        type: 'POST',
        url: "/Ausleihe/GetBuch",
        dataType: 'json',
        data: { buchnummer: buchnummer_ },
        success: function (data) {
            //wenn das buch schon in der db eingetragen ist:
            if (data != null) {
                //Daten in den Input eintragen
                $(Buch_Autor).val(data.autor);
                //Den Input disablen damit man nicht mehr eintragen kann
                $(Buch_Autor).attr('disabled', true);
                //für alle anderen Inputs:

                $(Buch_Sachgebiet).val(data.sachgebiet);
                $(Buch_Sachgebiet).attr('disabled', true);

                $(Buch_Titel).val(data.titel);
                $(Buch_Titel).attr('disabled', true);

                $(Buch_Ort).val(data.ort);
                $(Buch_Ort).attr('disabled', true);

                $(Buch_Erscheinungsjahr).val(data.erscheinungsjahr);
                $(Buch_Erscheinungsjahr).attr('disabled', true);

                //Collapse einklappen
                $('#collapseBuch').collapse('hide')

                //kurze info über den/die SchülerIn anzeigen
                document.getElementById('kurzInfoBuch').innerText = data.titel + ", von " + data.autor;
                //wenn es das Buch noch gibt werden alle Input wieder enabled und der Inhalt gelöscht
            } else {
                //enable
                $(Buch_Autor).attr('disabled', false);
                //input leeren
                $(Buch_Autor).val("");

                $(Buch_Sachgebiet).attr('disabled', false);
                $(Buch_Sachgebiet).val("");

                $(Buch_Titel).attr('disabled', false);
                $(Buch_Titel).val("");

                $(Buch_Ort).attr('disabled', false);
                $(Buch_Ort).val("");

                $(Buch_Erscheinungsjahr).attr('disabled', false);
                $(Buch_Erscheinungsjahr).val("");

                //Collapse ausklappen
                $('#collapseBuch').collapse('show')

                //kurze info über den/die SchülerIn ausblenden
                document.getElementById('kurzInfoBuch').innerText = "";
            }

        },
        error: function (ex) {

        }
    });
}

//function für SchuelerInnen
function checkSchuelerIn(ausweisnummer_) {
    $.ajax({
        type: 'POST',
        url: '/Ausleihe/GetSchuelerIn',
        dataType: 'json',
        data: { ausweisnummer: ausweisnummer_ },
        success: function (data) {
            //wenn der/die SchuelerIn schon in der db eingetragen ist:
            if (data != null) {
                //Daten in den Input eintragen
                $(SchuelerIn_Vorname).val(data.vorname);
                //Den Input disablen damit man nicht mehr eintragen kann
                $(SchuelerIn_Vorname).attr('disabled', true);
                //für alle anderen Inputs:

                $(SchuelerIn_Nachname).val(data.nachname);
                $(SchuelerIn_Nachname).attr('disabled', true);

                //Collapse einklappen
                $('#collapseSchuelerIn').collapse('hide');
                //kurze info über den/die SchülerIn anzeigen
                document.getElementById('kurzInfoSchuelerIn').innerText = data.vorname + " " + data.nachname;

                //wenn es das Buch noch gibt werden alle Input wieder enabled und der Inhalt gelöscht
            } else {
                //enable
                $(SchuelerIn_Vorname).attr('disabled', false);
                //input leeren
                $(SchuelerIn_Vorname).val("");

                $(SchuelerIn_Nachname).val("");
                $(SchuelerIn_Nachname).attr('disabled', false);

                //Collapse ausklappen
                $('#collapseSchuelerIn').collapse('show');

                //kurze info über den/die SchülerIn ausblenden
                document.getElementById('kurzInfoSchuelerIn').innerText = "";
            }
        },
        error: function (ex) {

        }
    });
}
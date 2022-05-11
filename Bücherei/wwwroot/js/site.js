// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { error } = require("jquery");

// Write your JavaScript code.

$("#btnSave").on("click", async function () {
    //schüler stuff
    var schuelerIn_ = {};
    schuelerIn_.Ausweisnummer = $("#ausweisnummer").val();
    schuelerIn_.Vorname = $(SchuelerIn_Vorname).val();
    schuelerIn_.Nachname = $(SchuelerIn_Nachname).val();

    $.ajax({
        type: 'POST',
        url: "/Ausleihe/CreateFehlendeSchuelerInnen",
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(schuelerIn_),
        success: function () {
            console.log("schueler")
        },
        error: function (ex) {
            console.log(ex);
            console.log(JSON.stringify(schuelerIn_));
        }
    });

    //bücher stuff
    await $("#tblAusleihe TBODY TR").each(function () {
        var row = $(this);
        var buch_ = {};
        buch_.Buchnummer = row.find("TD").eq(0).html();
        buch_.Autor = row.find("TD").eq(1).html();
        buch_.Sachgebiet = row.find("TD").eq(2).html();
        buch_.Titel = row.find("TD").eq(3).html();
        buch_.Ort = row.find("TD").eq(4).html();
        buch_.Erscheinungsjahr = row.find("TD").eq(5).html();
        $.ajax({
            type: 'POST',
            url: "/Ausleihe/CreateFehlendeBuecher",
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(buch_),
            success: function () {
                console.log("buch")
            },
            error: function (ex) {
                console.log(ex);
            }
        });
    });

    //ausleihe stuff
    $("#tblAusleihe TBODY TR").each(function () {
        var row = $(this);
        var ausleihe_ = {};
        ausleihe_.Buchnummer = row.find("TD").eq(0).html();
        ausleihe_.Ausweisnummer = $('#ausweisnummer').val();
        ausleihe_.Ausleihdatum = document.getElementById('Ausleihdatum').value;
        $.ajax({
            type: 'POST',
            url: "/Ausleihe/CreateAusleihe",
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(ausleihe_),
            success: function (data) {
                console.log("ausleihe");
            },
            error: function (ex) {
                console.log(ex);
            }
        });
    });
});




function addRow() {
    var bereitsInTable = false;
    var bereitsAusgeborgt = false;
    //Zellen Referenz "holen"
    var txtBuchnummer = $("#txtBuchnummer");
    String(txtBuchnummer).padStart(6, '0');
    var txtAutor = $("#txtAutor");
    var txtSachgebiet = $("#txtSachgebiet");
    var txtTitel = $("#txtTitel");
    var txtOrt = $("#txtOrt");
    var txtErscheinungsjahr = $("#txtErscheinungsjahr");
    $("#tblAusleihe TBODY TR").each(function () {
        var row = $(this);
        if (txtBuchnummer.val() == row.find("TD").eq(0).html()) {
            bereitsInTable = true;
            alert("Dieses Buch ist bereits in der Liste!");
            enableInputs();
            //clear inputs
            txtBuchnummer.val("");
            txtAutor.val("");
            txtSachgebiet.val("");
            txtOrt.val("");
            txtTitel.val("");
            txtErscheinungsjahr.val("");
        }        
    });

    var test = $("#txtBuchnummer").val();
    $.ajax({
        type: 'POST',
        url: "/Ausleihe/GetAusleihe",
        dataType: 'json',
        
        data: { buchnummer: $("#txtBuchnummer").val() },
        success: function (data) {
             if (data == null) {
                if (!bereitsInTable && !bereitsAusgeborgt) {
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
                    cell.html(txtTitel.val());

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

                    
                }
            } else {
                alert("dieses Buch ist bereits ausgeborgt");
            }
                //Clear the TextBoxes.
                txtBuchnummer.val("");
                txtAutor.val("");
                txtSachgebiet.val("");
                txtOrt.val("");
                txtTitel.val("");
                txtErscheinungsjahr.val("");

                //inputs freigeben
                enableInputs();
        },
        error: function (ex) {
            console.log(ex);
        }
    });


    
    
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
        enableInputs();
    }
};



//function für Bücher
function checkBuch(buchnummer_) {
    var test = buchnummer_;
    $.ajax({
        type: 'POST',
        url: "/Ausleihe/GetBuch",
        dataType: 'json',
        data: { Buchnummer: buchnummer_ },
        success: function (data) {
            //wenn das buch schon in der db eingetragen ist:
            if (data != null) {
                //Daten in den Input eintragen
                $(txtAutor).val(data.autor);
                //Den Input disablen damit man nicht mehr eintragen kann
                $(txtAutor).attr('disabled', true);
                //für alle anderen Inputs:

                $(txtSachgebiet).val(data.sachgebiet);
                $(txtSachgebiet).attr('disabled', true);

                $(txtTitel).val(data.titel);
                $(txtTitel).attr('disabled', true);

                $(txtOrt).val(data.ort);
                $(txtOrt).attr('disabled', true);

                $(txtErscheinungsjahr).val(data.erscheinungsjahr);
                $(txtErscheinungsjahr).attr('disabled', true);

                //wenn es das Buch noch gibt werden alle Input wieder enabled und der Inhalt gelöscht
            } else {
                enableInputs();
            }

        },
        error: function (ex) {
            console.log(ex);
        }
    });
}


function enableInputs() {
    //enable
    $(txtAutor).attr('disabled', false);
    //input leeren
    $(txtAutor).val("");

    $(txtSachgebiet).attr('disabled', false);
    $(txtSachgebiet).val("");

    $(txtTitel).attr('disabled', false);
    $(txtTitel).val("");

    $(txtOrt).attr('disabled', false);
    $(txtOrt).val("");

    $(txtErscheinungsjahr).attr('disabled', false);
    $(txtErscheinungsjahr).val("");
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
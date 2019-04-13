var itemUser = null;
var listCompanies = new Array();

//$(document).ready(function () {
//    $('#data-table').DataTable(dataTableOptions);
//});

function OpenModalSearchUser() {
    
    $("#btnAction").remove();
    $(".modal-header").html("<h2>Importar usuario</h2>");

    var html = "<div>";

    html += "<div class='row'>";
    html += "</div><br/>";

    html += "<div class='row'>";
    html += "<div class='col-sm-3'><label>Usuario: </label><input type='text' style='width: 100%' class='form-control' id='txtSearchUser' /></div>";
    html += "<div class='col-sm-3'><label>Nombre: </label><input type='text' style='width: 100%' class='form-control' id='txtSearchName' /></div>";
    html += "<div class='col-sm-3'><label>Correo: </label><input type='text' style='width: 100%' class='form-control' id='txtSearchMail' /></div>";
    html += "<div class='col-sm-1'><i id='showSpinner' style='display:none; margin-top: 60%;' class='fa fa-spinner fa-pulse fa-2x fa-fw'></i><span class='sr-only'>Buscando...</span></div>";
    html += "<div class='col-sm-2'><span class='btn btn-success' style='margin-top: 20%;' onclick='SearchUser();'><i class='fa fa-search' aria-hidden='true'></i> Buscar</span></div>";
    html += "</div><br/><hr/>";
    html += "<div id='containerList' style='width:100%; height:225px; overflow: scroll;'></div>";
    html += "</div>";

    //$('#optionModal').before('<a class="btn btn-register-solve" style="background-color:green;" href="/User/Index"><i class="fa fa-check-square" aria-hidden="true"> Usuarios directorio activo</i></a>');
    $(".solution-footer").css("display", "inherit");
    $('.infoModal').html(html);
}

function SearchUser() {
    $("#showSpinner").fadeIn();
    var user = {
        Name: $("#txtSearchName").val(),
        Email: $("#txtSearchMail").val(),
        UserName: $("#txtSearchUser").val(),
    };
    var jsonData = JSON.stringify(user);
    
    $.ajax({

        url: '/Users/SearchUser',
        data: jsonData,
        type: "post",
        contentType: "application/json",
        cache: false,
        success: function (request) {
            PrintSearch(request);
            $("#showSpinner").fadeOut();
            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert_message("Ocurrio un error, por favor intente de nuevo");
            $("#showSpinner").fadeOut();
        
        }
    });
}

function serviceUser(url) {
    var appUser = {
        Name: itemUser.Name,
        Mail: itemUser.Mail,
        NameUser: itemUser.NameUser,
        Active: $("#chckActiveUser").is(":checked"),
        ProfileId: $("#slcProfile option:selected").val(),
        //Companys: listCompanies,
    };
    var jsonData = JSON.stringify(appUser);
    $.ajax({
        url: url,
        data: jsonData,
        type: "post",
        contentType: "application/json",
        cache: false,
        success: function (request) {
            //alert_message(request);
            alert(request);
            setTimeout(function () { location.reload(); }, 2000);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //alert_message("Ocurrio un error, por favor intente de nuevo");
            alert("Ocurrio un error, por favor intente de nuevo");
        }
    });
}

function UpdateUser() {
    //listCompanies = [];
    //$("#slcCompanies :selected").each(function () {
    //    var objectCompany = new Object();
    //    objectCompany.Id = this.value
    //    listCompanies.push(objectCompany);
    //});

    //if (listCompanies.length > 0) {
    //    $.confirm({
    //        theme: 'bootstrap',
    //        icon: 'fa fa-warning',
    //        title: 'Confirmación',
    //        content: '¿Desea actualizar el usuario?',
    //        confirm: function () {
                serviceUser('/Users/Update');
    //        },
    //        confirmButton: 'Confirmar',
    //        cancelButton: 'Cancelar'
    //    });
    //}
    //else {
    //    alert_message("Por favor seleccione una compañía");
    //}
}

function Subscribe() {
    //alert("1");
    //listCompanies = [];
    //$("#slcCompanies :selected").each(function () {
    //    var objectCompany = new Object();
    //    objectCompany.Id = this.value
    //    listCompanies.push(objectCompany);
    //});
    //alert("2");
    //if (true) {
    //    $.confirm({
    //        theme: 'bootstrap',
    //        icon: 'fa fa-warning',
    //        title: 'Confirmación',
    //        content: '¿Desea almacenar el usuario?',
    //        confirm: function () {
                serviceUser('/Users/Add');
    //        },
    //        confirmButton: 'Confirmar',
    //        cancelButton: 'Cancelar'
    //    });
    //}
    //else {
     //   alert_message("Por favor seleccione una compañía");
    //}
    //alert("3");
}

function OpenModalActiveUser(objUser, destination) {
    if (destination == 2) {
        objUser.NameUser = objUser.Name;
        objUser.Name = objUser.UserName;
        objUser.Active = false;
        objUser.Mail = objUser.Email;
    }

    itemUser = objUser;
    $("#btnAction").remove();
    $(".modal-header").html("<h2>Administración de usuarios</h2>");
    //var html = '<h1 class="page-header">Información de usuario</h1>';
    var html = '<div class="table-responsive">';
    html += '<table class="table table-striped table-bordered table-hover" id="datatable"><table style="width: 50%;">';
    var userActive = "";
    if (objUser.Active) {
        userActive = "checked";
        
    }

    var correo = "No especificado";
    if (objUser.Mail) {
        correo = objUser.Mail;
       
    }

    html += '<tr><td style="font-weight: bold;">Nombre: </td><td><input class="form-control" type="text" value="' + objUser.NameUser + '" disabled style="width: 100%;"/></td></tr>';
    html += '<tr><td style="font-weight: bold;">Usuario: </td><td><input class="form-control" type="text" value="' + objUser.Name + '" disabled style="width: 100%;"/></td></tr>';
    html += '<tr><td style="font-weight: bold;">Activo: </td><td><input id="chckActiveUser" type="checkbox" ' + userActive + '/></td></tr>'
    html += '<tr><td style="font-weight: bold;">Correo: </td><td><input class="form-control" type="text" value="' + correo + '" disabled style="width: 100%;"/></td></tr>';

    // html += '<td style="font-weight: bold;">Compañía: </td><td><select class="form-control" id="slcCompanies" name="slcCompanies" multiple="multiple" name="origen" size="3">';


    //for (k = 0; k < listCompany.length; k++) {
    //    var companySelected = "";
    //    for (q = 0; q < objUser.Companys.length; q++) {
    //        if (listCompany[k].Id == objUser.Companys[q].Id) {
    //            companySelected = "selected";
    //        }
    //    }
    //    html += '<option value="' + listCompany[k].Id + '" ' + companySelected + '>' + listCompany[k].Name + '</option>';
    //}
 
    //html += '</select></td></tr>'

    html += '<tr><td style="font-weight: bold;">Perfil: </td><td><select class="form-control" id="slcProfile">';//<option value="0">Seleccione...</option>
    var auxSelected = 0;
    for (i = 0; i < listProfile.length; i++) {
        var selected = "";
        if (objUser.Profile != null) {
            if (objUser.Profile.Id == listProfile[i].Id) {
                selected = "selected";
                auxSelected = objUser.Profile.Id;
            }
        }
        html += '<option value="' + listProfile[i].Id + '" ' + selected + '>' + listProfile[i].Name + '</option>';
    }

    html += '</select></td></tr>';

    html += '</table><tbody>';
    html += '</tbody></table></div>';
    html += '</table></div>';

    html += '<div id="dvInfoProfile"></dvi>';


    if (canModify) {
        

        if (destination == 2) {
            $('#optionModal').before('<a id="btnAction" class="btn btn-register-solve" style="background-color:green;" href="#" onclick="Subscribe();"><i class="fa fa-check-square" aria-hidden="true"> Importar Usuario</i></a>');
        } else if (destination == 1) {
            $('#optionModal').before('<a id="btnAction" class="btn btn-register-solve" style="background-color:green;" href="#" onclick="UpdateUser();"><i class="fa fa-check-square" aria-hidden="true"> Actualizar Usuario</i></a>');
        }
    }

    $(".solution-footer").css("display", "inherit");
    $('.infoModal').html(html);

}

function PrintSearch(listUser) {
    var html = "<label>Usuarios encontrados: </label>" + listUser.length;
    $.each(listUser, function (index, value) {
        html += "<div class='row'>";
        html += "<div class='col-sm-3'><label>Usuario: </label><input type='text' style='width: 100%' class='form-control' value= '" + value.UserName + "' disabled/></div>";
        html += "<div class='col-sm-3'><label>Nombre: </label><input type='text' style='width: 100%' class='form-control' value= '" + value.Name + "' disabled/></div>";
        html += "<div class='col-sm-3'><label>Correo: </label><input type='text' style='width: 100%' class='form-control' value= '" + value.Email + "' disabled/></div>";
        html += "<div class='col-sm-3'><span class='btn btn-success' style='margin-top: 10%;' onclick='OpenModalActiveUser(" + JSON.stringify(value) + ", 2)'><i class='fa fa-search' aria-hidden='true'></i> Importar</span></div>";
        html += "</div>";
    });
    $("#containerList").html(html);
}
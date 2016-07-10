var parametros =
    {
        general: {
            mensajeConfirmacion: "<li class='fa fa-warning'></li>&nbsp;Esta seguro que desea continuar?",
            mensajeSatistactorio: "<li class='fa fa-info'></li>&nbsp; Operación realizada con exito"
        }
    };


$(document).ready(function () {
    Globalize.culture('es-PE');
});


function mostrarNotificacionError(mensaje) {
    PNotify.removeAll();
    new PNotify({
        title: 'Respuesta Sistema!',
        type: "error",
        text: mensaje
    });
}


function mostrarNotificacion(data) {
    PNotify.removeAll();
    new PNotify({
        title: 'Respuesta Sistema!',
        type: (data.Exito) ? "success" : "error",
        text: data.Mensaje
    });
}


$.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
    // DataTables 1.10 compatibility - if 1.10 then `versionCheck` exists.
    // 1.10's API has ajax reloading built in, so we use those abilities
    // directly.
    if (jQuery.fn.dataTable.versionCheck) {
        var api = new jQuery.fn.dataTable.Api(oSettings);

        if (sNewSource) {
            api.ajax.url(sNewSource).load(fnCallback, !bStandingRedraw);
        }
        else {
            api.ajax.reload(fnCallback, !bStandingRedraw);
        }
        return;
    }

    if (sNewSource !== undefined && sNewSource !== null) {
        oSettings.sAjaxSource = sNewSource;
    }

    // Server-side processing should just call fnDraw
    if (oSettings.oFeatures.bServerSide) {
        this.fnDraw();
        return;
    }

    this.oApi._fnProcessingDisplay(oSettings, true);
    var that = this;
    var iStart = oSettings._iDisplayStart;
    var aData = [];

    this.oApi._fnServerParams(oSettings, aData);

    oSettings.fnServerData.call(oSettings.oInstance, oSettings.sAjaxSource, aData, function (json) {
        /* Clear the old information from the table */
        that.oApi._fnClearTable(oSettings);

        /* Got the data - add it to the table */
        var aData = (oSettings.sAjaxDataProp !== "") ?
            that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

        for (var i = 0 ; i < aData.length ; i++) {
            that.oApi._fnAddData(oSettings, aData[i]);
        }

        oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();

        that.fnDraw();

        if (bStandingRedraw === true) {
            oSettings._iDisplayStart = iStart;
            that.oApi._fnCalculateEnd(oSettings);
            that.fnDraw(false);
        }

        that.oApi._fnProcessingDisplay(oSettings, false);

        /* Callback user function - for event handlers etc */
        if (typeof fnCallback == 'function' && fnCallback !== null) {
            fnCallback(oSettings);
        }
    }, oSettings);
};

$.fn.dataTableExt.oApi.fnReloadData = function (oSettings, data) {
    this.fnClearTable();
    if (data.length > 0) this.fnAddData(data);
};

function getUrlFileDownload(id) {
    return sistema.url.descargaArchivo.replace("_id_", encodeURIComponent(id));
};

$.fn.fileInput = function (options) {
    return this.each(function () {
        var $input = $(this);
        var $parent = $input.parents(".input-group");
        var opts = $.extend({}, $.fn.fileInput.defaults, options);

        $input.removeAttr("data-disabled");
        $input.removeAttr("data-file");
        $input.removeAttr("data-namefile");

        $parent.children().not($input).remove();
        //seteando data
        $input.attr("data-disabled", opts.disabled);
        $input.attr("data-file", opts.file);
        $input.attr("data-namefile", opts.fileName.length > 0 ? opts.fileName : "file_" + $input.attr("id"));

        var disabledFile = $input.attr("data-disabled") == "false" ? false : true;
        var $htmlFile = $("<span class=\"input-group-btn\"><span class=\"btn btn-default btn-fileInput\">Examinar&hellip; <input type=\"file\" name=\"" + $input.data("namefile") + "\" id=\"" + $input.data("namefile") + "\"  value=' ' " + (disabledFile ? "disabled" : "") + "></span></span>");
        var $fileInput = $htmlFile.find(":file").eq(0);
        var $htmlLink = $("<a href='" + getUrlFileInput($input.attr("data-file")) + "' id='link_" + $input.attr("id") + "' title=\"Descargar\" class=\"alert-info input-group-addon no-tooltip\"><i class=\"fa fa-paperclip fa-fw fa-lg\"></i></a>");
        var $htmlDelete = $("<a href=\"javascript:;\" id='delete_" + $input.attr("id") + "' title=\"Eliminar\" class=\"alert-danger input-group-addon no-tooltip\"><i class=\"fa fa-trash-o fa-fw fa-lg\"></i></a>");

        $htmlFile.insertBefore($input);
        if ($input.attr("data-file") != "") $htmlLink.insertAfter($input);
        if ($input.attr("data-file") != "" && $input.attr("data-disabled") == "false") {
            $htmlDelete.insertAfter($input); $htmlDelete.on('click', deleteFile);
        }

        $htmlFile.on('change', '#' + $input.data("namefile"), changeFile);
        $htmlFile.on('click', '.btn-file:eq(0)', function () { if ($input.data("disabled")) return; $('input:file', this).get(0).click(); });

        function changeFile() {
            if ($input.data("disabled")) return;
            var files = this.files;
            $("#" + $input.data("namefile")).val();
            $input.val("");
            if (!files.length) { return; }
            var strVal = "";
            for (var i = 0, l = files.length; i < l; i++) strVal = strVal + files[i].name + ",";
            $input.val(strVal.substr(0, strVal.length - 1));
            $parent.find($htmlLink).remove();
            if ($input.val() != "") {
                $htmlDelete.insertAfter($input);
                $htmlDelete.on('click', deleteFile);
            }
        }

        function deleteFile() {
            $fileInput.replaceWith($fileInput.clone());
            $input.val("");
            $("#" + $input.data("namefile")).val();
            $parent.find($htmlLink).remove();
            $parent.find($htmlDelete).remove();
            $input.data("file", "");
            $.each(opts.ctrlCleanDelete, function (index) { $("#" + opts.ctrlCleanDelete[index]).val(""); });
        }

        function getUrlFileInput(id) {
            return opts.urlFileDownload == undefined ? getUrlFileDownload(id) : options.urlFileDownload;
        }

        return $input;
    });
};

$.fn.fileInput.defaults = {
    file: "",//Codigo del Archivo en BD
    fileName: "",//Nombre del type:file
    disabled: false,//Indicador si el control es "disabled"
    urlFileDownload: undefined,//Ruta de descarga del archivo
    ctrlCleanDelete: new Array(),//Controles a limpiar en el evento "Delete"
    refresh: false
};




$.fn.datepicker.defaults.format = "dd/mm/yyyy";
$.fn.datepicker.defaults.todayBtn = 'linked';
$.fn.datepicker.defaults.language = "es";
$.fn.datepicker.defaults.autoclose = true;
$.fn.datepicker.defaults.todayHighlight = true;



$.extend($.fn.dataTable.defaults, {
    language: {

        "sProcessing": "Procesando...",
        "sLengthMenu": "Mostrar _MENU_ registros",
        "sZeroRecords": "No se encontraron resultados",
        "sEmptyTable": "Ningún dato disponible en esta tabla",
        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        "sInfoPostFix": "",
        "sSearch": "Buscar:",
        "sUrl": "",
        "sInfoThousands": ",",
        "sLoadingRecords": "Cargando...",
        "oPaginate": {
            "sFirst": "Primero",
            "sLast": "Último",
            "sNext": "Siguiente",
            "sPrevious": "Anterior"
        },
        "oAria": {
            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
        }
    },
    "searching": false,
    "bServerSide": true,
    "bProcessing": true,
    "bAutoWidth": false
});



$.fn.isValid = function (hasTabs) {
    var form = this.closest('form');
    if (hasTabs) {
        form.data("validator").settings.ignore = '.tab-pane';
    }
    if (form.valid()) {
        return true;
    } else {
        var located;
        var ctrl;
        $(form).find('.input-validation-error:visible').each(function (index, ictrl) {
            located = true;
            ctrl = ictrl;
            return false;
        });
        if (!located && hasTabs) {
            $(form).find('.input-validation-error:hidden').each(function (index, ictrl) {
                var tab = $(ictrl).closest('.tab-pane');
                $('.nav-tabs a[href="#' + tab.attr("id") + '"]').tab('show');
                ctrl = ictrl;
                return false;
            });
        }
        $('html, body').animate({ scrollTop: $(ctrl).offset().top - 75 }, 'slow');
        $(ctrl).focus();
        return false;
    }
};


$.fn.updateValidation = function () {
    var form = this.closest("form").removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    return this;
};

$.fn.cleanValidation = function () {
    $(this).find(".field-validation-error").each(function () {
        $(this).removeClass("field-validation-error").addClass("field-validation-valid");
    });
    $(this).find(".input-validation-error").each(function () {
        $(this).removeClass("input-validation-error").addClass("valid");
    });
    $(this).find(".validation-summary-errors").each(function () {
        $(this).find("ul").empty();
        $(this).removeClass("validation-summary-errors").addClass("validation-summary-valid");
    });
    $(this).updateValidation();
};

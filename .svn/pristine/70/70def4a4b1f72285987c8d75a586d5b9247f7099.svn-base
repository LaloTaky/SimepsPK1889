var Indicador33 = {
    mapValorEntidad: function (data) {
        $('.ValorEntidad').text("Valor del indicador por entidad federativa");
        //SE RELACIONA LOS ESTADOS CON SU NOMENGLATURA MX
        var datos = Indicador33.MachEstados(data, "Entidad");

        //SE CREA EL MAPA CON LOS DATOS TRABAJADOS
        var map = AmCharts.makeChart("mapEntidad_", {
            "type": "map",
            "theme": "light",
            "projection": "miller",
            "zoomOnDoubleClick": false,
            "mouseEnabled": false,
            "dragMap": false,
            "zoomControl": {
                "zoomControlEnabled": false,
                "panControlEnabled": false,
                "homeButtonEnabled": false,
            },
            "areasSettings": {
                "autoZoom": false,
                "balloonText": "[[title]]: <b>[[value]]<b>",
                "color": "#E2EFDA",
                "colorSolid": "#375623",
            },
            "dataProvider": {
                "map": "mexicoSae",
                "getAreasFromMap": true,
                "areas": datos
            }
        });
    },
    mapPerformance: function (data) {
        $('.Desempenio').text("Cumplimiento de metas del indicador por entidad federativa");
        //SE RELACIONA LOS ESTADOS CON SU NOMENGLATURA MX
        var datos = Indicador33.MachEstados(data, "Municipio");

        //SE CREA EL MAPA CON LOS DATOS TRABAJADOS
        var map = AmCharts.makeChart("mapDes_", {
            "type": "map",
            "theme": "light",
            "projection": "miller",
            "zoomOnDoubleClick": false,
            "mouseEnabled": false,
            "dragMap": false,
            "zoomControl": {
                "zoomControlEnabled": false,
                "panControlEnabled": false,
                "homeButtonEnabled": false,
            },
            "areasSettings": {
                "autoZoom": false,
                "balloonText": "[[title]]: <b>[[value]]<b>",
                "color": "#D9E1F2",
                "colorSolid": "#203764",
            },
            "dataProvider": {
                "map": "mexicoSae",
                "getAreasFromMap": true,
                "areas": datos
            }
        });
    },
    MachEstados: function (datos, tipMap) {
        var data = [];
        $.each(datos, function (i, val) {
            var obj = {};
            obj.IdEstado = val.IdEstado;
            obj.IdIndicador = val.IdIndicador;
            obj.value = tipMap == "Entidad" ? val.MetaRel : val.PromedioMetas;
            obj.id = "MX-" + val.IdEstado;
            data.push(obj);
        });
        return data;
    }
}

$(document).ready(function () {
    console.log("Indicador R33...");

    var arrayDatos = $('.arrayjsonEnt').val().split("|");
    var arrayDatos2 = $('.arrayjsonDes').val().split("|");

    $.each(arrayDatos, function (index, value) {
        try {
            if (value != "") {
                var dataEntidad = JSON.parse(arrayDatos[index]);
                Indicador33.mapValorEntidad(dataEntidad);
            }
        } catch (e) {
            console.error(JSON.stringify(e));
        }
    });

    if (arrayDatos2.length > 0) {
        $.each(arrayDatos2, function (index, value) {
            try {
                if (value != "") {
                    var dataDesempeño = JSON.parse(arrayDatos2[index]);
                    Indicador33.mapPerformance(dataDesempeño);
                }
            }
            catch (e) {
                console.error(JSON.stringify(e));
            }

        });
    }
});
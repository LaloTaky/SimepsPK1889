/*creado por :AJS Fecha: 17/05/2016 
  Obtener los parámetros de la URL
  name = nombre del parámertro.
  url = string donde se encuentre el parámetro que se dea buscar
*/
function getParameterUrl(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

/*creado por :Devnet04 Fecha: 25/05/2018
  Obtener los parámetros de la URL
  name = nombre del parámertro.
  url = string donde se encuentre el parámetro que se dea buscar
*/
function fjsgetParameterInd(name) {
    
    var valor = '';
    var id = '';
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        id = inputs[i].name.toString();
        if (id == name) {
            valor = inputs[i].value.toString();
        }
    }

    return valor;
}

/*creado por :Devnet04 Fecha: 25/05/2018
  Obtener los parámetros de la URL
  name = nombre del parámertro.
  url = string donde se encuentre el parámetro que se dea buscar
*/
function fjsgetParameterInd(name) {
    
    var valor = '';
    var id = '';
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        id = inputs[i].name.toString();
        if (id == name) {
            valor = inputs[i].value.toString();
        }
    }

    return valor;
}




/*creado por :DEVNET01 Fecha: 21/05/2018
  cambia de icono en el acordeon para mostrar que esta desplegado.
*/
function fjschangerow(obj) {
    $(obj).find('span').eq(1).toggleClass('glyphicon-chevron-down').toggleClass('glyphicon-chevron-right');				 
}

/*creado por :DEVNET05 Fecha: 18/06/2018
  cambia el tamaño de columnas para que se muestre mejor la grafica de detalle del indicador.
*/
function MostrarOcultar() {
   var titulo = document.getElementById("atitulo");
   var DivDer = document.getElementById("IndicadoresdeFin");
   var btOcultarr = document.getElementById("btOcultar");
   var Divobjetivos = document.getElementById("idDivListObj");
   var btncollapse = document.getElementById("btnOptions");

   if (Divobjetivos.style.display != "none") {
      $("#idDivListObj").hide(200);
      DivDer.className = "col-md-12 detIndSecBackGrounds";
      btOcultarr.className = "glyphicon glyphicon-chevron-right";
      btncollapse.style.marginLeft = "-30px";
      btncollapse.style.cssFloat = "left";
      titulo.title = "Mostrar listado de objetivos";
      
   } else {
      $("#idDivListObj").show(200);
      DivDer.className = "col-md-8 detIndSecBackGrounds";
      btOcultarr.className = "glyphicon glyphicon-chevron-left";
      btncollapse.style.cssFloat = "right";
      titulo.title = "Colapsar sección a la izquierda";
   }
}

/*
creado por: AJS Fecha: 25/04/2016
Objetivo: Elimina los estilo de la página para que se abra de forma correcta en el dialog
*/
function removeStyleDialog() {
    var descargaIndicador = document.getElementById("linkDescargaFichaTecnica");
    var linkDescargaBaseIndicadores = document.getElementById("linkDescargaBaseIndicadores");
    var linkDescargaRepMonitoreo = document.getElementById("linkDescargaRepMonitoreo");

    if (descargaIndicador != null)
        descargaIndicador.style.display = "none";
    if (linkDescargaBaseIndicadores != null)
        linkDescargaBaseIndicadores.style.display = "none";
    if (linkDescargaRepMonitoreo != null)
        linkDescargaRepMonitoreo.style.display = "none";
}

/*
creado por: AJS Fecha: 20/07/2015
Objetivo: cuando se presiona ENTER en la busqueda por indicadores realiza el filtrado de la misma
*/
function EnterEvent(e) {
    if (e.keyCode == 13) {
        fjsUpdatePanel();
    }
}
/*
creado por: AJS Fecha: 08/07/2015
Objetivo: Obtiene el texto del input oculto y pasa el valor a la caja de texto principal.
*/
function fjsPassValue() {
    var valorText = document.getElementById("ctl00_PlaceHolderMain_texBuscador").value;
    document.getElementById("ctl00_PlaceHolderMain_texBuscaURL").value = valorText;
}
/*
creado por: AJS Fecha: 08/07/2015
Objetivo: Realiza una actualización del panel
*/

function fjsUpdatePanel() {
    document.getElementById("ctl00_PlaceHolderMain_BtnSubmit").click();
}
/*
creado por: AJS Fecha: 08/07/2015
Objetivo: Obtiene el texto de la URL y lo almacena en la caja de texto.
*/

function fjsSetValue() {
    var txt = document.getElementById("ctl00_PlaceHolderMain_texBuscador").value;
    if (txt == '') {
        var sUrl = location.toString();
        var valor = fjsRecuperaParametroPae(sUrl, 'txtBuscador');
        document.getElementById("ctl00_PlaceHolderMain_texBuscaURL").value = valor;
        document.getElementById("ctl00_PlaceHolderMain_texBuscador").value = valor;
    }
}

/*
creado por: AJS Fecha: 08/07/2015
Objetivo: Pasa el texto ingresado por el usuario como parámetro en la URI
*/
function fjsBuscaIndicador() {
    var textBuscador = document.getElementById("ctl00_PlaceHolderMain_texBuscador").value;
    if (textBuscador != '') {
        location.href = 'BuscaIndicador.aspx?txtBuscador=' + textBuscador;
    }
}
/*
creado por: AJS Fecha: 08/07/2015
Objetivo: Valida que la gráfica en el detalle del Indicador contenga al menos un registro, caso contrario muestra una leyenda
*/
function fjsVerificaDivInfo() {
    var menu = document.getElementById("ctl00_PlaceHolderMain_ddlGraficaInd");
    if (menu.options.length < 0) {
        document.getElementById("divSinInformacion").style.display = '';
    }
}


/*
Objetivo: Valida que se haya seleccionado un anio y si es el caso ejecutar la consulta para los mosaicos
ParÃ¡metros: bPostBack: Indicador Verdadero si es la segunda vez o n que se ejecuta el viaje al servidor
*/
function fjsValidaAnio(bPostBack) {
    var ddlAnio = document.getElementById('ctl00_PlaceHolderMain_ddlAnio');
    fjsBloquearPantalla("Cargando...");
    if (ddlAnio && ddlAnio.length < 1) {
        document.getElementById('divMosaico').style.display = 'none';
        return false;
    }
    else {
        var txtAnioSelected = document.getElementById('txtAnioSelected');
        var anioTxt = '';
        if (txtAnioSelected && txtAnioSelected.value != '')
            anioTxt = txtAnioSelected.value;

        if (!bPostBack)
            fjsInicializaAnio(ddlAnio, anioTxt);

        //Prepara la funciÃ³n para que ejecute el pintado del Mosaico despues de ejecutar el __DoPostBack
        var requestManager = Sys.WebForms.PageRequestManager.getInstance();

        function EndRequestHandler(sender, args) {
            fjsCargarMosaico(ddlAnio);
            requestManager.remove_endRequest(EndRequestHandler);
            fjsDesbloquearPantalla();

        }
        requestManager.add_endRequest(EndRequestHandler);

        if (!bPostBack)
            __doPostBack(ddlAnio.id, '');
    }
}

/*
 *Asigna el aÃ±o actual en el ddl de anio cuando se carga la pagina por primera vez
 */
function fjsInicializaAnio(control, anioCache) {
    var fecha = new Date();
    var anio = fecha.getFullYear();

    if (anioCache != '')
        anio = anioCache;

    for (var i = 0; i < control.options.length; i++) {
        if (control.options[i].value == anio) {
            control.options[i].selected = true;
            break;
        } else
            control.options[control.options.length - 1].selected = true;
    }
}

/*Bloquea la pantalla para que no sea posible modificar nada por los usuarios*/
function fjsBloquearPantalla(str) {
    scroll(0, 0);
    var back = document.getElementById('skm_LockBackground');
    var pane = document.getElementById('skm_LockPane');
    var text = document.getElementById('skm_LockPaneText');

    if (back) { back.className = 'LockBackground'; back.style.display = 'block'; }
    if (pane) { pane.className = 'LockPane'; pane.style.display = 'block'; }
    if (text) text.innerHTML = str;
}

/* Desbloquea la pantalla */
function fjsDesbloquearPantalla() {
    scroll(0, 0);
    var back = document.getElementById('skm_LockBackground');
    var pane = document.getElementById('skm_LockPane');
    var text = document.getElementById('skm_LockPaneText');

    if (back) { back.className = 'LockOff'; back.style.display = 'none'; }
    if (pane) { pane.className = 'LockOff'; pane.style.display = 'none'; }
}

function fjsObtenerAnioSelected(ddlAnio) {
    if (ddlAnio) {
        var txtAnioSelected = document.getElementById('txtAnioSelected');
        if (txtAnioSelected)
            txtAnioSelected.value = ddlAnio.options[ddlAnio.selectedIndex].value;
        return ddlAnio.options[ddlAnio.selectedIndex].value;
    }
    return "-1";
}

function fjsObtenerRamo(ramoUnidad) {
    var ramo = 0;
    if (ramoUnidad.length > 0) {
        ramo = ramoUnidad.split('-')[0];
        if (ramo.indexOf('.') > -1)
            ramo = ramo.substring(0, ramo.indexOf('.'));
    }
    return ramo;
}

function fjsObtenerUnidad(ramoUnidad) {
    var unidad = 0;
    if (ramoUnidad.length > 0) {
        unidad = ramoUnidad.split('-')[1];

        if (!unidad)
            unidad = ramoUnidad.split('-')[0];

        if (unidad.indexOf('.') > -1)
            unidad = unidad.substring(0, unidad.indexOf('.'));
    }
    return unidad;
}

/*Objetivo: Cargar los componentes necesarios para la correcta visualizaciÃ³n de la informaciÃ³n del mosaico seleccionado*/
function fjsCargarProgramas() {
    setTimeout(function () { fjsDesbloquearPantalla(); }, 200);
    /*alert("Cargando lista de programas");*/
}

/*Objetivo: Cargar los componentes necesarios para la correcta visualizaciÃ³n de la informaciÃ³n de un programa seleccionado*/
function fjsCargaPrograma() {
    //fjsAsignarNombrePrograma();
    fjsCargarGraficaIndicadores('ddlGraficaProp');
    fjsCargarGraficaIndicadores('ddlGraficaComp');
}

function fjsCargaIndicadores() {
    var spnIndProp = document.getElementById('spnIndProposito');
    var spnIndComp = document.getElementById('spnIndComponente');
    var ddlIndProp = document.getElementById('ctl00_PlaceHolderMain_ddlIndProp');
    var ddlIndComp = document.getElementById('ctl00_PlaceHolderMain_ddlIndComp');
    if (spnIndProp && ddlIndProp && ddlIndProp.length > 0)
        spnIndProp.innerHTML = ddlIndProp.options[ddlIndProp.selectedIndex].text;
    if (spnIndComp && ddlIndComp && ddlIndComp.length > 0)
        spnIndComp.innerHTML = ddlIndComp.options[ddlIndComp.selectedIndex].text;

}

function fjsAsignarNombrePrograma() {
    var spnNomProg = document.getElementById('spnNombrePrograma');
    var sUrl = location.toString();
    spnNomProg.innerHTML = fjsRecuperaParametroPae(sUrl, 'pPrograma');

}


function fjsValidarDescargaZip(url, Clave, Modalidad, Nivel, urlZip) {
    
    if (Clave.length == 1)
        var clave = "00" + Clave;
    else if (Clave.length > 1 && Clave.length < 3)
        var clave = "0" + Clave;
    else
        var clave = Clave;

    //var urlZip = '<%=ConfigurationManager.AppSettings["urlZip"]%>';

    var sUrl = location.toString();
    var parametroCiclo = fjsRecuperaParametroPae(sUrl, 'ciclo');
    var parametroRamo = fjsRecuperaParametroPae(sUrl, 'ramo');
    var parametroT = fjsRecuperaParametroPae(sUrl, 't')
    

    if (parametroRamo.length == 1)
        var ramo = "0" + parametroRamo;
    else
        var ramo = parametroRamo;


    if (Nivel == "3" && (parametroT == "c" || parametroT == "C"))
    {
        //window.open(urlZip + parametroCiclo + "/Ficha Evolución MIR " + parametroCiclo + ramo + Modalidad + clave + ".zip");
        var sUrlValidacion = sUrl + '&descarga=1&modalidad=' + Modalidad + '&clave=' + clave;
        window.location.href = sUrlValidacion;
    }
}

//function fjsDescargaZip(UrlDescarga) {
//    window.open(UrlDescarga);
//    window.location.href = sUrl;
//}

function fjsCargaDependenciaYCiclo() {
    var LblDependecia = document.getElementById('ContentPlaceHolder1_LblDependencia');
    var LblCiclo = document.getElementById('ContentPlaceHolder1_LblCiclo');
    var sUrl = location.toString();
    var camino = fjsRecuperaParametro('t');
    if (camino == 'c') {
       LblDependecia.innerHTML = document.getElementById('ContentPlaceHolder1_grvProgramaC_HdDependencia_0') != null ?document.getElementById('ContentPlaceHolder1_grvProgramaC_HdDependencia_0').value:"";
    } else {
       LblDependecia.innerHTML = document.getElementById('ContentPlaceHolder1_gvAB_HdDependencia_0') != null ? document.getElementById('ContentPlaceHolder1_gvAB_HdDependencia_0').value:"";
    }    
    LblCiclo.innerHTML = fjsRecuperaParametro('ciclo');
    
}

/*Objetivo: Recupera el valor del parametro especificado*/
function fjsRecuperaParametroPae(sUrl, sParam) {
    var sValor = '';
    sValor = getParameterUrl(sParam, sUrl);
    return sValor;
}
function fjsCarousel() {

   jQuery('.carousel[data-type="multi"] .item').each(function () {
      var $contPosicion = $('.carousel[data-type="multi"] .item').next(NodeList).length;

         var next = jQuery(this).next();
         if (!next.length) {
            next = jQuery(this).siblings(':first');

         }
         next.children(':first-child').clone().appendTo(jQuery(this));
        
         for (var i = 0; i < ($contPosicion < 4 ? $contPosicion - 1 : 4); i++) {
            next = next.next();
            if (!next.length) {
               next = jQuery(this).siblings(':first');

            }
            next.children(':first-child').clone().appendTo($(this));

         }
   });
}

function fjsCargarGraficaIndicadorSectorial(ddlPattern, divPrint, iBanInd) {
    var colSelect = document.getElementsByTagName('select');
    for (var iIndex = 0; iIndex < colSelect.length; iIndex++) {
        if ((colSelect[iIndex].id).indexOf(ddlPattern) > -1)
            fjsGraficarIndicadorSectorial(colSelect[iIndex], false, divPrint, iBanInd);

    }
}

function fjsGraficarIndicadorSectorial(ddl, showScroll, divPrint, iBanInd) {
    /*if (ddl.length > 0) {
        var valueJson = ddl.options[0].text;
        if (valueJson && valueJson.length > 0) {
            valueJson = valueJson.split(',')[0];
            //alert(valueJson);
            if (valueJson.indexOf(':"') > -1) {
                valueJson = valueJson.substring(valueJson.indexOf(':"') + 2, valueJson.length - 1);
                //alert(valueJson); 
            }
            //var idDivGrafica = valueJson;
            if (valueJson && valueJson != '') {
                var chartDataDer = fjsGeneraArregloGrafica(ddl);
                var titleGrafica = "";
                var arr = new Array(0);

                for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
                    arr.push(eval(+ddl.options[iIndex].value));

                var AniomaxClasif = Math.max.apply(null, arr);
                var AniominClasif = Math.min.apply(null, arr);
                if (AniomaxClasif != null && AniominClasif != null)
                    titleGrafica = "Histórico ";
                fjsGeneraHistogramaIndicadoresSectoriales(chartDataDer, 'CICLO', 'LineaBase', 'MetaAlcanzada', 'MetaIntermedia', 'Meta2018', divPrint, titleGrafica, showScroll, iBanInd);
                //for (var i = 0; i < ddl.options.length; i++) {
                //}
            }
        }
    }
    */

    var chartDataDer = fjsGeneraArregloGrafica(ddl);
    var titleGrafica = "";
    var arr = new Array(0);

    for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
        arr.push(eval(+ddl.options[iIndex].value));

    var AniomaxClasif = Math.max.apply(null, arr);
    var AniominClasif = Math.min.apply(null, arr);
    if (AniomaxClasif != null && AniominClasif != null)
        titleGrafica = "Histórico ";
    fjsGeneraHistogramaIndicadoresSectoriales(chartDataDer, 'CICLO', 'LineaBase', 'MetaAlcanzada', 'MetaIntermedia', 'Meta2018', divPrint, titleGrafica, showScroll, iBanInd);

}

function fjsCargarGraficaIndicadores(ddlPattern) {
    var colSelect = document.getElementsByTagName('select');
    for (var iIndex = 0; iIndex < colSelect.length; iIndex++) {
        if ((colSelect[iIndex].id + 'X').indexOf(ddlPattern) > -1)
            fjsGraficarIndicador(colSelect[iIndex], false);

    }
}

function fjsGraficarIndicador(ddl, showScroll) {
    if (ddl.length > 0) {
        var valueJson = ddl.options[0].text;
        if (valueJson && valueJson.length > 0) {
            valueJson = valueJson.split(',')[0];
            //alert(valueJson);
            if (valueJson.indexOf(':"') > -1) {
                valueJson = valueJson.substring(valueJson.indexOf(':"') + 2, valueJson.length - 1);
                //alert(valueJson); 
            }
            var idDivGrafica = valueJson;
            if (idDivGrafica && idDivGrafica != '') {
                var chartDataDer = fjsGeneraArregloGrafica(ddl);
                fjsGeneraHistogramaComparativa(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', idDivGrafica, 'Histórico (Gráfica Lineal)', showScroll);
                //for (var i = 0; i < ddl.options.length; i++) {
                //}
            }
        }
    }
}

function fjsGraficaIndicador() {
    //Prepara la funciÃ³n para que ejecute el pintado de la tabla despues de ejecutar el __DoPostBack
    //var requestManager = Sys.WebForms.PageRequestManager.getInstance();

    //  function EndRequestHandler(sender, args) {

    fjsBloquearPantalla("Cargando...");
    //Construye la grafica de barras de indicadores
    var chartDataDer = fjsGeneraArregloGrafica(document.getElementById('ctl00_PlaceHolderMain_ddlGrafica'));
    fjsGeneraHistogramaComparativa(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', 'chartInd', 'Histórico', true);

    fjsDesbloquearPantalla();

    // requestManager.remove_endRequest(EndRequestHandler);
    //}
    //requestManager.add_endRequest(EndRequestHandler);
}

/*
Objetivo: Generar el arreglo con los datos que deberÃ¡n de mostrarse en la grÃ¡fica
ParÃ¡metros: ddl: control DropDownList donde se ejecutÃ³ la consulta
*/
function fjsGeneraArregloGrafica(ddl) {
    var chartData = new Array(0);
    var aColors = new Array(0);
    for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
        if (ddl.options[iIndex].text != "") {
            chartData.push(eval('(' + ddl.options[iIndex].text + ')'));
        }
    return chartData;
}


function fjsCargaDetalleIndicador() {
    fjsBloquearPantalla("Cargando...");
    var nivelConsultado = fjsRecuperaParametroPae(location.href, 'pNivel');
    var ciclo = fjsRecuperaParametro('pCiclo');
    var spnNivelSeleccionado = document.getElementById('spnTituloIndicador');
    if (spnNivelSeleccionado) {
        var sHTML = '<table style="width: 100%;"><tr><td ';
        sHTML += nivelConsultado == 2 ? ' class="headerProposito headerTituloNivel" ' : (nivelConsultado == 3 ? ' class="headerComponente headerTituloNivel" ' : '');
        sHTML += '>';
        sHTML += nivelConsultado == 2 ? 'Indicador del objetivo del programa' : (nivelConsultado == 3 ? 'Indicador del Apoyo del programa (Componente)' : '');
        sHTML += '</td></tr>';
        spnNivelSeleccionado.innerHTML = sHTML;
    }
    var tituloNivel = document.getElementById('ContentPlaceHolder1_LblIndicador');
    if (tituloNivel) {
        tituloNivel.innerHTML = nivelConsultado == 2 ? ' Indicador de proposito ' + ciclo : (nivelConsultado == 3 ? ' Indicador de componente ' + ciclo : 'Indicador ' + ciclo)
    }
    var nombreIndicador = document.getElementById('ContentPlaceHolder1_LblTituloInd');
    if (nombreIndicador) {
        nombreIndicador.innerHTML = document.getElementById('ContentPlaceHolder1_rprDetalleInd_lblNombreInd_0').innerHTML;
    }
    fjsDibujarGraficaInd('lineal', 'divGrafica');
    fjsDesbloquearPantalla();

}

function fjsDibujarGraficaInd(tipo, divToPrintChart) {
   var NombreIndicador = $("#ContentPlaceHolder1_LblTituloInd").text();

    var chartDataDer = fjsGeneraArregloGrafica(document.getElementById('ContentPlaceHolder1_ddlGraficaInd'));
    
    if (tipo == 'barra')
       fjsGeneraBarraComparativa(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', divToPrintChart, NombreIndicador);
    if (tipo == 'lineal')
       fjsGeneraHistogramaComparativa(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', divToPrintChart, NombreIndicador, true);
}

function fjsMostrarGrafica(tipo) {
    var lineal = document.getElementById('divGrafica');
    if (lineal) {
        lineal.innerHTML = '';
        fjsDibujarGraficaInd(tipo, 'divGrafica');
    }
}

function fjsMostrarGraficaHome(tipo, divPrint, ContentPlaceControl) {
    var lineal = document.getElementById(divPrint);
    if (lineal) {
        lineal.innerHTML = '';
        fjsGenerargrafico(tipo, divPrint, ContentPlaceControl);
    }
}

function fjsGenerargrafico(tipo, divToPrintChart, ContentPlaceControl) {
    var chartDataDer = fjsGeneraArregloGrafica(document.getElementById(ContentPlaceControl));

    var ddls = document.getElementById('ContentPlaceHolder1_ddlTotalClasificados');
    var ddl = document.getElementById('ContentPlaceHolder1_ddlTotalIndicadores');
    var arrT = new Array(0);
    var arr = new Array(0);

    for (var iIndex = 0; iIndex < ddls.options.length; iIndex++)
        arr.push(eval(+ddls.options[iIndex].value));

    var AniomaxClasif = Math.max.apply(null, arr);
    var AniominClasif = Math.min.apply(null, arr);

    for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
        arrT.push(eval(+ddl.options[iIndex].value));

    var AniomaxTot = Math.max.apply(null, arrT);
    var AniominTot = Math.min.apply(null, arrT);

    var Total = new Array(0);
    var Tot = new Array(0);

    for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
        Total.push(ddl.options[iIndex].text.substring(41, 36).replace('"', ""));

    var ValorMax = Math.max.apply(null, Total);
    var ValorMin = Math.min.apply(null, Total);

    if (tipo == 'IndicaClasificados')
       fjsGenerarConteoIndicadores("Estructura de los indicadores de los programas sociales por objetivos", chartDataDer, 'ciclo', 'resultados', 'servicio', 'gestion', divToPrintChart);
    if (tipo == 'IndicaTotal')
        fjsGenerarTotalIndicadores("Histórico del Número de Indicadores " + AniominTot + " - " + AniomaxTot, chartDataDer, 'ciclo', 'TotalIndicadores', divToPrintChart, AniominTot, AniomaxTot, ValorMax, ValorMin);
}

/*TOOD: ELiminar si ya no se ocupa*/
function fjsCargarTablaIndicadorHist() {
    var ddl = document.getElementById('ctl00_PlaceHolderMain_ddlIndicadorHist');
    var divIndHist = document.getElementById('divTablaIndicadorHist');
    var sHTML = '';
    if (ddl) {
        for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
            sHTML += ddl.options[iIndex].text;
        divIndHist.innerHTML = sHTML;
    }
}

function fjsExcelReport(tabla, nombreArchivo) {
    var tab_text = "<table border='2px'><tr style='background:#1886F0'><td>";
    var textRange; var j = 0;

    tab = document.getElementById(tabla); // id of table
    for (j = 0 ; j < tab.rows.length ; j++) {
        //tab_text += "<tr><td>";
        if (j != 0)
            tab_text += '<tr><td>';
        tab_text += tab.rows[j].innerHTML + "</td></tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    //tab_text= tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    //tab_text= tab_text.replace(/<img[^>]*>/gi,""); // remove if u want images in your table
    //tab_text= tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, "HistoricoIndicador.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
    return (sa);
}
/*
function fjsMuestraPrograma(idMatriz,unidad,nomProgrma){
  var href =  "/Evaluacion/Paginas/paemir/Programa.aspx?pIdMatriz="+idMatriz+"&pUnidad="unidad+"&pPrograma="+nomProgrma;
  return href;
}*/


//Objetivo: EnvÃ­Â­a los parÃ¡mtros requeridos del reporte con los criterios seleccionados
function fjsAplicaFiltros() {
    //var sPrefijoFiltro = 'ctl00_PlaceHolderMain_';
    //var sPrefijoReporte='ctl00_m_g_a408b807_9dc3_4100_ba06_53dbc69584ce_ctl00_ctl17_ctl06_';
    var iIndex = -1;
    var inputs = document.getElementsByTagName('input');
    var fIni = -1; //Rango Inicial
    var fFin = -1; //Rango final
    var sUrl = location.toString();
    fjsBloquearPantalla('Cargando...');
    for (i = 0; i < inputs.length; i++) {
        if (inputs[i].type == 'text' && (inputs[i].id + 'X').indexOf('ReportViewer') == -1 && (inputs[i].id + 'X').indexOf('txtValue') > -1) {
            iIndex++;
            inputs[i].value = fjsRecuperaParametroPae(sUrl, 'pIdIndicador');
        }
        else if (inputs[i].type == 'submit' && (inputs[i].value + 'X').indexOf('View Report') > -1) {
            //Prepara la funciÃ³n para que ejecute el pintado de la tabla despues de ejecutar el __DoPostBack
            var requestManager = Sys.WebForms.PageRequestManager.getInstance();

            function EndRequestHandler(sender, args) {
                fjsOcultaParametros();

                fjsDesbloquearPantalla();
                requestManager.remove_endRequest(EndRequestHandler);
            }
            requestManager.add_endRequest(EndRequestHandler);
            inputs[i].click();
            document.getElementById('divReporte').style.left = '0px';
        }
    }
}

function fjsOcultaParametros() {
    var divs = document.getElementsByTagName('div');
    for (var i = 0; i < divs.length; i++) {
        if ((divs[i].id + 'X').indexOf('ctl01_ToggleParam') > -1) {
            divs[i].parentElement.parentElement.style.display = 'none';
            //divs[i].style.color="white";
            break;
            // alert(divs[i].id);
        }
    }
}

function mostrarPrograma(url) {

   var sUrl = location.toString();
   var parametroCiclo = fjsRecuperaParametroPae(sUrl, 'ciclo');
   var parametroRamo = fjsRecuperaParametroPae(sUrl, 'ramo');
   var parametroT = fjsRecuperaParametroPae(sUrl, 't')

   window.location = url + '&pCiclo=' + parametroCiclo + '&pRamo=' + parametroRamo;
}
/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Agrega los parametros necesarios para poder realizar la descarga de el excel de Ficha de indicadores
*/
function fjsDescargaFichaIndicadores(link, indicador) {
    fjsBlockUIForDownload(link);
    var sUrl = location.toString();
    var sUrlDescarga = "";
    var parametroUniversoPaemir = "";
    var parametroMatriz = fjsRecuperaParametroPae(sUrl, 'pIdMatriz');
    var parametroCiclo = fjsRecuperaParametroPae(sUrl, 'pCiclo');
    var parametroRamo = fjsRecuperaParametroPae(sUrl, 'pRamo');
    parametroUniversoPaemir = fjsRecuperaParametroPae(sUrl, 'pUniP');
    if (parametroUniversoPaemir == null) parametroUniversoPaemir = "";
    else parametroUniversoPaemir= "&pUniP=" + parametroUniversoPaemir

    if (indicador) {
        var parametroIndicador = fjsRecuperaParametroPae(sUrl, 'pIdIndicador');
        var parametroNivel = fjsRecuperaParametroPae(sUrl, 'pNivel');
        sUrlDescarga = "FichaIndicadores.aspx" + "?pIdMatriz=" + parametroMatriz + "&pCiclo=" + parametroCiclo + "&pRamo=" + parametroRamo + '&pIdIndicador=' + parametroIndicador + '&pNivel=' + parametroNivel + parametroUniversoPaemir;
    } else {
       sUrlDescarga = "FichaIndicadores.aspx" + "?pIdMatriz=" + parametroMatriz + "&pCiclo=" + parametroCiclo + "&pRamo=" + parametroRamo + parametroUniversoPaemir;
    }

    window.location = sUrlDescarga;
}

/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Agrega los parametros necesarios para poder realizar la descarga de el excel de Reporte de monitoreo
*/
function fjsDescargaRepMonitoreo(link, indicador) {
    fjsBlockUIForDownload(link);
    var sUrl = location.toString();
    var sUrlDescarga = "";
    var parametroMatriz = fjsRecuperaParametroPae(sUrl, 'pIdMatriz');
    var parametroCiclo = fjsRecuperaParametroPae(sUrl, 'pCiclo');
    var parametroRamo = fjsRecuperaParametroPae(sUrl, 'pRamo');

    if (indicador) {
        var parametroIndicador = fjsRecuperaParametroPae(sUrl, 'pIdIndicador');
        var parametroNivel = fjsRecuperaParametroPae(sUrl, 'pNivel');
        sUrlDescarga = "Monitoreo.aspx" + "?pIdMatriz=" + parametroMatriz + "&pCiclo=" + parametroCiclo + "&pRamo=" + parametroRamo + '&pIdIndicador=' + parametroIndicador + '&pNivel=' + parametroNivel;
    } else {
        sUrlDescarga = "Monitoreo.aspx" + "?pIdMatriz=" + parametroMatriz + "&pCiclo=" + parametroCiclo + "&pRamo=" + parametroRamo;
    }
    window.location = sUrlDescarga;
}

/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Agrega los parametros necesarios para entrar a la pantalla que muestra el detalle del indicador.
*/
function mostrarIndicador(url) {
    var sUrl = location.toString();
    var parametroMatriz = fjsRecuperaParametroPae(sUrl, 'pIdMatriz');
    var parametroCiclo = fjsRecuperaParametroPae(sUrl, 'pCiclo');
    var parametroRamo = fjsRecuperaParametroPae(sUrl, 'pRamo');
    var nuevoLink = document.createElement("a");
    window.location = url + '&pIdMatriz=' + parametroMatriz + '&pCiclo=' + parametroCiclo + '&pRamo=' + parametroRamo;
}

/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Agrega los parametros necesarios para poder realizar la descarga de el excel de Base de indicadores
tipo : Parametro para indentificar el tipo de reporte a emitir.
         tipo = 1.- Reporte emitido por fitros
         tipo = 2.- Reporte emitido por historico
         tipo = 3.- Reporte emitido por ciclos
         tipo = 4.- Reporte emitido por fitros en formato CSV
*/
function fjsDescargaBaseIndicadores(link, tipo) {
   fjsBlockUIForDownload(link);
   var sUrl = location.toString();
   var sUrlDescarga = "";
   var parametroMatriz = 0;
   var parametroCiclo = 0;
   var parametroRamo = 0;
   var parametroPantalla = '';
   

   if (tipo == 1) {
      parametroPantalla = 'PROGRAMA';
      parametroMatriz = fjsRecuperaParametroPae(sUrl, 'pIdMatriz');
      parametroCiclo = fjsRecuperaParametroPae(sUrl, 'pCiclo');
      parametroRamo = fjsRecuperaParametroPae(sUrl, 'pRamo');
   }
   else if (tipo == 3) {
      parametroPantalla = 'FIN';
      if ($("#hddCicloMosaico").length) {
         parametroCiclo = $("#hddCicloMosaico").val();
      }
   }
   sUrlDescarga = "BDIndicadoresProgramaRS.aspx" + "?pIdMatriz=" + parametroMatriz + "&pCiclo=" + parametroCiclo + "&pRamo=" + parametroRamo + "&pPantalla=" + parametroPantalla;
   window.location = sUrlDescarga;
}

function fjsDescargaBaseIndicadoresCSV(link,tipo) {
    var sUrl = location.toString();
    var sUrlDescarga = "";
    var parametroMatriz = fjsRecuperaParametroPae(sUrl, 'pIdMatriz');
    var parametroCiclo = fjsRecuperaParametroPae(sUrl, 'pCiclo');
    var parametroRamo = fjsRecuperaParametroPae(sUrl, 'pRamo');
    if (tipo == 1) {
        sUrlDescarga = "BDIndicadoresProgramaCSV.aspx" + "?pIdMatriz=" + (parametroMatriz == null ? -1 : parametroMatriz) + "&pCiclo=" + (parametroCiclo == null ? -1 : parametroCiclo) + "&pRamo=" + (parametroRamo == null ? -1 : parametroRamo) + "&pTipo=" + "PROGRAMA";
    }
    if (tipo == 2) {
        if ($("#hddCicloMosaico").length) {
            parametroCiclo = $("#hddCicloMosaico").val();
        }
        sUrlDescarga = "BDIndicadoresProgramaCSV.aspx" + "?pIdMatriz=" + (parametroMatriz == null ? -1 : parametroMatriz) + "&pCiclo=" + (parametroCiclo == null ? -1 : parametroCiclo) + "&pRamo=" + (parametroRamo == null ? -1 : parametroRamo) + "&pTipo=" + "FIN";
    }
        
    window.location = sUrlDescarga;
}


/*
creado por: DevASp3 Fecha: 21/08/2017
Objetivo: Establece la ruta del excel de Base de indicadores historico y total por ruta 
*/
function fjsEstableceUrlReportesSIPS() {
    var sUrlDescargaHco = "", sUrlDescargatod = "";
    var parametroCiclo = 0;

    var parametroRutaIndicadoresApro = document.getElementById("ContentPlaceHolder1_HdnRutaHistoricoAproInd").value;
    if (parametroRutaIndicadoresApro.length > 0) {
        var xlsHcoIndApro = document.getElementById("xlsHcoIndApro");
        var csvHcoIndApro = document.getElementById("csvHcoIndApro");
        xlsHcoIndApro.href = parametroRutaIndicadoresApro + ".xls";
        csvHcoIndApro.href = parametroRutaIndicadoresApro + ".csv";
    }
        
    else{
        parametroCiclo = document.getElementById("hddCicloMosaico").value;

        var parametroRutaHco = document.getElementById("ContentPlaceHolder1_HdnRutaHistorico").value;
        var parametroRutaIndicadores = document.getElementById("ContentPlaceHolder1_HdnRutaTodos").value;

        sUrlDescargaHco = parametroRutaHco + parametroCiclo;
        sUrlDescargatod = parametroRutaIndicadores + parametroCiclo;

        var xlsHco = document.getElementById("xlsHcoInd");
        var csvHco = document.getElementById("csvHcoInd");
        var xlsTotal = document.getElementById("xlsTodosInd");
        var csvTotal = document.getElementById("csvTodosInd");
        xlsHco.href = sUrlDescargaHco + ".xls";
        csvHco.href = sUrlDescargaHco + ".csv";
        xlsTotal.href = sUrlDescargatod + ".xls";
        csvTotal.href = sUrlDescargatod + ".csv";
    }
}
/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Sirve para detener la ejecución normal del link y poder agregar los parametros a la url
*/
function fjsDetenerEvento(event) {
    event.preventDefault();
}

/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Permite revisar si un archivo ya se ha descargado correctamente
*/
var fileDownloadCheckTimer;
var listaBotones = "";

/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Obtiene el valor de una cookie pasando como parametro el nombre
*/
function fjsGetCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Permite establece el valor de una cookie pasando como parametro en nombre y el valor
*/
function fjsSetCookie(cname, cvalue) {
    var d = new Date();
    d.setTime(d.getTime());
    document.cookie = cname + "=" + cvalue + ";path=/";
}

/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Bloquea la pantalla mientras se realiza la descarga de un archivo
*/
function fjsBlockUIForDownload(link) {
    if (navigator.cookieEnabled) {
        fjsSetCookie("fileDownloadToken", "false");
        //$('#descargaModal').modal('show');

        var btnsDescarga = document.getElementById("divBotones").getElementsByTagName("input");
        for (var i = 0; i < btnsDescarga.length; i++) {
            btnsDescarga[i].disabled = true;
            listaBotones = listaBotones + btnsDescarga[i].id + "=" + btnsDescarga[i].value + ",";
        }

        link.value = "Descargando archivo...";
        fileDownloadCheckTimer = setInterval(function () {
            var cookieValue = fjsGetCookie("fileDownloadToken");
            if (cookieValue == "true") {
                fjsFinishDownload();
            }
        }, 1000);
    }
}

/*
creado por: JPL Fecha: 16/06/2016
Objetivo: Desbloquea y limpia la variable que realiza la consulta del valor de la cookie en intervalos de tiempo
*/
function fjsFinishDownload() {
    window.clearInterval(fileDownloadCheckTimer);
    //$('#descargaModal').modal('hide');

    var btnsDescarga = document.getElementById("divBotones").getElementsByTagName("input");
    for (var i = 0; i < btnsDescarga.length; i++) {
        var textosBotones = listaBotones.split(",");
        for (var j = 0; j < textosBotones.length; j++) {
            if (textosBotones[j].split("=")[0] == btnsDescarga[i].id) {
                btnsDescarga[i].value = textosBotones[j].split("=")[1];
                break;
            }
        }

        btnsDescarga[i].disabled = false;
    }
}
/*
Creado por: AJS Fecha: 31/05/2016
Objetivo: Descarga el archivo de MIR 
*/
function fjsDescargaMIR(link) {
    fjsBlockUIForDownload(link);
    var sUrl = location.toString();
    var sUrlDescarga = "";
    var parametroMatriz = fjsRecuperaParametroPae(sUrl, 'pIdMatriz');
    var parametroCiclo = fjsRecuperaParametroPae(sUrl, 'pCiclo');
    var parametroRamo = fjsRecuperaParametroPae(sUrl, 'pRamo');

    sUrlDescarga = "DescargaMIR.aspx" + "?pIdMatriz=" + parametroMatriz + "&pCiclo=" + parametroCiclo + "&pRamo=" + parametroRamo;
    window.location = sUrlDescarga;
}

function fjsDescargaFTIndicador(link) {
    fjsBlockUIForDownload(link);
    var sUrl = location.toString();
    var sUrlDescarga = "";
    var idPrograma = fjsgetParameterInd('ctl00$ContentPlaceHolder1$inputIdProgSec');
    var idIndicador = fjsgetParameterInd('ctl00$ContentPlaceHolder1$inputIdIndicadorSec');
    sUrlDescarga = "DescargarFTIndicador.aspx?id=" + idPrograma + "&idIndicador=" + idIndicador;
    window.location = sUrlDescarga;
}

function fjsDescargaFTIndicadores(link) {
    fjsBlockUIForDownload(link);
    var sUrl = location.toString();
    var sUrlDescarga = "";
    var idPrograma = fjsgetParameterInd('ctl00$ContentPlaceHolder1$inputIdProgSec');
    var idIndicador = fjsgetParameterInd('ctl00$ContentPlaceHolder1$inputIdIndicadorSec');
    sUrlDescarga = "DescargarFTIndicadores.aspx?id=" + idPrograma + "&idIndicador=" + idIndicador;
    window.location = sUrlDescarga;
}
function fjsDescargaBaseDatos(link, tipo) {
    fjsDescargaBaseDatosAll(link, tipo, "false");
}

function fjsDescargaBaseDatosAll(link, tipo, fullProgramas) {
    var sUrl = location.toString();
    var sUrlDescarga = "";
    var idPrograma = fjsgetParameterInd('ctl00$ContentPlaceHolder1$inputIdProgSec');
    var idIndicador = fjsgetParameterInd('ctl00$ContentPlaceHolder1$inputIdIndicadorSec');
    sUrlDescarga = "DescargarBasePND.aspx?" + (fullProgramas == "true" ? "id=" + "-1" + "&idIndicador=" + "-1" + "&tipo=" + tipo : "id=" + idPrograma + "&idIndicador=" + idIndicador + "&tipo=" + tipo);
   /*https://www.coneval.org.mx/coordinacion/Documents/monitoreo/Bases_SIPOL/Sectoriales/Base_de_Datos_del_PND_2017.xls; */
    window.location = sUrlDescarga;
}

function fjsDescargaBaseDatosAllPND19_24() {
    var sUrl = location.toString();
    var sUrlDescarga = "https://www.coneval.org.mx/coordinacion/Documents/monitoreo/Bases_SIPOL/Sectoriales_19-20/Programas_derivados_PND.xls";   
    window.location = sUrlDescarga;
}
function fjsCargaEstadisticas()
{
    //Estadisticas
    var totalAlcanzada = 0;
    var totalPlaneada = 0;
    var contAlc = 0;
    var contPla = 0;
    var vectorAlc = [];
    var vectorPla = [];
    var control = document.getElementById('ContentPlaceHolder1_ddlGraficaInd');
    for (var i = 0; i < control.options.length; i++) {
        var datos = JSON.parse(control.options[i].innerHTML);
        if (datos.MetaAlcanzada && datos.MetaAlcanzada != '-') {
            vectorAlc[contAlc] = datos.MetaAlcanzada;
            contAlc = contAlc + 1;
            totalAlcanzada = totalAlcanzada + parseFloat(datos.MetaAlcanzada);
        }
        if (datos.MetaPlaneada && datos.MetaPlaneada != '-') {
            vectorPla[contPla] = datos.MetaPlaneada;
            contPla = contPla + 1;
            totalPlaneada = totalPlaneada + parseFloat(datos.MetaPlaneada);
        }
    }
    var avgPlaneada = 0;
    var avgAlcanzada = 0;
    contAlc = (contAlc == 0 ? 1 : contAlc);
    contPla = (contPla == 0 ? 1 : contPla);
    avgAlcanzada = totalAlcanzada / contAlc;
    avgPlaneada = totalPlaneada / contPla;
    document.getElementById('ContentPlaceHolder1_rprDetalleInd_LblAlcanzadaPro_0').innerHTML = "" + avgAlcanzada.toFixed(2);
    document.getElementById('ContentPlaceHolder1_rprDetalleInd_LblPlaneadaPro_0').innerHTML = "" + avgPlaneada.toFixed(2);
    if (vectorAlc.length > 0) {
       document.getElementById('ContentPlaceHolder1_rprDetalleInd_LblAlcanzadaMax_0').innerHTML = "" + Math.max.apply(null, vectorAlc).toFixed(2);
       document.getElementById('ContentPlaceHolder1_rprDetalleInd_LblAlcanzadaMim_0').innerHTML = "" + Math.min.apply(null, vectorAlc).toFixed(2);
    }
    if (vectorPla.length > 0) {
       document.getElementById('ContentPlaceHolder1_rprDetalleInd_LblPlaneadaMax_0').innerHTML = "" + Math.max.apply(null, vectorPla).toFixed(2);
       document.getElementById('ContentPlaceHolder1_rprDetalleInd_LblPlaneadaMin_0').innerHTML = "" + Math.min.apply(null, vectorPla).toFixed(2);
    }


    //Calidad
    var vCla = $("#cajaClaridad").data("visible");
    var vRel = $("#cajaRelevancia").data("visible");
    var vAde = $("#cajaAdecuacion").data("visible");
    var vMon = $("#cajaMonito").data("visible");
    if (vCla == "" && vRel == "" && vAde == "" && vMon == "") {
        $("#LeyendaAviso").attr("style", "display:block");
        $("#divSemaforos").attr("style", "display:none");
    }
    else {
        if (vCla == "0")
            $("#cajaClaridad").attr("style", "background-color:red");
        if (vRel == "0")
            $("#cajaRelevancia").attr("style", "background-color:red");
        if (vAde == "0")
            $("#cajaAdecuacion").attr("style", "background-color:red");
        if (vMon == "0")
            $("#cajaMonito").attr("style", "background-color:red");
    }
}

function fjsMostrarValoraciones(CalTotal, CalEDR) {
    var divEDR = document.getElementById('DivCalEDR');
    var divTotal = document.getElementById('DivCalTot');
    switch (CalEDR)
    {
        case '0':
            divEDR.className = 'cajaRojaMIR';
            break;
        case '0.5':
            divEDR.className = 'cajaAmarillaMIR';
            break;
        case '1':
            divEDR.className = 'cajaVerdeMIR';
            break;
        default:
            divEDR.className = '';
            divEDR.style = 'font-weight: bold;';
            divEDR.innerHTML = 'NA';
            break;
    }
    switch (CalTotal) {
        case '1':
            divTotal.className = 'cajaRojaMIR';
            break;
        case '2':
            divTotal.className = 'cajaAmarillaMIR';
            break;
        case '3':
            divTotal.className = 'cajaVerdeClaroMIR';
            break;
        case '4':
            divTotal.className = 'cajaVerdeMIR';
            break;
        default:
            divTotal.className = '';
            divTotal.style = 'font-weight: bold;';
            divTotal.innerHTML = 'NA';
            break;
    }
    //alert('PRUEBA - Entro a la función. Calificación Total ' + CalTotal + ', Calificación Enfoque de Resultado ' + CalEDR);
}

//Muestra una gráfica para La calidad MIR de la dependencia
function fjsCalidadMIR(MIRDestacado, MIRAdecuado, MIRModerado, MIROportunidadMejora) {
    var chart = AmCharts.makeChart("divCalidadMIR", {
        "theme": "light",
        "type": "serial",
        "dataProvider": [{
            "Calidad MIR": "Destacado",
            "Num. Programas": MIRDestacado,
            "color": "darkgreen"

        }, {
            "Calidad MIR": "Adecuado",
            "Num. Programas": MIRAdecuado,
            "color": "RGB(146,208,80)"
        }, {
            "Calidad MIR": "Moderado",
            "Num. Programas": MIRModerado,
            "color": "yellow"
        }, {
            "Calidad MIR": "Oportunidad de Mejora",
            "Num. Programas": MIROportunidadMejora,
            "color": "red"
        }],
        "startDuration": 2.5,
        "graphs": [{
            "balloonText": "[[category]]: [[value]] Programa(s)",
            "fillAlphas": 1,
            "lineAlpha": 0.2,
            "title": "Número de Programas",
            "type": "column",
            "valueField": "Num. Programas",
            "colorField": "color",
        }],
        "rotate": true,
        "categoryField": "Calidad MIR",
        "categoryAxis": {
            "gridPosition": "start",
            "fillAlpha": 0.5,
            "position": "left"
        },
        "export": {
            "enabled": true
        }
    });
}

//Muestra una gráfica para el Enfoque de Resultados de la dependencia
function fjsEnfoqueResultados(EDRDestacado, EDRModerado, EDROportunidadMejora) {
   var chart = AmCharts.makeChart("divResultados", {
      "type": "pie",
      "theme": "light",
      "legend": {
         "position": "right",
         "divId": "legenddiv",
         "valueText": ""
      },
      "dataProvider": [{
         "title": "Programas sin Enfoque de Resultados",
         "value": EDROportunidadMejora,
         "color": "red"
      }, {
         "title": "Programas con Enfoque de Resultados",
         "value": EDRDestacado,
         "color": "green"
      }, {
         "title": "Programas que deben mejorar su Enfoque de Resultados",
         "value": EDRModerado,
         "color": "yellow"
      }],
      "titleField": "title",
      "valueField": "value",
      "colorField": "color",
      "labelRadius": 5,
      "radius": "30%",
      "innerRadius": "55%",
      "labelText": "[[percents]]%",
      "export": {
         "enabled": true
      }
    });
}
function fjsGeneraGraficoDetalleIndicador(ID_OBJETIVO_M, NOMBRE, ID_INDICADOR_PND, UNIDAD_MEDIDA) {
    
    //params.pais = $("#<%=ddlPaises.ClientID%>").val();
    //params = JSON.stringify(params);
    var params = JSON.stringify({
        iIdObetivoM: ID_OBJETIVO_M,
        sNombre: NOMBRE,
        unidadMedida: UNIDAD_MEDIDA
    });
    $.ajax({
        async: true,
        type: 'POST',
        url: "MetasNacionales.aspx/GraficaIndicadoresPND",
        data: params,
        contentType: "application/json; charset=utf-8",
        error: function (err, status, e) {
            
        },
        success: function (result) {
            var data = JSON.parse(result.d)
            fjsGeneraGraficaDetalleIndicador(data, "CICLO", "LineaBase", "MetaAlcanzada", "MetaIntermedia", "Meta2018", "divGrafIcaDetalleIndicador" + ID_INDICADOR_PND,"Historico",false,1)
        }
    });

}
function fjsGeneraGraficoDetalleIndicadorTrans(NOMBRE, ID_INDICADOR_ESTR_TRANS) {

    //params.pais = $("#<%=ddlPaises.ClientID%>").val();
    //params = JSON.stringify(params);
    var params = JSON.stringify({
        sNOMBRE: NOMBRE
    });
    $.ajax({
        async: true,
        type: 'POST',
        url: "MetasNacionales.aspx/GraficaIndicadoresTrans",
        data: params,
        contentType: "application/json; charset=utf-8",
        error: function (err, status, e) {

        },
        success: function (result) {
            var data = JSON.parse(result.d)
            fjsGeneraGraficaDetalleIndicador(data, "CICLO", "LineaBase", "MetaAlcanzada", "MetaIntermedia", "Meta2018", "divGrafIcaDetalleIndicadorTrans" + ID_INDICADOR_ESTR_TRANS, "Historico", false, 1)
        }
    });

}

function fjsCargarGraficaIndicadorSectorial4T(ddlPattern, divPrint, iBanInd, NIndicador) {
    var colSelect = document.getElementsByTagName('select');
    for (var iIndex = 0; iIndex < colSelect.length; iIndex++) {
        if ((colSelect[iIndex].id).indexOf(ddlPattern) > -1)
            fjsGraficarIndicadorSectorial4T(colSelect[iIndex], false, divPrint, iBanInd, NIndicador);

    }
}
function fjsGraficarIndicadorSectorial4T(ddl, showScroll, divPrint, iBanInd, NIndicador) {

    var chartDataDer = fjsGeneraArregloGrafica(ddl);
    var titleGrafica = "";
    var arr = new Array(0);

    var test = new Array(0);
    var bandera = false;
    for (var i = 0; i < chartDataDer.length; i++) {

        var prueba = chartDataDer[i];

        if (prueba.DOF_LB < 1) {
            prueba.test2 = '*';
        }

        if (prueba.DOF_META < 1)
            prueba.CICLO += '*';

        test.push(prueba);
    }

    for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)

        arr.push(eval(+ddl.options[iIndex].value));
    var AniomaxClasif = Math.max.apply(null, arr);
    var AniominClasif = Math.min.apply(null, arr);
    if (AniomaxClasif != null && AniominClasif != null)
        titleGrafica = NIndicador;
    fjsGeneraHistogramaIndicadoresSectoriales4T(test, 'CICLO', 'LineaBase', 'MetaAlcanzada', 'MetaIntermedia', 'Meta2018', divPrint, titleGrafica, showScroll, iBanInd);

}
function fjsCargarGraficaIndicadores4T(ddlPattern) {
    var colSelect = document.getElementsByTagName('select');
    for (var iIndex = 0; iIndex < colSelect.length; iIndex++) {
        if ((colSelect[iIndex].id + 'X').indexOf(ddlPattern) > -1)
            fjsGraficarIndicador4T(colSelect[iIndex], false);

    }
}
function fjsGraficarIndicador4T(ddl, showScroll) {
    if (ddl.length > 0) {
        var valueJson = ddl.options[0].text;
        if (valueJson && valueJson.length > 0) {
            valueJson = valueJson.split(',')[0];
            if (valueJson.indexOf(':"') > -1) {
                valueJson = valueJson.substring(valueJson.indexOf(':"') + 2, valueJson.length - 1);
            }
            var idDivGrafica = valueJson;
            if (idDivGrafica && idDivGrafica != '') {
                var chartDataDer = fjsGeneraArregloGrafica(ddl);
                fjsGeneraHistogramaComparativa4T(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', idDivGrafica, 'Histórico (Gráfica Lineal)', showScroll);
            }
        }
    }
}

function fjsGraficaIndicador4T() {
    fjsBloquearPantalla("Cargando...");
    var chartDataDer = fjsGeneraArregloGrafica4T(document.getElementById('ctl00_PlaceHolderMain_ddlGrafica'));
    fjsGeneraHistogramaComparativa4T(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', 'chartInd', 'Histórico', true);

    fjsDesbloquearPantalla();
}
function fjsGeneraArregloGrafica4T(ddl) {
    var chartData = new Array(0);
    var aColors = new Array(0);
    for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
        if (ddl.options[iIndex].text != "") {
            chartData.push(eval('(' + ddl.options[iIndex].text + ')'));
        }
    return chartData;
}
function fjsCargaDetalleIndicador4T() {
    fjsBloquearPantalla("Cargando...");
    var nivelConsultado = fjsRecuperaParametroPae(location.href, 'pNivel');
    var ciclo = fjsRecuperaParametro('pCiclo');
    var spnNivelSeleccionado = document.getElementById('spnTituloIndicador');
    if (spnNivelSeleccionado) {
        var sHTML = '<table style="width: 100%;"><tr><td ';
        sHTML += nivelConsultado == 2 ? ' class="headerProposito headerTituloNivel" ' : (nivelConsultado == 3 ? ' class="headerComponente headerTituloNivel" ' : '');
        sHTML += '>';
        sHTML += nivelConsultado == 2 ? 'Indicador del objetivo del programa' : (nivelConsultado == 3 ? 'Indicador del Apoyo del programa (Componente)' : '');
        sHTML += '</td></tr>';
        spnNivelSeleccionado.innerHTML = sHTML;
    }
    var tituloNivel = document.getElementById('ContentPlaceHolder1_LblIndicador');
    if (tituloNivel) {
        tituloNivel.innerHTML = nivelConsultado == 2 ? ' Indicador de proposito ' + ciclo : (nivelConsultado == 3 ? ' Indicador de componente ' + ciclo : 'Indicador ' + ciclo)
    }
    var nombreIndicador = document.getElementById('ContentPlaceHolder1_LblTituloInd');
    if (nombreIndicador) {
        nombreIndicador.innerHTML = document.getElementById('ContentPlaceHolder1_rprDetalleInd_lblNombreInd_0').innerHTML;
    }
    fjsDibujarGraficaInd4T('lineal', 'divGrafica');
    fjsDesbloquearPantalla();

}
function fjsDibujarGraficaInd4T(tipo, divToPrintChart) {
    var NombreIndicador = $("#ContentPlaceHolder1_LblTituloInd").text();

    var chartDataDer = fjsGeneraArregloGrafica4T(document.getElementById('ContentPlaceHolder1_ddlGraficaInd'));

    if (tipo == 'barra')
        fjsGeneraBarraComparativa(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', divToPrintChart, NombreIndicador);
    if (tipo == 'lineal')
        fjsGeneraHistogramaComparativa(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', divToPrintChart, NombreIndicador, true);
}
function fjsMostrarGrafica4T(tipo) {
    var lineal = document.getElementById('divGrafica');
    if (lineal) {
        lineal.innerHTML = '';
        fjsDibujarGraficaInd4T(tipo, 'divGrafica');
    }
}
function fjsMostrarGraficaHome4T(tipo, divPrint, ContentPlaceControl) {
    var lineal = document.getElementById(divPrint);
    if (lineal) {
        lineal.innerHTML = '';
        fjsGenerargrafico4T(tipo, divPrint, ContentPlaceControl);
    }
}
function fjsGenerargrafico4T(tipo, divToPrintChart, ContentPlaceControl) {
    var chartDataDer = fjsGeneraArregloGrafica4T(document.getElementById(ContentPlaceControl));

    var ddls = document.getElementById('ContentPlaceHolder1_ddlTotalClasificados');
    var ddl = document.getElementById('ContentPlaceHolder1_ddlTotalIndicadores');
    var arrT = new Array(0);
    var arr = new Array(0);

    for (var iIndex = 0; iIndex < ddls.options.length; iIndex++)
        arr.push(eval(+ddls.options[iIndex].value));

    var AniomaxClasif = Math.max.apply(null, arr);
    var AniominClasif = Math.min.apply(null, arr);

    for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
        arrT.push(eval(+ddl.options[iIndex].value));

    var AniomaxTot = Math.max.apply(null, arrT);
    var AniominTot = Math.min.apply(null, arrT);

    var Total = new Array(0);
    var Tot = new Array(0);

    for (var iIndex = 0; iIndex < ddl.options.length; iIndex++)
        Total.push(ddl.options[iIndex].text.substring(41, 36).replace('"', ""));

    var ValorMax = Math.max.apply(null, Total);
    var ValorMin = Math.min.apply(null, Total);

    if (tipo == 'IndicaClasificados')
        fjsGenerarConteoIndicadores("Estructura de los indicadores de los programas sociales por objetivos", chartDataDer, 'ciclo', 'resultados', 'servicio', 'gestion', divToPrintChart);
    if (tipo == 'IndicaTotal')
        fjsGenerarTotalIndicadores("Histórico del Número de Indicadores " + AniominTot + " - " + AniomaxTot, chartDataDer, 'ciclo', 'TotalIndicadores', divToPrintChart, AniominTot, AniomaxTot, ValorMax, ValorMin);
}
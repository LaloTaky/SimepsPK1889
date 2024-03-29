
/*
Objetivo: Construye una grafica de tipo Pila de Barras  
Par�metros: chartData: Arreglo con los objetos a mostrar en el pie
graphData: Arreglo de graficas
sCategoria: Campo de la categoria
divChart: Tag div donde se mostrar� la gr�fica
sTituloAMostrar: T�tulo a mostrar en la gr�fica
*/

function fjsGeneraBarStack(chartData, graphData, sCategoria, divChart, sTituloAMostrar, sClick, sDoubleClick) {
    var chart;

    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.dataProvider = chartData;
    chart.categoryField = sCategoria;
    chart.plotAreaBorderAlpha = 0.2;
    //chart.rotate = true;
    //chart.depth3D = 20;
    //chart.angle = 30;
    chart.columnWidth = 0.3;
    chart.marginRight = 0;
    //chart.autoMargins=true;

    // AXES
    // Category
    var categoryAxis = chart.categoryAxis;
    categoryAxis.gridAlpha = 0.1;
    categoryAxis.axisAlpha = 0;
    categoryAxis.gridPosition = "start";

    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.stackType = "100%";
    valueAxis.gridAlpha = 0.1;
    valueAxis.axisAlpha = 0;
    valueAxis.title = "Porcentajes";
    chart.addValueAxis(valueAxis);

    // GRAPHS
    // firstgraph
    for (var iIndex = 0; iIndex < graphData.length; iIndex++)
        chart.addGraph(graphData[iIndex]);

    // LEGEND
    var legend = new AmCharts.AmLegend();
    legend.position = "right";
    legend.borderAlpha = 0.3;
    legend.horizontalGap = 10;
    legend.switchType = "v";
    legend.labelWidth = 80;
    legend.fontSize = 9;
    legend.width = 190;
    //legend.right=10
    chart.addLegend(legend);

    // WRITE
    chart.write(divChart);
}



/*
Objetivo: Construye una grafica de tipo Pie 
Par�metros: chartData: Arreglo con los objetos a mostrar en el pie
            aColors: Arreglo de colores
            sTitulo: Campo del objeto que representa el t�tulo
            sValor: Campo del objeto que representa el valor
            divChart: Tag div donde se mostrar� la gr�fica
            sTituloAMostrar: T�tulo a mostrar en la gr�fica
            sClickParam: Par�metro que identifica el tipo de pantalla que genera el evento
*/
function fjsGeneraPie(chartData, aColors, sTitulo, sValor, divChart, sTituloAMostrar, sYPos, iAnchoEtiqueta, sClickParam) {
    var chartPie;


    // PIE CHART
    chartPie = new AmCharts.AmPieChart();
    if (sTituloAMostrar) chartPie.addTitle(sTituloAMostrar, 12);
    chartPie.dataProvider = chartData;
    chartPie.colors = aColors;
    chartPie.titleField = sTitulo;
    chartPie.valueField = sValor;
    /*chartPie.outlineColor = "#FFFFFF";
    chartPie.outlineAlpha = 0.8;
    chartPie.outlineThickness = 2;*/
    chartPie.balloonText = "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>";
    // this makes the chart 3D
    chartPie.depth3D = 15;
    chartPie.angle = 30;
    chartPie.marginLeft = 0;
    chartPie.marginRight = 0;
    if (sYPos) chartPie.pieY = sYPos;
    //chartPie.width = "120%";
    //chartPie.height = "100%";
    if (iAnchoEtiqueta) chartPie.maxLabelWidth = iAnchoEtiqueta;
    else chartPie.maxLabelWidth = 70;
    chartPie.creditsPosition = "top";

    if (sClickParam) {
        chartPie.addListener("clickSlice", function (event) {
            if (event.dataItem.dataContext.id != undefined) {
                fjsPieClick(event.dataItem.dataContext.id, sClickParam);
            }
        });
    }

    // WRITE
    chartPie.write(divChart)

}

/*
Objetivo: Construye una grafica de tipo Columna o barra
Par�metros: chartData: Arreglo con los objetos a mostrar en el pie
             sTitulo: Campo del objeto que representa el t�tulo
             sValor: Campo del objeto que representa el valor
             divChart: Tag div donde se mostrar� la gr�fica
             sTituloAMostrar: T�tulo a mostrar en la gr�fica
             sClickParam: Par�metro que identifica el tipo de pantalla que genera el evento
*/
function fjsGeneraBarra(chartData, sTitulo, sValor, divChart, sTituloAMostrar, sClickParam) {
    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.categoryField = sTitulo;
    chart.startDuration = 5;
    chart.zoomOutText = "Mostrar Todo";


    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#FAFAFA";
    categoryAxis.gridPosition = "start";

    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.dashLength = 5;
    //valueAxis.title = "Visitors from country";
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH 
    var graph = new AmCharts.AmGraph();
    //graph.title = "Visits";
    graph.valueField = sValor;
    graph.colorField = "color";
    graph.balloonText = "<b>[[category]]: [[value]]</b>";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    chart.addGraph(graph);

    // CURSOR
    var chartCursor = new AmCharts.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = false;
    chartCursor.categoryBalloonEnabled = true;
    //chartCursor.oneBalloonOnly=true;
    chart.addChartCursor(chartCursor);

    // SCROLLBAR
    var chartScrollbar = new AmCharts.ChartScrollbar();
    chart.addChartScrollbar(chartScrollbar);

    // WRITE
    chart.write(divChart);
}


/*
Objetivo: Construye una grafica de tipo Columna o barra
Par�metros: chartData: Arreglo con los objetos a mostrar en el pie
             sTitulo: Campo del objeto que representa el t�tulo
             sValor: Campo del objeto que representa el valor
             divChart: Tag div donde se mostrar� la gr�fica
             sTituloAMostrar: T�tulo a mostrar en la gr�fica
             sClickParam: Par�metro que identifica el tipo de pantalla que genera el evento
*/
function fjsGeneraBarraComparativa(chartData, sTitulo, sValor1, sValor2, divChart, sTituloAMostrar, sClickParam) {
    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/SIMEPS/img/Grafica/";
    chart.dataProvider = chartData;
    chart.plotAreaFillColors = "#D2D2D2";
    chart.plotAreaFillAlphas = 1;
    chart.categoryField = sTitulo;
    chart.startDuration = 5;
    chart.zoomOutText = "Mostrar Todo";

    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#D2D2D2";
    categoryAxis.gridPosition = "start";

    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.dashLength = .5;
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH 
    var graph = new AmCharts.AmGraph();
    graph.valueField = sValor1;
    graph.colorField = "color1";
    graph.balloonText = "<span style='font-size:13px;'>[[title]] Meta Planeada [[category]]:<b>[[value]]</b></span>";
    graph.type = "column";
    graph.lineAlpha = 0;
    graph.fillAlphas = 1;
    chart.addGraph(graph);

    // GRAPH 2
    var graph2 = new AmCharts.AmGraph();
    //graph.title = "Visits";
    graph2.valueField = sValor2;
    graph2.colorField = "color2";
    graph2.balloonText = "<span style='font-size:13px;'>[[title]] Meta Alcanzada [[category]]:<b>[[value]]</b></span>";
    graph2.type = "column";
    graph2.lineAlpha = 0;
    graph2.fillAlphas = 1;
    chart.addGraph(graph2);

    // CURSOR
    var chartCursor = new AmCharts.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = true;
    chartCursor.categoryBalloonEnabled = true;
    //chartCursor.oneBalloonOnly=true;
    chart.addChartCursor(chartCursor);

    // SCROLLBAR
    var chartScrollbar = new AmCharts.ChartScrollbar();
    chart.addChartScrollbar(chartScrollbar);

    // WRITE
    chart.write(divChart);
}


/*
Objetivo: Construye una grafica de tipo histograma
Par�metros: chartData: Arreglo con los objetos a mostrar en el pie
            sTitulo: Campo del objeto que representa el t�tulo
            sValor: Campo del objeto que representa el valor
            divChart: Tag div donde se mostrar� la gr�fica
            sTituloAMostrar: T�tulo a mostrar en la gr�fica
            sClickParam: Par�metro que identifica el tipo de pantalla que genera el evento
*/
function fjsGeneraHistograma(chartData, sTitulo, sValor, divChart, sTituloAMostrar, sClickParam) {
    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.categoryField = sTitulo;
    chart.startDuration = 2;
    chart.zoomOutText = "Mostrar Todo";

    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    //categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#FAFAFA";
    categoryAxis.gridPosition = "start";

    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.dashLength = 5;
    //valueAxis.title = "Visitors from country";
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph2 = new AmCharts.AmGraph();
    graph2.type = "line";
    graph2.lineColor = "#27c5ff";
    graph2.bulletColor = "#FFFFFF";
    graph2.bulletBorderColor = "#27c5ff";
    graph2.bulletBorderThickness = 2;
    graph2.bulletBorderAlpha = 1;
    graph2.valueField = sValor;
    graph2.lineThickness = 2;
    graph2.bullet = "round";
    graph2.fillAlphas = 0;
    graph2.balloonText = "<span style='font-size:13px;'>[[title]] A&ntilde;o [[category]]:<b>[[value]]</b></span>";
    chart.addGraph(graph2);

    // CURSOR
    var chartCursor = new AmCharts.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = false;
    chartCursor.categoryBalloonEnabled = true;
    //chartCursor.oneBalloonOnly=true;
    chart.addChartCursor(chartCursor);

    // SCROLLBAR
    var chartScrollbar = new AmCharts.ChartScrollbar();
    chart.addChartScrollbar(chartScrollbar);

    // WRITE
    chart.write(divChart);
}
/*
sValor1 = meta alcanzada
sValor2 = metas intermedias 
sValor3 = meta alcanzada 2018
*/
function fjsGeneraHistogramaIndicadoresSectoriales(chartData, sCategoria, sValorLinea, sValor1, sValor2, sValor3, divChart, sTituloAMostrar, showScroll, iBanInd) {
    // SERIAL CHART
    var amc = AmCharts;
    chart = new amc.AmSerialChart();
    var legend = new amc.AmLegend();
    //legend.position = "top";
    legend.useGraphSettings = true;
    //legend.markerType = "bubble";
    legend.markerSize = 4;
    legend.verticalGap = 15;
    legend.spacing = 50;
    legend.horizontalGap = 10;
    legend.valueText = "";
    if (iBanInd == 1){
       chart.addLegend(legend, 'divFooterGrafIndicadorSectorial');

    }
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.categoryField = sCategoria;
    chart.startDuration = 2;
    chart.zoomOutText = "Mostrar Todo";
    chart.autoResize = true;
    chart.responsive = {
        "enabled": true
    };
   
    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    //categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#FAFAFA";
    categoryAxis.gridPosition = "start";
    categoryAxis.axisAlpha= 1;
    categoryAxis.autoWrap= false;
    categoryAxis.minHorizontalGap= 0;
    categoryAxis.gridThickness = 0;
    categoryAxis.fontSize = 8;

    // value
    var valueAxis = new amc.ValueAxis();
    valueAxis.dashLength = 5;
    valueAxis.gridAlpha = 0;
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph0 = new amc.AmGraph();
    graph0.type = "line";
    //graph1.colorField = "color1";
    graph0.lineColor = "#c1c1c1";//"#27c5ff";
    graph0.bulletColor = "#c1c1c1";
    //graph0.bulletBorderColor = "#00a94f";
    graph0.bulletBorderThickness = 2;
    graph0.bulletBorderAlpha = 1;
    graph0.valueField = sValorLinea;
    graph0.lineThickness = 3;
    graph0.bullet = "round";
    graph0.fillAlphas = 0;
    graph0.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph0.title = "L\u00EDnea Base";
    graph0.labelText = "[[value]]";
    graph0.visibleInLegend = true;
    chart.addGraph(graph0);

    // GRAPH
    var graph1 = new amc.AmGraph();
    graph1.type = "line";
    //graph1.colorField = "color1";
    graph1.lineColor = "#00a94f";//"#27c5ff";
    graph1.bulletColor = "#00a94f";
    //graph1.bulletBorderColor = "#27c5ff";
    graph1.bulletBorderThickness = 2;
    graph1.bulletBorderAlpha = 1;
    graph1.valueField = sValor1;
    graph1.lineThickness = 3;
    graph1.bullet = "round";
    graph1.fillAlphas = 0;
    graph1.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph1.title = "Meta Alcanzada";
    graph1.labelText = "[[value]]";
    graph1.visibleInLegend = true;
    chart.addGraph(graph1);

    // GRAPH
    var graph2 = new amc.AmGraph();
    graph2.type = "line";
    //graph2.colorField = "color2";
    graph2.lineColor = "#072a5f";//"#27c5ff";
    graph2.bulletColor = "#072a5f";
    //graph2.bulletBorderColor = "#F7D358";
    graph2.bulletBorderThickness = 2;
    graph2.bulletBorderAlpha = 1;
    graph2.valueField = sValor2;
    graph2.lineThickness = 3;
    graph2.bullet = "round";
    graph2.fillAlphas = 0;
    graph2.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph2.title = "Meta Intermedia";
    graph2.labelText = "[[value]]";
    graph2.visibleInLegend = true;
    chart.addGraph(graph2);

    // GRAPH
    var graph3 = new amc.AmGraph();
    graph3.type = "line";
    //graph2.colorField = "color2";
    graph3.lineColor = "#868484";//"#27c5ff";
    graph3.bulletColor = "#868484";
   // graph3.bulletBorderColor = "#F7D358";
    graph3.bulletBorderThickness = 2;
    graph3.bulletBorderAlpha = 1;
    graph3.valueField = sValor3;
    graph3.lineThickness = 3;
    graph3.bullet = "round";
    graph3.fillAlphas = 0;
    graph3.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph3.title = "Meta Planeada";
    graph3.labelText = "[[value]]";
    graph3.visibleInLegend = true;
    chart.addGraph(graph3);

    // CURSOR
    var chartCursor = new amc.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = true;
    chartCursor.categoryBalloonEnabled = true;
    chart.addChartCursor(chartCursor);
    // WRITE
    chart.write(divChart);
}

/*
Objetivo: Construye una grafica de tipo histograma
Par�metros: chartData: Arreglo con los objetos a mostrar en el pie
            sTitulo: Campo del objeto que representa el t�tulo
            sValor: Campo del objeto que representa el valor
            divChart: Tag div donde se mostrar� la gr�fica
            sTituloAMostrar: T�tulo a mostrar en la gr�fica
            sClickParam: Par�metro que identifica el tipo de pantalla que genera el evento
*/
function fjsGeneraHistogramaComparativa(chartData, sTitulo, sValor1, sValor2, divChart, sTituloAMostrar, showScroll) {
    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.plotAreaFillColors = "#D2D2D2";
    chart.plotAreaFillAlphas = 1;
    chart.categoryField = sTitulo;
    chart.startDuration = 2;
    chart.zoomOutText = "Mostrar Todo";
    chart.autoResize = true;
    chart.responsive = {
        "enabled": true
    };
    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    //categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#D2D2D2";
    categoryAxis.gridPosition = "start";

    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.dashLength = 5;
    valueAxis.gridAlpha = 0;
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph1 = new AmCharts.AmGraph();
    graph1.type = "line";
    //graph1.colorField = "color1";
    graph1.lineColorField = "color1";//"#27c5ff";
    graph1.bulletColor = "#FFFFFF";
    graph1.bulletBorderColor = "#7E015B";
    graph1.bulletBorderThickness = 2;
    graph1.bulletBorderAlpha = 1;
    graph1.valueField = sValor1;
    graph1.lineThickness = 2;
    graph1.bullet = "round";
    graph1.fillAlphas = 0;
    graph1.balloonText = "<span style='font-size:13px;'>[[title]] Meta Planeada [[category]]:<b>[[value]]</b></span>";
    chart.addGraph(graph1);

    // GRAPH
    var graph2 = new AmCharts.AmGraph();
    graph2.type = "line";
    //graph2.colorField = "color2";
    graph2.lineColorField = "color2";;//"#27c5ff";
    graph2.bulletColor = "#FFFFFF";
    graph2.bulletBorderColor = "#3C1559";
    graph2.bulletBorderThickness = 2;
    graph2.bulletBorderAlpha = 1;
    graph2.valueField = sValor2;
    graph2.lineThickness = 2;
    graph2.bullet = "round";
    graph2.fillAlphas = 0;
    graph2.balloonText = "<span style='font-size:13px;'>[[title]] Meta Alcanzada [[category]]:<b>[[value]]</b></span>";
    chart.addGraph(graph2);

    // CURSOR
    var chartCursor = new AmCharts.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = true;
    chartCursor.categoryBalloonEnabled = true;
    //chartCursor.oneBalloonOnly=true;

    //chartCursor.enabled=false; JCC
    chart.addChartCursor(chartCursor);

    // SCROLLBAR
    /* if(showScroll){
        var chartScrollbar = new AmCharts.ChartScrollbar();
        chart.addChartScrollbar(chartScrollbar);
     }*/
    // WRITE
    chart.write(divChart);
}


/*
Objetivo: Construye una grafica de tipo Dona 
Par�metros: 
*/
function fjsGeneraDona(chartData, graphData, sTitulo, sValor, divChart, sClick, sDoubleClick) {
    var chartDona;


    chartDona = new AmCharts.AmPieChart();
    //chartDona.addTitle(sTituloAMostrar, 14);

    chartDona.dataProvider = chartData;
    chartDona.titleField = sTitulo;
    chartDona.valueField = sValor;
    chartDona.sequencedAnimation = true;
    chartDona.startEffect = "elastic";
    chartDona.innerRadius = "30%";
    chartDona.startDuration = 2;
    chartDona.labelRadius = 10;
    chartDona.balloonText = "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>";
    chartDona.depth3D = 10;
    chartDona.angle = 15;
    chartDona.maxLabelWidth = 120;

    chartDona.addListener("clickSlice", function (event) {
        if (event.dataItem.dataContext.id != undefined) {
            fjsMuestraPopUp(event, divChart, event.dataItem.dataContext.id, event.dataItem.tx, event.dataItem.ty);
        }
    });

    chartDona.write(divChart)
}


/*
Objetivo: Recupera el valor del par�metro proporcionado
Par�metros: sParam: Nombre del par�metro a buscar en el Query String
            Regresa: Valor del par�metro
*/
function fjsRecuperaParametro(sParam) {
    var sValor = '0';
    if (location.search.split(sParam + '=')[1]) {
        sValor = location.search.split(sParam + '=')[1];
        if (sValor.indexOf('&') > -1) sValor = sValor.substring(0, sValor.indexOf('&'));
    }
    return sValor;
}


function fjsGenerarConteoIndicadores(sTitulo, chartData, sCiclo, resultado, servicio, gestion, divPrint) {
    var chart = AmCharts.makeChart(divPrint, {
        "type": "serial",
        "theme": "light",
        "titles": [{
            "text": sTitulo,
            "size": 12
        }],
        "legend": {
            "useGraphSettings": true,
            "markerSize": 5,
            "spacing": 1,
            "align": "center"
        },
        "dataProvider": chartData,
        "valueAxes": [{
            "stackType": "regular",
            "axisAlpha": 0.3,
            "gridAlpha": 0,
            "fontSize": 9,
        }],
        "startDuration": 1,
        "graphs": [{
            "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
            "fillAlphas": 0.8,
            "labelText": "",
            "fontSize":7,
            "lineAlpha": 0.3,
            "title": "Resultados",
            "type": "column",
            "color": "#ffffff",
            "fillColors": "#0F92B1",
            "lineColor": "#0F92B1",
            "valueField": resultado
        }, {
            "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
            "fillAlphas": 0.8,
            "labelText": "",
            "lineAlpha": 0.3,
            "title": "Servicios",
            "fontSize": 7,
            "type": "column",
            "color": "#ffffff",
            "fillColors": "#024C90",
            "lineColor": "#024C90",
            "valueField": servicio
        }, {
            "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
            "fillAlphas": 0.8,
            "labelText": "",
            "lineAlpha": 0.3,
            "title": "Gestión",
            "fontSize": 7,
            "type": "column",
            "color": "#ffffff",
            "fillColors": "#63AC20",
            "lineColor": "#63AC20",
            "valueField": gestion
        }],
        "categoryField": sCiclo,
        "categoryAxis": {
            "gridPosition": "start",
            "axisAlpha": 0,
            "gridAlpha": 0,
            "position": "left",
            "autoWrap": false,
            "minHorizontalGap": 0,
        },
        "export": {
            "enabled": true
        }

    });
}


function fjsGenerarTotalIndicadores(sTitulo, chartData, sCiclo, svalor1, divPrint, anioMin, anioMax, valorMax, valorMin) {

    var chart = AmCharts.makeChart(divPrint, {
        "type": "serial",
        "theme": "light",
        "titles": [{
            "text": sTitulo,
            "size": 12
        }],
        "legend": {
            "useGraphSettings": true,
            "align": "right"
        },
        "dataProvider": chartData,
        "valueAxes": [{
            "integersOnly": true,
            "axisAlpha": 1,
            "dashLength": 0,
            "gridCount": 10,
            "position": "left",
            "fontSize": 9
        }],
        "startDuration": 0.5,
        "graphs": [{
            "balloonText": "[[value]]",
            "labelText": "",
            "bullet": "round",
            "title": "Total de Indicadores",
            "valueField": svalor1,
            "fillColors": "#2049BE",
            "lineColor": "#2049BE",
            "fillAlphas": 0
        }],
        "trendLines": [{
            "finalCategory": anioMax,
            "finalValue": valorMax,
            "initialCategory": anioMin,
            "initialValue": valorMin,
            "lineColor": "#77B73D"
        }],
        "chartCursor": {
            "cursorAlpha": 0,
            "zoomable": false
        },
        "categoryField": sCiclo,
        "categoryAxis": {
            "axisAlpha": 1,
            "autoWrap": false,
            "minHorizontalGap": 0,
            "gridThickness": 0,
           "fontSize":11
           // "dashLength": 0,
           // "fillAlpha": 0,
           // "fillColor": "#FFFFFF",
           // "gridAlpha": 0,
           // "minorTickLength": 0,
           // "tickLength": 0,
           //"twoLineMode":false
        },
        "export": {
            "enabled": true
        }
    });

}

function fjsGeneraGraficaDetalleIndicador(chartData, sCategoria, sValorLinea, sValor1, sValor2, sValor3, divChart, sTituloAMostrar, showScroll, iBanInd) {
    //(chartData, sCategoria, sValorLinea, sValor1, sValor2, sValor3, divChart, sTituloAMostrar, showScroll, iBanInd) {

    // SERIAL CHART
    var amc = AmCharts;
    chart = new amc.AmSerialChart();
    var legend = new amc.AmLegend();

    //legend.position = "top";
    legend.useGraphSettings = true;
    //legend.markerType = "bubble";
    legend.markerSize = 4;
    legend.verticalGap = 15;
    legend.spacing = 50;
    legend.horizontalGap = 10;
    legend.valueText = "";
    if (iBanInd == 1) {
        chart.addLegend(legend, 'divFooterGrafIndicadorSectorial');

    }
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.categoryField = sCategoria;
    chart.startDuration = 2;
    chart.zoomOutText = "Mostrar Todo";
    chart.autoResize = true;
    chart.responsive = {
        "enabled": true
    };

    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    //categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#FAFAFA";
    categoryAxis.gridPosition = "start";
    categoryAxis.axisAlpha = 1;
    categoryAxis.autoWrap = false;
    categoryAxis.minHorizontalGap = 0;
    categoryAxis.gridThickness = 0;
    categoryAxis.fontSize = 8;

    // value
    var valueAxis = new amc.ValueAxis();
    valueAxis.dashLength = 5;
    valueAxis.gridAlpha = 0;
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph1 = new amc.AmGraph();
    graph1.type = "line";
    //graph1.colorField = "color1";
    graph1.lineColor = "#00a94f";//"#27c5ff";
    graph1.bulletColor = "#00a94f";
    //graph1.bulletBorderColor = "#27c5ff";
    graph1.bulletBorderThickness = 2;
    graph1.bulletBorderAlpha = 1;
    graph1.valueField = sValor1;
    graph1.lineThickness = 3;
    graph1.bullet = "round";
    graph1.fillAlphas = 0;
    graph1.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph1.title = "Meta Alcanzada";
    graph1.labelText = "[[value]]";
    graph1.visibleInLegend = true;
    chart.addGraph(graph1);


    // CURSOR
    var chartCursor = new amc.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = true;
    chartCursor.categoryBalloonEnabled = true;
    chart.addChartCursor(chartCursor);
    // WRITE
    chart.write(divChart);
}

function fjsGeneraHistogramaIndicadoresSectoriales4T(chartData, sCategoria, sValorLinea, sValor1, sValor2, sValor3, divChart, sTituloAMostrar, showScroll, iBanInd) {
    // SERIAL CHART
    var amc = AmCharts;
    chart = new amc.AmSerialChart();
    var legend = new amc.AmLegend();
    //legend.position = "top";    
    legend.useGraphSettings = true;
    //legend.markerType = "bubble";
    legend.markerSize = 4;
    legend.verticalGap = 15;
    legend.spacing = 50;
    legend.horizontalGap = 10;
    legend.valueText = "";
    if (iBanInd == 1) {
        chart.addLegend(legend, 'divFooterGrafIndicadorSectorial');

    }   
    
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12,"#072a5f");
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.categoryField = sCategoria;
    chart.startDuration = 2;
    chart.zoomOutText = "Mostrar Todo";
    chart.autoResize = true;
    chart.responsive = {
        "enabled": true
    };

   
       
    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    //categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#FAFAFA";
    categoryAxis.gridPosition = "start";
    categoryAxis.axisAlpha = 1;
    categoryAxis.autoWrap = false;
    categoryAxis.minHorizontalGap = 0;
    categoryAxis.gridThickness = 0;
    categoryAxis.fontSize = 8;
    //categoryAxis.axisHeight = 132;
    // value
    var valueAxis = new amc.ValueAxis();
    valueAxis.dashLength = 5;
    valueAxis.gridAlpha = 0;
    valueAxis.axisAlpha = 0;    
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph0 = new amc.AmGraph();
    graph0.type = "line";
    //graph1.colorField = "color1";
    graph0.lineColor = "#c1c1c1";//"#27c5ff";
    graph0.bulletColor = "#c1c1c1";
    //graph0.bulletBorderColor = "#00a94f";
    graph0.bulletBorderThickness = 2;
    graph0.bulletBorderAlpha = 1;
    graph0.valueField = sValorLinea;
    graph0.lineThickness = 3;
    graph0.bullet = "round";   
    graph0.fillAlphas = 0;
    graph0.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph0.title = "L\u00EDnea Base";
    graph0.labelText = "[[value]]";
    console.log(sValorLinea.LineaBase);
    graph0.visibleInLegend = true;
    graph0.legendAlpha = 1;
    graph0.bulletAlpha = 1;
    graph0.lineAlpha = 1;
    graph0.index = 2;
    graph0.labelPosition = "bottom";
    chart.addGraph(graph0);

    // GRAPH
    var graph1 = new amc.AmGraph();
    graph1.type = "line";
    //graph1.colorField = "color1";
    graph1.lineColor = "#00a94f";//"#27c5ff";
    graph1.bulletColor = "#00a94f";
    //graph1.bulletBorderColor = "#27c5ff";
    graph1.bulletBorderThickness = 2;
    graph1.bulletBorderAlpha = 1;
    graph1.valueField = sValor1;
    graph1.lineThickness = 3;
    graph1.bullet = "round";
    graph1.fillAlphas = 0;            
    graph1.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph1.title = "Meta Alcanzada";
    graph1.labelText = "[[value]]";
    graph1.visibleInLegend = true;
    graph1.legendAlpha = 0;
    graph1.bulletAlpha = 1;
    graph1.index = 1;
    graph1.labelPosition = "bottom";
    chart.addGraph(graph1);   
    
    // GRAPH
    var graph2 = new amc.AmGraph();
    graph2.type = "line";
    //graph2.colorField = "color2";
    graph2.lineColor = "#072a5f";//"#27c5ff";
    graph2.bulletColor = "#072a5f";
    //graph2.bulletBorderColor = "#F7D358";
    graph2.bulletBorderThickness = 2;
    graph2.bulletBorderAlpha = 1;
    graph2.valueField = sValor2;
    graph2.lineThickness = 3;
    graph2.bullet = "round";
    graph2.fillAlphas = 0;
    graph2.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph2.title = "Meta Intermedia";
    graph2.labelText = "[[value]]";
    graph2.visibleInLegend = true;
    graph2.index = 3;
    graph2.labelPosition = "bottom";
    chart.addGraph(graph2);

    // GRAPH
    var graph3 = new amc.AmGraph();
    graph3.type = "line";
    //graph2.colorField = "color2";
    graph3.lineColor = "#868484";//"#27c5ff";
    graph3.bulletColor = "#868484";
    // graph3.bulletBorderColor = "#F7D358";
    graph3.bulletBorderThickness = 2;
    graph3.bulletBorderAlpha = 1;
    graph3.valueField = sValor3;
    graph3.lineThickness = 3;
    graph3.bullet = "round";
    graph3.fillAlphas = 0;
    graph3.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph3.title = "Meta Planeada";
    graph3.labelText = "[[value]]";
    graph3.visibleInLegend = true;
    graph3, index = 4;
    graph3.labelPosition = "bottom";
    chart.addGraph(graph3);
    
    amc.addInitHandler(function (chart) {
        chart.graphs.sort(function (a, b) {
            if (a.index > b.index) return 1;
            if (a.index < b.index) return -1;
            return 0;
        });
    }, ["serial"]);
      
    // CURSOR
    var chartCursor = new amc.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = true;
    chartCursor.categoryBalloonEnabled = true;
    chart.addChartCursor(chartCursor);
    // WRITE
    chart.write(divChart);  

   
}



function fjsGeneraHistogramaComparativa4T(chartData, sTitulo, sValor1, sValor2, divChart, sTituloAMostrar, showScroll) {
    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.plotAreaFillColors = "#D2D2D2";
    chart.plotAreaFillAlphas = 1;
    chart.categoryField = sTitulo;
    chart.startDuration = 2;
    chart.zoomOutText = "Mostrar Todo";
    chart.autoResize = true;
    chart.responsive = {
        "enabled": true
    };
    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    //categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#D2D2D2";
    categoryAxis.gridPosition = "start";

    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.dashLength = 5;
    valueAxis.gridAlpha = 0;
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph1 = new AmCharts.AmGraph();
    graph1.type = "line";
    //graph1.colorField = "color1";
    graph1.lineColorField = "color1";//"#27c5ff";
    graph1.bulletColor = "#FFFFFF";
    graph1.bulletBorderColor = "#7E015B";
    graph1.bulletBorderThickness = 2;
    graph1.bulletBorderAlpha = 1;
    graph1.valueField = sValor1;
    graph1.lineThickness = 2;
    graph1.bullet = "round";
    graph1.fillAlphas = 0;
    graph1.balloonText = "<span style='font-size:13px;'>[[title]] Meta Planeada [[category]]:<b>[[value]]</b></span>";
    chart.addGraph(graph1);

    // GRAPH
    var graph2 = new AmCharts.AmGraph();
    graph2.type = "line";
    //graph2.colorField = "color2";
    graph2.lineColorField = "color2";;//"#27c5ff";
    graph2.bulletColor = "#FFFFFF";
    graph2.bulletBorderColor = "#3C1559";
    graph2.bulletBorderThickness = 2;
    graph2.bulletBorderAlpha = 1;
    graph2.valueField = sValor2;
    graph2.lineThickness = 2;
    graph2.bullet = "round";
    graph2.fillAlphas = 0;
    graph2.balloonText = "<span style='font-size:13px;'>[[title]] Meta Alcanzada [[category]]:<b>[[value]]</b></span>";
    chart.addGraph(graph2);

    // CURSOR
    var chartCursor = new AmCharts.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = true;
    chartCursor.categoryBalloonEnabled = true;
    //chartCursor.oneBalloonOnly=true;

    //chartCursor.enabled=false; JCC
    chart.addChartCursor(chartCursor);

    // SCROLLBAR
    /* if(showScroll){
        var chartScrollbar = new AmCharts.ChartScrollbar();
        chart.addChartScrollbar(chartScrollbar);
     }*/
    // WRITE
    chart.write(divChart);
}

function fjsGeneraHistograma4T(chartData, sTitulo, sValor, divChart, sTituloAMostrar, sClickParam) {
    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.categoryField = sTitulo;
    chart.startDuration = 2;
    chart.zoomOutText = "Mostrar Todo";

    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    //categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#FAFAFA";
    categoryAxis.gridPosition = "start";

    // value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.dashLength = 5;
    //valueAxis.title = "Visitors from country";
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph2 = new AmCharts.AmGraph();
    graph2.type = "line";
    graph2.lineColor = "#27c5ff";
    graph2.bulletColor = "#FFFFFF";
    graph2.bulletBorderColor = "#27c5ff";
    graph2.bulletBorderThickness = 2;
    graph2.bulletBorderAlpha = 1;
    graph2.valueField = sValor;
    graph2.lineThickness = 2;
    graph2.bullet = "round";
    graph2.fillAlphas = 0;
    graph2.balloonText = "<span style='font-size:13px;'>[[title]] A&ntilde;o [[category]]:<b>[[value]]</b></span>";
    chart.addGraph(graph2);

    // CURSOR
    var chartCursor = new AmCharts.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = false;
    chartCursor.categoryBalloonEnabled = true;
    //chartCursor.oneBalloonOnly=true;
    chart.addChartCursor(chartCursor);

    // SCROLLBAR
    var chartScrollbar = new AmCharts.ChartScrollbar();
    chart.addChartScrollbar(chartScrollbar);

    // WRITE
    chart.write(divChart);
}

function fjsGeneraGraficaDetalleIndicador4T(chartData, sCategoria, sValorLinea, sValor1, sValor2, sValor3, divChart, sTituloAMostrar, showScroll, iBanInd) {
    //(chartData, sCategoria, sValorLinea, sValor1, sValor2, sValor3, divChart, sTituloAMostrar, showScroll, iBanInd) {

    // SERIAL CHART
    var amc = AmCharts;
    chart = new amc.AmSerialChart();
    var legend = new amc.AmLegend();

    //legend.position = "top";
    legend.useGraphSettings = true;
    //legend.markerType = "bubble";
    legend.markerSize = 4;
    legend.verticalGap = 15;
    legend.spacing = 50;
    legend.horizontalGap = 10;
    legend.valueText = "";
    if (iBanInd == 1) {
        chart.addLegend(legend, 'divFooterGrafIndicadorSectorial');

    }
    if (sTituloAMostrar) chart.addTitle(sTituloAMostrar, 12);
    chart.pathToImages = "/img/Grafica/";
    chart.dataProvider = chartData;
    chart.categoryField = sCategoria;
    chart.startDuration = 2;
    chart.zoomOutText = "Mostrar Todo";
    chart.autoResize = true;
    chart.responsive = {
        "enabled": true
    };

    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    //categoryAxis.labelRotation = 45;
    categoryAxis.gridAlpha = 0;
    categoryAxis.fillAlpha = 1;
    categoryAxis.fillColor = "#FAFAFA";
    categoryAxis.gridPosition = "start";
    categoryAxis.axisAlpha = 1;
    categoryAxis.autoWrap = false;
    categoryAxis.minHorizontalGap = 0;
    categoryAxis.gridThickness = 0;
    categoryAxis.fontSize = 8;

    // value
    var valueAxis = new amc.ValueAxis();
    valueAxis.dashLength = 5;
    valueAxis.gridAlpha = 0;
    valueAxis.axisAlpha = 0;
    chart.addValueAxis(valueAxis);

    // GRAPH
    var graph1 = new amc.AmGraph();
    graph1.type = "line";
    //graph1.colorField = "color1";
    graph1.lineColor = "#00a94f";//"#27c5ff";
    graph1.bulletColor = "#00a94f";
    //graph1.bulletBorderColor = "#27c5ff";
    graph1.bulletBorderThickness = 2;
    graph1.bulletBorderAlpha = 1;
    graph1.valueField = sValor1;
    graph1.lineThickness = 3;
    graph1.bullet = "round";
    graph1.fillAlphas = 0;
    graph1.balloonText = "<span style='font-size:13px;'>[[title]] [[category]]:<b>[[value]]</b></span>";
    graph1.title = "Meta Alcanzada";
    graph1.labelText = "[[value]]";
    graph1.visibleInLegend = true;
    chart.addGraph(graph1);


    // CURSOR
    var chartCursor = new amc.ChartCursor();
    chartCursor.cursorAlpha = 1;
    chartCursor.zoomable = true;
    chartCursor.categoryBalloonEnabled = true;
    chart.addChartCursor(chartCursor);
    // WRITE
    chart.write(divChart);
}

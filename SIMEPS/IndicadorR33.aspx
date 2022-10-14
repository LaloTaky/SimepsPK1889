<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndicadorR33.aspx.cs" Inherits="SIMEPS.IndicadorR33" %>



<asp:Content ID="Content3" ContentPlaceHolderID="cphTituloHeader" runat="server">

    <script src="<%:Page.ResolveUrl("~/Scripts/jquery-1.12.3.min.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/Scripts/charts/amcharts.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/scripts/charts/ammap.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/scripts/charts/light.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/Scripts/mexicoSae.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/Scripts/IndicadorR33.js") %>" type="text/javascript"></script>
    <style>
        .chartMap {
            width: 100%;
            height: 230px;
        }

        .img-descarga {
            float: left;
        }

        .datosBase {
            position: absolute;
            padding-top: 10px;
            padding-left: 15px;
        }

        .title-amps {
            font-weight: bold;
            text-align: center;
        }

        .chartMap {
            width: 100%;
            height: 230px;
        }

        #GraficaDesempenioHist {
            width: 100%;
            height: 230px;
        }

        .negritas {
            font-weight: bold;
        }

        .NomIndicador {
            margin: auto !important;
            color: #333;
            background-color: #f5f5f5;
            border-color: #ddd;
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        .D_Indicador {
            padding-top: 10px;
        }
        
        .DescripcionAmb {
            font-weight: normal !important;
            width: 100%;
        }
        .Div-DescAmb {
            text-align: center;
            padding: 20px;
            overflow: auto;
            overflow: hidden;
        }
    </style>

    <p class="lblTitulo">
        <asp:Label runat="server" ID="lblTitulo">Módulo de indicadores de Ramo 33 del ámbito social</asp:Label>
    </p>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <br />
        <div class="content">
            <input type="hidden" id="arrayjsonEnt" class="arrayjsonEnt" runat="server" />
            <input type="hidden" id="arrayjsonDes" class="arrayjsonDes" runat="server" />
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                    <h3>
                        <strong>
                            <asp:Label ID="LblFondoSiglas" runat="server"></asp:Label>
                        </strong>
                    </h3>
                    <br />
                </div>
                <div style="padding-left: 15px; padding-right: 15px;">
                    <div class="row col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 NomIndicador panel" style="border-bottom: 1px solid #bbb;">
                        <div class="col-md-3">
                            <div class="panel-heading negritas">Nombre del Indicador</div>
                        </div>
                        <div class="col-md-6 D_Indicador">
                            <asp:Label ID="lblNombreIndicador" runat="server" Text="" CssClass="titulo-niveles1"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <img src="img/descarga_excel.jpg" alt="EXCEL" class="img-descarga" />
                            <span class="input-group-text datosBase">Descarga base de datos</span>
                        </div>
                    </div>
                </div>
                <div class="row col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12" style="margin-top: 20px;">
                    <div class="col-md-4 col-lg-6">
                        <div id="GraficaDesempenioHist"></div>
                        <asp:DropDownList runat="server"  ID="ddlGraficaProp" CssClass="selectDesempenioHist" Style="display: none">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 col-lg-3 title-amps ">
                        <span class="input-group-text title-amps ValorEntidad "></span>
                        <div class="chartMap" id='mapEntidad_'></div>
                    </div>
                    <div class="col-md-4 col-lg-3 title-amps ">
                        <span class="input-group-text Desempenio"></span>
                        <div class="chartMap" id='mapDes_'></div>
                    </div>
                </div>
                <div class="row col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                    <div class="col-md-6 pull-right Div-DescAmb">
                        <span class="input-group-text title-amps DescripcionAmb" runat="server" id="DescripcionAmb"></span>
                    </div>
                </div>
                <div class="row col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                    <div class="col-md-4 col-lg-4">
                        <div class="panel panel-default">
                            <div class="panel-heading negritas">Descripción:</div>
                        </div>
                        <div>
                            <asp:Label ID="lblDescripcion" runat="server" Text="" CssClass="titulo-niveles1"></asp:Label>
                        </div>
                        <br />
                    </div>
                    <div class="col-md-4 col-lg-4">
                        <div class="panel panel-default">
                            <div class="panel-heading negritas">Unidad de medida:</div>
                        </div>
                        <div>
                            <asp:Label ID="lblUnidadMedida" runat="server" Text="" CssClass="titulo-niveles1"></asp:Label><br />
                        </div>
                    </div>
                    <div class="col-md-4 col-lg-4">
                        <div class="panel panel-default">
                            <div class="panel-heading negritas">Calificación del Indicador:</div>
                        </div>
                        <div id="LeyendaAviso" runat="server" class="row col-sm-12 text-center" style="display: none">
                            <asp:Label runat="server" ID="LblavisoCalidad" Text="El indicador no cuenta con valoración para este año"></asp:Label><br />
                        </div>
                        <div id="divSemaforos" runat="server">
                            <div class="row col-lg-12">
                                <div class="col-sm-2">
                                    <div id="cajaClaridad" runat="server" class="cajaVerde" style="width:20px; height:20px;"></div>
                                    <br />
                                </div>
                                <div class="col-sm-9" style="padding-left: 0">
                                    <span>Claridad</span>
                                </div>
                            </div>
                            <div class="row col-lg-12">
                                <div class="col-sm-2">
                                    <div id="cajaRelevancia" runat="server" class="cajaVerde" style="width:20px; height:20px;"></div>
                                    <br />
                                </div>
                                <div class="col-sm-9" style="padding-left: 0">
                                    <span>Relevancia</span>
                                </div>
                            </div>
                            <div class="row col-lg-12">
                                <div class="col-sm-2">
                                    <div id="cajaMonito" runat="server" class="cajaVerde" style="width:20px; height:20px;"></div>
                                    <br />
                                </div>
                                <div class="col-sm-9" style="padding-left: 0">
                                    <span>Monitoreabilidad</span>
                                </div>
                            </div>
                            <div class="row col-sm-12">
                                <div class="col-sm-2">
                                    <div id="cajaAdecuacion" runat="server" class="cajaVerde" style="width:20px; height:20px;"></div>
                                    <br />
                                </div>
                                <div class="col-sm-9" style="padding-left: 0">
                                    <span>Adecuación</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                    <div class="col-md-4 col-lg-4">
                        <div class="panel panel-default">
                            <div class="panel-heading negritas">Método de cálculo:</div>
                        </div>
                        <div>
                            <asp:Label ID="lblMetodoCalculo" runat="server" Text="" CssClass="titulo-niveles1"></asp:Label><br />
                        </div>
                    </div>
                    <div class="col-md-4 col-lg-4">
                        <div class="panel panel-default">
                            <div class="panel-heading negritas">Frecuencia de medición:</div>
                        </div>
                        <div>
                            <asp:Label ID="lblFrecuenciaMedicion" runat="server" Text="" CssClass="titulo-niveles1"></asp:Label><br />
                        </div>
                    </div>
                    <div class="col-md-4 col-lg-4">
                        <div class="panel panel-default">
                            <div class="panel-heading negritas">Medios de Verificacion:</div>
                        </div>
                        <div style="word-wrap:break-word">
                            <asp:Label ID="lblMediosVerificacion" runat="server" Text="" CssClass="titulo-niveles1"></asp:Label><br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        
        (function () {
            fjsCargarGraficaIndicadores('ddlGraficaProp');
        })();

        function fjsCargarGraficaIndicadores(ddlPattern) {
            var colSelect = document.getElementsByTagName('select');
            for (var iIndex = 0; iIndex < colSelect.length; iIndex++) {
                if ((colSelect[iIndex].id + 'X').indexOf(ddlPattern) > -1) {
                    fjsGraficarIndicador(colSelect[iIndex], false);
                }
            }
        }

        function fjsGraficarIndicador(ddl, showScroll) {
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
                        fjsGeneraHistogramaComparativa(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', 'GraficaDesempenioHist', 'Desempeño Histórico', showScroll);
                    }
                }
            }
        }

    </script>
</asp:Content>



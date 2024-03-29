﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndicadoresR33.aspx.cs" Inherits="SIMEPS.IndicadoresR33" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTituloHeader" runat="server">
    <p class="lblTitulo col-sm-12">
        <asp:Label runat="server" ID="lblTitulo">Módulo de indicadores de Ramo 33 del ámbito social</asp:Label>
    </p>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="<%:Page.ResolveUrl("~/Scripts/charts/amcharts.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/scripts/charts/ammap.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/scripts/charts/light.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/Scripts/mexicoSae.js") %>" type="text/javascript"></script>
    <script src="<%:Page.ResolveUrl("~/Scripts/indicadorRamo33.js") %>" type="text/javascript"></script>
    <style>
        .chartMap {
            width: 100%;
            height: 230px;
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

    <div class="main">
        <asp:Label  ID="lblVerMIR" runat="server" class="col-sm-2 flo" style="padding: 22px; float: right;"></asp:Label>
        <br />
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <div style="float:right;">
                    <table>    
                        <tr>
                            <td>
                                <a id="xlsBdR33">
                                    <img src="img/descarga_excel.jpg" class="img-descarga" /></a>
                            </td>
                            <td rowspan="2">Descarga base de datos de 
                                <br />
                                <asp:Label ID="LblFondoNombreBD" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a id="csvBdR33">
                                    <img src="img/descarga_csv.jpg" class="img-descarga" /></a>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                
            <br />
            </div>
        <div class="row" runat="server" id="matrizFondo">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
            <h3>
                <strong>
                    <asp:Label ID="LblFondoSiglas" runat="server"></asp:Label></strong>
                <asp:Label ID="LblFondoNombre" runat="server"></asp:Label>
            </h3>
                
            <br />
            </div>
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                <div id="accordion borde">
                    <input type="hidden" id="arrayjsonEnt" class="arrayjsonEnt" runat="server" />
                    <input type="hidden" id="arrayjsonDes" class="arrayjsonDes" runat="server" />


                    <div class="card navbar navbar-default" role="navigation">
                        <div class="card-header" id="headingProp">
                            <a href="#" class="btn btn-default btn-programa" data-toggle="collapse" data-target="#proposito_" aria-expanded="true" aria-controls="proposito_" onclick="fjschangerow(this)" style="text-align: left;">
                                <span id="spnTitProposito" runat="server" class="titulo-niveles"></span>
                                <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                            </a>
                        </div>
                        <div id="proposito_" class="collapse in" aria-labelledby="headingProp" data-parent="#accordion">
                            <asp:Repeater ID="rprObjetivosComp" runat="server" OnItemDataBound="rprObjetivosComp_ItemDataBound">
                                <ItemTemplate>
                                    <div class="rowPemir">
                                        <div class="rowDescObjetivo">
                                            <div class="card navbar navbar-default" role="navigation">
                                                <div class="card-header" id="headingTwo">
                                                    <a href="#" class="btn btn-default btn-programa" data-toggle="collapse" data-target="#proposito_<%#Eval("Index") %>" aria-expanded="true" aria-controls="proposito_<%#Eval("Index") %>" onclick="fjschangerow(this)" style="text-align: left;">
                                                        <span class="titulo-niveles"><%#Eval("DescNivel") %></span>
                                                        <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                                                    </a>
                                                </div>
                                                <div id="proposito_<%#Eval("Index") %>" class="collapse in" aria-labelledby="headingTwo" data-parent="#accordion">
                                                    <div class="rowPemir">
                                                        <div class="rowDescObjetivo">
                                            <div class="card-body">
                                                                <div class="card navbar navbar-default" role="navigation">
                                                                    <div class="card-header" id="headingtwo">
                                                                        <a href="#" class="btn btn-default btn-programa" data-toggle="collapse" data-target="#num_<%#Eval("Index") %>" aria-expanded="true" aria-controls="num_<%#Eval("Index") %>" onclick="fjschangerow(this)" style="text-align: left;">
                                                                            <span class="titulo-niveles">Indicadores (<%#Eval("CantidadIndicadores") %>)</span>
                                                    <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                                                </a>
                                            </div>
                                                                    <div id="num_<%#Eval("Index") %>" class="collapse in" aria-labelledby="headingtwo" data-parent="#accordion">
                                                                        <div class="panel-heading panel-heading-ico panel-heading-noneLink panelProgramas">
                                                                            <asp:Repeater ID="rprObjIndicadores" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem,"Indicadores") %>'>
                                            <ItemTemplate>
                                                    <div class="rowPemir">
                                                                                        <div class="rowDescObjetivo">
                                                                                            <div class="card-header">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <a href='<%# DataBinder.Eval(Container.DataItem,"Url") %>' class="btn btn-default btn-objetivo">
                                                                                                            <asp:Label ID="lblDescNivelComp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NombreIndicador") %>'></asp:Label>
                                                                </a>
                                                            </div>
                                                            </div>
                                                        </div>
                                                                                            <div class="card-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div id='graph_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>' style="width: 100%; height: 220px;">
                                                    </div>
                                                                                                        <asp:DropDownList ID="ddlGraficaProp" runat="server" CssClass="select"
                                                                                                            DataTextField="GRAFICA" DataValueField="NO" Style="display: none" DataSource='<%# DataBinder.Eval(Container.DataItem,"VALORES_GRAFICA") %>'>
                                                                </asp:DropDownList>
                                                            </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class=" col-md-6">
                                                                                                            <span class="input-group-text title-amps ValorEntidad_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>"></span>
                                                                                                            <div class="chartMap" id='mapEntidad_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>'></div>
                                                                                                        </div>
                                                                                                        <div class=" col-md-6">
                                                                                                            <span class="input-group-text Desempenio_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>"></span>
                                                                                                            <div class="chartMap" id='mapDes_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>'></div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6 pull-right Div-DescAmb">
                                                                                                        <span class="input-group-text title-amps DescripcionAmb" runat="server" id="DescripcionAmb"><%#Eval("DescCobertura") %></span>
                                                            </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                    <div class="card navbar navbar-default" role="navigation">
                        <div class="card-header" id="headingOne">
                            <a href="#" class="btn btn-default btn-programa" data-toggle="collapse" data-target="#componente_" aria-expanded="true" aria-controls="componente_" onclick="fjschangerow(this)" style="text-align: left;">
                                <span id="spnTitComponente" runat="server" class="titulo-niveles"></span>
                                <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                            </a>
                        </div>
                        <div id="componente_" class="collapse in" aria-labelledby="headingOne" data-parent="#accordion">
                            <asp:Repeater ID="rprComponentes" runat="server" OnItemDataBound="rprComponentes_ItemDataBound">
                                <ItemTemplate>
                                    <div class="rowPemir">
                                        <div class="rowDescObjetivo">
                                            <div class="card navbar navbar-default" role="navigation">
                                                <div class="card-header" id="headingTwo">
                                                    <a href="#" class="btn btn-default btn-programa" data-toggle="collapse" data-target="#descComponente_<%#Eval("Index") %>" aria-expanded="true" aria-controls="descComponente_<%#Eval("Index") %>" onclick="fjschangerow(this)" style="text-align: left;">
                                                        <span class="titulo-niveles"><%#Eval("DescNivel") %></span>
                                                        <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                                                    </a>
                                                </div>
                                                <div id="descComponente_<%#Eval("Index") %>" class="collapse in" aria-labelledby="headingTwo" data-parent="#accordion">
                                        <div class="rowPemir">
                                            <div class="rowDescObjetivo">
                                                <div class="card-body">
                                                                <div class="card navbar navbar-default" role="navigation">
                                                                    <div class="card-header" id="headingtwo">
                                                                        <a href="#" class="btn btn-default btn-programa" data-toggle="collapse" data-target="#IndicadoresComp_<%#Eval("Index") %>" aria-expanded="true" aria-controls="IndicadoresComp_<%#Eval("Index") %>" onclick="fjschangerow(this)" style="text-align: left;">
                                                                            <span class="titulo-niveles">Indicadores (<%#Eval("CantidadIndicadores") %>)</span>
                                                        <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                                                    </a>
                                                </div>
                                                                    <div id="IndicadoresComp_<%#Eval("Index") %>" class="collapse in" aria-labelledby="headingtwo" data-parent="#accordion">
                                                                        <div class="panel-heading panel-heading-ico panel-heading-noneLink panelProgramas">
                                                                            <asp:Repeater ID="rprCompIndicadores" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem,"Indicadores") %>'>
                                                                                <ItemTemplate>
                                                                                    <div class="rowPemir">
                                                                                        <div class="rowDescObjetivo">
                                                                                            <div class="card-header">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <a href='<%# DataBinder.Eval(Container.DataItem,"Url") %>' class="btn btn-default btn-objetivo">
                                                                                                            <asp:Label ID="lblDescNivelComp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NombreIndicador") %>'></asp:Label>
                                                                                                        </a>
                                            </div>
                                        </div>
                                    </div>
                                                                                            <div class="card-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div id='graph_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>' style="width: 100%; height: 220px;">
                                                                    </div>
                                                                                                        <asp:DropDownList ID="ddlGraficaProp" runat="server" CssClass="select"
                                                                                                            DataTextField="GRAFICA" DataValueField="NO" Style="display: none" DataSource='<%# DataBinder.Eval(Container.DataItem,"VALORES_GRAFICA") %>'>
                                                                                                        </asp:DropDownList>
                                                            </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class=" col-md-6">
                                                                                                            <span class="input-group-text title-amps ValorEntidad_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>"></span>
                                                                                                            <div class="chartMap" id='mapEntidad_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>'></div>
                                                            </div>
                                                                                                        <div class=" col-md-6">
                                                                                                            <span class="input-group-text Desempenio_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>"></span>
                                                                                                            <div class="chartMap" id='mapDes_<%# DataBinder.Eval(Container.DataItem,"IdIndicador") %>'></div>
                                                        </div>
                                                    </div>
                                                            </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6 pull-right Div-DescAmb">
                                                                                                        <span class="input-group-text title-amps DescripcionAmb" runat="server" id="DescripcionAmb"><%#Eval("DescCobertura") %></span>
                                                            </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                        </div>
                    </div>
                </div>
            </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                    </div>
                </ItemTemplate>
                            </asp:Repeater>
                        </div>
        </div>

        </div>
    </div>
        </div>
    </div>
    <script type="text/javascript">
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
                        fjsGeneraHistogramaComparativa(chartDataDer, 'ciclo', 'MetaPlaneada', 'MetaAlcanzada', 'graph_' + idDivGrafica, 'Desempeño Histórico', showScroll);
                    }
                }
            }
        }
    </script>
</asp:Content>


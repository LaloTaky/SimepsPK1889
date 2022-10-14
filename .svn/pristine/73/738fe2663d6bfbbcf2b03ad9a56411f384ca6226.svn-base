<%@ Page Title="SIMEPS - Sistema de Medición de la Política Social" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Programa.aspx.cs" Inherits="SIMEPS.Programa" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTituloHeader" runat="server">
    <p class="lblTitulo">
        <asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de los programas y acciones de desarrollo social</asp:Label>
    </p>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divBotones" class="secprogocu">
        <input type="submit" id="linkDescargaRepMonitoreo" onclick='fjsDetenerEvento(event); fjsDescargaRepMonitoreo(this, false);' value="Descarga Reporte de Monitoreo" class="botonDescargaMonitoreo" />
        <input type="submit" id="linkDescargaFichaTecnica" onclick='fjsDetenerEvento(event); fjsDescargaFichaIndicadores(this, false);' value="Descarga Ficha de los indicadores" class="botonDescarga" />
        <input type="submit" id="linkDescargaBaseIndicadores" onclick='fjsDetenerEvento(event); fjsDescargaBaseIndicadores(this, 1);' value="Descarga base de datos de los indicadores" class="botonDescarga" />
        <input type="submit" id="linkDescargaBaseIndicadoresCSV" onclick='fjsDetenerEvento(event); fjsDescargaBaseIndicadoresCSV(this, 1);' value="CSV" class="botonDescargaCSV" />



    </div>
    <div class="main">
        <label style="display: none;" id="LblProgEspecial" runat="server"></label>
        <div id="divHead" runat="server" class="headerPrograma" onload="divHead_Load">
            <div class="headerPrograma">
                <br />
                <asp:Label ID="LblDependencia" runat="server" Text="DEPENDENCIA"></asp:Label><br />
                <asp:Label runat="server" ID="LblNomPrograma" Text="Programa"></asp:Label><br />
                <br />
                <asp:Label ID="LblCiclo" runat="server" Text="CICLO"></asp:Label>
            </div>
        </div>
        <div id="divValoraciones" class="rowPemir secprogocu" style="padding: 0.5em  1em">
            <h4 style="color: #00a94f; font-weight: bold;">Valoraciones CONEVAL</h4>
            <div id="divDictamen" runat="server">
                <asp:Label ID="LblTituloDictamen" runat="server" Text="Dictamen de aprobación: " Font-Bold="true"></asp:Label>
                <asp:Label runat="server" Font-Bold="true" ID="LblDictamen" Text="NA"></asp:Label>
            </div>
            <div>
                <b>Observaciones históricas anuales promedio por indicador: </b>
                <asp:Label runat="server" Font-Bold="true" ID="LblObservaciones" Text="-"></asp:Label>
            </div>
            <div>
                <b>Tasa de permanencia anual: </b>
                <asp:Label runat="server" Font-Bold="true" ID="LblTasaPermanencia" Text="-"></asp:Label>
            </div>


        </div>
        <br />
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
            <div id="accordion borde">
                <div class="card navbar navbar-default" role="navigation">
                    <div class="card-header" id="headingOne">
                        <a href="#" class="btn btn-default btn-programa" data-toggle="collapse" data-target="#proposito_" aria-expanded="true" aria-controls="proposito_" onclick="fjschangerow(this)">
                            <asp:Label ID="lblPropositos" runat="server" Text="" CssClass="titulo-niveles"></asp:Label>
                            <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                        </a>
                    </div>
                    <div id="proposito_" class="collapse in" aria-labelledby="headingOne" data-parent="#accordion">
                        <asp:Repeater ID="rprObjetivosProp" runat="server" DataSourceID="odsObjetivosProp" OnPreRender="RepeaterObj_LoadP">
                            <ItemTemplate>
                                <div class="rowPemir">
                                    <div class="rowDescObjetivo">
                                        <div class="card-body">
                                            <a href="#numObj1_<%#Eval("ID_NIVEL") %>" data-toggle="collapse" class="btn btn-default btn-objetivo" onclick="fjschangerow(this)">
                                                <asp:Label ID="lblDescNivelProp" runat="server" Text='<%#Eval("DESC_NIVEL") %>'></asp:Label>
                                                <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                                            </a>
                                            <asp:Label ID="lblIdNivelProp" runat="server" Text='<%#Eval("ID_NIVEL") %>' CssClass="labelObjInd"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div id="numObj1_<%#Eval("ID_NIVEL") %>" class="panel-collapse collapse in">
                                    <asp:Repeater ID="rprIndProp" runat="server" DataSource='<%#Eval("INDICADORES") %>'>
                                        <ItemTemplate>
                                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                <br />
                                                <div class="rowPemir">
                                                    <div class="rowIndicador col-lg-12" onmouseover="this.className='rowIndicadorHover col-lg-12';" onmouseout="this.className='rowIndicador col-lg-12';">
                                                        <div class="col-lg-8 reajustarCol">
                                                            <a id="linkIndProp" runat="server" href='<%#Eval("LIGA")+"&ciclo="+Request.Params["pCiclo"] +"&siglas="+Request.Params["siglas"]%>' class="LigaIndicador" target="_self" onclick="fjsDetenerEvento(event);mostrarIndicador(this);">
                                                                <div id="divLigaProp" style="width: 100%">
                                                                    <asp:Label ID="lblNomIndProp" runat="server" Text='<%#Eval("NOMBRE_IND") %>'></asp:Label>
                                                                    <asp:Label ID="lblIdIndicadorIndProp" runat="server" Text='<%#Eval("ID_INDICADOR") %>' CssClass="labelObjInd"></asp:Label>
                                                                </div>
                                                            </a>
                                                        </div>
                                                        <div class="col-lg-4 secprogocu <%#Eval("CALIFICACION_PROMEDIO")  %>" style="margin-left: 80px;">
                                                            <asp:Label ID="LblSinCaliL" runat="server" Text='<%#Eval("CALIFICACION_PROMEDIO").ToString().Equals("divLeyenda")?"El indicador no cuenta con valoración para este año.":""%>' CssClass="LigaIndicador"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="rowPemir">
                                                    <div class="bordeInd">
                                                        <div id="divGraficaProp">
                                                            <div id="<%#Eval("ID_INDICADOR") %>" style="width: 100%; height: 220px;"></div>
                                                            <asp:DropDownList ID="ddlGraficaProp" runat="server" CssClass="select" DataSource='<%#Eval("HISTORICOS") %>'
                                                                DataTextField="GRAFICA" DataValueField="NO" Style="display: none">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="fuenteGrafica">
                                                            Fuente: Elaboración CONEVAL con base en información del Portal Aplicativo de​ la Secretaría de Hacienda y Crédito Público (PASH)
                                                        </div>
                                                        <div>
                                                            <asp:Label runat="server" ID="lblNotaIzq" Visible='<%#Eval("TieneIndicadorComple") %>' CssClass="fuenteGrafica" Text="*Nota: Información proporcionada directamente por el programa​​​"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <asp:ObjectDataSource ID="odsObjetivosProp" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarObjetivos">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="idMatriz" QueryStringField="pIdMatriz" Type="Decimal" DefaultValue="0" />
                        <asp:QueryStringParameter Name="dIndicador" DefaultValue="0" />
                        <asp:QueryStringParameter Name="sNivel" DefaultValue="2" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <div class="card navbar navbar-default" role="navigation">
                    <div class="card-header" id="headingtwo">
                        <a href="#" class="btn btn-default btn-programa" data-toggle="collapse" data-target="#num_" aria-expanded="true" aria-controls="num" onclick="fjschangerow(this)">
                            <asp:Label ID="lbCountObjetivos" runat="server" Text="" CssClass="titulo-niveles"></asp:Label>
                            <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                        </a>
                    </div>
                    <div id="num_" class="collapse in" aria-labelledby="headingtwo" data-parent="#accordion">
                        <asp:Repeater ID="rprObjetivosComp" runat="server" DataSourceID="odsObjetivosComp" OnPreRender="RepeaterObj_Load">
                            <ItemTemplate>
                                <div class="panel-heading panel-heading-ico panel-heading-noneLink panelProgramas">
                                    <div class="rowPemir">
                                        <div class="rowDescObjetivo">
                                            <div class="card-body">
                                                <a href="#numObj2_<%#Eval("ID_NIVEL") %>" data-toggle="collapse" class="btn btn-default btn-objetivo" onclick="fjschangerow(this)">
                                                    <asp:Label ID="lblDescNivelComp" runat="server" Text='<%#Eval("DESC_NIVEL") %>'></asp:Label>
                                                    <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                                                </a>
                                                <asp:Label ID="lblIdNivelComp" runat="server" Text='<%#Eval("ID_NIVEL") %>' CssClass="labelObjInd"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="numObj2_<%#Eval("ID_NIVEL") %>" class="panel-collapse collapse in">
                                    <asp:Repeater ID="rprIndComp" runat="server" DataSource='<%#Eval("INDICADORES") %>'>
                                        <ItemTemplate>

                                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                                <br />
                                                <div class="rowPemir">
                                                    <div class="rowIndicador  col-lg-12" onmouseover="this.className='rowIndicadorHover col-lg-12';" onmouseout="this.className='rowIndicador col-lg-12';">
                                                        <div class="col-lg-8 reajustarCol">
                                                            <a id="linkIndComp" runat="server" href='<%#Eval("LIGA")+"&ciclo="+Request.Params["pCiclo"] +"&siglas="+Request.Params["siglas"] %>' class="LigaIndicador" target="_self" onclick="fjsDetenerEvento(event);mostrarIndicador(this);">
                                                                <div id="divLigaProp" style="width: 100%">
                                                                    <asp:Label ID="lblNomIndComp" runat="server" Text='<%#Eval("NOMBRE_IND") %>'></asp:Label>
                                                                    <asp:Label ID="lblIdIndicadorIndComp" runat="server" Text='<%#Eval("ID_INDICADOR") %>' CssClass="labelObjInd"></asp:Label>
                                                                </div>
                                                            </a>
                                                        </div>
                                                        <div class="col-lg-4 secprogocu <%#Eval("CALIFICACION_PROMEDIO") %>" style="margin-left: 80px;">
                                                            <asp:Label ID="LblSinCalificacionR" runat="server" Text='<%#Eval("CALIFICACION_PROMEDIO").ToString().Equals("divLeyenda")?"El indicador no cuenta con valoración para este año.":""%>' CssClass="LigaIndicador"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="rowPemir">
                                                    <div class="bordeInd">
                                                        <div id="divGraficaComp">
                                                            <div id="<%#Eval("ID_INDICADOR") %>" style="width: 100%; height: 220px;"></div>
                                                            <asp:DropDownList ID="ddlGraficaComp" runat="server" CssClass="select" DataSource='<%#Eval("HISTORICOS") %>'
                                                                DataTextField="GRAFICA" DataValueField="NO" Style="display: none">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="fuenteGrafica">
                                                            Fuente: Elaboración CONEVAL con base en información del Portal Aplicativo de​ la Secretaría de Hacienda y Crédito Público (PASH)
                                                        </div>
                                                        <div>
                                                            <asp:Label runat="server" ID="lblNotaDer" Visible='<%#Eval("TieneIndicadorComple") %>' CssClass="fuenteGrafica" Text="*Nota: Información proporcionada directamente por el programa​​​"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsObjetivosComp" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarObjetivos">
            <SelectParameters>
                <asp:QueryStringParameter Name="idMatriz" QueryStringField="pIdMatriz" Type="Decimal" DefaultValue="0" />
                <asp:QueryStringParameter Name="dIndicador" DefaultValue="0" />
                <asp:QueryStringParameter Name="sNivel" DefaultValue="3" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <div id="divHistoricoProgramas" style="padding: 1.5em  1em; margin-top: 120px;">
            <asp:Label ID="lblTituloHco" runat="server" Text="Ver indicadores anteriores" CssClass="tutuloResumenMir"></asp:Label>
            <br />
            <br />
            <div class="secprogocu">
                <asp:Repeater ID="RepeaterHistorico" runat="server" DataSourceID="odsHcoProgramas">
                    <ItemTemplate>
                        <a href='<%#Eval("LIGA")+"&&siglas="+Request.Params["siglas"]+"&nombre="+Request.Params["nombre"]%>' class="btn btn-primary"><%#Eval("CICLO") %></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="divBtnPlaneacion" style="display: none">
                <a id="btnCicloProg" runat="server" class="btn btn-primary" style="text-align:center;"></a>
            </div>
            <asp:ObjectDataSource ID="odsHcoProgramas" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarHistoricoPrograma">
                <SelectParameters>
                    <asp:QueryStringParameter Name="dMatriz" QueryStringField="pIdMatriz" DefaultValue="-1" />
                    <asp:QueryStringParameter Name="sPantalla" QueryStringField="t" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>

        <div id="divfooter" class="secprogocu" style="padding: 0.5em  1em; width: 100%; float: right">
            <asp:Label runat="server" ID="LblNota" Text="Nota: los círculos que aparecen junto al nombre del indicador corresponden al promedio de sus calificaciones en el cumplimiento de los criterios mínimos." Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
            <asp:Label runat="server" ID="LblNotaDown" Text="Verde oscuro-Destacado, Verde claro-Adecuado, Amarillo-Intermedio y Rojo-Oportunidad de mejora" Font-Size="X-Small" Font-Bold="true"></asp:Label><br />
            <asp:Label ID="LblTasaPer" runat="server" Font-Bold="true" Font-Size="X-Small" Text="Tasa de permanencia de los indicadores: Proporción de indicadores que han perdurado en un determinado periodo"></asp:Label>
            <br />
        </div>
    </div>

    <script type="text/javascript">
        (function () {
            fjsCargaPrograma();
            if (window.location.search.match("[?&]IsDlg=1") || window.location.search.match("[?&]view=")) {
                removeStyleDialog();
                $('.LigaIndicador').each(function () {
                    this.href += '&IsDlg=1';
                })
            }
            var progEspecial = $("#ContentPlaceHolder1_LblProgEspecial").text();
            if (progEspecial == "true") {
                $(".sitemap").remove();
                $(".secprogocu").remove();
                $(".divBtnPlaneacion").show();
                $(".reajustarCol").removeClass("col-lg-8").addClass("col-lg-12");
            }
        })();
    </script>
    <!--[if IE]>
	<style>    
        .rowIndicador{	
	        font-style: italic;
	        font-weight: bold;
	        font-size: 12px;
	        padding: 5px 10px 5px 15px;
	        color: #FFFFFF;
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#B6B6B6', endColorstr='#B6B6B6',GradientType=0 )
	        border-radius: 11px;
  	        -moz-border-radius: 11px;  
	        -webkit-border-radius: 11px;
        }
        .rowIndicadorHover{	
	        font-style: italic;
	        font-weight: bold;
	        font-size: 12px;
	        padding: 5px 10px 5px 15px;
	        color: #FFFFFF;
	        /*CCS3 Degradado*/


	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#0EA957', endColorstr='#0EA957',GradientType=0 )
	        border-radius: 11px;
  	        -moz-border-radius: 11px;
  	        -webkit-border-radius: 11px;
        }
</style>
<![endif]-->
</asp:Content>


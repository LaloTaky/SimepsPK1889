<%@ Page Title="SIMEPS - Sistema de Medición de la Política Social" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MosaicoSips.aspx.cs" Inherits="SIMEPS.Mosaico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphTituloHeader" runat="server">
    <p class="lblTitulo">
        <asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de los programas y acciones de desarrollo social</asp:Label>
    </p>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main">

        <h2 class="PlecaEncabezado" runat="server" id="TituloEncabezado">
            <span id="headerContent">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label>&#8203;&#8203;&#8203;</span>
        </h2>

        <div class="textoMosaico">
            <div>
                <h2 runat="server" id="SubtituloEncabezado" style="color: #00a94f; line-height: 18px; font-family: arial; font-size: 10pt; text-align: justify; margin-bottom: 0px !important;">
                    <span class="subtitulos">
                        <asp:Label ID="lblSubtitulo" runat="server"></asp:Label>
                    </span>
                </h2>
                <asp:Label ID="lblCuerpo1" runat="server"></asp:Label>
                <asp:Label ID="lblCuerpo1Cursiva" runat="server"></asp:Label><asp:Label ID="lblCuerpo1Continuacion" runat="server"></asp:Label>
                <span style="line-height: 15.33px; font-family: arial, sans-serif; font-size: 10pt; background-color: transparent;">
                    <asp:Label runat="server" ID="lblMIRSiglas">&nbsp;(MIR).&#8203;&#8203;</asp:Label></span><br />
            </div>
            <div>
                <span>&#8203;<br />
                </span>
            </div>

            <p class="MsoNormal">
                <span>
                    <asp:Label ID="lblCuerpo2" runat="server"></asp:Label></span>
                <br />
                <br />
                <span>
                    <asp:Label ID="lblCuerpo3" runat="server"></asp:Label></span>
            </p>


            <div id="dContenido" runat="server">

                <h2 style="color: #00a94f; line-height: 18px; font-family: arial; font-size: 10pt; text-align: justify; margin-bottom: 0px !important;">
                    <span class="subtitulos">
                        <asp:Label ID="lblPregunta" runat="server"></asp:Label></span>
                </h2>
                <br />
                <br />

                <div class="row" style="margin-top: -25px;">
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-2">
                        <img src="img/tomadoresdecisiones.jpg" class="bulletMosaico" />
                    </div>
                    <div class="col-md-6 align-middle">
                        <p>
                            <br />
                            <asp:Label ID="lblRespuesta1" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-2">
                        <img src="img/transparencia.jpg" class="bulletMosaico" />
                    </div>
                    <div class="col-md-6 align-middle">
                        <p>
                            <br />
                            <br />
                            <asp:Label ID="lblRespuesta2" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-2">
                        <img src="img/mejorapoliticas.jpg" class="bulletMosaico" />
                    </div>
                    <div class="col-md-6 align-middle">
                        <p>
                            <br />
                            <br />
                            <asp:Label ID="lblRespuesta3" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>

                <br />
                <p id="pCuerpo" runat="server">
                    <span>
                        <asp:Label ID="lblCuerpo4" runat="server"></asp:Label>
                    </span>
                    <span class="apple-converted-space">
                        <span style="color: #262626; line-height: 115%; font-size: 10pt;">&nbsp;</span>
                    </span>
                    <span>
                        <asp:Label ID="lblCuerpo5" runat="server"></asp:Label>
                    </span>
                    <span class="apple-converted-space">
                        <span style="color: #262626; line-height: 115%; font-size: 10pt;">&nbsp;</span>
                    </span>
                    <span>
                        <asp:Label ID="lblCuerpo6" runat="server"></asp:Label></span>
                </p>

                <p>
                    <span>
                        <asp:Label ID="lblCuerpo7" runat="server"></asp:Label></span>
                </p>

                <p>
                    <span>
                        <asp:Label ID="lblCuerpo8" runat="server" Class="subtitulonegritas"></asp:Label></span>
                    <span style="line-height: 1.231; background-color: transparent;">&#8203;&#8203;&#8203;&#8203;&#8203;&#8203;&#8203;&#8203;&#8203;</span>
                    <span style="line-height: 1.231; background-color: transparent;">&#8203;&#8203;&#8203;&#8203;&#8203;</span>
                </p>
            </div>
        </div>

        <div class="col-center">
            <div id="divBotones" runat="server" class="divBotones">
                <asp:HiddenField runat="server" ID="HdnRutaHistorico" />
                <asp:HiddenField runat="server" ID="HdnRutaTodos" />

                <input id="hddCicloMosaico" type="hidden" value="<%=iCiclo%>" />
                <div class="col-sm-3"></div>
                <div class="col-sm-6">
                    <div class="col-sm-6">
                        <table>
                            <tr>
                                <td>
                                    <a href="Historico.xls" target="_blank" id="xlsHcoInd">
                                        <img src="img/descarga_excel.jpg" class="img-descarga" onclick="fjsEstableceUrlReportesSIPS();" /></a>
                                </td>
                                <td rowspan="2">Descarga base histórica de indicadores
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="Historico.csv" target="_blank" id="csvHcoInd">
                                        <img src="img/descarga_csv.jpg" class="img-descarga" onclick="fjsEstableceUrlReportesSIPS();" /></a>
                                </td>
                                <td></td>
                            </tr>
                        </table>




                    </div>
                    <div class="col-sm-6">
                        <table>
                            <tr>
                                <td>
                                    <a href="TodosInd.xls" target="_blank" id="xlsTodosInd">
                                        <img src="img/descarga_excel.jpg" class="img-descarga" onclick="fjsEstableceUrlReportesSIPS();" /></a>
                                </td>
                                <td rowspan="2">Descarga base de datos de todos los indicadores
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="TodosInd.xls" target="_blank" id="csvTodosInd">
                                        <img src="img/descarga_csv.jpg" class="img-descarga" onclick="fjsEstableceUrlReportesSIPS();" /></a>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <div class="row">
                    <div class="col-xs-2"></div>
                    <div class="col-xs-9">
                        <div class="row">
                            <asp:Repeater ID="rprCiclos" runat="server" DataSourceID="odsCiclos">
                                <ItemTemplate>
                                    <div class="col-xs-2" style="padding: 5px;">
                                        <a href="MosaicoSips.aspx?pCIclo=<%#Eval("CICLO_VALUE")+valorT%>" class="btn-mosaico <%#Eval("CICLO_VALUE").ToString()==iCiclo.ToString()?" btn-mosaico-selected":"btn-mosaico-general"%>"><%#Eval("CICLO_VALUE")%></a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ObjectDataSource ID="odsCiclos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarCiclos">
                <SelectParameters>
                    <asp:QueryStringParameter Name="sPantalla" QueryStringField="t" DefaultValue="A" />
                </SelectParameters>
            </asp:ObjectDataSource>

        </div>

        <div class="col-center">
            <div id="divBotonesDic" runat="server" class="divBotones">
                <asp:HiddenField runat="server" ID="HdnRutaHistoricoAproInd" />
                <div class="col-sm-5"></div>
                <div class="col-sm-6">
                    <div class="col-sm-6">
                        <table>
                            <tr>
                                <td>
                                    <a href="Historico.xls" target="_blank" id="xlsHcoIndApro">
                                        <img src="img/descarga_excel.jpg" class="img-descarga"  onclick="fjsEstableceUrlReportesSIPS();" /></a>
                                </td>
                                <td rowspan="2" id="td-Descarga">Descarga base histórica de aprobación de indicadores
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="Historico.csv" target="_blank" id="csvHcoIndApro">
                                        <img src="img/descarga_csv.jpg" class="img-descarga" onclick="fjsEstableceUrlReportesSIPS();" /></a>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <br />

        <table class="tablaMosaico">
            <asp:Repeater ID="rprMosaicos" runat="server" DataSourceID="odsMosaicos">
                <ItemTemplate>
                    <%# Container.ItemIndex%3 == 0 ? (Container.ItemIndex == 0 ?"":"</tr><tr>"):""%>
                    <td class="tdMosaico" onmouseover="this.className='tdMosaicoHover';" onmouseout="this.className='tdMosaico';">
                        <a href='<%#Eval("LIGA")+valorT%>'>
                            <img src='<%#Eval("NOM_ARCHIVO") %>' alt="Responsive image" onerror="this.src='https://www.coneval.org.mx/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'" class="imagenMosaico">
                        </a>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <asp:ObjectDataSource ID="odsMosaicos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarMosaicos">
            <SelectParameters>
                <asp:QueryStringParameter QueryStringField="pCiclo" Name="pCiclo" />
                <asp:QueryStringParameter DefaultValue="A" QueryStringField="t" Name="pCamino" />
                <asp:Parameter Type="Boolean" DefaultValue="false" Name="pMosaicoFin" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <br />
        <div class="row">
            <a href="BuscaIndicador.aspx" class="subtitulos">
                <img src="img/busqueda.jpg" class="imageBusqueda" />&nbsp;Buscador temático</a>
        </div>

        <div class="row" style="display: none">
            <div class="col-xs-2">
                <img src="img/comparador.jpg" />
            </div>
            <div class="col-xs-6">
                <div class="row">
                    <div class="col-xs-12">
                        <span>Coparador de indicadores</span>
                        <span>Comparador de indicadores de los programas y acciones sociales y la politica social</span>
                        <span>Explora, selecciona y compara indicadores</span>
                        <span>Con este aplicativo podrás comparar el desempeño de los programas y acciones sociales y las politicas sociales</span>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script>
        (function () {
            fjsEstableceUrlReportesSIPS();
        })();
    </script>
</asp:Content>

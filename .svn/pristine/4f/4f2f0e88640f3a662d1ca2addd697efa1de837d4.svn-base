<%@ Page Title="SIMEPS - Sistema de Medición de la Política Social" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleIndicador.aspx.cs" Inherits="SIMEPS.DetalleIndicador" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphTituloHeader" runat="server">
   <p class="lblTitulo">
      <asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de la política social</asp:Label>
   </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <br />
   <div class="main">
      <div id="divRegresar" style="display: none; float: right">
         <label id="lblRegresar" class="btn btn-primary" style="font-size: 11pt;" onclick="window.history.back()">
            Regresar
         </label>
      </div>
      <br />
      <div id="divSubHead" runat="server" style="margin: 10px" onload="divHead_Load">
         <div class="row">
            <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8 col-xl-8">
               <div class="textoPrograma" style="line-height: normal !important;">
                  <asp:Label ID="LblTituloPrograma" runat="server" Text="PROGRAMA"></asp:Label>
               </div>
               <div style="color: RGB(0,112,192); margin: 40px 0px; font-size: 15px;">
                  <asp:Label ID="LblTituloObjetivo" runat="server" Text="Nombre del Objetivo" Font-Size="Medium" Font-Bold="true"></asp:Label>
               </div>

               <div style="margin-left: 70px;">
                  <asp:Label ID="LblTituloInd" runat="server" Text="Nombre del Indicador" Font-Size="Small"></asp:Label><br />
               </div>
            </div>
            <div class="row">
               <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                  <div id="divBotones" style="">
                     <%--boton--%>
                     <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                        <img src="img/descarga_datos2.jpg" class="botonimagenn" onclick='fjsDetenerEvento(event); fjsDescargaFichaIndicadores(this, true);' />
                     </div>
                     <div class="col-xs-12 col-sm-1 col-md-1 col-lg-1 col-xl-1">
                        <div style="padding-left: 5%;">
                           <p class="imaBotonn">Descargar ficha indicador</p>
                           <%--<input type="submit" id="linkDescargaFichaTecnica" onclick='fjsDetenerEvento(event); fjsDescargaFichaIndicadores(this, true);' value="Descarga Ficha del indicador" class="botonDescarga" style="float:right" />--%>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <br />
      <br />
      <br />
      <br />
      <%--botones grafica--%>
      <div class="text-center">
         <b>Tipo de Gráfica: </b>
         <img src="img/Grafica/barras.png" title="Gráfica de barras" style="width: 30px" onclick="fjsMostrarGrafica('barra')"
            class="btnOut" onmouseover="this.className='btnHover'" onmouseout="this.className='btnOut'" />
         <img src="img/Grafica/lineal.png" title="Gráfica lineal" style="width: 32px" onclick="fjsMostrarGrafica('lineal')"
            class="btnOut" onmouseover="this.className='btnHover'" onmouseout="this.className='btnOut'" />
      </div>

      <div class="centrarPadre">
         <div id="divGrafica" class="centrarGrafica" style="max-width: 580px; height: 300px; padding-left: 10px;"></div>
         <div style="text-align: center; font-size: 10px; font-family: Arial; font-style: italic">
            Fuente: Elaboración CONEVAL con base en 
									información del Portal Aplicativo de​ la 
									Secretaría de Hacienda y Crédito Público 
									(PASH)
         </div>

      </div>

      <br />
      <div class="text-center" style="font-size: 12px; font-family: Arial; font-weight: bold; display: none" id="divSinInformacion">
         No se cuenta con información del indicador 
									del programa
      </div>

      <asp:DropDownList ID="ddlGraficaInd" runat="server" CssClass="select" DataSourceID="odsGraficaIndicador" DataTextField="GRAFICA" DataValueField="NO" Style="display: none">
      </asp:DropDownList>

      <asp:ObjectDataSource ID="odsGraficaIndicador" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarHistorico">
         <SelectParameters>
            <asp:QueryStringParameter Name="dIndicador" QueryStringField="pIdIndicador" Type="Decimal" DefaultValue="0" />
         </SelectParameters>
      </asp:ObjectDataSource>
      <div class="container">
         <div class="row">
            <rsweb:ReportViewer
               ID="ReportViewer1"
               runat="server"
               Height="130px"
               Width="100%"
               ProcessingMode="Remote"
               ShowParameterPrompts="false"
               OnLoad="ReportViewer_OnLoad"
               CssClass="reporteHistorico">
            </rsweb:ReportViewer>
         </div>
      </div>
      <br />
      <table class="tablaIndicadores">
         <tr style="vertical-align: top">
            <td></td>
         </tr>
         <asp:Repeater ID="rprDetalleInd" runat="server" DataSourceID="odsDetalleInd">
            <ItemTemplate>
               <tr class="tablasinHeigth">
                  <td class="headerPrograma">
                     <asp:Label ID="lblPrograma" CssClass="caracteristicas" runat="server" Text='Características principales del indicador'></asp:Label>
                  </td>
               </tr>

               <%--<tr>
				<td class="">Objetivo</td>
                       
			</tr>
			<tr>
				<td class="">
				     <asp:Label ID="lblDescNivel" runat="server" Text='<%#Eval("DESC_NIVEL") %>'></asp:Label>
			    </td>
			</tr>--%>


               <tr>
                  <td>
                     <table class="tablaDetalleInd">
                        <tr>
                           <%--le pega al indicador--%>
                           <td class="nomIndicador" style="font-weight: bold; font-size: 20px; text-align: center; border: 2px; border-color: #FFFFFF; border-style: solid; font-family: sans-serif;">Indicador</td>
                           <td class="nomIndicador" style="font-weight: bold; font-size: 17px; text-align: center; border: 2px; border-color: #FFFFFF; border-style: solid; font-family: sans-serif;">
                              <asp:Label ID="lblNombreInd" runat="server" Text='<%#Eval("NOMBRE_IND") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">Definición
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblDefInd" runat="server" Text='<%#Eval("DEFINICION_IND") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">Método de Cálculo
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblMetCalcInd" runat="server" Text='<%#Eval("METODO_CALCULO_IND") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">Frecuencia de Medición
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblFrecMedInd" runat="server" Text='<%#Eval("FRECUENCIA_MEDICION") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">Unidad de Medida
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblUnidadMedidaInd" runat="server" Text='<%#Eval("UNIDAD_MEDIDA") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">Meta Absoluta Planeada
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblMetaAbsPlaInd" runat="server" Text='<%#Eval("META_ABS_PLANEADA") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">Meta Absoluta Alcanzada
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblMetaAbsAlcInd" runat="server" Text='<%#Eval("META_ABS_ALCANZADA") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">Meta Relativa Planeada
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblMetaRelPlaInd" runat="server" Text='<%#Eval("META_REL_PLANEADA") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">Meta Relativa Alcanzada
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblMetaRelAbsInd" runat="server" Text='<%#Eval("META_REL_ALCANZADA") %>'></asp:Label>
                           </td>
                        </tr>
                        <tr>
                           <td class="indicadorDetalleTd columnDetalleInd" style="background-color: #F0EDEE; border: 2px; border-color: #FFFFFF; border-style: solid;">Línea Base
                           </td>
                           <td class="indicadorDetalleTd" style="background-color: #C7DCE2; border: 2px; border-color: #FFFFFF; border-style: solid;">
                              <asp:Label ID="lblLineaBaseInd" runat="server" Text='<%#Eval("LINEA_BASE") %>'></asp:Label>
                           </td>
                        </tr>
                     </table>
                  </td>
               </tr>
               <tr>
                  <td>
                     <br />
                     <br />
                     <div class="row" id="divEstadisticas" runat="server">
                        <div class="col-lg-6">
                           <h5 class="t" style="color: #000000; font-weight: bold; font-size: 17px;">Calidad del Indicador</h5>
                           <br />
                           <br />
                           <div id="LeyendaAviso" class="row col-sm-12 text-center" style="display: none">
                              <asp:Label runat="server" ID="LblavisoCalidad" Text="El indicador no cuenta con valoración para este año"></asp:Label><br />
                           </div>
                           <div id="divSemaforos" class="row">
                              <div class="row col-lg-12">
                                 <div class="col-sm-3">
                                    <span>Claridad:</span>
                                 </div>
                                 <div class="col-sm-3">
                                    <div id="cajaClaridad" class="cajaVerde" data-visible='<%#Eval("Claridad") %>'></div>
                                 </div>
                                 <div class="col-sm-4 ">
                                    <span>Relevancia:</span>
                                 </div>
                                 <div class="col-sm-2">
                                    <div id="cajaRelevancia" class="cajaVerde" data-visible='<%#Eval("Relevancia") %>'></div>
                                 </div>
                              </div>
                              <br />
                              <br />
                              <br />
                              <div class="row col-sm-12">
                                 <div class="col-sm-3 ">
                                    <span>Adecuación:</span>
                                 </div>
                                 <div class="col-sm-3">
                                    <div id="cajaAdecuacion" class="cajaVerde" data-visible='<%#Eval("Adecuacion") %>'></div>
                                 </div>
                                 <div class="col-sm-4">
                                    <span>Monitoreabilidad:</span>
                                 </div>
                                 <div class="col-sm-2">
                                    <div id="cajaMonito" class="cajaVerde" data-visible='<%#Eval("Monitoreabilidad") %>'></div>
                                 </div>
                              </div>
                           </div>
                           <br />
                           <br />

                        </div>
                        <%--inicia estadisticas rapidas--%>
                        <div class="col-lg-6">
                           <div class="row">
                              <h5 class="" style="color: #000000; font-weight: bold; font-size: 17px;">Estadísticas rápidas</h5>

                              <div class="row col-md-12 metasAll">
                                 <asp:Label Text="Metas Planeadas" runat="server" Font-Bold="true" Style="font-size: 15px;" />
                              </div>
                              <br />
                              <br />
                              <div class="row">
                                 <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                                    <p class="centrarPadre">Valor <br /> máximo</p>
                                    <div class="centrar">
                                       <div class="verdePlaneadas">
                                          <asp:Label runat="server" ID="LblPlaneadaMax" CssClass="" Font-Bold="true" Text="0"></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                                 <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                                    <%--<asp:Label Text="Valor mnimo" runat="server" />--%>
                                    <p class="centrarPadre">Valor <br /> mínimo</p>
                                    <div class="centrar">
                                       <div class="verdePlaneadas">
                                          <asp:Label runat="server" ID="LblPlaneadaMin" CssClass="" Font-Bold="true" Text="0"></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                                 <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                                    <%--<asp:Label Text="Valor promedio" runat="server" />--%>
                                    <p class="centrarPadre">Valor <br /> promedio</p>
                                    <div class="centrar">
                                       <div class="verdePlaneadas">
                                          <asp:Label runat="server" ID="LblPlaneadaPro" CssClass="" Font-Bold="true" Text="0"></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                              </div>
                              <br />
                              <br />
                              <div class="row col-md-12">
                                 <asp:Label Text="Metas Alcanzadas" Font-Bold="true" Style="font-size: 15px;" runat="server" />
                              </div>
                              <br />
                              <br />
                              <div class="row">
                                 <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                                    <%-- <asp:Label Text="Valor máximo" runat="server" />--%>
                                    <p class="centrarPadre">
                                       Valor<br />
                                       máximo
                                    </p>
                                    <div class="centrar">
                                       <div class="azulAlcanzadas">
                                          <asp:Label runat="server" ID="LblAlcanzadaMax" CssClass="" Font-Bold="true" Text="0"></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                                 <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                                    <p class="centrarPadre">
                                       Valor<br />
                                       mínimo
                                    </p>
                                    <div class="centrar">
                                       <div class="azulAlcanzadas">
                                          <asp:Label runat="server" ID="LblAlcanzadaMim" CssClass="" Font-Bold="true" Text="0"></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                                 <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                                    <%--<asp:Label Text="Valor promedio" runat="server" />--%>
                                    <p class="centrarPadre">Valor <br /> promedio</p>
                                    <div class="centrar">
                                       <div class="azulAlcanzadas">
                                          <asp:Label runat="server" ID="LblAlcanzadaPro" Font-Bold="true" Text=""></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <%--termina estadisticas rapidas--%>
                     </div>
                  </td>
               </tr>
            </ItemTemplate>
         </asp:Repeater>
         <asp:ObjectDataSource ID="odsDetalleInd" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarIndicador">
            <SelectParameters>
               <asp:QueryStringParameter Name="idMatriz" QueryStringField="pIdMatriz" Type="Decimal" DefaultValue="0" />
               <asp:QueryStringParameter Name="nivel" QueryStringField="pNivel" Type="Int32" DefaultValue="0" />
               <asp:QueryStringParameter Name="idNivel" QueryStringField="pIdNivel" Type="Decimal" DefaultValue="0" />
               <asp:QueryStringParameter Name="dIndicador" QueryStringField="pIdIndicador" Type="String" DefaultValue="0" />
            </SelectParameters>
         </asp:ObjectDataSource>
      </table>
      <br /><br /><br /><br /><br />
   </div>

   <script>
      (function () {
         fjsCargaDetalleIndicador();
         fjsCargaEstadisticas();
         if (window.location.search.match("[?&]IsDlg=1") || window.location.search.match("[?&]view=")) {
            removeStyleDialog();
            document.getElementById("divRegresar").style.display = "block";
         }
      })();

   </script>
</asp:Content>

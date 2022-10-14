<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="IndicadorSectorial4T.aspx.cs" Inherits="SIMEPS.IndicadorSectorial4T" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphTituloHeader" runat="server">
   <p class="lblTitulo">
      <asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de la política social</asp:Label>
   </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <link href="<%:Page.ResolveUrl("~/Content/carouselIndSectorial.css")%>" rel="stylesheet" />

     <div class="contentText">
             <span class="lbTituloPND">Programas derivados del PND 2019-2024 </span>
         </div>

     <div class="row">
      
   <div class="row col-xs-12 ddlFloat" style="margin-top: 2%;">
      <div class="col-md-9">
         <asp:DropDownList ID="ddlSectores"
            runat="server"
            CssClass="textoMosaico form-control"
            DataSourceID="odsSectores"
            AutoPostBack="true"
            DataTextField="NOMBRE"
            DataValueField="ID_SECTOR"
            Style="background-color: #f78e33; color:#fff !important;"
            
            OnDataBound="ddlSectores_DataBound">
         </asp:DropDownList>
         <asp:ObjectDataSource ID="odsSectores" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarSectores4T">
            <SelectParameters>
               <asp:Parameter DefaultValue="-1" Name="idSector" />
            </SelectParameters>
         </asp:ObjectDataSource>
      </div>
      <div class="row col-md-3 " id="divBotonDescargaBD">
         <center>
            <div class="col-md-4">
               <asp:ImageButton ImageUrl="img/descarga_datos2.jpg" runat="server" ID="ImgBtnDescargaBD" Width="55px" CssClass="btnDescargas" OnClientClick='fjsDetenerEvento(event); fjsDescargaBaseDatosAll(this,"xls","true");'/>
            </div>
            <div class="col-md-7">
               <label id="idLblDescargaBD" class="imgBtnDescargaRep">Base de datos de los indicadores derivados del PND 2019-2024</label>
            </div>
         </center>
      </div>
   </div>
   <div class="row col-md-12"></div>
   <div class="row col-md-12">
      <br />
      <div class="col-md-12">
         <br />
         <asp:Repeater ID="rpmSectores" runat="server" DataSourceID="odsDetalleSector">
            <ItemTemplate>
               <div class="col-xs-3" style="height: 60px">
                  <center><asp:Label runat="server" id="lblConteo" class="numCountSec"> <%#Eval("CONTEO") %></asp:Label></center>
                  <center><asp:Label runat="server" id="labTipo" class="desCountSec" ><%#Eval("TIPO") %> </asp:Label></center>
               </div>
            </ItemTemplate>
         </asp:Repeater>
         <!--ObjetDataSource para el contador de estadistica * idSector-->
         <asp:ObjectDataSource ID="odsDetalleSector" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultaEstadisticaBasica4T">
            <SelectParameters>
               <asp:ControlParameter ControlID="ddlSectores" DefaultValue="0" Name="nSector" />
            </SelectParameters>
         </asp:ObjectDataSource>
      </div>
   </div>
   <div class="row">
      <div class="col-xs-12">
        <div class="contenCarousel">
            <div class='carousel slide <%=(Request.Browser.Browser.Contains("InternetExplorer") == true) ? "carousel-IE" : "carousel-simeps"%>'  data-ride="carousel" data-type="multi" data-interval="false" id="myCarouselInd">
               <div class='carousel-inner <%=(Request.Browser.Browser.Contains("InternetExplorer") == true) ? "carousel-IE" : "carousel-simeps"%>'  style="width: 100%;">
                  <asp:Repeater ID="rpmProgramas" runat="server" DataSourceID="odsProgramas" OnItemCreated="rpmProgramas_ItemCreated">
                     <ItemTemplate>
                        <div class='item <%# (Container.ItemIndex ==0 ? "active" : "") %>'>
                           <div class="col-xs-2">
                              <asp:ImageButton ImageUrl='<%#Eval("URL_ICONO")%>' IDPROGSEC='<%#Eval("ID_PROG_SECTORIAL")%>'  NOMBRE='<%#Eval("NOMBRE")%>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID_PROG_SECTORIAL")%>' OnCommand="imbProgramaurl_Command" CommandName="PROGRAMAS" runat="server" ID="imbProgramaurl" />
                           </div>
                        </div>
                     </ItemTemplate>
                  </asp:Repeater>
               </div>
               <a class="left carousel-control carousel-simepsC " href="#myCarouselInd" data-slide="prev" style="background-color: #8080804d; width: 15px; align-content: center;"><i class="glyphicon glyphicon-chevron-left"></i></a>
               <a class="right carousel-control carousel-simepsC" href="#myCarouselInd" data-slide="next" style="background-color: #8080804d; width: 15px; align-content: center;"><i class="glyphicon glyphicon-chevron-right"></i></a>
            </div>
            <asp:ObjectDataSource ID="odsProgramas" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarProgramaSectoriales4T">
               <SelectParameters>
                  <asp:ControlParameter ControlID="ddlSectores" DefaultValue="0" Name="idProgramaSectorial"/>
               </SelectParameters>
            </asp:ObjectDataSource>
         </div>
      </div>
   </div>
   <br />
   <div  ID="divIndicadorPro" runat="server" style="width:97%; margin:auto">
     <div>
        <asp:Label runat="server" ID="lblNombrePrograma" CssClass="lblNombre-ProgramaSec"></asp:Label><br />
   </div>
   <div class="row">
      <div class="col-xs-12">
         <div class="col-xs-4">
            <div id="btnOptions" style="float: right; margin-right: -10px;">
               <a id="atitulo" onclick="MostrarOcultar();" href="#" class="btn btn-default btn-objetivo" title="Colapsar sección a la izquierda">
                  <span id="btOcultar" class="glyphicon glyphicon-chevron-left"></span>
               </a>
            </div>
         </div>
         <div class="col-xs-8"></div>
      </div>
   </div>
   <div id="idGralDetalleInd"   class="row equal" runat="server">
      <div id="idDivListObj" class="col-md-4 indSecBackGrounds">
         <div class="row">
           
             <div class="col-xs-12">
               <div class="col-xs-4">
                  <div class="contenCounter">
                     <div class="lblObjetivosS">
                        <asp:Label runat="server" ID="lbObjetivosSecL" class="lbObjetivosSecL"></asp:Label>
                     </div>
                     <label class="lbcontador-objetivos">Objetivos prioritarios</label>
                  </div>
               </div>
               <div class="col-xs-4">
                  <div class="contenCounter">
                     <div class="lblIndicadoresS">
                        <asp:Label runat="server" ID="lbMetasbienestar" class="lbIndicadoresSecL"></asp:Label>
                     </div>
                     <label style="margin:auto" class="lbcontador-indicadores">Metas para el bienestar (M)</label>
                  </div>
               </div>
                <div class="col-xs-4">
                  <div class="contenCounter">
                     <div class="lblIndicadoresS">
                        <asp:Label runat="server" ID="lbParametros" class="lbIndicadoresSecL"></asp:Label>
                     </div>
                     <label style="margin:auto" class="lbcontador-indicadores">Parámetros (P)</label>
                  </div>
               </div>
            </div>
            <div class="col-md-12" style="margin-top: 5%;"></div>
         
         </div>
         <div class="row">
            <div class="col-md-12">
               <div class="panel-group" id="accordion1">
                  <div class="panel panel-default">
                     <div class="panel-heading panel-heading-ico panel-heading-noneLink BorderPanels" style="padding-bottom: 0px; padding-top: 0px;">
                        <div class="row" style="background-color: #013360;">
                           <div class="col-md-1">
                           </div>
                           <div class="col-md-11" style="padding: 0px; border-left: solid; border-left-color: white;">
                              <h4 class="panel-title"><a class="collapsed" data-toggle="collapse" data-parent="#accordion1" href="#collapseTwo" style="padding: 20px;"><strong>Indicadores derivados del Plan Nacional de Desarrollo</strong></a></h4>
                           </div>
                        </div>
                     </div>
                     <div id="collapseTwo" class="panel-collapse collapse">
                        <div class="panel-body-white">
                           <asp:Repeater ID="rprObjetivosL" runat="server" DataSourceID="odsObjetivosSecL" OnItemCreated="rprObjetivosL_ItemCreated" OnItemDataBound="rprObjetivosL_ItemCreated">
                              <ItemTemplate>
                                 <asp:TextBox runat="server" ID="txtObjetivo" Style="display: none" Text='<%# DataBinder.Eval(Container.DataItem, "OBJETIVO")%>'></asp:TextBox>
                                 <asp:TextBox runat="server" ID="txtNumObjetivo" Style="display: none" Text='<%# DataBinder.Eval(Container.DataItem, "NUM_OBJETIVO")%>'></asp:TextBox>
                                 <asp:TextBox runat="server" ID="txtIdProgSec" Style="display: none" Text='<%# DataBinder.Eval(Container.DataItem, "ID_PROGRAMA_SEC")%>'></asp:TextBox>
                                 <div class="panel-group" id="accordion2">
                                    <div class="panel panel-default">
                                       <div class="panel-heading panel-heading-ico panel-heading-noneLink BorderPanels" style="padding-bottom: 0px; padding-top: 0px;">
                                          <div id="IndPnd" runat="server" class="row">
                                             <div class="col-md-1" style="text-align: center; padding: 12px; color: #ffffff; font-weight:bold;">
                                                <strong>
                                                   <asp:Label runat="server" ID="lblConsecutivo">
                                                                                  <%#Eval("NUM_OBJETIVO")%>
                                                   </asp:Label>
                                                </strong>
                                             </div>
                                             <div class="col-md-11" style="padding: 0px; border-left: solid; border-left-color: white;">
                                                <h4 class="panel-title">
                                                   <div>
                                                      <a class="collapsed" data-toggle="collapse" data-parent="#accordion2" href="#<%#Eval("NUM_OBJETIVO")%>" style="padding: 12px; font-weight:bold; color:#ffffff !important;"><%#Eval("OBJETIVO")%></a>
                                                   </div>
                                                </h4>
                                             </div>
                                          </div>
                                       </div>
                                       <div id="<%#Eval("NUM_OBJETIVO")%>" class="panel-collapse collapse">
                                          <asp:Repeater ID="rprIndicadoresSectoriales" runat="server" DataSourceID="odsIndicadores" OnItemCreated="rprIndicadoresSectoriales_ItemCreated">
                                             <ItemTemplate>
                                                <div class="panel-body BorderPanels" style="margin-bottom: 10px; margin-top: 10px; padding-bottom: 0px; padding-top: 0px;" runat="server">
                                                   <div class="row">
                                                      <div class="col-md-1" style="text-align: center; padding: 12px;" runat="server"> 
                                                          <strong>
                                                   <asp:Label runat="server" ID="lblConsecutivoA">
                                                                                  <%#Eval("TIPO_INDICADOR")%>
                                                   </asp:Label>
                                                </strong>
                                                      </div>
                                                      <div class="col-md-11" style="padding: 15px; border-left: groove; border-left-color: white;">
                                                         <asp:LinkButton runat="server" ID="lbtIndicadorSec" Text='<%# DataBinder.Eval(Container.DataItem, "INDICADOR")%>' OnCommand="lbtIndicadorSec_Command" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID_INDICADOR")%>' CommandName="INDICADORES"></asp:LinkButton>
                                                      </div>
                                                   </div>
                                                </div>
                                             </ItemTemplate>
                                          </asp:Repeater>
                                          <asp:ObjectDataSource ID="odsIndicadores" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarIndicadoresSectoriales4T">
                                             <SelectParameters>
                                                <asp:ControlParameter ControlID="txtIdProgSec" Name="idProgramaSectorial" PropertyName="Text" />
                                                <asp:Parameter Name="opcion" DefaultValue="2" />
                                                <asp:ControlParameter ControlID="txtObjetivo" Name="descObjetivo" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="txtNumObjetivo" Name="numObjetivo" PropertyName="Text" />
                                             </SelectParameters>
                                          </asp:ObjectDataSource>
                                       </div>
                                    </div>
                                 </div>
                              </ItemTemplate>
                           </asp:Repeater>
                           <asp:ObjectDataSource ID="odsObjetivosSecL" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarObjetivosSectoriales4T">
                              <SelectParameters>
                                 <asp:Parameter Name="idProgramaSectorial" DefaultValue="-1" />
                                  
                              </SelectParameters>
                           </asp:ObjectDataSource>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
         </div>
         <%--***************************************AQUI DEBEN IR LOS INDICADORES DE FIN****************************** --%>
      </div>
      <div id="IndicadoresdeFin" class="col-md-8 detIndSecBackGrounds">

         <div class="row">
            <div class="col-xs-12" onload="Page_Load">
               <input id="inputIdProgSec" runat="server" hidden="" />
               <input id="inputIdIndicadorSec" runat="server" hidden="" />
               <%--Botones de descarga--%>
               <div id="divBotones" class="row">
                  <div class="row col-md-6">
                     <div class="col-md-3 ">
                        <asp:ImageButton ImageUrl="img/descarga_datos3.jpg" runat="server" ID="imgbFTIndicadores" Width="55px" CssClass="btnDescargas" OnClientClick="fjsDetenerEvento(event),fjsDescargaFTIndicadores(this);" />
                     </div>
                     <div class="col-md-3 ">
                        <label id="idlblfichaTecPlur" class="imgBtnDescargaRep">Fichas técnicas de los indicadores</label>
                     </div>
                     <div class="col-md-3 ">
                        <asp:ImageButton ImageUrl="img/descarga_docdatos.jpg" runat="server" ID="imgbFTIndicadorSec" Width="55px" CssClass="btnDescargas" OnClientClick="fjsDetenerEvento(event),fjsDescargaFTIndicador(this);" />
                     </div>
                     <div class="col-md-3 ">
                        <label id="idlblfichaTecSing" class="imgBtnDescargaRep">Ficha técnica del indicador</label>
                     </div>
                  </div>
                  <div class="row col-md-6">
                     <div class="col-md-3 ">
                        <asp:ImageButton ImageUrl="img/descarga_excel.jpg" runat="server" CssClass="imgbBDExcel" ID="ImageButton2" Width="55px" OnClientClick="fjsDetenerEvento(event),fjsDescargaBaseDatos(this, 'xls');" />
                     </div>
                     <div class="col-md-3 ">
                        <label class="imgBtnDescargaRep">Base de datos de excel</label>
                     </div>
                     <div class="col-md-3 ">
                        <asp:ImageButton ImageUrl="img/descarga_csv.jpg" CssClass="btnDescargas" runat="server" Width="55px" OnClientClick="fjsDetenerEvento(event),fjsDescargaBaseDatos(this, 'csv')" />
                     </div>
                     <div class="col-md-3 ">
                        <label class="imgBtnDescargaRep">Base de datos csv</label>
                     </div>
                  </div>
               </div>
               <%--Botones de descarga--%>
               <div class="row">
                  <asp:Repeater ID="DetalleIndicador" runat="server" DataSourceID="odsDetalleIndicador" OnItemDataBound="DetalleIndicador_ItemDataBound">
                     <ItemTemplate>
                        <div class="row">
                           <div class="col-xs-12">
                              <label class="headerdetalleIndicador">Nombre del Indicador: <%#Eval("NOMBRE")%></label>
                              <br />
                              <br />
                              <p style="font-weight: bold; font-size: 20px">Información general del indicador</p>
                           </div>
                        </div>

                         <div class="grafDetInd col-lg-10">
                           <div class="row">
                              <div id="divGrafIndicadorSectorial" style="width: 100%; height: 400px; margin: 5px -6px 20px;"></div>
                              <div id="DivLPU" runat="server">
                                 <div class="col-md-10">
                                    <p style="font-size: 11px">*El valor de la línea base fue proporcionado por la dependencia</p>
                                     
                                 </div>
                              </div>
                           </div>
                        </div>

                        <div class="col-md-10">
                           <br>
                           

                           <p style="color: dimgray" class="borderParrafo1">Objetivo Prioritario: <span class="textoIndicadores"><%#Eval("OBJETIVO")%></span></p>

                           <p style="color: deepskyblue" class="borderParrafo2">Definición: <span class="textoIndicadores"><%#Eval("NOMBRE")%></span></p>

                           <p style="color: darkblue" class="borderParrafo3">Descripción: <span class="textoIndicadores"><%#Eval("DESCRIPCION")%></span></p>

                           <p style="color: dodgerblue" class="borderParrafo4">Método de cálculo: <span class="textoIndicadores"><%#Eval("METODO")%></span></p>

                           <p style="color: blue; -ms-word-wrap: break-word;" class="borderParrafo5">Fuentes de Información: <span class="textoIndicadores"><%#Eval("FUENTE")%></span></p>
         
                            <div  id="datos" style="display:none">
                               
                            <p style="color: dodgerblue" class="borderParrafo4">Frecuencia de Medición: <span class="textoIndicadores"><%#Eval("PERIODICIDAD")%></span></p>
                            <p style="color: dodgerblue" class="borderParrafo4">Tipo: <span class="textoIndicadores"><%#Eval("TIPO_INDICADOR_GRAFICA")%></span></p>
                            <p style="color: dodgerblue" class="borderParrafo4">Unidad de Medida: <span class="textoIndicadores"><%#Eval("UDM")%></span></p>
                            <p style="color: dodgerblue" class="borderParrafo4">Tendencia Esperada: <span class="textoIndicadores"><%#Eval("TENDENCIA")%></span></p>
                            <p style="color: dodgerblue" class="borderParrafo4">Nivel de desagregación: <span class="textoIndicadores"><%#Eval("NIVEL_DESAGREGACION")%></span></p>
                                    
                            </div>

                             <a href="#" id="datosvermas" class="btn btn-outline-light">Ver más</a>

                           <div>
                              <p style="color: #39508A">Periodicidad: <span class="textoIndicadores"><%#Eval("PERIODICIDAD")%></span></p>
                           </div>
                           <div>
                              <p style="color: #39508A">Unidad de medida: <span class="textoIndicadores"><%#Eval("UDM")%></span></p>
                           </div>
                 

                       
                        </div>

                        

                        <div id="divNotaGraf" runat="server" visible="false" class="col-md-6">
                           <p style="font-size: 11px; text-align: justify">
                              Nota: Al inicio de la presente administración, la Secretaría de Educación Pública (SEP) disponía de dos instrumentos para medir el logro académico de los alumnos: la Evaluación Nacional del Logro Académico en Centros Escolares (ENLACE, de carácter censal) y los Exámenes de la Calidad y el Logro Educativos (EXCALE, de carácter muestral). Los resultados de ENLACE se consideraron para definir un indicador en el Plan Nacional de Desarrollo 2013-2018 (PND), y los de EXCALE para el Programa Sectorial de Educación 2013-2018 (PSE).<br />
                              <br />
                              Durante 2013 y 2014, un grupo de especialistas de diferentes instituciones llevó a cabo un análisis de ENLACE y EXCALE a fin de determinar sus fortalezas y limitaciones y, derivado de sus recomendaciones, en 2014 la SEP resolvió suspender la aplicación de ambas pruebas, que se venían realizando anualmente desde 2006 y 2005, respectivamente. En sustitución se implementó el Plan Nacional para la Evaluación de los Aprendizajes (PLANEA), desarrollado por el Instituto Nacional para la Evaluación de la Educación (INEE), que empezó a aplicarse en 2015.
                              <br />
                              <br />
                              Las evaluaciones de PLANEA no son equivalentes a ENLACE y EXCALE, razón por la cual los indicadores del PND y del PSE asociados a estas pruebas dejaron de reportarse desde 2014, dado que no es posible utilizar para ello los resultados de las nuevas evaluaciones.
                           </p>
                        </div>

                        <%--inicia estadisticas rapidas--%>
                        <div class="row col-lg-12">
                           <h5 class="" style="color: #000000; font-weight: bold; font-size: 20px;">Estadísticas rápidas</h5>
                           <div class="row">
                              <div class="row col-md-12 metasAll">
                                 <asp:Label Font-Bold="true" Text="Metas Planeadas" runat="server" Style="font-size: 15px;" />
                              </div>
                              <br/>
                              <br />
                              <div class="row">
                                 <div class="col-sm-4">
                                    <p class="centrarPadre">Valor <br/> máximo</p>
                                    <div class="centrar">
                                       <div class="verdePlaneadas">
                                          <asp:Label runat="server" ID="LblPlaneadaMax" CssClass="" Font-Bold="true"><%#Eval("MAX_META_PLANEADA")%></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                                 <div class="col-sm-4">
                                    <p class="centrarPadre">Valor <br/> mínimo</p>
                                    <div class="centrar">
                                       <div class="verdePlaneadas">
                                          <asp:Label runat="server" ID="LblPlaneadaMin" CssClass="" Font-Bold="true"><%#Eval("MIN_META_PLANEADA")%></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                                 <div class="col-sm-4">
                                    <p class="centrarPadre">Valor <br /> promedio</p>
                                    <div class="centrar">
                                       <div class="verdePlaneadas">
                                          <asp:Label runat="server" ID="LblPlaneadaPro" CssClass="" Font-Bold="true"><%#Eval("AVG_META_PLANEADA")%></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                              </div>
                              <br />
                              <br />
                              <div class="row col-md-12 metasAll">
                                 <asp:Label Font-Bold="true" Text="Metas Alcanzadas" runat="server" Style="font-size: 15px;" />
                              </div>
                              <br />
                              <br />
                              <div class="row">
                                 <div class="col-sm-4">
                                    <p class="centrarPadre">Valor <br /> máximo</p>
                                    <div class="centrar">
                                       <div class="azulAlcanzadas">
                                          <asp:Label runat="server" ID="LblAlcanzadaMax" CssClass="" Font-Bold="true"><%#Eval("MAX_META_ALCANZADA")%></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                                 <div class="col-sm-4">
                                    <p class="centrarPadre">Valor<br />mínimo</p>
                                    <div class="centrar">
                                       <div class="azulAlcanzadas">
                                          <asp:Label runat="server" ID="LblAlcanzadaMim" CssClass="" Font-Bold="true"><%#Eval("MIN_META_ALCANZADA")%></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                                 <div class="col-sm-4">
                                    <p class="centrarPadre">Valor <br /> promedio</p>
                                    <div class="centrar">
                                       <div class="azulAlcanzadas">
                                          <asp:Label runat="server" ID="LblAlcanzadaPro" Font-Bold="true"><%#Eval("AVG_META_ALCANZADA")%></asp:Label><br />
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <%--termina estadisticas rapidas--%>

                        <%--Desempeño del indicador--%>
                        <div class="row col-md-12">
                           <br />
                           <br />
                           <p style="font-size: 20px; font-weight: bold; text-align: justify">Desempeño del indicador</p>
                           <div class="col-md-12">
                              <br />
                              <center>
                                 <div class="col-md-12 " >
                                    <div class="col-md-3 " >
                                       <div class="counterDesempenoInd" style="background-color:<%#Eval("LB_COLOR")%>"><%#Eval("VALOR_LB")%></div>
                                       <div>
                                             <label style="text-align:center">Línea Base</label>
                                       </div>
                                    </div>
                                    <div class="col-md-3 " >
                                       <div class="counterDesempenoInd" style="background-color: <%#Eval("MALCANZADA_COLOR")%>"><%#Eval("META_ALCANZADA")%></div>
                                       <div>
                                             <label style="text-align:center">Último valor alcanzado</label>
                                       </div>
                                    </div>
                                    <div class="col-md-3 " >
                                       <div class="counterDesempenoInd" style="background-color: <%#Eval("META_COLOR")%>"><%#Eval("META")%></div>
                                       <div>
                                             <label style="text-align:center">Meta 2024</label>
                                       </div>
                                    </div>
                                    <div class="col-md-3 " >
                                       <div id="PorcentajeAvance" class="counterDesempenoInd" style="color:<%# Eval("PORCENTAJE_COLOR").Equals("#FFFF00") ?"#000":"#fff"%>; background-color: <%#Eval("PORCENTAJE_COLOR")%>"><%#Eval("PORCENTAJE_AVANCE")%></div>
                                       <div>
                                             <label style="text-align:center">Porcentaje de avance con respecto al 2024 </label>
                                       </div>
                                    </div>
                                 </div>
                              </center>
                              <br />
                           </div>
                        </div>
                        <br />
                        <div class="row col-lg-12">
                           <div class="row col-md-6">
                              <%--Calidad del indicador--%>
                              <p style="font-size: 20px; font-weight: bold; text-align: justify">Calidad del indicador</p>
                              <div class="imgCalidad">
                                 <asp:Image runat="server" ImageUrl='<%# Eval("CLARIDAD") !=null ?((bool)Eval("CLARIDAD") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"):"img/iconoindicador_02.jpg" %>' />
                                 <label>Claridad</label>
                              </div>
                              <div class="imgCalidad">
                                 <asp:Image runat="server" ImageUrl='<%# (bool)Eval("RELEVANCIA") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                 <label>Relevancia</label>
                              </div>
                              <div class="imgCalidad">
                                 <asp:Image runat="server" ImageUrl='<%# (bool)Eval("MONITOREABILIDAD") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                 <label>Monitoreabilidad</label>
                              </div>
                              <div class="imgCalidad">
                                 <asp:Image runat="server" ImageUrl='<%# (bool)Eval("PERTINENCIA") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                 <label>Pertinencia</label>
                              </div>
                              <br />
                           </div>
                           <div class="row col-md-6">
                               <%-- Seccion Comentario --%>
                                <div runat="server" visible='<%# Eval("COMENTARIO") != null && Eval("COMENTARIO") != "" ? true : false  %>'>
                                    
                               <p style="font-size: 20px; font-weight: bold;">Comentario para la dependencia responsable</p> 
                                    <p>
                                        <%#Eval("COMENTARIO")%>
                                    </p>
                                </div>
                              <%--Derehcho social vinculado--%>
                              <p style="font-size: 20px; font-weight: bold;">Derecho social vinculado</p>
                              <asp:Repeater ID="rprDerechoIndCon" runat="server" DataSourceID="odsDerechosInd">
                                 <ItemTemplate>
                                    <div class="col-md-3">
                                       <center>
                                                <img src="img/derecho_<%#Eval("DER_DESCRIPCION")%>.jpg" style="width:60px;" />
                                                <div>
                                                    <label><%#Eval("DER_DESCRIPCION")%></label>
                                                </div>
                                             </center>
                                    </div>
                                 </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="defaultItemRep" runat="server" Visible='<%#((Repeater)Container.NamingContainer).Items.Count == 0 %>' Text="El indicador no se encuentra vinculado a algún derecho social." />
                                 </FooterTemplate>
                              </asp:Repeater>
                           </div>
                        </div>
                        <%--Programas Vinculados--%>
                        <div class="row col-xs-12 ">
                           <h2 class="TitulosProgVinculados ">
                             <p class="tituloProgramas">Programas y acciones sociales vinculados</p>
                           </h2>
                           <p class="textoIndicadores leyenda text-justify">A continuación se presentan los programas y acciones sociales cuyo nivel de Fin se encuentra asociado al presente indicador</p>
                           <table class="tblProgramasSectoriales">
                              <asp:Repeater ID="rprProgramasVinculados" runat="server" DataSourceID="odsProgramaSectorialVinculado">
                                 <ItemTemplate>
                                    <tr>
                                       <td class="tdProgramasSectorialesPP">
                                          <strong style="color: white">
                                             <%#Eval("PP")%>
                                          </strong>
                                       </td>
                                       <td class="tdProgramasSectoriales">
                                          <strong class="strgProgramasSectoriales">
                                             <a href="<%#Eval("LIGA")%>" style="color: white; cursor: pointer;"><%#Eval("NOMBRE")%></a>
                                          </strong>
                                       </td>
                                    </tr>
                                 </ItemTemplate>
                              </asp:Repeater>
                           </table>
                        </div>
                     </ItemTemplate>
                  </asp:Repeater>
                  <asp:ObjectDataSource ID="odsDetalleIndicador" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarDetalleIndicador4T">
                     <SelectParameters>
                        <asp:Parameter Name="idIndicador" DefaultValue="-1" />
                        <asp:Parameter Name="opcion" DefaultValue="1" />
                     </SelectParameters>
                  </asp:ObjectDataSource>
                  <asp:ObjectDataSource ID="odsProgramaSectorialVinculado" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConcultarProgramaIndicador">
                     <SelectParameters>
                        <asp:Parameter Name="idIndicador" DefaultValue="-1" />
                     </SelectParameters>
                  </asp:ObjectDataSource>
                  <asp:DropDownList ID="ddlHistoricoMetas" runat="server" CssClass="select" DataSourceID="odsGraficaHistoricoMetas" DataTextField="METASHISTORICO" DataValueField="CICLO" Style="display: none">
                  </asp:DropDownList>
                  <asp:ObjectDataSource ID="odsGraficaHistoricoMetas" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarMetasIndicador4T">
                     <SelectParameters>
                        <asp:Parameter Name="idIndicador" DefaultValue="-1" />
                        <asp:Parameter Name="opcion" DefaultValue="2" />
                     </SelectParameters>
                  </asp:ObjectDataSource>
                  <asp:ObjectDataSource ID="odsDerechosInd" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultaDerechoSocialInd">
                     <SelectParameters>
                        <asp:Parameter Name="idIndicador" DefaultValue="-1" />
                     </SelectParameters>
                  </asp:ObjectDataSource>
               </div>
            </div>
         </div>
      </div>
   </div>
</div>

         <script> 
             $(document).ready(function () {
                 $('#datosvermas').click(
                     // Primer click
                     function (e) {
                         if ($('#datosvermas').text() == 'Ver más') {
                             $('#datosvermas').text('Ver menos');
                             $('#datos').css({ 'display': 'block' });
                         }
                         else {
                             $('#datosvermas').text('Ver más');
                             $('#datos').css({ 'display': 'none' });
                         }
                         e.preventDefault();
                     });

             });
         </script>

   <script> 
       $(document).ready(function () {
           fjsCarousel();
     });

   </script>
</asp:Content>

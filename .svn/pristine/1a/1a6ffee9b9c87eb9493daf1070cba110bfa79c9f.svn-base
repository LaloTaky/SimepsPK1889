<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Programas.aspx.cs" Inherits="SIMEPS.Programas" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTituloHeader" runat="server">
   <p class="lblTitulo">
      <asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de los programas y acciones de desarrollo social</asp:Label>
   </p>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="margin-top:-30px;">
   <div class="main">
      <br />
      <h3>
         <asp:Label ID="LblDependencia" runat="server" Text="DEPENDENCIA"></asp:Label>
         <asp:Label ID="LblCiclo" runat="server" Text="CICLO"></asp:Label></h3>
      <h4><b>Principales características de la dependencia</b></h4>
      <div class="contadores">
         <div class="row">
            <div class="col-6 col-sm-6 col-md-2">
               <div class="titulocontador col-md-12">
                  <p>Programas</p>
               </div>
               <div class="numeroProgramas">
                  <p>
                     <asp:Label runat="server" ID="LblNumeroProgramas" Text="0"></asp:Label>
                  </p>
               </div>
            </div>
            <div class="col-6 col-sm-6 col-md-2">
               <div class="titulocontador col-md-12">
                  <p>Indicadores</p>
               </div>
               <div class="numeroIndicadores">
                  <p>
                     <asp:Label runat="server" ID="LblNumeroIndicadores" Text="0"></asp:Label>
                  </p>
               </div>
            </div>
            <div class="col-6 col-sm-6 col-md-4">
               <div class="titulocontador col-md-12">
                  <p>Programas Sociales con Indicadores Aprobados</p>
               </div>
               <div class="numeroAprobados">
                  <p>
                     <asp:Label runat="server" ID="LblNumeroAprobados" Text="0"></asp:Label>
                  </p>
               </div>
            </div>
            <div class="col-6 col-sm-6 col-md-4">
               <div class="titulocontador col-md-12">
                  <p>Tasa de permanencia de los Indicadores</p>
               </div>
               <div class="numeroTasaPermanencia">
                  <p>
                     <asp:Label runat="server" ID="LblPromedioTasaPermamencia" Text="0"></asp:Label>
                  </p>
               </div>
            </div>
         </div>
      </div>
      <br />
      <br />
      <div class="container">
         <div class="row">
            <div class="col-md-7 col-center sinpadding">
               <div class="panel-group tablaprogramas" id="accordion1">
                  <div class="panel panel-default">
                     <div class="panel-heading panel-heading-ico panel-heading-noneLink BorderPanels" style="padding-bottom: 0px; padding-top: 0px;">
                        <div class="row" style="background-color: #808080;">
                           <div style="padding: 0px;">
                             <div> <h4 class="panel-title"><a class="" data-toggle="collapse" data-parent="#accordion1" href="#collapseTwo" style="padding: 0px;"></a></h4></div>
                                 <div>
                                    <div id="headerTC" style="width:600px;padding-left:20px" runat="server" onload="Page_Load" class="row"></div>
                                </div>
                           </div>
                            
                        </div>
                     </div>
                     <div style="padding: 0px" id="collapseTwo" class="panel-collapse tablaprogramas collapse in">
                        <div id="divAB" runat="server" onload="Page_Load">
                           <asp:GridView ID="gvAB" runat="server" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="false" DataSourceID="odsProgramaAB" AllowPaging="true" PageSize="30" CssClass="tabla" OnDataBound="gvAB_DataBound">
                              <Columns>
                                 <asp:BoundField DataField="PP" HeaderText="PP" ItemStyle-Width="10%" />
                                 <asp:TemplateField ItemStyle-Width="90%" HeaderText="Nombre del Programa">
                                    <ItemTemplate>
                                       <a id="a2" runat="server" href='<%#Eval("LIGA")+"&t="+Request.Params["t"] %>' class="LigaPrograma">

                                          <asp:Label ID="Label21" runat="server" Text='<%#Eval("NOMBRE") %>'></asp:Label>
                                       </a>
                                       <asp:HiddenField runat="server" ID="HdDependencia" Value='<%#Eval("DEPENDENCIA")%>' />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                              </Columns>
                              <FooterStyle Font-Size="Medium" Font-Bold="true" />
                              <RowStyle CssClass="tablaRenglon" />
                              <PagerStyle CssClass="PaginacionProgramas"/>
                           </asp:GridView>


                           <asp:ObjectDataSource ID="odsProgramaAB" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarPrograma" OnSelected="odsProgramaAB_Selected">
                              <SelectParameters>
                                 <asp:QueryStringParameter Name="Anio" QueryStringField="ciclo" Type="Decimal" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="Ramo" QueryStringField="ramo" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="Unidad" QueryStringField="unidad" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="palabraClave" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="dMatriz" QueryStringField="dMatriz" DefaultValue="-1" />
                                 <asp:QueryStringParameter Name="dIndicador" QueryStringField="sIndicador" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="sNivel" QueryStringField="sNivel" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="sPantalla" QueryStringField="t" />
                                 <asp:Parameter Name="universoPaemir" DefaultValue="1" />
                                 <asp:Parameter Name="idIndicadorSectorial" DefaultValue="-1" />
                              </SelectParameters>
                           </asp:ObjectDataSource>
                        </div>

                        <div id="divProgramas" runat="server" onload="Page_Load">
                           <asp:GridView ID="grvProgramaC" runat="server" AutoGenerateColumns="false" ShowHeader="false" DataSourceID="odsProgramasC" AllowPaging="true" PageSize="30" CssClass="tabla" OnDataBound="grvProgramaC_DataBound">
                              <Columns>
                                 <asp:BoundField DataField="PP" HeaderText="PP" ItemStyle-Width="70px" />
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Justify" ItemStyle-Width="255px" HeaderText="Nombre del Programa">
                                    <ItemTemplate>
                                       <a id="a2" runat="server" href='<%#Eval("LIGA") %>' class="LigaPrograma">
                                          <asp:Label  ID="Label2"  runat="server" Text='<%#Eval("NOMBRE") %>'></asp:Label>
                                       </a>
                                       <asp:HiddenField runat="server" ID="HdDependencia" Value='<%#Eval("DEPENDENCIA")%>' />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="85px" HeaderText="Proceso Aprobación">
                                    <ItemTemplate>
                                       <asp:Label  ID="Label3" runat="server" Text='<%#Eval("PROCESO_APROBACION") %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80px" HeaderText="Año del Dictamen">
                                    <ItemTemplate>
                                       <asp:Label ID="Label3_1" runat="server" Text='<%#Eval("CICLO_APROBACION") %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ItemStyle-Width="110px" HeaderText="Dictamen de Aprobación">
                                    <ItemTemplate>
                                       <a id="zip" runat="server" visible='<%#Convert.ToInt16(Eval("ID_NIVEL_APROBACION")) == 3%>' href='<%#Eval("LIGA") %>'
                                          onclick='<%#"fjsDetenerEvento(event);fjsValidarDescargaZip(this,\""+Eval("CLAVE")+"\",\""+Eval("MODALIDAD")+"\",\""+Eval("ID_NIVEL_APROBACION")+"\",\""+ConfigurationManager.AppSettings["urlZip"]+"\" );"%>' class="LigaPrograma">
                                          <asp:Label ID="Label3_2" runat="server" Text='<%#Eval("DESC_APROBACION_DICTAMEN") %>'></asp:Label>
                                       </a>
                                       <asp:Label ID="Label1" runat="server" Visible='<%#Convert.ToInt16(Eval("ID_NIVEL_APROBACION")) != 3%>' Text='<%#Eval("DESC_APROBACION_DICTAMEN") %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>

                              </Columns>
                              <RowStyle CssClass="tablaRenglon" />
                              <PagerStyle CssClass="PaginacionProgramas" />
                           </asp:GridView>
                           <asp:ObjectDataSource ID="odsProgramasC" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarPrograma" OnSelected="odsProgramasC_Selected">
                              <SelectParameters>
                                 <asp:QueryStringParameter Name="Anio" QueryStringField="ciclo" Type="Decimal" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="Ramo" QueryStringField="ramo" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="Unidad" QueryStringField="unidad" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="palabraClave" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="dMatriz" QueryStringField="dMatriz" DefaultValue="-1" />
                                 <asp:QueryStringParameter Name="dIndicador" QueryStringField="sIndicador" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="sNivel" QueryStringField="sNivel" DefaultValue="0" />
                                 <asp:QueryStringParameter Name="sPantalla" QueryStringField="t" />
                                 <asp:Parameter Name="universoPaemir" DefaultValue="1" />
                                 <asp:Parameter Name="idIndicadorSectorial" DefaultValue="-1" />
                              </SelectParameters>
                           </asp:ObjectDataSource>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <div id="tituloDivValoraciones" class="valoraciones col-md-12" runat="server">
         <h4><b>Valoraciones del CONEVAL acerca de los programas de la dependencia</b></h4>

         <div id="barraCumplimientoMetas" class="col-md-12 col-xs-12" runat="server">
            <div class="col-md-3 col-xs-12 tituloVal">
               <h5>Cumplimiento de metas</h5>
            </div>
            <div id="divMetas" class="col-md-9 col-xs-12" >
               <div class="centrarGraficaMetas">
                  <div class="progress">
                     <div class="progress-bar barraMetas" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="<%=barraMetas%>">
                        <p><%=promedioMetas%>%</p>
                     </div>
                  </div>
               </div>
            </div>
         </div>

         <div id="graficaMIR" class="col-md-12 col-xs-12" runat="server">

            <div class="col-md-3 col-xs-12 tituloVal">
               <h5>Calidad de la MIR</h5>
            </div>
            <div class="col-md-9">
            <div id="divCalidadMIR"></div>
            <div style="text-align: center; font-size: 10px; font-family: Arial; font-style: italic">
                Número de programas
            </div>
            </div>
         </div>

         <div id="graficaEDR" class="col-md-12" runat="server">
            <div class="col-md-2 col-xs-12 tituloVal">
               <h5>Enfoque de Resultados</h5>
            </div>
            <div id="divResultados" class="col-md-5"></div>
            <div id="legenddiv" class="col-md-5" style="margin-top: 7%;"></div>
         </div>
      </div>
      <br />
      <br />
      <div id="divFooter" class="col-md-12" style="padding: 1.5em  1em">
         <asp:Label runat="server" ID="LblFooterOne" Font-Bold="true" Font-Size="X-Small" Text="*​ Los programas S y U están sujetos al proceso de aprobación"></asp:Label><br />
         <asp:Label ID="LblFooterTwo" runat="server"  Font-Bold="true" Font-Size="X-Small" Text="Tasa de permanencia de los indicadores: Proporción de indicadores que han perdurado en un determinado periodo" ></asp:Label> <br />
       </div>
   </div>
     </div>
   <script>
       (function () {
           fjsCargaDependenciaYCiclo();
       })();

   </script>
</asp:Content>

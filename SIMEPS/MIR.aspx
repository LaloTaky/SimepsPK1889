<%@ Page Title="SIMEPS - Sistema de Medición de la Política Social" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="MIR.aspx.cs" Inherits="SIMEPS.MIR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphTituloHeader" runat="server"> 
   <p class="lblTitulo"><asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de los programas y acciones de desarrollo social</asp:Label></p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="row">
       <div class="col-md-12">
        <div style="padding: 1em 0 ">
            <asp:Label ID="LblCiclo" runat="server" Text="CICLO" CssClass="tituloMir"></asp:Label>
        </div>
          <asp:Label ID="LblNombrePrograma" runat="server" Text="NOMBREPROGRAMA" CssClass="NombreProgramaMir"></asp:Label>
      </div>
   </div>
   <div class="row">
      <div class="col-md-12">
         <div id="divValoraciones" style="padding: 0em  1em; display:none">
            <h4 style="color: #000; font-size: 12pt !important;">Resultados Valoraciones CONEVAL</h4>
            <div class="col-lg-12">
               Dictamen de aprobación:
               <asp:Label runat="server" ID="LblDictamen" Text="NA"></asp:Label>
            </div>
            <div class="col-lg-12">
               Observaciones históricas anuales promedio por indicador:
               <asp:Label runat="server" ID="LblObservaciones" Text="-"></asp:Label>
            </div>
            <div class="col-lg-12">
               Tasa de permanencia anual:
               <asp:Label runat="server" ID="LblTasaPermanencia" Text="-"></asp:Label>
            </div>
            <div class="col-lg-12" style="padding-left:0px">
               <div class="col-lg-3"> Enfoque de Resultados<sup>/1</sup>:</div>
               <div class="col-lg-9">
                  <div id="DivCalEDR" class='<%=divEDRclassName%>'></div>
                  <asp:Label runat="server" ID="LblCalEDR"></asp:Label>
               </div>
            </div>
            <br />
            <div class="col-lg-12" style="padding-left:0px">
               <div class="col-lg-3">Calificación MIR<sup>/2</sup>: </div>
               <div class="col-lg-9">
                  <div id="DivCalTot" class='<%=divTotalclassName%>'></div>
                  <asp:Label runat="server" ID="LblCalTot"></asp:Label>
               </div>
            </div>
         </div>
         <br />
             
             <div id="divBotones" style="text-align: left !important" class="divDescargaMir">
                 <br /><br />
                 <img src="img/descarga_datos2.jpg" id="linkDescargaMIR" onclick='fjsDetenerEvento(event); fjsDescargaMIR(this);' alt="Descarga MIR" class="descargaMir" style="cursor:pointer"/>
                 <p>Descarga MIR<br />Descargar archivo</p>

                 <br />
             </div>

         <asp:Repeater ID="rptPrograma" runat="server" DataSourceID="odsProgramas">
            <ItemTemplate>
               <asp:Label ID="lblNombrePrograma" runat="server" Text='<%#Eval("NOMBRE") %>' CssClass="tituloMir" Visible="false" ></asp:Label>
               <asp:Label ID="lblTituloResumen" runat="server" Text="Resumen de la Matriz de Indicadores" CssClass="tutuloResumenMir" Visible="false" ></asp:Label>
               <label class="tituloObjetivos" style="display: none;">Objetivo nacional: </label>
               <asp:Label ID="lblPrograma" runat="server" Text='<%#Eval("OBJETIVO_NACIONAL") %>' Visible="false" ></asp:Label>
               <label class="tituloObjetivos" style="display: none;">Objetivo estrategico: </label>
               <asp:Label ID="lblEstrategico" runat="server" Text='<%#Eval("OBJETIVO_ESTRATEGICO") %>' Visible="false" ></asp:Label>
               <asp:GridView ID="gvObjetivos" runat="server" DataSource='<%#Eval("OBJETIVOS") %>' AutoGenerateColumns="false" OnDataBound="gvObjetivos_DataBound" CssClass="gridObjetivos" HeaderStyle-CssClass="header" BorderWidth="0">
                  <Columns>
                     <asp:TemplateField HeaderStyle-Width="180px" ItemStyle-Width="180px" ItemStyle-CssClass="nivel">
                        <HeaderTemplate>
                           
                        </HeaderTemplate>
                        <ItemTemplate>
                           <div class="vertical">
                              <asp:Label ID="lblObjetivo" runat="server" Text='<%#Eval("NIVEL_TEXTO") %>'></asp:Label>
                           </div>
                        </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="300px">
                        <HeaderTemplate>
                           <asp:Label runat="server" Width="300px" CssClass="titulosIndicadores">Resumen Narrativo</asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="ObjetivoIndicador">
                           <asp:Label runat="server" Text='<%#Eval("NUMERACION").ToString().Equals("0")?"":Eval("NUMERACION").ToString().Replace(".","-")+"."%>' CssClass="tituloObjetivos"></asp:Label>
                           <asp:Label ID="lblObjetivo" runat="server" Text='<%#Eval("DESC_NIVEL") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="600px">
                        <HeaderTemplate>
                           <asp:Label runat="server" Width="280px" CssClass="titulosIndicadores">Indicador</asp:Label>
                           <asp:Label runat="server" Width="280px" CssClass="titulosIndicadores">Medios de Verificación</asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="">
                           <asp:GridView runat="server" ID="gvIndicadores" DataSource='<%#Eval("INDICADORES") %>' AutoGenerateColumns="false" ShowHeader="false" Width="600px" OnRowDataBound="gvIndicadores_RowDataBound" BorderWidth="0" >
                              <Columns>
                                 <asp:TemplateField  ItemStyle-Width="300px" ItemStyle-BorderColor="White">
                                    <ItemTemplate>
                                        <asp:HyperLink id="hyperlink"  NavigateUrl='<%#Eval("LIGA")%>' Text='<%#Eval("NOMBRE_IND") %>' Target="_self" runat="server"/>  
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField  ItemStyle-Width="300px" ItemStyle-BorderColor="White">
                                    <ItemTemplate>
                                       <asp:GridView runat="server" ID="gvVariables" DataSource='<%#Eval("VARIABLES") %>' AutoGenerateColumns="false" BorderWidth="0" ShowHeader="false" Width="300px" >
                                          <Columns>
                                             <asp:TemplateField>
                                                <ItemTemplate>
                                                   <p style="width:300px;word-break:normal;word-wrap:break-word">
                                                      <%#Eval("DESC_MEDIO_VERIFICACION") %>
                                                   </p>
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                          </Columns>
                                       </asp:GridView>                                    
                                    </ItemTemplate>
                                 </asp:TemplateField>
                              </Columns>
                           </asp:GridView>
                        </ItemTemplate>                         
                     </asp:TemplateField>                     
                  </Columns>
               </asp:GridView>              
            </ItemTemplate>
         </asp:Repeater>
         <asp:ObjectDataSource ID="odsProgramas" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarPrograma" >
            <selectparameters>
               <asp:querystringparameter name="Anio" querystringfield="pCiclo" type="Decimal" defaultvalue="0" />
               <asp:querystringparameter name="Ramo" querystringfield="pRamo" defaultvalue="0" />
               <asp:querystringparameter name="Unidad" querystringfield="unidad" defaultvalue="0" />
               <asp:querystringparameter name="palabraClave" defaultvalue="0"/>
               <asp:querystringparameter name="dMatriz" querystringfield="pIdMatriz" defaultvalue="-1" />
               <asp:querystringparameter name="dIndicador" querystringfield="sIndicador" defaultvalue="0" />
               <asp:querystringparameter name="sNivel" querystringfield="sNivel" defaultvalue="0" />
               <asp:querystringparameter name="sPantalla" querystringfield="t" />
               <asp:Parameter Name="universoPaemir" DefaultValue="1" />
               <asp:Parameter Name="idIndicadorSectorial" DefaultValue="-1" />
            </selectparameters>
         </asp:ObjectDataSource>
         <div id="divHistoricoProgramas" style="padding: 1.5em  1em">
            <br /><br />               
            <asp:Repeater ID="RepeaterHistorico" runat="server" DataSourceID="odsHcoProgramas">
               <ItemTemplate>
                  <a href='<%#Eval("LIGA") %>' class="btn btn-primary"><%#Eval("CICLO") %></a>
               </ItemTemplate>
            </asp:Repeater>
            <asp:ObjectDataSource ID="odsHcoProgramas" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarHistoricoPrograma" >
               <selectparameters>
                  <asp:querystringparameter name="dMatriz" querystringfield="pIdMatriz" defaultvalue="-1" />
                  <asp:querystringparameter name="sPantalla" querystringfield="t" />
               </selectparameters>
            </asp:ObjectDataSource>
         </div>
         <div id="divFooter" style="padding: 1.5em  1em">
            <asp:Label runat="server" ID="LblFooterOne" Font-Bold="true" Font-Size="X-Small" Text="<sup>/1</sup> La valoración de Enfoque de Resultados se lleva a cabo todos los años nones. La primera valoración se llevó a cabo en 2016. "></asp:Label>
            <br />
            <asp:Label runat="server" ID="LblFooterTwo" Font-Bold="true" Font-Size="X-Small" Text="<sup>/2</sup> La valoración MIR se lleva a cabo todos los años pares."></asp:Label>
         </div>
      </div>
   </div>
</asp:Content>


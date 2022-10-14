<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuscaIndicador.aspx.cs" Inherits="SIMEPS.BuscaIndicador" %>
<%@ Register Src="~/Comun/Controls/Progress.ascx" TagPrefix="uc1" TagName="Progress" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="main">   
      <asp:UpdatePanel runat="server" ID="UpdatePFiltros">
         <ContentTemplate>
            <asp:Panel runat="server" ID="panelBusquedaEspecializada" Visible="false">
                <br />

                <div class="row">
                    <p class="BuscEncabezado">  <img src="img/busqueda.jpg" class="imageBusqueda" />
                            Buscador temático
                        </p>
                </div>               
                <br />
               <div class="row">
                  <div class="col-xs-6 col-md-2">
                     <label class="tituloFiltros">Año:</label>
                     <asp:DropDownList ID="ddlCiclos" runat="server" DataTextField="CICLO_VALUE" DataValueField="CICLO_ID" CssClass="textoMosaico form-control" AutoPostBack="true">
                     </asp:DropDownList>
                     <asp:ObjectDataSource ID="odsCiclos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarCiclos">
                        <SelectParameters>
                           <asp:Parameter DefaultValue="BuscaIndicador" Name="sPantalla" />
                        </SelectParameters>
                     </asp:ObjectDataSource>
                  </div>
                  <div class="col-xs-6 col-md-4">
                     <label class="tituloFiltros">Dependencia:</label>
                     <asp:DropDownList ID="ddlRamo" runat="server" CssClass="textoMosaico form-control" AutoPostBack="true"></asp:DropDownList>
                     <asp:ObjectDataSource ID="odsRamo" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarRamosIndicadores">
                        <SelectParameters>
                           <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="2010" Name="pCiclo" />
                           <asp:QueryStringParameter DefaultValue="A" QueryStringField="t" Name="pCamino" />
                        </SelectParameters>
                     </asp:ObjectDataSource>
                  </div>
                  <div class="col-xs-12 col-md-6">
                     <label class="tituloFiltros">Programa:</label>
                     <asp:DropDownList ID="ddlPrograma" runat="server" CssClass="textoMosaico form-control" AutoPostBack="true"></asp:DropDownList>
                     <asp:ObjectDataSource ID="odsPrograma" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarProgramasIndicadores">
                        <SelectParameters>
                           <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="<%=dCiclo%>" Name="anio" />
                           <asp:ControlParameter ControlID="ddlRamo" DefaultValue="16" Name="ramo" />
                        </SelectParameters>
                     </asp:ObjectDataSource>
                  </div>
               </div>
               <div class="row">
                  <div class="col-xs-12 col-md-6">
                     <label class="tituloFiltros">Obj. del Plan Nacional de Desarrollo:</label>
                     <asp:DropDownList ID="ddlObjetivoN" runat="server" CssClass="textoMosaico form-control" AutoPostBack="false"></asp:DropDownList>
                     <asp:ObjectDataSource ID="odsObjetivoN" runat="server" TypeName="Simeps.Dal.IndicadoresDal" SelectMethod="ConsultarObjetivoNacional">
                        <SelectParameters>
                           <asp:ControlParameter ControlID="ddlPrograma" DefaultValue="0" Name="pIdMatriz" />
                           <asp:ControlParameter ControlID="ddlRamo" DefaultValue="16" Name="pRamo" />
                        </SelectParameters>
                     </asp:ObjectDataSource>
                  </div>

                  <div class="col-xs-12 col-md-6">
                     <label class="tituloFiltros">Programa Sectorial:</label>
                     <asp:DropDownList ID="ddlProgramaSectorial" runat="server" CssClass="textoMosaico form-control" AutoPostBack="false">
                     </asp:DropDownList>
                     <asp:ObjectDataSource runat="server" ID="odsProgramaSectorial" TypeName="Simeps.Dal.IndicadoresDal" SelectMethod="ConsultarProgramaSectoriales">
                        <SelectParameters>
                           <asp:Parameter DefaultValue="-2" Name="idProgramaSectorial" />
                        </SelectParameters>
                     </asp:ObjectDataSource>
                  </div>
               </div>
               <div class="row">
                  <div class="col-xs-6 col-md-4">
                     <label class="tituloFiltros">Nivel de MIR:</label>
                     <asp:DropDownList ID="ddlNivelM" runat="server" CssClass="textoMosaico form-control" AutoPostBack="true">
                        <asp:ListItem Selected="True" Value="0">-Seleccione-</asp:ListItem>
                        <asp:ListItem Value="1">Fin</asp:ListItem>
                        <asp:ListItem Value="2">Propósito</asp:ListItem>
                        <asp:ListItem Value="3">Componente</asp:ListItem>
                        <asp:ListItem Value="4">Actividad</asp:ListItem>
                     </asp:DropDownList>
                  </div>
                  <div class="col-xs-6 col-md-4">
                     <label class="tituloFiltros">Unidad de Medida:</label>
                     <asp:DropDownList ID="ddlUnidaM" runat="server" CssClass="textoMosaico form-control" AutoPostBack="true"></asp:DropDownList>
                     <asp:ObjectDataSource ID="odsUnidadM" runat="server" TypeName="Simeps.Dal.IndicadoresDal" SelectMethod="ConsultarUnidadMEdida">
                        <SelectParameters>
                           <asp:ControlParameter ControlID="ddlPrograma" DefaultValue="0" Name="pIdMatriz" />
                           <asp:ControlParameter ControlID="ddlRamo" DefaultValue="0" Name="pRamo" />
                        </SelectParameters>
                     </asp:ObjectDataSource>
                  </div>
                  <div class="col-xs-12 col-md-4">
                     <label class="tituloFiltros">Frecuencia de Medición:</label>
                     <asp:DropDownList ID="ddlFrecuanciaM" runat="server" CssClass="textoMosaico form-control" AutoPostBack="true"></asp:DropDownList>
                     <asp:ObjectDataSource ID="odsFrecuanciaM" runat="server" TypeName="Simeps.Dal.IndicadoresDal" SelectMethod="ConsultarFrecuenciaMedicion">
                        <SelectParameters>
                           <asp:ControlParameter ControlID="ddlPrograma" DefaultValue="0" Name="pIdMatriz" />
                           <asp:ControlParameter ControlID="ddlRamo" DefaultValue="0" Name="pRamo" />
                        </SelectParameters>
                     </asp:ObjectDataSource>
                  </div>
                   
                       <div class="col-xs-12 col-md-4">
                            <label class="tituloFiltros">Derecho social:</label>
                        <asp:DropDownList ID="ddlDerechos" runat="server" CssClass="textoMosaico form-control">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsDerechos" runat="server" TypeName="Simeps.Dal.IndicadoresDal" SelectMethod="ConsultarDerechos"></asp:ObjectDataSource>
                           <br />                      
                       </div>                     

                   
               </div>


              <div class="row">
                    <div class="col-xs-3  col-sm-1 col-md-3 ">
                   <label class="tituloFiltros" style="float:right; position:inherit;"> Palabra clave:</label>
                        </div>
                  <div class="col-xs-12 col-md-9">
                                <asp:TextBox ID="texBuscador" runat="server" CssClass="textoBusqueda form-control"></asp:TextBox>
                            </div>

                  </div>

                 <div class="row">

                  
                </div>
            </asp:Panel>
         </ContentTemplate>
      </asp:UpdatePanel>
      <asp:UpdateProgress runat="server" ID="ProgresFiltros" AssociatedUpdatePanelID="UpdatePFiltros">
         <ProgressTemplate>
            <uc1:Progress runat="server" id="Progress" />
         </ProgressTemplate>
      </asp:UpdateProgress>

      <div class="row">
        
      </div>
      
      <hr />
      <div class="row">
         <div class="col-xs-12 text-center">
            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-default btn-sm" Width="200px" Visible="true" OnClick="btnBuscar_Click" />
            <asp:Button runat="server" ID="btnRegresar" Text="Regresar página de inicio" CssClass="btn btn-default btn-sm" Width="200px" OnClick="btnRegresar_Click" />
            <asp:Button runat="server" ID="btnLimpiar" Text="Borrar búsqueda" CssClass="btn btn-default btn-sm" Width="200px" OnClick="btnLimpiar_Click" />
         </div>
      </div>

      <hr />
      <asp:UpdatePanel ID="upnIndicadores" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
         <ContentTemplate>
            <asp:GridView ID="grvPrograma" runat="server" DataSourceID="sdsBuscador" AutoGenerateColumns="false" ShowHeader="true" Style="width: 100%" AllowPaging="true" PageSize="30" CssClass="tablaBusqueda">
               <Columns>
                  <asp:TemplateField ItemStyle-Width="84%" HeaderText="Resultados de la búsqueda" HeaderStyle-CssClass="EncabezadoProgramas">
                     <ItemTemplate>
                        <a id="aPrograma" runat="server" style="color: #1b4093" href='<%#Eval("LIGA")+"&vBusqueda=Buscador Temático "%>' class="LigaPrograma">
                           <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE") %>'></asp:Label>
                        </a>
                        <br />
                     </ItemTemplate>
                  </asp:TemplateField>
               </Columns>
               <EmptyDataTemplate>
                  <p class="text-center">"No se encontraron registros con los filtros seleccionados"</p>
               </EmptyDataTemplate>
               <EmptyDataRowStyle CssClass="tituloFiltros" BorderStyle="None" />
               <RowStyle CssClass="tablaRenglon" />
               <PagerStyle CssClass="PaginacionProgramas" />
               <AlternatingRowStyle BackColor="#e1e1e1" />
            </asp:GridView>
            <asp:HiddenField runat="server" ID="HdCiclo" />
            <asp:HiddenField runat="server" ID="HdRamo" />
            <asp:HiddenField runat="server" ID="HdPrograma" />
            <asp:HiddenField runat="server" ID="HdObjetivo" />
            <asp:HiddenField runat="server" ID="HdProgSecto" />
            <asp:HiddenField runat="server" ID="HdNivelMIR" />
            <asp:HiddenField runat="server" ID="HdnUnidad" />
            <asp:HiddenField runat="server" ID="HdFrecuencia" />
            <asp:HiddenField runat="server" ID="HdDerecho" />
            <asp:HiddenField runat="server" ID="HdPalabra" />
            <asp:ObjectDataSource ID="sdsBuscador" runat="server" TypeName="Simeps.Dal.IndicadoresDal" SelectMethod="BuscarIndicadores" OnSelected="sdsBuscador_Selected">
               <SelectParameters>
                  <asp:ControlParameter Name="textoBusqueda" ControlID="texBuscador" Type="String" />
                  <asp:ControlParameter Name="derecho" ControlID="HdDerecho" Type="Int16" DefaultValue="0" />
                  <asp:ControlParameter Name="ciclo" ControlID="HdCiclo" Type="Int16" DefaultValue="" />
                  <asp:ControlParameter Name="ramo" ControlID="HdRamo" Type="Int16" DefaultValue="" />
                  <asp:ControlParameter Name="idMatriz" ControlID="HdPrograma" Type="String" DefaultValue="0" />
                  <asp:ControlParameter Name="objetivo" ControlID="HdObjetivo" Type="Int16" DefaultValue="0" />
                  <asp:ControlParameter Name="nivelMIR" ControlID="HdNivelMIR" Type="Int16" DefaultValue="0" />
                  <asp:ControlParameter Name="unidadM" ControlID="HdnUnidad" Type="String" DefaultValue="0" />
                  <asp:ControlParameter Name="frecuencia" ControlID="HdFrecuencia" Type="String" DefaultValue="0" />
                  <asp:ControlParameter Name="sectorial" ControlID="HdProgSecto" Type="String" DefaultValue="0" />
               </SelectParameters>
            </asp:ObjectDataSource>
         </ContentTemplate>
         <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grvPrograma" />
         </Triggers>
      </asp:UpdatePanel>
      <asp:UpdateProgress runat="server" ID="ProgressResults" AssociatedUpdatePanelID="upnIndicadores">
         <ProgressTemplate>
            <uc1:Progress runat="server" id="ProgressInd" />
         </ProgressTemplate>
      </asp:UpdateProgress>
   </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MosaicoFin.aspx.cs" Inherits="SIMEPS.MosaicoFin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphTituloHeader" runat="server">
   <p class="lblTitulo">
      <asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de la política social</asp:Label>
   </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <br />
   <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 NombreFichaMonitoreo TituloFin">
      <p class="" style="float: left;">Indicadores de Fin de los programas y acciones sociales</p>
   </div>
   <br />
   <br />
   <div class="row">
      <div class="col-xs-12">
         <div class="row">
            <div class="col-xs-2"></div>
            <div class="col-xs-9">
               <div class="row">
                  <asp:Repeater ID="rprCiclos" runat="server" DataSourceID="odsCiclos">
                     <ItemTemplate>
                        <div class="col-xs-4 col-sm-2 col-md-2 col-lg-2 col-xl-2" style="padding: 5px;">
                           <a href="MosaicoFin.aspx?pCiclo=<%#Eval("CICLO_VALUE")+valorT%>" class="btn-mosaico <%#Eval("CICLO_VALUE").ToString()==iCiclo.ToString()?" btn-mosaico-selected":"btn-mosaico-general"%>"><%#Eval("CICLO_VALUE")%></a>
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
      <div id="divBotones" class="divBotones">
        <input id="hddCicloMosaico" type="hidden" value="<%=iCiclo%>" />
         <br />
         <div class="col-sm-3"></div>
         <div class="col-sm-6">
            <div class="col-sm-6">
               <table>
                  <tr>
                     <td>
                        <asp:ImageButton ImageUrl="img/descarga_excel.jpg" runat="server" ID="ImgHistoricoIndFinExcel" Text="EXCEL" CssClass="img-descarga" />
                     </td>
                     <td rowspan="2">Descargar base histórica de indicadores
                     </td>
                  </tr>
                  <tr>
                     <td>
                        <asp:ImageButton ImageUrl="img/descarga_csv.jpg" runat="server" ID="ImgHistoricoIndFinCSV" Text="CSV" CssClass="img-descarga" />
                     </td>
                     <td></td>
                  </tr>
               </table>
            </div>
            <div class="col-sm-6">
               <table>
                  <tr>
                     <td>
                       <asp:ImageButton ImageUrl="img/descarga_excel.jpg" runat="server" ID="ImgIndExcel"  OnClientClick="fjsDetenerEvento(event); fjsDescargaBaseIndicadores(this, 3);" Text="EXCEL" CssClass="img-descarga" />
                     </td>
                     <td rowspan="2">Descargar base de datos de todos los indicadores
                     </td>
                  </tr>
                  <tr>
                     <td>
                       <asp:ImageButton ImageUrl="img/descarga_csv.jpg" runat="server" ID="ImageButton1"  OnClientClick="fjsDetenerEvento(event); fjsDescargaBaseIndicadoresCSV(this, 2);" Text="CSV" CssClass="img-descarga" /> 
                     </td>
                     <td></td>
                  </tr>
               </table>
            </div>
         </div>
         <br />
      </div>
   </div>
   <br />
   <table class="tablaMosaico">
      <asp:Repeater ID="rprMosaicos" runat="server" DataSourceID="odsMosaicos">
         <ItemTemplate>
            <%# Container.ItemIndex%3 == 0 ? (Container.ItemIndex == 0 ?"":"</tr><tr>"):""%>
            <td class="tdMosaico" onmouseover="this.className='tdMosaicoHover';" onmouseout="this.className='tdMosaico';">
               <a href='<%#Eval("LIGA")%>'>
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
         <asp:Parameter Type="Boolean" DefaultValue="true" Name="pMosaicoFin" />
      </SelectParameters>
   </asp:ObjectDataSource>
   <script>
      $(document).ready(function () {
         $("#ContentPlaceHolder1_ImgIndCSV").click(function () {
            setTimeout(function () {
               fjsBlockUIForDownload(this);
            }, 1000);
         });
      });
</script>
</asp:Content>



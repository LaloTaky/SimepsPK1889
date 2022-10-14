<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FichasMonitoreo.aspx.cs" Inherits="SIMEPS.FichasMonitoreo" %>

<asp:Content ID="Content4" ContentPlaceHolderID="cphTituloHeader" runat="server">
   <p class="lblTitulo">
      <asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de la política social</asp:Label>
   </p>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <!-- Owl Stylesheets -->
   <link  rel="stylesheet" href="Content/owl.carousel.min.css" rel="stylesheet" />
   <link href="Content/owl.theme.default.min.css" rel="stylesheet" />
   <script src="scripts/jquery.min.js"></script>
   <script src="scripts/owl.carousel.js"></script>

   <br />
   <br />

   <div class="row">
      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 NombreFichaMonitoreo">
         <p style="line-height:normal">Fichas de Monitoreo de Políticas Sociales</p>
      </div>
   </div>

   <br />
   <br />
   <div class="row">
      <div class="col-xs-12">
         <table>
            <tr>
               <asp:Repeater ID="rprCiclos" runat="server" DataSourceID="odsCiclos">
                  <ItemTemplate>
                     <td style="padding: 5px">
                        <a href="FichasMonitoreo.aspx?pCIclo=<%#Eval("CICLO")%>" class="btn-mosaico <%#Eval("CICLO").ToString()==iCiclo.ToString()?" btn-mosaico-selected":"btn-mosaico-general"%>"><%#Eval("CICLO")%></a>
                     </td>
                  </ItemTemplate>
               </asp:Repeater>
            </tr>
         </table>
      </div>
   </div>
   <br />
   <br />
   <asp:UpdatePanel runat="server" ID="UpdateCicloFiltros">
      <ContentTemplate>
         <div class="row">
            <div class="col-xs-12 text-center">
               <span id="cfmAnteriorRes" class="glyphicon glyphicon-chevron-left carr-flechaRes"></span>
               &nbsp;
               <span id="cfmSiguienteRes" class="glyphicon glyphicon-chevron-right carr-flechaRes"></span>
            </div>
         </div>
         <div class="row">
            <div class="col-xs-1 text-center">
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />
               <span id="cfmAnterior" class="glyphicon glyphicon-chevron-left carr-flecha"></span>
            </div>
            <div class="col-xs-10">
                     <div class="owl-carousel owl-theme">
                        <asp:Repeater runat="server" ID="Repeater1" DataSourceID="odsFichasM">
                           <ItemTemplate>
                              <div class="item">
                                 <a href="<%#Eval("URL_FICHA") %>">
                                    <img class="img-responsive" src="<%#Eval("URL_PORTADA") %>" onerror="this.src='https://www.coneval.org.mx/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'" height='300'>
                                 </a>
                              </div>
                           </ItemTemplate>
                        </asp:Repeater>
                     </div>
            </div>
            <div class="col-xs-1 text-center">
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />
               <span id="cfmSiguiente" class="glyphicon glyphicon-chevron-right carr-flecha"></span>
            </div>
         </div>
      </ContentTemplate>
   </asp:UpdatePanel>


   <asp:ObjectDataSource ID="odsFichasM" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarFichasMonitoreo">
      <SelectParameters>
         <asp:QueryStringParameter QueryStringField="pCIclo" DefaultValue="2016" Name="iCiclo" />
      </SelectParameters>
   </asp:ObjectDataSource>

   <asp:ObjectDataSource ID="odsCiclos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarCiclosFichasMonitoreo"></asp:ObjectDataSource>

   <script>
      $(document).ready(function () {
         var owl = $('.owl-carousel').owlCarousel({
            loop: true,
            margin: 10,
            responsiveClass: true,
            autoplay: true,
            responsive: {
               0: {
                  items: 1,
                  nav: false,
                  loop: true
               },
               600: {
                  items: 3,
                  nav: false,
                  loop: true
               },
               1000: {
                  items: 4,
                  nav: false,
                  loop: true
               }
            }
         });

         $('#cfmAnterior').click(function () {
            owl.trigger('prev.owl.carousel');

         });

         $('#cfmSiguiente').click(function () {
            owl.trigger('next.owl.carousel');
         });

         $('#cfmAnteriorRes').click(function () {
            owl.trigger('prev.owl.carousel');

         });

         $('#cfmSiguienteRes').click(function () {
            owl.trigger('next.owl.carousel');
         });
      })
   </script>

</asp:Content>


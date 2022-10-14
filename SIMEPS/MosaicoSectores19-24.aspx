<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MosaicoSectores19-24.aspx.cs" Inherits="SIMEPS.MosaicoSectores19_24" %>

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
   <div class="row">
       <div class="contentText">
             <span class="lbTituloPND">&nbsp;&nbsp;&nbsp;&nbsp;Programas derivados del PND 2019-2024 </span>
         </div>
      <div class="col-xs-12">
          <div class="col-md-3 col-md-offset-9 col-sm-4 col-sm-offset-8 col-xs-12" style="float:right; zoom:0.8; margin-top:-26px; padding:10px; right:0px;" id="divBotonDescargaBD">
                <div class="col-md-4 col-xs-2 col-sm-4">
                   <asp:ImageButton  ImageUrl="img/descarga_datos2.jpg" runat="server" ID="ImgBtnDescargaBD" Width="55px" CssClass="btnDescargas"  OnClientClick='fjsDetenerEvento(event); fjsDescargaBaseDatosAllPND19_24()'/>
                </div>
                 <div class="col-md-8 col-xs-12 col-sm-8">
                     <label id="idLblDescargaBD" class="imgBtnDescargaRep">Base de datos de los indicadores derivados del PND 2019-2024</label>
                 </div>
            </div>
           
         <div class="contentText">
            <span class="lbEstadisticas">Estadísticas Básicas</span>
         </div>

         <div class="jumbotron">
            <div class="row" style="display:flex">
               <asp:Repeater ID="rprContador" runat="server" DataSourceID="odsContadorSipol">
                  <ItemTemplate>
                     <div class="col-xs-6" style="margin:auto">
                        <div class="contenido4T">
                           <span class="single-line"><%#Eval("TIPO") %></span>
                           <br />
                            <span> </span>
                            <br />
                           <div class="contenCounter" style="margin: auto; display: inline-block; width: auto;">
                              <div class="counter <%#Eval("TIPO").ToString().ToLower().Replace(" ", "")%>" data-count="<%#Eval("CONTEO") %>">0</div>
                           </div>     
                        </div>

                     </div>
                  </ItemTemplate>
               </asp:Repeater>
            </div>
         </div>

      </div>
   </div>
   <asp:ObjectDataSource ID="odsContadorSipol" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultaEstadisticaBasica4T">
      <SelectParameters>
         <asp:QueryStringParameter Name="nSector" QueryStringField="nSector" DefaultValue="0" />
      </SelectParameters>

   </asp:ObjectDataSource>
   <div class="row">
      <div class="col-xs-12">
         <span class="lbEstadisticas">Monitoreo de indicadores de la política social por sector</span>
      </div>
   </div>

   <div class="row">
      <div class="col-xs-12 col-md-12 contenIndicadores">
         <asp:Repeater ID="rprMosaicos" runat="server" DataSourceID="odsMosaicos">
            <ItemTemplate>
               <div class="col-xs-6 col-md-3 response">
                  <a href="IndicadorSectorial19-24.aspx?idsector=<%#Eval("ID_SECTOR")%>">
                     <img class="img-sector" src='<%#Eval("ICONO") %>' alt="Responsive image" onerror="this.src='/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'">
                     <div style="height: 35px;"><span class="nombre-sectores"><%#Eval("NOMBRE") %></span></div>
                  </a>
               </div>
            </ItemTemplate>
         </asp:Repeater>
      </div>
      <asp:ObjectDataSource ID="odsMosaicos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarSectores4T">
         <SelectParameters>
            <asp:QueryStringParameter Name="idSector" QueryStringField="id" DefaultValue="-1" />
         </SelectParameters>
      </asp:ObjectDataSource>
   </div>

   <div class="row">
      <div class="col-md-12">
         <div class="col-xs-2">
         </div>
         <div class="col-xs-8">
            <div class="jumbotron" style="align-content: center; background-color: white">
            </div>
         </div>
      </div>
   </div>



   <div class="row contenComparadorIndicadores" style="display:none">
      <div class="col-xs-12">
         <div class="col-xs-2">
         </div>
         <div class="col-md-3 col-xs-12">
            <div class="col-xs-12" style="align-content: center; text-align: center">
               <a href="MosaicoFin.aspx" class="url-imgb-ficha-indicador">
                  <img class="img-sector" src='img/indicadoresprogramas.jpg' alt="Responsive image" onerror="this.src='/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'" />
                  <div style="align-content: center; text-align: center; margin-bottom: 100px">
                     <span>Indicadores de fin de los programas y acciones sociales</span>
                  </div>
               </a>
            </div>
         </div>
         <div class="col-xs-2">
         </div>
         <div class="col-md-3 col-xs-12">
            <div class="col-xs-12" style="align-content: center; text-align: center">
               <a href="FichasMonitoreo.aspx" class="url-imgb-ficha-indicador">
                  <img class="img-sector" src='img/fichasmonitoreo.jpg' alt="Responsive image" onerror="this.src='/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'" />
                  <div style="align-content: center; text-align: center; margin-bottom: 100px">
                     <span>Fichas de Monitoreo de Políticas Sociales</span>
                  </div>
               </a>
            </div>
         </div>
         <div class="col-xs-2">
         </div>
      </div>
   </div>


   <script type="text/javascript">
      $('.counter').each(function () {
         var $this = $(this),
            countTo = $this.attr('data-count');

         $({ countNum: $this.text() }).animate({
            countNum: countTo
         },

            {

               duration: 8000,
               easing: 'linear',
               step: function () {
                  $this.text(Math.floor(this.countNum));
               },
               complete: function () {
                  $this.text(this.countNum);
                  //alert('finished');
               }

            });

      });
   </script>
</asp:Content>

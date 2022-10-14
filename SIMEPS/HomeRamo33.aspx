<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomeRamo33.aspx.cs" Inherits="SIMEPS.HomeRamo33" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTituloHeader" runat="server">
    <p class="lblTitulo">
        <asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de Ramo 33 del ámbito social</asp:Label>
    </p>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main">

        <%--<h2 class="PlecaEncabezado" runat="server" id="TituloEncabezado">
            <span id="headerContent">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label>&#8203;&#8203;&#8203;</span>
        </h2>--%>

        <h2 runat="server" id="SubtituloEncabezado" style="color: #00a94f; line-height: 18px; font-family: arial; font-size: 10pt; text-align: justify; margin-bottom: 0px !important;">
            <span class="subtitulos">
                <asp:Label ID="lblSubtitulo" runat="server"></asp:Label>
            </span>
        </h2>
        
        <br />

        <div class="textoMosaico">

            <div>
                <span>&#8203;<br />
                </span>
            </div>
                       

            <div class="row" style="margin-top: -25px;">
                <%--<div class="col-md-1"></div>--%>
                <div class="col-md-11 textoHomeR33 text-justify">
                    <ul>
                        <li><span>El Ramo 33 se compone de fondos de recursos que el gobierno federal otorga a las entidades federativas y los municipios, para que estas provean de servicios a su población.</span></li>
                        <li><span>Dichos recursos deben ser asignados a funciones específicas de cada fondo, tales como: salud, educación, alimentación, vivienda, seguridad pública y fortalecimiento financiero.</span></li>
                        <li><span>Los fondos del Ramo 33 representan una parte importante del <i>Presupuesto de Egresos de la Federación</i>, por lo que es relevante desarrollar herramientas que nos permitan evaluar qué tan bien se están manejando sus recursos.</span></li>
                        <li><span>Para ello, los fondos del Ramo 33 cuentan con Matrices de Indicadores para Resultados (MIR), las cuales establecen los objetivos que pretenden alcanzar, así como indicadores que muestran su desempeño en la consecución de estos logros.</span></li>

                    </ul>
                </div>
                <div class="col-md-1"></div>
            </div>

            
            <br />
            <br />

            <div class="row">
                <div class="col-md-4">
                    <p>
                        <span>
                            <asp:Label ID="lblCuerpo8" runat="server" Class="subtitulonegritas"></asp:Label></span>
                    </p>
                </div>
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <%--PKP1377- Se comenta codigo hasta que exista información en la BD para poder descargas las bases de datos--%>
                    <table>    
                        <tr>
                            <td>
                                <a >
                                    <asp:ImageButton id="xlsTodosInd" runat="server" ImageUrl="img/descarga_excel.jpg" Class="img-descarga" OnClick="Descargareportexls" /></a>
                            </td>
                            <td rowspan="2">Descarga base de datos de todos
                                <br />
                                los indicadores de Ramo 33
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>

            <br />

            <div class="col-center">
                <div id="divBotonesDic" runat="server" class="divBotones">
                    <asp:HiddenField runat="server" ID="HdnRutaHistoricoAproInd" />
                    <div class="col-sm-5"></div>
                    <div class="col-sm-6">
                        <div class="col-sm-6">
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12">
                    <div class="row">
                        <div class="col-xs-2"></div>
                        <div class="col-xs-8">
                            <div class="row">
                                <asp:Repeater ID="rprCiclos" runat="server" DataSourceID="odsCiclos">
                                    <ItemTemplate>
                                        <div class="col-xs-3" style="padding: 5px;">
                                            <a href="HomeRamo33.aspx?pCIclo=<%#Eval("CICLO_VALUE")+valorT%>" class="btn-mosaico <%#Eval("CICLO_VALUE").ToString()==iCiclo.ToString()?" btn-mosaico-selected":"btn-mosaico-general"%>"><%#Eval("CICLO_VALUE")%></a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:ObjectDataSource ID="odsCiclos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarCiclosRamo33">
                    <SelectParameters>
                        <%--<asp:QueryStringParameter Name="sPantalla" QueryStringField="t" DefaultValue="A" />--%>
                    </SelectParameters>
                </asp:ObjectDataSource>

            </div>

        </div>

        <br />

        <table class="tablaMosaico">
            <asp:Repeater ID="rprFondos" runat="server">
                <ItemTemplate>
                    <%# Container.ItemIndex%3 == 0 ? (Container.ItemIndex == 0 ?"":"</tr><tr>"):""%>
                    <td class="tdMosaico">
                        <a href="<%#Eval("Url")%>">
                            <img src='<%#Eval("Icono")%>' alt="Responsive image" onerror="this.src='https://www.coneval.org.mx/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'"
                                class="imagenMosaico growimagenMosaico" />
                        </a>
                    </td>

                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div id="NotaPie" visible="false" runat="server" style="font-family: arial, sans-serif; font-size:10pt; text-align:justify; margin:25px auto 0 auto;">Nota: la información se actualizará durante el mes de agosto considerando la publicación definitiva del avance de los indicadores de acuerdo con el artículo 26, fracción XII del Presupuesto de Egresos de la Federación 2021 y el artículo 107 de la Ley Federal de Presupuesto y Responsabilidad Hacendaria. </div>
    </div>
</asp:Content>

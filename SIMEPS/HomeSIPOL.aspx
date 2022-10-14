<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomeSIPOL.aspx.cs" Inherits="SIMEPS.HomeSIPOL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphTituloHeader" runat="server">
    <p class="lblTitulo">
        <asp:Label runat="server" ID="lblTitulo">Módulo de indicadores de la política social</asp:Label>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="row">
            <div class="col-md-12">
                <h1 id="tituloHomeSIPOL">Planeación Nacional</h1>
            </div>
            <div class="col-md-1"></div>
            <div class="textHomeSipol col-md-10">
                <asp:Label ID="TextoParrafo1" runat="server">La Planeación Nacional del Desarrollo representa el ordenamiento racional y 
                         sistemático de las acciones de gobierno con el propósito de </asp:Label><asp:Label ID="Label1" CssClass="isColorGreen" runat="server">fomentar</asp:Label>
                <asp:Label ID="Label2" runat="server"> el </asp:Label><asp:Label ID="Label3" CssClass="isColorGreen" runat="server">desarrollo social </asp:Label><asp:Label ID="Label4" runat="server">y </asp:Label><asp:Label ID="Label5" CssClass="isColorGreen" runat="server">económico</asp:Label>
                <asp:Label ID="Label29" runat="server"> del país en el </asp:Label><asp:Label ID="Label6" CssClass="isColorGreen" runat="server">largo plazo</asp:Label><asp:Label ID="Label7" runat="server">. Su documento principal consiste en el </asp:Label><asp:Label ID="Label8" CssClass="isColorGreen" runat="server">Plan Nacional de Desarrollo</asp:Label>
                <asp:Label ID="Label9" runat="server"> (PND), que contiene los</asp:Label><asp:Label ID="Label10" CssClass="isColorGreen" runat="server"> objetivos</asp:Label><asp:Label ID="Label11" runat="server">, </asp:Label><asp:Label ID="Label12" CssClass="isColorGreen" runat="server">estrategias </asp:Label><asp:Label ID="Label13" runat="server">y </asp:Label><asp:Label ID="Label14" CssClass="isColorGreen" runat="server">líneas </asp:Label>
                <asp:Label ID="Label15" runat="server">de </asp:Label><asp:Label ID="Label16" CssClass="isColorGreen" runat="server">acción</asp:Label>
                <asp:Label ID="Label17" runat="server"> que rigen la actuación del gobierno federal para dar cumplimiento a las metas nacionales de desarrollo.</asp:Label>
                <br />
                <br />
                <asp:Label ID="TextoParrafo2" runat="server">Del PND se desprenden los </asp:Label>
                <asp:Label ID="Label18" CssClass="isColorGreen" runat="server">programas sectoriales</asp:Label><asp:Label ID="Label19" runat="server">, </asp:Label>
                <asp:Label ID="Label20" CssClass="isColorGreen" runat="server">especiales</asp:Label><asp:Label ID="Label21" runat="server">,</asp:Label>
                <asp:Label ID="Label22" CssClass="isColorGreen" runat="server">regionales</asp:Label><asp:Label ID="Label23" runat="server">,</asp:Label>
                <asp:Label ID="Label24" CssClass="isColorGreen" runat="server">institucionales </asp:Label><asp:Label ID="Label25" runat="server">y</asp:Label>
                <asp:Label ID="Label26" CssClass="isColorGreen" runat="server">transversales</asp:Label><asp:Label ID="Label27" runat="server">,</asp:Label>
                <asp:Label ID="Label28" runat="server">que dirigen las estrategias de las dependencias y entidades de la Administración Pública Federal (APF) al logro de los objetivos
                         de prioridad nacional.</asp:Label>
                <br />
                <br />
                <asp:Label ID="TextoParrafo3" runat="server">Todos los programas cuentan con indicadores para evaluar si el objetivo del programa se está  cumpliendo: </asp:Label>
                <br />
                <asp:Label ID="TextoParrafo4" CssClass="isBold" runat="server">¡Consúltalos!</asp:Label>
            </div>
            <div class="col-md-1"></div>
        </div>
        <div class="row">
            
            <div class="col-md-3"></div>

            <div class="col-md-4">

                <table class="tableHomeSIPOL">
                    <tr>
                        <th id="td-titulo ">2013 - 2018
                        </th>
                    </tr>
                    <tr>
                        <td id="td-linkPND">
                            <asp:ImageButton
                                ImageUrl="~/img/BotonesMetasNacionales/boton_pnd.jpg"
                                OnClick="ModuloMetasNacionales_Clic"
                                CssClass="img-responsive"
                                ID="BtnPlanNacionalDesarrolloPND"
                                Width="248"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td id="td-linkSIPOL">
                            <asp:ImageButton
                                ImageUrl="~/img/BotonesMetasNacionales/boton_pasd_pnd.jpg"
                                OnClick="ModuloSIPOL_Clic"
                                CssClass="img-responsive"
                                ID="btnProgramaAmbitoSocialPND"
                                Width="248"
                                runat="server" />
                        </td>
                    </tr>
                </table>
                 
            </div>

            <div class="col-md-4">

                <table class="tableHomeSIPOL">
                    <tr>
                        <th id="td-titulo ">2019 - 2024
                        </th>
                    </tr>
                    <tr>
                        <td id="td-linkSIPOL">
                            <asp:ImageButton
                                ImageUrl="~/img/BotonesMetasNacionales/boton_pasd_pnd.jpg"
                                OnClick="ModuloSIPOL4T_Clic"
                                CssClass="img-responsive"
                                ID="btnProgramaAmbitoSocialPND4T"
                                Width="248"
                                runat="server" />
                        </td>
                    </tr>
                </table>

            </div>

        </div>



    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SIMEPS.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphTituloHeader" runat="server">
    <p class="lblTitulo">
        <asp:Label runat="server" ID="lblTitulo">El Sistema de Monitoreo de la Política Social da seguimiento a los indicadores de los programas y políticas sociales para conocer el alcance de sus objetivos</asp:Label>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">



        <div class="row">
            <div class="col-md-12">
                <h1 id="tituloHome">¿Para qué sirve el Sistema de Monitoreo de la Política Social?</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-10 textoHome">
                <ul>
                    <li><span>Para advertir a los hacedores de política pública sobre el grado de avance, el logro de los objetivos planteados y el uso de los recursos asignados.</span></li>
                    <li><span>Para conocer de manera histórica el desempeño de los programas, fondos y políticas del ámbito social.</span></li>
                    <li><span>Para detectar áreas de oportunidad en las cuales es necesario ajustar, mejorar y corregir la ejecución de un programa o fondo o política pública.</span></li>
                    <li><span>Para que los tomadores de decisiones y la ciudadanía en general cuenten con información acerca de los indicadores, metas y resultados de los programas y acciones sociales, fondos federales del ámbito social y de las políticas sociales.</span></li>
                    <li><span>Para contribuir a la transparencia y rendición de cuentas.</span></li>
                </ul>
            </div>
            <div class="col-md-1"></div>
        </div>
        <br />
        <br />
        <div class="row text-center margin-home-icon">

            <div class="col-xs- 12 col-sm-12 col-md-6 col-lg-6">
                <div class=" col-sm-12 col-md-12 menuMosaicos">
                    <asp:ImageButton runat="server" ID="SIPS" ImageUrl="img/Iconos_home/moduloindicadores_blue.png" OnClick="SIPS_Click"></asp:ImageButton>
                </div>
            </div>

            <div class="col-xs- 12 col-sm-12 col-md-6 col-lg-6">
                <div class=" col-sm-12 col-md-12 menuMosaicos">
                    <asp:ImageButton runat="server" ID="SIPOL" ImageUrl="img/Iconos_home/moduloindicadores_green.png" OnClick="SIPOL_Click"></asp:ImageButton>
                </div>
            </div>

        </div>

        <div class="row text-center margin-home-icon">

            <div class="col-md-3"></div>
            <div class="col-xs- 12 col-sm-12 col-md-6 col-lg-6">
                <div class=" col-sm-12 col-md-12 menuMosaicos">
                    <asp:ImageButton runat="server" ID="RAMO33" ImageUrl="~/img/Iconos_home/moduloindicadorramo33.jpg" OnClick="RAMO33_Click"></asp:ImageButton>
                </div>
            </div>
            <div class="col-md-3"></div>

        </div>


    </div>
    <script>
        $(document).ready(function () {
            $('#SiteMapPathSIMEPS').css("display", "none");
        });
    </script>
</asp:Content>

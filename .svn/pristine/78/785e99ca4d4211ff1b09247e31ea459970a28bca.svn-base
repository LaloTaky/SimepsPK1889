<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MosaicoRamo33.aspx.cs" Inherits="SIMEPS.MosaicoRamo33" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTituloHeader" runat="server">
    <p class="lblTitulo">
        <asp:Label runat="server" ID="lblTitulo">Módulo de indicadores de Ramo 33 del ámbito social</asp:Label>
    </p>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="main">

        <h3 class="PlecaEncabezadoR33">

            <%--<asp:Label ID="LblFondoSiglas" runat="server" CssClass="pull-left"></asp:Label>--%>

            <asp:Label ID="LblFondoNombre" runat="server" CssClass="pull-left"></asp:Label>
            <br />
            <asp:HiddenField ID="numComponentes" Value="0" runat="server" />
        </h3>

        <table class="tablaMosaico">
            <asp:Repeater ID="rprMosaico" runat="server">
                <ItemTemplate>
                    <%# Container.ItemIndex%2 == 0 ? (Container.ItemIndex == 0 ?"":"</tr><tr>"):""%>
                    <td class="tdMosaicoR33" colspan='<%# Container.ItemIndex%2 == 0 &&  Container.ItemIndex == int.Parse( numComponentes.Value)-1 ? 2:0  %>'>
                        <a class="btn" href="<%#Eval("Url")%>">
                            <img src='<%#Eval("Icono")%>' alt="Responsive image"
                                onerror="this.src='https://www.coneval.org.mx/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'" class="widthImagen" />
                        </a>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
        </table>




    </div>
</asp:Content>

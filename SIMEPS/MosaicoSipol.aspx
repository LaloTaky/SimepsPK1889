<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MosaicoSipol.aspx.cs" Inherits="SIMEPS.Mosaico1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="borderedFrame row">
        <div class="col-sm-8">
            <h2 class="TituloMosaico">
                <span id="headerContent">Sistema de Indicadores de la Política Social</span>
                <br />
                <span class="subMosaico">Indicadores derivados del PND y de Fin</span>
            </h2>
        </div>
        <div class="col-sm-4">
            <div id="divBotones">
                <input id="LinkDescargaBaseDatos" type="submit" class="btn btn-primary botonDescarga-In text-center" value="Base de Datos de los Indicadores de los Programas del Ámbito Social Derivados del PND 2013-2018" onclick='fjsDetenerEvento(event); fjsDescargaBaseDatos(this,"xls");'/>
            </div>
        </div>
        <asp:UpdatePanel ID="upnMosaicos" runat="server">
            <ContentTemplate>
                <br />
                <table class="tablaMosaico">
                    <asp:Repeater ID="rprMosaicos" runat="server" DataSourceID="odsMosaicos">
                        <ItemTemplate>
                            <%# Container.ItemIndex%5 == 0 ? (Container.ItemIndex == 0 ?"":"</tr><tr>"):""%>
                            <td class="tdMosaicoSipol" onmouseover="this.className='tdMosaicoHover';" onmouseout="this.className='tdMosaicoSipol';">
                                <a href="<%#Eval("LIGA")%>">
                                <img src='<%#Eval("URL_ICONO") %>' alt="Responsive image" onerror="this.src='/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'" class="imagenMosaico">
                                </a>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div style="text-align: right;">
            <asp:Button runat="server" ID="btnInicio" Text="Inicio" CssClass="botonInicio" OnClick="btnInicio_Click" />
        </div>
        <asp:ObjectDataSource ID="odsMosaicos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarProgramaSectoriales">
            <SelectParameters>
                <asp:QueryStringParameter Name="idProgramaSectorial" QueryStringField="id" DefaultValue="-1" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

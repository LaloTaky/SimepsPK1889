<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProgramaFin.aspx.cs" Inherits="SIMEPS.ProgramaFin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphTituloHeader" runat="server"> 
   <p class="lblTitulo"><asp:Label runat="server" ID="LabeleTituloPrin">Módulo de indicadores de la política social</asp:Label></p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div id="divHead" runat="server" class="headerProgramaFin TituloFin" style="text-align: left">
        <label>Indicadores de Fin de los programas y acciones sociales, <asp:Label ID="LblCiclo" runat="server" Text=" "></asp:Label></label>
        <br />
        <br />
        <asp:Label ID="LblDependencia" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <br />
    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
        <div id="accordion borde">
            <div class="card navbar navbar-default" role="navigation">
                <div class="card-header" id="headingOne">
                    <a href="#" class="btn btn-default btn-programa" style="width: 100%;" data-toggle="collapse" data-target="#proposito_" aria-expanded="true" aria-controls="proposito_" onclick="fjschangerow(this)">
                        <asp:Label ID="Label1" runat="server" Text="Programas" CssClass="titulo-niveles"></asp:Label>
                        <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                    </a>
                </div>
                <div id="proposito_" class="collapse in" aria-labelledby="headingOne" data-parent="#accordion">
                    <asp:Repeater ID="rprMosaicos" runat="server" DataSourceID="odsMosaicos">
                        <ItemTemplate>
                            <div class="rowPemir">
                                <div class="rowDescObjetivo">
                                    <div class="card-body">
                                        <a href="#numObj1_<%#Eval("ID_MATRIZ") %>" data-toggle="collapse" style="width: 100%;" class="btn btn-default btn-programa" onclick="fjschangerow(this)">
                                            <asp:Label ID="lblDescNivelProp" runat="server" Text='<%#Eval("NOMBRE") %>' CssClass="titulo-niveles soloprograma"></asp:Label>
                                            <span class="glyphicon glyphicon-chevron-down" style="float: right;"></span>
                                            
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div id="numObj1_<%#Eval("ID_MATRIZ") %>" class="panel-collapse collapse in">
                                <asp:Repeater ID="rprIndProp" runat="server" DataSource='<%#Eval("INDICADORES") %>'>
                                    <ItemTemplate>
                                        <div class="rowPemir">
                                            <div class="col-lg-12">
                                                <a href='<%#Eval("LIGA")+"&pCiclo="+Request.Params["pCiclo"] +"&pRamo="+Request.Params["pRamo"] +"&pSiglas="+Request.Params["pSiglas"]%>' style="width: 100%; margin-bottom: 15px;" class="btn btn-default btn-objetivo" onclick="fjschangerow(this)">
                                                    <asp:Label ID="lblNomIndProp" runat="server" Text='<%#Eval("NOMBRE_IND") %>' CssClass="titulo-niveles"></asp:Label>
                                                </a>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsMosaicos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarProgramasFin">
        <SelectParameters>
            <asp:QueryStringParameter QueryStringField="pCiclo" Name="pCiclo"/>
            <asp:QueryStringParameter QueryStringField="pRamo" Name="pRamo"/>
            <asp:QueryStringParameter QueryStringField="pUnidad" Name="pUnidad" DefaultValue="0"/>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

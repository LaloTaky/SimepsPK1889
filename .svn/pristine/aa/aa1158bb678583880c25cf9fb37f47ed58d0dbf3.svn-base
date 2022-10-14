<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="MetasNacionales.aspx.cs"
    Inherits="SIMEPS.MetasNacionales" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphTituloHeader" runat="server">
    <p class="lblTitulo">
        <asp:Label runat="server" ID="LabeleTituloPrin">Plan Nacional de Desarrollo 2013 - 2018<br />Metas Nacionales</asp:Label>
    </p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="row">
                    <div class="text-left col-md-4 col-sm-offset-9">
                        <div class="col-md-2">
                            <asp:ImageButton ImageUrl="img/descarga_excel.jpg" runat="server" ID="ImgIndExcel"
                                OnCommand="DescargaArchivoIndicadores" Text="EXCEL" CssClass="img-descarga" />
                        </div>
                        <div class="col-md-6">
                            Base de datos de los indicadores derivados del PND 2013-2018
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- Botones Metas --%>
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="row">
                    <ul class="col-md-12">
                        <li class="col-xs-4 col-md-2 col-md-offset-1 list-unstyled ">
                            <asp:ImageButton runat="server" CssClass="img-responsive"
                                ID="BotonMeta1"
                                OnCommand="BotonMetaClick"
                                CommandArgument="Meta1" />
                        </li>
                        <li class="col-xs-4 col-md-2 list-unstyled ">
                            <asp:ImageButton runat="server" CssClass="img-responsive"
                                ID="BotonMeta2"
                                OnCommand="BotonMetaClick"
                                CommandArgument="Meta2" />
                        </li>
                        <li class="col-xs-4 col-md-2 list-unstyled ">
                            <asp:ImageButton runat="server" CssClass="img-responsive"
                                ID="BotonMeta3"
                                OnCommand="BotonMetaClick"
                                CommandArgument="Meta3" />
                        </li>
                        <li class="col-xs-4 col-md-2 list-unstyled ">
                            <asp:ImageButton runat="server" CssClass="img-responsive"
                                ID="BotonMeta4"
                                OnCommand="BotonMetaClick"
                                CommandArgument="Meta4" />
                        </li>
                        <li class="col-xs-4 col-md-2 list-unstyled ">
                            <asp:ImageButton runat="server" CssClass="img-responsive"
                                ID="BotonMeta5"
                                OnCommand="BotonMetaClick"
                                CommandArgument="Meta5" />
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <%--  Botón de Estrategias transversales  --%>
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="container">
                    <div class="col-md-10 col-md-offset-1 ">
                        <asp:ImageButton runat="server" CssClass="img-responsive"
                            ID="BotonEstrategiasTransversales"
                            OnCommand="BotonEstrategiasTransversalesClick"
                            CommandArgument="EstrategiasTransversales" />
                    </div>
                </div>
            </div>
        </div>

        <%-- Lista de Indicadores Transversales --%>
        <div class="row" runat="server" id="panelListIndicadores" visible="false">
            <div class="col-md-12 col-lg-12">
                <div class="row text-center">
                    <label runat="server" class="subtitulos" id="tituloTrans"></label>
                </div>
                <asp:Repeater ID="RepeaterIndicadoresTrans" runat="server">
                    <ItemTemplate>
                        <div class="panel">
                            <div class="headerComponente">
                                <asp:LinkButton
                                    Text='<%# DataBinder.Eval(Container.DataItem,"NOMBRE") %>'
                                    runat="server"
                                    CssClass="indicadoresClass"
                                    OnCommand="ObtenerDetalleIndicador_Click"
                                    CommandArgument='<%# string.Concat(DataBinder.Eval(Container.DataItem, "NOMBRE"),"|", DataBinder.Eval(Container.DataItem, "ID_INDICADOR_ESTR_TRANS"))%>' />

                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <!-- end parent repeater -->
            </div>
        </div>

        <%-- Lista de Objetivos --%>
        <div class="row" runat="server" id="panelListObjetivos" visible="false">
            <div class="col-md-12 col-lg-12">
                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                    <!-- start parent repeater -->
                    <div class="row text-center">
                        <div class="contenedorSubtituloObjetivo">
                            <label runat="server" class="subtitulosObjetivos" id="tituloObjetivos"></label>
                        </div>
                    </div>
                    <asp:Repeater ID="parentRepeater" runat="server">
                        <ItemTemplate>
                            <div class="panel">
                                <div class="objetivosClass headerComponente text-center objetivoListado" role="tab" id="metaObjetivo">
                                    <div class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" aria-expanded="false"
                                        aria-controls="collapseThree" data-target='#collapseItem<%#Eval("ID_OBJETIVO_M") %>'>
                                        <%# DataBinder.Eval(Container.DataItem,"DESC_OBJETIVO") %>
                                    </div>
                                </div>
                                <div class="panel-collapse collapse text-center" id='collapseItem<%#Eval("ID_OBJETIVO_M") %>'>
                                    <!-- start child repeater -->
                                    <asp:Repeater ID="childRepeater" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("myrelation") %>'
                                        runat="server">
                                        <ItemTemplate>
                                            <div class="panel">
                                                <div class="headerComponente objetivoIndicadorInfo">
                                                    <asp:Label
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "[\"NOMBRE\"]") %>'
                                                        runat="server"
                                                        CssClass="indicadorNoInfo"
                                                        Visible='<%#Int32.Parse(DataBinder.Eval(Container.DataItem, "[\"ID_INDICADOR_PND\"]").ToString()) == -1? true:false  %>' />
                                                    <asp:LinkButton
                                                        Visible='<%#Int32.Parse(DataBinder.Eval(Container.DataItem, "[\"ID_INDICADOR_PND\"]").ToString()) != -1? true:false  %>'
                                                        Text='<%# DataBinder.Eval(Container.DataItem,"[\"NOMBRE\"]") %>'
                                                        runat="server"
                                                        CssClass="indicadoresClass"
                                                        OnCommand="ObtenerDetalleObjetivoIndicador_Click"
                                                        CommandArgument='<%# string.Concat(DataBinder.Eval(Container.DataItem, "[\"ID_OBJETIVO_M\"]"),"|", DataBinder.Eval(Container.DataItem, "[\"NOMBRE\"]"), "|", DataBinder.Eval(Container.DataItem, "[\"ID_INDICADOR_PND\"]"), "|", DataBinder.Eval(Container.DataItem, "[\"UNIDAD_MEDIDA\"]"))%>' />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <!-- end child repeater -->
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

        <div class="row" id="panelDetalleIndicadores" runat="server" visible="false">
            <div class="col-md-12 col-lg-12">
                <div class="row text-center">
                    <label runat="server" class="subtitulos" id="Label1"></label>
                </div>
                <asp:Repeater ID="PanelRepeaterIndicadoresTrans" runat="server">
                    <ItemTemplate>
                        <div id="PanelDetalleIndicadorTrans<%# DataBinder.Eval(Container.DataItem, "ID_INDICADOR_ESTR_TRANS")%>">
                            <style type="text/css">
                                .ovalo {
                                    width: 150px;
                                    height: 50px;
                                    padding-top: 10px;
                                    border-radius: 50%;
                                    line-height: 1em;
                                    border: 3px solid RGB(0,176,80);
                                    text-align: center;
                                }
                            </style>
                            <div class="row col-md-12">
                                <div class="col-md-6">
                                    <div class="row textAjustado">
                                        <asp:Label ID="Label2" CssClass="isColorGreen" runat="server">Indicador:</asp:Label>
                                        <%# DataBinder.Eval(Container.DataItem, "NOMBRE")%>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label7" CssClass="isColorGreen" runat="server">Definición del Indicador:</asp:Label>
                                        <br />
                                        <%# DataBinder.Eval(Container.DataItem, "DEFINICION")%>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label8" CssClass="isColorGreen" runat="server">Unidad de medida:</asp:Label>
                                        <%# DataBinder.Eval(Container.DataItem, "UNIDAD_MEDIDA")%>
                                        <br />
                                        <br />

                                        <div class="row col-md-12">
                                            <%--Calidad del indicador--%>
                                            <asp:Label ID="Label9" CssClass="isColorGreen" runat="server">Calificación del Indicador:</asp:Label>
                                            <div class="imgCalidad">
                                                <asp:Image runat="server" ImageUrl='<%# Eval("CALIF_CLARIDAD") !=null ?((bool)Eval("CALIF_CLARIDAD") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"):"img/iconoindicador_02.jpg" %>' />
                                                <label>Claridad</label>
                                            </div>
                                            <div class="imgCalidad">
                                                <asp:Image runat="server" ImageUrl='<%# (bool)Eval("CALIF_RELEVANCIA") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                                <label>Relevancia</label>
                                            </div>
                                            <div class="imgCalidad">
                                                <asp:Image runat="server" ImageUrl='<%# (bool)Eval("CALIF_MONITOREABILIDAD") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                                <label>Monitoreabilidad</label>
                                            </div>
                                            <div class="imgCalidad">
                                                <asp:Image runat="server" ImageUrl='<%# (bool)Eval("CALIF_PERTINENCIA") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                                <label>Pertinencia</label>
                                            </div>
                                            <br />
                                        </div>
                                        <br />
                                        <br />
                                    </div>
                                    <div class="row">
                                    </div>
                                    <div class="row">
                                        <br />
                                        <asp:Label ID="Label15" CssClass="isColorGreen" runat="server">Calidad:</asp:Label>
                                        <br />
                                        <br />
                                        <div class="col-md-4">
                                            <div>
                                                <%# DataBinder.Eval(Container.DataItem, "CATEGORIA_CALIDAD")%>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="row">
                                        <asp:Label ID="Label4" CssClass="isColorGreen" runat="server">Enfoque del Indicador:</asp:Label>
                                        <%# DataBinder.Eval(Container.DataItem, "ENFOQUE_INDICADOR")%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div id="divGrafIcaDetalleIndicadorTrans<%# DataBinder.Eval(Container.DataItem, "ID_INDICADOR_ESTR_TRANS")%>" style="width: 100%; height: 400px; margin: 5px -6px 20px;">
                                    </div>
                                    <%--<p>
                                        "La información publicada fue verificada por las dependencias y entidades responsables de los programas."
                                    </p>
                                    <p>
                                        Nota: La información fue actualizada en octubre de 2018.
                                    </p>--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:Button Text="Regresar" CssClass="btn btn-secondary" runat="server" OnClick="VisualizarIndicadorTrans_Click" />
                                    <p></p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <!-- end parent repeater -->
            </div>
        </div>

        <div class="row" id="panelDetalleObjetivos" runat="server">
            <div class="row text-center">
                <label runat="server" class="subtitulos" id="Label6"></label>
            </div>
            <div class="col-md-12 col-lg-12">
                <!-- start child repeater -->
                <asp:Repeater ID="PanelRepeaterDetalleObjetivoIndicador" runat="server">
                    <ItemTemplate>
                        <asp:Repeater ID="childRepeater" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("myrelation") %>' runat="server">
                            <ItemTemplate>
                                <style type="text/css">
                                    .ovalo {
                                        width: 150px;
                                        height: 50px;
                                        padding-top: 10px;
                                        border-radius: 50%;
                                        line-height: 1em;
                                        border: 3px solid RGB(0,176,80);
                                        text-align: center;
                                    }
                                </style>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="textAjustado" style="word-wrap: break-word;">
                                            <asp:Label ID="Label3" CssClass="isColorGreen" runat="server">Objetivo:</asp:Label>
                                            <%# DataBinder.Eval( ((RepeaterItem)Container.Parent.Parent).DataItem,"DESC_OBJETIVO") %>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label2" CssClass="isColorGreen" runat="server">Indicador:</asp:Label>
                                            <%# DataBinder.Eval(Container.DataItem, "[\"NOMBRE\"]")%>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label7" CssClass="isColorGreen" runat="server">Definición del Indicador:</asp:Label>
                                            <br />
                                            <%# DataBinder.Eval(Container.DataItem, "[\"DEFINICION\"]")%>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label8" CssClass="isColorGreen" runat="server">Unidad de medida:</asp:Label>
                                            <%# DataBinder.Eval(Container.DataItem, "[\"UNIDAD_MEDIDA\"]")%>
                                            <br />
                                            <br />

                                            <div class="row col-md-12">
                                                <%--Calidad del indicador--%>
                                                <asp:Label ID="Label10" CssClass="isColorGreen" runat="server">Calificación del Indicador:</asp:Label>
                                                <div class="imgCalidad">
                                                    <asp:Image runat="server" ImageUrl='<%# Eval("[\"CALIF_CLARIDAD\"]") !=null ?((bool)Eval("[\"CALIF_CLARIDAD\"]") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"):"img/iconoindicador_02.jpg" %>' />
                                                    <label>Claridad</label>
                                                </div>
                                                <div class="imgCalidad">
                                                    <asp:Image runat="server" ImageUrl='<%# (bool)Eval("[\"CALIF_RELEVANCIA\"]") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                                    <label>Relevancia</label>
                                                </div>
                                                <div class="imgCalidad">
                                                    <asp:Image runat="server" ImageUrl='<%# (bool)Eval("[\"CALIF_MONITOREABILIDAD\"]") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                                    <label>Monitoreabilidad</label>
                                                </div>
                                                <div class="imgCalidad">
                                                    <asp:Image runat="server" ImageUrl='<%# (bool)Eval("[\"CALIF_PERTINENCIA\"]") ? "img/iconoindicador_02.jpg":"img/iconoindicador_01.jpg"%>' />
                                                    <label>Pertinencia</label>
                                                </div>
                                                <br />
                                            </div>
                                            <br />
                                            <br />
                                        </div>
                                        <div class="row">
                                        </div>
                                        <div class="">
                                            <br />
                                            <asp:Label ID="Label5" CssClass="isColorGreen" runat="server">Calidad:</asp:Label>
                                            <br />
                                            <br />
                                            <div class="col-md-4">
                                                <div>
                                                    <%# DataBinder.Eval(Container.DataItem, "[\"CATEGORIA_CALIDAD\"]")%>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <div>
                                            <asp:Label ID="Label4" CssClass="isColorGreen" runat="server">Enfoque del Indicador:</asp:Label>
                                            <%# DataBinder.Eval(Container.DataItem, "[\"ENFOQUE_INDICADOR\"]")%>
                                        </div>
                                        <div runat="server" visible='<%#string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "[\"COMENTARIO\"]").ToString()) ? false : true  %>'>
                                            <br />
                                            <div class="isColorGreen" style="width: 15%; vertical-align: text-top; display: inline-block;">Comentario:</div>
                                            <div style="width: 80%; vertical-align: text-top; display: inline-block;">
                                                <%# DataBinder.Eval(Container.DataItem, "[\"COMENTARIO\"]")%>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    <div class="col-md-6">
                                        <div id="divGrafIcaDetalleIndicador<%# DataBinder.Eval(Container.DataItem, "[\"ID_INDICADOR_PND\"]")%>" style="width: 100%; height: 400px; margin: 5px -6px 20px;">
                                        </div>
                                        <%--<p>
                                            "La información publicada fue verificada por las dependencias y entidades responsables de los programas."
                                        </p>
                                        <p>
                                            Nota: La información fue actualizada en octubre de 2018.
                                        </p>--%>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <asp:Button Text="Regresar" CssClass="btn btn-secondary" runat="server" OnClick="VisualizarObjetivoIndicador_Click" />
                                        <p></p>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
                <!-- end child repeater -->
            </div>
        </div>
    </div>

</asp:Content>


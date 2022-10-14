<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaObjetivos.aspx.cs" Inherits="SIMEPS.ListaObjetivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="borderedFrame">
      <div class="row">
         <div class="col-md-12">
            <asp:Repeater ID="rprObjetivosComp" runat="server" DataSourceID="odsMosaicos">
               <ItemTemplate>
                  <div class="row">
                     <div class="col-sm-9" >
                        <div class="row">
                           <div class="col-sm-12">
                              <br />
                              <p class="TitulosObjetivosPND">
                                 <span id="headerContent" style="font-size: 14pt;">Sistema de Indicadores de la Política Social</span>
                              </p>
                              <p class="TitulosObjetivosPND subMosaico">
                                 <span>Indicadores derivados del PND y de Fin</span>
                              </p>
                           </div>
                        </div>
                        <div class="row">
                           <div class="col-sm-12">
                              <div><br />
                                 <p class="TitulosObjetivosPND" style="color: #00a94f; font-family: arial;"><%#Eval("NOMBRE") %></p>
                              </div>
                              <hr style="border-bottom: 1px solid #d0cfcf;" />
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-3">
                        <div class="row">
                           <br />
                           <p class="text-center">
                              <a href="MosaicoSipol.aspx">
                                 <img src='<%#Eval("URL_ICONO") %>' alt="Responsive image" onerror="this.src='/SiteCollectionImages/Paemir/LogosDependencias/imgNoExist.png'" class="img-responsive" style="min-width: 170px; min-height: 140px; display: inline">
                              </a>
                           </p>
                        </div>
                     </div>
                  </div>
               </ItemTemplate>
            </asp:Repeater>
         </div>
      </div>
      <div class="row">
         <div class="col-md-12">
            <div class="panel-group" id="accordion1">
               <div class="panel panel-default">
                  <div class="panel-heading panel-heading-ico panel-heading-noneLink BorderPanels" style="padding-bottom: 0px; padding-top: 0px;">
                     <div class="row" style="background-color: #013360;">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-11" style="padding: 0px; border-left: solid; border-left-color: white;">
                           <h4 class="panel-title"><a class="collapsed" data-toggle="collapse" data-parent="#accordion1" href="#collapseTwo" style="padding: 20px;"><strong>Indicadores derivados del Plan Nacional de Desarrollo</strong></a></h4>
                        </div>
                     </div>
                  </div>
                  <div id="collapseTwo" class="panel-collapse collapse">
                     <div class="panel-body-white">
                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Objetivos" OnItemCreated="Repeater1_ItemCreated">
                           <ItemTemplate>
                              <asp:TextBox runat="server" ID="txtObjetivo" style="display:none" Text='<%# DataBinder.Eval(Container.DataItem, "OBJETIVO")%>' ></asp:TextBox>
                              <asp:TextBox runat="server" ID="txtNumObjetivo" style="display:none" Text='<%# DataBinder.Eval(Container.DataItem, "NUM_OBJETIVO")%>' ></asp:TextBox>
                              <div class="panel-group" id="accordion2">
                                 <div class="panel panel-default">
                                    <div class="panel-heading panel-heading-ico panel-heading-noneLink BorderPanels" style="padding-bottom: 0px; padding-top: 0px;">
                                       <div id="IndPnd" runat="server" class="row">
                                          <div class="col-md-1" style="text-align: center; padding: 12px; color: #ffffff">
                                             <strong>
                                                <asp:Label runat="server" ID="lblConsecutivo">
                                                   <%#Eval("NUM_OBJETIVO")%>
                                                </asp:Label>
                                             </strong>
                                          </div>
                                          <div class="col-md-11" style="padding: 0px; border-left: solid; border-left-color: white;">
                                             <h4 class="panel-title">
                                                <div><a class="collapsed" data-toggle="collapse" data-parent="#accordion2" href="#<%#Eval("NUM_OBJETIVO")%>" style="padding: 12px;"><%#Eval("OBJETIVO")%></a></div>
                                             </h4>
                                          </div>
                                       </div>
                                    </div>

                                    <div id="<%#Eval("NUM_OBJETIVO")%>" class="panel-collapse collapse">
                                       <asp:Repeater ID="Repeater2" runat="server" DataSourceID="Indicadores" OnItemCreated="Repeater2_ItemCreated" >
                                          <ItemTemplate>
                                             <div class="panel-body BorderPanels" style="margin-bottom: 10px; margin-top: 10px; padding-bottom: 0px; padding-top: 0px;" runat="server">
                                                <div class="row">                                             
                                                   <div class="col-md-1" style="text-align: center; padding: 12px;" runat="server">
                                                       strong>
                                                   <asp:Label runat="server" ID="lblTIPO">
                                                                                 <%#Eval("TIPO_INDICADOR")%>
                                                   </asp:Label>
                                                </strong>
                                                   </div>
                                                   <div class="col-md-11" style="padding: 15px; border-left: groove; border-left-color: white;">
                                                      <a href="<%#Eval("LIGA")+"&nSectorial="+ HttpContext.Current.Request["nSectorial"]%>"><strong><%#Eval("INDICADOR")%></strong></a>
                                                   </div>
                                                </div>
                                             </div>
                                          </ItemTemplate>
                                       </asp:Repeater>
                                       <asp:ObjectDataSource ID="Indicadores" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarIndicadoresSectoriales4T">
                                          <SelectParameters>
                                             <asp:QueryStringParameter Name="id" QueryStringField="id" DefaultValue="-1" />
                                             <asp:Parameter Name="opcion" DefaultValue="2" />
                                             <asp:ControlParameter ControlID="txtObjetivo" Name="objetivo" PropertyName="Text" />
                                             <asp:ControlParameter ControlID="txtNumObjetivo" Name="numobjetivo" PropertyName="Text" />
                                          </SelectParameters>
                                       </asp:ObjectDataSource>
                                    </div>
                                 </div>
                              </div>
                           </ItemTemplate>
                        </asp:Repeater>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <div class="row">
         <div class="col-md-12">
            <div class="panel-group" runat="server" id="accordion3">
               <div class="panel panel-default">
                  <div class="panel-heading panel-heading-ico panel-heading-noneLink BorderPanels" style="padding-bottom: 0px; padding-top: 0px;">
                     <div class="row" style="background-color: #013360;">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-11" style="padding: 0px; border-left: solid; border-left-color: white;">
                           <h4 class="panel-title"><a class="collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapseTwo1" style="padding: 20px;"><strong>Indicadores de Fin</strong></a></h4>
                        </div>
                     </div>
                  </div>
                  <div id="collapseTwo1" class="panel-collapse collapse">
                     <div class="panel-body-white">
                        <asp:Repeater ID="Repeater3" runat="server" DataSourceID="IndicadoresFIN" OnItemCreated="Repeater3_ItemCreated">
                           <ItemTemplate>
                              <div class="panel-group" id="accordion4">
                                 <div class="panel panel-default">
                                    <div class="panel-heading BorderPanels" style="padding-bottom: 0px; padding-top: 0px;">
                                       <div id="IndFin" runat="server" class="row">
                                          <div class="col-md-1" style="text-align: center; padding: 12px; color: #ffffff">
                                             <strong><%#Eval("NUM_INDICADOR") %></strong>
                                          </div>
                                          <div class="col-md-11" style="padding: 0px; border-left: solid; border-left-color: white;">
                                             <h4 class="panel-title"><a href="<%#Eval("LIGA")%>" style="padding: 12px;"><%#Eval("INDICADOR")%></a></h4>
                                          </div>
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </ItemTemplate>
                        </asp:Repeater>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
      <div class="row">
         <div class="col-md-12">
            <asp:Repeater ID="RepeaterEvaluacion" runat="server" DataSourceID="odsMosaicos">
               <ItemTemplate>
                  <div class="row" >
                     <div class="col-sm-2">
                        <div class="row" style='<%# (Eval("EVALUACIONLLIGA") != null)?"":"display:none;" %>'>
                           <br />
                           <p class="text-center" >

                              <a href='<%#Eval("EVALUACIONLLIGA") %>' >
                      
                                       <img src='<%#Eval("EVALUACIONIMAGEN") %>' class="img-responsive portada-evaluacion" >
                        
                              </a>                          
                           </p>
                        </div>
                     </div>
                     <div class="col-sm-5" >
                        <br />
                        <br />
                        <div class="row">
                           <div class="col-sm-12">
                              <div>
                                 <br />
                                 <a href='<%#Eval("EVALUACIONLLIGA") %>'>
                                    <p class="textoIndicadores" style="font-size:large" ><%#Eval("EVALUACIONDESC") %></p>
                                 </a>
                              </div>
                           </div>
                        </div>
                     </div>
                     <div class="col-sm-5"></div>
                  </div>
               </ItemTemplate>
            </asp:Repeater>
         </div>
      </div>
      <br />
      <asp:ObjectDataSource ID="odsMosaicos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarProgramaSectoriales4T">
         <SelectParameters>
            <asp:QueryStringParameter Name="idProgramaSectorial" QueryStringField="id" DefaultValue="0" />
         </SelectParameters>
      </asp:ObjectDataSource>

      <asp:ObjectDataSource ID="Objetivos" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarObjetivosSectoriales4T">
         <SelectParameters>
            <asp:QueryStringParameter Name="idProgramaSectorial" QueryStringField="id" DefaultValue="-1" />
         </SelectParameters>
      </asp:ObjectDataSource>

      <asp:ObjectDataSource ID="IndicadoresFIN" runat="server" TypeName="SIMEPS.Dal.IndicadoresDal" SelectMethod="ConsultarIndicadorFin">
         <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" DefaultValue="-1" />
            <asp:Parameter Name="opcion" DefaultValue="3" />
         </SelectParameters>
      </asp:ObjectDataSource>
   </div>
</asp:Content>

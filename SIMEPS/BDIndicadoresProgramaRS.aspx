﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BDIndicadoresProgramaRS.aspx.cs" Inherits="SIMEPS.BDIndicadoresProgramaRS" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    					
							<rsweb:ReportViewer 
							ID="ReportViewer1" 
							runat="server" 
								Height="130px"
                                Width="100%"
							ProcessingMode="Remote" 
							ShowParameterPrompts="false"
							CssClass="reporteHistorico"            
							>
						</rsweb:ReportViewer>
</asp:Content>
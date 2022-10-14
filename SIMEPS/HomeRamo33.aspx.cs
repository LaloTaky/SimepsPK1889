﻿using SIMEPS.Dal;
using SIMEPS.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SIMEPS.Comun;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;



namespace SIMEPS
{
    public partial class HomeRamo33 : System.Web.UI.Page
    {
        public String valorT = "";
        int iFila = 2;
        int iFilaEstatal = 2;
        int iFilaMunicipal = 2;
        int In = 2, letra;
        int iColumna = 1, columnaFinal = 27;
        public int iCiclo = DateTime.Now.Year;
        FondoRamo33[] fondos = null;
        Workbook wb = null;
        IndicadoresDal indicadores = new IndicadoresDal();

        ExcelUtilities comun = new ExcelUtilities();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            IndicadoresDal simeps = new IndicadoresDal();


            if (Request.Params["pCiclo"] != null)
            {
                //odsMosaicos.SelectParameters["pCiclo"].DefaultValue = Request.Params["pCiclo"].ToString();
                iCiclo = Convert.ToInt16(Request.Params["pCiclo"].ToString());
                if (iCiclo == 2020 || iCiclo == 2021)
                    NotaPie.Visible = true;
            }
            else
            {
                Response.Redirect(string.Format("HomeRamo33.aspx?pCiclo={0}", DateTime.Now.Year));
            }

            this.fondos = simeps.ConsultaFondosRamo33PorCiclo(iCiclo);
            rprFondos.DataSource = this.fondos;
            rprFondos.DataBind();

            lblSubtitulo.Text = "¿Qué es el Ramo 33?";
            lblCuerpo8.Text = "¡Consulta los indicadores del Ramo 33!";
        }

        public void Descargareportexls(object sender, ImageClickEventArgs e)
        {
            int Ciclo = Int32.Parse(Request.Params["pCiclo"]);
            string filepath = @"C:\Exceles\"+ Request.Params["pCiclo"] + "_IndicadoresRamo33.xlsx";

            //Creamos el Archivo
            SpreadsheetDocument workbook = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);

            //Creamos El Woorkbook
            WorkbookPart workbookPart = workbook.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            //Creamos La Hoja de Trabajo
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            WorksheetPart worksheetPart2 = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart2.Worksheet = new Worksheet(new SheetData());

            WorksheetPart worksheetPart3 = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart3.Worksheet = new Worksheet(new SheetData());

            //agregar la lista de Hojas
            Sheets sheets = workbook.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

         

            //Agregamos la nueva hoja de trabajo y se asocia con el libro de trabajo.
            Sheet sheet = new Sheet()
            {
                Id = workbook.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Federal"
            };

            Sheet sheetEstatal = new Sheet()
            {
                Id = workbook.WorkbookPart.GetIdOfPart(worksheetPart2),
                SheetId = 2,
                Name = "Estatal"
            };

            Sheet sheetMunicipal = new Sheet()
            {
                Id = workbook.WorkbookPart.GetIdOfPart(worksheetPart3),
                SheetId = 3,
                Name = "Municipal"
            };

            sheets.Append(sheet);
            sheets.Append(sheetEstatal);
            sheets.Append(sheetMunicipal);

            //Cerramos y guardamos la hojda de calculo

            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();

            Worksheet worksheet2 = worksheetPart2.Worksheet;
            SheetData sheetData2 = worksheet2.GetFirstChild<SheetData>();

            Worksheet worksheet3 = worksheetPart3.Worksheet;
            SheetData sheetData3 = worksheet3.GetFirstChild<SheetData>();

            llenarDatosFederal(sheetData, Ciclo);
            llenarDatosEstatal(sheetData2, Ciclo);
            llenarDatosMunicipal(sheetData3, Ciclo);

            worksheetPart.Worksheet.Save();
            worksheetPart2.Worksheet.Save();
            worksheetPart3.Worksheet.Save();

            workbook.Close();

        }


       
        public void llenarDatosFederal(SheetData sheet, int Ciclo)
        {
            List<Ramo33BDFederal> ListaBDFondoRamo33Federal = new List<Ramo33BDFederal>();
            ListaBDFondoRamo33Federal = indicadores.GetBaseDeDatosFondosIndicadoresRamo33Federal(Ciclo);
            System.Data.DataTable tabladedatos = comun.ToDataTable(ListaBDFondoRamo33Federal);
            comun.cargaTabla(sheet, tabladedatos);
        }

        public void llenarDatosEstatal(SheetData sheet, int Ciclo)
        {
            List<Ramo33BDEstatal> ListaBDFondoRamo33Estatal = new List<Ramo33BDEstatal>();
            ListaBDFondoRamo33Estatal = indicadores.GetBaseDeDatosFondosIndicadoresRamo33Estatal(Ciclo);
            System.Data.DataTable tabladedatos = comun.ToDataTable(ListaBDFondoRamo33Estatal);
            comun.cargaTabla(sheet, tabladedatos);
        }

        public void llenarDatosMunicipal(SheetData sheet, int Ciclo)
        {
            List<Ramo33BDMunicipal> ListaBDFondoRamo33Municipal = new List<Ramo33BDMunicipal>();
            ListaBDFondoRamo33Municipal = indicadores.GetBaseDeDatosFondosIndicadoresRamo33Municipal(Ciclo);
            System.Data.DataTable tabladedatos = comun.ToDataTable(ListaBDFondoRamo33Municipal);
            comun.cargaTabla(sheet, tabladedatos);
        }



       

    }
}
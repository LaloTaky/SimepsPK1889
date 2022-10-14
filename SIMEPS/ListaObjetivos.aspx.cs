using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SIMEPS.Dal;
using SIMEPS.Modelo;
using MoreLinq;

namespace SIMEPS
{
   public partial class ListaObjetivos : System.Web.UI.Page
   {
      public int NUMRepDe = 0;
      public int NUMRepFin = 0;
      public string valorDer = "";
      public string valorFin = "";
      public int valorSub = 0;
      public string color = "";
      public int id = 0;
      int total = 0;
      object[] arr = new object[5];
      int j = 0;
      int i = 0;
      IndicadoresDal indicadorDAL = new IndicadoresDal();
      List<SIMEPS.Modelo.IndicadorSectorial> Lista = new List<SIMEPS.Modelo.IndicadorSectorial>();

      protected void Page_Load(object sender, EventArgs e)
      {
         int id = 0;
         List<Modelo.IndicadorSectorial> Lista = new List<Modelo.IndicadorSectorial>();
         if (Request.Params["id"] != null)
         {
            id = Convert.ToInt16(Request.Params["id"].ToString());
            Lista = indicadorDAL.ConsultarIndicadorFin(id, 3);
         }
         if (Lista.Count == 0)
            accordion3.Visible = false;
         arr[0] = "#39508A";
         arr[1] = "#63AC20";
         arr[2] = "#0F92B1";
         arr[3] = "#686F76";
         arr[4] = "#657EC0";
         
         

         
      }

      protected void Repeater1_ItemCreated(object sender, RepeaterItemEventArgs e)
      {
         NUMRepDe = NUMRepDe + 1;
         valorDer = Convert.ToString(NUMRepDe);

         HtmlGenericControl IndPnd = e.Item.FindControl("IndPnd") as HtmlGenericControl;
         if (i > 4)
         {
            i = 0;
            IndPnd.Style.Add("background-color", arr[i].ToString());
            i = i + 1;
         }
         else
         {
            IndPnd.Style.Add("background-color", arr[i].ToString());
            i = i + 1;
         }

      }

      protected void Repeater2_ItemCreated(object sender, RepeaterItemEventArgs e)
      {
         valorSub = NUMRepDe;
      }

      protected void Repeater3_ItemCreated(object sender, RepeaterItemEventArgs e)
      {
         NUMRepFin = NUMRepFin + 1;
         valorFin = Convert.ToString(NUMRepFin);

         HtmlGenericControl IndFin = e.Item.FindControl("IndFin") as HtmlGenericControl;
         if (j > 4)
         {
            j = 0;
            IndFin.Style.Add("background-color", arr[j].ToString());
            j = j + 1;
         }
         else
         {
            IndFin.Style.Add("background-color", arr[j].ToString());
            j = j + 1;
         }
      }
   }
}
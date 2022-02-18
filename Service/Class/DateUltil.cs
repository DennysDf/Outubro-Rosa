using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


public static class DateUltil
{

    public static string ToDateFull(this string Date)
    {
        var meses = new string[] {"Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
        var dateArr =  Date.Split("/");
        var mes = Int32.Parse(dateArr[1]);
        var dateFull = $"{dateArr[0]} de {meses[mes-1]} de {dateArr[2]}";
        return dateFull;
   
    }

    public static string ToDateMin(this string Date)
    {
        var meses = new string[] { "Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };             
        var dateArr = Date.Split("/");
        var mes = Int32.Parse(dateArr[1]);
        var dateFull = $"{dateArr[0]} de {meses[mes - 1]} de {dateArr[2]}";
        return dateFull;
   
    }

    public static DateTime ParseDateTime(this string Date)
    {
        if (Date != "")
        {

            var dataArr = Date.Split("/");
            var mes =  Int32.Parse(dataArr[1]);
            var mesS = mes < 10 ? "0" + mes : mes.ToString();
            var data = $"{dataArr[1]}/{mesS}/{dataArr[2]}";

            var dutchCultureInfo = CultureInfo.CreateSpecificCulture("pt-BR");
            var date1 = DateTime.ParseExact(Date, "dd/MM/yyyy", dutchCultureInfo);
            

            return date1;
        }
        else
        {
            return DateTime.Now;
        }

    }

}

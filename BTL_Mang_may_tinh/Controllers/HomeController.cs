using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hangfire.Common;
using Hangfire.Server;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
namespace BTL_Mang_may_tinh.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
        public ActionResult Weather1()
        {
            string url = "https://forecast.weather.gov/MapClick.php?lat=37.7772&lon=-122.4168&fbclid=IwAR0OJ_GC81HD6uHRF9QJT_E2m4ARbyUQW2seYKSmlCdzHr0xYhfKxExOF94#.XrWOF2gzZPb";
            GetFullInformation(url);
            return View();
        }
        public ActionResult Weather2()
        {
            string url = "https://forecast.weather.gov/MapClick.php?lon=-122.11138932779431&lat=37.39241840723042#.Xra7iWgzZPY";
            GetFullInformation(url);
            return View();
        }
        public ActionResult Weather3()
        {
            string url = "https://forecast.weather.gov/MapClick.php?lon=-122.2803039718419&lat=37.80001858110637#.Xra8eGgzZPY";
            GetFullInformation(url);
            
            return View();
        }
        public string GetFullInformation(string url)
        {
            
            var web = new HtmlWeb();
            var doc = web.Load(url);

            HtmlNode panel_heading = doc.DocumentNode.SelectSingleNode("//div[@class='panel-heading']");
            ViewBag.panel_heading = panel_heading.InnerHtml;
            HtmlNode panel_title = doc.DocumentNode.SelectSingleNode("//h2[@class='panel-title']");
            ViewBag.panel_title = panel_title.InnerText;
            HtmlNode forecast_current = doc.DocumentNode.SelectSingleNode("//div[@id='current_conditions-summary']");
            ViewBag.summary = forecast_current.InnerHtml;
          

            HtmlNodeCollection tombstone = doc.DocumentNode.SelectNodes("//div[@class='tombstone-container']");
            string tomb0 = tombstone[0].InnerHtml;
            tomb0=tomb0.Insert(tomb0.IndexOf("img")+4,"style='display:none;'");
            ViewBag.tomb0 = tomb0;
            string tomb1 = tombstone[1].InnerHtml;
            tomb1 = tomb1.Insert(tomb1.IndexOf("img") + 4, "style='display:none;'");
            ViewBag.tomb1 = tomb1;
            string tomb2 = tombstone[2].InnerHtml;
            tomb2 = tomb2.Insert(tomb2.IndexOf("img") + 4, "style='display:none;'");
            ViewBag.tomb2 = tomb2;
            string tomb3 = tombstone[3].InnerHtml;
            tomb3 = tomb3.Insert(tomb3.IndexOf("img") + 4, "style='display:none;'");
            ViewBag.tomb3 = tomb3;
            string tomb4 = tombstone[4].InnerHtml;
            tomb4 = tomb4.Insert(tomb4.IndexOf("img") + 4, "style='display:none;'");
            ViewBag.tomb4 = tomb4;
            string tomb5 = tombstone[5].InnerHtml;
            tomb5 = tomb5.Insert(tomb5.IndexOf("img") + 4, "style='display:none;'");
            ViewBag.tomb5 = tomb5;
            string tomb6 = tombstone[6].InnerHtml;
            tomb6 = tomb6.Insert(tomb6.IndexOf("img") + 4, "style='display:none;'");
            ViewBag.tomb6 = tomb6;
            string tomb7 = tombstone[7].InnerHtml;
            tomb7 = tomb7.Insert(tomb7.IndexOf("img") + 4, "style='display:none;'");
            ViewBag.tomb7 = tomb7;

            HtmlNodeCollection details_day = doc.DocumentNode.SelectNodes("//div[@class='col-sm-2 forecast-label']");
            ViewBag.Day0 = details_day[0].InnerText;
            ViewBag.Day1 = details_day[1].InnerText;
            ViewBag.Day2 = details_day[2].InnerText;
            ViewBag.Day3 = details_day[3].InnerText;
            ViewBag.Day4 = details_day[4].InnerText;
            ViewBag.Day5 = details_day[5].InnerText;

            HtmlNodeCollection details = doc.DocumentNode.SelectNodes("//div[@class='col-sm-10 forecast-text']");
            ViewBag.details0 = details[0].InnerText;
            ViewBag.details1 = details[0].InnerText;
            ViewBag.details2 = details[0].InnerText;
            ViewBag.details3 = details[0].InnerText;
            ViewBag.details4 = details[0].InnerText;
            ViewBag.details5 = details[0].InnerText;

            HtmlNode current_details = doc.DocumentNode.SelectSingleNode("//div[@id='current_conditions_detail']");
            ViewBag.current_details = current_details.InnerHtml;

            HtmlNodeCollection desc = doc.DocumentNode.SelectNodes("//p[@class='short-desc']");
            ViewBag.desc0 = CheckWeather(desc[0].InnerText) + ".svg";
            ViewBag.desc1 = CheckWeather(desc[1].InnerText) + ".svg";
            ViewBag.desc2 = CheckWeather(desc[2].InnerText) + ".svg";
            ViewBag.desc3 = CheckWeather(desc[3].InnerText) + ".svg";
            ViewBag.desc4 = CheckWeather(desc[4].InnerText) + ".svg";
            ViewBag.desc5 = CheckWeather(desc[5].InnerText) + ".svg";
            ViewBag.desc6 = CheckWeather(desc[6].InnerText) + ".svg";
            return JsonConvert.SerializeObject("Load success");
        }
        public int CheckWeather(string input)
        {
            if(input.Contains("Mostly Sunny") == true)
            {
                return 2;
            }
            else if (input.Contains("Partly Sunny") == true)
            {
                return 3;
            }
            
            if (input.Contains("Clouds") == true || input.Contains("Partly Cloudy") == true)
            {
                return 5;
            }
            else if (input.Contains("Mostly Cloudy") == true)
            {
                return 6;
            }
            if (input.Contains("Chance") == true && input.Contains("Showers") == true)
            {
                return 8;
            }
            else if(input.Contains("Showers") == true)
            {
                return 9;
            }
            if (input.Contains("Chance") == true && input.Contains("Rain") == true && input.Contains("Partly") == true && input.Contains("Sunny") == true)
            {
                return 4;
            }
            else if(input.Contains("Chance") == true && input.Contains("Rain") == true)
            {
                return 9;
            }
            if(input.Contains("Gradual") == true && input.Contains("Clearing") == true)
            {
                return 1;
            }
            if (input.Contains("Sunny") == true)
            {
                return 1;
            }
            return 0;
        }
    }
}
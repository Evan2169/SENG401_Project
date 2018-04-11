using ClientApplicationMVC.Models;

using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;

using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Text;
using System.Collections.Generic;
using Messages.ServiceBusRequest.Weather.Requests;
using Messages.DataTypes.Database.Weather;
using Messages.ServiceBusRequest.Weather.Responses;

namespace ClientApplicationMVC.Controllers
{
    /// <summary>
    /// This class contains the functions responsible for handling requests routed to *Hostname*/CompanyListings/*
    /// </summary>
    public class CompanyListingsController : Controller
    {
        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings
        /// </summary>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult Index()
        {
            if (Globals.isLoggedIn())
            {
                ViewBag.Companylist = null;
                return View("Index");
            }
            return RedirectToAction("Index", "Authentication");
        }

        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings/Search
        /// </summary>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult Search(string textCompanyName)
        {

            if (Globals.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if(connection == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            CompanySearchRequest request = new CompanySearchRequest(textCompanyName);

            CompanySearchResponse response = connection.getCompanies(request);
            if (response.result == false)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ViewBag.Companylist = response.list;

            return View("Index");
        }

        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings/DisplayCompany/*info*
        /// </summary>
        /// <param name="id">The name of the company whos info is to be displayed</param>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult DisplayCompany(string id)
        {
            if (Globals.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Authentication");
            }
            if ("".Equals(id))
            {
                return View("Index");
            }

            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if (connection == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ViewBag.CompanyName = id;
            GetCompanyInfoRequest infoRequest = new GetCompanyInfoRequest(new CompanyInstance(id));
            GetCompanyInfoResponse infoResponse = connection.getCompanyInfo(infoRequest);

            if (infoResponse.result)
            {
                ViewBag.CheckReviews = true;
                ViewBag.CompanyInfo = infoResponse.companyInfo;
                if (infoResponse.companyInfo.reviewList.reviews == null)
                {
                    ViewBag.CheckReviews = false;
                }
                else
                {
                    List<ReviewInstance> r = infoResponse.companyInfo.reviewList.reviews;
                    string[] timestamp_readable = new string[r.Count];
                    
                    for (int i = 0; i < timestamp_readable.Length; i++)
                    {
                        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(Convert.ToInt64(r[i].timestamp)).ToLocalTime();
                        timestamp_readable[i] = Convert.ToString(dtDateTime);
                    }
                    ViewBag.Timestamp = timestamp_readable;
                }
            }

            //Still assume location array is of one value.
            GetWeatherRequest weatherRequest = new GetWeatherRequest(new CompanyWeather { location = infoResponse.companyInfo.locations[0] });
            GetWeatherResponse weatherResponse = connection.getWeather(weatherRequest);
            if (!weatherResponse.result)
                ViewBag.success = false;
            else
            {
                ViewBag.success = true;
                ViewBag.realFeel = weatherResponse.companyWeather.realFeelTemperature;
                ViewBag.temp = weatherResponse.companyWeather.temperature;
                ViewBag.weatherText = weatherResponse.companyWeather.weatherText;
            }

            return View("DisplayCompany");
        }

        public ActionResult SaveReview(string textUserReview, string rating, string companyName)
        {
            if (Globals.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Authentication");
            }
            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if (connection == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ReviewInstance review = new ReviewInstance(companyName, textUserReview , Convert.ToInt32(rating),
                DateTimeOffset.Now.ToUnixTimeSeconds(), Globals.getUser());
            SaveCompanyReviewRequest request = new SaveCompanyReviewRequest(review);
            SaveCompanyReviewResponse response = connection.saveCompanyReview(request);

            return RedirectToAction("DisplayCompany", new { id = companyName });
        }
    }
}
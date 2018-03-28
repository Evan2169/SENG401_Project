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
                        //TODO
                        timestamp_readable[i] = new DateTime(Convert.ToInt64(r[i].timestamp)).ToString("MM-dd-yyyy-HH:mm:ss");
                    }
                    ViewBag.Timestamp = timestamp_readable;
                }
            }
            return View("DisplayCompany");
        }

        public ActionResult SaveReview(string textUserReview, string rating, string companyName)
        {
            ReviewInstance review = new ReviewInstance(companyName, textUserReview , Convert.ToInt32(rating),
                DateTimeOffset.Now.ToUnixTimeSeconds(), Globals.getUser());
            var review_JSON = new JavaScriptSerializer().Serialize(review);
            var client = new HttpClient();
            var content = new StringContent(review_JSON.ToString(), Encoding.UTF8, "application/json");
            var result = client.PostAsync("http://localhost:50151/DBLS/SaveCompanyReview/", content).Result;
            return RedirectToAction("DisplayCompany", new { id = companyName });
        }
    }
}
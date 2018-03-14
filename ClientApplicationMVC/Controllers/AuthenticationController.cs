using ClientApplicationMVC.Models;

using Messages.NServiceBus.Commands;
using Messages.DataTypes;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Authentication.Requests;

using System.Web.Mvc;

namespace ClientApplicationMVC.Controllers
{
    /// <summary>
    /// This class contains the functions responsible for handling requests routed to *Hostname*/Authentication/*
    /// </summary>
    public class AuthenticationController : Controller
    {
        /// <summary>
        /// The default method for this controller
        /// </summary>
        /// <returns>The login page</returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Please enter your username and password.";
            return View("Index");
        }

        public ActionResult Submit(string un, string pw)
        {
            System.Diagnostics.Debug.Print("MESSAGE:got here");
            LogInRequest loginReq = new LogInRequest(un, pw);
            //ServiceBusConnection connection = new ServiceBusConnection(un);
            //ServiceBusResponse response = connection.sendLogIn(loginReq);
            ServiceBusResponse response = ConnectionManager.sendLogIn(loginReq);
            if (!response.result)
            {
                ViewBag.response = "Login Unsuccessful!\n" + response.response;
            }
            else
            {
                ViewBag.response = "Successfully logged in!\n";
            }
            return View("Index");
        }
        public ActionResult Register()
        {
            CreateAccount newAcc = new CreateAccount();
            //newAcc.username = username
            //newAcc.password = password
            //newAcc.address = address
            //newAcc.phonenumber = phonenumber
            //newAcc.email = email
            //newAcc.type = type
            CreateAccountRequest CAR = new CreateAccountRequest(newAcc);
            ServiceBusResponse response = ConnectionManager.sendNewAccountInfo(CAR);

            if (!response.result)
            {
                ViewBag.response = "Error in creating account. Login Unsuccessful!\n" + response.response;
            }
            else
            {
                ViewBag.response = "New account successfully created. Successfully logged in!\n";
            }


            return View("Index");
        }
		//This class is incomplete and should be completed by the students in milestone 2
		//Hint: You will need to make use of the ServiceBusConnection class. See EchoController.cs for an example.
    }
}
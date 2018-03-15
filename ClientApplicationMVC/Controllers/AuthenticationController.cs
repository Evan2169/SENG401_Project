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
            if (Globals.isLoggedIn() == false)
            {
                ViewBag.Message = "Please enter your username and password.";
                return View("Index");
            }
            else
            {
                return View("LoggedIn");
            }
        }

        public ActionResult LogOut()
        {
            ConnectionManager.getConnectionObject(Globals.getUser()).close();
            Globals.setUser("Log In");
            return View("Index");
        }

        public ActionResult Submit(string un, string pw)
        {           
            LogInRequest loginReq = new LogInRequest(un, pw);
            
            ServiceBusResponse response = ConnectionManager.sendLogIn(loginReq);
            if (!response.result)
            {
                ViewBag.response = "Login Unsuccessful!\n" + response.response;
            }
            else
            {
                ViewBag.response = "Successfully logged in!\n";
                return View("LoggedIn");
            }
            return View("Index");
        }
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }
        public ActionResult Register(string proposedUsername, string proposedPassword, 
            string proposedAddress, string proposedPhoneNumber, string proposedEmail, AccountType proposedType)
        {
            CreateAccount newAcc = new CreateAccount();
            newAcc.username = proposedUsername;
            newAcc.password = proposedPassword;
            newAcc.address = proposedAddress;
            newAcc.phonenumber = proposedPhoneNumber;
            newAcc.email = proposedEmail;
            newAcc.type = proposedType;
            CreateAccountRequest CAR = new CreateAccountRequest(newAcc);
            ServiceBusResponse response = ConnectionManager.sendNewAccountInfo(CAR);

            if (!response.result)
            {
                ViewBag.createaccountresponse = "Error in creating account.\n" + response.response;
            }
            else
            {
                ViewBag.createaccountresponse = "New account successfully created. Successfully logged in!\n";
            }

            return View("CreateAccount");
        }
		//This class is incomplete and should be completed by the students in milestone 2
		//Hint: You will need to make use of the ServiceBusConnection class. See EchoController.cs for an example.
    }
}
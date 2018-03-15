﻿// To be completed by students in milestone 2

//This function is the preliminary username/password checker before the username and password are sent to the server to log in.
function validate() {
    var un = loginForm.un.value;
    var pw = loginForm.pw.value;
    if (un.trim() === "" || pw === "" || un.length > 15 || pw.length > 15 || un.length < 2 || pw.length < 2) {
        alert("Invalid username or password: Both must not be empty and between 2 and 15 characters long");
        return false;
    }
    else if (un.match(/^\d/)) {
        alert("Invalid username: Username cannot start with a number");
        return false;
    }
    else if (un.match(/[^a-zA-Z0-9]/)) {
        alert("Invalid username: Username can only be alphanumeric");
        return false;
    }
    return true;
}

function registerUser()
{
    var user = registerForm.proposedUsername.value;
    var pass = registerForm.proposedPassword.value;
    var address = registerForm.proposedAddress.value;
    var phone = registerForm.proposedPhoneNumber.value;
    var email = registerForm.proposedEmail.value;
    var type = registerForm.proposedType.value;
    if (user === "" || pass === "" || address === "" || phone === "" || email === "") {
        alert("One or more fields are missing!");
        return false;
    } else if (user.length < 2 || un.length > 15) {
        alert("Please enter a valid username, between 2 and 15 characters long.");
        return false;
    } else if (pass.length < 6 || pw.length > 15) {
        alert("Please enter a valid password, between 6 and 15 characters long.");
        return false;
    } else if (address.length < 2 || address.length > 25) {
        alert("Please enter a valid address, between 2 and 25 characters long.");
        return false;
    } else if (phone.length < 7 || phone.length > 11) {
        alert("Please enter a valid phone number, between 7 and 11 digits.");
        return false;
    } else if (email.length < 2 || !email.includes("@") || email.length > 25) {
        alert("Please enter a valid e-mail, between 2 and 25 characters");
        return false;
    } 
    return true;
    //If we get to here, allow the user to register an account in the database.
}
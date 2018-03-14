// To be completed by students in milestone 2

//This function is the preliminary username/password checker before the username and password are sent to the server to log in.
function validate() {
    var un = loginForm.un.value;
    var pw = loginForm.pw.value;
    if (un.trim() === "" || pw === "" || un.length > 50 || pw.length > 50) {
        alert("Invalid username or password");
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
    var un = registerForm.proposedUsername.value;
    var pw = registerForm.proposedPassword.value;
    if (un === "" || pw === "") {
        alert("You must enter a valid username or password");
    } else if (un.length < 2 || un.length > 15) {
        alert("Please enter a valid username, between 2 and 15 characters long.");
    } else if (pw.length < 6 || pw.length > 15) {
        alert("Please enter a valid password, between 6 and 15 characters long.");
    }
    //If we get to here, allow the user to register an account in the database.
}
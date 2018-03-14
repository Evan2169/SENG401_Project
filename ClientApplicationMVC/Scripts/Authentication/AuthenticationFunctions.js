// To be completed by students in milestone 2

//This function is the preliminary username/password checker before the username and password are sent to the server to log in.
function validate()
{
    var un = document.getElementById("un");
    var pw = document.getElementById("pw");
    if (un === "" || pw === "") {
        return false;
    }
 
    return true;
}

function registerUser()
{
    var un = document.getElementById("proposedUsername");
    var pw = document.getElementById("proposedPassword");
    if (un === "" || pw === "") {
        alert("You must enter a valid username or password");
    } else if (un.length < 2 || un.length > 15) {
        alert("Please enter a valid username, between 2 and 15 characters long.")
    } else if (pw.length < 6 || pw.length > 15) {
        alert("Please enter a valid password, between 6 and 15 characters long.")
    }
    //If we get to here, allow the user to register an account in the database.
}
// To be completed by students in milestone 2

//This function is the preliminary username/password checker before the username and password are sent to the server to log in.
function validate()
{
    var un = document.login.un.value;
    var pw = document.login.pw.value;
    if (un === "" || pw === "") {
        return false;
    }
    return true;
}

function registerUser()
{
   // var un = document.getElementById.usernam
}
// To be completed by students in milestone 2

function validate() {
    var un = document.login.un.value;
    var pw = document.login.pw.value;
    if (un === "" || pw === "") {
        return false;
    }
    return true;
}
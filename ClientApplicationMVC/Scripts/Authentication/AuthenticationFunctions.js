// To be completed by students in milestone 2

function validate() {
    var un = document.login.un.value;
    var pw = document.login.pw.value;
    if (un === "") {
        return false;
    }
    else if (Form.password.value === "") {
        return false;
    }
    return true;
}
function login() {
    let username = document.getElementById('username').value
    let password = document.getElementById('password').value
    let url = '/User/Login'
    let data = JSON.stringify({
        username: username,
        pwd: password
    })
    post(url, data, (resData) => {
        if (resData.message == "not authenticated") {
            window.location = '/Book/Index'
        }
        if (resData.message) {
            alert("wrong password or username")
        }
        let user = resData.user
        let userRole = resData.userRole
        var now = new Date();
        var time = now.getTime();
        time += 3600 * 1000;
        now.setTime(time);
        document.cookie = "username=" + user + ";" + "userRole=" + userRole.toString() + ";" + 'expires=' + now.toUTCString() + ';'
        window.location = '/Book/Index'
    })
}
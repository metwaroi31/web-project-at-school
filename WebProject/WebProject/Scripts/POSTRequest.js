function post(url, data, callback) {
    const request = new XMLHttpRequest()
    request.open('POST', url)
    request.setRequestHeader('Content-Type', 'application/json')
    request.onreadystatechange = function () {
        if (request.status == 200) {
            let data = JSON.parse(this.responseText)

            callback(data)
        }
    }
    request.send(data)
}
function error(xhr, status, err) {
    alert(xhr);
    alert("readyState: " + xhr.readyState
                + "\nstatus: " + xhr.status
                + "\nstatusText : " + xhr.statusText
                + "\nresponseText: " + xhr.responseText
                + "\nresponseXML: " + xhr.responseXML
                + "\nerror: " + err);
}

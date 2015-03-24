http://www.dotnet-tricks.com/Tutorial/wcf/X8QN260412-Calling-Cross-Domain-WCF-Service-using-Jquery.html


$.ajax({
    url: 'http://192.168.5.140:8182/Device',
    type: 'POST',
    data: {
        RegistrationId: "ShoaibDevice1"
	},
    contentType: 'application/json; charset=utf-8',
    success: function (data) {
        alert(data.success);
    },
    error: function () {
        alert("error");
    }
});


var postData={};
postData.RegistrationId='ShoaibDevice1';
$.ajax({
    type: 'POST',
    url: 'http://192.168.5.140:8182/Device',
    data: JSON.stringify(postData),
    success: function(data) { alert('data: ' + data); },
     contentType: "application/json;charset-uf8",
    dataType: 'json'
});



$.ajax({
    url: 'http://192.168.5.140:8082/Device',
    type: 'POST',
    dataType: 'json',
    contentType: "application/json; charset=utf-8",
    data: {
        RegistrationId: "ShoaibDevice1"
	},
    success: function (data) {
        alert(data);
    },
    error: function () {
        alert("error");
    }
});

var url = "http://192.168.5.140:8082/Device";
$.post(url, { RegistrationId: "ShoaibDevice1" }, function (data) {
	alert(JSON.stringify(data));
});



var url = "http://192.168.5.140:8181/DeviceRegister.svc/AddDevice";
$.post(url, { RegistrationId: "ShoaibDevice1" }, function (data) {
	alert(JSON.stringify(data));
});
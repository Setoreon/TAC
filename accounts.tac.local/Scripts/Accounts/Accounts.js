function login(componentid) {
    var logincontrol = jQuery("#" + componentid);
    var usernameField = logincontrol.find("#popupLoginEmail");
    var passwordField = logincontrol.find("#popupLoginPassword");
    jQuery.ajax(
        {
            url: "/api/sitecore/Accounts/_Login",
            method: "POST",
            data: {
                userName: usernameField.val(),
                password: passwordField.val()
            },
            success: function (data) {
                if (data.RedirectUrl != null && data.RedirectUrl !== undefined) {
                    window.location.assign(data.RedirectUrl);
                } else {
                    var body = logincontrol.find(".login-body");
                    var parent = body.parent();
                    body.remove();
                    parent.html(data);
                }
            },
            error: function (data) {
                console.log("error: " + data);
            }
        });
}
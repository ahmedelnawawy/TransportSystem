WebFont.load({
    google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
    active: function () {
        sessionStorage.fonts = true;
    }
});
function SetLanguageCookie(selectedLanguage) {
    var expDate = new Date();
    expDate.setDate(expDate.getDate() + 20); // Expiration 20 days from today
    document.cookie = "langCookie=" + selectedLanguage + "; expires=" + expDate.toUTCString() + "; path=/";
};
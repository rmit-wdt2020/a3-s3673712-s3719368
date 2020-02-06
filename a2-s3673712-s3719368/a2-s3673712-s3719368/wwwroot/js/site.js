// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function checkChanged(toggleSwitchId, formID) {
    var toggleSwitch = $("#" + toggleSwitchId);
    if (toggleSwitch !== undefined) {
        toggleSwitch.prop("checked", !toggleSwitch.prop("checked"));
    }
    var thisForm = $("#" + formID);
    if (thisForm !== undefined) {
        thisForm.submit();
    }
}
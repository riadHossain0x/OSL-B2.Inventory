//get file size
function GetFileSize(fileid) {
    try {
        fileSize = document.getElementById(fileid).files[0].size;
        fileSize = fileSize / 1048576;
        return fileSize;
    }
    catch (e) {
        console.log("Error is :" + e);
    }
}

//get file path from client system
function getNameFromPath(strFilepath) {
    var objRE = new RegExp(/([^\/\\]+)$/);
    var strName = objRE.exec(strFilepath);

    if (strName == null) {
        return null;
    }
    else {
        return strName[0];
    }
}

$("#btnSubmit").on("click", function () {
    if ($('#imageFile').val() == "") {
        $("#spanfile").html("The Image field is required.");
        return false;
    }
    else {
        return checkfile();
    }
});

function checkfile() {
    var file = getNameFromPath($("#imageFile").val());
    if (file != null) {
        var extension = file.substr((file.lastIndexOf('.') + 1));
        // alert(extension);
        switch (extension) {
            case 'jpg':
            case 'png':
            case 'gif':
            case 'jpeg':
                flag = true;
                break;
            default:
                flag = false;
        }
    }
    if (flag == false) {
        $("#spanfile").text("You can upload only jpg, jpeg, png, gif extension file");
        return false;
    }
    else {
        var size = GetFileSize('imageFile');
        if (size > 5) {
            $("#spanfile").text("You can upload file up to 5 MB");
            return false;
        }
        else {
            $("#spanfile").text("");
        }
    }
}

$(function () {
    $("#imageFile").change(function () {
        checkfile();
    });
});
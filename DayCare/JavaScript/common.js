
function mOver(btnid) {
    btnid.style.color = "#c00";
}

function mOut(btnid) {
    btnid.style.color = "Black";
}

function ValidateFormWithRadGrid(sender, strElements, MessageRow) {

    //    alert(MessageRow);   
    var cnt = 0;
    //alert(sender.name);  
    strElements = strElements.split(",");
    //alert(strElements);  
    for (var i = 0; i < strElements.length; i++) {
        str = strElements[i];
        //alert(str);
        // debugger;
        str = document.getElementById(str);
        //alert(str);
        //alert(str.value);   
        //alert(str.value.substring(0,2)); 
        //                        alert(str); 
        //                        alert(str!=null);                          
        //
        if (str != null) {
            //            alert(str.id);
            //            alert(str.value); 
            if (str.value == "" || str.value == "00000000-0000-0000-0000-000000000000" || str.value.substring(0, 2) == "Eg" || str.value.match(/Select/) == "Select") {
                // alert(str.value.match(/Select/));
                if (str.value.match(/Select/) == "Select") {
                    //                    alert(str.id); 
                    //                    alert(str.value); 
                    document.getElementById(str.id + "_Input").style.backgroundColor = "#FFE6A2";
                }
                //ctl00_ContentPlaceHolder1_EventInformation_userControl_RadComTimeZone_Input


                str.style.backgroundColor = "#FFE6A2";
                cnt = cnt + 1;

            }
            else {
                str.style.backgroundColor = "white";
            }
            //alert(str.id.match(/mail/)); 
            var IsMail = str.id.match(/mail/);
            //                          alert(IsMail);
            if (IsMail != null && str.value.substring(0, 2) != "Eg") {
                //alert("In Email"); 
                var IsValidEmail = mailcheck(str);
                //alert(IsValidEmail);   
                if (IsValidEmail == false) {
                    cnt = cnt + 1;

                }

            }
        }


    }


    if (cnt > 0) {

        //alert("Please fill the Highlighted fields...");

        return false;
    }
    //alert(cnt); 

    return true;

}

function ValidateForm(strElements) {

    // alert("ValidateForm"); 
    var cnt = 0;
    var str;
    strElements = strElements.split(",");
    //alert(strElements);  
    for (var i = 0; i < strElements.length; i++) {
        str = strElements[i];

        str = document.getElementById(str);

        /* str.value.match(/Select/)  condition is for RadCombos   */



        if (str != null) {
            // alert(str.value.length);
            if (str.value == "" || (str.value.length == 0) || str.value == "00000000-0000-0000-0000-000000000000" || str.value.substring(0, 2) == "Eg" || str.value.match(/Select/) == "Select") {
                //alert(str.value.match(/Select/)); 
                if (str.value.match(/Select/) == "Select") {

                    document.getElementById(str.id + "_Input").style.backgroundColor = "#FFE6A2";
                }
                //ctl00_ContentPlaceHolder1_EventInformation_userControl_RadComTimeZone_Input


                str.style.backgroundColor = "#FFE6A2";
                cnt = cnt + 1;

            }
            else {
                str.style.backgroundColor = "white";
            }
            //alert(str.id.match(/mail/)); 
            var IsMail = str.id.match(/mail/);
            //                          alert(IsMail);
            if (IsMail != null && str.value.substring(0, 2) != "Eg") {
                //alert("In Email"); 
                var IsValidEmail = mailcheck(str);
                //alert(IsValidEmail);
                if (IsValidEmail == false) {
                    str.style.backgroundColor = "#FFE6A2"
                    cnt = cnt + 1;

                }

            }
        }


    }


    if (cnt > 0) {

        //alert("Please fill the Highlighted fields...");

        return false;
    }
    //alert(cnt); 

    return true;
}

function mailcheck(obj) {
    var id = obj.id;
    var strEmail = obj.value.trim();
    if (strEmail.length > 0) {
        var filter = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
        if (!filter.test(strEmail)) {
            //alert("Please Enter Valid Email.");
            obj.focus();
            return false;
        }
        else {
            return true;
        }
    }
}
function ValidateDates(FromDate, ToDate) {
    var sErrors = "";

    if (isDate(FromDate, "dd/MM/yyyy") || (FromDate == "dd/mm/yyyy")) {
    }
    else {
        sErrors = "\n\tInvalid From Date";
    }
    if (isDate(ToDate, "dd/MM/yyyy") || (ToDate == "dd/mm/yyyy")) {
    }
    else {
        sErrors += "\n\tInvalid To Date";
    }

    return sErrors;
}
function isDate(s, f) {
    var a1 = s.split("/");
    var a2 = s.split("-");
    var e = true;
    if ((a1.length != 3) && (a2.length != 3)) {
        e = false;
    }
    else {
        if (a1.length == 3)
            var na = a1;
        if (a2.length == 3)
            var na = a2;
        if (isPositiveInteger(na[0]) && isPositiveInteger(na[1]) && isPositiveInteger(na[2])) {
            if (f == 1) {
                var d = na[1], m = na[0];
            }
            else {
                var d = na[0], m = na[1];
            }
            var y = na[2];
            if (((e) && (y < 1753) || y.length > 4))
                e = false
            if (e) {
                v = new Date(m + "/" + d + "/" + y);
                if (v.getMonth() != m - 1)
                    e = false;
            }
        }
        else {
            e = false;
        }
    }
    return e
}
function ShowMessage(Msg, ReturnMode) {

    alert(Msg);
    return ReturnMode;
}
function textonly(e) {
    var code;
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    var character = String.fromCharCode(code);
    //alert('Character was ' + character);
    //alert(code);
    //if (code == 8) return true;
    var AllowRegex = /^[\ba-zA-Z\s-]$/;
    if (AllowRegex.test(character)) return true;
    return false;
}

function gettimezone(s) {
    var d = new Date()
    var gmtHours = -d.getTimezoneOffset() / 60;
    return s;
}
//Alphanumeric with '-' and  '_' 
function alphaNumwithyphenUnderScore(input) {
    var validStr = '-_ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    validateControl(input, validStr);
}
function validateControl(input, validStr) {
    var temp;
    var tempStr = '';
    var tempFlag = true;
    var strVal = input.value;
    for (var i = 0; i < strVal.length; i++) {
        temp = strVal.substring(i, i + 1);
        if (validStr.indexOf(temp) == -1) {
            input.value = tempStr;
            input.focus();
            return false;
        }
        tempStr = tempStr + temp;


        if (tempStr.substring(0, 1) == ' ') {
            //tempStr=strVal.substring(1,strVal.length) 
            tempStr = RemoveInitialWhiteSpace(strVal);
            input.value = tempStr;
            input.focus();
            return false;
        }

    }
    return true;
}
function RemoveInitialWhiteSpace(tempStr) {
    if (tempStr.substring(0, 1) == ' ') {
        tempStr = tempStr.substring(1, tempStr.length)
        if (tempStr.substring(0, 1) == ' ') {
            tempStr = RemoveInitialWhiteSpace(tempStr)
        }
        return tempStr;
    }
}

//Check for decimal value for Single precision value
//Write this function on KeyDown event only

function IsDecimal(ctrl, Evt) {
    Evt = (Evt) ? Evt : window.event
    var Kcode = (Evt.which) ? Evt.which : Evt.keyCode

    if (Kcode == 32)//If Shift is pressed then cancel it
    {
        if (Evt.which) {
            Evt.which = 505;
        }
        else {
            Evt.keyCode = 505;
        }
        Kcode = 505;
    }

    if (Kcode >= 65 && Kcode <= 90 ||
              (Kcode == 505 || Kcode == 16 || Kcode == 189 || Kcode == 187 ||
               Kcode == 220 || Kcode == 111 || Kcode == 106 || Kcode == 109 || Kcode == 107 || Kcode == 191 ||
               Kcode == 188 || Kcode == 192)
          )
        return false;

    var str = ctrl.value;

    if (Kcode == 110 || Kcode == 190) {
        str = str + '.';
    }

    var dotcount = str.split('.');
    var cnt = dotcount.length;
    if (cnt > 2)
        return false;
}
//---------------------------------------------------------------------------------------------------
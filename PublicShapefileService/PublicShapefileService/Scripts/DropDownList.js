

$(function () {
   
    
    $("#DropDown").autocomplete({
        source: function (request, response) {
            var param = { word:$('#DropDown').val() };
            $.ajax({
                url: "http://localhost/PublicShapefileService/Download/GetLocality",
                data: { word: request.term },
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        minLength: 3,
        delay:0,
        select: function (event, ui) {
            
        },
        focus: function () {
            return false;
        }
    });


    $("#DropDownLayer").autocomplete({
        source: function (request, response) {
            var param = { keyword: $('#DropDown').val() };
            $.ajax({
                url: "http://localhost/PublicShapefileService/Download/Get2",
                data: "{'keyword':' " + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        minLength: 3,
        delay: 0,
        select: function (event, ui) {
            var LastValue = splitCurrentText(this.value);
            LastValue.pop();
            LastValue.push(ui.item.value);
            LastValue.push("");

            this.value = LastValue.join(",");
            return false;  
        },
        focus: function () {
            return false;
        }
    });
    function splitCurrentText(LastTerm) {

        return LastTerm.split(/,\s*/);
    }

    function GetCurrentSearchTerm(LastTerm) {

        return splitCurrentText(LastTerm).pop();
    }  
});
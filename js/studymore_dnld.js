var def = 'search surah...';
function ClearDefaultVal() {
    if ($('#filterTextField').val() == def) 
        $('#filterTextField').val('') ;
}
function SearchSurah()
{
    var txt = $('#filterTextField').val().toLowerCase();
    $('#Surah').empty();
    if (txt.length > 1) {
               
        var found = false;
        $.each(jsArrayNames, function (i, item) {
            if (item.toLowerCase().indexOf(txt) >= 0) {
                found = true;
                $('#Surah').append($('<option>', {
                    value: jsArrayIDs[i],
                    text: jsArrayNames[i]
                }));
            }
        });

        SetAyatDD();

    }
    else {
        $.each(jsArrayIDs, function (i, item) {
            $('#Surah').append($('<option>', {
                value: jsArrayIDs[i],
                text: jsArrayNames[i]
            }));
        });
               
    }
}
$.each(jsArrayIDs, function (i, item) {
    $('#Surah').append($('<option>', {
        value: jsArrayIDs[i],
        text: jsArrayNames[i]
    }));
});

$('#filterTextField').val(def);

function SetAyatDD() {
    var sura = $('[name=Surah]').val();
    var ddl = $("#AyahFrom");
    ddl.empty();
    for (k = 1; k <= jsArrayAyat[sura - 1]; k++)
        ddl.append("<option value='" + k + "'>" + k + "</option>");

    var ddlPage = $("#Page");
    ddlPage.empty();
    for (k = 1; k <= 604; k++)
        ddlPage.append("<option value='" + k + ".htm'>" + k + "</option>");
}
 

$('[name=Surah]').val(jSurah);
SetAyatDD();
$('[name=AyahFrom]').val(jAyahFrom);
$('[name=Page]').val(jPageNum+'.htm');

var JuzNum = 0;
var RukuNum = 0;

////---show juz markers
$(".juzmark").remove(); //clear all to avoid dups
$('.ayahNum').each(function () {
    var loc = $(this).data('ayah_all');
    loc = loc.replace('(', '');
    loc = loc.replace(')', '');
    loc = loc.replace(':', ',');
    if (JuzNum == 0) { //if a juz is found, no need to check again
        for (var i = 2; i < 31; i++) { //leave Juz 1 marker
            if (QuranData.Juz[i].toString().trim() == loc) {
                $(this).parent().prepend('<div class="juzmark">Juz (Part): ' + i + '</div>');
                JuzNum = i;
                break;
            }
        };
    }
});

$('#chkRuku').bind('change', function () {
    if ($(this).is(':checked')) {
        ShowJuzRuku();
        $('.rukumark').show();
    }
    else {
        $('.rukumark').hide();
    }

});


function ShowJuzRuku() {
    $(".rukumark").remove(); //clear all to avoid dups
    $('.ayahNum').each(function () {
        var loc = $(this).data('ayah_all');
        loc = loc.replace('(', '');
        loc = loc.replace(')', '');
        loc = loc.replace(':', ',');
        if (RukuNum == 0) {
            for (var i = 2; i < 557; i++) { // with i=2, leave ruku marker in Al-Fatiha
                if (QuranData.Ruku[i].toString().trim() == loc) {
                    $(this).parent().prepend('<span class="rukumark" title="Ruku #' + i.toString() + '">(\u0639) </span>');
                    RukuNum = i;
                    break;
                }
            };
        }
        else { //this elase block is to perf improvement. Once a ruku is found, next ruku will be within next 5 rukus
            for (var i = RukuNum; i < RukuNum + 5; i++) { // with i=2, leave ruku marker in Al-Fatiha
                if (QuranData.Ruku[i].toString().trim() == loc) {
                    $(this).parent().prepend('<span class="rukumark" title="Ruku #' + i.toString() + '">(\u0639) </span>');
                    RukuNum = i;
                    break;
                }
            };
        }

      

    });
}
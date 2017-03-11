var def = 'search surah...';
function ClearDefaultVal() {
    // alert($('#filterTextField').val());
    if ($('#filterTextField').val() == def) 
        $('#filterTextField').val('') ;
}
function SearchSurah()
{
    var txt = $('#filterTextField').val().toLowerCase();
    $('#Surah').empty();
    if (txt.length > 1) {
               
        // var found = $.inArray(txt, jsArrayNames);
        // alert(found);
        var found = false;
        $.each(jsArrayNames, function (i, item) {
            if (item.toLowerCase().indexOf(txt) >= 0) {
                found = true;
                //return false;
                //alert(i);
                $('#Surah').append($('<option>', {
                    value: jsArrayIDs[i],
                    text: jsArrayNames[i]
                }));
            }
        });

        //$('#Surah').attr("size","100"); 
        //$('#Surah').slideDown(100);
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
}

function SetJuzDD() {
    //var sura = $('[name=JuzDD]').val();
    var ddl = $("#JuzDD");
    ddl.empty();
    for (k = 1; k <= 30; k++)
        ddl.append("<option value='" + QuranData.Juz[k] + "'>" + k + "</option>");
}
$('[name=Surah]').val(jSurah);
SetAyatDD();
SetJuzDD();
$('[name=AyahFrom]').val(jAyahFrom);
$('[name=NumVerses]').val(jNumAyah);
$('[name=Page]').val(jPageNum);

function submitJuz() {
    var juzdata = $('[name=JuzDD]').val();
    var juzSura = juzdata.split(",")[0];
    var juzAya = juzdata.split(",")[1];

    $('[name=Surah]').val(juzSura);
    $('[name=AyahFrom]').val(juzAya);

    
    //alert(aa);
}
//
/****iterate thru one array
$.each(jsArrayIDs, function (index, value) {
    $('#Surah').append($('<option>', { 
        value: value,
        text : value 
    }));
});*/
//$("#NumVerses").find('option:[name="7a"]').attr("selected", true);

     
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

$(document).ready(function () {
   
    //$('#chkRuku').bind('click', function () {
    //     alert('ee');
    //    if ($(this).is(':checked')) {
    //        ShowJuzRuku();
    //        $('.rukumark').show();
           
    //    }
    //    else {
    //        $('.rukumark').hide();
    //    }

    //});
});

//$('#chkRuku').onchange = function () {
//    alert("ww");
//};

function ShowJuzRuku() {
    $(".rukumark").remove(); //clear all to avoid dups
    $('.ayahNum').each(function () {
        var loc = $(this).data('ayah_all');
        //alert(loc);
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
        else { //this else block is to perf improvement. Once a ruku is found, next ruku will be within next 5 rukus
            for (var i = RukuNum; i < RukuNum + 5; i++) { // with i=2, leave ruku marker in Al-Fatiha
                if (QuranData.Ruku[i].toString().trim() == loc) {
                    $(this).parent().prepend('<span class="rukumark" title="Ruku #' + i.toString() + '">(\u0639) </span>');
                    RukuNum = i;
                    break;
                }
            };
        }

        //if (JuzNum == 0) { //if a juz is found, no need to check again
        //    for (var i = 2; i < 31; i++) { //leave Juz 1 marker
        //        if (QuranData.Juz[i].toString().trim() == loc) {
        //            $(this).parent().prepend('<div class="juzmark">Juz (Part): ' + i + '</div>');
        //            JuzNum = i;
        //            break;
        //        }
        //    };
        //}


    });
}
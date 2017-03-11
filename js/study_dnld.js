var jsArrayIDs = new Array(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114);
var jsArrayNames = new Array("1 - Al-Faatiha", "2 - Al-Baqara", "3 - Aal-i-Imraan", "4 - An-Nisaa", "5 - Al-Maaida", "6 - Al-An'aam", "7 - Al-A'raaf", "8 - Al-Anfaal", "9 - At-Tawba", "10 - Yunus", "11 - Hud", "12 - Yusuf", "13 - Ar-Ra'd", "14 - Ibrahim", "15 - Al-Hijr", "16 - An-Nahl", "17 - Al-Israa", "18 - Al-Kahf", "19 - Maryam", "20 - Taa-Haa", "21 - Al-Anbiyaa", "22 - Al-Hajj", "23 - Al-Muminoon", "24 - An-Noor", "25 - Al-Furqaan", "26 - Ash-Shu'araa", "27 - An-Naml", "28 - Al-Qasas", "29 - Al-Ankaboot", "30 - Ar-Room", "31 - Luqman", "32 - As-Sajda", "33 - Al-Ahzaab", "34 - Saba", "35 - Faatir", "36 - Yaseen", "37 - As-Saaffaat", "38 - Saad", "39 - Az-Zumar", "40 - Al-Ghaafir", "41 - Fussilat", "42 - Ash-Shura", "43 - Az-Zukhruf", "44 - Ad-Dukhaan", "45 - Al-Jaathiya", "46 - Al-Ahqaf", "47 - Muhammad", "48 - Al-Fath", "49 - Al-Hujuraat", "50 - Qaaf", "51 - Adh-Dhaariyat", "52 - At-Tur", "53 - An-Najm", "54 - Al-Qamar", "55 - Ar-Rahmaan", "56 - Al-Waaqia", "57 - Al-Hadid", "58 - Al-Mujaadila", "59 - Al-Hashr", "60 - Al-Mumtahana", "61 - As-Saff", "62 - Al-Jumu'a", "63 - Al-Munaafiqoon", "64 - At-Taghaabun", "65 - At-Talaaq", "66 - At-Tahrim", "67 - Al-Mulk", "68 - Al-Qalam", "69 - Al-Haaqqa", "70 - Al-Ma'aarij", "71 - Nooh", "72 - Al-Jinn", "73 - Al-Muzzammil", "74 - Al-Muddaththir", "75 - Al-Qiyaama", "76 - Al-Insaan", "77 - Al-Mursalaat", "78 - An-Naba", "79 - An-Naazi'aat", "80 - Abasa", "81 - At-Takwir", "82 - Al-Infitaar", "83 - Al-Mutaffifin", "84 - Al-Inshiqaaq", "85 - Al-Burooj", "86 - At-Taariq", "87 - Al-A'laa", "88 - Al-Ghaashiya", "89 - Al-Fajr", "90 - Al-Balad", "91 - Ash-Shams", "92 - Al-Lail", "93 - Ad-Dhuhaa", "94 - Ash-Sharh", "95 - At-Tin", "96 - Al-Alaq", "97 - Al-Qadr", "98 - Al-Bayyina", "99 - Az-Zalzala", "100 - Al-Aadiyaat", "101 - Al-Qaari'a", "102 - At-Takaathur", "103 - Al-Asr", "104 - Al-Humaza", "105 - Al-Fil", "106 - Quraish", "107 - Al-Maa'un", "108 - Al-Kawthar", "109 - Al-Kaafiroon", "110 - An-Nasr", "111 - Al-Masad", "112 - Al-Ikhlaas", "113 - Al-Falaq", "114 - An-Naas");
var jsArrayAyat = new Array(7, 286, 200, 176, 120, 165, 206, 75, 129, 109, 123, 111, 43, 52, 99, 128, 111, 110, 98, 135, 112, 78, 118, 64, 77, 227, 93, 88, 69, 60, 34, 30, 73, 54, 45, 83, 182, 88, 75, 85, 54, 53, 89, 59, 37, 35, 38, 29, 18, 45, 60, 49, 62, 55, 78, 96, 29, 22, 24, 13, 14, 11, 11, 18, 12, 12, 30, 52, 52, 44, 28, 28, 20, 56, 40, 31, 50, 40, 46, 42, 29, 19, 36, 25, 22, 17, 19, 26, 30, 20, 15, 21, 11, 8, 8, 19, 5, 8, 8, 11, 11, 8, 3, 9, 5, 4, 7, 3, 6, 3, 5, 4, 5, 6);
 

$(document).ready(function () {
    $('.ayahNum').mouseover(function (event) {
        $(this).addClass('hoverme');
        positionHover(event);
        var loc = $(event.target).data('ayah_all'); //HTML5 only? test!

        loc = loc.replace('(', '');
        loc = loc.replace(')', '');
        var arrWord = loc.split(":");
        var sura = arrWord[0];
        var ayah = arrWord[1];
        var meanTrans= eval('tran' + sura + '_'+ ayah);

        $("#diagHover #meanHover").html(meanTrans);
           
        $('#diagHover').css({ 'width': '150px' });
        $('#diagHover').css({ 'height': '50px' });
        if (meanTrans.length > 40) {
            $('#diagHover').css({ 'width': '200px' });
            $('#diagHover').css({ 'height': '65px' });
        }

        if (meanTrans.length > 50) {
            $('#diagHover').css({ 'width': '200px' });
            $('#diagHover').css({ 'height': '75px' });
        }
        if (meanTrans.length > 70) {
            $('#diagHover').css({ 'width': '220px' });
            $('#diagHover').css({ 'height': '85px' });
        }

        if (meanTrans.length > 100) {
            $('#diagHover').css({ 'width': '300px' });
            $('#diagHover').css({ 'height': '100px' });
        }
        if (meanTrans.length > 200) {
            $('#diagHover').css({ 'width': '400px' });
            $('#diagHover').css({ 'height': '100px' });
        }
        if (meanTrans.length > 400) {
            $('#diagHover').css({ 'width': '400px' });
            $('#diagHover').css({ 'height': '170px' });
        }
        if (meanTrans.length > 500) {
            $('#diagHover').css({ 'width': '400px' });
            $('#diagHover').css({ 'height': '155px' });
        }
        if (meanTrans.length > 800) {
            $('#diagHover').css({ 'width': '400px' });
            $('#diagHover').css({ 'height': '300px' });
        }
        if (meanTrans.length > 800) { //only 2:282
            $('#diagHover').css({ 'width': '400px' });
            $('#diagHover').css({ 'height': '400px' });
        }
    });
    $('.ayahNum').mouseout(function () {
        $(this).removeClass('hoverme');
        $("#diagHover").css({ 'visibility': 'hidden' });

    });
    $('div .word').mouseover(function (event) {
        $(this).addClass('hoverme');
        positionHover(event);
        var details = $(event.target).data('word_all'); //HTML5 only? test!
        var arrWord = details.split("|");
        var meaning = arrWord[0].replace('`', '\'');
        $("#diagHover #meanHover").html(meaning);//being corrected
        $('#diagHover').css({ 'height': WordHoverDiagHeight+'px' });
        if(meaning.length>30)
            $('#diagHover').css({ 'width': '200px'});
        else
            $('#diagHover').css({ 'width': '100px'});
    });

    $('div .word').mouseout(function () {
        $(this).removeClass('hoverme');
        $("#diagHover").css({ 'visibility': 'hidden' });
    });
    $('div.mainLeft').addClass('Arabic');

    // thanks to  stackoverflow.com/questions/10073699/pad-a-number-with-leading-zeros-in-javascript
    String.prototype.lpad = function (padString, length) {
        var str = this;
        while (str.length < length)
            str = padString + str;
        return str;
    }

    $('div .ayahNum').click(function (event) {
        $('#dvOverlay').addClass('fadeMe');
        $('#dvOverlay').click(function () {
            $('#dvOverlay').removeClass('fadeMe');
            $("#dialog").css({ 'visibility': 'hidden' });

        });
        positionPopup(event, true);
        var details = $(event.target).data('ayah_all'); //HTML5 only? test!
        details = details.replace('(', '');
        details = details.replace(')', '');
        var arrWord = details.split(":");
        var sura = arrWord[0];
        var ayah = arrWord[1];
        var mp3Ayah = "/audio/" + sura.lpad("0", 3) + ayah.lpad("0", 3) + ".mp3";
        var mp3Ayah2 = sura.lpad("0", 3) + (String(parseInt(ayah) + 1)).lpad("0", 3);
        mp3Ayah2 = "/audio/" + mp3Ayah2 + ".mp3";

        $.ajax({
            url: "ActionHandler.aspx?reqType=getTafsirLink&surahID=" + sura + "&ayahID=" + ayah + "&rn=" + Math.random().toString(16),
            async: false,
            success: function (result) {
                if (result.indexOf("SUCCESS") > -1) {
                    var arrRes = result.split("|");
                    $("#dialog #tafsirPage").html('<a target="_blank" href="' + arrRes[1] + '">Read Tafsir</a>');
                }
                else
                    alert("Note: " + result);
            }
        });

        $.ajax({
            url: "ActionHandler.aspx?reqType=getUserAyahNote&surahID=" + sura + "&ayahID=" + ayah + "&rn=" +  Math.random().toString(16),
            async: true,
            success: function (result) {
                if (result.indexOf("SUCCESS") > -1) {                             
                    var arrRes = result.split("|");
                    $("#dialog #txtAyahNote").html(arrRes[1]);                
                }
                else
                    alert("Note: " + result);
            }
        });


        $("#dialog #sectAyah  #spAyahUnmarked").unbind('click');
        $("#dialog #sectAyah  #spAyahUnmarked").click(function () {
            ClickAyah(event, sura, ayah, 0);
            return false;
        });

        $("#dialog #sectAyah  #spAllKnown").unbind('click');
        $("#dialog #sectAyah  #spAllKnown").click(function () {
            ClickAyah(event, sura, ayah, 1);
            return;
        });
     
        $("#dialog #sectAyah  #spAyahMemod").unbind('click');
        $("#dialog #sectAyah  #spAyahMemod").click(function () {
            ClickAyah(event, sura, ayah, 11);
            return false;
        });

        $("#dialog #sectAyah  #spAyahUnderlined").unbind('click');
        $("#dialog #sectAyah  #spAyahUnderlined").click(function () {
            ClickAyah(event, sura, ayah, 12);
            return false;
        });

        $("#dialog #sectAyah  #spAyahHilited").unbind('click');
        $("#dialog #sectAyah  #spAyahHilited").click(function () {
            ClickAyah(event, sura, ayah, 13);
            return false;
        });

        $("#dialog #sectAyah  #spAyahBoxed").unbind('click');
        $("#dialog #sectAyah  #spAyahBoxed").click(function () {
            ClickAyah(event, sura, ayah, 14);
            return false;
        });

        $("#dialog #saveAyahNote").unbind('click');
        $("#dialog #saveAyahNote").click(function () {
            alert('Not available in offline version. \nPlease go online and login.'); return;

            $.ajax({
                url: "ActionHandler.aspx?reqType=addUserAyahNote&surahID=" + sura + "&ayahID=" + ayah + "&ayahNote=" + $("#dialog #txtAyahNote").html(),
                async: false,
                success: function (result) {
                    if (result.indexOf("SUCCESS") > -1) {
                        $("#dialog .spSavedMsg").fadeIn(400);
                        $("#dialog .spSavedMsg").html('<span style="color:green"><b> Saved Successfully</b></span>');
                        $("#dialog .spSavedMsg").fadeOut(1200); 
                    }
                    else
                        alert("Note: " + result);

                }
            });
        });
        function ClickAyah(event, sura, ayah, stateID) {
            alert('Not available in offline version. \nPlease go online and login.'); return;

            $.ajax({
                url: "ActionHandler.aspx?reqType=markAyahLemmas&surahID=" + sura + "&ayahID=" + ayah + "&stateID=" + stateID + "&rn=" + Math.random().toString(16),
                success: function (result) {
                    if (result.indexOf("SUCCESS|")>-1) {
                        if (stateID == 1) {
                            $(event.target).parent().children().removeClass("wordstate0 wordstate1 wordstate11 wordstate12 wordstate13 wordstate14");
                            $(event.target).parent().children().addClass("wordstate" + stateID);

                            var arrRes = result.split("|");
                            $(".mainRight #spAgKnown").html(arrRes[1] + ' (' + parseInt(parseInt(arrRes[1]) * 100 / 77429) + '%)');
                            $(".mainRight #spUniqKnown").html(arrRes[2]);
                            $(".mainRight #spUniqUnderline").html(arrRes[3]);
                            $(".mainRight #spUniqHilited").html(arrRes[4]);
                            $(".mainRight #spUniqBoxed").html(arrRes[5]);
                        }
                        else {
                            $(event.target).removeClass("wordstate0 wordstate1 wordstate11 wordstate12 wordstate13 wordstate14");
                            $(event.target).addClass("wordstate" + stateID);
                        }                                   
                    }
                    else
                        alert(result);

                }
            });

            //---close
            $("#close_x").click();
        }

        $("#close_x").click(function () {
            $('#dvOverlay').removeClass('fadeMe');
            $("#dialog").css({ 'visibility': 'hidden' });
            return false;
        });
        return false;
    });

    $('div .word').click(function (event) {
        $('#dvOverlay').addClass('fadeMe');
        $('#dvOverlay').click(function () {
            $('#dvOverlay').removeClass('fadeMe');
            $("#dialog").css({ 'visibility': 'hidden' });
        });
        positionPopup(event, false);

        var details = $(event.target).data('word_all'); //HTML5 only? test!
        var arrWord = details.split("|");
        var meaning = arrWord[0].replace('`', '\'');
        var lemma = arrWord[1].replace('"', '\'');
        var lemmaID = arrWord[2];
        var root = arrWord[3];
        var arabicWord = arrWord[4];
        var wordNote = arrWord[5];
        if (root.length > 2)
            $("#rootLetters").html("<a target='_blank' href='http://corpus.quran.com/qurandictionary.jsp?q=" + root + "'>" + root + "</a>");

        $(".arRoot").html(GetArWord(root, ' '));
        $("#lexPage").html(GetLexiconPages(root));
        //$("#lexPage").html("<a target='_blank' href='LanesLex.aspx?p=" + lexpages[1] + "'>" + lexpages[0] + "</a>");
        var lemArabic = GetArWord(lemma,'');//buckArab["r"] + buckArab["~"] + buckArab["a"] + buckArab["H"] + buckArab["i"] + buckArab["y"] + buckArab["m"];
                 

        //r~aHiym
        $("#dialog #mean").html("<span>" + arabicWord + "</span> = " + meaning);
        if (root.length>2)
            $("#dialog #lemma").html("Base: <span id='arabWord'>" + lemArabic + "</span>&nbsp; <span id='spSearch'><a target='_blank' href='http://corpus.quran.com/qurandictionary.jsp?q=" + root + "'>[Search Quran]</a></span>");
        else
            $("#dialog #lemma").html("Base: <span id='arabWord'>" + lemArabic + "</span>&nbsp;");

        $("#dialog #txtLemmaNote").html(wordNote);

        $("#dialog #dvKnown").unbind('click');
        $("#dialog #dvKnown").click(function () {
            ClickWord(event, lemmaID, 1);
            return false;
        });

        $("#dialog #dvUnderlined").unbind('click');
        $("#dialog #dvUnderlined").click(function () {
            ClickWord(event, lemmaID, 2);
            return false;
        });

        $("#dialog #dvHilited").unbind('click');
        $("#dialog #dvHilited").click(function () {
            ClickWord(event, lemmaID, 3);
            return false;
        });

        $("#dialog #dvBoxed").unbind('click');
        $("#dialog #dvBoxed").click(function () {
            ClickWord(event, lemmaID, 4);
            return false;
        });

        $("#dialog #dvUnmarked").unbind('click');
        $("#dialog #dvUnmarked").click(function () {
            ClickWord(event, lemmaID, 0);
            return false;
        });
                 
        $("#dialog #saveLemmaNote").unbind('click');
        $("#dialog #saveLemmaNote").click(function () {
            alert('Not available in offline version. \nPlease go online and login.'); return;

            $.ajax({
                url: "ActionHandler.aspx?reqType=addUserLemmaNote&LemmaID=" + lemmaID + "&LemmaNote=" + $('#dialog #txtLemmaNote').html(),
                async: false,
                success: function (result) {
                    if (result.indexOf("SUCCESS") > -1) {
                        $("#dialog .spSavedMsg").fadeIn(400);
                        $("#dialog .spSavedMsg").html('<span style="color:green"><b> Saved Successfully</b></span>');
                        $("#dialog .spSavedMsg").fadeOut(1200);

                    }
                    else
                        alert("Note: " + result);

                }
            });
        });

        function ClickWord(event, lemmaID, stateID) {
            alert('Not available in offline version. \nPlease go online and login.'); return;
           
            $.ajax({
                url: "ActionHandler.aspx?reqType=markLemma&lemmaID=" + lemmaID + "&stateID=" + stateID,
                async: false,
                success: function (result) {
                    if (result.indexOf("SUCCESS")>-1) {
                        $(event.target).removeClass("wordstate0 wordstate1 wordstate2 wordstate3 wordstate4");
                        $(event.target).addClass("wordstate" + stateID);
                        var arrRes = result.split("|");
                        $(".mainRight #spAgKnown").html(arrRes[1] + ' (' + parseInt(parseInt(arrRes[1]) * 100 / 77429) + '%)');
                        $(".mainRight #spUniqKnown").html(arrRes[2]);
                        $(".mainRight #spUniqUnderline").html(arrRes[3]);
                        $(".mainRight #spUniqHilited").html(arrRes[4]);
                        $(".mainRight #spUniqBoxed").html(arrRes[5]);

                        //---close
                        $("#close_x").click();

                    }
                    else
                        alert(result);

                }
            });
        }
        $("#close_x").click(function () {
            $('#dvOverlay').removeClass('fadeMe');
            $("#dialog").css({ 'visibility': 'hidden' });
            return false;
        });

        return false;

    });
});
        
function positionPopup(event, isAyahClicked) {
    var tPosX = event.pageX - 50;
    var tPosY = event.pageY + 20;
    $('#dialog').css({ 'visibility': 'visible', 'position': 'absolute', 'top': tPosY, 'left': tPosX });
    if (isAyahClicked)
    {
        $("#sectWord").hide();
        $("#sectAyah").show();
    }
    else
    {
        $("#sectWord").show();
        $("#sectAyah").hide();
    }

};

function positionHover(event) {
    var tPosX = event.pageX - 20;
    var tPosY = event.pageY + 20;
    $('#diagHover').css({'visibility': 'visible', 'position': 'absolute', 'top': tPosY, 'left': tPosX });
};
function positionAyahMenu(event) {
    var tPosX = event.pageX - 20;
    var tPosY = event.pageY + 20;
    $('#diagHover').css({'visibility': 'visible', 'position': 'absolute', 'top': tPosY, 'left': tPosX });
};
function OpenInNewTab(url)
{
    var win=window.open(url, '_blank');
    win.focus();
}
       

function SubmitGo(surah, ayah) {
    var thePage = 0;
    var surah = $('#Surah').val();
    var ayah = $('#AyahFrom').val();


    if (surah == 81) thePage = 586;
    if (surah == 85) thePage = 590;
    if (surah == 91) thePage = 595;
    if (surah == 93) thePage = 596;
    if (surah == 95) thePage = 597;
    if (surah == 97) thePage = 598;
    if (surah == 99) thePage = 599;
    if (surah == 101) thePage = 600;
    if (surah == 102) thePage = 600;
    if (surah == 104) thePage = 601;
    if (surah == 105) thePage = 601;
    if (surah == 107) thePage = 602;
    if (surah == 108) thePage = 602;
    if (surah == 110) thePage = 603;
    if (surah == 111) thePage = 603;
    if (surah == 113) thePage = 603;
    if (surah == 114) thePage = 604;

    if (thePage > 0) {
        window.document.location.href = thePage + '.htm';
        return;
    }

    for (var i = 0; i < 603; i++) {
        if (QuranData.Pagemark[i][0] == surah) {
            if (QuranData.Pagemark[i + 1][0] > surah) {
                thePage = i + 1;
                break;
            }
            if (QuranData.Pagemark[i][1] <= ayah && QuranData.Pagemark[i + 1][1] >= ayah) {
                thePage = i + 1; break;

            }
        }
    }

    window.document.location.href = thePage + '.htm';

}

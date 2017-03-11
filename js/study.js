
$(document).ready(function () {
  //  alert('QuranData.Sura[1][4]');
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
        //w x h requirment: less than 200 chars=> 305 x 100
        //w x h requirment: less than 500 chars=> 400 x 100
        //w x h requirment: less than 800 chars=>  
        //$('#diagHover').css({ 'height': '100px' });
        //if (meanTrans.length > 200)
        //    $('#diagHover').css({ 'width': '400px' });
        //else
        //    $('#diagHover').css({ 'width': '305px' });

        //start with this
         
        /*
        //test with 
        //    select LEN(ayahtexten), * from translationEn where LEN(ayahtexten)>50 order by LEN(ayahtexten) desc
        
         * and 
         *  $("#diagHover #meanHover").html(meanTrans.length + meanTrans);
         */
           
        $('#diagHover').css({ 'width': '150px' });
        $('#diagHover').css({ 'height': '50px' });
        //alert(meanTrans.length);
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
        var wordNote = arrWord[5];

        if (wordNote.length > 0)
            meaning = meaning + "<br>[" + wordNote + "]";

        $("#diagHover #meanHover").html(meaning);//being corrected
         //alert(WordHoverDiagHeight);
        if (meaning.length > 30) {
            $('#diagHover').css({ 'width': '200px' });
            WordHoverDiagHeight = 60;
        }
        else {
            $('#diagHover').css({ 'width': '100px' });
            WordHoverDiagHeight = 40;
        }

        $('#diagHover').css({ 'height': WordHoverDiagHeight + 'px' });

    });

    $('div .word').mouseout(function () {
        $(this).removeClass('hoverme');
        $("#diagHover").css({ 'visibility': 'hidden' });
    });
    $('div.mainLeft').addClass('ArabicIndoPak');

    // thanks to  stackoverflow.com/questions/10073699/pad-a-number-with-leading-zeros-in-javascript
    String.prototype.lpad = function (padString, length) {
        var str = this;
        while (str.length < length)
            str = padString + str;
        return str;
    }

    $("#saveGeneralNote").unbind('click');
    $("#saveGeneralNote").click(function () {
       // alert("Note: " + $("#txtGeneralNote").val());
        //alert($("#txtGeneralNote").val());
        $.ajax({
            url: "ActionHandler.aspx?reqType=saveUserGeneralNote", //&generalNote=" + $("#txtGeneralNote").html(),
            async: false,
            type: 'POST',
            data: { generalNote: $("#txtGeneralNote").val() },
            success: function (result) {
                //alert(result);
                if (result.indexOf("SUCCESS") > -1) {
                    $(".spGenSavedMsg").fadeIn(400);
                    $(".spGenSavedMsg").html('<span style="color:green"><b> Saved Successfully</b></span>');
                    $(".spGenSavedMsg").fadeOut(1200);
                }
                else {
                    if (result.indexOf("SESSION_EXPIRED") > -1)
                        ShowLoginDiv();
                    else
                        alert("Note: " + result);
                }

            },
            error: function (ret) {
                    alert(ret);
                }
        });
    });

    $("#btnReLogin").click(function () {
        //alert($("#uname2").val()); return;
        $.ajax({
            url: "ActionHandler.aspx?reqType=clickReLogin&uname=" + $("#uname").val(),
            async: false,
            type: 'POST',
            data: { pword: $("#pword").val() },
            success: function (result) {
                if (result.indexOf("SUCCESS") > -1) {
                    //alert("Logged-in successfully! page will be reloaded.");
                    location.reload();

                   // $(".spGenSavedMsg").fadeIn(400);
                    //$(".spGenSavedMsg").html('<span style="color:green"><b> Saved Successfully</b></span>');
                   // $(".spGenSavedMsg").fadeOut(1200);
                }
                else {
                   // if (result.indexOf("1010") > -1)
                    //    ShowLoginDiv();
                    //else
                        alert("Note: " + result);
                }

            }
        });
    });

    function ShowLoginDiv()
    {
        if ($('#re-login-div').length) {
            $('#dvOverlay').addClass('fadeMe');
            $("#re-login-div").css({ 'visibility': 'visible' });
            //$("#re-login-div").top = $(window).height / 2;
            $('#dvOverlay').click(function () {
                $('#dvOverlay').removeClass('fadeMe');
                $("#re-login-div").css({ 'visibility': 'hidden' });
            });
        }
        else {
            alert("Please log in to track progress, insha'Allah.\n\n For testing purpose use- \n\nLogin: guest@myQuranStudy.com, Password: pass");
        }
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
        //document.getElementById("flPlayerObj").setUrl(mp3Ayah);//+ "|" + mp3Ayah2
        //document.getElementById("flPlayerObj").SetU
        //alert(mp3Ayah);
       // document.getElementById("flPlayerObj").SetVariable("player:jsUrl", mp3Ayah); //%7C

        $("#currAudioVerse").text(sura.lpad("0", 3) + ":" + String(ayah).lpad("0", 3));

        $("#audioayah").attr({
            "src": mp3Ayah            
        })
        /*
        $("#audioayah")[0].addEventListener("ended", function () {
            var ok1 = GetNextAyah(this.src); //http://../audio/002003.mp3
            this.src = ok1;
            $("#audioayah").removeEventListener("ended");
            this.play();
        }, false);
         */
        //alert(document.getElementById("audioayah").src);
        //document.getElementById("flPlayerObj").isPlaying
        //now get getUserAyahNote
        function GetNextAyah(url)
        {
            //$("#audioayah")[0].removeEventListener('ended');
           // $("#audioayah")[0].removeEventListener('ended');

           // console.log(url);
            //var filename = url.split('/').pop();//002003.mp3
            var filename = url.substring(url.lastIndexOf("/") + 1, url.lastIndexOf(".")); //002003
            var ayah1 = parseInt(filename.substring(3)) + 1;
            var sura1 = filename.substring(0, 3);
            $("#currAudioVerse").text(sura1.lpad("0", 3) + ":" + String(ayah1).lpad("0", 3));
           // alert(ayah.lpad("0", 3));
            var mp3Ayah1 = "/audio/" + sura1.lpad("0", 3) + String(ayah1).lpad("0", 3) + ".mp3";
           // alert(mp3Ayah);
            return mp3Ayah1;            
        }

        $.ajax({
            url: "ActionHandler.aspx?reqType=getTafsirLink&surahID=" + sura + "&ayahID=" + ayah + "&rn=" + Math.random().toString(16),
            async: false,
            success: function (result) {
                if (result.indexOf("SUCCESS") > -1) {
                    var arrRes = result.split("|");
                    $("#dialog #tafsirPage").html('<a target="_blank" href="TafsirIbnKathir.aspx?fn=' + arrRes[1] + '">Read Tafsir here</a>' + ' or <a target="_blank" href="' + arrRes[2] + '">here</a>');
                }
                else
                    alert("Note: " + result);
               // alert("Note: " + "ActionHandler.aspx?reqType=getTafsirLink&surahID=" + sura + "&ayahID=" + ayah + "&rn=" + Math.random().toString(16));
            }
        });

        $.ajax({
            url: "ActionHandler.aspx?reqType=getUserAyahNote&surahID=" + sura + "&ayahID=" + ayah + "&rn=" +  Math.random().toString(16),
            async: false,
            success: function (result) {
                if (result.indexOf("SUCCESS") > -1) {                             
                    var arrRes = result.split("|");
                    $("#dialog #txtAyahNote").html(arrRes[1]);                
                }
                else
                    if (result.indexOf("SESSION_EXPIRED") > -1)
                        ShowLoginDiv();
                    else
                        alert("Note: " + result);
            }
        });

        /* various earlier trials did not work
        var flVars = "mp3=/audio/002001.mp3&amp;showstop=1&amp;showvolume=1&amp;bgcolor1=888888&amp;bgcolor2=999999";
        $('#dvPlayer').find('param[name="FlashVars"]').val(flVars);
        $('#dvPlayer object').html($('#dvPlayer object').html());
        $("object param[name='flashvars']").attr("value", flVars);
        */

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
            $.ajax({
                url: "ActionHandler.aspx?reqType=addUserAyahNote&surahID=" + sura + "&ayahID=" + ayah,// + "&ayahNote=" + $("#dialog #txtAyahNote").html(),
                async: false,
                type: 'POST',
                data: { ayahNote: $("#dialog #txtAyahNote").val() },
                success: function (result) {
                    if (result.indexOf("SUCCESS") > -1) {
                        $("#dialog .spSavedMsg").fadeIn(400);
                        $("#dialog .spSavedMsg").html('<span style="color:green"><b> Saved Successfully</b></span>');
                        $("#dialog .spSavedMsg").fadeOut(1200); 
                    }
                    else
                        if (result.indexOf("SESSION_EXPIRED") > -1)
                            ShowLoginDiv();
                        else
                            alert("Note: " + result);

                }
            });
        });
       
        function ClickAyah(event, sura, ayah, stateID) {
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
                        if (result.indexOf("SESSION_EXPIRED") > -1)
                            ShowLoginDiv();
                        else
                            alert("Note: " + result);

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
            $("#rootLetters").html(root);
            //$("#rootLetters").html("<a target='_blank' href='LanesLex.aspx?p=http://corpus.quran.com/qurandictionary.jsp?q=" + root + "'>" + root + "</a>");

        $(".arRoot").html(GetArWord(root, ' '));
        $("#lexPage").html(GetLexiconPages(root));
        //$("#lexPage").html("<a target='_blank' href='LanesLex.aspx?p=" + lexpages[1] + "'>" + lexpages[0] + "</a>");
        var lemArabic = GetArWord(lemma,'');//buckArab["r"] + buckArab["~"] + buckArab["a"] + buckArab["H"] + buckArab["i"] + buckArab["y"] + buckArab["m"];
                 

        //r~aHiym
        $("#dialog #mean").html("<span>" + arabicWord + "</span> = " + meaning);
        if (root.length>2)
            $("#dialog #lemma").html("Base: <span id='arabWord'>" + lemArabic + "</span>&nbsp; <span id='spSearch'><a target='_blank' href='LanesLex.aspx?p=http://corpus.quran.com/qurandictionary.jsp?q=" + root + "'>[Search Quran]</a></span>");
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
            $.ajax({
                url: "ActionHandler.aspx?reqType=addUserLemmaNote&LemmaID=" + lemmaID,// + "&LemmaNote=" + $('#dialog #txtLemmaNote').html(),
                async: false,
                type: 'POST',
                data: { LemmaNote: $("#dialog #txtLemmaNote").val() },
                success: function (result) {
                    if (result.indexOf("SUCCESS") > -1) {
                        $("#dialog .spSavedMsg").fadeIn(400);
                        $("#dialog .spSavedMsg").html('<span style="color:green"><b> Saved Successfully</b></span>');
                        $("#dialog .spSavedMsg").fadeOut(1200);

                    }
                    else
                        if (result.indexOf("SESSION_EXPIRED") > -1)
                            ShowLoginDiv();
                        else
                            alert("Note: " + result);

                }
            });
        });

        function ClickWord(event, lemmaID, stateID) {
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
                        if (result.indexOf("SESSION_EXPIRED") > -1)
                        {
                            $("#close_x").click();
                            ShowLoginDiv(); // alert("Note11: ");
                        }
                        else
                            alert("Note: " + result);

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

    //=====
    $('#chkRuku').bind('click', function () {
        //alert('ee');
        if ($('#chkRuku').is(':checked')) {
           // ShowJuzRuku();
           // $('.rukumark').show();

            //$(".Arabic").css({ 'visibility': 'hidden' });
            $('.mainLeft').removeClass('ArabicIndoPak');
            $('.mainLeft').removeClass('Arabic');
            $('.mainLeft').addClass('ArabicIndoPak');
        }
        else {
          //  $('.rukumark').hide();

            $('.mainLeft').removeClass('ArabicIndoPak');
            $('.mainLeft').removeClass('Arabic');
            $('.mainLeft').addClass('Arabic');
       }

    });
    //onready to this
    ShowJuzRuku();
    //$('#chkRuku').prop('checked', true);

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
       
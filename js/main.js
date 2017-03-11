/*
       //var buckArab0 = { "{": "ا", "'": "ء", "|": "آ", "?": "أ", "&": "ؤ", "<": "إ", "}": "ئ", "A": "ا", "b": "ب", "p": "ة", "t": "ت", "v": "ث", "j": "ج", "H": "ح", "x": "خ", "d": "د", "*": "ذ", "r": "ر", "z": "ز", "s": "س", "$": "ش", "S": "ص", "D": "ض", "T": "ط", "Z": "ظ", "E": "ع", "g": "غ", "_": "ـ", "f": "ف", "q": "ق", "k": "ك", "l": "ل", "m": "م", "n": "ن", "h": "ه", "w": "و", "Y": "ى", "y": "ي", "F": "ً", "N": "ٌ", "K": "ٍ", "~": "ّ", "o": "ْ", "u": "ُ", "a": "َ", "i": "ِ" };
        
       //below is mine based http://www.qamus.org/transliteration.htm - last works in IE but Visula Studio does not show correctly
       //also based on http://corpus.quran.com/java/buckwalter.jsp
       var buckArab1 = {//total = 51 = 47 + 4 (not used)
           "'": "ء", "|": "آ", ">": "أ", "&": "ؤ", "<": "إ", "}": "ئ", "A": "ا", "b": "ب", "p": "ة", "t": "ت",//10
           "v": "ث", "j": "ج", "H": "ح", "x": "خ", "d": "د", "*": "ذ", "r": "ر", "z": "ز", "s": "س", "$": "ش", //10
           "S": "ص", "D": "ض", "T": "ط", "Z": "ظ", "E": "ع", "g": "غ", "_": "ـ", "f": "ف", "q": "ق", "k": "ك", //10
           "l": "ل", "m": "م", "n": "ن", "h": "ه", "w": "و", "Y": "ى", "y": "ي", "F": "ً", "N": "ٌ", "K": "ٍ", //10
           "a": "َ", "u": "ُ", "i": "ِ", "~": "ّ", "o": "ْ", //5
           "`": "ٰ", "{": "ا", //4 not used V J P G , select top 20 Lemma  from dbo.Lemma where Lemma COLLATE sql_latin1_general_cp1_cs_as like '%V%' --V J P G
           "^": "\u0653", "#": "\u0654", "@": "\u06DF", ",": "\u06E5", ".": "\u06E6", //to do these             
           "]": "x","[": "x", ":": "x","\"": "x",";": "x","!": "x", "-": "x", "+": "x","%": "x"

       }
       */
//based on http://corpus.quran.com/java/buckwalter.jsp , to regenerate run - select '"' + ASCIIVal + '": ' + '"\' + hex + '", '  from dbo.BuckwalterEx 
//another ef http://www.qamus.org/transliteration.htm
var buckArab = {
    "'": "\u0621",
    ">": "\u0623",
    "&": "\u0624",
    "<": "\u0625",
    "}": "\u0626",
    "A": "\u0627",
    "b": "\u0628",
    "p": "\u0629",
    "t": "\u062A",
    "v": "\u062B",
    "j": "\u062C",
    "H": "\u062D",
    "x": "\u062E",
    "d": "\u062F",
    "*": "\u0630",
    "r": "\u0631",
    "z": "\u0632",
    "s": "\u0633",
    "$": "\u0634",
    "S": "\u0635",
    "D": "\u0636",
    "T": "\u0637",
    "Z": "\u0638",
    "E": "\u0639",
    "g": "\u063A",
    "_": "\u0640",
    "f": "\u0641",
    "q": "\u0642",
    "k": "\u0643",
    "l": "\u0644",
    "m": "\u0645",
    "n": "\u0646",
    "h": "\u0647",
    "w": "\u0648",
    "Y": "\u0649",
    "y": "\u064A",
    "F": "\u064B",
    "N": "\u064C",
    "K": "\u064D",
    "a": "\u064E",
    "u": "\u064F",
    "i": "\u0650",
    "~": "\u0651",
    "o": "\u0652",
    "^": "\u0653",
    "#": "\u0654",
    "`": "\u0670",
    "{": "\u0671",
    ":": "\u06DC",
    "@": "\u06DF",
    "\"": "\u06E0",
    "[": "\u06E2",
    ";": "\u06E3",
    ",": "\u06E5",
    ".": "\u06E6",
    "!": "\u06E8",
    "-": "\u06EA",
    "+": "\u06EB",
    "%": "\u06EC",
    "]": "\u06ED"
}

function GetArWord(lemma, seperator) {
    //  lemma='{$omaaz~ato';
    // lemma = '$a`fiEiyn';
   
    //if (lemma == '-') return 'Pronouns/disjointed letters';
    var out = "";
    var res = lemma.split("");
    for (i = 0; i < res.length; i++) {
        //alert(buckArab[res[i]]);
        if (buckArab[res[i]] != 'undefined')
            out += buckArab[res[i]] + seperator;
    }
    //alert(out);
    return out;//buckArab["r"] + buckArab["~"] + buckArab["a"] + buckArab["H"] + buckArab["i"] + buckArab["y"] + buckArab["m"];
}

function GetLexiconPages(letters) {
    var htmlCont = '';
    $.ajax({
        url: "GetHandler.aspx?reqType=getLex&root=" + letters,
        async: false,
        success: function (result) {
            if (result.indexOf("SUCCESS") > -1) {
                // $(event.target).css({ 'color': 'goldenrod' });
                var arrRes = result.split("|");
                if (arrRes[1] != "") {
                    var vol = arrRes[1];
                    var arrPg = arrRes[2].split(",");
                    //alert(arrPg.length);

                    var firstPage = arrPg[0].trim();
                    var lastPage = arrPg[arrPg.length - 1].trim();
                    for (var i = 0; i < arrPg.length; i++) {
                        var p = arrPg[i].trim();
                        var url = "/LanesLexicon.aspx?v=" + vol + "&p=" + p + "&range1=" + firstPage + "&range2=" + lastPage; // /LaneLexicon/V3/00000114.pdf
                        htmlCont += "<a target='_blank' href='" + url + "'>" + p + "</a>, ";
                    }

                    //OLD
                    //for (i = 0; i < arrPg.length; i++) {
                    //    var p = arrPg[i].trim();
                    //    var url = "http://www.studyquran.org/LaneLexicon/Volume" + vol + "/" + p.lpad("0", 8) + ".pdf"; //http://www.studyquran.org/LaneLexicon/Volume3/00000114.pdf
                    //    htmlCont += "<a target='_blank' href='LanesLex.aspx?p=" + url + "'>" + p + "</a>, ";
                    //}

                } 

            }
            else
                htmlCont = 'not found';

        },
        error: function (request, status, error) {
            htmlCont = error;
        }

    });
    return htmlCont;
}

function htmlEncode(value) {
    //create a in-memory div, set it's inner text(which jQuery automatically encodes)
    //then grab the encoded contents back out.  The div never exists on the page.
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).val();
}


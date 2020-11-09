var data = JSON.parse(data);

$(function () {
    changeSearchbox();
    show('#search');
});

function changeSearchbox() {
    selectLevel = $('#selectLevel').val();
    selectType = $('#selectType').val();

    content = '<tbody>';

    for (i = 0; i < data.length; i++) {
        if (data[i].level == selectLevel && data[i].type == selectType) {
            content = content + '<tr><td><label class="mb-0"><input type="radio" name="lesson" value="' + data[i].lesson + '"> ' + data[i].lesson + '</label></td></tr>"';
        }
    }
    content = content + '</tbody>';
    $("#table_select_lesson").empty();
    $("#table_select_lesson").html(content);
}

function show(id) {
    $('#learn').hide();
    $('#test').hide();
    $('#search').hide();
    $('#review').hide();
    $(id).show();
}

function learnClick() {

    lesson = $("input[type='radio']:checked").val();
    if (lesson == '') return;

    content = "";
    for (i = 0; i < data.length; i++) {
        if (data[i].lesson == lesson) {
            for (j = 0; j < data[i].kanji.length; j++) {
                if (j % 2 == 0) {
                    content += '<div class="row p-3 bg_odd">';
                } else {
                    content += '<div class="row p-3">';
                }
                content += '<span class="col-sm-1">' + (j + 1) + '.</span>' +
                    '<span class="col-sm-3 pl-0">' + data[i].kanji[j] + '</span>' +
                    '<span class="col-sm-4 pl-0">' + data[i].hira[j] + '</span>' +
                    '<span class="col-sm-4 pl-0">' + data[i].mean[j] + '</span></div>';
            }
            break;
        }
    }
    $("#content_lesson").empty();
    $("#content_lesson").html(content);

    show('#learn');
}

function testClick() {
    initTestPage();
    $('#content_test').animate({
        scrollTop: "0"
    });
}

function initTestPage() {
    lesson = $("input[type='radio']:checked").val();
    if (lesson == '') return;

    content = "";
    for (i = 0; i < data.length; i++) {
        if (data[i].lesson == lesson) {
            for (j = 0; j < data[i].kanji.length; j++) {
                if (j % 2 == 0) {
                    content += '<div class="row p-3 bg_odd">';
                } else {
                    content += '<div class="row p-3">';
                }
                content += '<span class="col-sm-1">' + (j + 1) + '.</span>' + '<span class="col-sm-3 pl-0">' + data[i].kanji[j] + '</span>';
                randomInt = random();
                if (randomInt % 2 == 0) {
                    content += '<span class="col-sm-4 pl-0 color_high" valueHidden="' + data[i].hira[j] + '">_____________</span>' + '<span class="col - sm - 4 pl - 0 ">' + data[i].mean[j] +
                        '</span></div>';
                } else {
                    content += '<span class="col-sm-4 pl-0">' + data[i].hira[j] + '</span>' + '<span class="col-sm-4 pl-0 color_high" valueHidden="' + data[i].mean[j] + '">_____________</span></div>';
                }
            }
            break;
        }
    }
    $("#content_test").empty();
    $("#content_test").html(content);

    $('#btnCheckAgain').html('Check');

    show('#test');
}

function reviewClick() {
    lesson = $("input[type='radio']:checked").val();
    if (lesson == '') return;

    content = "";
    for (i = 0; i < data.length; i++) {
        if (data[i].lesson == lesson) {
            for (j = 0; j < data[i].kanji.length; j++) {
                if (j % 2 == 0) {
                    content += '<div class="row row_review bg_odd">';
                } else {
                    content += '<div class="row row_review">';
                }

                if (data[i].kanji[j].length == 0) {
                    content += '<div class="col-sm-4 cell_review"><span>-</span></div>';
                } else {
                    content += '<div class="col-sm-4 cell_review"><span>' + data[i].kanji[j] + '</span></div>';
                }

                randomInt = random() % 2;
                if (randomInt == 0) {
                    content += '<div class="col-sm-4 cell_review" onclick=\'$("#' + 'hira_' + j + '").toggleClass("d-none")\'><span id="' + 'hira_' + j + '">' + data[i].hira[j] + '</span></div>';
                    content += '<div class="col-sm-4 cell_review" onclick=\'$("#' + 'mean_' + j + '").toggleClass("d-none")\'><span class="d-none" id="' + 'mean_' + j + '">' + data[i].mean[j] + '</span></div>';
                } else {
                    content += '<div class="col-sm-4 cell_review" onclick=\'$("#' + 'hira_' + j + '").toggleClass("d-none")\'><span class="d-none" id="' + 'hira_' + j + '">' + data[i].hira[j] + '</span></div>';
                    content += '<div class="col-sm-4 cell_review" onclick=\'$("#' + 'mean_' + j + '").toggleClass("d-none")\'><span id="' + 'mean_' + j + '">' + data[i].mean[j] + '</span></div>';
                }
                content += '</div>';
            }
            break;
        }
    }

    $("#content_review").empty();
    $("#content_review").html(content);

    show('#review');
}

function random() {
    return Math.floor((Math.random() * 100) + 1);
}

function checkAgainClick() {
    if ($('#btnCheckAgain').html() == 'Again') {
        initTestPage();
    } else {
        spanArr = $('#content_test').find('span.color_high');

        for (i = 0; i < spanArr.length; i++) {
            value = $(spanArr[i]).attr('valueHidden');
            $(spanArr[i]).text(value);
        }

        $('#btnCheckAgain').html('Again');
    }
    $('#content_test').animate({
        scrollTop: "0"
    });
}

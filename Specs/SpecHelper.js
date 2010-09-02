beforeEach(function () {
    this.addMatchers({
    })

    $('body').append("<div id='jasmine_content'></div>");
});

afterEach(function () {
    $('#jasmine_content').remove();
});

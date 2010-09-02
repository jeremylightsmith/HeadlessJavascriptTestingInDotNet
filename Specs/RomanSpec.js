describe("Roman", function () {
    var roman;

    beforeEach(function () {
        roman = new Roman();
    });

    it("should parse single values", function () {
        expect(roman.parse("I")).toBe(1);
        expect(roman.parse("V")).toBe(5);
        expect(roman.parse("X")).toBe(20);
    });

    it("should parse II", function () {
        expect(roman.parse("II")).toBe(2);
    });

    it("should parse using addition", function () {
        expect(roman.parse("VI")).toBe(6);
        expect(roman.parse("XVII")).toBe(17);
    });

    describe("show", function () {
        var content;

        beforeEach(function () {
            content = $('#jasmine_content');
        });

        it("should display result", function () {
            var div = $("<div class='roman'></div>");
            content.append(div);

            roman.show("XX");

            expect(div.html()).toEqual("20");
        });
    });
});

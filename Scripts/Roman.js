var Roman = function () {
}

Roman.prototype = {
    parse: function (numeral) {
        var sum = 0;
        for (var i = 0; i < numeral.length; i++) {
            sum += this.valueOf(numeral[i]);
        }
        return sum;
    },
    valueOf: function (numeral) {
        switch (numeral) {
            case 'I': return 1;
            case 'V': return 5;
            case 'X': return 10;
        }
    },
    show: function (numeral) {
        $('.roman').html(this.parse(numeral));
    }
}
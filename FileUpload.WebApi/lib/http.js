function ajax(method, url, data, options) {
    var parameters = method === 'GET' ? data : ko.toJSON(data);
    var o = {
        url: url,
        data: parameters,
        type: method,
        contentType: 'application/json',
        dataType: 'json',
        traditional: 'true'
    };
    if (options) {
        $.extend(o, options);
    }
    return $.ajax(o);
}

var $http = {
    get: function (url, query) {
        return ajax('GET', url, query);
    },
    post: function (url, data, options) {
        return ajax('POST', url, data, options);
    }
};
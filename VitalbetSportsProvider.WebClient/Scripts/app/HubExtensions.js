var app = app || {};

(function (app) {
    "use strict";

    var isConnected = false;
    var scheduledLoad = [];

    const onFail = (error) => console.error(error.message);

    var onHubStarted = () => {
        isConnected = true;
        for (let i = 0; i < scheduledLoad.length; i++) {
            scheduledLoad[i]();
        }
    };

    var startHub = () => $.connection.hub.start().done(onHubStarted).fail(onFail);

    $.connection.hub.error(onFail);
    $.connection.hub.reconnected(onHubStarted);
    $.connection.hub.disconnected(() => {
        isConnected = false;
        setTimeout(startHub, 1000);
    });
    
    class Hub {
        call(data) {
            if (isConnected) {
                const hub = $.connection[data.hub];
                const params = data.params || [];
                const onDone = function (result) {
                    if (data.onDone) {
                        if (!result) {
                            throw Error("Null Result: {0}.{1}({2})".format(data.hub, data.method, params.join(", ")));
                        }

                        data.onDone(result)
                    }
                };
                hub.server[data.method].apply(this, params).done(onDone).fail(onFail);
            } else {
                scheduledLoad.push(() => this.call(data));
            }
        }
    }

    app.hub = app.hub || new Hub();
    startHub();

})(app);
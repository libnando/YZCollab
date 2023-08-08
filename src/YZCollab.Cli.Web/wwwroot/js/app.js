const app = (function () {

    async function start(user) {
        try {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl(`https://localhost:7202/hub?user=${user}`)
                .configureLogging(signalR.LogLevel.Information)
                .build();

            connection.on("RegisterLog", (message) => {
                const li = document.createElement("li");
                li.textContent = `${message}`;
                document.getElementById("z-logs").querySelector("ul").appendChild(li);
            });

            connection.on("RegisterUser", (message) => {
                const li = document.createElement("li");
                li.textContent = `${message}`;
                document.getElementById("z-users").querySelector("ul").appendChild(li);
            });

            connection.onclose(async () => {
                await start();
            });

            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    function init(user) {
        start(user);
    }

    return {
        init: init
    };
})();
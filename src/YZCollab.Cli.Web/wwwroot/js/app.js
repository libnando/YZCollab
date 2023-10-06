const app = (function () {

    const modal = document.getElementById("z-modal");
    const form = modal.querySelector("form");
    const userInput = form.querySelector("input[name='user']");
    const btnSubmit = form.querySelector("button[name='submit']");
    const keySubmit = 13;

    function submit(event) {
        event.preventDefault();
        modal.style.display = "none";

        hub.init(userInput.value);
    }

    form.addEventListener("submit", function (event) {
        event.preventDefault();
    });

    btnSubmit.addEventListener("click", function (event) {
        submit(event);
    });

    userInput.addEventListener("keydown", function (event) {
        if (event.keyCode === keySubmit) {
            submit(event);

            return false;
        }
    });

    function init() {
        modal.style.display = "block";
    }

    return {
        init: init
    };
})();


const hub = (function () {

    function registerEvent(section, message) {
        const li = document.createElement("li");
        li.textContent = `${message}`;
        section.prepend(li);
    }

    async function start(user) {
        try {
            const urlHub = document.querySelector("main").getAttribute("data-url-hub");
            const logsSectionList = document.getElementById("z-logs").querySelector("ul");
            const usersSectionList = document.getElementById("z-users").querySelector("ul");

            const connection = new signalR.HubConnectionBuilder()
                .withUrl(`${urlHub}?user=${user}`)
                .configureLogging(signalR.LogLevel.Information)
                .build();

            connection.on("RegisterLog", (message) => {
                registerEvent(logsSectionList, message);
            });

            connection.on("RegisterUser", (message) => {
                registerEvent(usersSectionList, message);
            });

            connection.onclose(async () => {
                await start(user);
            });

            await connection.start();

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

app.init();
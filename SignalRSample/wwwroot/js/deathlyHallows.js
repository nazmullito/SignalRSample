var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
var wandSpan = document.getElementById("wandCounter");

//Create Connection
var connectionDeathlyHallows = new signalR.HubConnectionBuilder().withUrl("/hubs/deathlyhallows").build();

//connect to methods that hub invokes aka receive notifications from hub
connectionDeathlyHallows.on("updateDeathlyHallowCount", (cloak, stone, wand) => {
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
})

//invoke hub methods aka send notification to hub

//start connection
function fulfilled() {
    console.log("Connection to User Hub Successful");
    connectionDeathlyHallows.invoke("GetRaceStatus").then((raceCounter) => {
        cloakSpan.innerText = raceCounter.cloak.toString();
        stoneSpan.innerText = raceCounter.stone.toString();
        wandSpan.innerText = raceCounter.wand.toString();
    })
}

function rejected() {

}
connectionDeathlyHallows.start().then(fulfilled, rejected);

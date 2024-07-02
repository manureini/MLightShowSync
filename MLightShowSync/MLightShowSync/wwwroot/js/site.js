
window.setImage = async (imageElementId, imageStream) => {
    const arrayBuffer = await imageStream.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const image = document.getElementById(imageElementId);
    image.onload = () => {
        URL.revokeObjectURL(url);
    }
    image.src = url;
}

window.addDefaultPreventingHandler = function (element, eventName) {
    element.addEventListener(eventName, e => e.preventDefault(), { passive: false });
}


let dotnetObj = null;

window.setKeyListener = function (dotnetObjRef) {
    dotnetObj = dotnetObjRef;
}


window.loadAudioFile = (src) => {
    var audio = document.getElementById('player');
    var audioSource = document.getElementById('playerSource');

    if (audioSource.src !== src) {
        audioSource.src = src;
        audio.load();
    }
}

window.playAudio = () => {
    var audio = document.getElementById('player');
    audio.play();
}

window.pauseAudio = () => {
    var audio = document.getElementById('player');
    audio.pause();
}

window.seekAudio = (position) => {
    var audio = document.getElementById('player');
    audio.currentTime = position;
}

window.getAudioPosition = () => {
    var audio = document.getElementById('player');
    return audio.currentTime;
}


(function () {

    window.addEventListener('keypress', function (e) {
        if (dotnetObj != null) {
            dotnetObj.invokeMethodAsync('OnKeyDown', e.code);
        }
    });

})();


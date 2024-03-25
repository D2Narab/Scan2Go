// wwwroot/jsInterop.js
function setupFaceLivenessListener() {
    const component = document.querySelector('face-liveness');
    
    function listener(event) {
        if (event.detail) {
            const response = event.detail;
            console.log(response);
        }
    }

    /*component.addEventListener('face-liveness', listener);*/
    component.addEventListener('face-liveness', (event) => console.log(event.detail));
}

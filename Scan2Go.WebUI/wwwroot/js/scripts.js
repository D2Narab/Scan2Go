window.cameraInterop = {
    videoElement: null,
    stream: null,

    startCamera: function (videoElementId) {
        if (navigator.mediaDevices.getUserMedia) {
            navigator.mediaDevices.getUserMedia({ video: true })
                .then(stream => {
                    // Camera access granted
                    this.stream = stream;
                    this.videoElement = document.getElementById(videoElementId);
                    if (this.videoElement) {
                        this.videoElement.srcObject = stream;
                    }
                })
                .catch(err => {
                    console.error(`An error occurred: ${err}`);
                    alert("Unable to access the camera. Please ensure you've granted camera access and that a camera is available.");
                });
        }
    },

    stopCamera: function () {
        if (this.stream) {
            this.stream.getTracks().forEach(track => track.stop());
            if (this.videoElement) {
                this.videoElement.srcObject = null; // Disconnect the stream from the video element
            }
            this.stream = null; // Clear the stream reference
            this.videoElement = null; // Clear the video element reference
        }
    },

    captureImage: function () {
        return new Promise((resolve, reject) => {
            if (!this.videoElement) {
                reject("Video element is not initialized.");
                return;
            }
            let canvas = document.createElement('canvas');
            canvas.width = this.videoElement.videoWidth;
            canvas.height = this.videoElement.videoHeight;
            canvas.getContext('2d').drawImage(this.videoElement, 0, 0);
            this.stream.getTracks().forEach(track => track.stop()); // Stop the camera stream
            resolve(canvas.toDataURL('image/png')); // Return the captured image as a base64 encoded string
        });
    }
};


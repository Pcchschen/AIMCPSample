window.speechToText = {
    recognition: null,
    transcript: "",
    start: function (dotNetHelper) {
        if (!('webkitSpeechRecognition' in window)) {
            alert('Speech recognition not supported');
            return;
        }
      
        this.transcript = "";
        const recognition = new webkitSpeechRecognition();
        recognition.lang = 'en - US'; // 或 'en-US'，根据需要
        recognition.interimResults = false;
        recognition.maxAlternatives = 1;
        recognition.continuous = true; // 关键：持续识别
        recognition.onresult = function (event) {
            for (let i = event.resultIndex; i < event.results.length; ++i) {
                if (event.results[i].isFinal) {
                    window.speechToText.transcript += event.results[i][0].transcript;
                }
            }
        };
        recognition.onerror = function (event) {
            alert('Speech recognition error: ' + event.error);
        };
        recognition.onend = function () {
            // 不自动回传结果，等 stop 时再回传
        };
        this.recognition = recognition;
        recognition.start();
    },
    stop: function (dotNetHelper) {
        if (this.recognition) {
            this.recognition.onend = function () {
                if (dotNetHelper) {
                    dotNetHelper.invokeMethodAsync('OnSpeechRecognized', window.speechToText.transcript);
                }
                window.speechToText.recognition = null;
            };
            this.recognition.stop();
        }
    }
};
document.getElementById.addEventListener('keydown', function(event) {
    if (event.code === 'Enter') {
        const question = document.getElementById('ask').value;
        window.electron.sendQuestion(question);
    }
});

window.electron.onAnswer((answer) => {
    document.getElementById('answer').innerText = answer;
});

// Write your Javascript code.
$(document).ready(function () {
    var dropZone = $('#Text'),
        maxFileSize = 1000000; // максимальный размер файла - 1 мб.

    if (typeof (window.FileReader) === 'undefined') {
        dropZone.text('Не поддерживается браузером!');
        dropZone.addClass('error');
    }
    dropZone[0].ondragover = function () {
        dropZone.addClass('hover');
        return false;
    };

    dropZone[0].ondragleave = function () {
        dropZone.removeClass('hover');
        return false;
    };
    dropZone[0].ondrop = function (event) {
        event.preventDefault();
        dropZone.removeClass('hover');
        dropZone.addClass('drop');

        var file = event.dataTransfer.files[0],
            reader = new FileReader();

        reader.onload = function (event) {
            console.log(event.target);
            dropZone.val(event.target.result);
        };
        console.log(file);
        reader.readAsText(file)

        if (file.size > maxFileSize) {
            dropZone.text('Файл слишком большой!');
            dropZone.addClass('error');
            return false;
        }
    };
});
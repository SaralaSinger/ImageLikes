$(() => {
    const id = $("#image-id").val();
    $("#like-button").on('click', function () {       
        $.post('/home/updatelikes', { id }, function () {
        });
        $("#like-button").prop('disabled', true)
    });

    setInterval(() => {
        $.get('/home/getimage', { id }, image => {
            $("#likes-count").text(image.likes);
        })
    }, 400);        
    

    });


var loginController = function () {
    this.initialize = function () {
        registerEvents();
    };
    function registerEvents() {
        $("#login_form").submit(function (e) {
            e.preventDefault(); // avoid to execute the actual submit of the form.

            var form = $(this);
            var formModel = {
                customerName: document.getElementById("customerName").value
            };


            var url = form.attr('action');
            $.ajax({
                url: url,
                type: 'POST',
                data: formModel,
                success: function (data) {
                    window.location.href = '/Home/Index';
                },
                beforeSend: function () {
                    $('#contact-loader').show();
                    $('#btn-login').hide();
                },
                cache: false,
                error: function (err) {
                    //hide loading
                    $('#contact-loader').hide();
                    $('#btn-login').show();
                    // end hide loading
                    $('#message-result').html('');
                    if (err.status === 400 && err.responseText) {
                        var errMsgs = JSON.parse(err.responseText);
                        $(".kt-error-btm").remove();

                        for (field in errMsgs) {
                            switch (field) {
                                case 'CustomerName':
                                    $('<span class="text-danger d-block mt-2 kt-error-btm">' +
                                        errMsgs[field] +
                                        '</span>').insertAfter("#customerName");
                                    break;
                                default:
                                    $('#message-result').append(errMsgs[field] + '<br>');
                                    break;
                            }
                        }

                    }
                }
            });

        });
    }
}
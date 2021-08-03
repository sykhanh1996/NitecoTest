var layout = function () {
    this.initialize = function () {
        registerEvents();
    };
    function registerEvents() {
        $("#btn-logout").click(function (e) {
            e.preventDefault(); // avoid to execute the actual submit of the form.
            var url = '/Login/LogOut';
            $.ajax({
                url: url,
                type: 'POST',
                success: function () {
                    window.location.href = '/Login'; 
                },
                cache: false,
                error: function () {
                    //hide loading
               
                }
            });

        });
    }
}
var homeController = function () {
    this.initialize = function () {
        registerEvents();
    };
    function registerEvents() {
        $("#search_form").submit(function (e) {
            e.preventDefault(); // avoid to execute the actual submit of the form.

            var search = document.getElementById("input-search").value;
            window.location.href = '/Home/Index?filter=' + search;
        });

        $("#createOrder").click(function () {
            document.getElementById("btnmodal").click();
        });
        $("#btnModalCreateOrder").click(function (e) {
            e.preventDefault(); // avoid to execute the actual submit of the form.
            var order = {
                CustomerId: document.getElementById("customer").value, 
                ProductId: document.getElementById("product").value,
                Amount: document.getElementById("amount").value,
                OrderDate: document.getElementById("datepicker").value
            }
            var url = '/Home/CreateNewOrder';
            $.ajax({
                url: url,
                type: 'POST',
                data: order,
                success: function () {
                    window.location.href = '/Home/Index';
                },
                cache: false,
                error: function (err) {
                    if (err.status === 404) {
                        alert('Amount of Order is greater than Quantity of Product!');
                    }
                    document.getElementById("btnModalCancel").click();
                }
            });
        });
  
        $("#datepicker").datepicker();
  
    }
}
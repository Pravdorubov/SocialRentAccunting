trySetHouse = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#modDialog').modal('hide');
                    $.ajax({
                        url: "SetHouseComponent?id=" + res.houseId,
                        type: "get",
                        dataType: "html",
                        beforeSend: function (x) { },
                        data: null,
                        success: function (result) {
                            $("#house").html(result);
                        }
                    });
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

$("#CreateHouse").click(function () {
    $.ajax({
        url: "CreateHouse",
        type: "get",
        dataType: "html",
        beforeSend: function (x) { },
        data: null,
        success: function (result) {
            $('#dialogContent').html(result);
            $('#modDialog').modal('show');

        }
    });

});




$("#FindHouse").click(function () {
    $.ajax({
        url: "GetHouseSearchComponent",
        type: "get",
        dataType: "html",
        beforeSend: function (x) { },
        data: null,
        success: function (result) {

            $('#dialogContent').html(result);
            $('#modDialog').modal('show');

            $('#housesSearch tfoot th').each(function () {
                var title = $(this).text().trim();
                $(this).html('<input type="text" placeholder="'+title+'" />');
            });

            var table = $('#housesSearch').DataTable({
                initComplete: function () {
                    this.api().columns().every(function () {
                        var that = this;
                        $('input', this.footer()).on('keyup change clear', function () {
                            if (that.search() !== this.value) {
                                that.search(this.value).draw();
                            }
                        });
                    });
                },
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Russian.json"
                },
                searchable: false,
                ordering: false
            });

            $(".chooseHouse").click(function () {
                var id = $(this).val();
                $('#modDialog').modal('hide');
                $.ajax({
                    url: "SetHouseComponent?id=" + id,
                    type: "get",
                    dataType: "html",
                    beforeSend: function (x) { },
                    data: null,
                    success: function (result) {
                        $("#house").html(result);
                    }
                });
            });
        }
    });
});
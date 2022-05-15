trySetLandlord = form => {
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
                        url: "SetLandlordComponent?id=" + res.landlordId,
                        type: "get",
                        dataType: "html",
                        beforeSend: function (x) { },
                        data: null,
                        success: function (result) {
                            $("#landlord").html(result);
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

$("#CreateLandlord").click(function () {
    $.ajax({
        url: "CreateLandlord",
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




$("#FindLandlord").click(function () {
    $.ajax({
        url: "GetLandlordSearchComponent",
        type: "get",
        dataType: "html",
        beforeSend: function (x) { },
        data: null,
        success: function (result) {

            $('#dialogContent').html(result);
            $('#modDialog').modal('show');

            $('#landlordsSearch tfoot th').each(function () {
                var title = $(this).text().trim();
                $(this).html('<input type="text" placeholder="'+title+'" />');
            });

            var table = $('#landlordsSearch').DataTable({
                initComplete: function () {
                    this.api().columns().every(function () {
                        var that = this;
                        $('input', this.footer()).on('keyup change clear', function () {
                            if (that.search() !== this.value) {
                                that.search(this.value).draw();
                            }
                        });
                    });
                },,
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Russian.json"
                },
                searchable: false,
                ordering: false
            });

            $(".chooseLandlord").click(function () {
                var id = $(this).val();
                $('#modDialog').modal('hide');
                $.ajax({
                    url: "SetLandlordComponent?id=" + id,
                    type: "get",
                    dataType: "html",
                    beforeSend: function (x) { },
                    data: null,
                    success: function (result) {
                        $("#landlord").html(result);
                    }
                });
            });
        }
    });
});
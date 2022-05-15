trySetTenant = form => {
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
                        url: "SetTenantComponent?id=" + res.tenantId,
                        type: "get",
                        dataType: "html",
                        beforeSend: function (x) { },
                        data: null,
                        success: function (result) {
                            $("#tenant").html(result);
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

$("#CreateTenant").click(function () {
    $.ajax({
        url: "CreateTenant",
        type: "get",
        dataType: "html",
        beforeSend: function (x) { },
        data: null,
        success: function (result) {
            $('#dialogContent').html(result);
            $('#modDialog').modal('show');

            $("#addRow").click(function () {
                var rowCount = parseInt($("#totalLans").val());
                rowCount++;
                $("#totalLans").val(rowCount);
                $.ajax({
                    url: "/Tenants/GetKinsmanCreateComponent?count=" + rowCount,
                    type: "get",
                    dataType: "html",
                    beforeSend: function (x) { },
                    data: null,
                    success: function (result) {
                        $("#newRow").append(result)
                    }
                });
            });

            $(document).on('click', '#removeRow', function () {
                var rowCount = parseInt($("#totalLans").val());
                rowCount--;
                $("#totalLans").val(rowCount);
                $(this).closest('#inputFormRow').remove();
            });
        }
    });

});




$("#FindTenant").click(function () {
    $.ajax({
        url: "GetTenantSearchComponent",
        type: "get",
        dataType: "html",
        beforeSend: function (x) { },
        data: null,
        success: function (result) {

            $('#dialogContent').html(result);
            $('#modDialog').modal('show');

            $('#tenantsSearch tfoot th').each(function () {
                var title = $(this).text().trim();
                $(this).html('<input type="text" placeholder="'+title+'" />');
            });

            var table = $('#tenantsSearch').DataTable({
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
            });

            $(".chooseTenant").click(function () {
                var id = $(this).val();
                $('#modDialog').modal('hide');
                $.ajax({
                    url: "SetTenantComponent?id=" + id,
                    type: "get",
                    dataType: "html",
                    beforeSend: function (x) { },
                    data: null,
                    success: function (result) {
                        $("#tenant").html(result);
                    }
                });
            });
        }
    });
});
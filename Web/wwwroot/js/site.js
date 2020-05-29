// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {
    $('#students_table').DataTable({
        "lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]],
        "order": [],
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }]
    });

    $('#pop').click(function () {
        $('#divcertpopup').toggle();
    });
    $('#certclose').click(function () {
        $('#divcertpopup').fadeOut();
    });

    $(window).on('resize', function () {
        var win = $(this);
        if (win.width() <= 700) {
            $('#students_table,#students_table2').removeClass('table-sm').addClass('table-responsive');
        }
        else {
            $('#students_table,#studnets_table2').removeClass('table-responsive-sm').addClass('table-sm');
        }

    });
});
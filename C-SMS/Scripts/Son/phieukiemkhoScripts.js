﻿$(document).ready(function () {
    var orderItems = [];
    //Add button click function
    $('#add').click(function () {
        //Check validation of inventory ballot detail
        var isValidItem = true;

        if ($('#maHangHoa').val().trim() == '') {
            isValidItem = false;
            $('#maHangHoa').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#maHangHoa').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#tenHangHoa').val().trim() == '') {
            isValidItem = false;
            $('#tenHangHoa').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#tenHangHoa').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#soLuongKiemTra').val().trim() != '' && !isNaN($('#soLuongKiemTra').val().trim()))) {
            isValidItem = false;
            $('#soLuongKiemTra').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#soLuongKiemTra').siblings('span.error').css('visibility', 'hidden');
        }

        //Add product to list if valid
        if (isValidItem) {

            var i, j;
            var string_value_product = $('#maHangHoa').val().trim();

            //get value Id
            var productID = string_value_product.slice(0, 10); // PR00001

            if (orderItems.length > 0) {
                var test = true;
                var productIdOfTable = "";
                var row = document.getElementById('productTable').rows.length;
                for (var i = 1; i < row; i++) {
                    productIdOfTable = document.getElementById("productTable").rows[i].cells[0].innerHTML;

                    if (productIdOfTable == productID) {
                        test = false;
                        $('#maHangHoa').siblings('span.error').css('visibility', 'visible');
                        break;
                    }
                }

                if (test == true) {
                    $('#maHangHoa').siblings('span.error').css('visibility', 'hidden');
                    orderItems.push({
                        MaHangHoa: $('#maHangHoa').val().trim(),
                        TenHangHoa: $("#tenHangHoa").val().trim(),
                        DonViTinh: $('#donViTinh').val().trim(),
                        SoLuongHienTai: parseInt($('#soLuongHienTai').val().trim()),
                        SoLuongKiemTra: parseInt($('#soLuongKiemTra').val().trim()),
                    });

                    //Clear fields
                    $('#maHangHoa').focus().val('');
                    $('#tenHangHoa').val('');
                    $('#donViTinh').val('');
                    $('#soLuongHienTai').val('');
                    $('#soLuongKiemTra').val('');

                    //populate inventory ballot detail
                    GeneratedItemsTable();
                }
            }

            if (orderItems.length == 0) {
                $('#productStock').siblings('span.error').css('visibility', 'hidden');
                orderItems.push({
                    MaHangHoa: $('#maHangHoa').val().trim(),
                    TenHangHoa: $("#tenHangHoa").val().trim(),
                    DonViTinh: $('#donViTinh').val().trim(),
                    SoLuongHienTai: parseInt($('#soLuongHienTai').val().trim()),
                    SoLuongKiemTra: parseInt($('#soLuongKiemTra').val().trim()),
                });

                //Clear fields
                $('#maHangHoa').focus().val('');
                $('#tenHangHoa').val('');
                $('#donViTinh').val('');
                $('#soLuongHienTai').val('');
                $('#soLuongKiemTra').val('');


                //populate inventory ballot detail
                GeneratedItemsTable();
            }
        }
    });

    //Print inventoy ballot when click button Print
    $('#print').click(function () {
        Print();
    });

    //Function print inventory ballot
    function Print() {
        var toPrint = document.getElementById('Items');
        var $table = $('<table id="productTables" style="border: solid; width:100%; text-align:center"/>');
        $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn Vị Tính</th><th>Số Lượng Hiện Có</th><th>Số Lượng Kiểm Tra</th></tr></thead>');
        var $tbody = $('<tbody/>');
        $.each(orderItems, function (i, val) {
            var $row = $('<tr style="border:solid">');
            $row.append($('<td/>').html(val.MaHangHoa));
            $row.append($('<td/>').html(val.TenHangHoa));
            $row.append($('<td/>').html(val.DonViTinh));
            $row.append($('<td/>').html(val.SoLuongHienTai));
            $row.append($('<td/>').html(val.SoLuongKiemTra));
            $tbody.append($row);
        });
        console.log("current", orderItems);
        $table.append($tbody);
        $('#Items').html($table);

        var popupWin = window.open('', '_blank', 'width=800,height=500'); //create new page     
        popupWin.document.open(); //open new page
        popupWin.document.write('<html><title>Phiếu kiểm kho</title><body onload="window.print()">')
        popupWin.document.write('<table style="border:solid; width:100%"; text-align:center">')
        popupWin.document.write('Thông tin phiếu kiểm kho');
        popupWin.document.write('<tr><td>')
        popupWin.document.write('Số phiếu kiểm kho: ');
        popupWin.document.write($('#soPhieuKiemKho').val().trim());
        popupWin.document.write('</td>')

        popupWin.document.write('<td>')
        popupWin.document.write('Ngày kiểm: ');
        popupWin.document.write($('#ngayKiemKho').val().trim());
        popupWin.document.write('</td></tr>')

        popupWin.document.write('<tr><td>')
        popupWin.document.write('Nhân viên: ');
        popupWin.document.write($('#tenNhanVien').val().trim());
        popupWin.document.write('</td>')

        popupWin.document.write('<td>')
        popupWin.document.write('Ghi chú: ');
        popupWin.document.write($('#ghiChu').val().trim());
        popupWin.document.write('</td></tr>')

        popupWin.document.write('</table>')

        popupWin.document.write('<br>');
        popupWin.document.write('Danh sách hàng hóa');
        popupWin.document.write(toPrint.innerHTML);

        popupWin.document.write('<p style="text-align:right">')
        popupWin.document.write('Nhân viên kiểm kho')
        popupWin.document.write('<br>')
        popupWin.document.write('(Ký tên)')
        popupWin.document.write('</p>')
        popupWin.document.write('</html>');
        popupWin.document.close();
    }

    //Save button click function
    $('#submit').click(function () {
        //validation of inventory ballot detail
        var isAllValid = true;
        if (orderItems.length == 0) {
            $('#orderItems').html('<span style="color:red;">Phải có ít nhất 1 hàng hóa</span>');
            isAllValid = false;
        }

        //Save if valid
        if (isAllValid) {
            var data = {
                SoPhieuKiemKho: $('#soPhieuKiemKho').val().trim(),
                NgayKiemKho: $('#ngayKiemKho').val().trim(),
                MaNhanVien: $('#maNhanVien').val().trim(),
                GhiChu: $('#ghiChu').val().trim(),
                chiTietPhieuKiemKho: orderItems
            }

            $(this).val('Please wait...');

            $.ajax({
                url: "/KiemKho/LuuPhieuKiemKho",
                type: "POST",
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: "application/json",
                success: function (d) {
                    //check is successfully save to database
                    if (d.status == true) {
                        //will send status from server side
                        //clear form
                        orderItems = [];
                        $('#soPhieuKiemKho').val('');
                        $('#ngayKiemKho').val('');
                        $('#tenNhanVien').val('');
                        $('#ghiChu').val('');
                        $('#orderItems').empty();
                        window.location.href = '/Admin/KiemKho/';
                    }
                    else {
                        SetAlert("Something wrong! Please try again", "error");
                    }
                    $('#submit').val('Lưu Phiếu Kiểm Kho');
                },
                error: function () {
                    alert('Error. Please try again.');
                    $('#submit').val('Lưu Phiếu Kiểm Kho');
                }
            });
        }
    });

    //function for show added product in table
    function GeneratedItemsTable() {
        if (orderItems.length > 0) {
            var $table = $('<table id="productTable" style="max-width:970px" />');
            $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn Vị Tính</th><th>Số Lượng Hiện Có</th><th>Số Lượng Kiểm Tra</th><th>Hành Động</th></tr></thead>');
            var $tbody = $('<tbody/>');
            $.each(orderItems, function (i, val) {
                var $row = $('<tr/>');
                $row.append($('<td/>').html(val.MaHangHoa));
                $row.append($('<td/>').html(val.TenHangHoa));
                $row.append($('<td/>').html(val.DonViTinh));
                $row.append($('<td/>').html(val.SoLuongHienTai));
                $row.append($('<td/>').html(val.SoLuongKiemTra));
                var $remove = $('<input type="button" value="Xóa" style="padding:1px 20px" class="btn-danger"/>');
                $remove.click(function (e) {
                    e.preventDefault();
                    orderItems.splice(i, 1);
                    GeneratedItemsTable();
                });
                $row.append($('<td/>').html($remove));
                $tbody.append($row);
            });
            console.log("current", orderItems);
            $table.append($tbody);
            $('#orderItems').html($table);
        }
        else {
            $('#orderItems').html('');
        }
    }
});

// function only enter number
function checkNumber(e, element) {
    var charcode = (e.which) ? e.which : e.keyCode;
    //Check number
    if (charcode > 31 && (charcode < 48 || charcode > 57)) {
        return false;
    }
    return true;
}

//hidden error when user enter into textbox productID
function HideErrorProductName() {
    if (document.getElementById('tenHangHoa').value != '') {
        $('#tenHangHoa').siblings('span.error').css('visibility', 'hidden');
    }
}

$(document).ready(function () {
    $('#maHangHoa').on("input", function () {
        ClearValue();
        $.getJSON('/KiemKho/LoadThongTinHangHoa',
                    { id: $('#maHangHoa').val() },
                    function (data) {
                        if (data != null) {
                            $.each(data, function (index, row) {


                                $("#tenHangHoa").val(row.TenHangHoa);
                                $("#soLuongHienTai").val(row.SoLuongTon);
                                $("#donViTinh").val(row.DonViTinh);


                            });
                        }
                        else {

                        }
                    });
    });
})

function ClearValue() {
    $("#productName").val('');
    $("#unitName").val('');
    $("#currentQuantity").val('');

    $('#productID').siblings('span.error').css('visibility', 'hidden');
    $('#productName').siblings('span.error').css('visibility', 'hidden');
    $('#checkQuantity').siblings('span.error').css('visibility', 'hidden');
}
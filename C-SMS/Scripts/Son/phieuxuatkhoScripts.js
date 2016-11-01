$(document).ready(function () {
    var orderItems = [];
    $('#add').click(function () {
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

        if (!($('#soLuongXuat').val().trim() != '' && !isNaN($('#soLuongXuat').val().trim()))) {
            isValidItem = false;
            $('#soLuongXuat').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#soLuongXuat').siblings('span.error').css('visibility', 'hidden');
        }

        if (isValidItem) {

            var i, j;
            var string_value_product = $('#maHangHoa').val().trim();

            var productID = string_value_product.slice(0, 10); 

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
                        SoLuong: parseInt($('#soLuongXuat').val().trim()),
                        GiaNhap: parseInt($('#gia').val().trim()),
                        ThanhTien: parseInt($('#soLuongXuat').val().trim()) * parseInt($('#gia').val().trim()),
                    });

                    $('#maHangHoa').val('');
                    $('#tenHangHoa').val('');
                    $('#donViTinh').val('');
                    $('#soLuongTon').val('');
                    $('#soLuongXuat').val('');
                    $('#gia').val('');
                    $('#thanhTien').val('');
                    GeneratedItemsTable();
                    SumTotalAmount();
                }
            }

            if (orderItems.length == 0) {
                $('#productStock').siblings('span.error').css('visibility', 'hidden');
                orderItems.push({
                    MaHangHoa: $('#maHangHoa').val().trim(),
                    TenHangHoa: $("#tenHangHoa").val().trim(),
                    DonViTinh: $('#donViTinh').val().trim(),
                    SoLuong: parseInt($('#soLuongXuat').val().trim()),
                    Gia: parseInt($('#gia').val().trim()),
                    ThanhTien: parseInt($('#soLuongXuat').val().trim()) * parseInt($('#gia').val().trim()),
                });
            
                $('#maHangHoa').val('');
                $('#tenHangHoa').val('');
                $('#donViTinh').val('');
                $('#soLuongTon').val('');
                $('#soLuongXuat').val('');
                $('#gia').val('');
                $('#thanhTien').val('');
                GeneratedItemsTable();
                SumTotalAmount();
            }
        }
    });

    $('#print').click(function () {
        Print();
    });

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

    $('#submit').click(function () {
        var isAllValid = true;
        if (orderItems.length == 0) {
            $('#orderItems').html('<span style="color:red;">Phải có ít nhất 1 hàng hóa</span>');
            isAllValid = false;
        }

        if (isAllValid) {
            var data = {
                SoPhieuXuatKho: $('#soPhieuXuatKho').val().trim(),
                NgayXuatKho: $('#ngayXuatKho').val().trim(),
                MaNhanVien: $('#maNhanVien').val().trim(),
                TongTien: parseFloat($('#tongTien').val().trim().replace(/,/gi, "")),
                LyDoXuat: $('#lyDoXuat').val().trim(),
                chiTietPhieuXuatKho: orderItems
            }

            $(this).val('Please wait...');

            $.ajax({
                url: "/XuatKho/LuuPhieuXuatKho",
                type: "POST",
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: "application/json",
                success: function (d) {
                    if (d.status == true) {
                        orderItems = [];
                        $('#soPhieuXuatKho').val('');
                        $('#ngayXuatKho').val('');
                        $('#tenNhanVien').val('');
                        $('#lyDoXuat').val('');
                        $('#orderItems').empty();
                        window.location.href = '/Admin/XuatKho/';
                    }
                    else {
                        SetAlert("Something wrong! Please try again", "error");
                    }
                    $('#submit').val('Lưu Phiếu Xuất Kho');
                },
                error: function () {
                    $('#submit').val('Lưu Phiếu Xuất Kho');
                }
            });
        }
    });

    function GeneratedItemsTable() {
        if (orderItems.length > 0) {
            var $table = $('<table id="productTable" style="max-width:970px" />');
            $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn Vị Tính</th><th>Số Lượng Xuất</th><th>Giá</th><th>Thành Tiền</th><th> Hành Động</th></tr></thead>');
            var $tbody = $('<tbody/>');
            $.each(orderItems, function (i, val) {
                var $row = $('<tr/>');
                $row.append($('<td/>').html(val.MaHangHoa));
                $row.append($('<td/>').html(val.TenHangHoa));
                $row.append($('<td/>').html(val.DonViTinh));
                $row.append($('<td/>').html(val.SoLuong));
                $row.append($('<td/>').html(val.Gia));
                $row.append($('<td/>').html(val.ThanhTien));
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

    function SumTotalAmount() {
        var amount;

        var total = 0.0;
        var row = document.getElementById('productTable').rows.length;
        for (var i = 1; i < row; i++) {
            amount = document.getElementById("productTable").rows[i].cells[5].innerHTML.replace(/,/gi, "");

            total += parseFloat(amount);
        }
        $('#tongTien').val(formatNumber(parseFloat(total)));
    }

});

function checkNumber(e, element) {
    var charcode = (e.which) ? e.which : e.keyCode;
    if (charcode > 31 && (charcode < 48 || charcode > 57)) {
        return false;
    }
    return true;
}

function HideErrorTenHangHoa() {
    if (document.getElementById('maHangHoa').value != '') {
        $('#maHangHoa').siblings('span.error').css('visibility', 'hidden');
    }
}

function HideErrorTenHangHoa() {
    if (document.getElementById('tenHangHoa').value != '') {
        $('#tenHangHoa').siblings('span.error').css('visibility', 'hidden');
    }
}

function HideErrorSoLuongXuat() {
    if (document.getElementById('soLuongXuat').value != '') {
        $('#soLuongXuat').siblings('span.error').css('visibility', 'hidden');
    }
}

//$(document).ready(function () {
//    $('#maHangHoa').on("input", function () {
//        //ClearValue();
//        $.getJSON('/XuatKho/LoadThongTinHangHoa',
//                    { id: $('#maHangHoa').val() },
//                    function (data) {
//                        if (data != null) {
//                            $.each(data, function (index, row) {
//                                $("#tenHangHoa").val(row.TenHangHoa);
//                                $("#donViTinh").val(row.DonViTinh);
//                                $("#soLuongTon").val(row.SoLuongTon);
//                                $("#gia").val(row.GiaBan);

//                            });
//                        }
//                    });
//    });
//})

$(document).ready(function () {
    $('#maHangHoa').on("input", function () {
        ClearValue();
        $.getJSON('/XuatKho/LoadThongTinHangHoa',
                    { id: $('#maHangHoa').val() },
                    function (data) {
                        if (data != null) {
                            $.each(data, function (index, row) {
                                $("#tenHangHoa").val(row.TenHangHoa);
                                $("#donViTinh").val(row.DonViTinh);
                                $("#soLuongTon").val(row.SoLuongTon);
                                $("#gia").val(row.GiaBan);
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

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}

$(document).ready(function () {
    $('#soLuongXuat').on("input", function () {      
        var a = document.getElementById("gia").value;
        var b = document.getElementById("soLuongXuat").value;
        var x = Number(a) * Number(b);
        document.getElementById("thanhTien").value = x;
    });
})

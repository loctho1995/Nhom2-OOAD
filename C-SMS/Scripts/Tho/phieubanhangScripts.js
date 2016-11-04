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

        if (!($('#soLuong').val().trim() != '' && !isNaN($('#soLuong').val().trim()))) {
            isValidItem = false;
            $('#soLuong').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#soLuong').siblings('span.error').css('visibility', 'hidden');
        }
        
        //Add product to list if valid
        if (isValidItem) {
            var i, j;
            var string_value_product = $('#maHangHoa').val().trim();

            //get value IdorderItems.pus
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
                        SoPhieuBanHang: $('#soPhieuBanHang').val().trim(),
                        MaHangHoa: $('#maHangHoa').val().trim(),
                        TenHangHoa: $("#tenHangHoa").val().trim(),
                        Gia: $('#donGia').replace(/,/gi, "").val().trim(),
                        SoLuong: parseInt($('#soLuong').val().trim()),
                        ThanhTien: parseInt($('#thanhTien').val().trim().replace(/,/gi, "")),
                    });

                    //Clear fields
                    $('#maHangHoa').focus().val('');
                    $('#tenHangHoa').val('');
                    $('#donGia').val('');
                    $('#soLuong').val('');
                    $('#thanhTien').val('');

                    //populate inventory ballot detail
                    GeneratedItemsTable();
                }
            }

            if (orderItems.length == 0) {
                $('#productStock').siblings('span.error').css('visibility', 'hidden');
                orderItems.push({
                    SoPhieuBanHang: $('#soPhieuBanHang').val().trim(),
                    MaHangHoa: $('#maHangHoa').val().trim(),
                    TenHangHoa: $("#tenHangHoa").val().trim(),
                    Gia: $('#donGia').val().trim().replace(/,/gi, ""),
                    SoLuong: parseInt($('#soLuong').val().trim()),
                    ThanhTien: parseInt($('#thanhTien').val().trim().replace(/,/gi, "")),                    
                });

                //Clear fields
                $('#maHangHoa').focus().val('');
                $('#tenHangHoa').val('');
                $('#donGia').val('');
                $('#soLuong').val('');
                $('#thanhTien').val('');


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
                SoPhieuBanHang: $('#soPhieuBanHang').val().trim(),
                NgayBan: $('#ngayBan').val().trim(),
                TenNhanVien: $('#tenNhanVien').val().trim(),
                MaNhanVien: $('#maNhanVien').val().trim(),
                GhiChu: $('#ghiChu').val().trim(),
                TenKhachHang: $('#tenKhachHang').val().trim(),
                SoDienThoai: $('#soDienThoai').val().trim(),
                TongTien: $('#tongTien').val().replace(/,/gi, "").trim(),
                chiTietPhieuBanHang: orderItems
            }

            $(this).val('Please wait...');

            $.ajax({
                url: "/BanHang/LuuPhieuBanHang",
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
                        $('#soPhieuBanHang').val('');
                        $('#ngayBan').val('');
                        $('#tenNhanVien').val('');
                        $('#ghiChu').val('');
                        $('#orderItems').empty();
                        window.location.href = '/Admin/BanHang/';
                    }
                    else {
                        alert("Something wrong! Please try again", "error");
                    }
                    $('#submit').val('Lưu Phiếu Bán Hàng');
                },
                error: function () {
                    alert('Error. Please try again.');
                    $('#submit').val('Lưu Phiếu Bán Hàng');
                }
            });
        }
    });
    
    //function for show added product in table
    function GeneratedItemsTable() {
        if (orderItems.length > 0) {
            var $table = $('<table id="productTable" style="max-width:970px" />');
            $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn giá</th><th>Số lượng</th><th>Thành tiền</th><th>Hành Động</th></tr></thead>');
            var $tbody = $('<tbody/>');
            $.each(orderItems, function (i, val) {

                var $row = $('<tr/>');
                $row.append($('<td/>').html(val.MaHangHoa));
                $row.append($('<td/>').html(val.TenHangHoa));
                $row.append($('<td/>').html(formatNumber(val.Gia)));
                $row.append($('<td/>').html(val.SoLuong));
                $row.append($('<td/>').html(formatNumber(val.ThanhTien)));
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

        var tongTien = 0;
        for (var i = 0; i < orderItems.length; i++) {
            tongTien += parseInt(orderItems[i].ThanhTien);
        }

        document.getElementById('tongTien').value = formatNumber(tongTien);
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
        $.getJSON('/BanHang/LoadThongTinHangHoa',
                    { id: $('#maHangHoa').val() },
                    function (data) {
                        if (data != null) {
                            $.each(data, function (index, row) {
                                $("#tenHangHoa").val(row.TenHangHoa);
                                $("#donGia").val(formatNumber(row.GiaBan));
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

$(document).ready(function () {

    //this calculates values automatically
    Multiplica();
    $("#soLuong").on("keydown keyup", function () {
        Multiplica();
    });
});

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}

function Multiplica() {
    if (document.getElementById('soLuong').value == '' || document.getElementById('soLuong').value == 0) {
        document.getElementById('thanhTien').value = 0;
    }
    else {
        var unitPrice = document.getElementById('donGia').value.replace(/,/gi, "");
        var quantity = document.getElementById('soLuong').value;
        var result = parseInt(unitPrice) * parseInt(quantity);

        if (!isNaN(result)) {
            document.getElementById('thanhTien').value = formatNumber(result);
        }  
    }
}
//Lay model name theo ten san pham
$(document).ready(function () {
    $('#maHangHoa').on("change", function () {
        $.getJSON('/BaoHanh/LoadThongTinHangHoa',
                    { id: $('#maHangHoa').val() },
                    function (data) {
                        if (data != null) {
                            $.each(data, function (index, row) {
                                $("#modelName").val(row.ModelName);
                            });
                        }
                    });
    });
})
//
$(document).ready(function () {
    $('#submit').click(function () {
        //validation of inventory ballot detail
        var isAllValid = true;

        //Save if valid
        if (isAllValid) {
            var data = {
                SoPhieuBaoHanh: $('#soPhieuBaoHanh').val().trim(),
                NgayLap: $('#ngayLap').val().trim(),
                NgayGiao: $('#ngayGiao').val().trim(),
                MaNhanVien: $('#maNhanVien').val().trim(),
                TenNhanVien: $('#tenNhanVien').val().trim(),
                TenKhachHang: $('#tenKhachHang').val().trim(),
                SoDienThoai: $('#soDienThoai').val().trim(),
                GhiChu: $('#ghiChu').val().trim(),
                ModelName: $('#modelName').val().trim()
            }

            $(this).val('Please wait...');

            $.ajax({
                url: "/BaoHanh/LuuPhieuBaoHanh",
                type: "POST",
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: "application/json",
                success: function (d) {
                    //check is successfully save to database                    
                    if (d.status == true) {
                        //will send status from server side
                        //clear form
                        $('#soPhieuBaoHanh').val('');
                        $('#ngayLap').val('');
                        $('#tenNhanVien').val('');
                        $('#ghiChu').val('');
                        window.location.href = '/Admin/BaoHanh/';
                    }
                    else {
                        alert("Something wrong! Please try again", "error");
                    }
                    $('#submit').val('Lưu Phiếu Bảo Hành');
                },
                error: function () {
                    alert('Error. Please try again.');
                    $('#submit').val('Lưu Phiếu Bảo Hành');
                }
            });
        }
    });

    
});


$(document).ready(function () {
    //Add button click function
    $('#btNhanHang').click(function () {
    });

    $('#btThanhToan').click(function () {
    });
});
      
function createNhanHangDiv(id) {    
    document.write("<div id=NhanHang" + id + "> Chưa Nhận hàng </div> <a id= 'aNhanHang" + id + "' onclick='onBtNhanHangClicked(" + id + ")' class='btn btn-default'>Xác Nhận</a>"  );
}

function getNhanHangId(id) {
    return "NhanHang" + id;
}

function onBtNhanHangClicked(id) {
    var r = confirm("Bạn xác nhận khách hàng đã nhận được hàng?");

    if (r) {
        document.getElementById(getNhanHangId(id)).innerHTML = "Đã nhận hàng";

        var ele = document.getElementById("aNhanHang" + id);
        ele.outerHTML = "";
        delete ele;

        xacNhanNhanHang(id);

    } else {

    }
}

function createThanhToanDiv(id) {
    document.write("<div id=ThanhToan" + id + "> Chưa thanh toán </div> <a id= 'aThanhToan" + id + "' onclick='onBtThanhToanClicked(" + id + ")' class='btn btn-default'>Xác Nhận</a>");
}

function getThanhToanId(id) {
    return "ThanhToan" + id;
}

function onBtThanhToanClicked(id) {
    var r = confirm("Bạn xác nhận khách hàng đã thanh toán hóa đơn?");

    if (r) {
        document.getElementById(getThanhToanId(id)).innerHTML = "Đã thanh toán";

        var ele = document.getElementById("aThanhToan" + id);
        ele.outerHTML = "";
        delete ele;

        xacNhanThanhToan(id);

    } else {

    }
}

function xacNhanNhanHang(idx) {

    $.getJSON('/DatHang/XacNhanNhanHang',
                { id: idx },
                function (data) {
                });
}


function xacNhanThanhToan(idx) {

    $.getJSON('/DatHang/XacNhanThanhToan',
                { id: idx },
                function (data) {
                });
}
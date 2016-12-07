var BlankonFormValidation = function () {

    return {

        // =========================================================================
        // CONSTRUCTOR APP
        // =========================================================================
        init: function () {
            BlankonFormValidation.jqueryValidation();
        },

        // =========================================================================
        // JQUERY VALIDATION
        // =========================================================================
        jqueryValidation: function () {
            if($('#basic-validate').length){

                $.mockjax({
                    url: 'users.action',
                    response: function(settings) {
                        var user = settings.data.bv_username,
                            users = ["john", "peter", "bill", "jokowi"];
                        this.responseText = "true";
                        if ( $.inArray( user, users ) !== -1 ) {
                            this.responseText = 'false';
                        }
                    },
                    responseTime: 500
                });

                $('#basic-validate').validate({
                    rules:{
                        bv_required:{
                            required:true
                        },
                        email: {
                            required: true,
                            email: true
                        },

                        soLuongXuat: {
                            required: true,
                        },
                        tenloaihanghoa: {
                            required: true,
                        },
                        phantramloinhuan: {
                            required: true,
                            number: true,
                            min: 0,
                            max: 100
                        },
                        tennhacungcap: {
                            required:true,
                        },

                        tennhanvien: {
                            required: true,
                        },
                        tendangnhap: {
                            required: true,
                        },
                        makhau: {
                            required: true,
                        },
                        cmnd: {
                            required: true,
                        },

                        diachi: {
                            required:true,
                        },
                        sodienthoai: {
                            required: true,  
                        },
                        bv_username: {
                            required: true,
                            minlength: 2,
                            remote: "users.action"
                        }
                    },
                    messages: {
                        email: {
                            required: "Chưa nhập email!",
                            email: "Email không hợp lệ!",
                            remote: jQuery.validator.format("{0} is already in use")
                        },
                        tennhacungcap: {
                            required: "Chưa nhập tên nhà cung cấp !",
                            remote: jQuery.validator.format("{0} is already in use")
                        },

                        tenloaihanghoa: {
                            required: "Chưa nhập tên loại hàng hóa !",
                            remote: jQuery.validator.format("{0} is already in use")
                        },

                        tennhanvien: {
                            required: "Chưa nhập tên nhân viên !",
                            remote: jQuery.validator.format("{0} is already in use")
                        },

                        tendangnhap: {
                            required: "Chưa nhập tên đăng nhập!",
                            remote: jQuery.validator.format("{0} is already in use")
                        },

                        matkhau: {
                            required: "Chưa nhập mật khẩu !",
                            remote: jQuery.validator.format("{0} is already in use")
                        },
                        cmnd: {
                            required: "Chưa nhập chứng minh nhân dân !",
                            remote: jQuery.validator.format("{0} is already in use")
                        },


                        phantramloinhuan: {
                            required: "Chưa nhập phần trăm lợi nhuận !",
                            number: "Hãy nhập số!",
                            min: "Nhập lớn hơn hoặc bằng 0",
                            max: "Nhập nhỏ hơn 100",
                            remote: jQuery.validator.format("{0} is already in use")
                        },

                        soLuongXuat: {
                            required: "Nhập số lượng!",
                            remote: jQuery.validator.format("{0} is already in use")
                        },
                       
                        diachi: {
                            required: "Chưa nhập địa chỉ !",
                            remote: jQuery.validator.format("{0} is already in use")
                        },
                        sodienthoai: {
                            required: "Chưa nhập số điện thoại !",
                            remote: jQuery.validator.format("{0} is already in use")
                        },
                        
                        bv_username: {
                            remote: jQuery.validator.format("{0} is already in use")
                        }
                    },
                    highlight:function(element) {
                        $(element).parents('.form-group').addClass('has-error has-feedback');
                    },
                    unhighlight: function(element) {
                        $(element).parents('.form-group').removeClass('has-error');
                    },
                    //submitHandler: function() {
                    //    alert("submitted!");
                    //}
                });
            }

            if($('#number-validate').length){
                $('#number-validate').validate({
                    rules:{
                        nv_number:{
                            required:true,
                            number:true
                        },
                        nv_min:{
                            required: true,
                            min:5
                        },
                        nv_max:{
                            required:true,
                            max:5
                        }
                    },
                    highlight:function(element) {
                        $(element).parents('.form-group').addClass('has-error has-feedback');
                    },
                    unhighlight: function(element) {
                        $(element).parents('.form-group').removeClass('has-error');
                    },
                    submitHandler: function() {
                        alert("submitted!");
                    }
                });
            }

            if($('#password-validate').length){
                $('#password-validate').validate({
                    rules:{
                        pv_password:{
                            required: true,
                            minlength:5,
                            maxlength:20
                        },
                        pv_password2:{
                            required:true,
                            minlength:5,
                            maxlength:20,
                            equalTo:"#pv_password"
                        }
                    },
                    highlight:function(element) {
                        $(element).parents('.form-group').addClass('has-error has-feedback');
                    },
                    unhighlight: function(element) {
                        $(element).parents('.form-group').removeClass('has-error');
                    },
                    submitHandler: function() {
                        alert("submitted!");
                    }
                });
            }

            if($('#checkbox-radio-validate').length){
                $('#checkbox-radio-validate').validate({
                    rules:{
                        cr_gender:{
                            required: true
                        },
                        cr_skill:{
                            required:true,
                            minlength: 2
                        }
                    },
                    highlight:function(element) {
                        $(element).parents('.form-group').addClass('has-error has-feedback');
                    },
                    unhighlight: function(element) {
                        $(element).parents('.form-group').removeClass('has-error');
                    },
                    submitHandler: function() {
                        alert("submitted!");
                    }
                });
            }

            if($('#select-validate').length){
                $('#select-validate').validate({
                    rules:{
                        sv_skill_programming:{
                            required: true
                        },
                        sv_position:{
                            required: true
                        }
                    },
                    highlight:function(element) {
                        $(element).parents('.form-group').addClass('has-error has-feedback');
                    },
                    unhighlight: function(element) {
                        $(element).parents('.form-group').removeClass('has-error');
                    },
                    submitHandler: function() {
                        alert("submitted!");
                    }
                });
            }

            if($('#sample-validation-1').length){

                $.mockjax({
                    url: 'emails.action2',
                    response: function(settings) {
                        var email = settings.data.sv1_email,
                            emails = ["jokowi@jk.co.id", "george@bush.gov", "bill@gates.com"];
                        this.responseText = "true";
                        if ( $.inArray( email, emails ) !== -1 ) {
                            this.responseText = 'false';
                        }
                    },
                    responseTime: 500
                });

                $.mockjax({
                    url: 'users.action2',
                    response: function(settings) {
                        var user = settings.data.sv1_username,
                            users = ["john", "peter", "bill", "jokowi"];
                        this.responseText = "true";
                        if ( $.inArray( user, users ) !== -1 ) {
                            this.responseText = 'false';
                        }
                    },
                    responseTime: 500
                });

                $("#sample-validation-1").validate({
                    rules: {
                        sv1_firstname: "required",
                        sv1_lastname: "required",
                        sv1_username: {
                            required: true,
                            minlength: 2,
                            remote: "users.action2"
                        },
                        sv1_password: {
                            required: true,
                            minlength: 5
                        },
                        sv1_password_confirm: {
                            required: true,
                            minlength: 5,
                            equalTo: "#password"
                        },
                        sv1_email: {
                            required: true,
                            email: true,
                            remote: "emails.action2"
                        },
                        sv1_gender: "required",
                        sv1_terms: "required"
                    },
                    messages: {
                        sv1_firstname: "Enter your firstname",
                        sv1_lastname: "Enter your lastname",
                        sv1_username: {
                            required: "Enter a username",
                            minlength: jQuery.validator.format("Enter at least {0} characters"),
                            remote: jQuery.validator.format("{0} is already in use")
                        },
                        sv1_password: {
                            required: "Provide a password",
                            rangelength: jQuery.validator.format("Enter at least {0} characters")
                        },
                        sv1_password_confirm: {
                            required: "Repeat your password",
                            minlength: jQuery.validator.format("Enter at least {0} characters"),
                            equalTo: "Enter the same password as above"
                        },
                        sv1_email: {
                            required: "Email không hợp lệ",
                            minlength: "Email không hợp lệ",
                            remote: jQuery.validator.format("{0} is already in use")
                        },
                        sv1_gender: "Choose your gender",
                        sv1_terms: "Please check our terms of use again"
                    },
                    highlight:function(element) {
                        $(element).parents('.form-group').addClass('has-error has-feedback');
                    },
                    unhighlight: function(element) {
                        $(element).parents('.form-group').removeClass('has-error');
                    },
                    submitHandler: function() {
                        alert("submitted!");
                    }
                });
            }

            if($('#sample-validation-2').length){
                $("#sample-validation-2").validate({
                    rules: {
                        sv2_company_name: "required",
                        sv2_firstname: "required",
                        sv2_lastname: "required",
                        sv2_city: "required",
                        sv2_state: "required",
                        sv2_phone: {
                            required: true,
                            number: true
                        },
                        sv2_email: {
                            required: true,
                            email: true,
                            remote: "emails.action"
                        },
                        sv2_password: {
                            required: true,
                            minlength: 5
                        },
                        sv2_password_confirm: {
                            required: true,
                            minlength: 5,
                            equalTo: "#sv2_password"
                        },
                        sv2_credit_card: {
                            required: true,
                            minlength: 1
                        },
                        sv2_credit_card_number: {
                            required: true,
                            number: true
                        }
                    },
                    messages: {
                        sv2_company_name: "Enter your company name",
                        sv2_firstname: "Enter your firstname",
                        sv2_lastname: "Enter your lastname",
                        sv2_city: "Enter your city",
                        sv2_state: "Enter your state",
                        sv2_phone: "Enter your phone number",
                        sv2_username: {
                            required: "Enter a username",
                            minlength: jQuery.validator.format("Enter at least {0} characters")
                        },
                        sv2_email: {
                            required: "Email không hợp lệ",
                            minlength: "Email không hợp lệ"
                        },
                        sv2_password: {
                            required: "Provide a password",
                            rangelength: jQuery.validator.format("Enter at least {0} characters")
                        },
                        sv2_password_confirm: {
                            required: "Repeat your password",
                            minlength: jQuery.validator.format("Enter at least {0} characters"),
                            equalTo: "Enter the same password as above"
                        },
                        sv2_credit_card: {
                            required: "Choose your credit card",
                            minlength: jQuery.validator.format("Enter at least {0} credit card")
                        },
                        sv2_credit_card_number: {
                            required: "Please enter credit card number",
                            minlength: jQuery.validator.format("Enter at least {0} credit card")
                        }
                    },
                    highlight:function(element) {
                        $(element).parents('.form-group').addClass('has-error has-feedback');
                    },
                    unhighlight: function(element) {
                        $(element).parents('.form-group').removeClass('has-error');
                    },
                    submitHandler: function() {
                        alert("submitted!");
                    }
                });
            }
        }

    };

}();

// Call main app init
BlankonFormValidation.init();
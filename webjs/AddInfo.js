function changecode() {
    document.getElementById("checkimg").src = "/Admin/Images/tb.gif";
    document.getElementById("checkimg").src = "/Admin/checkCode.aspx?rnd=" + Math.random();
}
//验证邮箱
function CheckMail(mail) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (filter.test(mail)) return true;
    else return false;
}
//验证电话
function CheckTel(tel) {
    var filter = /^((0?1[358]\d{9})|((0(10|2[1-3]|[3-9]\d{2}))[-]?[1-9]\d{6,7}))$/;
    if (filter.test(tel)) return true;
    else return false;
}
$(function () {
    $('#BMInfo').submit(function () {
        if ($('#name').val().trim() == '') {
            alert('请输入姓名');
            $('#name').focus();
            return false;
        }
        if ($('#email').val().trim() == '') {
            alert('请输入邮箱');
            $('#email').focus();
            return false;
        } else {
            if (!CheckMail($('#email').val())) {
                alert('请输入正确的邮箱');
                $('#email').focus();
                return false;
            }
        }

        if ($('#tel').val().trim() == '') {
            alert('请输入电话号码');
            $('#tel').focus();
            return false;
        } else {
            if (!CheckTel($('#tel').val())) {
                alert('请提交正确的手机号码或电话号码,如：186****12541、0755888888或0755-888888');
                $('#tel').focus();
                return false;
            }
        }
        //        var checkcode = $.trim($(":input[name='checkcode']").val());
        //        if (checkcode == "") {
        //            alert("对不起，验证码不能为空！");
        //            changecode();
        //            $(":input[name='checkcode']").val();
        //            return false;
        //        }
        //        else {
        //            var bolError = true;
        //            $.ajax(
        //                                {
        //                                    url: "/ashx/CodeIsError.ashx",
        //                                    data: { "checkcode": checkcode, "rnd": Math.random() },
        //                                    dataType: "text",
        //                                    type: "get",
        //                                    success: function (e) {
        //                                        if (e == "1") {
        //                                            alert('对不起，您输入的验证码错误!');
        //                                            changecode();
        //                                            $(":input[name='checkcode']").focus();
        //                                            bolError = false;
        //                                            $(":input[name='checkcode']").val("");
        //                                        } else {

        //                                        }
        //                                    },
        //                                    async: false
        //                                }
        //                            );

        //            if (bolError == false) {
        //                return false;
        //            }
        //        }

        if ($('#Details').val().trim() == '') {
            alert('请输入留言内容');
            $('#Details').focus();
            return false;
        }
        $.ajax({
            type: 'get',
            url: '/ashx/AddLiuyan.ashx',
            data: $('#BMInfo').serialize(),
            success: function (data) {
                alert(data);
                window.location.reload();
                $('#name').val("");
                $('#email').val("");
                $('#Details').val("");
                $("#tel").val("");
                location.reload();
            }
        });
        return false;
    });
})

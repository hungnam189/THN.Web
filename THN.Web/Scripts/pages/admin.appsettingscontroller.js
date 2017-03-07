$(function(){
    
    $('#btnCompanyInfoGeneral').click(function(e){
       
       if($("#CompanyInfoGeneral").valid()){
           $.ajax({
            url: '/Admin/AppSettings/SaveInfoCompanyGeneral',
            type: 'POST',
            data:$('#CompanyInfoGeneral').serialize(),
            async: true,
            success: function (data) {
                loadWaiting(false);
                if(data.status == true){
                    $.notify("Thêm thành công!", "success");
                }
                else
                    $.notify("Thêm không thành công!", "warn");
            },      
            error: function (xhr, status, text) {
                if (xhr.status == 0) {//Error het session
                    alert('Bạn đã rời phiên làm việc quá lâu! Trang web sẽ tự động tải lại');
                    location.reload();
                }
            },
            beforeSend: function () {
                loadWaiting(true);
            }
        });
       }
       e.preventDefault();
    });
    
    $('#btnCompanyInfoSosial').click(function(e){
       $.ajax({
            url: '/Admin/AppSettings/SaveSosialInfoCompany',
            type: 'POST',
            data:$('#CompanyInfoSosial').serialize(),
            async: true,
            success: function (data) {
                loadWaiting(false);
                if(data.status == true){
                    $.notify(data.msg, "success");
                }
                else
                    $.notify(data.msg, "warn");
            },      
            error: function (xhr, status, text) {
                if (xhr.status == 0) {//Error het session
                    alert('Bạn đã rời phiên làm việc quá lâu! Trang web sẽ tự động tải lại');
                    location.reload();
                }
            },
            beforeSend: function () {
                loadWaiting(true);
            }
        });
       
        e.preventDefault();        
    });
    
    $('#btnCompanyInfoSEO').click(function(e){
       $.ajax({
            url: '/Admin/AppSettings/CompanyInfoSEO',
            type: 'POST',
            data:$('#CompanyInfoSEO').serialize(),
            async: true,
            success: function (data) {
                loadWaiting(false);
                if(data.status == true){
                    $.notify(data.msg, "success");
                }
                else
                    $.notify(data.msg, "warn");
            },      
            error: function (xhr, status, text) {
                if (xhr.status == 0) {//Error het session
                    alert('Bạn đã rời phiên làm việc quá lâu! Trang web sẽ tự động tải lại');
                    location.reload();
                }
            },
            beforeSend: function () {
                loadWaiting(true);
            }
        });
       
        e.preventDefault();        
    });
    
    
    
    $("#CompanyInfoGeneral").validate();
    
    
});
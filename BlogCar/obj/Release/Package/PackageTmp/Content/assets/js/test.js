//Ana Sayfa Araba Click Eventi
$(".cardetail").click(function () {
    var photo = $(this).children("span").children("img").attr("src");
    var model = $(this).children("header").children("h3").text();
    var kilometer = $(this).find("i:first").text();
    var power = $(this).find("i:eq(1)").text();
    var gearbox = $(this).find("i:eq(2)").text();
    var price = $(this).children("header").find("p:eq(1)").text();
    var properties = $(this).children("header").find("p:eq(2)").text().split("/");
    var modelyear = properties[0];
    var fueltype = properties[1];
    var color = properties[2];
    var id = $(this).find("button").val();
    Swal.fire({
        title: model,
        html: "Fiyat:" + price + "</br>" + "&nbsp;&nbsp;Kilometre:" + kilometer + "&nbsp;&nbsp;Motor Gücü:" + power + "</br>" + "&nbsp;&nbsp;Vites:" + gearbox +
            "&nbsp;&nbsp;&nbsp;&nbsp;Model Yılı: " + modelyear + "</br>" + "&nbsp;&nbsp;Yakıt Türü: " + fueltype + "&nbsp;&nbsp;Renk:" + color,
        imageUrl: photo,
        imageWidth: 1200,
        imageHeight: 250,
        imageAlt: model,
        confirmButtonText: 'İlanı Gör',
        showCancelButton: true,
        cancelButtonText: 'Kapat',
        allowEscapeKey: false

    }).then((result) => {
        if (result.dismiss === Swal.DismissReason.cancel || result.dismiss == Swal.DismissReason.backdrop)
            return;
        else
            window.location = "/Car/Detail/" + id;
    })
});

//İletişim Butonları Click Eventi
$(".contactbutton").click(async function () {
    var button = $(this);
    await Swal.fire({
        title: 'İletişim Formu',
        html:
            '</br>' +
            '<p>Ad Soyad<p>' +
            '<input id="Name" class="swal2-input">' +
            '<p>E-Posta<p>' +
            '<input id="Mail" class="swal2-input">' +
            '<p>Mesaj<p>' +
            '<textarea id="MessageText" class="swal2-input">',
        focusConfirm: true,
        showCancelButton: true,
        confirmButtonText: "Gönder",
        cancelButtonText: "Vazgeç",
        allowEscapeKey: false

    }).then((result) => {
        if (result.dismiss === Swal.DismissReason.cancel || result.dismiss == Swal.DismissReason.backdrop)
            return;
        var Message = {
            Name: document.getElementById('Name').value,
            Mail: document.getElementById('Mail').value,
            MessageText: document.getElementById('MessageText').value,
            CarId: $(this).val()
        };
        $.ajax({
            type: 'Post',
            url: '/Home/ContactForm/',
            data: { "message": Message },
            dataType: 'Json',
            success: function (control) {
                if (control == 1) {
                    Swal.fire({
                        type: 'success',
                        icon: 'success',
                        title: 'Başarılı',
                        text: 'Mesajınız başarıyla bize ulaştı.',
                        confirmButtonText: 'Tamam'

                    })
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: 'Lütfen tüm alanları eksiksiz doldurun.',
                        confirmButtonText: 'Tamam'

                    }).then(() => {
                        button.click();
                    })
                }
            }
        })
    })
});

$('.carousel').carousel({
    interval: 2000
});


$("#carMessage").on("click", function () {
    var Message = {
        Name: $("#Name").val(),
        Mail: $("#Mail").val(),
        MessageText: $("#MessageText").val(),
        CarId: $("#ilanno").val()
    }

    $.ajax({
        url: "/Car/Detail/",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'message': Message },
        success: function (control) {
            if (control == 1) {
                Swal.fire({
                    type: 'success',
                    icon: 'success',
                    title: 'Başarılı',
                    text: 'Mesajınız başarıyla bize ulaştı.',
                    confirmButtonText: 'Tamam'
                })
                $("#Name").val("");
                $("#Mail").val(""),
                    $("#MessageText").val("")
            }
            else
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Lütfen tüm alanları eksiksiz doldurun.',
                    confirmButtonText: 'Tamam'

                })
        }
    })

})

function carFocus(id) {
    var div = document.getElementById(id);
    div.style.backgroundColor = "darkslategray";
};

function carUnfocus(id) {
    var div = document.getElementById(id);
    div.style.backgroundColor = "slategray";
};

//Admin panelde row'a araba tıklandığında önizleme ve silme: 
$('tr[data-href]').on("click", function () {
    var Id = $(this).find("#trid").text();
    $.ajax({
        url: "/Admin/CarDetail/",
        type: "Get",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { 'id': Id },
        success: function (car) {
            Swal.fire({
                title: car[0] + " " + car[1],
                html: "Fiyat: " + car[2] + " ₺" + "</br>" + "&nbsp;&nbsp;Kilometre: " + car[7] + "&nbsp;&nbsp;Motor Gücü: " + car[9] + "hp" + "</br>" + "&nbsp;&nbsp;Vites: " + car[6] +
                    "&nbsp;&nbsp;&nbsp;&nbsp;Model Yılı: " + car[8] + "</br>" + "&nbsp;&nbsp;Yakıt Türü: " + car[5] + "&nbsp;&nbsp;Renk: " + car[4],
                imageUrl: car[3],
                imageWidth: 1200,
                imageHeight: 250,
                imageAlt: car[0] + " " + car[1],
                confirmButtonColor: '#d33',
                confirmButtonText: 'Sil',
                showCancelButton: true,
                cancelButtonText: 'Kapat',
                allowEscapeKey: false

            }).then((result) => {
                if (result.dismiss === Swal.DismissReason.cancel || result.dismiss == Swal.DismissReason.backdrop)
                    return;
                else {
                    $.ajax({
                        url: "/Admin/DeleteCar/",
                        type: "Get",
                        dataType: 'json',
                        data: { 'id': Id }

                    });
                    document.getElementById(Id).remove();
                    Swal.fire({
                        type: 'success',
                        icon: 'success',
                        title: 'Başarılı',
                        text: 'İlan başarıyla silindi.',
                        confirmButtonText: 'Tamam'
                    })
                }
            })
        }
    })
})

//admin panel table search
$("#tsearch").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#tblcars tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

//admin panel add brand
$("#btnAddBrand").click(async function () {
    await Swal.fire({
        title: 'Marka Ekle',
        html:
            '<p>Marka Adını Girin:<p>' +
            '<input id="Name" class="swal2-input">',
        focusConfirm: true,
        showCancelButton: true,
        confirmButtonText: "Ekle",
        cancelButtonText: "İptal",
        allowEscapeKey: false

    }).then((result) => {
        if (result.dismiss === Swal.DismissReason.cancel || result.dismiss == Swal.DismissReason.backdrop)
            return;
        var Name = document.getElementById('Name').value;
        $.ajax({
            type: 'Post',
            url: '/Admin/AddBrand/',
            data: { "name": Name },
            dataType: 'Json',
            success: function (control) {
                if (control == 1) {
                    Swal.fire({
                        type: 'success',
                        icon: 'success',
                        title: 'Başarılı',
                        text: Name + ' markası başarıyla eklendi.',
                        confirmButtonText: 'Tamam'

                    })
                }
                else if (control===0)
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: 'Marka adı boş geçilemez.',
                        confirmButtonText: 'Tamam'
                    })
                else
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: 'Bu marka zaten kayıtlı.',
                        confirmButtonText: 'Tamam'
                    })
            }
        })
    })
})



//admin panel delete message
$(".btnDelMessage").click(function () {
    var id = $(this).val();
    $.ajax({
        type: 'Get',
        url: '/Admin/DelMessage/',
        data: { "id": id },
        dataType: 'Json',
    });
    Swal.fire({
        type: 'success',
        icon: 'success',
        title: 'Başarılı',
        text: 'Mesaj başarıyla silindi.',
        confirmButtonText: 'Tamam'
    });
    document.getElementById(id).remove();
});

//admin panel addCar validate upload count
function clearPhotos() {
    $('#uploadPhoto').val(null);
    $('#uploadPhoto').click();
}
$('#uploadPhoto').bind('change', function () {
    length = $("#uploadPhoto").get(0).files.length;
    if (length > 3) {
        Swal.fire({
            type: 'Error',
            icon: 'error',
            title: 'Hata!',
            text: 'En fazla 3 fotoğraf yüklemelisiniz.',
            confirmButtonText: 'Tamam'
        }).then((result) => {
            clearPhotos();
        });
    }
});

//admin login
$(".adminLogin").click(async function () {
    var button = $(this);
    $.ajax({
        type: 'Get',
        url: '/Admin/Login/',
        dataType: 'Json',
        data: { 'post': 1 },
        success: function (control) {
            if (control == 1) {
                window.location.href = "/Admin/Index/";
                return;
            }
            else
                LogIn();
        }
    });
    async function LogIn() {
        await Swal.fire({
            title: 'Giriş Yap',
            html:
                '<p>Kullanıcı Adı:<p>' +
                '<input id="userName" class="swal2-input">' +
                '<p>Şifre:<p>' +
                '<input id="password" type="password" class="swal2-input">',
            focusConfirm: true,
            showCancelButton: true,
            confirmButtonText: "Giriş Yap",
            cancelButtonText: "İptal",
            allowEscapeKey: false

        }).then((result) => {
            if (result.dismiss === Swal.DismissReason.cancel || result.dismiss == Swal.DismissReason.backdrop)
                return;
            var userName = document.getElementById('userName').value;
            var password = document.getElementById('password').value;
            var Admin = {
                UserName: userName,
                Password: password
            }
            $.ajax({
                type: 'Post',
                url: '/Admin/Login/',
                data: { 'admin': Admin },
                dataType: 'Json',
                success: function (control) {
                    if (control == 1) {
                        window.location.href = "/Admin/Index/";
                    }
                    else
                        Swal.fire({
                            icon: 'error',
                            title: 'Hata!',
                            text: 'Kullanıcı adı veya şifre hatalı',
                            confirmButtonText: 'Tamam'
                        }).then((result) => {
                            button.click();
                        });
                }
            })
        })
    }
})

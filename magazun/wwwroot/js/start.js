kol_tovar_vubor = 0;
nom_vubor = [];
function vxid1() {
    document.getElementById('all').style.display = "none";
    document.getElementById('vxid_user').style.display = "block";    
}

function vxid2() {
    document.getElementById('all').style.display = "none";
    document.getElementById('new_user').style.display = "block";
}

function aktiv_button() {
    let a = document.getElementById('login_user').value;
    let b = document.getElementById('pasw').value;

    if (a !="" && b != "") {
        
        document.getElementById('bt_vxod').disabled = false; return;
    }
    
    document.getElementById('bt_vxod').disabled = true;

}

function aktiv_button2() {
    a1 = document.getElementById('login_user2').value;
    a2 = document.getElementById('pasw2').value;
    a3 = document.getElementById('pasw3').value;
    a4 = document.getElementById('lastName2').value;
    a5 = document.getElementById('firstName2').value;
    a6 = document.getElementById('email2').value;

    if (a2 != "" && a2 == a3) {
        document.getElementById('img_reg').style.display = "";
    }
    else {
        document.getElementById('img_reg').style.display = "none";
    }

    if (a1 != "" && a2 != "" && a3 != "" && a4 != "" && a5 != "" && a6!="" && a2 == a3) {
        document.getElementById('bt_reg').disabled = false;
        document.getElementById('cop_email').value = a6;    
    }
    else {
        document.getElementById('bt_reg').disabled = true;
        
    }
}
function nazad_index2() {
    document.getElementById('nazad').submit(); // Отправить форму
}

function aktiv_bt_zakaz(id) {
    
    if (document.getElementById(id).checked == true) {
        nom_vubor[kol_tovar_vubor] = id;
        kol_tovar_vubor++;
    }
    else {
        kol_tovar_vubor--;
        // Создаем новый массив без указанного значения
        nom_vubor = nom_vubor.filter(item => item !== id);
    }

    if (kol_tovar_vubor != 0) {
        document.getElementById('bt_zamovlenny').innerHTML = "Зробити замовлення (кільк:" + kol_tovar_vubor.toString() + ")";
        document.getElementById('bt_zamovlenny').disabled = false;
    }
    else {
        document.getElementById('bt_zamovlenny').disabled = true;
        document.getElementById('bt_zamovlenny').innerHTML = "Зробити замовлення";
    }
    
}

function zamovlenny() {
    var formData = new FormData();
    formData.append('firstName', document.getElementById('firstName1').value);
    formData.append('lastName', document.getElementById('lastName1').value);
    nom_vubor.forEach(id => formData.append('masiv_product[]', id)); // Формируем массив

    fetch('/Product/Index_product1', { // Указываем корректный маршрут
        method: 'POST',
        body: formData,
    })
        .then(response => {
            if (response.ok) {
                //alert('Successfully updated!');
            } else {
                response.text().then(text => {
                    console.log('Response status:', response.status);
                    console.log('Response body:', text);
                    //alert('Failed to update: ' + text);
                });
            }
        })
        .catch(error => {
            console.log('Fetch error:', error);
            //alert('Error: ' + error.message);
        });

    document.getElementById('index_prod').submit(); // Отправить форму
    alert("Покупку зроблено");
}

function profil1()
{
    document.getElementById('all').style.display = "none";
    document.getElementById('info_profil').style.display = "block";    
    
}

function edit_profil() {
    //if (document.getElementById('btn_info_edit').innerHTML == "Редагувати") {
    //document.getElementById('btn_info_edit').innerHTML = "Зберегти"
    document.getElementById('btn_info_edit').style.display = "none";
    document.getElementById('btn_info_edit2').style.display = "";
    
        //document.getElementById('btn_info_edit').className = "btn btn-success";

        document.getElementById('info_pasw').disabled = false;
        document.getElementById('info_pasw2').style.display = "";
        document.getElementById('info_pasw22').style.display = "";
        document.getElementById('img_info_pasw22').style.display = "";

        document.getElementById('info_lastName').style.display = "none";
        document.getElementById('info_lastName1').style.display = "";

        document.getElementById('info_firstName').style.display = "none";
        document.getElementById('info_firstName1').style.display = "";

        document.getElementById('info_email').style.display = "none";
        document.getElementById('info_email2').style.display = "";
    /*}
    else {
        alert('zapus');
    }*/
}

function activ_btn_info() {
    a1 = document.getElementById('info_pasw').value;
    a2 = document.getElementById('info_pasw22').value;
    a3 = document.getElementById('info_lastName1').value;
    a4 = document.getElementById('info_firstName1').value;
    a5 = document.getElementById('info_email2').value;

    if (a1 != a2) {
        document.getElementById('img_info_pasw22').style.display = "none";
    }
    else {
        document.getElementById('img_info_pasw22').style.display = "";
    }

    if (a1 != "" && a2 != "" && a1 == a2 && a3 != "" && a3 != "" && a5 != "") {
        document.getElementById('btn_info_edit2').disabled = false;
    }
    else {
        document.getElementById('btn_info_edit2').disabled = true;
    }


    if (document.getElementById('info_email2').checkValidity()) {
        
        // Дополнительные действия, если валидно
    } else {
        document.getElementById('btn_info_edit2').disabled = true;
    }
}

function edit_nazad() {
    document.getElementById('all').style.display = "";
    document.getElementById('info_profil').style.display = "none";
    
}

function vubor_admin1(vubor) {
    document.getElementById('vubor_admin').value = vubor;
}
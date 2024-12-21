kol_tovar_vubor = 0;
nom_vubor = [];
function vxid1() {
    document.getElementById('all').style.display = "none";
    document.getElementById('vxid_user').style.display = "block";
}

function aktiv_button() {
    let a = document.getElementById('login_user').value;
    let b = document.getElementById('pasw').value;

    if (a !="" && b != "") {
        
        document.getElementById('bt_vxod').disabled = false; return;
    }
    
    document.getElementById('bt_vxod').disabled = true;

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
                alert('Successfully updated!');
            } else {
                response.text().then(text => {
                    console.log('Response status:', response.status);
                    console.log('Response body:', text);
                    alert('Failed to update: ' + text);
                });
            }
        })
        .catch(error => {
            console.log('Fetch error:', error);
            alert('Error: ' + error.message);
        });
}

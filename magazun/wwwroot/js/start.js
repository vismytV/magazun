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
window.onload = function(){
    openNav();
}

function openNav() {
    if(window.screen.width > 480)
    {
        document.getElementById("sidebar").style.width = "250px";
    }
    else{
        document.getElementById("sidebar").style.width = "100%";
        document.getElementById("closebtn").style.display = "none";
    }
}
  
function closeNav() {
    document.getElementById("sidebar").style.width = "0";
    document.getElementById("closebtn").style.display = "inline-block";
}
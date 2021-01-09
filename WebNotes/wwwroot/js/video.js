window.onload = function(){

    setDefaultValue();

    /*var range = document.getElementById("range");
    //range.value=1;
    range.onchange = function(){
        document.querySelector('video').playbackRate=this.value;
        document.getElementById("range_label").textContent="Speed: "+this.value;
    }*/
}

function setDefaultValue(){
    // Set speed to all 'range' controlls
    let elements = document.getElementsByClassName("form-control-range");
    for(let elem of elements){
        elem.value = 1;
    }
}

function speedChanger(range){
    range.parentElement.querySelector("label").textContent = "Speed: "+range.value;
    range.parentElement.parentElement.parentElement.querySelector("video").playbackRate=range.value;
    //range.closest("div").find(".label").textContent = "Speed: "+range.value;
}
function loadLogos() {

    init("images/FaceBook-icon.png", "canFaceBook");
    init("images/Twitter-icon.png", "canTwitter");
    init("images/MySpace-icon.png", "canMySpace");

//    init("images/Html5LogoSmall.png", "myCanvasHtml5");
//    init("images/Css3LogoSmall.png", "myCanvasCss3");
//    init("images/JQueryLogoSmall.png", "myCanvasJQuery");

}

function init(path, canvas) {
    var img = new Image();


    img.src = path;

    img.addEventListener("load", function () {
        var ctx = document.getElementById(canvas).getContext("2d");

        ctx.drawImage(img, 0, 0);

        ctx.save();

        ctx.scale(1, -1);
        ctx.translate(0, -img.height * 1.5);
        ctx.drawImage(img, 0, 0, img.width, img.height / 2);

        ctx.restore();

        var gradient = ctx.createLinearGradient(0, img.height, 0, img.height * 1.5);
        gradient.addColorStop(0, "rgba(58,79,99,0.2)");
        gradient.addColorStop(1, "rgba(58,79,99,1)");

        ctx.fillStyle = gradient;
        ctx.fillRect(0, img.height, img.width, img.height / 2);
    });
}
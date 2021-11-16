export class Structure{
    constructor() {
        this.top = 0;
        this.left = 0;
        this.marging = {left: 0, top:0, right: 0, bottom: 0};
        this.middle = { vertical: 0, horizontal: 0};
        this.width = 250;
        this.heigth = this.width * 0.75;
    }

    wrapText(context, text, x, y, maxWidth) {
        let words = text.split(' ');
        let line = '';
        let lastLinePos = y;

        for(let n = 0; n < words.length; n++) {
            let testLine = line + words[n] + ' ';
            let metrics = context.measureText(testLine);
            let testWidth = metrics.width;
            if (testWidth > maxWidth && n > 0) {
                context.fillText(line, x, y);
                line = words[n] + ' ';
                y += metrics.actualBoundingBoxDescent + 5;
                lastLinePos += metrics.actualBoundingBoxDescent + 5;
            }
            else {
                line = testLine;
            }
        }
        context.fillText(line, x, y);
        return lastLinePos;
    }

    setPositionRelative(element){
        this.setPosition(element.left, element.marging.bottom)
    }

    setPosition(left, top){
        this.top = top;
        this.left = left;

        this.marging = {
            left: 0,
            top: this.heigth/2,
            right: this.left + (this.width * 1.5),
            bottom: (this.top + this.heigth) + (this.heigth / 2)
        };

        this.middle = { 
            vertical: (this.top + this.heigth / 2), 
            horizontal: (this.left + this.width / 2) 
        };
    }    
}
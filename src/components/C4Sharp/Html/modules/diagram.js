export class Diagram{
    #canvas;

    constructor(canvas) {
        this.#canvas = canvas;
        this.#resizeCanvas(0, 0, window.outerWidth, window.outerHeight);
        this.elements = [];
    }
    
    #resizeCanvas(left, top, width, height){
        let maxSize = 16384

        this.#canvas.top = top;
        this.#canvas.left = left;
        this.#canvas.width = (width < maxSize) ? width : this.#canvas.width;
        this.#canvas.height = (height < maxSize) ?  height: this.#canvas.height;        
    }
    
    add(element){
        this.elements.push(element);
    }
    
    draw(){
        if (this.elements.length === 0)
            return;

        this.elements.forEach((el, index) => {
            if (index === 0)
                el.setPosition(10, 10);
            else{
                let pre = this.elements[index - 1];
                el.setPositionRelative(pre);
            }
        });

        let lastElement = this.elements[this.elements.length - 1];
        this.#resizeCanvas(0, 0, window.innerWidth,  lastElement.marging.bottom)        
        
        if (this.#canvas.getContext) {
            let ctx = this.#canvas.getContext('2d');

            this.elements.forEach((el, index) => {
                console.log(el);
                el.draw(ctx);
            });
        }
    }
}
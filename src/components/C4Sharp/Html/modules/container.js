import {Structure} from "./structure.js";

export class Container extends Structure {
    constructor(title, description, technology) {
        super();
        this.title = title ? title : 'Container';
        this.description = description ? description : '';
        this.technology = technology ? `: ${technology}` : '';
        this.context = undefined;
    }
    
    /* Drawn container */
    draw(ctx){
        this.context = ctx;
        this.#drawRect();
        this.#drawTitle()
        this.#drawDescription();
    }
    
    #drawRect(){
        this.context.fillStyle = '#438dd5';
        this.context.strokeStyle = "#2e6295";
        this.context.fillRect(this.left, this.top, this.width, this.heigth);
        this.context.strokeRect(this.left, this.top, this.width, this.heigth);        
    }
    
    #drawTitle(){
        this.#prepareTextDrawing();
        let top = this.wrapText(this.context, this.title, this.middle.horizontal, this.top + 30, this.width);

        this.context.font = 'normal lighter 0.8em "Verdana, Arial, Helvetica", sans-serif'
        this.wrapText(this.context, `[container${this.technology}]`, this.middle.horizontal, top + 20, this.width);
        this.#drawDescription();        
    }

    #prepareTextDrawing(){
        this.context.fillStyle = '#ffffff';
        this.context.font = 'normal bolder 1em "Verdana, Arial, Helvetica", sans-serif'
        this.context.textBaseline = "hanging";
        this.context.textAlign = 'center';
    }    
    
    #drawDescription(){
        this.context.font = 'normal lighter 0.9em "Verdana, Arial, Helvetica", sans-serif'
        let tam = this.context.measureText(this.description).actualBoundingBoxDescent / 2;
        this.wrapText(this.context, this.description, this.middle.horizontal, this.middle.vertical - tam, this.width);        
    }
}
import {Diagram} from "./modules/diagram.js";
import {Container} from "./modules/container.js";

const canvas = document.getElementById('canvas');
const diagram = new Diagram(canvas);


for (let i = 0; i < 10; i++) {
    const container = new Container(
        "Software System", 
        "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
        "Lorem Ipsum has been the industry's",
        "C#, ASP.NET, .net 6");
    diagram.add(container);
}


diagram.draw();
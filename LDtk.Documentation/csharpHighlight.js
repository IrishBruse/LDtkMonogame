"use strict";

const keywords = [
    " int",
    "string ",
    "bool ",
    "long ",
    "for ",
    "using ",
    "new ",
    "public",
    "class ",
    "get;",
    "set;",
    "foreach"
]

const classes = [
    "LDtkLevelData",

    "LDtkRenderer",
    "LDtkFile",
    "LDtkWorld",
    "LDtkLevel",
    "LDtkIntGrid",
    "ILDtkEntity",
    "Gun_Pickup",

    "Point ",

    "Vector2",
    " Color",
    "Rectangle",
    "Player",
    "SamplerState"
]

const ops = [
    "+",
    "-",
    "=",
    ";",
]

/**
 * Returns the sum of all numbers passed to the function.
 * @param {HTMLElement} block
 */
function handleCodeBlock(block) {
    block.childNodes.forEach((child) => {
        if (child.nodeType === 3) {
            const text = child.nodeValue;
            console.log(child.);
            child.innerHtml = "<h1>test</h1>";

        }
    });
}

function colorToken(contents, color) {
    return `<span class='hljs-${color}'>${contents}</span>`
}

setTimeout(() => {
    document.querySelectorAll(".hljs.csharp").forEach((block) => handleCodeBlock(block));
}, 1);

"use strict";

const keywords = [
    "int ",
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
]

const classes = [
    "LDtkRenderer",
    "LDtkFile",
    "LDtkWorld",
    "LDtkLevel",
    "LDtkIntGrid",
    "ILDtkEntity",
    "Gun_Pickup",

    "LDtkLevelData",

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

    block.innerHTML = block.innerText;

    let code = block.innerHTML;
    block.textContent = "";

    code = code.replaceAll("=", colorToken("=", "comment"));

    for (const c of classes) {
        code = code.replaceAll(c, colorToken(c, "class"));
    }

    const methods = code.match(/\..*\(/g);
    if (methods) {
        for (let i = 0; i < methods.length; i++) {
            let method = methods[i].substring(1, methods[i].length - 1);
            code = code.replaceAll(method, colorToken(method, "method"));
        }
    }

    for (const k of keywords) {
        code = code.replaceAll(k, colorToken(k, "keyword"));
    }


    const strings = code.match(/\".*\"/g);
    if (strings) {
        for (let i = 0; i < strings.length; i++) {
            code = code.replaceAll(strings[i], colorToken(strings[i], "string"));
        }
    }

    const comments = code.match(/\/\/.*/g);
    if (comments) {
        for (let i = 0; i < comments.length; i++) {
            code = code.replaceAll(comments[i], colorToken(comments[i], "comment"));
        }
    }

    code = code.replaceAll(".", colorToken(".", "comment"));

    code = code.replaceAll(";", colorToken(";", "comment"));
    code = code.replaceAll(":", colorToken(":", "comment"));

    block.innerHTML = code;

}

function colorToken(contents, color) {
    return `<span class='hljs-${color}'>${contents}</span>`
}

setTimeout(() => {
    document.querySelectorAll(".hljs.csharp").forEach((block) => handleCodeBlock(block));
}, 1);

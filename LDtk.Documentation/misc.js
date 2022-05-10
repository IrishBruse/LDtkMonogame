document.querySelectorAll("a>strong").forEach((prefix) => prefix.parentElement.removeChild(prefix));

document.querySelectorAll("img").forEach((badge) => {
    if (badge.src.includes("img.shields.io")) {
        badge.parentElement.removeChild(badge);
    }
});

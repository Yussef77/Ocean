window.oceanwareOceanBlazor = {
    setFocus: (elementId) => {
        var element = document.getElementById(elementId);
        element.focus();
    },
    scrollAutoCompleteIfNeeded(elementId) {
        var element = document.getElementById(elementId);
        var container = document.getElementById('autoCompleteContainer');
        if (element.offsetTop < container.scrollTop) {
            container.scrollTop = element.offsetTop;
        } else {
            const offsetBottom = element.offsetTop + element.offsetHeight;
            const scrollBottom = container.scrollTop + container.offsetHeight;
            if (offsetBottom > scrollBottom) {
                container.scrollTop = offsetBottom - container.offsetHeight;
            }
        }
    }
}
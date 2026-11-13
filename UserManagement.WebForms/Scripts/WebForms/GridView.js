//CdnPath=http://ajax.aspnetcdn.com/ajax/4.5.1/1/GrIdView.js
function GrIdView() {
    this.pageIndex = null;
    this.sortExpression = null;
    this.sortDirection = null;
    this.dataKeys = null;
    this.createPropertyString = GrIdView_createPropertyString;
    this.setStateField = GrIdView_setStateValue;
    this.getHIddenFieldContents = GrIdView_getHIddenFieldContents;
    this.stateField = null;
    this.panelElement = null;
    this.callback = null;
}
function GrIdView_createPropertyString() {
    return createPropertyStringFromValues_GrIdView(this.pageIndex, this.sortDirection, this.sortExpression, this.dataKeys);
}
function GrIdView_setStateValue() {
    this.stateField.value = this.createPropertyString();
}
function GrIdView_OnCallback (result, context) {
    var value = new String(result);
    var valsArray = value.split("|");
    var innerHtml = valsArray[4];
    for (var i = 5; i < valsArray.length; i++) {
        innerHtml += "|" + valsArray[i];
    }
    context.panelElement.innerHTML = innerHtml;
    context.stateField.value = createPropertyStringFromValues_GrIdView(valsArray[0], valsArray[1], valsArray[2], valsArray[3]);
}
function GrIdView_getHIddenFieldContents(arg) {
    return arg + "|" + this.stateField.value;
}
function createPropertyStringFromValues_GrIdView(pageIndex, sortDirection, sortExpression, dataKeys) {
    var value = new Array(pageIndex, sortDirection, sortExpression, dataKeys);
    return value.join("|");
}

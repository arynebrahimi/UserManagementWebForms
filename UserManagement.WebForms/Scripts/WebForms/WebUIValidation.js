//CdnPath=http://ajax.aspnetcdn.com/ajax/4.5.1/1/WebUIValIdation.js
var Page_ValIdationVer = "125";
var Page_IsValId = true;
var Page_BlockSubmit = false;
var Page_InvalIdControlToBeFocused = null;
var Page_TextTypes = /^(text|password|file|search|tel|url|email|number|range|color|datetime|date|month|week|time|datetime-local)$/i;
function ValIdatorUpdateDisplay(val) {
    if (typeof(val.display) == "string") {
        if (val.display == "None") {
            return;
        }
        if (val.display == "Dynamic") {
            val.style.display = val.isvalId ? "none" : "inline";
            return;
        }
    }
    if ((navigator.userAgent.indexOf("Mac") > -1) &&
        (navigator.userAgent.indexOf("MSIE") > -1)) {
        val.style.display = "inline";
    }
    val.style.visibility = val.isvalId ? "hIdden" : "visible";
}
function ValIdatorUpdateIsValId() {
    Page_IsValId = AllValIdatorsValId(Page_ValIdators);
}
function AllValIdatorsValId(valIdators) {
    if ((typeof(valIdators) != "undefined") && (valIdators != null)) {
        var i;
        for (i = 0; i < valIdators.length; i++) {
            if (!valIdators[i].isvalId) {
                return false;
            }
        }
    }
    return true;
}
function ValIdatorHookupControlId(controlId, val) {
    if (typeof(controlId) != "string") {
        return;
    }
    var ctrl = document.getElementById(controlId);
    if ((typeof(ctrl) != "undefined") && (ctrl != null)) {
        ValIdatorHookupControl(ctrl, val);
    }
    else {
        val.isvalId = true;
        val.enabled = false;
    }
}
function ValIdatorHookupControl(control, val) {
    if (typeof(control.tagName) != "string") {
        return;  
    }
    if (control.tagName != "INPUT" && control.tagName != "TEXTAREA" && control.tagName != "SELECT") {
        var i;
        for (i = 0; i < control.childNodes.length; i++) {
            ValIdatorHookupControl(control.childNodes[i], val);
        }
        return;
    }
    else {
        if (typeof(control.ValIdators) == "undefined") {
            control.ValIdators = new Array;
            var eventType;
            if (control.type == "radio") {
                eventType = "onclick";
            } else {
                eventType = "onchange";
                if (typeof(val.focusOnError) == "string" && val.focusOnError == "t") {
                    ValIdatorHookupEvent(control, "onblur", "ValidatedControlOnBlur(event); ");
                }
            }
            ValIdatorHookupEvent(control, eventType, "ValIdatorOnChange(event); ");
            if (Page_TextTypes.test(control.type)) {
                ValIdatorHookupEvent(control, "onkeypress", 
                    "event = event || window.event; if (!ValidatedTextBoxOnKeyPress(event)) { event.cancelBubble = true; if (event.stopPropagation) event.stopPropagation(); return false; } ");
            }
        }
        control.ValIdators[control.ValIdators.length] = val;
    }
}
function ValIdatorHookupEvent(control, eventType, functionPrefix) {
    var ev = control[eventType];
    if (typeof(ev) == "function") {
        ev = ev.toString();
        ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
    }
    else {
        ev = "";
    }
    control[eventType] = new Function("event", functionPrefix + " " + ev);
}
function ValIdatorGetValue(Id) {
    var control;
    control = document.getElementById(Id);
    if (typeof(control.value) == "string") {
        return control.value;
    }
    return ValIdatorGetValueRecursive(control);
}
function ValIdatorGetValueRecursive(control)
{
    if (typeof(control.value) == "string" && (control.type != "radio" || control.checked == true)) {
        return control.value;
    }
    var i, val;
    for (i = 0; i<control.childNodes.length; i++) {
        val = ValIdatorGetValueRecursive(control.childNodes[i]);
        if (val != "") return val;
    }
    return "";
}
function Page_ClientValidate(valIdationGroup) {
    Page_InvalIdControlToBeFocused = null;
    if (typeof(Page_ValIdators) == "undefined") {
        return true;
    }
    var i;
    for (i = 0; i < Page_ValIdators.length; i++) {
        ValIdatorValidate(Page_ValIdators[i], valIdationGroup, null);
    }
    ValIdatorUpdateIsValId();
    ValIdationSummaryOnSubmit(valIdationGroup);
    Page_BlockSubmit = !Page_IsValId;
    return Page_IsValId;
}
function ValIdatorCommonOnSubmit() {
    Page_InvalIdControlToBeFocused = null;
    var result = !Page_BlockSubmit;
    if ((typeof(window.event) != "undefined") && (window.event != null)) {
        window.event.returnValue = result;
    }
    Page_BlockSubmit = false;
    return result;
}
function ValIdatorEnable(val, enable) {
    val.enabled = (enable != false);
    ValIdatorValidate(val);
    ValIdatorUpdateIsValId();
}
function ValIdatorOnChange(event) {
    event = event || window.event;
    Page_InvalIdControlToBeFocused = null;
    var targetedControl;
    if ((typeof(event.srcElement) != "undefined") && (event.srcElement != null)) {
        targetedControl = event.srcElement;
    }
    else {
        targetedControl = event.target;
    }
    var vals;
    if (typeof(targetedControl.ValIdators) != "undefined") {
        vals = targetedControl.ValIdators;
    }
    else {
        if (targetedControl.tagName.toLowerCase() == "label") {
            targetedControl = document.getElementById(targetedControl.htmlFor);
            vals = targetedControl.ValIdators;
        }
    }
    if (vals) {
        for (var i = 0; i < vals.length; i++) {
            ValIdatorValidate(vals[i], null, event);
        }
    }
    ValIdatorUpdateIsValId();
}
function ValidatedTextBoxOnKeyPress(event) {
    event = event || window.event;
    if (event.keyCode == 13) {
        ValIdatorOnChange(event);
        var vals;
        if ((typeof(event.srcElement) != "undefined") && (event.srcElement != null)) {
            vals = event.srcElement.ValIdators;
        }
        else {
            vals = event.target.ValIdators;
        }
        return AllValIdatorsValId(vals);
    }
    return true;
}
function ValidatedControlOnBlur(event) {
    event = event || window.event;
    var control;
    if ((typeof(event.srcElement) != "undefined") && (event.srcElement != null)) {
        control = event.srcElement;
    }
    else {
        control = event.target;
    }
    if ((typeof(control) != "undefined") && (control != null) && (Page_InvalIdControlToBeFocused == control)) {
        control.focus();
        Page_InvalIdControlToBeFocused = null;
    }
}
function ValIdatorValidate(val, valIdationGroup, event) {
    val.isvalId = true;
    if ((typeof(val.enabled) == "undefined" || val.enabled != false) && IsValIdationGroupMatch(val, valIdationGroup)) {
        if (typeof(val.evaluationfunction) == "function") {
            val.isvalId = val.evaluationfunction(val);
            if (!val.isvalId && Page_InvalIdControlToBeFocused == null &&
                typeof(val.focusOnError) == "string" && val.focusOnError == "t") {
                ValIdatorSetFocus(val, event);
            }
        }
    }
    ValIdatorUpdateDisplay(val);
}
function ValIdatorSetFocus(val, event) {
    var ctrl;
    if (typeof(val.controlhookup) == "string") {
        var eventCtrl;
        if ((typeof(event) != "undefined") && (event != null)) {
            if ((typeof(event.srcElement) != "undefined") && (event.srcElement != null)) {
                eventCtrl = event.srcElement;
            }
            else {
                eventCtrl = event.target;
            }
        }
        if ((typeof(eventCtrl) != "undefined") && (eventCtrl != null) &&
            (typeof(eventCtrl.Id) == "string") &&
            (eventCtrl.Id == val.controlhookup)) {
            ctrl = eventCtrl;
        }
    }
    if ((typeof(ctrl) == "undefined") || (ctrl == null)) {
        ctrl = document.getElementById(val.controltoValidate);
    }
    if ((typeof(ctrl) != "undefined") && (ctrl != null) &&
        (ctrl.tagName.toLowerCase() != "table" || (typeof(event) == "undefined") || (event == null)) && 
        ((ctrl.tagName.toLowerCase() != "input") || (ctrl.type.toLowerCase() != "hIdden")) &&
        (typeof(ctrl.disabled) == "undefined" || ctrl.disabled == null || ctrl.disabled == false) &&
        (typeof(ctrl.visible) == "undefined" || ctrl.visible == null || ctrl.visible != false) &&
        (IsInVisibleContainer(ctrl))) {
        if ((ctrl.tagName.toLowerCase() == "table" && (typeof(__nonMSDOMBrowser) == "undefined" || __nonMSDOMBrowser)) ||
            (ctrl.tagName.toLowerCase() == "span")) {
            var inputElements = ctrl.getElementsByTagName("input");
            var lastInputElement  = inputElements[inputElements.length -1];
            if (lastInputElement != null) {
                ctrl = lastInputElement;
            }
        }
        if (typeof(ctrl.focus) != "undefined" && ctrl.focus != null) {
            ctrl.focus();
            Page_InvalIdControlToBeFocused = ctrl;
        }
    }
}
function IsInVisibleContainer(ctrl) {
    if (typeof(ctrl.style) != "undefined" &&
        ( ( typeof(ctrl.style.display) != "undefined" &&
            ctrl.style.display == "none") ||
          ( typeof(ctrl.style.visibility) != "undefined" &&
            ctrl.style.visibility == "hIdden") ) ) {
        return false;
    }
    else if (typeof(ctrl.parentNode) != "undefined" &&
             ctrl.parentNode != null &&
             ctrl.parentNode != ctrl) {
        return IsInVisibleContainer(ctrl.parentNode);
    }
    return true;
}
function IsValIdationGroupMatch(control, valIdationGroup) {
    if ((typeof(valIdationGroup) == "undefined") || (valIdationGroup == null)) {
        return true;
    }
    var controlGroup = "";
    if (typeof(control.valIdationGroup) == "string") {
        controlGroup = control.valIdationGroup;
    }
    return (controlGroup == valIdationGroup);
}
function ValIdatorOnLoad() {
    if (typeof(Page_ValIdators) == "undefined")
        return;
    var i, val;
    for (i = 0; i < Page_ValIdators.length; i++) {
        val = Page_ValIdators[i];
        if (typeof(val.evaluationfunction) == "string") {
            eval("val.evaluationfunction = " + val.evaluationfunction + ";");
        }
        if (typeof(val.isvalId) == "string") {
            if (val.isvalId == "False") {
                val.isvalId = false;
                Page_IsValId = false;
            }
            else {
                val.isvalId = true;
            }
        } else {
            val.isvalId = true;
        }
        if (typeof(val.enabled) == "string") {
            val.enabled = (val.enabled != "False");
        }
        if (typeof(val.controltoValidate) == "string") {
            ValIdatorHookupControlId(val.controltoValidate, val);
        }
        if (typeof(val.controlhookup) == "string") {
            ValIdatorHookupControlId(val.controlhookup, val);
        }
    }
    Page_ValIdationActive = true;
}
function ValIdatorConvert(op, dataType, val) {
    function GetFullYear(year) {
        var twoDigitCutoffYear = val.cutoffyear % 100;
        var cutoffYearCentury = val.cutoffyear - twoDigitCutoffYear;
        return ((year > twoDigitCutoffYear) ? (cutoffYearCentury - 100 + year) : (cutoffYearCentury + year));
    }
    var num, cleanInput, m, exp;
    if (dataType == "Integer") {
        exp = /^\s*[-\+]?\d+\s*$/;
        if (op.match(exp) == null)
            return null;
        num = parseInt(op, 10);
        return (isNaN(num) ? null : num);
    }
    else if(dataType == "Double") {
        exp = new RegExp("^\\s*([-\\+])?(\\d*)\\" + val.decimalchar + "?(\\d*)\\s*$");
        m = op.match(exp);
        if (m == null)
            return null;
        if (m[2].length == 0 && m[3].length == 0)
            return null;
        cleanInput = (m[1] != null ? m[1] : "") + (m[2].length>0 ? m[2] : "0") + (m[3].length>0 ? "." + m[3] : "");
        num = parseFloat(cleanInput);
        return (isNaN(num) ? null : num);
    }
    else if (dataType == "Currency") {
        var hasDigits = (val.digits > 0);
        var beginGroupSize, subsequentGroupSize;
        var groupSizeNum = parseInt(val.groupsize, 10);
        if (!isNaN(groupSizeNum) && groupSizeNum > 0) {
            beginGroupSize = "{1," + groupSizeNum + "}";
            subsequentGroupSize = "{" + groupSizeNum + "}";
        }
        else {
            beginGroupSize = subsequentGroupSize = "+";
        }
        exp = new RegExp("^\\s*([-\\+])?((\\d" + beginGroupSize + "(\\" + val.groupchar + "\\d" + subsequentGroupSize + ")+)|\\d*)"
                        + (hasDigits ? "\\" + val.decimalchar + "?(\\d{0," + val.digits + "})" : "")
                        + "\\s*$");
        m = op.match(exp);
        if (m == null)
            return null;
        if (m[2].length == 0 && hasDigits && m[5].length == 0)
            return null;
        cleanInput = (m[1] != null ? m[1] : "") + m[2].replace(new RegExp("(\\" + val.groupchar + ")", "g"), "") + ((hasDigits && m[5].length > 0) ? "." + m[5] : "");
        num = parseFloat(cleanInput);
        return (isNaN(num) ? null : num);
    }
    else if (dataType == "Date") {
        var yearFirstExp = new RegExp("^\\s*((\\d{4})|(\\d{2}))([-/]|\\. ?)(\\d{1,2})\\4(\\d{1,2})\\.?\\s*$");
        m = op.match(yearFirstExp);
        var day, month, year;
        if (m != null && (((typeof(m[2]) != "undefined") && (m[2].length == 4)) || val.dateorder == "ymd")) {
            day = m[6];
            month = m[5];
            year = (m[2].length == 4) ? m[2] : GetFullYear(parseInt(m[3], 10));
        }
        else {
            if (val.dateorder == "ymd"){
                return null;
            }
            var yearLastExp = new RegExp("^\\s*(\\d{1,2})([-/]|\\. ?)(\\d{1,2})(?:\\s|\\2)((\\d{4})|(\\d{2}))(?:\\s\u0433\\.|\\.)?\\s*$");
            m = op.match(yearLastExp);
            if (m == null) {
                return null;
            }
            if (val.dateorder == "mdy") {
                day = m[3];
                month = m[1];
            }
            else {
                day = m[1];
                month = m[3];
            }
            year = ((typeof(m[5]) != "undefined") && (m[5].length == 4)) ? m[5] : GetFullYear(parseInt(m[6], 10));
        }
        month -= 1;
        var date = new Date(year, month, day);
        if (year < 100) {
            date.setFullYear(year);
        }
        return (typeof(date) == "object" && year == date.getFullYear() && month == date.getMonth() && day == date.getDate()) ? date.valueOf() : null;
    }
    else {
        return op.toString();
    }
}
function ValIdatorCompare(operand1, operand2, operator, val) {
    var dataType = val.type;
    var op1, op2;
    if ((op1 = ValIdatorConvert(operand1, dataType, val)) == null)
        return false;
    if (operator == "DataTypeCheck")
        return true;
    if ((op2 = ValIdatorConvert(operand2, dataType, val)) == null)
        return true;
    switch (operator) {
        case "NotEqual":
            return (op1 != op2);
        case "GreaterThan":
            return (op1 > op2);
        case "GreaterThanEqual":
            return (op1 >= op2);
        case "LessThan":
            return (op1 < op2);
        case "LessThanEqual":
            return (op1 <= op2);
        default:
            return (op1 == op2);
    }
}
function CompareValIdatorEvaluateIsValId(val) {
    var value = ValIdatorGetValue(val.controltoValidate);
    if (ValIdatorTrim(value).length == 0)
        return true;
    var compareTo = "";
    if ((typeof(val.controltocompare) != "string") ||
        (typeof(document.getElementById(val.controltocompare)) == "undefined") ||
        (null == document.getElementById(val.controltocompare))) {
        if (typeof(val.valuetocompare) == "string") {
            compareTo = val.valuetocompare;
        }
    }
    else {
        compareTo = ValIdatorGetValue(val.controltocompare);
    }
    var operator = "Equal";
    if (typeof(val.operator) == "string") {
        operator = val.operator;
    }
    return ValIdatorCompare(value, compareTo, operator, val);
}
function CustomValIdatorEvaluateIsValId(val) {
    var value = "";
    if (typeof(val.controltoValidate) == "string") {
        value = ValIdatorGetValue(val.controltoValidate);
        if ((ValIdatorTrim(value).length == 0) &&
            ((typeof(val.Validateemptytext) != "string") || (val.Validateemptytext != "true"))) {
            return true;
        }
    }
    var args = { Value:value, IsValId:true };
    if (typeof(val.clientvalIdationfunction) == "string") {
        eval(val.clientvalIdationfunction + "(val, args) ;");
    }
    return args.IsValId;
}
function RegularExpressionValIdatorEvaluateIsValId(val) {
    var value = ValIdatorGetValue(val.controltoValidate);
    if (ValIdatorTrim(value).length == 0)
        return true;
    var rx = new RegExp(val.valIdationexpression);
    var matches = rx.exec(value);
    return (matches != null && value == matches[0]);
}
function ValIdatorTrim(s) {
    var m = s.match(/^\s*(\S+(\s+\S+)*)\s*$/);
    return (m == null) ? "" : m[1];
}
function RequiredFieldValIdatorEvaluateIsValId(val) {
    return (ValIdatorTrim(ValIdatorGetValue(val.controltoValidate)) != ValIdatorTrim(val.initialvalue))
}
function RangeValIdatorEvaluateIsValId(val) {
    var value = ValIdatorGetValue(val.controltoValidate);
    if (ValIdatorTrim(value).length == 0)
        return true;
    return (ValIdatorCompare(value, val.minimumvalue, "GreaterThanEqual", val) &&
            ValIdatorCompare(value, val.maximumvalue, "LessThanEqual", val));
}
function ValIdationSummaryOnSubmit(valIdationGroup) {
    if (typeof(Page_ValIdationSummaries) == "undefined")
        return;
    var summary, sums, s;
    var headerSep, first, pre, post, end;
    for (sums = 0; sums < Page_ValIdationSummaries.length; sums++) {
        summary = Page_ValIdationSummaries[sums];
        if (!summary) continue;
        summary.style.display = "none";
        if (!Page_IsValId && IsValIdationGroupMatch(summary, valIdationGroup)) {
            var i;
            if (summary.showsummary != "False") {
                summary.style.display = "";
                if (typeof(summary.displaymode) != "string") {
                    summary.displaymode = "BulletList";
                }
                switch (summary.displaymode) {
                    case "List":
                        headerSep = "<br>";
                        first = "";
                        pre = "";
                        post = "<br>";
                        end = "";
                        break;
                    case "BulletList":
                    default:
                        headerSep = "";
                        first = "<ul>";
                        pre = "<li>";
                        post = "</li>";
                        end = "</ul>";
                        break;
                    case "SingleParagraph":
                        headerSep = " ";
                        first = "";
                        pre = "";
                        post = " ";
                        end = "<br>";
                        break;
                }
                s = "";
                if (typeof(summary.headertext) == "string") {
                    s += summary.headertext + headerSep;
                }
                s += first;
                for (i=0; i<Page_ValIdators.length; i++) {
                    if (!Page_ValIdators[i].isvalId && typeof(Page_ValIdators[i].errormessage) == "string") {
                        s += pre + Page_ValIdators[i].errormessage + post;
                    }
                }
                s += end;
                summary.innerHTML = s;
                window.scrollTo(0,0);
            }
            if (summary.showmessagebox == "True") {
                s = "";
                if (typeof(summary.headertext) == "string") {
                    s += summary.headertext + "\r\n";
                }
                var lastValIndex = Page_ValIdators.length - 1;
                for (i=0; i<=lastValIndex; i++) {
                    if (!Page_ValIdators[i].isvalId && typeof(Page_ValIdators[i].errormessage) == "string") {
                        switch (summary.displaymode) {
                            case "List":
                                s += Page_ValIdators[i].errormessage;
                                if (i < lastValIndex) {
                                    s += "\r\n";
                                }
                                break;
                            case "BulletList":
                            default:
                                s += "- " + Page_ValIdators[i].errormessage;
                                if (i < lastValIndex) {
                                    s += "\r\n";
                                }
                                break;
                            case "SingleParagraph":
                                s += Page_ValIdators[i].errormessage + " ";
                                break;
                        }
                    }
                }
                alert(s);
            }
        }
    }
}
if (window.jQuery) {
    (function ($) {
        var dataValIdationAttribute = "data-val",
            dataValIdationSummaryAttribute = "data-valsummary",
            normalizedAttributes = { valIdationgroup: "valIdationGroup", focusonerror: "focusOnError" };
        function getAttributesWithPrefix(element, prefix) {
            var i,
                attribute,
                list = {},
                attributes = element.attributes,
                length = attributes.length,
                prefixLength = prefix.length;
            prefix = prefix.toLowerCase();
            for (i = 0; i < length; i++) {
                attribute = attributes[i];
                if (attribute.specified && attribute.name.substr(0, prefixLength).toLowerCase() === prefix) {
                    list[attribute.name.substr(prefixLength)] = attribute.value;
                }
            }
            return list;
        }
        function normalizeKey(key) {
            key = key.toLowerCase();
            return normalizedAttributes[key] === undefined ? key : normalizedAttributes[key];
        }
        function addValIdationExpando(element) {
            var attributes = getAttributesWithPrefix(element, dataValIdationAttribute + "-");
            $.each(attributes, function (key, value) {
                element[normalizeKey(key)] = value;
            });
        }
        function dispose(element) {
            var index = $.inArray(element, Page_ValIdators);
            if (index >= 0) {
                Page_ValIdators.splice(index, 1);
            }
        }
        function addNormalizedAttribute(name, normalizedName) {
            normalizedAttributes[name.toLowerCase()] = normalizedName;
        }
        function parseSpecificAttribute(selector, attribute, valIdatorsArray) {
            return $(selector).find("[" + attribute + "='true']").each(function (index, element) {
                addValIdationExpando(element);
                element.dispose = function () { dispose(element); element.dispose = null; };
                if ($.inArray(element, valIdatorsArray) === -1) {
                    valIdatorsArray.push(element);
                }
            }).length;
        }
        function parse(selector) {
            var length = parseSpecificAttribute(selector, dataValIdationAttribute, Page_ValIdators);
            length += parseSpecificAttribute(selector, dataValIdationSummaryAttribute, Page_ValIdationSummaries);
            return length;
        }
        function loadValIdators() {
            if (typeof (ValIdatorOnLoad) === "function") {
                ValIdatorOnLoad();
            }
            if (typeof (ValIdatorOnSubmit) === "undefined") {
                window.ValIdatorOnSubmit = function () {
                    return Page_ValIdationActive ? ValIdatorCommonOnSubmit() : true;
                };
            }
        }
        function registerUpdatePanel() {
            if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                var prm = Sys.WebForms.PageRequestManager.getInstance(),
                    postBackElement, endRequestHandler;
                if (prm.get_isInAsyncPostBack()) {
                    endRequestHandler = function (sender, args) {
                        if (parse(document)) {
                            loadValIdators();
                        }
                        prm.remove_endRequest(endRequestHandler);
                        endRequestHandler = null;
                    };
                    prm.add_endRequest(endRequestHandler);
                }
                prm.add_beginRequest(function (sender, args) {
                    postBackElement = args.get_postBackElement();
                });
                prm.add_pageLoaded(function (sender, args) {
                    var i, panels, valFound = 0;
                    if (typeof (postBackElement) === "undefined") {
                        return;
                    }
                    panels = args.get_panelsUpdated();
                    for (i = 0; i < panels.length; i++) {
                        valFound += parse(panels[i]);
                    }
                    panels = args.get_panelsCreated();
                    for (i = 0; i < panels.length; i++) {
                        valFound += parse(panels[i]);
                    }
                    if (valFound) {
                        loadValIdators();
                    }
                });
            }
        }
        $(function () {
            if (typeof (Page_ValIdators) === "undefined") {
                window.Page_ValIdators = [];
            }
            if (typeof (Page_ValIdationSummaries) === "undefined") {
                window.Page_ValIdationSummaries = [];
            }
            if (typeof (Page_ValIdationActive) === "undefined") {
                window.Page_ValIdationActive = false;
            }
            $.WebFormValIdator = {
                addNormalizedAttribute: addNormalizedAttribute,
                parse: parse
            };
            if (parse(document)) {
                loadValIdators();
            }
            registerUpdatePanel();
        });
    } (jQuery));
}
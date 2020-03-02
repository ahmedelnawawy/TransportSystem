
function PrintGridData(prtg) {
    document.getElementById(prtg).style.visibility = "visible";
    var prtGrid = document.getElementById(prtg);//<%--'<%=prtg.ClientID %>');--%>

    prtGrid.border = 2;
    var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
    prtwin.document.write(prtGrid.outerHTML);
    prtwin.document.close();
    prtwin.focus();
    prtwin.print();
    prtwin.close();
    prtGrid.style.visibility = 'hidden';

}
//دى للى هيطبع بشكل مباشر
function PrintGridDataCustom(prtg) {
    // document.getElementById(prtg).style.visibility = "visible";
    var divsToHide = document.getElementsByClassName("NotToPrint");
    for (var i = 0; i < divsToHide.length; i++) {
        //  divsToHide[i].style.visibility = "hidden"; // or
        divsToHide[i].style.display = "none"; // depending on what you're doing
    }

    var prtGrid = document.getElementById(prtg);//<%--'<%=prtg.ClientID %>');--%>

    prtGrid.border = 2;
    var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
    prtwin.document.write(prtGrid.outerHTML);
    prtwin.document.close();
    prtwin.focus();
    prtwin.print();
    prtwin.close();
    for (var i = 0; i < divsToHide.length; i++) {
        //  divsToHide[i].style.visibility = "visible"; // or
        divsToHide[i].style.display = ""; // depending on what you're doing
    }
}
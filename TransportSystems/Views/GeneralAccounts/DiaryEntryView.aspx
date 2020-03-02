<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="DiaryEntryView.aspx.cs" Inherits="TransportSystems.Views.GeneralAccounts.DiaryEntryView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_DateTxt").datepicker({
                dateFormat: 'yy-mm-dd'
            });
           
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="ResetBtn" CssClass="btn btn-success" runat="server" Text="جديد" OnClick="ResetBtn_Click" />
        <div  id="prtg" class="kt-portlet mb-0">
            
           
             <h1 style="text-align:center;color:red;"><u>قيد يومية</u></h1>
           
     
              <div class="kt-portlet__body p-2 pb-0">
                   <div class="row">
                <div class="form-group col-md-6">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
رقم القيد                    </label>
                    <asp:TextBox ID="EntryNoTxt" style="display: inline-block;margin-right: 3%;" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                       <div class="form-group col-md-6">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
التاريخ                    </label>
                    <asp:TextBox ID="DateTxt" style="display: inline-block;margin-right: 3%;" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                       </div>
              

                  <div class="row">
                <div class="form-group col-md-9">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
البيان                    </label>
                    <asp:TextBox ID="DescTxt" type='text' TextMode="multiline" Columns="50" Rows="5" runat="server" Cssclass="form-control col-md-12"></asp:TextBox>
                </div>
                       </div>
                  </div>
</div>
    <div class="row NotToPrint">
    <div class="form-group col-md-3">
                    
                    <asp:DropDownList ID="EntryNoForSearch"  runat="server"
                        CssClass="browser-default custom-select form-control" Width="100%">
                        
                    </asp:DropDownList>
        
         </div>
         <div class="form-group col-md-4">
                              <asp:Button ID="SearchBtn" CssClass="btn btn-success" runat="server" Text="بحث" OnClick="SearchBtn_Click" />

                                   
              <asp:Button ID="FirstBtn" CssClass="btn btn-success" runat="server" Text="الاول" OnClick="FirstBtn_Click" />
                 <asp:Button ID="NextBtn" CssClass="btn btn-success" runat="server" Text="التالى" OnClick="NextBtn_Click" />
                 <asp:Button ID="PrevBtn" CssClass="btn btn-success" runat="server" Text="السابق" OnClick="PrevBtn_Click" />
                 <asp:Button ID="LastBtn" CssClass="btn btn-success" runat="server" Text="الاخير" OnClick="LastBtn_Click" />

</div>
         
    </div>
   



     <table   dir="rtl" style="float:right;margin:20px;width: 100%;">

        

         <tr class="NotToPrint" >
             <td><asp:Label ID="Label1" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline" runat="server" Text="رقم الحساب"></asp:Label></td>
             <td><asp:Label ID="Label2" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline" runat="server" Text="اسم الحساب"></asp:Label></td>
             
             <td><asp:Label ID="Label3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline" runat="server" Text="رصيد مدين"></asp:Label></td>
             <td><asp:Label ID="Label4" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline" runat="server" Text="رصيد دائن"></asp:Label></td>
            <td><asp:Label ID="Label5" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline" runat="server" Text="بيان"></asp:Label></td>
         </tr>
         
         <tr class="NotToPrint" >
             <td><asp:TextBox ID="AccountNOTxt" CssClass="form-control" runat="server"  ></asp:TextBox></td>
             <td><asp:DropDownList ID="AccountNameDrop" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="AccountNameDrop_SelectedIndexChanged" ></asp:DropDownList></td>
             <td><asp:TextBox ID="Indebt_BalanceTxt" AutoPostBack="true" CssClass="form-control"   runat="server" OnTextChanged="Indebt_BalanceTxt_TextChanged"></asp:TextBox></td>
             <td><asp:TextBox ID="CreditTxt" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="CreditTxt_TextChanged"></asp:TextBox></td>
            <td><asp:TextBox ID="DesTxt" CssClass="form-control" runat="server"></asp:TextBox></td>
             <td> <asp:Button ID="addRowBtn" CssClass="btn btn-success" runat="server" Text="اضافة" OnClick="addRowBtn_Click"  /></td>

         </tr>
         <tr class="NotToPrint" >
             <td colspan="5">
                 <asp:Label ID="AddError" runat="server" Text=""></asp:Label>  
                   </td>
             
             
         </tr>
         </table>

 <div class="table-responsive mt-2">
     <asp:GridView ID="DiaryGrd" style="width:100%"  runat="server" BackColor="White" 
     BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False"
          OnRowDeleting="DiaryGrd_RowDeleting" OnRowEditing="DiaryGrd_RowEditing"
          OnRowUpdating="DiaryGrd_RowUpdating" OnRowCancelingEdit="DiaryGrd_RowCancelingEdit"  CssClass="table table-striped table-sm mb-0 text-center" ShowFooter="True">
      <FooterStyle BackColor="White" ForeColor="#333333"></FooterStyle>
                    <HeaderStyle Font-Bold="True" CssClass="thead-light" ForeColor="White"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Center" BackColor="#dddddd" ForeColor="black"></PagerStyle>
                    <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#487575"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#275353"></SortedDescendingHeaderStyle>
          <Columns>
          <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="ID">
                        <ItemTemplate>                            
                            <asp:Label id="RowNoLbl"  CssClass="form-control"   runat="server"  Text='<%# Eval("RecordID") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>
      <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رقم الحساب">
                        <ItemTemplate>                            
                            <asp:Label  ID="SubAccIDLbl" CssClass="form-control"   runat="server"  Text='<%# Eval("AccountNO") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>
      
      <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="اسم الحساب">
                        <ItemTemplate>
                            <asp:Label ID="AccountNameLbl"  CssClass="form-control"   runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                               </ItemTemplate>
          </asp:TemplateField>
      
            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="مدين">
                        <ItemTemplate>
                            <asp:Label ID="IndebtBalanceLbl"  CssClass="form-control"   runat="server"  Text='<%# Eval("IndebtBalance") %>'></asp:Label>
                             </ItemTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="IndebtBalanceTxt"  CssClass="form-control"   runat="server"  Text='<%# Eval("IndebtBalance") %>'></asp:TextBox>
                </EditItemTemplate>
          </asp:TemplateField>

             
         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="دائن">
                        <ItemTemplate>
                            <asp:Label ID="CreditBalanceLbl"  CssClass="form-control"   runat="server"  Text='<%# Eval("CreditBalance") %>'></asp:Label>
                             </ItemTemplate>
             <EditItemTemplate>
         <asp:textBox ID="CreditBalanceTxt"  CssClass="form-control"   runat="server"  Text='<%# Eval("CreditBalance") %>'></asp:textBox>

             </EditItemTemplate>

          </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="البيان">
                        <ItemTemplate>
                            <asp:Label ID="DescriptionLbl"  CssClass="form-control"   runat="server"  Text='<%# Eval("description") %>'></asp:Label>
                             </ItemTemplate>
                    <EditItemTemplate>
                <asp:Label ID="DescriptionTxt"  CssClass="form-control"   runat="server"  Text='<%# Eval("description") %>'></asp:Label>

                    </EditItemTemplate>
          </asp:TemplateField>

                            <%-- <asp:CommandField  ButtonType="Button"  ControlStyle-CssClass="btn-warning"  CancelText="الغاء" EditText="تعديل" ShowEditButton="True" UpdateText="تحديث" />

                           <asp:CommandField ButtonType="Button"  ControlStyle-CssClass="btn-danger" DeleteText="حذف" ShowDeleteButton="True" />--%>

          
         </Columns>
    

    <FooterStyle BackColor="White" ForeColor="#333333" />
    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="White" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F7F7F7" />
    <SortedAscendingHeaderStyle BackColor="#487575" />
    <SortedDescendingCellStyle BackColor="#E5E5E5" />
    <SortedDescendingHeaderStyle BackColor="#275353" />
</asp:GridView>
          </div>
    <div class="row" style="text-align: center;">
    <div class="form-group col-md-12 NotToPrint">
       
       
                 <asp:Button id="SaveBtn" Text="حفظ" CssClass="btn btn-success" runat="server" OnClick="SaveBtn_Click"  />
           
              <input id="PrintBtn"  type="button" class="btn btn-success" onclick="javascript:PrintGridDataCustom('prtg')" value="طباعة" />
                 <asp:Button id="DeleteBtn" Text="حذف" CssClass="btn btn-success" runat="server" OnClick="DeleteBtn_Click"  />

         </div>
        </div>
</asp:Content>

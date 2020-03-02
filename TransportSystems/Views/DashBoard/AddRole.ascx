<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddRole.ascx.cs" Inherits="TransportSystems.Views.DashBoard.AddRole" %>
<div class="input-group">
    <asp:DropDownList ID="RoleListhtml" runat="server" CssClass="browser-default custom-select" Width="100%">
    </asp:DropDownList>
    <div id="divmodal" runat="server" class="input-group-append" data-target="#RoleModal" data-toggle="modal">
        <span class="input-group-text btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm"><i class="flaticon2-add-1 white"></i></span>
    </div>
</div>
<div class="modal text-center" id="RoleModal" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4><%=Resources.Store.AddRole%> </h4>
                <button type="button" id="modal" class="close pull-right" data-dismiss="modal" aria-label="Close" value="Refresh Page" onclick="window.location.reload();">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div>
                            <div style="width: 100%">
                                <asp:HiddenField ID="RoleIdHid" runat="server" Value="0" />
                                <div class="form-group" style="width: 49%; display: inline-block;">
                                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Name%></label>
                                    <asp:TextBox ID="Role_Name" runat="server" CssClass="form-control col-md-12" placeholder="<%$Resources:Store,NamePH%>"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Role_Name"
                                        CssClass="alert-danger" ErrorMessage="<%$Resources:Store,NameEM%>">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="text-center">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert-danger" DisplayMode="List" />
                                <br />
                                <asp:Button ID="Save" runat="server" Text="<%$Resources:Store,Save%>" CssClass="btn btn-success" OnClick="Save_Click" />

                                <button id="button" runat="server" type="button" class="btn btn-warning" data-dismiss="modal" value="Refresh Page" onclick="window.location.reload();"
                                    aria-label="Close">
                                    <i class="la la-close"></i><%=Resources.Store.Close%></button>
                            </div>
                        </div>
                        <form>
                            <div class="table-responsive mt-3">
                                <asp:GridView ID="GridView1" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                                    runat="server" CssClass="table table-striped table-sm mb-0" GridLines="Horizontal">
                                    <FooterStyle BackColor="#ebedf2" ForeColor="#6c7293"></FooterStyle>
                                    <HeaderStyle Font-Bold="True" CssClass="thead-light" ForeColor="#6c7293"></HeaderStyle>
                                    <PagerStyle HorizontalAlign="Center" BackColor="#ebedf2" ForeColor="#6c7293"></PagerStyle>
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                #
                                           
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="<%$Resources:Store,Name%>" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <%=Resources.Store.Tools%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="edit_Btn" runat="server" CssClass="grid-tool" CausesValidation="False"
                                                    CommandArgument="<%# Container.DataItemIndex %>" OnCommand="edit_Btn_Command">
                                                    <i class="flaticon-edit-1"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="delete" runat="server" CssClass="grid-tool" CausesValidation="False"
                                                    OnClientClick="return confirm('Are you sure you would like Remove This Element?');"
                                                    CommandArgument="<%# Container.DataItemIndex %>" OnCommand="delete_Command">
                                                    <i class="flaticon-delete"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate><%=Resources.Store.GridNoRows%></EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>

<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HeatingPrograms.aspx.cs" Inherits="DigitalMicrowave.HeatingPrograms" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>

                <h2>Programas de Aquecimento</h2>

                <asp:Button ID="btnNew" runat="server" Text="Novo Programa" OnClick="btnNew_Click" />
                <br />
                <asp:GridView ID="gridPrograms" runat="server"
                    AutoGenerateColumns="false"
                    OnRowCommand="gridPrograms_RowCommand"
                    OnRowEditing="gridPrograms_RowEditing"
                    DataKeyNames="Id">

                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />

                        <asp:TemplateField HeaderText="Nome">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server"
                                    Text='<%# Eval("ProgramName") %>'
                                    CssClass='<%# !(bool)Eval("ProgramDefault") ? "italic" : "" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Food" HeaderText="Alimento" />
                        <asp:BoundField DataField="Time" HeaderText="Tempo(s)" />
                        <asp:BoundField DataField="Power" HeaderText="Potência" />
                        <asp:BoundField DataField="HeatingCharacteristic" HeaderText="Caractere" />

                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:Button Text="Editar" CommandName="edit"
                                    CommandArgument='<%# Eval("Id") %>' runat="server"
                                    Enabled='<%# !(bool)Eval("ProgramDefault") %>' />

                                <asp:Button Text="Excluir" CommandName="delete"
                                    CommandArgument='<%# Eval("Id") %>' runat="server"
                                    Enabled='<%# !(bool)Eval("ProgramDefault") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <!-- MODAL -->
                <asp:Panel ID="modal" runat="server" CssClass="modalBackground" Visible="false">
                    <asp:Panel runat="server" CssClass="modalPanel">
                        <h3 id="modalTitle" runat="server">Novo Programa</h3>

                        <asp:HiddenField ID="txtId" runat="server" />

                        Nome:<br />
                        <asp:TextBox ID="txtName" runat="server" /><br />
                        <br />

                        Alimento:<br />
                        <asp:TextBox ID="txtFood" runat="server" /><br />
                        <br />

                        Tempo (seg):<br />
                        <asp:TextBox ID="txtTime" runat="server" /><br />
                        <br />

                        Potência:<br />
                        <asp:TextBox ID="txtPower" runat="server" /><br />
                        <br />

                        Caractere (≠ "." e único):<br />
                        <asp:TextBox ID="txtChar" runat="server" /><br />
                        <br />

                        Instruções:<br />
                        <asp:TextBox ID="txtInstructions" runat="server" TextMode="MultiLine" /><br />
                        <br />

                        <asp:Label ID="lblError" runat="server" ForeColor="Red" /><br />

                        <asp:Button ID="btnSave" runat="server" Text="Salvar" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" />

                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <style>
            .italic { font-style: italic; }
            .modalBackground {
                background-color: rgba(0,0,0,0.4);
                position: fixed;
                top:0; left:0; width:100%; height:100%;
            }
            .modalPanel {
                background:white;
                padding:20px;
                width:400px;
                margin:100px auto;
                border-radius:6px;
            }
        </style>
    </main>
</asp:Content>

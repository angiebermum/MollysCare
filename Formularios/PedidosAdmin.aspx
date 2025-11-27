<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PedidosAdmin.aspx.cs" Inherits="MollysCare.Formularios.PedidosAdmin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Gestión de pedidos - Molly's Care</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container py-4">

        <h1 class="h4 mb-3">Gestión de pedidos</h1>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-muted d-block mb-3"></asp:Label>

        <asp:GridView ID="gvPedidos" runat="server"
            CssClass="table table-striped table-bordered table-sm"
            AutoGenerateColumns="False"
            DataKeyNames="IdPedido"
            OnRowEditing="gvPedidos_RowEditing"
            OnRowCancelingEdit="gvPedidos_RowCancelingEdit"
            OnRowUpdating="gvPedidos_RowUpdating"
            OnRowDataBound="gvPedidos_RowDataBound">

            <Columns>

            
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server"
                            CommandName="Edit"
                            Text="Editar"
                            CssClass="btn btn-sm btn-outline-primary me-1" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkGuardar" runat="server"
                            CommandName="Update"
                            Text="Guardar"
                            CssClass="btn btn-sm btn-primary me-1" />

                        <asp:LinkButton ID="lnkCancelar" runat="server"
                            CommandName="Cancel"
                            Text="Cancelar"
                            CssClass="btn btn-sm btn-secondary" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="IdPedido" HeaderText="N° pedido" ReadOnly="True" />
                <asp:BoundField DataField="Usuario" HeaderText="Cliente" ReadOnly="True" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}" ReadOnly="True" />
                <asp:BoundField DataField="Total" HeaderText="Total"
                    DataFormatString="₡{0:N2}" HtmlEncode="False" ReadOnly="True" />

              
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# Eval("Estado") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select form-select-sm">
                            <asp:ListItem Text="En proceso" Value="En proceso"></asp:ListItem>
                            <asp:ListItem Text="Enviado" Value="Enviado"></asp:ListItem>
                            <asp:ListItem Text="Entregado" Value="Entregado"></asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <hr class="my-4" />
        <a href="Menu.aspx" class="btn btn-outline-secondary btn-sm">Volver al menú</a>

    </form>
</body>
</html>

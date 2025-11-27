<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistorialPedidos.aspx.cs" Inherits="MollysCare.Formularios.HistorialPedidos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Historial de pedidos - Molly's Care</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container py-4">

        <h1 class="h4 mb-1">Historial de pedidos</h1>
        <asp:Label ID="lblRol" runat="server" CssClass="text-muted d-block mb-1"></asp:Label>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-muted d-block mb-3"></asp:Label>

        <asp:GridView ID="gvPedidosCliente" runat="server"
            CssClass="table table-striped table-bordered table-sm"
            AutoGenerateColumns="False"
            EmptyDataText="Aún no tienes pedidos registrados.">

            <Columns>
                <asp:BoundField DataField="IdPedido" HeaderText="N° pedido" />
                <asp:BoundField DataField="Fecha"
                    HeaderText="Fecha"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Total"
                    HeaderText="Total"
                    DataFormatString="₡{0:N2}" HtmlEncode="False" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
        </asp:GridView>

        <hr class="my-4" />
        <a href="Menu.aspx" class="btn btn-outline-secondary btn-sm">Volver al menú</a>

    </form>
</body>
</html>


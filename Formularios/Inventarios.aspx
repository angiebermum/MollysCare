<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="MollysCare.Formularios.Inventario" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Inventario - Molly's Care</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container py-4">

        <h1 class="h4 mb-1">Inventario de productos</h1>
        <asp:Label ID="lblRol" runat="server" CssClass="text-muted d-block mb-3"></asp:Label>

        <p class="mb-3">
            En esta pantalla se muestra la estructura del inventario basada en
            <strong>Stock actual</strong> y <strong>Stock mínimo</strong>.
            Los productos con stock por debajo del mínimo se marcan en rojo.
        </p>

        <asp:GridView ID="gvInventario" runat="server"
            CssClass="table table-bordered table-sm"
            AutoGenerateColumns="False"
            OnRowDataBound="gvInventario_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IdProducto" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                <asp:BoundField DataField="StockActual" HeaderText="Stock actual" />
                <asp:BoundField DataField="StockMinimo" HeaderText="Stock mínimo" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
        </asp:GridView>

        <small class="text-muted d-block mb-3">
            * Estado "Stock bajo" indica productos donde el stock actual está por debajo del mínimo.
        </small>

        <a href="Menu.aspx" class="btn btn-outline-secondary">Volver al menú</a>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

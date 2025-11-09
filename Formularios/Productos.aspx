<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="MollysCare.Formularios.Productos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Productos - Molly's Care</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container py-4">

        <h1 class="h4 mb-1">Catálogo de productos</h1>
        <asp:Label ID="lblRol" runat="server" CssClass="text-muted d-block mb-3"></asp:Label>

        <asp:GridView ID="gvProductos" runat="server"
            CssClass="table table-striped table-bordered table-sm"
            AutoGenerateColumns="False"
            DataKeyNames="IdProducto"
            OnRowEditing="gvProductos_RowEditing"
            OnRowCancelingEdit="gvProductos_RowCancelingEdit"
            OnRowUpdating="gvProductos_RowUpdating"
            OnRowDeleting="gvProductos_RowDeleting">

            <Columns>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" HeaderText="Acciones" />

                <asp:BoundField DataField="IdProducto" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                <asp:BoundField DataField="Precio" HeaderText="Precio"
                    DataFormatString="₡{0:N2}" HtmlEncode="False" />
                <asp:BoundField DataField="StockActual" HeaderText="Stock" />
                <asp:BoundField DataField="StockMinimo" HeaderText="Stock mínimo" />
            </Columns>
        </asp:GridView>

       
        <asp:Panel ID="pnlAdmin" runat="server">
            <hr class="my-4" />

            <h2 class="h5 mb-3">Registrar nuevo producto (solo administrador)</h2>

            <div class="row g-3">
                <div class="col-md-4">
                    <label class="form-label" for="txtNombre">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label" for="txtCategoria">Categoría</label>
                    <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label" for="txtMarca">Marca</label>
                    <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label class="form-label" for="txtPrecio">Precio</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label" for="txtStockActual">Stock actual</label>
                    <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label" for="txtStockMinimo">Stock mínimo</label>
                    <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" />
                </div>

                <div class="col-12">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mb-2"></asp:Label>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar producto" CssClass="btn btn-primary"
                        OnClick="btnGuardar_Click" />
                </div>
            </div>
        </asp:Panel>

        <hr class="my-4" />
        <a href="Menu.aspx" class="btn btn-outline-secondary">Volver al menú</a>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

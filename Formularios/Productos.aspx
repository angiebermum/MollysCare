<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="MollysCare.Formularios.Productos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Productos - Molly's Care</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .btn-carrito {
            border-radius: 999px;
            border: 1px solid #111827;
            background-color: #ffffff;
            color: #111827;
            font-weight: 600;
            padding: 3px 14px;
        }

        .btn-carrito:hover {
            background-color: #f3f4f6;
            color: #111827;
        }

        .btn-filtro {
            border-radius: 999px;
            border: none;
            background: linear-gradient(120deg, #ff7ac4, #6bc6ff);
            color: #ffffff;
            font-weight: 600;
        }

        .btn-filtro:hover {
            opacity: 0.9;
            color: #ffffff;
        }

        .filtro-input {
            border: 2px solid #111827;
            border-radius: .75rem;
            font-weight: 700;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="container py-4">

        <h1 class="h4 mb-1">Catálogo de productos</h1>

        <asp:Label ID="lblRol" runat="server" CssClass="text-muted d-block mb-1"></asp:Label>
        <asp:Label ID="lblCarritoMensaje" runat="server" CssClass="text-success d-block mb-3"></asp:Label>

      
        <div class="card mb-3">
            <div class="card-body py-2">
                <div class="row g-2 align-items-end">
                    <div class="col-md-4">
                        <label for="txtFiltroNombre" class="form-label mb-1">Nombre</label>
                        <input type="text" id="txtFiltroNombre"
                               class="form-control form-control-sm filtro-input"
                               placeholder="Buscar por nombre" />
                    </div>

                    <div class="col-md-3">
                        <label for="ddlFiltroCategoria" class="form-label mb-1">Categoría</label>
                        <select id="ddlFiltroCategoria"
                                class="form-select form-select-sm filtro-input">
                            <option value="">Todas</option>
                            <option value="alimento">Alimento</option>
                            <option value="higiene">Higiene</option>
                            <option value="prueba">Prueba</option>
                        </select>
                    </div>

                    <div class="col-md-2">
                        <label for="txtFiltroPrecioMin" class="form-label mb-1">Precio mín.</label>
                        <input type="number" id="txtFiltroPrecioMin"
                               class="form-control form-control-sm filtro-input" />
                    </div>

                    <div class="col-md-2">
                        <label for="txtFiltroPrecioMax" class="form-label mb-1">Precio máx.</label>
                        <input type="number" id="txtFiltroPrecioMax"
                               class="form-control form-control-sm filtro-input" />
                    </div>

                    <div class="col-md-1 d-grid">
                        <button type="button" class="btn btn-sm btn-filtro" onclick="aplicarFiltros()">Filtrar</button>
                    </div>
                    <div class="col-md-1 d-grid">
                        <button type="button" class="btn btn-sm btn-outline-secondary"
                                onclick="limpiarFiltros()">Limpiar</button>
                    </div>
                </div>
            </div>
        </div>

        
        <asp:GridView ID="gvProductos" runat="server"
            CssClass="table table-striped table-bordered table-sm"
            AutoGenerateColumns="False"
            DataKeyNames="IdProducto"
            OnRowEditing="gvProductos_RowEditing"
            OnRowCancelingEdit="gvProductos_RowCancelingEdit"
            OnRowUpdating="gvProductos_RowUpdating"
            OnRowDeleting="gvProductos_RowDeleting"
            OnRowCommand="gvProductos_RowCommand">

            <Columns>
         
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server"
                            CommandName="Edit"
                            Text="Editar"
                            CssClass="btn btn-sm btn-outline-primary me-1" />
                        <asp:LinkButton ID="lnkEliminar" runat="server"
                            CommandName="Delete"
                            Text="Eliminar"
                            CssClass="btn btn-sm btn-outline-danger"
                            OnClientClick="return confirm('¿Desea eliminar este producto?');" />
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

              
                <asp:TemplateField HeaderText="Carrito">
                    <ItemTemplate>
                        <asp:Button ID="btnAgregarCarrito" runat="server"
                            Text="Añadir"
                            CssClass="btn btn-sm btn-carrito"
                            CommandName="AgregarCarrito"
                            CommandArgument='<%# Eval("IdProducto") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="IdProducto" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
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

                <div class="col-12">
                    <label class="form-label" for="txtDescripcion">Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server"
                        CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </div>

                <div class="col-md-4">
                    <label class="form-label" for="txtProveedor">Proveedor</label>
                    <asp:TextBox ID="txtProveedor" runat="server" CssClass="form-control" />
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
                    <asp:Button ID="btnGuardar" runat="server"
                        Text="Guardar producto" CssClass="btn btn-primary"
                        OnClick="btnGuardar_Click" />
                </div>
            </div>
        </asp:Panel>

        <hr class="my-4" />
        <a href="Menu.aspx" class="btn btn-outline-secondary">Volver al menú</a>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

   
    <script type="text/javascript">
        function normalizarPrecio(texto) {
            if (!texto) return NaN;

            var limpio = texto
                .replace(/[^\d.,-]/g, '')
                .replace(/\./g, '')
                .replace(',', '.');

            var valor = parseFloat(limpio);
            return isNaN(valor) ? NaN : valor;
        }

        function obtenerIndiceColumnaPorTitulo(grid, tituloBuscado) {
            var ths = grid.getElementsByTagName('th');
            tituloBuscado = tituloBuscado.toLowerCase();

            for (var i = 0; i < ths.length; i++) {
                var texto = ths[i].innerText.toLowerCase();
                if (texto.indexOf(tituloBuscado) !== -1) {
                    return i;
                }
            }
            return -1;
        }

        function aplicarFiltros() {
            var nombre = document.getElementById('txtFiltroNombre').value.toLowerCase();
            var categoria = document.getElementById('ddlFiltroCategoria').value.toLowerCase();
            var precioMin = parseFloat(document.getElementById('txtFiltroPrecioMin').value);
            var precioMax = parseFloat(document.getElementById('txtFiltroPrecioMax').value);

            if (isNaN(precioMin)) precioMin = NaN;
            if (isNaN(precioMax)) precioMax = NaN;

            var grid = document.getElementById('<%= gvProductos.ClientID %>');
            if (!grid) return;

            var idxNombre = obtenerIndiceColumnaPorTitulo(grid, 'nombre');
            var idxCategoria = obtenerIndiceColumnaPorTitulo(grid, 'categoría');
            var idxPrecio = obtenerIndiceColumnaPorTitulo(grid, 'precio');

            if (idxNombre === -1 || idxCategoria === -1 || idxPrecio === -1) {
                console.warn('No se pudieron encontrar las columnas de filtro');
                return;
            }

            var rows = grid.getElementsByTagName('tr');

            for (var i = 1; i < rows.length; i++) {
                var cells = rows[i].getElementsByTagName('td');
                if (cells.length === 0) continue;

                var nombreCell = cells[idxNombre].innerText.toLowerCase();
                var categoriaCell = cells[idxCategoria].innerText.toLowerCase();
                var precioCell = cells[idxPrecio].innerText;
                var precioValor = normalizarPrecio(precioCell);

                var visible = true;

                if (nombre && nombreCell.indexOf(nombre) === -1) visible = false;
                if (categoria && categoriaCell !== categoria) visible = false;
                if (!isNaN(precioMin) && (isNaN(precioValor) || precioValor < precioMin)) visible = false;
                if (!isNaN(precioMax) && (isNaN(precioValor) || precioValor > precioMax)) visible = false;

                rows[i].style.display = visible ? '' : 'none';
            }
        }

        function limpiarFiltros() {
            document.getElementById('txtFiltroNombre').value = '';
            document.getElementById('ddlFiltroCategoria').value = '';
            document.getElementById('txtFiltroPrecioMin').value = '';
            document.getElementById('txtFiltroPrecioMax').value = '';
            aplicarFiltros();
        }
    </script>
</body>
</html>

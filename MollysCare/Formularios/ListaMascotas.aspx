<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaMascotas.aspx.cs" Inherits="TuProyecto.ListaMascotas" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Lista de Mascotas</title>
    <meta charset="utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        :root{ --pastel-rose:#FFD6E7; --pastel-mint:#CFF6E3; --pastel-lav:#E6E6FA; --pastel-sky:#D8F0FF; --pastel-peach:#FFE4C7; }
        body{
            background: radial-gradient(circle at 20% 10%, var(--pastel-rose), transparent 60%),
                        radial-gradient(circle at 80% 20%, var(--pastel-sky), transparent 60%),
                        radial-gradient(circle at 20% 80%, var(--pastel-mint), transparent 60%),
                        radial-gradient(circle at 90% 85%, var(--pastel-peach), transparent 60%),
                        var(--pastel-lav);
            font-family: Arial, sans-serif;
        }
        .card{ border:none; border-radius:1rem; box-shadow:0 8px 20px rgba(0,0,0,.08); background-color:#ffffffcc; backdrop-filter:blur(6px); }
        h2{ font-weight:800; color:#333; }
        .form-control{ border:2px solid var(--pastel-mint); border-radius:8px; }
        .form-control:focus{ border-color:var(--pastel-rose); box-shadow:0 0 5px var(--pastel-rose); }
        .btn-pastel{ background-color:var(--pastel-mint); border:none; color:#333; font-weight:800; }
        .btn-pastel:hover{ background-color:var(--pastel-rose); color:#000; }
        .mensaje-centrado{ text-align:center; font-weight:800; font-size:1.05rem; }
        .table thead th{ background: var(--pastel-sky); }
        .table-bordered> :not(caption)>*{ border-color: rgba(0,0,0,.08); }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="container py-5">
        <div class="card p-4 p-md-5">
            <h2 class="mb-3">Mascotas registradas</h2>

            <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="mensaje-centrado d-block mb-2"></asp:Label>

            <div class="d-flex flex-wrap gap-2 mb-3">
                <div class="input-group" style="max-width:380px;">
                    <span class="input-group-text">Buscar</span>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Nombre de la mascota" />
                </div>
                <asp:Button ID="btnBuscar" runat="server" Text="Filtrar" CssClass="btn btn-pastel" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-pastel" OnClick="btnLimpiar_Click" CausesValidation="false" />
                <div class="ms-auto d-flex gap-2">
                    <asp:Button ID="btnNueva" runat="server" Text="Nueva mascota" CssClass="btn btn-pastel" OnClick="btnNueva_Click" CausesValidation="false" />
                    <asp:Button ID="btnVolver" runat="server" Text="Regresar" CssClass="btn btn-pastel" OnClick="btnVolver_Click" CausesValidation="false" />
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gvMascotas" runat="server"
              AutoGenerateColumns="False"
              DataKeyNames="IdMascota"
              CssClass="table table-hover table-bordered align-middle"
              HeaderStyle-CssClass="table-light"
              AlternatingRowStyle-BackColor="#FAFAFA"
              EmptyDataText="No hay mascotas para mostrar."
              AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMascotas_PageIndexChanging"
              AllowSorting="true" OnSorting="gvMascotas_Sorting"
              OnRowEditing="gvMascotas_RowEditing" OnRowCancelingEdit="gvMascotas_RowCancelingEdit"
              OnRowUpdating="gvMascotas_RowUpdating" OnRowDeleting="gvMascotas_RowDeleting">

    <Columns>
        <asp:BoundField DataField="IdMascota" HeaderText="ID" ReadOnly="true" SortExpression="IdMascota" />

        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />

       
        <asp:TemplateField HeaderText="Nacimiento" SortExpression="FechaNacimiento">
            <ItemTemplate>
                <%# Eval("FechaNacimiento", "{0:yyyy-MM-dd}") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtFechaEdit" runat="server" Text='<%# Bind("FechaNacimiento","{0:yyyy-MM-dd}") %>' TextMode="Date" CssClass="form-control form-control-sm" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Especie" HeaderText="Especie" SortExpression="Especie" ReadOnly="true" />
        <asp:BoundField DataField="Raza" HeaderText="Raza" SortExpression="Raza" ReadOnly="true" />
        <asp:BoundField DataField="Dueno" HeaderText="Dueño" SortExpression="Dueno" ReadOnly="true" />

        <asp:CommandField ShowEditButton="True" EditText="Editar" UpdateText="Guardar" CancelText="Cancelar"
                          ShowDeleteButton="True" DeleteText="Eliminar">
            <ControlStyle CssClass="btn btn-sm btn-outline-secondary me-1" />
        </asp:CommandField>
    </Columns>
</asp:GridView>

            </div>
        </div>
    </div>
</form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientesAdmin.aspx.cs" Inherits="MollysCare.Formularios.ClientesAdmin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Clientes - Molly's Care</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>

    <style>
    :root{
        --pastel-rose:#FFD6E7;
        --pastel-mint:#CFF6E3;
        --pastel-lav:#E6E6FA;
        --pastel-sky:#D8F0FF;
        --pastel-peach:#FFE4C7;
        --text:#333333;
        --card-bg:#ffffffcc;
    }

    html,body{ height:100%; }

    body{
        background: radial-gradient(circle at 20% 10%, var(--pastel-rose), transparent 60%),
                    radial-gradient(circle at 80% 20%, var(--pastel-sky), transparent 60%),
                    radial-gradient(circle at 20% 80%, var(--pastel-mint), transparent 60%),
                    radial-gradient(circle at 90% 85%, var(--pastel-peach), transparent 60%),
                    var(--pastel-lav);
        color:var(--text);
        font-weight:700;
    }

    .page-wrapper{ max-width: 650px; }
    .page-card{
        background-color: var(--card-bg);
        backdrop-filter: blur(6px);
        border-radius: 1.25rem;
        border: 1px solid #111827;
        box-shadow: 0 10px 30px rgba(0,0,0,.08);
    }

    .page-title{
        font-size: 1.6rem;
        font-weight: 800;
        margin-bottom: .25rem;
    }

    .page-subtitle{
        font-size: .9rem;
        opacity: .8;
        margin-bottom: 1.5rem;
    }

    /* Inputs “gorditos” en negrita */
    .molly-input{
        border: 2px solid #111827 !important;
        border-radius: .75rem !important;
        font-weight: 700 !important;
        box-shadow: none !important;
    }

    label, .form-label{
        font-weight: 700;
    }

    /* Botones principales */
    .molly-btn-primary{
        font-weight: 700;
        border-radius: .75rem;
    }

    .molly-btn-outline{
        font-weight: 700;
        border-radius: .75rem;
        border-width: 2px;
    }

    /* Tablas (para ClientesAdmin) */
    .molly-grid{
        background-color:#fff;
        border:2px solid #111827;
        border-radius: 1rem;
        overflow:hidden;
        font-weight:700;
    }

    .molly-grid th{
        background-color:#f9fafb;
        border-bottom:2px solid #111827 !important;
        font-weight:800;
    }

    .molly-grid td{
        border-top:1px solid #e5e7eb !important;
    }

    .molly-tag{
        display:inline-block;
        padding:2px 10px;
        border-radius:999px;
        border:1px solid #111827;
        font-size:.8rem;
    }
</style>

<body>
    <form id="form1" runat="server" class="container py-4">

        <h1 class="h4 mb-1">Gestión de clientes</h1>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-muted d-block mb-3"></asp:Label>

        <asp:GridView ID="gvClientes" runat="server"
            CssClass="table table-striped table-bordered table-sm"
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Correo" HeaderText="Correo electrónico" />
                <asp:BoundField DataField="CantidadPedidos" HeaderText="Pedidos realizados" />
                <asp:BoundField DataField="MontoTotal"
                    HeaderText="Total comprado"
                    DataFormatString="₡{0:N2}" HtmlEncode="False" />
            </Columns>
        </asp:GridView>

        <hr class="my-4" />
        <a href="Menu.aspx" class="btn btn-outline-secondary btn-sm">Volver al menú</a>

    </form>
</body>
</html>

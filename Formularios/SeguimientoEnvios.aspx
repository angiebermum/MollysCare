<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="SeguimientoEnvios.aspx.cs"
    Inherits="MollysCare.Formularios.SeguimientoEnvios" %>

<%
    if (Session["Usuario"] == null ||
        (Session["Rol"] ?? "").ToString().ToUpperInvariant() != "ADMIN")
    {
        Response.Redirect("Login.aspx");
    }
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Seguimiento de envíos - Molly's Care</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

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

        .envios-wrapper{ max-width: 950px; }

        .envios-card{
            background-color: var(--card-bg);
            backdrop-filter: blur(8px);
            border-radius: 1.25rem;
            box-shadow: 0 10px 30px rgba(0,0,0,.08);
            border: 2px solid #111827;
        }

        .brand-badge{
            background: linear-gradient(135deg, #ff9ac4, #a9e9d2);
            -webkit-background-clip: text;
            background-clip: text;
            color: transparent;
            font-weight:800;
            letter-spacing:.5px;
        }

        .envios-table{
            font-weight:700;
            border: 2px solid #111827;
            border-radius: .75rem;
            overflow: hidden;
            background-color:#ffffff;
        }

        .envios-table thead{
            background-color: var(--pastel-sky);
        }

        .envios-table th,
        .envios-table td{
            border: 1px solid #111827 !important;
            vertical-align: middle;
        }

        .btn-primary{
            border-radius:999px;
            border:2px solid #111827;
            font-weight:700;
            background: linear-gradient(120deg, #ff7ac4, #6bc6ff);
        }

        .btn-outline-secondary{
            border-radius:999px;
            border:2px solid #111827;
            font-weight:700;
            color:#111827;
        }

        .btn-outline-secondary:hover{
            color:#111827;
            background-color:#f3f4f6;
        }

        .envios-meta{
            font-size:.9rem;
            opacity:.8;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <main class="container py-5 envios-wrapper">
            <div class="card envios-card">
                <div class="card-body p-4 p-md-5">

                    <div class="d-flex align-items-center justify-content-between gap-3 mb-4">
                        <div class="d-flex align-items-center gap-3">
                            <img src='<%= ResolveUrl("~/images/logo.png") %>' alt="Molly's Care" style="max-width:120px; height:auto;">
                            <div>
                                <h1 class="h4 mb-1 brand-badge" style="font-size: 1.9rem;">Molly's Care</h1>
                                <p class="mb-0">Gestión de envíos — Pedidos y estado de entrega.</p>
                            </div>
                        </div>
                    </div>

                    <asp:Label ID="lblMensaje" runat="server"
                               CssClass="envios-meta d-block mb-3"></asp:Label>

                    <asp:GridView ID="gvEnvios" runat="server"
                        CssClass="table table-sm envios-table"
                        AutoGenerateColumns="False"
                        OnRowCommand="gvEnvios_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IdPedido" HeaderText="Pedido #" />
                            <asp:BoundField DataField="Total" HeaderText="Total"
                                DataFormatString="₡{0:N2}" HtmlEncode="False" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado del envío" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnActualizar" runat="server"
                                        CommandName="ActualizarEstado"
                                        CommandArgument='<%# Eval("IdPedido") %>'
                                        CssClass="btn btn-primary btn-sm">
                                        Actualizar estado
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <div class="mt-3 d-flex flex-wrap gap-2">
                        <a href="Carrito.aspx" class="btn btn-outline-secondary btn-sm">
                            <i class="bi bi-cart-check"></i> Ir al carrito
                        </a>
                        <a href="Menu.aspx" class="btn btn-outline-secondary btn-sm">
                            <i class="bi bi-house-door"></i> Volver al menú
                        </a>
                    </div>

                </div>
            </div>
        </main>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>

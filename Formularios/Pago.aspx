<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Pago.aspx.cs"
    Inherits="MollysCare.Formularios.Pago" %>

<%
    
    if (Session["Usuario"] == null ||
        (Session["Rol"] ?? "").ToString().ToUpperInvariant() != "CLIENTE")
    {
        Response.Redirect("Menu.aspx");
    }
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Pago del pedido - Molly's Care</title>

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

        .pago-wrapper{
            max-width: 700px;
        }

        .pago-card{
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

        .pago-meta{
            font-size:.9rem;
            opacity:.8;
        }

        .monto-box{
            border-radius: .75rem;
            border: 2px solid #111827;
            background-color:#ffffff;
            padding: .75rem 1rem;
            font-weight:700;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <main class="container py-5 pago-wrapper">
            <div class="card pago-card">
                <div class="card-body p-4 p-md-5">

                    
                    <div class="d-flex align-items-center justify-content-between gap-3 mb-4">
                        <div class="d-flex align-items-center gap-3">
                            <img src='<%= ResolveUrl("~/images/logo.png") %>' alt="Molly's Care" style="max-width:120px; height:auto;">
                            <div>
                                <h1 class="h4 mb-1 brand-badge" style="font-size: 1.9rem;">Molly's Care</h1>
                                <p class="mb-0">Pago del pedido — Integración con PayPal y Stripe .</p>
                            </div>
                        </div>
                    </div>

                    <asp:Label ID="lblMensaje" runat="server"
                               CssClass="pago-meta d-block mb-3"></asp:Label>

                    <div class="mb-3">
                        <label class="form-label" for="txtMonto">Monto a pagar</label>
                        <div class="monto-box d-inline-flex align-items-center gap-2">
                            <asp:TextBox ID="txtMonto" runat="server"
                                         CssClass="form-control form-control-sm border-0 p-0"
                                         ReadOnly="true" />
                        </div>
                        <asp:Label ID="lblDetalleMonto" runat="server"
                                   CssClass="pago-meta d-block mt-1"></asp:Label>
                    </div>

                    <div class="mb-3">
                        <label class="form-label" for="ddlMetodo">Método de pago</label>
                        <asp:DropDownList ID="ddlMetodo" runat="server"
                                          CssClass="form-select">
                            <asp:ListItem Text="PayPal (simulado)" Value="PAYPAL" />
                            <asp:ListItem Text="Stripe (simulado)" Value="STRIPE" />
                        </asp:DropDownList>
                        <span class="pago-meta d-block mt-1">
                            En un entorno real aquí se redirigiría a la pasarela seleccionada.
                        </span>
                    </div>

                  
                    <div class="mb-3">
                        <label class="form-label" for="txtCorreo">Correo del cliente</label>
                        <asp:TextBox ID="txtCorreo" runat="server"
                                     CssClass="form-control"
                                     TextMode="Email" />
                    </div>

              
                    <div class="mb-3">
                        <label class="form-label" for="txtDescripcion">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server"
                                     CssClass="form-control"
                                     TextMode="MultiLine"
                                     Rows="3" />
                    </div>

                   
                    <div class="mt-3 d-flex flex-wrap gap-2">
                        <asp:Button ID="btnPagar" runat="server"
                                    Text="Pagar ahora"
                                    CssClass="btn btn-primary btn-sm"
                                    OnClick="btnPagar_Click" />

                        <a href="Carrito.aspx" class="btn btn-outline-secondary btn-sm">
                            <i class="bi bi-cart-check"></i> Volver al carrito
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

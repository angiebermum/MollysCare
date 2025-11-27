<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="MollysCare.Formularios.MiPerfil" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Mi perfil - Molly's Care</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

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

        .molly-input{
            border: 2px solid #111827 !important;
            border-radius: .75rem !important;
            font-weight: 700 !important;
            box-shadow: none !important;
        }

        label, .form-label{
            font-weight: 700;
        }

        .molly-btn-primary{
            font-weight: 700;
            border-radius: .75rem;
        }

        .molly-btn-outline{
            font-weight: 700;
            border-radius: .75rem;
            border-width: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <main class="container py-5 page-wrapper">
            <div class="card page-card">
                <div class="card-body p-4">

                   
                    <div class="mb-4 text-center">
                        <img src='<%= ResolveUrl("~/images/logo.png") %>' alt="Molly's Care"
                             style="max-width:120px; height:auto;" />
                        <h1 class="page-title mt-3">Mi perfil</h1>
                        <p class="page-subtitle">Consulta y actualiza tus datos personales.</p>
                    </div>

                    <asp:Label ID="lblInfoRol" runat="server" CssClass="text-muted d-block mb-2"></asp:Label>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mb-3"></asp:Label>

                    <div class="mb-3">
                        <label class="form-label" for="txtNombre">Nombre completo</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control molly-input" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Correo electrónico</label>
                        <asp:Label ID="lblCorreo" runat="server"
                                   CssClass="form-control-plaintext fw-bold"></asp:Label>
                    </div>

                    <hr class="my-4" />

                    <p class="fw-semibold mb-1">Cambio de contraseña (opcional)</p>
                    <p class="text-muted" style="font-size: .9rem;">
                        Si no desea cambiar la contraseña, deje estos campos en blanco.
                    </p>

                    <div class="mb-3">
                        <label class="form-label" for="txtNuevaContrasena">Nueva contraseña</label>
                        <asp:TextBox ID="txtNuevaContrasena" runat="server"
                                     TextMode="Password"
                                     CssClass="form-control molly-input" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label" for="txtConfirmarContrasena">Confirmar nueva contraseña</label>
                        <asp:TextBox ID="txtConfirmarContrasena" runat="server"
                                     TextMode="Password"
                                     CssClass="form-control molly-input" />
                    </div>

                    <div class="d-flex gap-2 mt-3">
                        <asp:Button ID="btnGuardar" runat="server"
                            Text="Guardar cambios"
                            CssClass="btn btn-primary molly-btn-primary"
                            OnClick="btnGuardar_Click" />
                        <a href="Menu.aspx" class="btn btn-outline-secondary molly-btn-outline">
                            Volver al menú
                        </a>
                    </div>

                </div>
            </div>
        </main>
    </form>
</body>
</html>


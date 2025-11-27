<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Informacion.aspx.cs" Inherits="MollysCare.Formularios.Informacion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Información del negocio - Molly's Care</title>

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

        .info-wrapper{
            max-width: 950px;
        }

        .info-card{
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

        .info-label{
            font-weight:700;
            margin-bottom: .2rem;
        }

        .form-control{
            border-radius: .75rem;
            border: 2px solid #111827;
            font-weight:700;
        }

        .form-control[readonly]{
            background-color:#f9fafb;
            cursor: default;
        }

        textarea.form-control{
            resize: vertical;
        }

        .section-title{
            font-size: 1rem;
            text-transform: uppercase;
            letter-spacing: .06em;
            color:#111827;
            margin-top:.75rem;
            margin-bottom:.25rem;
        }

        .section-divider{
            border-top:2px solid #111827;
            margin: .75rem 0 1rem 0;
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

        .info-meta{
            font-size:.9rem;
            opacity:.8;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <main class="container py-5 info-wrapper">
            <div class="card info-card">
                <div class="card-body p-4 p-md-5">

               
                    <div class="d-flex align-items-center justify-content-between gap-3 mb-4">
                        <div class="d-flex align-items-center gap-3">
                            <img src='<%= ResolveUrl("~/images/logo.png") %>' alt="Molly's Care" style="max-width:120px; height:auto;">
                            <div>
                                <h1 class="h4 mb-1 brand-badge" style="font-size: 1.9rem;">Molly's Care</h1>
                                <p class="mb-0">Información del negocio — Quiénes somos y cómo contactarnos.</p>
                            </div>
                        </div>
                    </div>

                    <asp:Label ID="lblRolInfo" runat="server" CssClass="text-muted d-block mb-2 info-meta"></asp:Label>
                    <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-3"></asp:Label>

                 
                    <div class="section-title">Datos generales</div>
                    <div class="section-divider"></div>

                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="info-label" for="txtNombreNegocio">Nombre del negocio</label>
                            <asp:TextBox ID="txtNombreNegocio" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-6">
                            <label class="info-label" for="txtDireccion">Dirección</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-6">
                            <label class="info-label" for="txtTelefono">Teléfono</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-6">
                            <label class="info-label" for="txtCorreo">Correo electrónico</label>
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-6">
                            <label class="info-label" for="txtInstagram">Instagram</label>
                            <asp:TextBox ID="txtInstagram" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                
                    <div class="section-title mt-4">Quiénes somos</div>
                    <div class="section-divider"></div>

                    <div class="mb-3">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>

               
                    <div class="section-title mt-3">Política de devoluciones</div>
                    <div class="section-divider"></div>

                    <div class="mb-3">
                        <asp:TextBox ID="txtPolitica" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>

                    <div class="mt-2 d-flex gap-2">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar cambios"
                            CssClass="btn btn-primary btn-sm" OnClick="btnGuardar_Click" />
                        <a href="Menu.aspx" class="btn btn-outline-secondary btn-sm">Volver al menú</a>
                    </div>

                    <asp:Label ID="lblNotaSoloLectura" runat="server" CssClass="text-muted d-block mt-2 info-meta"></asp:Label>

                </div>
            </div>
        </main>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>

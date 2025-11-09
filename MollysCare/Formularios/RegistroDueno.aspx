<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroDueno.aspx.cs" Inherits="MollysCare.Formularios.RegistroDueno" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Registrar Dueño</title>
    <meta charset="utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        :root{
            --pastel-rose:#FFD6E7; --pastel-mint:#CFF6E3; --pastel-lav:#E6E6FA;
            --pastel-sky:#D8F0FF; --pastel-peach:#FFE4C7;
        }
        body{
            background: radial-gradient(circle at 20% 10%, var(--pastel-rose), transparent 60%),
                        radial-gradient(circle at 80% 20%, var(--pastel-sky), transparent 60%),
                        radial-gradient(circle at 20% 80%, var(--pastel-mint), transparent 60%),
                        radial-gradient(circle at 90% 85%, var(--pastel-peach), transparent 60%),
                        var(--pastel-lav);
            font-family: Arial, sans-serif;
        }
        .card{ border:none; border-radius:1rem; box-shadow:0 8px 20px rgba(0,0,0,.08);
               background-color:#ffffffcc; backdrop-filter:blur(6px); }
        h2{ font-weight:800; color:#333; }
        .form-label{ font-weight:700; color:#444; }
        .form-control{
            border:2px solid var(--pastel-mint); border-radius:8px;
            transition:border-color .3s, box-shadow .3s;
        }
        .form-control:focus{ border-color:var(--pastel-rose); box-shadow:0 0 5px var(--pastel-rose); }
        .btn-pastel{ background-color:var(--pastel-mint); border:none; color:#333; font-weight:800; }
        .btn-pastel:hover{ background-color:var(--pastel-rose); color:#000; }
        .mensaje-centrado{ text-align:center; font-weight:800; font-size:1.05rem; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-md-7 col-lg-6">
                <div class="card p-4 p-md-5">
                    <h2 class="text-center mb-4">Registrar Dueño</h2>

                    <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="mensaje-centrado d-block mb-3"></asp:Label>

                    <div class="mb-3">
                        <asp:Label runat="server" Text="Nombre completo:" AssociatedControlID="txtNombre" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <asp:Label runat="server" Text="Teléfono:" AssociatedControlID="txtTelefono" CssClass="form-label" />
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <asp:Label runat="server" Text="Correo:" AssociatedControlID="txtCorreo" CssClass="form-label" />
                        <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
                    </div>

                    <div class="mb-3">
                        <asp:Label runat="server" Text="Dirección:" AssociatedControlID="txtDireccion" CssClass="form-label" />
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>

                    <div class="d-flex gap-2">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-pastel btn-lg flex-fill"
                                    OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnVolver" runat="server" Text="Regresar" CssClass="btn btn-pastel btn-lg flex-fill"
                                    OnClick="btnVolver_Click" CausesValidation="false" />
                    </div>

                    <div class="text-center mt-3">
                        <a href="RegistroMascota.aspx" class="btn btn-link fw-bold">Registrar mascota ahora →</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

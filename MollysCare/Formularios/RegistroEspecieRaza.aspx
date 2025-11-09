<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroEspecieRaza.aspx.cs" Inherits="MollysCare.Formularios.RegistroEspecieRaza" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Registro de Especie y Raza</title>
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
        .card{
            border:none; border-radius:1rem; box-shadow:0 8px 20px rgba(0,0,0,.08);
            background-color:#ffffffcc; backdrop-filter:blur(6px);
        }
        h2{ font-weight:800; color:#333; font-size:1.4rem; }
        .form-label{ font-weight:700; color:#444; }
        .form-control, .form-select{
            border:2px solid var(--pastel-mint); border-radius:8px;
            transition:border-color .3s, box-shadow .3s;
        }
        .form-control:focus, .form-select:focus{
            border-color:var(--pastel-rose); box-shadow:0 0 5px var(--pastel-rose);
        }
        .btn-pastel{ background-color:var(--pastel-mint); border:none; color:#333; font-weight:800; }
        .btn-pastel:hover{ background-color:var(--pastel-rose); color:#000; }
        .mensaje-centrado{ text-align:center; font-weight:800; font-size:1.05rem; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-9">
                <div class="card p-4 p-md-5">
                    <h2 class="text-center mb-4">Registro de Especie y Raza</h2>

                    <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="mensaje-centrado d-block mb-3"></asp:Label>

                    <div class="row g-4">
                      
                        <div class="col-md-6">
                            <div class="p-3 rounded" style="background:#fff;">
                                <h2 class="mb-3">Agregar nueva especie</h2>

                                <div class="mb-3">
                                    <asp:Label ID="lblEspecie" runat="server" Text="Nombre de especie:" CssClass="form-label" />
                                    <asp:TextBox ID="txtEspecie" runat="server" CssClass="form-control" />
                                </div>

                                <div class="d-grid">
                                    <asp:Button ID="btnAgregarEspecie" runat="server" Text="Guardar especie"
                                                CssClass="btn btn-pastel btn-lg" OnClick="btnAgregarEspecie_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="p-3 rounded" style="background:#fff;">
                                <h2 class="mb-3">Agregar nueva raza</h2>

                                <div class="mb-3">
                                    <asp:Label ID="lblSelEspecie" runat="server" Text="Seleccione especie:" CssClass="form-label" />
                                   
                                    <asp:DropDownList ID="ddlEspecie" runat="server" CssClass="form-select" />
                                </div>

                                <div class="mb-3">
                                    <asp:Label ID="lblRaza" runat="server" Text="Nombre de raza:" CssClass="form-label" />
                                    <asp:TextBox ID="txtRaza" runat="server" CssClass="form-control" />
                                </div>

                                <div class="d-grid">
                                    <asp:Button ID="btnAgregarRaza" runat="server" Text="Guardar raza"
                                                CssClass="btn btn-pastel btn-lg" OnClick="btnAgregarRaza_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <hr class="my-4" />
                    <div class="d-flex gap-2 justify-content-end">
                        <asp:Button ID="btnVolver" runat="server" Text="Volver al menú"
                                    CssClass="btn btn-pastel btn-lg" OnClick="btnVolver_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

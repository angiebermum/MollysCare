<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteMascotas.aspx.cs" Inherits="TuProyecto.ReporteMascotas" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Reporte de Mascotas</title>
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
        h2{ font-weight:800; color:#333; margin-bottom:.5rem; }
        .form-label{ font-weight:700; color:#444; }
        .form-select{ border:2px solid var(--pastel-mint); border-radius:8px; transition:border-color .2s, box-shadow .2s; }
        .form-select:focus{ border-color:var(--pastel-rose); box-shadow:0 0 5px var(--pastel-rose); }
        .btn-pastel{ background-color:var(--pastel-mint); border:none; color:#333; font-weight:800; }
        .btn-pastel:hover{ background-color:var(--pastel-rose); color:#000; }
        .mensaje-centrado{ text-align:center; font-weight:800; font-size:1.05rem; }
        .table thead th{ background: var(--pastel-sky); border-bottom:2px solid rgba(0,0,0,.05); }
        .table-bordered> :not(caption)>*{ border-color: rgba(0,0,0,.08); }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="container py-5">
        <div class="card p-4 p-md-5">
            <h2>Reporte de Mascotas</h2>

            <asp:Label ID="lblResumen" runat="server" CssClass="d-block mb-2 text-muted"></asp:Label>
            <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="mensaje-centrado d-block mb-3"></asp:Label>

            <div class="mb-3">
                <div class="row g-3">
                    <div class="col-sm-6 col-lg-3">
                        <label class="form-label">Especie</label>
                        <asp:DropDownList ID="ddlFiltroEspecie" runat="server" CssClass="form-select"
                                          AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEspecie_SelectedIndexChanged" />
                    </div>
                    <div class="col-sm-6 col-lg-3">
                        <label class="form-label">Raza</label>
                        <asp:DropDownList ID="ddlFiltroRaza" runat="server" CssClass="form-select" />
                    </div>
                    <div class="col-sm-6 col-lg-3">
                        <label class="form-label">Dueño</label>
                        <asp:DropDownList ID="ddlFiltroDueno" runat="server" CssClass="form-select" />
                    </div>

                    <div class="col-12 d-flex gap-2 mt-2">
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-pastel"
                                    OnClick="btnFiltrar_Click" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-pastel"
                                    OnClick="btnLimpiar_Click" CausesValidation="false" />
                        <div class="ms-auto d-flex gap-2">
                            <asp:Button ID="btnExportarPDF" runat="server" Text="Exportar PDF" CssClass="btn btn-pastel"
                                        OnClick="btnExportarPDF_Click" />
                            <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar Excel" CssClass="btn btn-pastel"
                                        OnClick="btnExportarExcel_Click" />
                            <asp:Button ID="btnVolver" runat="server" Text="Volver al menú" CssClass="btn btn-pastel"
                                        OnClick="btnVolver_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="true"
                              CssClass="table table-hover table-bordered align-middle"
                              HeaderStyle-CssClass="table-light"
                              AlternatingRowStyle-BackColor="#FAFAFA"
                              EmptyDataText="No hay resultados con los filtros seleccionados.">
                </asp:GridView>
            </div>
        </div>
    </div>
</form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

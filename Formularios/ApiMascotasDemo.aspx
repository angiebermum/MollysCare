<%@ Page Language="C#" AutoEventWireup="true" %>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
  <meta charset="utf-8" />
  <title>API Mascotas - Demo</title>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
  <style>
    body{ background:linear-gradient(135deg,#ffd6e7,#d8f0ff); }
    .card{ border-radius: 1rem; box-shadow: 0 10px 30px rgba(0,0,0,.08); }
  </style>
</head>
<body class="p-4">
<form id="form1" runat="server">
<div class="container">
  <div class="card p-4">
    <h3 class="mb-3">Consumo de API (Mascotas)</h3>

    <div class="row g-2 align-items-end mb-3">
      <div class="col-sm-3">
        <label class="form-label">ID (opcional)</label>
        <input id="txtId" class="form-control" placeholder="Ej: 3"/>
      </div>
      <div class="col-sm-5">
        <label class="form-label">Nombre (opcional)</label>
        <input id="txtNombre" class="form-control" placeholder="molly"/>
      </div>
      <div class="col-sm-4 d-flex gap-2">
        <button id="btnCargar" type="button" class="btn btn-primary">Cargar</button>
        <a id="lnkJson" class="btn btn-outline-secondary" target="_blank">Ver JSON</a>
      </div>
    </div>

    <div class="table-responsive">
      <table class="table table-striped table-bordered align-middle" id="tbl">
        <thead>
          <tr>
            <th>ID</th><th>Nombre</th><th>Fecha</th><th>Especie</th><th>Raza</th><th>Dueño</th>
          </tr>
        </thead>
        <tbody></tbody>
      </table>
    </div>

    <div id="msg" class="text-muted"></div>
  </div>
</div>
</form>

<script>
const endpoint = '<%= ResolveUrl("~/ObtenerMascotas.ashx") %>';
document.getElementById('lnkJson').href = endpoint;

const btn = document.getElementById('btnCargar');
const tbody = document.querySelector('#tbl tbody');
const msg = document.getElementById('msg');

btn.addEventListener('click', async () => {
  const id = document.getElementById('txtId').value.trim();
  const filtro = document.getElementById('txtNombre').value.trim().toLowerCase();

  const url = id ? `${endpoint}?id=${encodeURIComponent(id)}` : endpoint;
  msg.textContent = 'Cargando...';
  tbody.innerHTML = '';

  try {
    const res = await fetch(url);
    const data = await res.json();
    const list = Array.isArray(data) ? data : [data];

    const filtrada = filtro
      ? list.filter(x => (x.Nombre || '').toLowerCase().includes(filtro))
      : list;

    filtrada.forEach(x => {
      const tr = document.createElement('tr');
      tr.innerHTML =
        `<td>${x.IdMascota ?? ''}</td>
         <td>${x.Nombre ?? ''}</td>
         <td>${x.FechaNacimiento ?? ''}</td>
         <td>${x.Especie ?? ''}</td>
         <td>${x.Raza ?? ''}</td>
         <td>${x.Dueno ?? ''}</td>`;
      tbody.appendChild(tr);
    });

    msg.textContent = filtrada.length ? '' : 'Sin resultados';
  } catch (e) {
    msg.textContent = 'Error cargando datos';
  }
});
</script>
</body>
</html>

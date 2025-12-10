using MollysCare.Data;
using MollysCare.Modelos.Envios;
using MollysCare.Servicios;

namespace MollysCare.Controladores
{
    public class EnviosController
    {
        private readonly EnviosRepository _repo;
        private readonly ProveedorEnviosService _service;

        public EnviosController()
        {
            _repo = new EnviosRepository();
            _service = new ProveedorEnviosService();
        }

        public EnviosViewModel ObtenerPedidosCliente(string usuario)
        {
            var vm = new EnviosViewModel();

            if (string.IsNullOrWhiteSpace(usuario))
            {
                vm.Mensaje = "No se pudo identificar el usuario.";
                vm.EsExitoso = false;
                return vm;
            }

            vm.Pedidos = _repo.ObtenerPedidosPorUsuario(usuario);
            vm.EsExitoso = true;

            if (vm.Pedidos.Count == 0)
            {
                vm.Mensaje = "No hay pedidos registrados para mostrar.";
            }

            return vm;
        }

        public EnviosViewModel ActualizarEstadoDesdeWebService(string usuario, int idPedido)
        {
            var vm = new EnviosViewModel();

            if (string.IsNullOrWhiteSpace(usuario))
            {
                vm.Mensaje = "No se pudo identificar el usuario.";
                vm.EsExitoso = false;
                return vm;
            }

            var pedido = _repo.ObtenerPedidoPorId(idPedido);
            if (pedido == null)
            {
                vm.Mensaje = "No se encontró el pedido indicado.";
                vm.EsExitoso = false;
                return vm;
            }

            var resultado = _service.ConsultarYActualizarEstado(idPedido, pedido.Estado ?? string.Empty);
            _repo.ActualizarEstadoPedido(idPedido, resultado.NuevoEstado);

            vm.Pedidos = _repo.ObtenerPedidosPorUsuario(usuario);
            vm.Mensaje = resultado.Mensaje;
            vm.EsExitoso = true;

            return vm;
        }

        public EnviosViewModel ObtenerPedidosParaGestion()
        {
            var vm = new EnviosViewModel
            {
                Pedidos = _repo.ObtenerTodosLosPedidos(),
                EsExitoso = true
            };

            if (vm.Pedidos.Count == 0)
            {
                vm.Mensaje = "No hay pedidos registrados.";
            }

            return vm;
        }


        public EnviosViewModel ActualizarEstadoDesdeWebServiceAdmin(int idPedido)
        {
            var vm = new EnviosViewModel();

            var pedido = _repo.ObtenerPedidoPorId(idPedido);
            if (pedido == null)
            {
                vm.Mensaje = "No se encontró el pedido indicado.";
                vm.EsExitoso = false;
                return vm;
            }

            var resultado = _service.ConsultarYActualizarEstado(idPedido, pedido.Estado ?? string.Empty);
            _repo.ActualizarEstadoPedido(idPedido, resultado.NuevoEstado);

           
            string infoExtra = _service.ObtenerInfoAdicionalEnvio();

            vm.Pedidos = _repo.ObtenerTodosLosPedidos();

           
            vm.Mensaje = resultado.Mensaje + ". " + infoExtra;
            vm.EsExitoso = true;

            return vm;
        }
    }
}

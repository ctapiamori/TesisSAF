using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SOCAUD.Web.Hubs
{
    #region primera Parte

    [HubName("NotificacionHub")]
    public class NotificacionHub : Hub
    {
        private readonly Notificacion _notificacion;

        public NotificacionHub()
            : this(Notificacion.Instance)
        {

        }

        public NotificacionHub(Notificacion notificacion)
        {
            _notificacion = notificacion;
        }

        public override Task OnConnected()
        {
            var name = Context.QueryString["name"];
            if (!string.IsNullOrEmpty(name))
            {
                _notificacion.AddConnection(name, Context.ConnectionId);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.QueryString["name"];
            _notificacion.RemoveConnection(name, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var name = Context.QueryString["name"];
            _notificacion.Reconnect(name, Context.ConnectionId);
            return base.OnReconnected();
        }

        [HubMethodName("NotificarUsuario")]
        public void NotificarUsuario(string userId, string totalNotificaciones)
        {
            _notificacion.NotificarTotalMensajes(userId, totalNotificaciones);
        }

        public void NotificarPersonal(string userId, string message)
        {
            var connectionId = _notificacion.GetConnections(userId);

            Clients.Client(connectionId).ViewTotalNotificacion(message);
        }
    }

    #endregion

    #region segunda Parte

    public class Notificacion
    {
        private readonly static Lazy<Notificacion> _instance = new Lazy<Notificacion>(() => new Notificacion(GlobalHost.ConnectionManager.GetHubContext<NotificacionHub>().Clients));
        private readonly ConnectionMapping<string> _connections = new ConnectionMapping<string>();


        private Notificacion(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static Notificacion Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public void NotificarTotalMensajes(string userId, string total)
        {
            var connections = _connections.GetConnections(userId);
            foreach (var connection in connections)
            {
                Clients.Client(connection).ViewTotalNotificacion(total);
            }
        }

        public void AddConnection(string name, string connectionId)
        {
            Instance._connections.Add(name, connectionId);
        }

        public void RemoveConnection(string name, string connectionId)
        {
            Instance._connections.Remove(name, connectionId);
        }

        public void Reconnect(string name, string connectionId)
        {
            if (!Instance._connections.GetConnections(name).Contains(connectionId))
            {
                Instance._connections.Add(name, connectionId);
            }
        }

        public string GetConnections(string name)
        {
            return Instance._connections.GetConnections(name).ToString();
        }
    }

    #endregion


    #region ConnectionMapping
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }

    #endregion
}
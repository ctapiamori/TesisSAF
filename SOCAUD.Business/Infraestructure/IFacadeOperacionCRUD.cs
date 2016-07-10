using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SOCAUD.Business.Infraestructure
{
    [ServiceContract]
    public interface IFacadeOperacionCRUD<T>
    {
        /// <summary>
        /// Retorna mensaje despues de Registrar
        /// </summary>
        /// <param name="entidad">Entidad por Registrar</param>
        /// <returns>Mensaje de respuesta (tipo mensaje, mensaje, es correcto o no)</returns>
        [OperationContract]
        T Registrar(T entidad);
        /// <summary>
        /// Retorna mensaje despues de Actualizar
        /// </summary>
        /// <param name="entidad">Entidad por Actualizar</param>
        /// <returns>Mensaje de respuesta (tipo mensaje, mensaje, es correcto o no)</returns>
        [OperationContract]
        T Actualizar(T entidad);
        /// <summary>
        /// Retorna true si elimino y false si ocurrio un error
        /// </summary>
        /// <param name="id">Codigo del registro a eliminar</param>
        /// <returns>Valor boolean</returns>
        [OperationContract]
        bool Eliminar(int id);
        /// <summary>
        /// Retorna entidad encontrada
        /// </summary>
        /// <param name="id">id de la entidad</param>
        /// <returns>Objeto con datos de la entidad</returns>
        [OperationContract]
        T BuscarPorId(int id);
        /// <summary>
        /// Retorna todos los datos encontrados
        /// </summary>
        /// <returns>lista con los datos encontrados</returns>
        [OperationContract]
        IEnumerable<T> ListarTodos();
    }
}

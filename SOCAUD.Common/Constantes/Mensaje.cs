using System;

namespace SOCAUD.Common.Constantes
{
    public static class Mensaje
    {

        public const string MensajeOperacionRealizadaExito = "La Operacion se realizo con exito";
        public const string MensajeErrorNoControlado = "El sistema no responde, comuniquese con su administrador";

        #region MENSAJE DE VALIDACION
        public const string MensajeCampoRequerido = "Debe ingresar un valor campo obligatorio";
        public const string MensajeSoloLetras = "Solo puede ingresar letras y espacios";
        public const string MensajeSoloNumeros = "Solo puede ingresar números";
        public const string MensajeCampoDebeSerCorreo = "Valor debe tener formato de correo electrónico";
        public const string MensajeCampoLongitudIncorrecta = "Longitud excedida";
        #endregion
    }
}

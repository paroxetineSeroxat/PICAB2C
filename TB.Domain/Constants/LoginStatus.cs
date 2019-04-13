using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Domain.Constants
{
    public class LoginStatus
    {
        public const string OK = "OK";
        public const string USER_LOCKED = "USER_LOCKED";
        public const string LOGIN_ERROR = "ERROR AL INICIAR SESIÓN";
        public const string CUSTOMER_DISABLED = "CUSTOMER_DISABLED";
        public const string APPUSER_NOT_FOUND = "EL USUARIO NO EXISTE";
        public const string WRONG_PASSWORD = "CONTRASEÑA INCORRECTA";
        public const string USER_INACTIVE = "USUARIO INACTIVO POR FAVOR COMUNIQUESE CON EL ADMINISTRADOR";
        public const string USER_MOVIL = "USUARIO DE APLICACIÓN MOVIL, SIN PERMISOS ADMINISTRATIVOS";
    }
}

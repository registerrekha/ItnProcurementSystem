using Itn.Utilities.Enums;

namespace Itn.Utilities
{
    public class ServiceResult
    {
        public ServiceInfo AboutService { get; set; }
        public string SecurityResult { get; set; }
        public bool Successful { get; set; }
        public string Result { get; set; }
        public string ResultCode { get; set; }

        public ServiceResult()
        {
            SecurityResult = SecurityAuthResultType.None.ToString();
            Successful = true;
            Result = "";
            ResultCode = "";
        }



        public static ServiceResult Create(ServiceInfo aboutService, SecurityAuthResultType securityResult, string result)
        {
            return new ServiceResult { AboutService = aboutService, SecurityResult = securityResult.ToString(), Result = result };
        }

        public static ServiceResult Create(ServiceInfo aboutService, SecurityAuthResultType securityResult, string result, string resultCode)
        {
            return new ServiceResult { AboutService = aboutService, SecurityResult = securityResult.ToString(), Result = result, ResultCode = resultCode };
        }

        public static ServiceResult Create(ServiceInfo aboutService, SecurityAuthResultType securityResult, bool successful, string result)
        {
            return new ServiceResult { AboutService = aboutService, SecurityResult = securityResult.ToString(), Successful = successful, Result = result };
        }

        public static ServiceResult Create(ServiceInfo aboutService, SecurityAuthResultType securityResult, bool successful, string result, string resultCode)
        {
            return new ServiceResult { AboutService = aboutService, SecurityResult = securityResult.ToString(), Successful = successful, Result = result, ResultCode = resultCode };
        }


        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3} (Security:{4})",
                AboutService, Result, Successful, ResultCode, SecurityResult);
        }

    }
}

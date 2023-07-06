using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scan2Go.Enums;
using Scan2Go.Enums.Properties;
using System.Net;
using Utility.Core;
using Utility.Core.Exceptions;
using Utility.Core.LogClasses;
using Utility.Enum;
using Utility.Extensions;

//jira commit denemesi 3
namespace Scan2Go.Api.BaseClasses;

[Authorize]
[ApiController]
public class BaseController : ControllerBase
{
    protected IConfiguration configuration;

    /// <summary>
    /// Gets the MAC address (<see cref="PhysicalAddress"/>) associated with the specified IP.
    /// </summary>
    /// <param name="ipAddress">The remote IP address.</param>
    /// <returns>The remote machine's MAC address.</returns>
    public static System.Net.NetworkInformation.PhysicalAddress GetMacAddress(IPAddress ipAddress)
    {
        const int MacAddressLength = 6;
        int length = MacAddressLength;
        var macBytes = new byte[MacAddressLength];
        SendARP(BitConverter.ToInt32(ipAddress.GetAddressBytes(), 0), 0, macBytes, ref length);
        return new System.Net.NetworkInformation.PhysicalAddress(macBytes);
    }

    protected IActionResult ReturnOperationResult(OperationResult operationResult, bool IsLogSave = true)
    {
        //LogsManager logManager = new LogsManager(this.CurrentUser);

        if (IsLogSave)
            SaveOperationResult(operationResult);

        if (operationResult.State)
        {
            if (string.IsNullOrEmpty(operationResult.Message))
            {
                if (operationResult.OperationLog != null)
                {
                    if (string.IsNullOrEmpty(operationResult.OperationLog.OperationMessageResourceId))
                    {
                        LanguageEnum languageEnum = LanguageEnum.EN;

                        //if (this.CurrentUser != null)
                        //{
                        //    languageEnum = this.CurrentUser.Language;
                        //}

                        operationResult.Message = EnumMethods.GetResourceString(Convert.ToString(Enum.Parse(typeof(Operations), operationResult.OperationLog.OperationId.ToString())), languageEnum);
                    }
                    else
                    {
                        //operationResult.Message = logManager.FilterLog(operationResult.OperationLog).OperationMessages.First()?.Message;
                    }
                }
            }

            return Ok(operationResult);
        }
        else
        {
            if (operationResult.Exception != null || operationResult.InnerException != null)
            {
                if (operationResult.Exception is AuthorizationException || operationResult.InnerException is AuthorizationException)
                {
                    var message = operationResult.Message ?? operationResult.DetailResults.FirstOrDefault()?.Message;
                    //var returnMessage = message ?? logManager.FilterLog(operationResult.OperationLog).OperationMessages.First()?.Message ?? string.Empty;
                    return StatusCode(HttpStatusCode.Forbidden.AsInt(), new { message = string.Empty });
                }
                else
                {
                    var exception = operationResult.Exception ?? operationResult.InnerException;
                    operationResult.Message = EnumMethods.GetResourceString(nameof(MessageStrings.AnErrorHasAccured), LanguageEnum.EN);

                    return StatusCode(HttpStatusCode.InternalServerError.AsInt(), new { message = exception.Message });
                }
            }
            else
            {
                string returnMessage = string.Empty;

                OperationResult op = operationResult.DetailResults.First(p => p.State == false);

                if (op != null)
                {
                    returnMessage = op.Message ?? string.Empty;
                }
                else
                {
                    //returnMessage = operationResult.Message ?? logManager.FilterLog(operationResult.OperationLog).OperationMessages.First()?.Message ?? string.Empty;
                }

                // return StatusCode(HttpStatusCode.BadRequest.AsInt(),new { message= returnMessage });

                return StatusCode(HttpStatusCode.BadRequest.AsInt(), new { message = returnMessage });
            }
        }
    }

    protected void SaveOperationResult(OperationResult operationResult)
    {
        //LogsManager logManager = new LogsManager(this.CurrentUser);

        OperationLog mainOperationLog = null;
        if (operationResult.OperationLog != null)
        {
            mainOperationLog = operationResult.OperationLog;
        }
        else if (operationResult.DetailResults.Any(t => t.OperationLog != null))
        {
            mainOperationLog = operationResult.DetailResults.First(t => t.OperationLog != null).OperationLog;
        }

        if (mainOperationLog != null)
        {
            //if (mainOperationLog.OperationId == 0)
            //{
            //    return null;
            //}
            var baseIpAdress = Request.HttpContext.Connection.RemoteIpAddress;
            operationResult.OperationLog = mainOperationLog;

            //Currentuser could be null before logging in
            //if (CurrentUser != null)
            //{
            //    operationResult.OperationLog.UserId = CurrentUser.UserId;
            //}

            operationResult.OperationLog.MachineIp = baseIpAdress.ToString();
            IPAddress ipAdress = IPAddress.Parse(baseIpAdress?.ToString() ?? IPAddress.None.ToString());
            string machineName = string.Empty;
            //try
            //{
            //    machineName= System.Net.Dns.GetHostEntry(ipAdress).HostName;
            //}
            //finally
            //{
            //    if(string.IsNullOrEmpty(machineName))
            //    {
            //        machineName = ipAdress.ToString();
            //    }

            //}
            operationResult.OperationLog.MachineName = machineName;
            //operationResult.OperationLog.MachineMACAdress= GetMacAddress(ipAdress).ToString();
            operationResult.OperationLog.DateField = DateTime.Now;

            if (operationResult.Exception == null && !(operationResult.DetailResults.Any(t => t.Exception != null)))
            {
                if (operationResult.State)
                {
                    List<OperationLog> logs = new List<OperationLog>();
                    logs.Add(operationResult.OperationLog);
                    foreach (OperationResult operationDetailResult in operationResult.DetailResults)
                    {
                        if (operationDetailResult.OperationLog != null)
                        {
                            //operationDetailResult.OperationLog.UserId = CurrentUser.UserId;
                            operationDetailResult.OperationLog.MachineIp = baseIpAdress.ToString();

                            //operationDetailResult.OperationLog.MachineName = GetMacAddress(ipAdress).ToString();
                            operationDetailResult.OperationLog.MachineName = machineName;
                            //operationDetailResult.OperationLog.MachineMACAdress = GetMacAddress(ipAdress).ToString();
                            operationDetailResult.OperationLog.DateField = DateTime.Now;

                            logs.Add(operationDetailResult.OperationLog);
                        }
                    }

                    //logManager.AddLogs(logs);
                }
            }
            else
            {
                Exception exception = operationResult.Exception ?? operationResult.DetailResults.First(t => t.Exception != null).Exception;
                OperationException operationException = new OperationException(exception);
                //operationException.LaboratoryId = CurrentUser.LaboratoryId;
                //operationException.UserId = CurrentUser.UserId;
                operationException.MachineIp = operationResult.OperationLog.MachineIp;
                mainOperationLog.OperationException = operationException;
                mainOperationLog.OperationMessageResourceId = nameof(MessageStrings.AnErrorHasAccured);

                //logManager.AddLogs(mainOperationLog);
            }
        }
    }
    //protected override ExceptionResult InternalServerError(Exception exception)
    //{
    //    ExceptionResult e = new ExceptionResult(exception, this);
    //    return e;
    //    // return base.InternalServerError(exception);
    //}

    //protected void AddUserLoginEntryInfo(AuthenticateResponse authenticateResponse)
    //{
    //    if (authenticateResponse != null && authenticateResponse.dbUser != null)
    //    {
    //        var baseIpAdress = Request.HttpContext.Connection.RemoteIpAddress;

    //        LogsManager logManager = new LogsManager(authenticateResponse.dbUser);
    //        logManager.AddUserLoginEntryInfo(baseIpAdress?.ToString());

    //    }
    //}

    //public UsersModel CurrentUser
    //{
    //    get
    //    {
    //        UsersModel usersModel = this.HttpContext.Items["User"] as UsersModel;
    //        if (usersModel != null)
    //        {
    //            string key = "CUrrentLanguage";
    //            usersModel.interfaceLanguage = this.Request.Headers.Any(t => string.Equals(t.Key, key, StringComparison.CurrentCultureIgnoreCase)) &&
    //                                           this.Request.Headers.SingleOrDefault(t => string.Equals(t.Key, key, StringComparison.CurrentCultureIgnoreCase)).Value.Any() ?
    //                ((string[])this.Request.Headers.SingleOrDefault(t => string.Equals(t.Key, key, StringComparison.CurrentCultureIgnoreCase)).Value)[0] : "EN";

    //        }
    //        return usersModel;
    //    }
    //}

    [System.Runtime.InteropServices.DllImport("iphlpapi.dll", ExactSpelling = true)]
    private static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref int PhyAddrLen);
}
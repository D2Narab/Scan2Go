using System;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.DataLayer.ListCaptionBaseDataLater;
using Utility.Bases;
using Utility.Core;

namespace Scan2Go.DataLayer.ListCaptionBaseDataLater;

public class ListCaptionBaseDAO : Scan2GoDataLayerBase
{
    public OperationResult SaveListCaptionBase(ListCaptionBase listCaptionBase)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            new ListCaptionBaseDMLOperations(this).SaveListCaptionBase(listCaptionBase);

            operationResult.State = this.CommitTransaction();
        }
        catch (Exception exception)
        {
            operationResult.State = false;
            operationResult.Exception = exception;
            this.RollbackTransaction();
        }

        return operationResult;
    }
}
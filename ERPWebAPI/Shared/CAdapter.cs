using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CustSalesOrder
{
    public class CAdapterCustOrder
    {
        public static void SetIvGRNMobil(ref SqlDataAdapter da)
        {
            SqlParameter p;
            SqlCommand com;
            com = new SqlCommand();
            com.Parameters.Add("@pono", SqlDbType.NVarChar, 20, "pono");
            com.Parameters.Add("@porelno", SqlDbType.SmallInt, 8, "porelno");
            com.Parameters.Add("@poline", SqlDbType.SmallInt, 8, "poline");
            com.Parameters.Add("@packSz", SqlDbType.Float, 8, "packSz");
            com.Parameters.Add("@icode", SqlDbType.NVarChar, 20, "icode");
            com.Parameters.Add("@idesc", SqlDbType.NVarChar, 200, "idesc");
            com.Parameters.Add("@recvQty", SqlDbType.Float, 8, "recvQty");
            com.Parameters.Add("@purchaseUOM", SqlDbType.NVarChar, 10, "purchaseUOM");
            com.Parameters.Add("@dono", SqlDbType.NVarChar, 20, "dono");
            com.Parameters.Add("@createdOn", SqlDbType.DateTime, 8, "createdOn");
            com.Parameters.Add("@createdBy", SqlDbType.NVarChar, 10, "createdBy");
            com.Parameters.Add("@status", SqlDbType.NVarChar, 10, "status");
            com.CommandText = @"INSERT INTO dbo.IvGRNMobil
			                                  (pono,porelno,icode,idesc,poline,packSz,recvQty,purchaseUOM,dono
			                                  ,createdOn,createdBy,status)
                                 VALUES (@pono,@porelno,@icode,@idesc,@poline,@packSz,@recvQty,@purchaseUOM,@dono
			                                  ,@createdOn,@createdBy,@status)";
            da.InsertCommand = com;

            com = new SqlCommand();
            com.Parameters.Add("@UID", SqlDbType.Int, 8, "UID");
            com.Parameters.Add("@pono", SqlDbType.NVarChar, 20, "pono");
            com.Parameters.Add("@porelno", SqlDbType.SmallInt, 8, "porelno");
            com.Parameters.Add("@poline", SqlDbType.SmallInt, 8, "poline");
            com.Parameters.Add("@packSz", SqlDbType.Float, 8, "packSz");
            com.Parameters.Add("@icode", SqlDbType.NVarChar, 20, "icode");
            com.Parameters.Add("@idesc", SqlDbType.NVarChar, 200, "idesc");
            com.Parameters.Add("@recvQty", SqlDbType.Float, 8, "recvQty");
            com.Parameters.Add("@purchaseUOM", SqlDbType.NVarChar, 10, "purchaseUOM");
            com.Parameters.Add("@dono", SqlDbType.NVarChar, 20, "dono");
            com.Parameters.Add("@createdOn", SqlDbType.DateTime, 8, "createdOn");
            com.Parameters.Add("@createdBy", SqlDbType.NVarChar, 10, "createdBy");
            com.Parameters.Add("@status", SqlDbType.NVarChar, 10, "status");
            p = com.Parameters.Add("@OldUID", SqlDbType.Int, 8, "UID");
            p.SourceVersion = DataRowVersion.Original;
            com.CommandText = @"UPDATE [dbo].[IvGRNMobil]
                                   SET [pono] = @pono
                                      ,[porelno] = @porelno
                                      ,[icode] = @icode
                                      ,[idesc] = @idesc
                                      ,[poline] = @poline
                                      ,[packSz] = @packSz
                                      ,[recvQty] = @recvQty
                                      ,[purchaseUOM] = @purchaseUOM
                                      ,[dono] = @dono
                                      ,[createdBy] = @createdBy      
                                 WHERE UID=OldUID";
            da.UpdateCommand = com;

            com = new SqlCommand();
            com.Parameters.Add("@UID", SqlDbType.Int, 8, "UID");
            com.Parameters.Add("@pono", SqlDbType.NVarChar, 20, "pono");
            com.Parameters.Add("@porelno", SqlDbType.SmallInt, 8, "porelno");
            com.Parameters.Add("@poline", SqlDbType.SmallInt, 8, "poline");
            com.Parameters.Add("@packSz", SqlDbType.Float, 8, "packSz");
            com.Parameters.Add("@icode", SqlDbType.NVarChar, 20, "icode");
            com.Parameters.Add("@idesc", SqlDbType.NVarChar, 200, "idesc");
            com.Parameters.Add("@recvQty", SqlDbType.Float, 8, "recvQty");
            com.Parameters.Add("@purchaseUOM", SqlDbType.NVarChar, 10, "purchaseUOM");
            com.Parameters.Add("@dono", SqlDbType.NVarChar, 20, "dono");
            com.Parameters.Add("@createdOn", SqlDbType.DateTime, 8, "createdOn");
            com.Parameters.Add("@createdBy", SqlDbType.NVarChar, 10, "createdBy");
            com.Parameters.Add("@status", SqlDbType.NVarChar, 10, "status");
            p = com.Parameters.Add("@OldUID", SqlDbType.Int, 8, "UID");
            p.SourceVersion = DataRowVersion.Original;
            com.CommandText = @"DELETE FROM [dbo].[IvGRNMobil] WHERE UID=OldUID";
            da.DeleteCommand = com;
        }

        public static void SetSaSO(ref SqlDataAdapter da)
        {
            SqlParameter p;
            SqlCommand com;

            com = new SqlCommand();
            com.Parameters.Add("@SODate", SqlDbType.DateTime, 8, "SODate");
            com.Parameters.Add("@Status", SqlDbType.NVarChar, 10, "Status");
            com.Parameters.Add("@SONo", SqlDbType.NVarChar, 20, "SONo");
            com.Parameters.Add("@CustCode", SqlDbType.NVarChar, 20, "CustCode");
            com.Parameters.Add("@CustName", SqlDbType.NVarChar, 60, "CustName");
            com.Parameters.Add("@ShipName", SqlDbType.NVarChar, 60, "ShipName");
            com.Parameters.Add("@ShipAddress1", SqlDbType.NVarChar, 40, "ShipAddress1");
            com.Parameters.Add("@ShipAddress2", SqlDbType.NVarChar, 40, "ShipAddress2");
            com.Parameters.Add("@ShipAddress3", SqlDbType.NVarChar, 40, "ShipAddress3");
            com.Parameters.Add("@ShipAddress4", SqlDbType.NVarChar, 40, "ShipAddress4");
            com.Parameters.Add("@City", SqlDbType.NVarChar, 30, "City");
            com.Parameters.Add("@State", SqlDbType.NVarChar, 30, "State");
            com.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 10, "PostalCode");
            com.Parameters.Add("@Country", SqlDbType.NVarChar, 30, "Country");
            com.Parameters.Add("@Tel", SqlDbType.NVarChar, 30, "Tel");
            com.Parameters.Add("@Fax", SqlDbType.NVarChar, 30, "Fax");
            com.Parameters.Add("@Remarks", SqlDbType.NVarChar, 1000, "Remarks");
            com.Parameters.Add("@Taxes", SqlDbType.Float, 8, "Taxes");
            com.Parameters.Add("@TotAmnt", SqlDbType.Float, 8, "TotAmnt");
            com.Parameters.Add("@agent", SqlDbType.NVarChar, 10, "agent");
            com.Parameters.Add("@custpono", SqlDbType.NVarChar, 30, "custpono");
            com.Parameters.Add("@refno", SqlDbType.NVarChar, 30, "refno");
            com.Parameters.Add("@devimei", SqlDbType.NVarChar, 30, "devimei");

            p = com.Parameters.Add("@OldSONo", SqlDbType.NVarChar, 20, "SONo");
            p.SourceVersion = DataRowVersion.Original;
            p = com.Parameters.Add("@OldCustCode", SqlDbType.NVarChar, 20, "CustCode");
            p.SourceVersion = DataRowVersion.Original;

            com.CommandText = "Update SaCustSO Set SODate = @SODate, Status = @Status, SONo = @SONo," +
                               "CustCode = @CustCode, CustName = @CustName, ShipName=@ShipName, ShipAddress1 = @ShipAddress1, ShipAddress2 = @ShipAddress2, " +
                               "ShipAddress3 = @ShipAddress3, ShipAddress4 = @ShipAddress4, City = @City, State = @State, PostalCode = @PostalCode, " +
                               "Country = @Country, Tel = @Tel, Fax = @Fax, " +
                               "Remarks = @Remarks,Taxes=@Taxes,TotAmnt=@TotAmnt,agent=@agent,custpono=@custpono,refno=@refno,devimei=@devimei " +
                               "WHERE (SONo = @OldSONo) and (CustCode=@CustCode) ";
            da.UpdateCommand = com;


            com = new SqlCommand();
            com.Parameters.Add("@SODate", SqlDbType.DateTime, 8, "SODate");
            com.Parameters.Add("@Status", SqlDbType.NVarChar, 10, "Status");
            com.Parameters.Add("@SONo", SqlDbType.NVarChar, 20, "SONo");
            com.Parameters.Add("@CustCode", SqlDbType.NVarChar, 20, "CustCode");
            com.Parameters.Add("@CustName", SqlDbType.NVarChar, 60, "CustName");
            com.Parameters.Add("@ShipName", SqlDbType.NVarChar, 60, "ShipName");
            com.Parameters.Add("@ShipAddress1", SqlDbType.NVarChar, 40, "ShipAddress1");
            com.Parameters.Add("@ShipAddress2", SqlDbType.NVarChar, 40, "ShipAddress2");
            com.Parameters.Add("@ShipAddress3", SqlDbType.NVarChar, 40, "ShipAddress3");
            com.Parameters.Add("@ShipAddress4", SqlDbType.NVarChar, 40, "ShipAddress4");
            com.Parameters.Add("@City", SqlDbType.NVarChar, 30, "City");
            com.Parameters.Add("@State", SqlDbType.NVarChar, 30, "State");
            com.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 10, "PostalCode");
            com.Parameters.Add("@Country", SqlDbType.NVarChar, 30, "Country");
            com.Parameters.Add("@Tel", SqlDbType.NVarChar, 30, "Tel");
            com.Parameters.Add("@Fax", SqlDbType.NVarChar, 30, "Fax");
            com.Parameters.Add("@Remarks", SqlDbType.NVarChar, 1000, "Remarks");
            com.Parameters.Add("@Taxes", SqlDbType.Float, 8, "Taxes");
            com.Parameters.Add("@TotAmnt", SqlDbType.Float, 8, "TotAmnt");
            com.Parameters.Add("@agent", SqlDbType.NVarChar, 10, "agent");
            com.Parameters.Add("@custpono", SqlDbType.NVarChar, 30, "custpono");
            com.Parameters.Add("@refno", SqlDbType.NVarChar, 30, "refno");
            com.Parameters.Add("@devimei", SqlDbType.NVarChar, 30, "devimei");

            com.CommandText = "Insert into SaCustSO (SODate, Status, SONo," +
                              "CustCode, CustName, ShipName, ShipAddress1, " +
                               "ShipAddress2, ShipAddress3, ShipAddress4, City, State, PostalCode, Country, Tel, Fax, " +
                               "Remarks,Taxes,TotAmnt,agent,custpono,refno,devimei)" +
                               " Values " +
                               "(@SODate, @Status, @SONo, " +
                               "@CustCode, @CustName, @ShipName, @ShipAddress1, " +
                               "@ShipAddress2, @ShipAddress3, @ShipAddress4, @City, @State, @PostalCode, @Country, @Tel, @Fax," +
                               "@Remarks,@Taxes,@TotAmnt,@agent,@custpono,@refno,@devimei)";
            da.InsertCommand = com;

            com = new SqlCommand();
            com.Parameters.Add("@SONo", SqlDbType.NVarChar, 20, "SONo");

            p = com.Parameters.Add("@OldSONo", SqlDbType.NVarChar, 20, "SONo");
            p.SourceVersion = DataRowVersion.Original;

            com.CommandText = "Delete from SaCustSO where SONo = @OldSONo";
            da.DeleteCommand = com;

        }

        public static void SetSaSODetail(ref SqlDataAdapter da)
        {
            SqlParameter p;
            SqlCommand com;

            com = new SqlCommand();
            com.Parameters.Add("@SONo", SqlDbType.NVarChar, 20, "SONo");
            com.Parameters.Add("@Line", SqlDbType.SmallInt, 2, "Line");
            com.Parameters.Add("@ICode", SqlDbType.NVarChar, 20, "ICode");
            com.Parameters.Add("@IDesc", SqlDbType.NVarChar, 1000, "IDesc");
            com.Parameters.Add("@OrderQty", SqlDbType.Float, 8, "OrderQty");
            com.Parameters.Add("@UnitPrice", SqlDbType.Float, 8, "UnitPrice");
            com.Parameters.Add("@SellingUOM", SqlDbType.NVarChar, 5, "SellingUOM");
            com.Parameters.Add("@StdCustPSize", SqlDbType.Float, 5, "StdCustPSize");
            com.Parameters.Add("@DelDate", SqlDbType.DateTime, 8, "DelDate");
            com.Parameters.Add("@NetAmount", SqlDbType.Decimal, 18, "NetAmount");
            com.Parameters.Add("@TaxGroup", SqlDbType.NVarChar, 10, "TaxGroup"); 
            com.Parameters.Add("@TaxAmount", SqlDbType.Decimal, 18, "TaxAmount"); 
            com.Parameters.Add("@IsInclusive", SqlDbType.Bit, 1, "IsInclusive"); 
            com.Parameters.Add("@note", SqlDbType.NVarChar, 1000, "note");
            com.Parameters.Add("@refno", SqlDbType.NVarChar, 30, "refno");
            com.Parameters.Add("@devimei", SqlDbType.NVarChar, 30, "devimei");

            p = com.Parameters.Add("@OldSONo", SqlDbType.NVarChar, 20, "SONo");
            p.SourceVersion = DataRowVersion.Original;
            p = com.Parameters.Add("@OldLine", SqlDbType.SmallInt, 2, "Line");
            p.SourceVersion = DataRowVersion.Original;


            com.CommandText = "Update SaCustSODetail Set SONo = @SONo,Line = @Line,ICode = @ICode, " +
                              "IDesc = @IDesc, OrderQty = @OrderQty, UnitPrice = @UnitPrice, " +
                              "SellingUOM = @SellingUOM, StdCustPSize = @StdCustPSize,DelDate=@DelDate" +
                              "NetAmount = @NetAmount,TaxGroup=@TaxGroup,TaxAmount=@TaxAmount, IsInclusive=@IsInclusive,note=@note,refno=@refno,devimei=@devimei " +
                              "WHERE (SONo = @SONo)  and (Line = @OldLine)";
            da.UpdateCommand = com;


            com = new SqlCommand();
            com.Parameters.Add("@SONo", SqlDbType.NVarChar, 20, "SONo");
            com.Parameters.Add("@Line", SqlDbType.SmallInt, 2, "Line");
            com.Parameters.Add("@ICode", SqlDbType.NVarChar, 20, "ICode");
            com.Parameters.Add("@IDesc", SqlDbType.NVarChar, 1000, "IDesc");
            com.Parameters.Add("@OrderQty", SqlDbType.Float, 8, "OrderQty");
            com.Parameters.Add("@UnitPrice", SqlDbType.Float, 8, "UnitPrice");
            com.Parameters.Add("@SellingUOM", SqlDbType.NVarChar, 5, "SellingUOM");
            com.Parameters.Add("@StdCustPSize", SqlDbType.Float, 5, "StdCustPSize");
            com.Parameters.Add("@DelDate", SqlDbType.DateTime, 8, "DelDate");
            com.Parameters.Add("@NetAmount", SqlDbType.Decimal, 18, "NetAmount");
            com.Parameters.Add("@TaxGroup", SqlDbType.NVarChar, 10, "TaxGroup"); 
            com.Parameters.Add("@TaxAmount", SqlDbType.Decimal, 18, "TaxAmount");
            com.Parameters.Add("@IsInclusive", SqlDbType.Bit, 1, "IsInclusive");
            com.Parameters.Add("@note", SqlDbType.NVarChar, 1000, "note");
            com.Parameters.Add("@refno", SqlDbType.NVarChar, 30, "refno");
            com.Parameters.Add("@devimei", SqlDbType.NVarChar, 30, "devimei");


            com.CommandText = "Insert into SaCustSODetail (SONo,Line,ICode,IDesc,OrderQty,UnitPrice," +
                              "SellingUOM,StdCustPSize,DelDate,NetAmount,TaxGroup," +
                              "TaxAmount,IsInclusive,note,refno,devimei) Values " +
                              "(@SONo,@Line,@ICode,@IDesc,@OrderQty,@UnitPrice," +
                              "@SellingUOM,@StdCustPSize,@DelDate,@NetAmount,@TaxGroup," +
                              "@TaxAmount,@IsInclusive,@note,@refno,@devimei)";
            da.InsertCommand = com;


            com = new SqlCommand();
            com.Parameters.Add("@SONo", SqlDbType.NVarChar, 20, "SONo");
            com.Parameters.Add("@Line", SqlDbType.SmallInt, 2, "Line");

            p = com.Parameters.Add("@OldSONo", SqlDbType.NVarChar, 20, "SONo");
            p.SourceVersion = DataRowVersion.Original;
            p = com.Parameters.Add("@OldLine", SqlDbType.SmallInt, 2, "Line");
            p.SourceVersion = DataRowVersion.Original;

            com.CommandText = "Delete from SaCustSODetail where SONo = @OldSONo and Line = @OldLine ";
            da.DeleteCommand = com;

        }
    }
}
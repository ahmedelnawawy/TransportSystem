using ConnectDataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AdsSysWeb.Models
{
    //Use This Interface When You Calculate Quantity in ExcutionOrder
    public interface LmsaCalcQuntiy
    {
        string CalcQuntity(IEnumerable<LmsaEntitiesDB.Store> lst);

      //  LmsaEntitiesDB.Branch Card_CalcQuntity(IEnumerable<LmsaEntitiesDB.Branch> lst);
        string Balance_CalcQuntity(IEnumerable<LmsaEntitiesDB.Store> lst);
        LmsaEntitiesDB.InventoryReports.CardOfCategory Card_CalcQuntity(IEnumerable<LmsaEntitiesDB.InventoryReports.CardOfCategory> lst);

    }

    public class LmsaEntitiesDB
    {
        public class InventoryReports : LmsaCalcQuntiy
        {

            public class ImportsCategory
            {
                private ConnectFunction fun = new ConnectFunction();
                public Int64 subid { set; get; }
                public Double? quantity { set; get; }
                public DateTime date { set; get; }
                public string ProductID { set; get; }
                public string RType { set; get; }
                public string AccountName { set; get; }
                public List<ImportsCategory> imports(string productid, DateTime datefrom, DateTime dateto)
                {
                    string DateF = ConvertDate(datefrom.ToShortDateString());
                    string DateTo = ConvertDate(dateto.ToShortDateString());
                    string wa = "Ê«—œ";
                    string selimpot = @"select ReceivedOrder.subid,SubAccount.name as AccountName,
     ReceivedOrder.date,ReceivedOrderDetail.quantity,
                        
                        ReceivedOrderDetail.ProductID,ReceivedOrder.ReceivedType from ReceivedOrder 
inner join  ReceivedOrderDetail on ReceivedOrder.ID=ReceivedOrderDetail.ReceivedOrderID inner  join
SubAccount on ReceivedOrder.SubID=SubAccount.ID 
where ReceivedOrder.ReceivedType like N'%" + wa + "%' and ReceivedOrderDetail.ProductID=N'" + productid +
"' and ReceivedOrder.date>= " + DateF + " and ReceivedOrder.date<=" + DateTo + "";
                    DataTable dt = fun.GetData(selimpot);
                    List<ImportsCategory> lst = new List<ImportsCategory>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["ReceivedType"].ToString() == "Ê«—œ")
                        {
                            lst.Add(new ImportsCategory
                            {
                                date = DateTime.Parse(row["date"].ToString()),
                                ProductID = row["ProductID"].ToString(),
                                quantity = double.Parse(row["quantity"].ToString()),
                                RType = row["ReceivedType"].ToString(),
                                subid = Int64.Parse(row["subid"].ToString()),
                                AccountName = row["AccountName"].ToString()
                            });
                        }
                        else
                        {
                            lst.Add(new ImportsCategory
                            {
                                date = DateTime.Parse(row["date"].ToString()),
                                ProductID = row["ProductID"].ToString(),
                                quantity = -double.Parse(row["quantity"].ToString()),
                                RType = row["ReceivedType"].ToString(),
                                subid = Int64.Parse(row["subid"].ToString())
                               ,
                                AccountName = row["AccountName"].ToString()
                            });
                        }

                    }


                    string Transq = @"SELECT[Transfer].[ID] as TransferID,
    [SubStore].name as AccountName,
      [Transfer].[FromStorID],
      [Transfer].[ToStorID],
      [Transfer].[Date],
	  TransferDetail.ProductID,
	  TransferDetail.[TransferedQty]
  FROM[EPE].[dbo].[Transfer]
                inner join TransferDetail
               on TransferDetail.[TransferID]=[Transfer].[ID]
                inner join SubStore on SubStore.ID=[Transfer].FromStorID
where [Transfer].[Date]<=" + DateTo + "  and TransferDetail.ProductID=N'" +
                                                      productid + "'and [Transfer].[Date]>= " + DateF + " and [Transfer].[Date]<=" + DateTo + "";
                    DataTable dt1 = fun.GetData(Transq);
                    foreach (DataRow row in dt1.Rows)
                    {

                        lst.Add(new ImportsCategory
                        {
                            date = DateTime.Parse(row["date"].ToString()),
                            ProductID = row["ProductID"].ToString(),
                            quantity = +float.Parse(row["TransferedQty"].ToString()),
                            RType = "Ê«—œ „ÕÊ·",
                            subid = int.Parse(row["ToStorID"].ToString()),
                            AccountName = "„Œ“‰ " + row["AccountName"].ToString()

                        });

                    }

                    return lst;// ConvertDT<ImportsCategory>(dt);
                }
                //public string Import_CalcQuntity(List<ImportsCategory> lst)
                //{
                //    double?  TQuntoty = 0;
                //    foreach(var nn in lst)
                //    {
                //        TQuntoty += nn.quantity;
                //    }
                //    return TQuntoty.Value.ToString("#.0");
                //}




            }

            public class ExportsCategory
            {
                private ConnectFunction fun = new ConnectFunction();
                public Int64 CustID { set; get; }
                public Double? QuantityBefror { set; get; }
                public DateTime Date { set; get; }
                public string ProductID { set; get; }
                public string RType { set; get; }
                public string AccountName { set; get; }

                public List<ExportsCategory> Exports(string productid, DateTime datefrom, DateTime dateto)
                {
                    string DateF = ConvertDate(datefrom.ToShortDateString());
                    string DateTo = ConvertDate(dateto.ToShortDateString());
                    string wa = "’«œ—";
                    string e7teag = "«Õ Ì«Ã";

                    string selimpot = @"select Receipt.CustID,SubAccount.name as AccountName,Receipt.date,
ReceiptDetials.QuantityBefror,ReceiptDetials.ProductID,Receipt.rtype from Receipt inner join  
ReceiptDetials on Receipt.ID=ReceiptDetials.RID
inner  join SubAccount on Receipt.CustID=SubAccount.ID
where Receipt.rtype like N'%" + wa +
"%' and ReceiptDetials.ProductID=N'" + productid + "' and Receipt.date>= " + DateF +
" and Receipt.date<=" + DateTo + "";
                    DataTable dt = fun.GetData(selimpot);
                    List<ExportsCategory> lst = new List<ExportsCategory>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["rtype"].ToString() == "’«œ—" || row["rtype"].ToString() == e7teag)
                        {
                            lst.Add(new ExportsCategory
                            {
                                Date = DateTime.Parse(row["date"].ToString()),
                                ProductID = row["ProductID"].ToString(),
                                QuantityBefror = double.Parse(row["QuantityBefror"].ToString()),
                                RType = row["rtype"].ToString(),
                                CustID = Int64.Parse(row["CustID"].ToString())
                                ,
                                AccountName = row["AccountName"].ToString()
                            });
                        }
                        else
                        {
                            lst.Add(new ExportsCategory
                            {
                                Date = DateTime.Parse(row["date"].ToString()),
                                ProductID = row["ProductID"].ToString(),
                                QuantityBefror = -double.Parse(row["QuantityBefror"].ToString()),
                                RType = row["rtype"].ToString(),
                                CustID = Int64.Parse(row["CustID"].ToString())
                                                                ,
                                AccountName = row["AccountName"].ToString()

                            });
                        }

                    }
                    string Transq = @"SELECT[Transfer].[ID] as TransferID,
    [SubStore].name as AccountName,
      [Transfer].[FromStorID],
      [Transfer].[ToStorID],
      [Transfer].[Date],
	  TransferDetail.ProductID,
	  TransferDetail.[TransferedQty]
  FROM [Transfer]
                inner join TransferDetail
               on TransferDetail.[TransferID]=[Transfer].[ID]
                inner join SubStore on  SubStore.ID=[Transfer].ToStorID
where [Transfer].[Date]<=" + DateTo + "  and TransferDetail.ProductID=N'" +
                                                productid + "'and [Transfer].[Date]>= " + DateF + " and [Transfer].[Date]<=" + DateTo + "";
                    DataTable dt1 = fun.GetData(Transq);
                    foreach (DataRow row in dt1.Rows)
                    {

                        lst.Add(new ExportsCategory
                        {
                            Date = DateTime.Parse(row["date"].ToString()),
                            ProductID = row["ProductID"].ToString(),
                            QuantityBefror = +float.Parse(row["TransferedQty"].ToString()),
                            RType = "’«œ— „ÕÊ·",
                            CustID = int.Parse(row["FromStorID"].ToString()),
                            AccountName = "„Œ“‰ " + row["AccountName"].ToString()

                        });

                    }
                    return lst;// ConvertDT<ExportsCategory>(dt);
                }
                //public string Export_CalcQuntity(List<ExportsCategory> lst)
                //{
                //    double? TQuntoty = 0;
                //    foreach (var nn in lst)
                //    {
                //        TQuntoty += nn.QuantityBefror;
                //    }
                //    return TQuntoty.Value.ToString("#.0");
                //}
            }

            public class CardOfCategory
            {
                private ConnectFunction fun = new ConnectFunction();
                public Int64 subid { set; get; }
                // public Double? opbalance { set; get; } = 0;
                public Double? exquantity { set; get; } = 0;
                public Double? imquantity { set; get; } = 0;
                public Double? prbalance { set; get; } = 0;
                public Double? afbalance { set; get; } = 0;
                public DateTime date { set; get; }
                public string ProductID { set; get; }
                public string RType { set; get; }
                public string AccountName { set; get; }

                public List<CardOfCategory> imports(string productid, DateTime datefrom, DateTime dateto)
                {
                    List<CardOfCategory> lst = new List<CardOfCategory>();
                    string op = "—’Ìœ «›  «ÕÏ";
                    string DateF = ConvertDate(datefrom.ToShortDateString());
                    string DateTo = ConvertDate(dateto.ToShortDateString());
                    string wa = "1"; //"Ê«—œ";
                    string wa1 ="0";// "’«œ—";
                    string e7teag = "«Õ Ì«Ã";
                    double oprsed = 0;

                    ///////////////////////////////////////////////////////////////////////////
                    string selimpotpr = @"select PurchaseInvoice.SubAccountId,PurchaseInvoice.InvoiceDate,PurchaseInvoiceDetail.Qty,
PurchaseInvoiceDetail.ProductID,PurchaseInvoice.PurchaseType from PurchaseInvoice inner join  PurchaseInvoiceDetail 
on PurchaseInvoice.Id=PurchaseInvoiceDetail.PurchaseInvoiceID where  PurchaseInvoiceDetail.ProductID=N'" + productid +
"' and PurchaseInvoice.InvoiceDate< " + DateF + " ";
                    DataTable dtpr = fun.GetData(selimpotpr);
                    ///////////////////

                    foreach (DataRow row in dtpr.Rows)
                    {
                        if (row["PurchaseType"].ToString() == wa)
                        {
                            oprsed += (double)row["Qty"];
                        }
                        else
                        {
                            oprsed -= (double)row["Qty"];
                        }

                    }

                    string selimpot1pr = @"select SalesInvoice.CarId,SalesInvoice.InvoiceDate,SalesInvoiceDetail.Qty,
SalesInvoiceDetail.ProductID,SalesInvoice.PurchaseType from SalesInvoice inner join  SalesInvoiceDetail on SalesInvoice.Id=SalesInvoiceDetail.PurchaseInvoiceID where  
SalesInvoiceDetail.ProductID=N'" + productid + "' and SalesInvoice.InvoiceDate< " + DateF + " ";
                    DataTable dt1pr = fun.GetData(selimpot1pr);
                    foreach (DataRow row in dt1pr.Rows)
                    {
                        if (row["PurchaseType"].ToString() == wa1 )
                        {

                            oprsed -= (double)row["Qty"];
                        }
                        else
                        {
                            oprsed += (double)row["Qty"];
                        }
                    }
                    ///////////////////////////////////////////////////////////////////////////////////
                    string open = "select * from Product where ID=N'" + productid + "'";
                    DataTable dto = fun.GetData(open);
                    foreach (DataRow row in dto.Rows)
                    {
                        lst.Add(new Models.LmsaEntitiesDB.InventoryReports.CardOfCategory
                        {
                            prbalance = (double)row["BalanceQ"] + oprsed,
                            ProductID = row["ID"].ToString(),
                            AccountName = "—’Ìœ «›  «ÕÏ"
                                ,
                            RType = op,
                            date = datefrom.AddDays(-1)
                        });
                    }

                    string selimpot = @"select PurchaseInvoice.SubAccountId,SubAccount.name as AccountName,PurchaseInvoice.InvoiceDate,
PurchaseInvoiceDetail.Qty,PurchaseInvoiceDetail.ProductID,PurchaseInvoice.PurchaseType from PurchaseInvoice  inner join
PurchaseInvoiceDetail on PurchaseInvoice.Id=PurchaseInvoiceDetail.PurchaseInvoiceID  inner  join SubAccount on PurchaseInvoice.SubAccountId=SubAccount.ID where PurchaseInvoice.PurchaseType like N'" +
wa + "' and PurchaseInvoiceDetail.ProductID=N'" + productid + "' and PurchaseInvoice.InvoiceDate>= " +
DateF + " and PurchaseInvoice.InvoiceDate<=" + DateTo + "";
                    DataTable dt = fun.GetData(selimpot);
                    ///////////////////

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["PurchaseType"].ToString() == wa)
                        {
                            lst.Add(new Models.LmsaEntitiesDB.InventoryReports.CardOfCategory
                            {
                                subid = (long)row["SubAccountId"],
                                AccountName = row["AccountName"].ToString()
                                ,
                                imquantity = (double)row["Qty"],
                                ProductID = row["ProductID"].ToString(),
                                RType = row["PurchaseType"].ToString(),
                                date = DateTime.Parse(row["InvoiceDate"].ToString(), CultureInfo.CreateSpecificCulture("ar-EG"))
                            });
                        }
                        else
                        {
                            try
                            {
                                lst.Add(new Models.LmsaEntitiesDB.InventoryReports.CardOfCategory
                                {
                                    subid = (long)row["SubAccountId"],
                                    AccountName = row["AccountName"].ToString()
                                    ,
                                    exquantity = (double)row["Qty"],
                                    ProductID = row["ProductID"].ToString(),
                                    RType = row["PurchaseType"].ToString(),
                                    date = DateTime.Parse(row["InvoiceDate"].ToString(), CultureInfo.CreateSpecificCulture("ar-EG"))
                                });
                            }
                            catch (Exception ex) {
                            }
                        }
                    }

                    string selimpot1 = @"select SalesInvoice.CarId,Cars.CarNo as AccountName,SalesInvoice.InvoiceDate,SalesInvoiceDetail.Qty,
SalesInvoiceDetail.ProductID,SalesInvoice.PurchaseType  from SalesInvoice inner join  SalesInvoiceDetail on SalesInvoice.Id=SalesInvoiceDetail.PurchaseInvoiceID
 inner  join Cars on SalesInvoice.CarId=Cars.ID where 
SalesInvoice.PurchaseType like N'" + wa1 + "' and SalesInvoiceDetail.ProductID=N'" + productid + "' and SalesInvoice.InvoiceDate>= " + DateF +
" and SalesInvoice.InvoiceDate<=" + DateTo + "";
                    DataTable dt1 = fun.GetData(selimpot1);
                    foreach (DataRow row in dt1.Rows)
                    {
                        if (row["PurchaseType"].ToString() == wa1 )
                        {
                            lst.Add(new Models.LmsaEntitiesDB.InventoryReports.CardOfCategory
                            {
                                subid = (long)row["CarId"],
                                AccountName = row["AccountName"].ToString()
                                ,
                                exquantity = (double)row["Qty"],
                                ProductID = row["ProductID"].ToString(),
                                RType = row["PurchaseType"].ToString(),
                                date = DateTime.Parse(row["InvoiceDate"].ToString(),CultureInfo.CreateSpecificCulture("ar-EG"))
                            });
                        }
                        else
                        {
                            lst.Add(new Models.LmsaEntitiesDB.InventoryReports.CardOfCategory
                            {
                                subid = (long)row["CarId"],
                                imquantity = (double)row["Qty"],
                                AccountName = row["AccountName"].ToString()
                                ,
                                ProductID = row["ProductID"].ToString(),
                                RType = row["PurchaseType"].ToString(),
                                date = DateTime.Parse(row["InvoiceDate"].ToString(), CultureInfo.CreateSpecificCulture("ar-EG"))
                            });
                        }
                    }
                    
                    //////////////sort list by date then imported quantity desc
                    //lst.Sort(delegate (CardOfCategory l1, CardOfCategory l2)
                    //{
                    //    /////sort with date asc
                    //    int a = l2.date.ToShortDateString().CompareTo(l1.date.ToShortDateString());
                    //    if (a == 0)
                    //        //sort with imported quantity by desc when result of compare is 0
                    //        a = l2.imquantity.Value.CompareTo(l1.imquantity.Value);
                    //    return a;
                    //});
                    lst.OrderBy(c => c.date);
                    // lst.OrderBy(n => n.date);
                    //lst.OrderByDescending(n => n.imquantity);
                    ////////////////////////////////////////end sort
                    ////////////////////////////////////////Calc Balance
                    double? nextb = 0;
                    foreach (var rr in lst)
                    {
                        if (rr.RType == op)
                        {
                            rr.afbalance = rr.prbalance;
                        }
                        else
                        {
                            rr.prbalance = nextb;
                            rr.afbalance = rr.prbalance + rr.imquantity - rr.exquantity;
                        }
                        nextb = rr.afbalance;
                    }
                    ////////////////////////////////////////end calc
                    return lst;// ConvertDT<CardOfCategory>(dt);
                }





            }
            public class BalancesOfProducts
            {
                private ConnectFunction fun = new ConnectFunction();
                //public Int64 subid { set; get; }
                // public Double? opbalance { set; get; } = 0;
                public Double? balance { set; get; } = 0;
                //public Double? imquantity { set; get; } = 0;
                //public Double? prbalance { set; get; } = 0;
                //public Double? afbalance { set; get; } = 0;
                public DateTime date { set; get; }
                public string ProductID { set; get; }
                public string ProductName { set; get; }
                public string RType { set; get; }
                public List<BalancesOfProducts> balances(DateTime dateto)
                {
                    List<BalancesOfProducts> lst = new List<BalancesOfProducts>();
                    string op = "—’Ìœ «›  «ÕÏ";
                    //string DateF = ConvertDate(datefrom.ToShortDateString());
                    string DateTo = ConvertDate(dateto.ToShortDateString());
                    string wa = "Ê«—œ";
                    string wa1 = "’«œ—";
                    string e7teag = "«Õ Ì«Ã";
                    double oprsed = 0;
                    double wared = 0;
                    double sader = 0;
                    //////////////////////////////////////////////////////////////////////////
                    string selpr = "select * from product";
                    DataTable dpro = fun.GetData(selpr);
                    foreach (DataRow prow in dpro.Rows)
                    {
                        oprsed = 0;
                        wared = 0;
                        sader = 0;
                        ///////////////////////////////////////////////////////////////////////////
                        string selimpotpr = "select ReceivedOrder.subid,ReceivedOrder.date,ReceivedOrderDetail.quantity,ReceivedOrderDetail.ProductID,ReceivedOrder.ReceivedType from ReceivedOrder inner join  ReceivedOrderDetail on ReceivedOrder.ID=ReceivedOrderDetail.ReceivedOrderID where  ReceivedOrderDetail.ProductID=N'" +
                            prow["ID"].ToString() + "' and ReceivedOrder.date<= " + DateTo + " ";
                        DataTable dtpr = fun.GetData(selimpotpr);
                        ///////////////////

                        foreach (DataRow row in dtpr.Rows)
                        {
                            if (row["ReceivedType"].ToString() == wa)
                            {
                                wared += (double)row["quantity"];
                            }
                            else
                            {
                                sader += (double)row["quantity"];
                            }

                        }

                        string selimpot1pr = "select Receipt.CustID,Receipt.date,ReceiptDetials.QuantityBefror,ReceiptDetials.ProductID,Receipt.rtype from Receipt inner join  ReceiptDetials on Receipt.ID=ReceiptDetials.RID where  ReceiptDetials.ProductID=N'" +
                            prow["ID"].ToString() + "' and Receipt.date<= " + DateTo + " ";
                        DataTable dt1pr = fun.GetData(selimpot1pr);
                        foreach (DataRow row in dt1pr.Rows)
                        {
                            if (row["RType"].ToString() == wa1 || row["RType"].ToString() == e7teag)
                            {

                                sader += (double)row["QuantityBefror"];
                            }
                            else
                            {
                                wared += (double)row["QuantityBefror"];
                            }
                        }
                        ///////////////////////////////////////////////////////////////////////////////////

                        oprsed += (double)prow["BalanceQ"];
                        //lst.Add(new Models.EntitiesDB.InventoryReports.CardOfCategory { prbalance = (double)row["BalanceQ"] + oprsed, ProductID = int.Parse(row["ID"].ToString()), RType = op, date = datefrom.AddDays(-1) });


                        lst.Add(new BalancesOfProducts { ProductID = prow["ID"].ToString(), ProductName = prow["name"].ToString(), balance = oprsed + wared - sader });
                    }

                    return lst;// ConvertDT<CardOfCategory>(dt);
                }
                public List<BalancesOfProducts> balances(DateTime dateto, int StorID)
                {
                    List<BalancesOfProducts> lst = new List<BalancesOfProducts>();
                    string op = "—’Ìœ «›  «ÕÏ";
                    //string DateF = ConvertDate(datefrom.ToShortDateString());
                    string DateTo = ConvertDate(dateto.ToShortDateString());
                    string wa = "Ê«—œ";
                    string wa1 = "’«œ—";
                    string e7teag = "«Õ Ì«Ã";

                    double oprsed = 0;
                    double wared = 0;
                    double sader = 0;
                    //////////////////////////////////////////////////////////////////////////
                    string selpr = "select * from product";
                    DataTable dpro = fun.GetData(selpr);
                    foreach (DataRow prow in dpro.Rows)
                    {
                        oprsed = 0;
                        wared = 0;
                        sader = 0;
                        ///////////////////////////////////////////////////////////////////////////
                        if (StorID == 1)
                        {
                            string selimpotpr = @"select ReceivedOrder.subid,ReceivedOrder.date,
ReceivedOrderDetail.quantity,ReceivedOrderDetail.ProductID,ReceivedOrder.ReceivedType 
from ReceivedOrder inner join  ReceivedOrderDetail on ReceivedOrder.ID=ReceivedOrderDetail.
ReceivedOrderID where  ReceivedOrderDetail.ProductID=N'" +
                                prow["ID"].ToString() + "' and ReceivedOrder.date<= " + DateTo + " ";
                            DataTable dtpr = fun.GetData(selimpotpr);
                            ///////////////////

                            foreach (DataRow row in dtpr.Rows)
                            {
                                if (row["ReceivedType"].ToString() == wa)
                                {
                                    wared += (double)row["quantity"];
                                }
                                else
                                {
                                    sader += (double)row["quantity"];
                                }

                            }
                        }
                        string selimpot1pr = @"select Receipt.CustID,Receipt.date,
ReceiptDetials.QuantityBefror,ReceiptDetials.ProductID,Receipt.rtype from Receipt inner join
ReceiptDetials on Receipt.ID=ReceiptDetials.RID where  ReceiptDetials.ProductID=N'" +
                            prow["ID"].ToString() + "' and Receipt.date<= " + DateTo + " and ReceiptDetials.StorID=" + StorID + "";
                        DataTable dt1pr = fun.GetData(selimpot1pr);
                        foreach (DataRow row in dt1pr.Rows)
                        {
                            if (row["RType"].ToString() == wa1 || row["RType"].ToString() == e7teag)
                            {

                                sader += (double)row["QuantityBefror"];
                            }
                            else
                            {
                                wared += (double)row["QuantityBefror"];
                            }
                        }
                        ///////////////////////////////////////////////////////////////////////////////////

                        try
                        {
                            string Transq = @"SELECT  [Transfer].[ID] as TransferID,
      [Transfer].[FromStorID],
      [Transfer].[ToStorID],
      [Transfer].[Date],
	  TransferDetail.ProductID,
	  TransferDetail.[TransferedQty]
  FROM [EPE].[dbo].[Transfer]
  inner  join TransferDetail
  on TransferDetail.[TransferID]=[Transfer].[ID]
  where [Transfer].[Date]<=" + DateTo + "  and TransferDetail.ProductID=N'" +
                prow["ID"].ToString() + "' and ([Transfer].[FromStorID]=" + StorID + " or [Transfer].[ToStorID]=" + StorID + " )";


                            DataTable dt1trans = fun.GetData(Transq);
                            foreach (DataRow row in dt1trans.Rows)
                            {
                                if (row["FromStorID"].ToString() == StorID.ToString())
                                {

                                    sader += double.Parse(row["TransferedQty"].ToString());
                                }
                                else if (row["ToStorID"].ToString() == StorID.ToString())
                                {
                                    wared += double.Parse(row["TransferedQty"].ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        /////////////////////////////////////////////
                        if (StorID == 1)
                            oprsed += (double)prow["BalanceQ"];

                        //lst.Add(new Models.EntitiesDB.InventoryReports.CardOfCategory { prbalance = (double)row["BalanceQ"] + oprsed, ProductID = int.Parse(row["ID"].ToString()), RType = op, date = datefrom.AddDays(-1) });


                        lst.Add(new BalancesOfProducts { ProductID = prow["ID"].ToString(), ProductName = prow["name"].ToString(), balance = oprsed + wared - sader });
                    }

                    return lst;// ConvertDT<CardOfCategory>(dt);
                }
                public List<BalancesOfProducts> balances(DateTime dateto, string product_ID)
                {
                    //dateto = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                    List<BalancesOfProducts> lst = new List<BalancesOfProducts>();
                    string op = "—’Ìœ «›  «ÕÏ";
                    //string DateF = ConvertDate(datefrom.ToShortDateString());
                    string DateTo = ConvertDate(dateto.ToShortDateString());
                    string wa = "Ê«—œ";
                    string wa1 = "’«œ—";
                    string e7teag = "«Õ Ì«Ã";

                    double oprsed = 0;
                    double wared = 0;
                    double sader = 0;
                    //////////////////////////////////////////////////////////////////////////
                    string selpr = "select * from product where ID=N'" + product_ID + "'";
                    DataTable dpro = fun.GetData(selpr);
                    foreach (DataRow prow in dpro.Rows)
                    {
                        oprsed = 0;
                        wared = 0;
                        sader = 0;
                        ///////////////////////////////////////////////////////////////////////////
                        string selimpotpr = "select ReceivedOrder.subid,ReceivedOrder.date,ReceivedOrderDetail.quantity,ReceivedOrderDetail.ProductID,ReceivedOrder.ReceivedType from ReceivedOrder inner join  ReceivedOrderDetail on ReceivedOrder.ID=ReceivedOrderDetail.ReceivedOrderID where  ReceivedOrderDetail.ProductID=N'" + prow["ID"].ToString() + "' and ReceivedOrder.date<= " + DateTo + " ";
                        DataTable dtpr = fun.GetData(selimpotpr);
                        ///////////////////

                        foreach (DataRow row in dtpr.Rows)
                        {
                            if (row["ReceivedType"].ToString() == wa)
                            {
                                wared += (double)row["quantity"];
                            }
                            else
                            {
                                sader += (double)row["quantity"];
                            }

                        }

                        string selimpot1pr = "select Receipt.CustID,Receipt.date,ReceiptDetials.QuantityBefror,ReceiptDetials.ProductID,Receipt.rtype from Receipt inner join  ReceiptDetials on Receipt.ID=ReceiptDetials.RID where  ReceiptDetials.ProductID=N'" + prow["ID"].ToString() + "' and Receipt.date<= " + DateTo + " ";
                        DataTable dt1pr = fun.GetData(selimpot1pr);
                        foreach (DataRow row in dt1pr.Rows)
                        {
                            if (row["RType"].ToString() == wa1 || row["RType"].ToString() == e7teag)
                            {

                                sader += (double)row["QuantityBefror"];
                            }
                            else
                            {
                                wared += (double)row["QuantityBefror"];
                            }
                        }
                        ///////////////////////////////////////////////////////////////////////////////////

                        oprsed += (double)prow["BalanceQ"];
                        //lst.Add(new Models.EntitiesDB.InventoryReports.CardOfCategory { prbalance = (double)row["BalanceQ"] + oprsed, ProductID = int.Parse(row["ID"].ToString()), RType = op, date = datefrom.AddDays(-1) });


                        lst.Add(new BalancesOfProducts { ProductID = prow["ID"].ToString(), balance = oprsed + wared - sader });
                    }

                    return lst;// ConvertDT<CardOfCategory>(dt);
                }

                public List<BalancesOfProducts> StorProductBalances(DateTime dateto, string product_ID, int StorID)
                {

                    //dateto = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                    List<BalancesOfProducts> lst = new List<BalancesOfProducts>();
                    string op = "—’Ìœ «›  «ÕÏ";
                    //string DateF = ConvertDate(datefrom.ToShortDateString());
                    string DateTo = ConvertDate(dateto.ToShortDateString());
                    string wa = "Ê«—œ";
                    string wa1 = "’«œ—";
                    string e7teag = "«Õ Ì«Ã";

                    double oprsed = 0;
                    double wared = 0;
                    double sader = 0;
                    //////////////////////////////////////////////////////////////////////////
                    string selpr = "select * from product where ID=N'" + product_ID + "'";
                    DataTable dpro = fun.GetData(selpr);
                    foreach (DataRow prow in dpro.Rows)
                    {
                        oprsed = 0;
                        wared = 0;
                        sader = 0;
                        ///////////////////////////////////////////////////////////////////////////
                        if (StorID == 1)
                        {
                            string selimpotpr = "select ReceivedOrder.subid,ReceivedOrder.date,ReceivedOrderDetail.quantity,ReceivedOrderDetail.ProductID,ReceivedOrder.ReceivedType from ReceivedOrder inner join  ReceivedOrderDetail on ReceivedOrder.ID=ReceivedOrderDetail.ReceivedOrderID where  ReceivedOrderDetail.ProductID=N'" + prow["ID"].ToString() + "' and ReceivedOrder.date<= " + DateTo + " ";
                            DataTable dtpr = fun.GetData(selimpotpr);
                            ///////////////////

                            foreach (DataRow row in dtpr.Rows)
                            {
                                if (row["ReceivedType"].ToString() == wa)
                                {
                                    wared += (double)row["quantity"];
                                }
                                else
                                {
                                    sader += (double)row["quantity"];
                                }

                            }

                        }
                        string selimpot1pr = @"select Receipt.CustID,Receipt.date,
                      ReceiptDetials.QuantityBefror,ReceiptDetials.ProductID,
                     Receipt.rtype from Receipt inner join  ReceiptDetials on Receipt.ID=ReceiptDetials.RID where 
                    ReceiptDetials.ProductID=N'" +
                            prow["ID"].ToString() + "' and Receipt.date<= " + DateTo + " and ReceiptDetials.StorID=" + StorID;
                        DataTable dt1pr = fun.GetData(selimpot1pr);
                        foreach (DataRow row in dt1pr.Rows)
                        {
                            if (row["RType"].ToString() == wa1 || row["RType"].ToString() == e7teag)
                            {

                                sader += (double)row["QuantityBefror"];
                            }
                            else
                            {
                                wared += (double)row["QuantityBefror"];
                            }
                        }
                        ///////////////////////////////////////////////////////////////////////////////////
                        try
                        {
                            string Transq = @"SELECT  [Transfer].[ID] as TransferID,
      [Transfer].[FromStorID],
      [Transfer].[ToStorID],
      [Transfer].[Date],
	  TransferDetail.ProductID,
	  TransferDetail.[TransferedQty]
  FROM [EPE].[dbo].[Transfer]
  inner  join TransferDetail
  on TransferDetail.[TransferID]=[Transfer].[ID]
  where [Transfer].[Date]<=" + DateTo + "  and TransferDetail.ProductID=N'" +
                prow["ID"].ToString() + "' and ([Transfer].[FromStorID]=" + StorID + " or [Transfer].[ToStorID]=" + StorID + " )";


                            DataTable dt1trans = fun.GetData(Transq);
                            foreach (DataRow row in dt1trans.Rows)
                            {
                                if (row["FromStorID"].ToString() == StorID.ToString())
                                {

                                    sader += double.Parse(row["TransferedQty"].ToString());
                                }
                                else if (row["ToStorID"].ToString() == StorID.ToString())
                                {
                                    wared += double.Parse(row["TransferedQty"].ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        //////////////////////////////////////////
                        if (StorID == 1)
                        {
                            oprsed += (double)prow["BalanceQ"];
                        }
                        else
                        {
                            oprsed = 0;

                        }//lst.Add(new Models.EntitiesDB.InventoryReports.CardOfCategory { prbalance = (double)row["BalanceQ"] + oprsed, ProductID = int.Parse(row["ID"].ToString()), RType = op, date = datefrom.AddDays(-1) });



                        lst.Add(new BalancesOfProducts { ProductID = prow["ID"].ToString(), balance = oprsed + wared - sader });
                    }

                    return lst;// ConvertDT<CardOfCategory>(dt);
                }


            }

            public string Import_CalcQuntity(IEnumerable<ImportsCategory> lst)
            {
                double? TQuntoty = 0;
                foreach (var nn in lst)
                {
                    TQuntoty += nn.quantity;
                }
                return TQuntoty.Value.ToString("#.0");
            }

            public string Export_CalcQuntity(IEnumerable<ExportsCategory> lst)
            {
                double? TQuntoty = 0;
                foreach (var nn in lst)
                {
                    TQuntoty += nn.QuantityBefror;
                }
                return TQuntoty.Value.ToString("#.0");
            }


            public CardOfCategory Card_CalcQuntity(IEnumerable<CardOfCategory> lst)
            {
                double? ImpoQuntoty = 0;
                double? ExQuntoty = 0;
                foreach (var nn in lst)
                {
                    ImpoQuntoty += nn.imquantity;
                    ExQuntoty += nn.exquantity;
                }
                return new CardOfCategory { imquantity = ImpoQuntoty, exquantity = ExQuntoty };
            }

            public string Balance_CalcQuntity(IEnumerable<BalancesOfProducts> lst)
            {
                double? TQuntoty = 0;
                foreach (var nn in lst)
                {
                    TQuntoty += nn.balance;
                }
                return TQuntoty.Value.ToString("#.0");
            }

            public string CalcQuntity(IEnumerable<Store> lst)
            {
                throw new NotImplementedException();
            }

            public Branch Card_CalcQuntity(IEnumerable<Branch> lst)
            {
                throw new NotImplementedException();
            }

            public string Balance_CalcQuntity(IEnumerable<Store> lst)
            {
                throw new NotImplementedException();
            }
        }

        public static string formatDateTime(DateTime date)
        {
            if (date == null) { return null; }
            var year = ((date.Year).ToString().Length == 1) ? '0' + date.Year.ToString() : date.Year.ToString();
            var month = ((date.Month).ToString().Length == 1) ? '0' + (date.Month.ToString()) : (date.Month.ToString());
            var day = ((date.Day).ToString().Length == 1) ? '0' + date.Day.ToString() : date.Day.ToString();
            var hour = ((date.Hour).ToString().Length == 1) ? '0' + date.Hour.ToString() : date.Hour.ToString();
            var min = ((date.Minute).ToString().Length == 1) ? '0' + date.Minute.ToString() : date.Minute.ToString();
            var sec = ((date.Second).ToString().Length == 1) ? '0' + date.Second.ToString() : date.Second.ToString();
            return year.ToString() + '-' + month.ToString() + '-' + day.ToString() + " " + hour.ToString() + ':' + min.ToString() + ':' + sec.ToString();
        }
        public static string formatDate(DateTime date)
        {
            if (date == null) { return null; }
            var year = ((date.Year).ToString().Length == 1) ? '0' + date.Year.ToString() : date.Year.ToString();
            var month = ((date.Month).ToString().Length == 1) ? '0' + (date.Month.ToString()) : (date.Month.ToString());
            var day = ((date.Day).ToString().Length == 1) ? '0' + date.Day.ToString() : date.Day.ToString();
            return year.ToString() + '-' + month.ToString() + '-' + day.ToString();
        }
        public static string ReformateDateFromPicker(string date)
        {
            var alldate = date.Split('-');
            return alldate[0]+"-"+alldate[1]+"-"+ alldate[2];
        }
        public static string ConvertDate(string date)
        {
            //string bbsget;
            //0109
            string sadater;
            //string stimer;
            //DateTime utcTime = DateTime.UtcNow;
            //TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            //DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            //bbsget = custDateTime.ToString("HH:mm");
            string jj = DateTime.Parse(date).Year + "-" + DateTime.Parse(date).Month + "-" + DateTime.Parse(date).Day;
            sadater = "Cast(CONVERT(varchar,'" + jj + "', 102) AS DATETIME)";
            //stimer = bbsget;
            return sadater;
        }

        public static List<T> ConvertDT<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (dr[column.ColumnName] == DBNull.Value)
                        {
                            pro.SetValue(obj, null, null);
                            break;
                        }
                        else
                        {
                            if (column.DataType.Name.ToLower() == "datetime")
                            {
                                //DateTime date = DateTime.Parse(dr[column.ColumnName].ToString(), CultureInfo.CreateSpecificCulture("ar-EG"));
                                DateTime date = DateTime.Parse(dr[column.ColumnName].ToString());
                                pro.SetValue(obj, date, null);
                                break;
                            }
                            else
                            { pro.SetValue(obj, dr[column.ColumnName], null); break; }
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
        public class Store
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            public Store()
            {
            }
            public int? ID { set; get; }
            public string Name { set; get; }
            public int? UserID { set; get; }
            public int? BranchID { set; get; }

            public string BranchName { set; get; }
            public string BranchAddress { set; get; }
            public string BranchPhone { set; get; }

            public int StoreID;
            public void maxid()
            {
                try
                {
                    StoreID = int.Parse(fun.FireSql("select max(ID) from Store ").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    StoreID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Store AddedStore = new Store();
                    AddedStore = (Store)content;
                    if (AddedStore != null)
                    {
                        try
                        {
                            DataTable dtexist = fun.GetData("select * from store where name=N'" + AddedStore.Name + "'");
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            dtexist = fun.GetData("select * from Store where ID=" + AddedStore.ID);
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            fun.FireSql("insert  into Store(Name,UserID,BranchID)values(N'" + AddedStore.Name + "'," + AddedStore.UserID + "," + AddedStore.BranchID + ")");
                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Edit")
                {
                    Store EditStore = new Store();
                    EditStore = (Store)content;
                    if (EditStore != null)
                    {
                        try
                        {
                            string update = "update Store set Name=N'" + EditStore.Name + "',UserID=" + EditStore.UserID + ",BranchID=" + EditStore.BranchID + " where ID=" + EditStore.ID + "";
                            fun.FireSql(update);
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    int id = Convert.ToInt32(content);
                    DataTable dtexist = fun.GetData("select * from Store where ID=" + id);
                    if (dtexist.Rows.Count > 0)
                    {
                        try
                        {
                            fun.FireSql("delete from Store where ID=" + id);
                            return 1001;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        return 1004;//Not Exist
                    }
                }
                return 1009;
            }
            public object Get_StoreFilter(Store Store)
            {
                string strtemp = "";
                strtemp += (Store.ID != null) ? Store.ID.ToString() + "," : "NULL,";
                strtemp += (Store.Name != null) ? "'" + Store.Name.ToString() + "'," : "NULL,";
                strtemp += (Store.UserID != null) ? Store.UserID.ToString() + "," : "NULL,";
                strtemp += (Store.BranchID != null) ? Store.BranchID.ToString() : "NULL";

                if (Store != null)
                {
                    string getall = "Execute SP_StoreFilter " + strtemp;
                    DataTable allStore = fun.GetData(getall);
                    if (allStore != null)
                        return ConvertDT<Store>(allStore);
                    return 1008;
                }
                return 1008;
            }
        }
        public class Branch
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();

            public int ID { set; get; }
            public string Name { set; get; }
            public string Address { set; get; }
            public string Phone { set; get; }
            public int UserID { set; get; }

            public Store store { set; get; }
            public int BranchID;
            public Branch()
            {
                store = new Store();
            }
            public void maxid()
            {
                try
                {
                    BranchID = int.Parse(fun.FireSql("select max(ID) from Branch ").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    BranchID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Branch AddedBranch = new Branch();
                    AddedBranch = (Branch)content;
                    if (AddedBranch != null)
                    {
                        try
                        {
                            DataTable dtexist = fun.GetData("select * from Branch where name=N'" + AddedBranch.Name + "'");
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            dtexist = fun.GetData("select * from Branch where ID=" + AddedBranch.ID);
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            fun.FireSql("insert  into Branch(Name,Address,Phone,UserID)values(N'" + AddedBranch.Name + "',N'" + AddedBranch.Address + "',N'" + AddedBranch.Phone + "'," + AddedBranch.UserID + ")");
                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Edit")
                {
                    Branch EditBranch = new Branch();
                    EditBranch = (Branch)content;
                    if (EditBranch != null)
                    {
                        try
                        {
                            string update = "update Branch set Name=N'" + EditBranch.Name + "',Address=N'" + EditBranch.Address + "',Phone=N'" + EditBranch.Phone + "',UserID=" + EditBranch.UserID + " where ID=" + EditBranch.ID;
                            fun.FireSql(update);
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    int id = Convert.ToInt32(content);
                    DataTable dtexist = fun.GetData("select * from Branch where ID=" + id);
                    if (dtexist.Rows.Count > 0)
                    {
                        try
                        {
                            fun.FireSql("delete from Branch where ID=" + id);
                            return 1001;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        return 1004;//Not Exist
                    }
                }
                return 1009;
            }
            public object Get_AllBranches()
            {
                string getall = "select * from Branch";
                DataTable allbranches = fun.GetData(getall);
                if (allbranches != null)
                    return ConvertDT<Branch>(allbranches);
                return 1008;
            }
            public object Get_BranchByID(int ID)
            {
                string getall = "select * from Branch where ID=" + ID + "";
                DataTable allBranch = fun.GetData(getall);
                if (allBranch != null)
                    return ConvertDT<Branch>(allBranch).ToList().FirstOrDefault();
                return 1008;
            }
            public object Get_AllStoreByBranch(int Branch_ID)
            {
                string getall = "select * from store where BranchID=" + Branch_ID + "";
                DataTable allstore = fun.GetData(getall);
                if (allstore != null)
                    return ConvertDT<Store>(allstore);
                return 1008;
            }
            public object Get_AllBranchesByBrancheName(string Branch_Name)
            {
                string getall = "select * from Branch where Name=N'%" + Branch_Name + "%'";
                DataTable allBranch = fun.GetData(getall);
                if (allBranch != null)
                    return ConvertDT<Branch>(allBranch);
                return 1008;

            }
            public object Get_AllBranchesByUser(int User_ID)
            {
                string getall = "select * from Branch where UserID=" + User_ID + "";
                DataTable allBranches = fun.GetData(getall);
                if (allBranches != null)
                    return ConvertDT<Branch>(allBranches);
                return 1008;
            }
        }
        public class Privilage
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            private List<Privilage> Privilagehlst;

            public int ID { set; get; }
            public string Title { set; get; }

            public int PrivilageID;
            public Privilage()
            {
            }
            public void maxid()
            {
                try
                {
                    PrivilageID = int.Parse(fun.FireSql("select max(ID) from Privilage").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    PrivilageID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Privilagehlst = (List<Privilage>)content;
                    if (Privilagehlst.Count > 0)
                    {
                        try
                        {

                            foreach (var it in Privilagehlst)
                            {
                                DataTable dtexist = fun.GetData("select * from Privilage where Title=N'" + it.Title + "'");
                                if (dtexist.Rows.Count > 0)
                                {
                                    return 1003;//Exist
                                }
                                fun.FireSql("insert  into Privilage(Title)values(N'" + it.Title + ")");
                            }

                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;
                        }

                    }
                }
                else if (method == "Edit")
                {
                    Privilagehlst = (List<Privilage>)content;
                    if (Privilagehlst.Count > 0)
                    {
                        try
                        {


                            foreach (var row in Privilagehlst)
                            {
                                string update = "update Privilage set Title=N'" + row.Title + "' where ID=" + row.ID + "";
                                fun.FireSql(update);
                            }
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    Privilagehlst = (List<Privilage>)content;
                    if (Privilagehlst.Count > 0)
                    {
                        fun.FireSql("delete from Privilage where ID=" + Privilagehlst.First().ID + "");
                        return 1001;
                    }
                }
                return 1009;
            }
            public object Get_AllPrivilages()
            {

                string getall = "select * from Privilage";
                DataTable allPrivilage = fun.GetData(getall);
                if (allPrivilage != null)
                    return ConvertDT<Privilage>(allPrivilage);
                return 1008;
            }
            public object Get_PrivilageByID(int ID)
            {
                string getall = "select * from Privilage where ID=" + ID + "";
                DataTable allPrivilage = fun.GetData(getall);
                if (allPrivilage != null)
                    return ConvertDT<Privilage>(allPrivilage).ToList().FirstOrDefault();
                return 1008;
            }
        }
        public class Role
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            private List<Role> Rolelist;
            public int ID { set; get; }
            public string Name { set; get; }

            public int RoleID;
            public Role()
            {
            }
            public void maxid()
            {
                try
                {
                    RoleID = int.Parse(fun.FireSql("select max(ID) from Role").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    RoleID = 1;
                }
            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Rolelist = (List<Role>)content;
                    if (Rolelist.Count > 0)
                    {
                        try
                        {
                            foreach (var it in Rolelist)
                            {
                                DataTable dtexist = fun.GetData("select * from Role where Name=N'" + it.Name + "'");
                                if (dtexist.Rows.Count > 0)
                                {
                                    return 1003;//Exist
                                }
                                fun.FireSql("insert  into Role(Name)values(N'" + it.Name + ")");
                            }
                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;
                        }

                    }
                }
                else if (method == "Edit")
                {
                    Rolelist = (List<Role>)content;
                    if (Rolelist.Count > 0)
                    {
                        try
                        {
                            foreach (var row in Rolelist)
                            {
                                string update = "update Role set Name=N'" + row.Name + "' where ID=" + row.ID + "";
                                fun.FireSql(update);
                            }
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    Rolelist = (List<Role>)content;
                    if (Rolelist.Count > 0)
                    {
                        fun.FireSql("delete from Role where ID=" + Rolelist.First().ID + "");
                        return 1001;
                    }
                }
                return 1009;
            }
            public object Get_AllRoles()
            {
                string getall = "select * from Role";
                DataTable allRoles = fun.GetData(getall);
                if (allRoles != null)
                    return ConvertDT<Role>(allRoles);
                return 1008;
            }
            public object Get_RoleByID(int ID)
            {
                string getall = "select * from Role where ID=" + ID;
                DataTable allRoles = fun.GetData(getall);
                if (allRoles != null)
                    return ConvertDT<Role>(allRoles).ToList().FirstOrDefault();
                return 1008;
            }
        }
        public class RolePrivlageFT
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            private List<RolePrivlageFT> RolePrivlist;
            public int ID { set; get; }
            public int RoleID { set; get; }
            public int PrivilageID { set; get; }


            public int RolePrivID;
            public RolePrivlageFT()
            {
            }
            public void maxid()
            {
                try
                {
                    RolePrivID = int.Parse(fun.FireSql("select max(ID) from RolePrivlageFT").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    RoleID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    RolePrivlist = (List<RolePrivlageFT>)content;
                    if (RolePrivlist.Count > 0)
                    {
                        try
                        {
                            foreach (var it in RolePrivlist)
                            {
                                DataTable dtexist = fun.GetData("select * from RolePrivlageFT where ( RoleID=" + it.RoleID + " and  PrivilageID=" + it.PrivilageID + ")");
                                if (dtexist.Rows.Count > 0)
                                {
                                    return 1003;//Exist
                                }
                                fun.FireSql("insert  into RolePrivlageFT(RoleID,PrivilageID)values(" + it.RoleID + "," + it.PrivilageID + ")");
                            }
                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Edit")
                {
                    RolePrivlist = (List<RolePrivlageFT>)content;
                    if (RolePrivlist.Count > 0)
                    {
                        try
                        {
                            foreach (var row in RolePrivlist)
                            {
                                string update = "update RolePrivlageFT set RoleID=" + row.RoleID + ",PrivilageID=" + row.PrivilageID + " where ID=" + row.ID + "";
                                fun.FireSql(update);
                            }
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    RolePrivlist = (List<RolePrivlageFT>)content;
                    if (RolePrivlist.Count > 0)
                    {
                        fun.FireSql("delete from RolePrivlageFT where ID=" + RolePrivlist.First().ID + "");
                        return 1001;
                    }
                }
                return 1009;
            }
            public object Get_AllRelationByRole(int ID)
            {

                string getall = "select * from RolePrivlageFT where RoleID =" + ID;
                DataTable allRelation = fun.GetData(getall);
                if (allRelation != null)
                    return ConvertDT<RolePrivlageFT>(allRelation);
                return 1008;
            }
            public object Get_AllRelationByPrivlage(int ID)
            {

                string getall = "select * from RolePrivlageFT where PrivilageID =" + ID;
                DataTable allRelation = fun.GetData(getall);
                if (allRelation != null)
                    return ConvertDT<RolePrivlageFT>(allRelation);
                return 1008;
            }
            public object Get_RelationByID(int ID)
            {
                string getall = "select * from RolePrivlageFT where ID=" + ID + "";
                DataTable allRelation = fun.GetData(getall);
                if (allRelation != null)
                    return ConvertDT<Role>(allRelation).ToList().FirstOrDefault();
                return 1008;
            }
        }
        public class User
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            private List<User> Userlist;
            public Role Role { get; set; }

            public int ID { set; get; }
            public string Username { set; get; }
            public string Password { set; get; }
            public string Email { set; get; }
            public string Phone { set; get; }
            public int RoleID { set; get; }

            public int UserID;

            public User()
            {
            }
            public void maxid()
            {
                try
                {
                    UserID = int.Parse(fun.FireSql("select max(ID) from AspUser ").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    UserID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Userlist = (List<User>)content;
                    if (Userlist.Count > 0)
                    {
                        foreach (var it in Userlist)
                        {
                            DataTable dtexist = fun.GetData("select * from [AspUser] where Email=N'" + it.Email + "' OR Phone=N'" + it.Phone + "'");
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003; //Exist
                            }
                            fun.FireSql("insert  into [AspUser] (Username,Password,Email,Phone,RoleID)values(N'" + it.Username + "',N'" + it.Password + "'," + it.Email + ",N'" + it.Phone + "'," + it.RoleID + ")");
                        }
                        return 1001; //Success
                    }
                }
                else if (method == "Edit")
                {
                    Userlist = (List<User>)content;
                    if (Userlist.Count > 0)
                    {
                        try
                        {
                            foreach (var row in Userlist)
                            {
                                string update = "update [AspUser] set Username=N'" + row.Username + "',Password=N'" + row.Password + "',Email=N'" + row.Email + "' Phone=N'" + row.Phone + ",RoleID=" + row.RoleID + " where ID=" + row.ID + "";
                                fun.FireSql(update);
                            }
                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;// Failed
                        }
                    }
                }
                else if (method == "Delete")
                {
                    Userlist = (List<User>)content;
                    if (Userlist.Count > 0)
                    {
                        fun.FireSql("delete from [AspUser] where ID=" + Userlist.First().ID + "");
                        return 1001;//successs
                    }
                }
                return 1009;
            }
            public object Get_AllUser()
            {
                string getall = "select * from [AspUser]";
                DataTable allUser = fun.GetData(getall);
                if (allUser != null)
                    return ConvertDT<User>(allUser);
                return 1008;
            }
            public object Get_UserByID(int ID)
            {
                string getall = "select * from [AspUser] where ID=" + ID + "";
                DataTable allUser = fun.GetData(getall);
                if (allUser != null)
                    return ConvertDT<User>(allUser).ToList().FirstOrDefault();
                else return 1008;
            }
            public object Get_AllUsersByName(string User_Name)
            {
                string getall = "select * from [AspUser] where Username=N'" + User_Name + "'";
                DataTable allUser = fun.GetData(getall);
                if (allUser != null)
                    return ConvertDT<User>(allUser);
                else return 1008;
            }
            public object Get_AllUserByRole(int Role_ID)
            {
                string getall = "select * from [AspUser] where RoleID=" + Role_ID;
                DataTable allUser = fun.GetData(getall);
                return ConvertDT<User>(allUser);
            }
        }
        public class Units
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            public int ID { set; get; }
            public string Name { set; get; }

            public int UnitID;
            public Units()
            {
            }
            public void maxid()
            {
                try
                {
                    UnitID = int.Parse(fun.FireSql("select max(ID) from Unit").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    UnitID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Units AddedUnits = new Units();
                    AddedUnits = (Units)content;

                    if (AddedUnits != null)
                    {
                        try
                        {
                            DataTable dtexist = fun.GetData("select * from Unit where Name=N'" + AddedUnits.Name + "'");
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            dtexist = fun.GetData("select * from Unit where ID=" + AddedUnits.ID);
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            fun.FireSql("insert  into Unit(Name)values(N'" + AddedUnits.Name + "')");
                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Edit")
                {
                    Units EditUnits = new Units();
                    EditUnits = (Units)content;
                    if (EditUnits != null)
                    {
                        try
                        {
                            string update = "update Unit set Name=N'" + EditUnits.Name + "' where ID=" + EditUnits.ID + "";
                            fun.FireSql(update);
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    int id = Convert.ToInt32(content);
                    DataTable dtexist = fun.GetData("select * from Unit where ID=" + id);
                    if (dtexist.Rows.Count > 0)
                    {
                        try
                        {
                            fun.FireSql("delete from Unit where ID=" + id);
                            return 1001;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        return 1004;//Not Exist
                    }
                }
                return 1009;
            }
            public object Get_All()
            {

                string getall = "select * from Unit";
                DataTable allUnits = fun.GetData(getall);
                if (allUnits != null)
                    return ConvertDT<Units>(allUnits);
                return 1008;
            }
            public object Get_UnitByID(int ID)
            {
                string getall = "select * from Unit where ID=" + ID;
                DataTable allUnits = fun.GetData(getall);
                if (allUnits != null)
                    return ConvertDT<Units>(allUnits).ToList().FirstOrDefault();
                return 1008;
            }
        }
        public class Product
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            public int? ID { set; get; }
            public string Name { set; get; }
            public double? Price { set; get; }
            public double? Qty { set; get; }
            public int? UserID { set; get; }
            public int? UnitID { set; get; }
            public int? StoreID { set; get; }
            public bool? HasSize { set; get; }
            public double? Width { set; get; }
            public double? Lenght { set; get; }
            public int? MaterialID { set; get; }

            // For view and show
            public string StoreName { set; get; }
            public string UnitName { set; get; }
            public string MaterialName { set; get; }

            public int ProductID;
            public Product()
            {
            }
            public void maxid()
            {
                try
                {
                    ProductID = int.Parse(fun.FireSql("select max(ID) from Product").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    ProductID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Product AddedProduct = new Product();
                    AddedProduct = (Product)content;
                    if (AddedProduct != null)
                    {
                        try
                        {
                            DataTable dtexist = fun.GetData("select * from Product where name=N'" + AddedProduct.Name + "'");
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            dtexist = fun.GetData("select * from Product where ID=" + AddedProduct.ID);
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            fun.FireSql("insert  into Product(Name,Price,Qty,UserID,UnitID,StoreID,HasSize,Width,Lenght,MaterialID)values(N'" + AddedProduct.Name + "'," + AddedProduct.Price + "," + AddedProduct.Qty + "," + AddedProduct.UserID + "," + AddedProduct.UnitID + "," + AddedProduct.StoreID + "," + Convert.ToInt32(AddedProduct.HasSize) + "," + AddedProduct.Width + "," + AddedProduct.Lenght + "," + AddedProduct.MaterialID + ")");
                            return 1001;//Success
                        }
                        catch (Exception e)
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Edit")
                {
                    Product AddedProduct = new Product();
                    AddedProduct = (Product)content;
                    if (AddedProduct != null)
                    {
                        try
                        {
                            string update = "update Product set Name='" + AddedProduct.Name + "',Price=" + AddedProduct.Price + ",Qty=" + AddedProduct.Qty + ",UserID=" + AddedProduct.UserID + ",UnitID=" + AddedProduct.UnitID + ",StoreID=" + AddedProduct.StoreID + ",HasSize=" + Convert.ToInt32(AddedProduct.HasSize) + ",Width=" + AddedProduct.Width + ",Lenght=" + AddedProduct.Lenght + ",MaterialID=" + AddedProduct.MaterialID + " where ID=" + AddedProduct.ID;
                            fun.FireSql(update);
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    int id = Convert.ToInt32(content);
                    DataTable dtexist = fun.GetData("select * from Product where ID=" + id);
                    if (dtexist.Rows.Count > 0)
                    {
                        try
                        {
                            fun.FireSql("delete from Product where ID=" + id);
                            return 1001;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        return 1004;//Not Exist
                    }
                }
                return 1009;
            }
            public object Get_Filter(Product product)
            {
                string strtemp = "";
                strtemp += (product.ID != null) ? product.ID.ToString() + "," : "NULL,";
                strtemp += (product.Name != null) ? "'" + product.Name.ToString() + "'," : "NULL,";
                strtemp += (product.Price != null) ? product.Price.ToString() + "," : "NULL,";
                strtemp += (product.Qty != null) ? product.Qty.ToString() + "," : "NULL,";
                strtemp += (product.UserID != null) ? product.UserID.ToString() + "," : "NULL,";
                strtemp += (product.UnitID != null) ? product.UnitID.ToString() + "," : "NULL,";
                strtemp += (product.StoreID != null) ? product.StoreID.ToString() + "," : "NULL,";
                strtemp += (product.HasSize != null) ? product.HasSize.ToString() + "," : "NULL,";
                strtemp += (product.Width != null) ? product.Width.ToString() + "," : "NULL,";
                strtemp += (product.Lenght != null) ? product.Lenght.ToString() + "," : "NULL,";
                strtemp += (product.MaterialID != null) ? product.MaterialID.ToString() : "NULL";

                if (product != null)
                {
                    string getall = "Execute SP_ProductFilter " + strtemp;
                    DataTable allProduct = fun.GetData(getall);
                    if (allProduct != null)
                        return ConvertDT<Product>(allProduct);
                    return 1008;
                }
                return 1008;
            }
        }
        public class Sector
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            public int ID { set; get; }
            public string Name { set; get; }

            public int SectorID;
            public Sector()
            {
            }
            public void maxid()
            {
                try
                {
                    SectorID = int.Parse(fun.FireSql("select max(ID) from Sector").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    SectorID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Sector AddedSector = new Sector();
                    AddedSector = (Sector)content;
                    
                    if (AddedSector != null)
                    {
                        try
                        {
                            DataTable dtexist = fun.GetData("select * from Sector where Name=N'" + AddedSector.Name + "'");
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            dtexist = fun.GetData("select * from Sector where ID=" + AddedSector.ID);
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            fun.FireSql("insert  into Sector(Name)values(N'" + AddedSector.Name + "')");
                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Edit")
                {
                    Sector EditSector = new Sector();
                    EditSector = (Sector)content;
                    if (EditSector != null)
                    {
                        try
                        {
                            string update = "update Sector set Name=N'" + EditSector.Name + "' where ID=" + EditSector.ID;
                            fun.FireSql(update);
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    int id = Convert.ToInt32(content);
                    DataTable dtexist = fun.GetData("select * from Sector where ID=" + id);
                    if (dtexist.Rows.Count > 0)
                    {
                        try
                        {
                            fun.FireSql("delete from Sector where ID=" + id);
                            return 1001;
                        }
                        catch( Exception e)
                        {
                        }
                    }
                    else
                    {
                        return 1004;//Not Exist
                    }
                }
                return 1009;
            }
            public object Get_All()
            {

                string getall = "select * from Sector";
                DataTable allSector = fun.GetData(getall);
                if (allSector != null)
                    return ConvertDT<Sector>(allSector);
                return 1008;
            }
            public object Get_UnitByID(int ID)
            {
                string getall = "select * from Sector where ID=" + ID;
                DataTable allSector = fun.GetData(getall);
                if (allSector != null)
                    return ConvertDT<Sector>(allSector).ToList().FirstOrDefault();
                return 1008;
            }
        }
        public class Service
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            public int ID { set; get; }
            public string Name { set; get; }
            public int UserID { set; get; }

            public int ServiceID;
            public Service()
            {
            }
            public void maxid()
            {
                try
                {
                    ServiceID = int.Parse(fun.FireSql("select max(ID) from Service").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    ServiceID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    Service AddedService = new Service();
                    AddedService = (Service)content;

                    if (AddedService != null)
                    {
                        try
                        {
                            DataTable dtexist = fun.GetData("select * from Service where Name=N'" + AddedService.Name + "'");
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            dtexist = fun.GetData("select * from Service where ID=" + AddedService.ID);
                            if (dtexist.Rows.Count > 0)
                            {
                                return 1003;//Exist
                            }
                            fun.FireSql("insert  into Service(Name,UserID)values(N'" + AddedService.Name + "'," + AddedService.UserID + ")");
                            return 1001;//Success
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Edit")
                {
                    Service EditService = new Service();
                    EditService = (Service)content;
                    if (EditService != null)
                    {
                        try
                        {
                            string update = "update Service set Name=N'" + EditService.Name + "',UserID=" + EditService.UserID + " where ID=" + EditService.ID + "";
                            fun.FireSql(update);
                            return 1001;
                        }
                        catch
                        {
                            return 1002;
                        }
                    }
                }
                else if (method == "Delete")
                {
                    int id = Convert.ToInt32(content);
                    DataTable dtexist = fun.GetData("select * from Service where ID=" + id);
                    if (dtexist.Rows.Count > 0)
                    {
                        try
                        {
                            fun.FireSql("delete from Service where ID=" + id);
                            return 1001;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        return 1004;//Not Exist
                    }
                }
                return 1009;
            }
            public object Get_All()
            {

                string getall = "select * from Service";
                DataTable allService = fun.GetData(getall);
                if (allService != null)
                    return ConvertDT<Service>(allService);
                return 1008;
            }
            public object Get_ServiceByID(int ID)
            {
                string getall = "select * from Service where ID=" + ID;
                DataTable allService = fun.GetData(getall);
                if (allService != null)
                    return ConvertDT<Service>(allService).ToList().FirstOrDefault();
                return 1008;
            }
            public object Get_ServiceByUserID(int ID)
            {
                string getall = "select * from Service where UserID=" + ID;
                DataTable allServices = fun.GetData(getall);
                if (allServices != null)
                    return ConvertDT<Service>(allServices).ToList().FirstOrDefault();
                return 1008;
            }
        }
        public class MainAccount
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            //private object  prod;
            private List<MainAccount> po;

            public Int64 ID { set; get; }
            public string name { set; get; }
            public int UpAccount { set; get; }
            public int Level { set; get; }
            public int UserID { set; get; }
            public SubAccountManul subaccounts { set; get; }
            public int AccountID = 1;
            public void maxid()
            {
                try
                {
                    AccountID = int.Parse(fun.FireSql("select max(id),upaccount from MainAccount group by upaccount").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    AccountID = 1;
                }

            }
            public void maxid(int UpAccount)
            {
                try
                {
                    AccountID = int.Parse(fun.FireSql("select max(ID) from MainAccount where UpAccount =" + UpAccount).ToString()) + 1;
                }
                catch (Exception ex)
                {
                    AccountID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    po = (List<MainAccount>)content;
                    //string con = $"{FieldsName.ElementAt(0).Key} + {FieldsName.ElementAt(1).Key} +{FieldsName.ElementAt(2).Key } ";
                    if (po.Count > 0)
                    {
                        foreach (var it in po)
                        {
                            fun.FireSql("insert  into MainAccount(ID,name,UpAccount,Level,UserID)values(" + it.ID + ",N'" + it.name + "'," + it.UpAccount + "," + it.Level + "," + it.UserID + ")");
                        }
                        return "Added Successfully";
                    }
                }
                else if (method == "Edit")
                {
                    po = (List<MainAccount>)content;
                    if (po.Count > 0)
                    {
                        string update;
                        foreach (var row in po)
                        {
                            DataTable dt = fun.GetData("select * from MainAccount where id=" + row.AccountID + "");
                            if (dt.Rows.Count == 0)
                            {
                                fun.FireSql("insert  into MainAccount(ID,name,UpAccount,Level,UserID)values(" + row.ID + ",N'" + row.name + "'," + row.UpAccount + "," + row.Level + "," + row.UserID + ")");
                            }
                            else
                            {
                                update = "update MainAccount set ID=" + row.ID + ", name=N'" + row.name +
                                    "',upaccount=" + row.UpAccount + ",level=" + row.Level +
                                    ",userid=" + row.UserID + " where id=" + row.AccountID + "";
                                fun.FireSql(update);
                            }

                        }
                        return "Updated Successfully";
                    }
                }
                else if (method == "Delete")
                {
                    po = (List<MainAccount>)content;

                    if (po.Count > 0)
                    {
                        fun.FireSql("delete from MainAccount where id=" + po.First().ID + "");
                        return "Deleted Successfully";
                    }
                }

                return "No Action";
            }
            public List<MainAccount> GetAllMain()
            {
                string select = "select * from MainAccount";
                DataTable Maccounts = fun.GetData(select);
                return ConvertDT<MainAccount>(Maccounts);
            }
            public List<MainAccount> GetAllMainStartBy(int prefix)
            {
                string select = "select * from MainAccount where UpAccount like'" + prefix + "%'";
                DataTable Maccounts = fun.GetData(select);
                return ConvertDT<MainAccount>(Maccounts);
            }
            public List<MainAccount> GetAllMainStartBy(int prefix, int level)
            {
                string select = "";
                if (level == 1)
                {
                    level = 2;
                    select = "select * from MainAccount where ID like'" + prefix + "%' and Level=" + level;
                }
                else
                {
                    select = "select * from MainAccount where UpAccount like'" + prefix + "%' and Level=" + level;
                }
                DataTable Maccounts = fun.GetData(select);
                return ConvertDT<MainAccount>(Maccounts);
            }
            public List<MainAccount> GetMainAccount_ByID(int Account_ID)
            {
                string select = "select * from MainAccount where id=" + Account_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<MainAccount>(Account);
            }

            public List<MainAccount> GetMainAccount_ByUpID(int UpAccount_ID)
            {
                string select = "select * from MainAccount where upaccount=" + UpAccount_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<MainAccount>(Account);
            }

            public List<MainAccount> GetMainAccount_ByLevelID(int Level_ID)
            {
                string select = "select * from MainAccount where level=" + Level_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<MainAccount>(Account);
            }
            public object Get_AllMainAccount()
            {
                string getall = "select * from MainAccount";
                DataTable allMainAccount = fun.GetData(getall);
                if (allMainAccount != null)
                    return ConvertDT<MainAccount>(allMainAccount);
                return 1008;
            }
            public object Get_MainAccountByID(int ID)
            {
                string getall = "select * from MainAccount where ID=" + ID;
                DataTable allMainAccount = fun.GetData(getall);
                if (allMainAccount != null)
                    return ConvertDT<MainAccount>(allMainAccount).ToList().FirstOrDefault();
                else return 1008;
            }
            public object Get_MainAccountsByName(string MainAccount_Name)
            {
                string getall = "select * from MainAccount where Name=N'%" + MainAccount_Name + "%'";
                DataTable AllMainAccount = fun.GetData(getall);
                if (AllMainAccount != null)
                    return ConvertDT<MainAccount>(AllMainAccount);
                else return 1008;
            }
            public object Get_AllMainAccountByUserID(int User_ID)
            {
                string getall = "select * from MainAccount where UserID=" + User_ID;
                DataTable allMainAccount = fun.GetData(getall);
                if (allMainAccount != null)
                    return ConvertDT<MainAccount>(allMainAccount);
                else return 1008;
            }
        }
        public class SubAccountManul
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            //private object  prod;
            private List<SubAccountManul> po;

            public Int64 ID { set; get; }
            public string name { set; get; }
            public int Level { set; get; }
            public string UpAccount { set; get; }
            public string BType { set; get; }
            public DateTime RegisterDate { set; get; }
            public Int64 MainAccount_id { set; get; }
            public int user_id { set; get; }
            public Decimal ABalance { set; get; }

            public Levels level { set; get; }
            public SAccounts BasicAccounts { set; get; }

            public long AccountID { set; get; } = 1;
            public void maxid()
            {
                try
                {
                    AccountID = int.Parse(fun.FireSql("select max(id),MainAccount_id from SubAccount group by MainAccount_id").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    AccountID = 1;
                }

            }
            public void maxid(int UpAccount)
            {
                try
                {
                    AccountID = int.Parse(fun.FireSql("select max(ID)from SubAccount where upAccount =" + UpAccount).ToString()) + 1;
                }
                catch (Exception ex)
                {
                    AccountID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    po = (List<SubAccountManul>)content;
                    //string con = $"{FieldsName.ElementAt(0).Key} + {FieldsName.ElementAt(1).Key} +{FieldsName.ElementAt(2).Key } ";
                    if (po.Count > 0)
                    {
                        foreach (var it in po)
                        {
                            try
                            {
                                string str = "insert  into SubAccount(ID,name,[Level],BType,RegisterDate,MainAccount_id,user_id,ABalance,UpAccount)values(" + it.ID +
                                ",N'" + it.name + "'," + it.Level + ",N'" + it.BType + "'," + ConvertDate(it.RegisterDate.ToString()) +
                                "," + it.MainAccount_id + "," + it.user_id + "," + it.ABalance + "," + it.MainAccount_id + ")";
                                fun.FireSql(str);
                            }
                            catch(Exception except)
                            {

                            }
                            
                        }
                        return "Added Successfully";
                    }
                }
                else if (method == "Edit")
                {
                    po = (List<SubAccountManul>)content;
                    if (po.Count > 0)
                    {
                        string update;
                        foreach (var row in po)
                        {
                            DataTable dt = fun.GetData("select * from SubAccount where id=" + row.AccountID + "");
                            if (dt.Rows.Count == 0)
                            {
                                //fun.FireSql("insert  into SubAccount(ID,name,Level,BType,RegisterDate,MainAccount_id,user_id,ABalance,UpAccount)values(" +
                                //    row.ID + ",N'" + row.name + "'," + row.Level + ",N'" + row.BType + "'," +
                                //    ConvertDate(row.RegisterDate.ToShortDateString()) + "," + row.MainAccount_id +
                                //    "," + row.user_id + "," + row.ABalance + "," + row.MainAccount_id + ")");
                            }
                            else
                            {
                                update = "update SubAccount set ID=" + row.ID + ", name=N'" + row.name + "',level=" + row.Level +
                                    ",btype=N'" + row.BType + "',RegisterDate=" + ConvertDate(row.RegisterDate.ToString()) +
                                    ",MainAccount_id=" + row.MainAccount_id + ",user_id=" + row.user_id + ",ABalance=" +
                                    row.ABalance + ",UpAccount=" + row.MainAccount_id + " where id=" + row.AccountID + "";
                                fun.FireSql(update);
                            }

                        }
                        return "Updated Successfully";
                    }
                }
                else if (method == "Delete")
                {
                    po = (List<SubAccountManul>)content;
                    if (po.Count > 0)
                    {
                        fun.FireSql("delete from SubAccount where id=" + po.First().ID + "");
                        return "Deleted Successfully";
                    }
                }
                return "No Action";
            }
            public List<SubAccountManul> GetAllSub()
            {
                string select = "select * from SubAccount";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Saccounts);
            }
            // and Level = " + level
            public List<SubAccountManul> GetAllSubAccountStartBy(int prefix)
            {
                string select = "select * from SubAccount where UpAccount like'" + prefix + "%'";
                DataTable saccounts = fun.GetData(select);
                return ConvertDT<SubAccountManul>(saccounts);
            }
            public List<SubAccountManul> GetAllSubAccountStartBy(int prefix, int level)
            {
                string select = "select * from SubAccount where UpAccount like'" + prefix + "%' and Level = " + level;
                DataTable saccounts = fun.GetData(select);
                return ConvertDT<SubAccountManul>(saccounts);
            }
            public List<SubAccountManul> GetAllSubNotKhazna()
            {
                string select = "select * from SubAccount where MainAccount_id!=1103";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Saccounts);
            }
            public List<SubAccountManul> GetAllNotClientsOrSuppliers()//SubNotKhazna()
            {
                string select = "select * from SubAccount where MainAccount_id!=1101 and MainAccount_id!=2103";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Saccounts);
            }
            public List<SubAccountManul> GetAllSubNotIncomsCheq()
            {
                string select = "select * from SubAccount where MainAccount_id!=1121";
                DataTable Saccounts = fun.GetData(select);

                return ConvertDT<SubAccountManul>(Saccounts);
            }
            public List<SubAccountManul> GetAllSubNotOutcomsCheq()
            {
                string select = "select * from SubAccount where MainAccount_id!=1122";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Saccounts);
            }
            public List<SubAccountManul> GetSubAccount_ByID(int Account_ID)
            {
                string select = "select * from SubAccount where id=" + Account_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Account);
            }
            public List<SubAccountManul> GetSubAccount_ByName(string acc_name)
            {
                string select = "select * from SubAccount where name like N'%" + acc_name + "%'";
                DataTable Account = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Account);
            }
            public List<SubAccountManul> GetSubAccount_ByNameAndByUpAccount(string acc_name, int upAccountID)
            {
                string select = "select * from SubAccount where name like N'%" + acc_name + "%' and MainAccount_id=" + upAccountID;
                DataTable Account = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Account);
            }
            public List<SubAccountManul> GetSubAccount_ByUpID(int UpAccount_ID)
            {
                string select = "select * from SubAccount where UpAccount=" + UpAccount_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Account);
            }
            public List<SubAccountManul> GetSubAccount_ByLevelID(int Level_ID)
            {
                string select = "select * from SubAccount where level=" + Level_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<SubAccountManul>(Account);
            }

            /////////////
            public object Get_Filter(SubAccountManul subAccount)
            {
                string strtemp = "";
                strtemp += (subAccount.ID != 0) ? subAccount.ID.ToString() + "," : "NULL,";
                strtemp += (subAccount.name != null) ? "'" + subAccount.name.ToString() + "'," : "NULL,";
                strtemp += (subAccount.UpAccount != null) ? subAccount.UpAccount.ToString() + "," : "NULL,";
                strtemp += (subAccount.Level != 0) ? subAccount.Level.ToString() + "," : "NULL,";
                strtemp += (subAccount.ABalance != 0) ? subAccount.ABalance.ToString() + "," : "NULL,";
                strtemp += (subAccount.BType != null) ? "'" + subAccount.BType.ToString() + "'," : "NULL,";
                strtemp += (subAccount.RegisterDate != Convert.ToDateTime("0001-01-01T00:00:00")) ? "'" + subAccount.RegisterDate.ToString() + "'," : "NULL,";
                strtemp += (subAccount.user_id != 0) ? subAccount.user_id.ToString() + "," : "NULL,";
                strtemp += (subAccount.MainAccount_id != 0) ? subAccount.MainAccount_id.ToString() : "NULL";
                if (subAccount != null)
                {
                    string getall = "Execute SP_SubAccountFilter " + strtemp;
                    DataTable allSubAccountFilter = fun.GetData(getall);
                    if (allSubAccountFilter != null)
                        return ConvertDT<SubAccountManul>(allSubAccountFilter);
                    return 1008;
                }
                return 1008;
            }
            public object Get_AllSubAccount()
            {

                string getall = "select * from SubAccount";
                DataTable allSubAccount = fun.GetData(getall);
                if (allSubAccount != null)
                    return ConvertDT<SubAccountManul>(allSubAccount);
                return 1008;
            }
            public object Get_SubAccountByID(int ID)
            {
                string getall = "select * from SubAccount where ID=" + ID;
                DataTable allSubAccount = fun.GetData(getall);
                if (allSubAccount != null)
                    return ConvertDT<SubAccountManul>(allSubAccount).ToList().FirstOrDefault();
                else return 1008;
            }
            public object Get_SubAccountsByName(string SubAccount_Name)
            {
                string getall = "select * from SubAccount where name=N'%" + SubAccount_Name + "%'";
                DataTable AllSubAccount = fun.GetData(getall);
                if (AllSubAccount != null)
                    return ConvertDT<SubAccountManul>(AllSubAccount);
                else return 1008;
            }
            public object Get_AllSubAccountByUserID(int User_ID)
            {
                string getall = "select * from SubAccount where user_id=" + User_ID;
                DataTable allSubAccount = fun.GetData(getall);
                if (allSubAccount != null)
                    return ConvertDT<SubAccountManul>(allSubAccount);
                else return 1008;
            }
            public object Get_AllSubAccountByMainAccount(int MainAccount)
            {
                string getall = "select * from SubAccount where MainAccount_id=" + MainAccount;
                DataTable allSubAccount = fun.GetData(getall);
                if (allSubAccount != null)
                    return ConvertDT<SubAccountManul>(allSubAccount);
                else return 1008;
            }

        }
        public class Levels
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            public int ID { set; get; }
            public string LName { set; get; }

            public List<Levels> levels()
            {
                string sel = "select * from levels";
                DataTable dt = fun.GetData(sel);
                return ConvertDT<Levels>(dt);
            }
        }
        public class SAccounts
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            public int id { set; get; }
            public string SNAme { set; get; }
            public int LID { set; get; }
            public List<SAccounts> Basic_accounts()
            {
                string sel = "select * from saccounts";
                DataTable dt = fun.GetData(sel);
                return ConvertDT<SAccounts>(dt);
            }
        }
        public class Indx
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            //private object  prod;
            private List<Indx> po;

            public int ID { set; get; }
            public string ClientCategory { set; get; }
            public string ClientType { set; get; }
            public int? SalesUesrId { set; get; }
            public string ResponsiblePersonName { set; get; }
            public string ResponsiblePersonPhone { set; get; }
            public string AnotherResponsiblePersonPhone { set; get; }
            public string Email { set; get; }
            public string Address { set; get; }
            public string Sgl_TaxNO { set; get; }
            public string CommercialDocument { set; get; }
            public string TaxDocument { set; get; }
            public long? Sub_ID { set; get; }

            //ChartOfAccount
            public string Maamria { set; get; }
            public string MobileNo { set; get; }
            public string PersonalID { set; get; }

            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    po = (List<Indx>)content;
                    //string con = $"{FieldsName.ElementAt(0).Key} + {FieldsName.ElementAt(1).Key} +{FieldsName.ElementAt(2).Key } ";
                    if (po.Count > 0)
                    {
                        foreach (var it in po)
                        {
                           var saleuserid = (it.SalesUesrId != null) ? it.SalesUesrId.ToString() : "NULL";

                            string tes = @"insert  into Indx([ClientCategory],[ClientType],[SalesUesrId],[ResponsiblePersonName],[ResponsiblePersonPhone],[AnotherResponsiblePersonPhone]
                          ,[Email],[Address],[Sgl_TaxNO],[CommercialDocument],[TaxDocument],[Sub_ID],[Maamria] ,[MobileNo]
                          ,[PersonalID])values(N'" + it.ClientCategory + "',N'" + it.ClientType + "'," + saleuserid
                          + ",N'" + it.ResponsiblePersonName + "',N'" + it.ResponsiblePersonPhone + "',N'" + it.AnotherResponsiblePersonPhone
                          + "',N'" + it.Email + "',N'" + it.Address + "',N'" + it.Sgl_TaxNO +"',N'" + it.CommercialDocument + "',N'"
                          + it.TaxDocument + "'," + it.Sub_ID + ",N'" + it.Maamria +
                         "',N'" + it.MobileNo +"',N'" + it.PersonalID + "')";

                          fun.FireSql(tes);
                        }
                        return "Added Successfully";
                    }
                }
                else if (method == "Edit")
                {
                    po = (List<Indx>)content;
                    if (po.Count > 0)
                    {
                        string update;
                        foreach (var row in po)
                        {
                            DataTable dt = fun.GetData("select * from Indx where ID=" + row.ID + "");
                            if (dt.Rows.Count == 0)
                            {
                                string tes = @"insert  into Indx([ClientCategory],[ClientType],[SalesUesrId],[ResponsiblePersonName],[ResponsiblePersonPhone],[AnotherResponsiblePersonPhone]
                                ,[Email],[Address],[Sgl_TaxNO],[CommercialDocument],[TaxDocument],[Sub_ID],[Maamria] ,[MobileNo]
                                ,[PersonalID])values(N'" + row.ClientCategory + "',N'" + row.ClientType + "'," + row.SalesUesrId
                                 + ",N'" + row.ResponsiblePersonName + "',N'" + row.ResponsiblePersonPhone + "',N'" + row.AnotherResponsiblePersonPhone
                                 + "',N'" + row.Email + "',N'" + row.Address + "',N'" + row.Sgl_TaxNO + "',N'" + row.CommercialDocument + "',N'"
                                 + row.TaxDocument + "'," + row.Sub_ID + ",N'" + row.Maamria +
                                "',N'" + row.MobileNo + "',N'" + row.PersonalID + "')";

                                fun.FireSql(tes);
                            }
                            else
                            {
                                update = "update Indx set ClientCategory=N'" + row.ClientCategory + "',ClientType=N'" + row.ClientType + ",SalesUesrId=" + row.SalesUesrId
                                   + ",ResponsiblePersonName=N'"+ row.ResponsiblePersonName + "',ResponsiblePersonPhone=N'" + row.ResponsiblePersonPhone
                                   + "',AnotherResponsiblePersonPhone=N'" + row.AnotherResponsiblePersonPhone + "',Email=N'" + row.Email
                                   + "',Address=N'" + row.Address + "',Sgl_TaxNO=N'"+ row.Sgl_TaxNO + "',CommercialDocument=N'" + row.CommercialDocument
                                   + "',TaxDocument=N'" + row.TaxDocument + "',Sub_ID=" + row.Sub_ID + ",Maamria=N'" + row.Maamria + "',MobileNo=N'" + row.MobileNo 
                                   +"',PersonalID=N'" + row.PersonalID + "' where ID=" + row.ID + "";
                                fun.FireSql(update);
                            }
                        }
                        return "Updated Successfully";
                    }
                }
                else if (method == "Delete")
                {
                    po = (List<Indx>)content;
                    if (po.Count > 0)
                    {
                        fun.FireSql("delete from Indx where ID=" + po.First().ID + "");
                        return "Deleted Successfully";
                    }
                }
                return "No Action";
            }
            public List<Indx> GetAllIndx()
            {
                string select = "select * from Indx";
                DataTable Maccounts = fun.GetData(select);
                return ConvertDT<Indx>(Maccounts);
            }
            public List<Indx> GetIndxByID(int Account_ID)
            {
                string select = "select * from Indx where ID=" + Account_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<Indx>(Account);
            }
            public List<Indx> GetIndx_BySubID(Int64 Sub_ID)
            {
                string select = "select * from Indx where Sub_ID=" + Sub_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<Indx>(Account);
            }
        }
        public class BankMovedManul
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            private List<BankMovedManul> po;
            public int ID { set; get; }
            public bool state { set; get; }
            public Decimal Value { set; get; }
            public Int64 AccountID { set; get; }
            public string Description { set; get; }
            public string ChequeNo { set; get; }
            public string BankName { set; get; }
            public DateTime Date { set; get; }
            public DateTime? SarfDate { set; get; }
            public bool EntryState { set; get; }//New
            public int EntryID { set; get; }//New
            public Int64 DocID { set; get; }//New
            public string FinancialPostitionType { set; get; }//New
            public int FinancialPostitionId { set; get; }//New
            public MainAccount accounts { set; get; }
            public SubAccountManul subAccounts { set; get; }
            string date1, date2;

            public int MoveID = 1;
            public void maxid(int _state)
            {
                try
                {
                    MoveID = int.Parse(fun.FireSql("select max(id) from BankMoved where state=" + _state + "").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    MoveID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    po = (List<BankMovedManul>)content;
                    //string con = $"{FieldsName.ElementAt(0).Key} + {FieldsName.ElementAt(1).Key} +{FieldsName.ElementAt(2).Key } ";
                    if (po.Count > 0)
                    {
                        foreach (var it in po)
                        {
                            int ii = 0;
                            int Entry_State = 0;
                            if (it.state == true)
                            {
                                ii = 1;
                            }
                            if (it.EntryState == true)
                            {
                                Entry_State = 1;
                            }
                            it.Date = DateTime.Parse(it.Date.ToString("yyyy-MM-dd"));
                            date1 = ConvertDate(it.Date.ToString());
                            if (it.SarfDate == null)
                            {
                                date2 = date1;
                            }
                            else
                                date2 = ConvertDate(it.SarfDate.ToString()).ToString();

                            string test = "insert  into BankMoved(ID,state,value,AccountID,[Description],ChequeNo,BankName,Date,SarfDateEntryState,EntryID,DocID)values(" + MoveID + "," + ii +
                                "," + it.Value + "," + it.AccountID + ",N'" + it.Description + "',N'" + it.ChequeNo + "',N'" + it.BankName + "'," + date1 + "," + date2 + "," + DocID + ")";
                            it.maxid(ii);
                            it.ID = it.MoveID;

                            fun.FireSql("insert  into BankMoved(ID,state,value,AccountID,[Description],ChequeNo,BankName,Date,SarfDate,EntryState,EntryID,DocID,[FinancialPostitionType],[FinancialPostitionId])values(" 
                                + it.ID + "," + ii +"," + it.Value + "," + it.AccountID + ",N'" + it.Description + "',N'" + it.ChequeNo 
                                + "',N'" + it.BankName + "'," + date1 +"," + date2 + "," + Entry_State + "," + it.EntryID + "," 
                                + it.DocID + ",N'" + it.FinancialPostitionType + "'," + it.FinancialPostitionId + ")");
                        }
                        return "Added Successfully";
                    }
                }
                else if (method == "Edit")
                {
                    po = (List<BankMovedManul>)content;
                    if (po.Count > 0)
                    {
                        string update;
                        foreach (var row in po)
                        {
                            int ii = 0, Entry_State = 0;
                            if (row.state == true)
                            {
                                ii = 1;
                            }
                            if (row.EntryState == true)
                            {
                                Entry_State = 1;
                            }

                            date1 = ConvertDate(row.Date.ToString());
                            if (row.SarfDate == null)
                            {
                                date2 = date1;
                                row.SarfDate = Date;
                            }
                            else
                                date2 = ConvertDate(row.SarfDate.ToString()).ToString();

                            update = "update BankMoved set ID=" + row.ID + ", state=" + ii + ",value=" + row.Value +
                                 ",AccountID=" + row.AccountID + ",[Description]=N'" + row.Description + "',ChequeNo=N'" +
                                 row.ChequeNo + "',BankName=N'" + row.BankName + "',Date=" + date1 + ",SarfDate=" +
                                 ConvertDate(row.SarfDate.ToString()) + ",DocID=" + row.DocID + ",EntryState=" + Entry_State 
                                 + ",EntryID=" + row.EntryID + ",FinancialPostitionType=N'" + row.FinancialPostitionType 
                                 + "',FinancialPostitionId=" + row.FinancialPostitionId + " where id=" + row.ID + " and state=" + ii + "and DocID=" + row.DocID + "";
                            fun.FireSql(update);
                        }
                        return "Updated Successfully";
                    }
                }
                else if (method == "Delete")
                {
                    po = (List<BankMovedManul>)content;
                    if (po.Count > 0)
                    {
                        int ii = 0;
                        if (po.First().state == true)
                        {
                            ii = 1;
                        }
                        fun.FireSql("delete from BankMoved where id=" + po.First().ID + " and state=" + ii + "");
                        return "Deleted Successfully";
                    }
                }
                return "No Action";
            }
            public List<BankMovedManul> GetAllBankMoved()
            {
                string select = "select * from BankMoved";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Saccounts);
            }
            public List<BankMovedManul> GetBankMoved_ByAccountID(int Account_ID)
            {
                string select = "select * from BankMoved where AccountId=" + Account_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Account);
            }
            public List<BankMovedManul> GetAllBankMovedByEntryIDAndState(int Entry, int state)
            {
                string select = "select * from BankMoved where [EntryID]=" + Entry + " and state=" + state;
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Saccounts);
            }
            public List<BankMovedManul> GetBankMoving_ByID(int TransID, int state)
            {
                string select = "select * from BankMoved where id=" + TransID + " and state=" + state + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Account);
            }

            public List<BankMovedManul> GetBankID_state(int state)
            {
                string select = "select * from BankMoved where state=" + state + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Account);
            }

            public List<BankMovedManul> BankMoved_Between_Date(string fromdate, string todate)
            {

                string select = "select * from KhznaMoved where Date >= " + ConvertDate(fromdate) + "and Date <= " + ConvertDate(todate) + " order by date";
                DataTable Account = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Account);
            }

            public List<BankMovedManul> BankMoved_Between_Date_ForAccount(string fromdate, string todate, int account_id)
            {
                string select = "select * from BankMoved where AccountId=" + account_id + " and Date >= " + ConvertDate(fromdate) + "and Date <= " + ConvertDate(todate) + " order by date";
                DataTable Account = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Account);
            }

            //«—Ã«⁄ ﬂ· «·⁄„·Ì«  «·Œ«’… »Œ“‰… „⁄Ì‰…
            public List<BankMovedManul> GetAllBankMovedByKhaznaIDAndState(int KhaznaID, int state)
            {
                string select = "select * from BankMoved where [DocID]=" + KhaznaID + " and state=" + state;
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Saccounts);
            }
            public List<BankMovedManul> GetAllBankMovedByBankIDAndStateAndDate(int KhaznaID, int state, string Date)
            {
                string select = "select * from BankMoved where [DocID]=" + KhaznaID +
                    " and state=" + state + " and [Date]=" + ConvertDate(Date) + "";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<BankMovedManul>(Saccounts);
            }

        }
        public class EntryManul
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            private List<EntryManul> po;
            public int ID { set; get; }
            public string status { set; get; }// œ· ⁄·Ï «·„œÌ‰0 Ê«·œ«Ì‰1
            public Decimal value { set; get; }
            public string description { set; get; }

            public DateTime Date { set; get; }
            public Int64 SubAccount_id { set; get; }//—ﬁ„ «·Œ“‰… [TreasuryID]

            public MainAccount accounts { set; get; }
            public SubAccountManul subAccounts { set; get; }
            public int EntryID { set; get; }
            public int RecordID { set; get; }
            public string EntryType { set; get; }
            string date1;


            public int MoveID = 1;
            public void maxid()
            {
                try
                {
                    MoveID = int.Parse(fun.FireSql("select max([EntryID]),[status] from Entry group by [status]").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    MoveID = 1;
                }

            }
            public void maxid(string state)
            {
                try
                {
                    RecordID = int.Parse(fun.FireSql("select max([EntryID]) from Entry where [status]='" + state + "'").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    RecordID = 1;
                }

            }
            public void maxRecordID(int Entry_ID)
            {
                try
                {
                    RecordID = int.Parse(fun.FireSql("select max([RecordID]) from Entry  where EntryID=" + Entry_ID).ToString()) + 1;
                }
                catch (Exception ex)
                {
                    RecordID = 1;
                }
            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    po = (List<EntryManul>)content;
                    //string con = $"{FieldsName.ElementAt(0).Key} + {FieldsName.ElementAt(1).Key} +{FieldsName.ElementAt(2).Key } ";
                    if (po.Count > 0)
                    {
                        foreach (var it in po)
                        {
                            //int ii = 0;
                            //if (it.status == true)
                            //{
                            //    ii = 1;
                            //}

                            date1 = ConvertDate(it.Date.ToString());
                            string test = "insert  into Entry(ID,[status],Value,SubAccount_id,[description],Date)values(" + it.ID + ",'" + it.status + "'," +
                                it.value + "," + it.SubAccount_id + ",N'" + it.description + "'," + date1 + ")";
                                fun.FireSql(@"insert  into Entry([status],Value,SubAccount_id,[description],Date,[EntryID],RecordID,EntryType)values('" + it.status + "'," +
                                    it.value + "," + it.SubAccount_id + ",N'" + it.description + "'," + date1.ToString() + "," + it.EntryID + "," + it.RecordID + ",'" + it.EntryType + "')");
                            
                            }
                        return "Added Successfully";
                    }
                }
                else if (method == "Edit")
                {
                    po = (List<EntryManul>)content;
                    if (po.Count > 0)
                    {
                        string update;
                        foreach (var row in po)
                        {
                            //int ii = 0;
                            //if (row.status == true)
                            //{
                            //    ii = 1;
                            //}
                            string select = "select * from Entry where [EntryID]=" + row.EntryID + " and [status]='" + row.status + "' and SubAccount_id=" +
                                row.SubAccount_id + " and RecordID=" + row.RecordID;

                            DataTable dt = fun.GetData(select);
                            if (dt.Rows.Count == 0)
                            {
                                date1 = ConvertDate(row.Date.ToString());

                                fun.FireSql(@"insert  into Entry([status],Value,SubAccount_id,[description],Date,[EntryID],RecordID,EntryType)values('" + row.status + "'," +
                                                                row.value + "," + row.SubAccount_id + ",N'" + row.description + "'," + date1.ToString() + "," + row.EntryID + "," + row.RecordID + ",N'" + row.EntryType + "')");
                            }
                            else
                            {
                                date1 = ConvertDate(row.Date.ToString());
                                update = "update Entry set  status='" + row.status + "',value=" + row.value + ",[SubAccount_id]=" + row.SubAccount_id + ",description=N'" +
                                    row.description + "',Date=" + date1.ToString() + ",RecordID=" + row.RecordID + ",EntryType=N'" + row.EntryType + "' where EntryID=" + row.EntryID +
                                    " and status='" + row.status + "' and RecordID=" + row.RecordID + " and SubAccount_id=" + row.SubAccount_id;
                                fun.FireSql(update);
                            }
                        }
                        return "Updated Successfully";
                    }
                }
                else if (method == "Delete")
                {
                    po = (List<EntryManul>)content;
                    if (po.Count > 0)
                    {
                        //int ii = 0;
                        //if (po.First().status == true)
                        //{
                        //    ii = 1;
                        //}
                        fun.FireSql("delete from Entry where EntryID=" + po.First().EntryID);//[RecordID]=" + po.First().RecordID + " and status='" + po.First().status + "' and SubAccount_id="+po.First().SubAccount_id+" and EntryID="+po.First().EntryID);
                        return "Deleted Successfully";
                    }
                }
                return "No Action";
            }
            public List<EntryManul> GetAllEntry()
            {
                string select = "select * from Entry order by status  desc";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<EntryManul>(Saccounts);
            }
            public List<EntryManul> GetAllEntryID()
            {
                string select = "select DISTINCT(EntryID) from [Entry] ";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<EntryManul>(Saccounts);
            }
            public List<EntryManul> GetEntry_ByAccountIDByDate(Int64 AccountID, string ToDate)
            {
                string select = "select * from Entry where SubAccount_id Like '%" + AccountID + "%' and Date<=" + ConvertDate(ToDate).ToString() + " order by status  desc";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<EntryManul>(Saccounts);
            }

            public List<EntryManul> GetAllEntryByID(int EntryID)
            {
                string select = "select * from Entry where EntryID=" + EntryID + " order by status  desc";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<EntryManul>(Saccounts);
            }
            public List<EntryManul> GetAllEntryIDAndState(int EntryID, int state)
            {
                string select = "select * from Entry where [EntryID]=" + EntryID + " and [status]='" + state + "' order by status  desc";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<EntryManul>(Saccounts);
            }
            public List<EntryManul> GetAllEntryByIDAndStateAndDate(int EntryID, int state, string Date)
            {
                string select = "select * from Entry where [EntryID]=" + EntryID +
                    " and [status]='" + state + "' and [Date]=" + ConvertDate(Date).ToString() + " order by status  desc";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<EntryManul>(Saccounts);
            }

            public List<EntryManul> GetAllEntryByRecIDAndStateAndAccountID(int AccountID, int RecordID, int EntryID, string state)
            {
                string select = "select * from Entry where [EntryID]=" + EntryID + " and [status]='" + state + "' and SubAccount_id=" + AccountID + " order by status  desc";

                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<EntryManul>(Saccounts);
            }

            public List<EntryManul> GetAllEntryByRecIDAndAccountID(int AccountID, int RecordID, int EntryID)
            {
                string select = "select * from Entry where [EntryID]=" + EntryID + " and SubAccount_id=" + AccountID + " order by status  desc";

                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<EntryManul>(Saccounts);
            }

            public List<EntryManul> GetEntry_ByAccountID(int Account_ID)
            {
                string select = "select * from Entry where [SubAccount_id]=" + Account_ID + " order by status  desc";
                DataTable Account = fun.GetData(select);
                return ConvertDT<EntryManul>(Account);
            }
            public List<EntryManul> GetEntry_ByAccountIDByDate(int Account_ID, string FromDate, string ToDate)
            {
                string select = "select * from Entry where [SubAccount_id]=" + Account_ID + " and Date >= " + ConvertDate(FromDate).ToString() + "and Date <= " + ConvertDate(ToDate).ToString() + " order by status  desc";
                DataTable Account = fun.GetData(select);
                return ConvertDT<EntryManul>(Account);
            }

            public List<EntryManul> GetEntry_ByID(int TransID, int state)
            {
                string select = "select * from Entry where EntryID=" + TransID + " and status=" + state + " order by status  desc";
                DataTable Account = fun.GetData(select);
                return ConvertDT<EntryManul>(Account);
            }

            public List<EntryManul> GetEntryBystate(int state)
            {
                string select = "select * from Entry where status='" + state + "'  order by status  desc";
                DataTable Account = fun.GetData(select);
                return ConvertDT<EntryManul>(Account);
            }

            public List<EntryManul> Entry_Between_Date(string fromdate, string todate)
            {
                DateTime date1 = DateTime.Parse(ConvertDate(fromdate).ToString());
                DateTime date2 = DateTime.Parse(ConvertDate(todate).ToString());
                string select = "select * from Entry where Date>=" + date1.ToString() + " and Date<=" + date2.ToString() + " order by Date";
                DataTable Account = fun.GetData(select);
                return ConvertDT<EntryManul>(Account);
            }
            public List<EntryManul> KhznaMoved_Between_Date_ForAccount(string fromdate, string todate, int account_id)
            {
                DateTime date1 = DateTime.Parse(ConvertDate(fromdate).ToString());
                DateTime date2 = DateTime.Parse(ConvertDate(todate).ToString());
                string select = "select * from Entry where Date>=" + date1.ToString() + " and Date<=" + date2.ToString() + " and [SubAccount_id]=" + account_id + " order by Date";
                DataTable Account = fun.GetData(select);
                return ConvertDT<EntryManul>(Account);
            }



        }
        public class KhznaMovedManul
        {
            private ConnectDataBase.ConnectFunction fun = new ConnectDataBase.ConnectFunction();
            private List<KhznaMovedManul> po;
            public int ID { set; get; }
            public bool state { set; get; }
            public Decimal Value { set; get; }
            public Int64 AccountID { set; get; }
            public string Description { set; get; }
            public DateTime Date { set; get; }
            public bool EntryState { set; get; }//New
            public int EntryID { set; get; }//New
            public Int64 TreasuryID { set; get; }//—ﬁ„ «·Œ“‰… [TreasuryID]
            public string FinancialPostitionType { set; get; }
            public int FinancialPostitionId { set; get; }//New

            public MainAccount accounts { set; get; }
            public SubAccountManul subAccounts { set; get; }

            string date1;

            public int MoveID = 1;
            public void maxid()
            {
                try
                {
                    int _state = 0;
                    _state = state == true ? 1 : 0;
                    MoveID = int.Parse(fun.FireSql("select max(ID) from KhznaMoved where state="+ _state).ToString()) + 1;
                }
                catch (Exception ex)
                {
                    MoveID = 1;
                }

            }
            public void maxid(int state)
            {
                try
                {
                    MoveID = int.Parse(fun.FireSql("select max(ID) from KhznaMoved where state=" + state + "").ToString()) + 1;
                }
                catch (Exception ex)
                {
                    MoveID = 1;
                }

            }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    po = (List<KhznaMovedManul>)content;
                    //string con = $"{FieldsName.ElementAt(0).Key} + {FieldsName.ElementAt(1).Key} +{FieldsName.ElementAt(2).Key } ";
                    if (po.Count > 0)
                    {
                        foreach (var it in po)
                        {
                            int ii = 0;
                            int Entry_State = 0;
                            if (it.state == true)
                            {
                                ii = 1;
                            }
                            if (it.EntryState == true)
                            {
                                Entry_State = 1;
                            }
                            it.Date = DateTime.Parse(it.Date.ToString("yyyy-MM-dd"));
                            date1 = ConvertDate(it.Date.ToString());
                            string test = "insert  into KhznaMoved(ID,state,value,AccountID,[Description],Date,TreasuryID,[FinancialPostitionType],[FinancialPostitionId])values(" + MoveID + "," + ii + "," +
                                it.Value + "," + it.AccountID + ",N'" + it.Description + "'," + date1 + ",TreasuryID)";
                            try
                            {
                                it.maxid();
                                it.ID = it.MoveID;
                                fun.FireSql("insert  into KhznaMoved(ID,state,value,AccountID,[Description],Date,EntryState,EntryID,TreasuryID,[FinancialPostitionType],[FinancialPostitionId])values(" 
                                    + it.ID + "," + ii + "," +it.Value + "," + it.AccountID + ",N'" + it.Description + "'," + date1 
                                    + "," + Entry_State + "," + it.EntryID + "," + it.TreasuryID + ",N'" + it.FinancialPostitionType + "'," + it.FinancialPostitionId + ")");
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        return "Added Successfully";
                    }
                }
                else if (method == "Edit")
                {
                    po = (List<KhznaMovedManul>)content;
                    if (po.Count > 0)
                    {
                        string update;
                        foreach (var row in po)
                        {
                            int ii = 0;
                            int Entry_State = 0;
                            if (row.state == true)
                            {
                                ii = 1;
                            }
                            if (row.EntryState == true)
                            {
                                Entry_State = 1;
                            }
                            date1 = ConvertDate(row.Date.ToString());
                            DataTable dt = fun.GetData("select * from KhznaMoved where id=" + row.ID + " and state=" + ii + "");
                            if (dt.Rows.Count == 0)
                            {
                                fun.FireSql("insert  into KhznaMoved(ID,state,value,AccountID,Description,Date,EntryState,EntryID,TreasuryID)values(" +
                                   row.ID + "," + ii + "," + row.Value + "," + row.AccountID + ",N'" + row.Description + "'," + date1 +
                                    "," + Entry_State + "," + row.EntryID + "," + row.TreasuryID + ")");
                            }
                            else
                            {
                                update = "update KhznaMoved set ID=" + row.ID + ", state=" + ii + ",value=" + row.Value 
                                    + ",AccountID=" + row.AccountID + ",Description=N'" +row.Description 
                                    + "',Date=" + date1 + ",EntryState=" 
                                    + Entry_State + ",EntryID=" + row.EntryID + ",TreasuryID=" + row.TreasuryID +
                                    ",FinancialPostitionType=N'"+ row.FinancialPostitionType 
                                    +"',FinancialPostitionId=" + row.FinancialPostitionId + " where id=" + row.ID + " and state=" + ii + "";
                                fun.FireSql(update);
                            }

                        }
                        return "Updated Successfully";
                    }
                }
                else if (method == "Delete")
                {
                    po = (List<KhznaMovedManul>)content;
                    if (po.Count > 0)
                    {
                        int ii = 0;
                        if (po.First().state == true)
                        {
                            ii = 1;
                        }
                        fun.FireSql("delete from KhznaMoved where ID=" + po.First().ID + " and state=" + ii + "");
                        return "Deleted Successfully";
                    }
                }
                return "No Action";
            }
            public List<KhznaMovedManul> GetAllKhznaMoved()
            {
                string select = "select * from KhznaMoved";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Saccounts);
            }
            //«—Ã«⁄ ﬂ· «·⁄„·Ì«  «·Œ«’… »Œ“‰… „⁄Ì‰…
            public List<KhznaMovedManul> GetAllKhznaMovedByKhaznaIDAndState(int KhaznaID, int state)
            {
                string select = "select * from KhznaMoved where [TreasuryID]=" + KhaznaID + " and state=" + state;
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Saccounts);
            }

            public List<KhznaMovedManul> GetAllKhznaMovedByEntryIDAndState(int Entry, int state)
            {
                string select = "select * from KhznaMoved where [EntryID]=" + Entry + " and state=" + state;
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Saccounts);
            }
            public List<KhznaMovedManul> GetAllKhznaMovedByKhaznaIDAndStateAndDate(int KhaznaID, int state, string Date)
            {
                string select = "select * from KhznaMoved where [TreasuryID]=" + KhaznaID +
                    " and state=" + state + " and [Date]=" + ConvertDate(DateTime.Parse(Date.ToString(), CultureInfo.CreateSpecificCulture("ar-EG")).ToString()) + "";
                DataTable Saccounts = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Saccounts);
            }

            public double getVAlueSum(int KhaznaID, int state)
            {
                string select = @"SELECT sum([Value])
  FROM[EPE].[dbo].[KhznaMoved]
  where TreasuryID =" + KhaznaID + "and state =" + state + "";
                var Saccounts = fun.FireSql(select);
                return double.Parse(Saccounts.ToString());
            }
            public List<KhznaMovedManul> GetKhznaMoved_ByAccountID(int Account_ID)
            {
                string select = "select * from KhznaMoved where AccountId=" + Account_ID + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Account);
            }
            public List<KhznaMovedManul> GetKhznaMoved_ByAccountIDByDate(int Account_ID, string FromDate, string ToDate)
            {
                string select = "select * from KhznaMoved where AccountId=" + Account_ID + " and Date >= " + ConvertDate(FromDate) + "and Date <= " + ConvertDate(ToDate) + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Account);
            }

            public List<KhznaMovedManul> GetKhznaMoved_ByID(int TransID, int state)
            {
                string select = "select * from KhznaMoved where ID=" + TransID + " and state=" + state + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Account);
            }

            public List<KhznaMovedManul> GetKhznaMovedBystate(int state)
            {
                string select = "select * from KhznaMoved where state=" + state + "";
                DataTable Account = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Account);
            }

            public List<KhznaMovedManul> KhznaMoved_Between_Date(string fromdate, string todate)
            {
                DateTime date1 = DateTime.Parse(ConvertDate(fromdate).ToString());
                DateTime date2 = DateTime.Parse(ConvertDate(todate).ToString());
                string select = "select * from KhznaMoved where Date>=" + date1 + " and Date<=" + date2 + " order by Date";
                DataTable Account = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Account);
            }
            public List<KhznaMovedManul> KhznaMoved_Between_Date_ForAccount(string fromdate, string todate, int account_id)
            {
                DateTime date1 = DateTime.Parse(ConvertDate(fromdate).ToString());
                DateTime date2 = DateTime.Parse(ConvertDate(todate).ToString());
                string select = "select * from KhznaMoved where Date>=" + date1 + " and Date<=" + date2 + " and AccountId=" + account_id + " order by Date";
                DataTable Account = fun.GetData(select);
                return ConvertDT<KhznaMovedManul>(Account);
            }
        }
        public class PurchaseInvoiceManual
        {
            private ConnectFunction fun = new ConnectFunction();
            //private object  prod;
            private List<PurchaseInvoiceManual> po;

            public string Id { set; get; }
            public string InvoiceDate { set; get; }
            public int UserID { set; get; }
            public Double Total { set; get; }
            public Double PaymentValue { set; get; }
            public bool PurchaseType { set; get; }
            public Int64 SubAccountId { set; get; }
            public int KeadNo { set; get; }
            public string PaymentMethod { set; get; }
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    po = (List<PurchaseInvoiceManual>)content;
                    //string con = $"{FieldsName.ElementAt(0).Key} + {FieldsName.ElementAt(1).Key} +{FieldsName.ElementAt(2).Key } ";
                    if (po.Count > 0)
                    {
                        int dtype, comtype, ttype, tstate, isimport;
                        foreach (var it in po)
                        {
                            
                            try
                            {

                                string ssa = @"insert  into PurchaseInvoice([Id]
           ,[InvoiceDate]
           ,[SubAccountId]
           ,[PurchaseType]
           ,[Total]
           ,[UserID]
           ,[PaymentMethod]
           ,[PaymentValue]
           ,[KeadNo])values(N'" + it.Id + "',N'" +
                                   /*FormatDate(it.date)*/ it.InvoiceDate.ToString() + "'," +
                                   it.SubAccountId + "," + Convert.ToInt32(it.PurchaseType)+ "," + it.Total +
                                    "," + it.UserID + ",N'" + it.PaymentMethod + "'," + it.PaymentValue +
                                    "," + it.KeadNo+ "')";

                                    fun.FireSql(ssa);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        return "Added Successfully";
                    }
                }
                else if (method == "Edit")
                {
                    po = (List<PurchaseInvoiceManual>)content;
                    if (po.Count > 0)
                    {
                        foreach (var row in po)
                        {
                             string   update = @"update PurchaseInvoice SET [Id] =N'"+row.Id+@"'
      ,[InvoiceDate] = "+row.InvoiceDate.ToString()+ @"
      ,[SubAccountId] ="+row.SubAccountId+@"
      ,[PurchaseType] ="+Convert.ToInt32(row.PurchaseType)+@"
      ,[Total] ="+row.Total+@"
      ,[UserID] = "+row.UserID+@"
      ,[PaymentMethod] = N'"+row.PaymentMethod+@"'
      ,[PaymentValue] = "+row.PaymentValue+@"
      ,[KeadNo] ="+row.KeadNo+" where id=" + row.Id + " and PurchaseType=" + Convert.ToInt32( row.PurchaseType) + "";
                            try
                            {
                                fun.FireSql(update);
                            }
                            catch (Exception ex)
                            {
                                string cs = ex.Message;
                            }
                        }
                        return "Updated Successfully";
                    }
                }
                else if (method == "Delete")
                {
                    po = (List<PurchaseInvoiceManual>)content;
                    if (po.Count > 0)
                    {
                        fun.FireSql("delete from PurchaseInvoice where id=N'" + po.First().Id + "' and PurchaseType=" +Convert.ToInt32(po.First().PurchaseType )+ "");
                        return "Deleted Successfully";
                    }
                }
                return "No Action";
            }
            public List<PurchaseInvoiceManual> GetAllPurchaseInvoice()
            {
                string select = "select * from PurchaseInvoice";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder);
            }
           
            public List<PurchaseInvoiceManual> GetAllPurchaseInvoiceByEntrID(int EntryID)
            {
                string select = "select * from PurchaseInvoice where KeadNo=" + EntryID;
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder);
            }

            public List<PurchaseInvoiceManual> GetAllPurchaseInvoice(string fromdate, string todate)
            {
                
                string select = "select * from PurchaseInvoice where InvoiceDate>=" + ConvertDate(fromdate).ToString() + " and InvoiceDate <=" + ConvertDate(todate).ToString();
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder);
            }

            public List<PurchaseInvoiceManual> GetAllPurchaseInvoice(string fromdate, string todate, bool Type)
            {
                string select = "select * from PurchaseInvoice where PurchaseType=" + Convert.ToInt32(Type) + " and InvoiceDate>=" + ConvertDate(fromdate).ToString() + " and InvoiceDate <=" + ConvertDate(todate).ToString();
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder);
            }
            public List<PurchaseInvoiceManual> GetAllPurchaseInvoiceInDate(string InDate, bool Type)
            {
                string select = "select * from PurchaseInvoice where PurchaseType=" + Convert.ToInt32(Type) + " and InvoiceDate=" + ConvertDate(InDate).ToString();
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder);
            }

            public PurchaseInvoiceManual GetPOByOrderID(string OrderID, bool type)
            {
                string select = "select * from PurchaseInvoice where id=N'" + OrderID + "' and PurchaseType=" + Convert.ToInt32(type) + "";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder).ToList().FirstOrDefault();
            }
            public List<PurchaseInvoiceManual> GetPurchaseInvoiceByUserID(int UserID)
            {
                string select = "select * from PurchaseInvoice where UserID=" + UserID + "";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder);
            }

            public List<PurchaseInvoiceManual> GetPurchaseInvoiceBySubID(int SubID)
            {
                string select = "select * from PurchaseInvoice where SubAccountId=" + SubID + "";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder);
            }
            public List<PurchaseInvoiceManual> GetOrdersByState(bool type)
            {
                string select = "select * from PurchaseInvoice where PurchaseType=" + Convert.ToInt32(type) + "";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceManual>(POrder);
            }
           
           
        }
        public class PurchaseInvoiceDetail
        {
            private ConnectFunction fun = new ConnectFunction();
            //private object  prod;
            private List<PurchaseInvoiceDetail> po;

            public int ID { set; get; }
            public string ProductID { set; get; }
            public string PurchaseInvoiceID { set; get; }
            public double Qty { set; get; }
            public Double ProductPrice { set; get; }
            public Double PricePerRecord { set; get; }
           
           
            public object Operations(string method, object content = null)
            {
                if (method == "Add")
                {
                    po = (List<PurchaseInvoiceDetail>)content;
                    //string con = $"{FieldsName.ElementAt(0).Key} + {FieldsName.ElementAt(1).Key} +{FieldsName.ElementAt(2).Key } ";
                    if (po.Count > 0)
                    {
                        foreach (var it in po)
                        {

                            string test = @"insert  into [dbo].[PurchaseInvoiceDetail]
           ([ProductID]
           ,[ProductPrice]
           ,[Qty]
           ,[PricePerRecord]
           ,[PurchaseInvoiceID])
values(N'" +it.ProductID + "'," + it.ProductPrice+ "," + it.Qty + "," + it.PricePerRecord + ",N" + it.PurchaseInvoiceID +"')";
                            try
                            {
                                fun.FireSql(test);
                            }
                            catch (Exception ex)
                            {

                                string cc = ex.Message;
                            }
                        }
                        return "Added Successfully";
                    }
                }
                else if (method == "Edit")
                {
                    po = (List<PurchaseInvoiceDetail>)content;
                    if (po.Count > 0)
                    {
                        string update;
                        foreach (var row in po)
                        {
                            DataTable dt = fun.GetData("select * from PurchaseInvoiceDetail where ID='N" + row.ID + "'");
                            if (dt.Rows.Count == 0)
                            {
                                string test = @"insert  into [dbo].[PurchaseInvoiceDetail]
           ([ProductID]
           ,[ProductPrice]
           ,[Qty]
           ,[PricePerRecord]
           ,[PurchaseInvoiceID])
values(N'" + row.ProductID + "'," + row.ProductPrice + "," + row.Qty + "," + row.PricePerRecord + ",N" + row.PurchaseInvoiceID + "')";
                                    fun.FireSql(test);
                                }
                            else
                            {
                                update = @"update [PurchaseInvoiceDetail]
   SET[ProductID] =N'"+row.ProductID+"',[ProductPrice] ="+row.ProductPrice+",[Qty] ="+row.Qty+@"
      ,[PricePerRecord] ="+row.PricePerRecord+@",[PurchaseInvoiceID] ="+row.PurchaseInvoiceID+" WHERE ID="+row.ID;
                                fun.FireSql(update);
                            }

                        }
                        return "Updated Successfully";
                    }
                }
                else if (method == "Delete")
                {
                    po = (List<PurchaseInvoiceDetail>)content;
                    if (po.Count > 0)
                    {
                        fun.FireSql("delete from PurchaseInvoiceDetail where ID=" + po.First().ID + "");
                        return "Deleted Successfully";
                    }
                }
                return "No Action";
            }
            public List<PurchaseInvoiceDetail> GetAllReDetail()
            {
                string select = "select * from PurchaseInvoiceDetail";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceDetail>(POrder);
            }
            public List<PurchaseInvoiceDetail> GetReDetailByOrderID(string OrderID, string type)
            {
                string select = "select * from PurchaseInvoiceDetail where PurchaseInvoiceID=N'" + OrderID + "'";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceDetail>(POrder);
            }
            public PurchaseInvoiceDetail GetOrdersDetailByID(int ID)
            {
                string select = "select * from PurchaseInvoiceDetail where id=" + ID + "";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceDetail>(POrder).ToList().FirstOrDefault();
            }

            public List<PurchaseInvoiceDetail> GetOrdersByProductid(string productid)
            {
                string select = "select * from PurchaseInvoiceDetail where ProductID=N'" + productid + "'";
                DataTable POrder = fun.GetData(select);
                return ConvertDT<PurchaseInvoiceDetail>(POrder);
            }
            

            public string Get_Quntity(string orderid, bool type)
            {
                return fun.FireSql("select sum(Qty) from PurchaseInvoiceDetail where PurchaseInvoiceID=N'" + orderid + "' and PurchaseType=" +Convert.ToInt32( type) + "").ToString();
            }
            public string Get_TotalLE(int orderid, string type)
            {
                return fun.FireSql("select sum(PricePerRecord) from PurchaseInvoiceDetail where PurchaseInvoiceID=N'" + orderid + "' and PurchaseType=" + Convert.ToInt32(type) + "").ToString();
            }
        }

    }
}

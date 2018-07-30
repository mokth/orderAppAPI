using ERPWebAPI.Shared;
using galaCoreAPI.Entities;
using galaCoreAPI.Shared;
using galaCoreAPI.Shared.CodeShare.Library.Passwords;
using galaEatAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MRP.BL;
using galaCoreAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using galaOrderAPI.Entities;

public class DataDbContect : DbContext
{
   
    public DbSet<CustAcct> AdUsers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<OrderHdr> OrderHdrs { get; set; }
    public DbSet<OrderDtl> OrderDtls { get; set; }
    public DbSet<NumberRunNO> NumberRunNOs { get; set; }
    public DbSet<ItemCategory> ItemCategorys { get; set; }
    public DbSet<FavouriteItem> FavouriteItems { get; set; }    

    public virtual DbSet<vItems> vItems { get; set; }
    public virtual DbSet<vOrderItems> vOrderItems { get; set; }
    public virtual DbSet<vFavourite> vFavourites { get; set; }
    public virtual DbSet<OrderSettings> OrderSetting { get; set; }


    public DataDbContect(DbContextOptions<DataDbContect> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustAcct>().ToTable("CustAcct");
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Item>().ToTable("Item");
        modelBuilder.Entity<OrderHdr>().ToTable("OrderHdr");
        modelBuilder.Entity<OrderDtl>().ToTable("OrderDtl");
        modelBuilder.Entity<NumberRunNO>().ToTable("NumberRunNO");
        modelBuilder.Entity<ItemCategory>().ToTable("ItemCategory");
        modelBuilder.Entity<FavouriteItem>().ToTable("FavouriteItem");
        modelBuilder.Entity<OrderSettings>().ToTable("OrderSettings");
        //view
        modelBuilder.Entity<vItems>(entity => { entity.HasKey(e => e.ID); });
        modelBuilder.Entity<vOrderItems>(entity => { entity.HasKey(e => e.ID); });
        modelBuilder.Entity<vFavourite>(entity => { entity.HasKey(e => e.ID); });

    }

    public bool SaveNewOrder(OrderHdr order, List<OrderDtl> items, NumberRunNO runNum, ref string msg)
    {
        bool success = false;
        using (var dbContextTransaction = this.Database.BeginTransaction())
        {
            try
            {
                Entry(order).State = EntityState.Added;
                foreach(OrderDtl item in items )
                {
                    Entry(item).State = EntityState.Added;
                }                
                Entry(runNum).State = (runNum.isNewMode) ? EntityState.Added : EntityState.Modified;

                SaveChanges(true);
                dbContextTransaction.Commit();
                success = true;
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                Console.WriteLine(ex);
                msg = ex.Message;
            }
        }
       
        return success;
    }

    public bool SaveFavouriteItems(string custcode,List<FavouriteItem> items,ref string msg)
    {
        bool success = false;
        using (var dbContextTransaction = this.Database.BeginTransaction())
        {
            try
            {
                this.Database.ExecuteSqlCommand("Delete from FavouriteItem where CustomerCode='"+custcode+"'");
                

                foreach (FavouriteItem item in items)
                {
                    Entry(item).State = EntityState.Added;
                }
                
                SaveChanges(true);
                dbContextTransaction.Commit();
                success = true;
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                Console.WriteLine(ex);
                msg = ex.Message;
            }
        }

        return success;
    }


    public bool ChangePassowrd(UserInfo user, ref string msg)
    {
        bool success = false;
        try
        {
            var found = AdUsers.Where(x => x.ID == user.name).ToList();
            if (found.Count >0)
            {
                found[0].PWord = user.fullname;
                found[0].chgpass = false;
                SaveChanges(true);
                success = true;
            }
         
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            msg = ex.InnerException.Message;
        }
        
        return success;
    }

    public bool ResetPassowrd(string userid, ICommonService configuration, string webRootPath, ref string msg)
    {
        bool success = false;
        try
        {
            var found = AdUsers.Where(x => x.ID == userid).ToList();
            if (found.Count > 0)
            {
               string tempPass = PasswordGenerator.GeneratePassword(true, true, true, false, false, 8);
               string hash = AuthHelper.HashString(tempPass);
                found[0].PWord = hash;
                found[0].chgpass = true;
                SaveChanges(true);
          
                success = EmailHelper.SendTempPassEmail(userid, tempPass, configuration, webRootPath,ref msg);
            }
            else
            {
                msg = "email not registered...";
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            msg = ex.InnerException.Message;
        }

        return success;
    }

  
    public List<T> ConvertDataTableToGenericList<T>(DataTable dt)
    {
        var columnNames = dt.Columns.Cast<DataColumn>()
               .Select(c => c.ColumnName.ToLower())
               .ToList();

        var properties = typeof(T).GetProperties();
        DataRow[] rows = dt.Select();
        return rows.Select(row =>
        {
            var objT = Activator.CreateInstance<T>();
            foreach (var pro in properties)
            {
                if (columnNames.Contains(pro.Name.ToLower()))
                    pro.SetValue(objT, row[pro.Name]);
            }

            return objT;
        }).ToList();
    }
}
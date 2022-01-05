using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Neag_Cristina_Lab2_EB.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccess = Neag_Cristina_Lab2_EB.Data;
using ModelAccess = Neag_Cristina_Lab2_EB.Models;

namespace GrpcCustomersService
{
    public class GrpcCrudService : CustomerService.CustomerServiceBase
    {
        
        private DataAccess.LibraryContext db = null;
        public GrpcCrudService(DataAccess.LibraryContext db)
        {
            this.db = db;
        }
        public override Task<CustomerList> GetAll(Empty empty, ServerCallContext
       context)
        {

            CustomerList pl = new CustomerList();
            var query = from cust in db.Customers
                        select new Customer()
                        {
                            CustomerId = cust.CustomerID,
                            Name = cust.Name,
                            Adress = cust.Adress,
                            Birthdate = cust.BirthDate.ToString()
                        };
            pl.Item.AddRange(query.ToArray());
            return Task.FromResult(pl);
        }


        public override Task<Customer> Get(CustomerId customerId, ServerCallContext
       context)
        {

            Customer custm = new Customer();
            ModelAccess.Customer dbCustomer = new ModelAccess.Customer();

            var result = from cust in db.Customers
                         where cust.CustomerID == customerId.Id
                         select cust;

            dbCustomer = result.FirstOrDefault();
            return Task.FromResult(new Customer()
            {
                CustomerId = dbCustomer.CustomerID,
                Name = dbCustomer.Name,
                Adress = dbCustomer.Adress,
                Birthdate = dbCustomer.BirthDate.ToString()
            });
        }

        public override Task<Empty> Insert(Customer requestData, ServerCallContext
       context)
        {
            db.Customers.Add(new ModelAccess.Customer
            {
                CustomerID = requestData.CustomerId,
                Name = requestData.Name,
                Adress = requestData.Adress,
                BirthDate = DateTime.Parse(requestData.Birthdate)
            });
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override Task<Customer> Update(Customer requestData, ServerCallContext
       context)
        {
             db.Customers.Update(new ModelAccess.Customer()
            {
                CustomerID = requestData.CustomerId,
                Name = requestData.Name,
                Adress = requestData.Adress,
                BirthDate = DateTime.Parse(requestData.Birthdate)
             });

            db.SaveChanges();
            return Task.FromResult(requestData);
        }

        public override Task<Empty> Delete(CustomerId customerId, ServerCallContext
       context)
        {
            ModelAccess.Customer dbCustomer = new ModelAccess.Customer();

            var result = from cust in db.Customers
                         where cust.CustomerID == customerId.Id
                         select cust;

            dbCustomer = result.FirstOrDefault();
            db.Customers.Remove(dbCustomer);
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

    }
}

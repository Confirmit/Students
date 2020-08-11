﻿
namespace BillSplitter.Data
{
    public class UnitOfWork
    {
        public readonly BillRepository Bills;
        public readonly UserRepository Users;
        public readonly CustomerRepository Customers;
        public readonly OrderRepository Orders;
        public readonly PositionsDbAccessor Positions;

        private readonly BillContext _context;

        public UnitOfWork(BillContext context)
        {
            Bills = new BillRepository(context);
            Users = new UserRepository(context);
            Customers = new CustomerRepository(context);
            Orders = new OrderRepository(context);
            Positions = new PositionsDbAccessor(context);
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
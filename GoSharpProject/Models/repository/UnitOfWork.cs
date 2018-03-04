using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models.repository
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Order> orderRepository;
        private GenericRepository<ApplicationUser> applicationRepository;
        private GenericRepository<ProjectTask> workItemRepository;
        private GenericRepository<SiteTemplate> siteTemplateRepository;
        private GenericRepository<OrderDetail> orderDetailsRepository;
        private GenericRepository<Project> projectRepository;
        private GenericRepository<CartRecord> cartRepository;
        private GenericRepository<Manager> managerRepository;
        private GenericRepository<Customer> customerRepository;

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {

                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Customer>(context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<OrderDetail> OrderDetailRepository
        {
            get
            {

                if (this.orderDetailsRepository == null)
                {
                    this.orderDetailsRepository = new GenericRepository<OrderDetail>(context);
                }
                return orderDetailsRepository;
            }
        }

        public GenericRepository<CartRecord> CartRepository
        {
            get
            {

                if (this.cartRepository == null)
                {
                    this.cartRepository = new GenericRepository<CartRecord>(context);
                }
                return cartRepository;
            }
        }

        public GenericRepository<SiteTemplate> SiteTemplateRepository
        {
            get
            {

                if (this.siteTemplateRepository == null)
                {
                    this.siteTemplateRepository = new GenericRepository<SiteTemplate>(context);
                }
                return siteTemplateRepository;
            }
        }

        public GenericRepository<Project> ProjectRepository
        {
            get
            {

                if (this.projectRepository == null)
                {
                    this.projectRepository = new GenericRepository<Project>(context);
                }
                return projectRepository;
            }
        }

        public GenericRepository<Manager> ManagerRepository
        {
            get
            {

                if (this.managerRepository == null)
                {
                    this.managerRepository = new GenericRepository<Manager>(context);
                }
                return managerRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }

        public GenericRepository<ProjectTask> WorkItemRepository
        {
            get
            {

                if (this.workItemRepository == null)
                {
                    this.workItemRepository = new GenericRepository<ProjectTask>(context);
                }
                return workItemRepository;
            }
        }

        public GenericRepository<ApplicationUser> UserRepository
        {
            get
            {

                if (this.applicationRepository == null)
                {
                    this.applicationRepository = new GenericRepository<ApplicationUser>(context);
                }
                return applicationRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
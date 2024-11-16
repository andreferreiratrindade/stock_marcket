﻿using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Framework.Core.Data
{
    public abstract class DbContextCustom<TContext> : DbContext, IUnitOfWork
         where TContext : DbContext
    {
        private readonly IEventStored _eventStored;
        private readonly IMediatorHandler _mediatorHandler;

        public DbContextCustom(DbContextOptions<TContext> options, IEventStored eventStored, IMediatorHandler mediatorHandler)
           : base(options)
        {
            _eventStored = eventStored;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<DomainEvent>();
            modelBuilder.Ignore<AggregateRoot>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;


            base.OnModelCreating(modelBuilder);
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }

        public async Task<bool> Commit()
        {
            AddCurrentDateTimeAtCreateAndUpdateDateTime();

            bool sucesso = false;

            var aggregate = ChangeTracker.Entries<AggregateRoot>().FirstOrDefault();
            if (aggregate != null)
            {
                var events = GetEventsByContext();
                CleanEventsByContext();

                //await _eventStored.SaveAsync(events, aggregate.Entity.AggregateId, "aggregateTemp");
                await _mediatorHandler.PublishEvent(events);
                sucesso = await base.SaveChangesAsync() > 0;
            }

            return sucesso;
        }

        private void AddCurrentDateTimeAtCreateAndUpdateDateTime()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                }
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;

            }
        }

        private IEnumerable<IDomainEvent> GetEventsByContext()
        {
            var domainEntities = ChangeTracker.Entries<AggregateRoot>().Where(x => x.Entity.GetUncommittedChanges().Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetUncommittedChanges());

            return domainEvents.ToList();
        }

        private void CleanEventsByContext()
        {
            var domainEntities = ChangeTracker.Entries<AggregateRoot>().Where(x => x.Entity.GetUncommittedChanges().Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.MarkChangesAsCommitted());
        }
    }
}

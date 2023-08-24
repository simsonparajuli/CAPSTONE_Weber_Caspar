using CASPAR.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity); // Like a commit
            _dbContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity); // Like a commit
            _dbContext.SaveChanges();
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities); // Like a commit
            _dbContext.SaveChanges();
        }

        // Comma seperate includes on comma (example: student,assignment)
        public virtual T Get(Expression<Func<T, bool>> predicate, bool trackChanges = false, string includes = null)
        {
            if (includes == null) //we are not joining other objects
            {
                if (!trackChanges) //if set to false we are not tracking changes
                {
                    return _dbContext.Set<T>()
                        .AsNoTracking()
                        .Where(predicate)
                        .FirstOrDefault();
                }
                else   //track changes
                {
                    return _dbContext.Set<T>()
                        .Where(predicate)
                        .FirstOrDefault();
                }
            }
            else //if there are other objects to include (join)
            {
                IQueryable<T> queryable = _dbContext.Set<T>(); //convert the set to a queryable
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }

                if (!trackChanges)
                {
                    return queryable
                        .AsNoTracking()
                        .Where(predicate)
                        .FirstOrDefault();
                }
                else
                {
                    return queryable
                        .Where(predicate)
                        .FirstOrDefault();
                }
            }
        }


        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNotTracking = false, string includes = null)

        {
            if (includes == null) // If we are not joining any objects 
            {
                if (!asNotTracking) // If tracking is false, we're not tracking changes
                {
                    return await _dbContext.Set<T>()
                             .Where(predicate)
                             .AsNoTracking()
                             .FirstOrDefaultAsync();
                }
                else
                {
                    return await _dbContext.Set<T>()
                            .Where(predicate)
                            .FirstOrDefaultAsync();
                }
            }
            else // If there are other objects to include
            {
                IQueryable<T> queryable = _dbContext.Set<T>();

                // Split includes by , (example student,assignment) and remove spaces
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable.Include(includeProperty);
                }

                if (!asNotTracking) // If tracking is false, we're not tracking changes
                {
                    return await queryable
                             .Where(predicate)
                             .AsNoTracking()
                             .FirstOrDefaultAsync();
                }
                else
                {
                    return await queryable
                              .Where(predicate)
                              .FirstOrDefaultAsync();
                }
            }
        }

        public virtual T GetById(int? id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, Expression<Func<T, int>> orderBy = null, string includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            if (predicate != null && includes == null)
            {
                return _dbContext.Set<T>()
                    .Where(predicate)
                    .AsEnumerable();
            }
            // has optional includes
            else if (includes != null)
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
				}
			}

            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queryable.AsEnumerable();
                }
                else
                {
                    return queryable.OrderBy(orderBy).ToList().AsEnumerable();
                }
            }
            else
            {
                if (orderBy == null)
                {

                    return queryable.Where(predicate).ToList().AsEnumerable();

                }
                else
                {
                    return queryable.Where(predicate).OrderBy(orderBy).ToList().AsEnumerable();
                }
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Expression<Func<T, int>> orderBy = null, string includes = null)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            if (predicate != null && includes == null)
            {
                return _dbContext.Set<T>()
                    .Where(predicate)
                    .AsEnumerable();
            }
            // has optional includes
            else if (includes != null)
            {
                foreach (var includeProperty in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includeProperty);
                }
            }

            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queryable.AsEnumerable();
                }
                else
                {
                    return await queryable.OrderBy(orderBy).ToListAsync();
                }
            }
            else
            {
                if (orderBy == null)
                {

                    return await queryable.OrderBy(orderBy).ToListAsync();

                }
                else
                {
                    return await queryable.Where(predicate).OrderBy(orderBy).ToListAsync();
                }
            }
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
		}

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>();
        }
    }
}
